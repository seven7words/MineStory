using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Wsc.Behaviour;

namespace Wsc.Asset
{
    public class AssetBundleManager
    {
        public AssetBundleManager(IBehaviourHandler behaviourHandler)
        {
            this.asynsHandler = behaviourHandler;

            InitManifest();
        }
        ~AssetBundleManager()
        {
            ClearAssetBundle();
            ClearManifest();
        }

        #region Path
        private string AssetsNameToBundleName(string file)
        {
            string f = file.Replace('/', '.');
            f = f.ToLower();
            f += ".assetbundle";
            return f;
        }

        private string MainifestFilePath()
        {
#if UNITY_EDITOR
            return Path.Combine(Application.streamingAssetsPath, "StreamingAssets");
#else
        return Path.Combine(Application.persistentDataPath, "AssetBundle/StreamingAssets");
#endif
        }

        private string BundleNameToBundlePath(string bundleFilename)
        {
#if UNITY_EDITOR
            return Path.Combine(Application.streamingAssetsPath, bundleFilename);
#else
        return Path.Combine(Application.persistentDataPath, "AssetBundle/" + bundleFilename);
#endif
        }
        #endregion

        #region Manifest
        private AssetBundleManifest manifest = null;
        // 加载manifest，用来处理关联资源
        private void InitManifest()
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(MainifestFilePath());
            manifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            // 压缩包直接释放掉
            bundle.Unload(false);
            bundle = null;
        }
        private void ClearManifest()
        {
            Resources.UnloadAsset(manifest);
            manifest = null;
        }
        #endregion

        #region AssetBundle
        private Dictionary<string, AssetBundle> dicAssetBundle = new Dictionary<string, AssetBundle>();
        public AssetBundle GetAssetBundle(string filename)
        {
            string bundleName = AssetsNameToBundleName(filename);
            if (dicAssetBundle.ContainsKey(bundleName))
            {
                return dicAssetBundle[bundleName];
            }
            return null;
        }
        public void ClearAssetBundle()
        {
            foreach (var pair in dicAssetBundle)
            {
                var bundle = pair.Value;
                if (bundle != null)
                {
                    bundle.Unload(true);
                    bundle = null;
                }
            }
            dicAssetBundle.Clear();
        }

        #endregion

        // filename : Assets全路径，比如Assets/Prefab/***.prefab
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public AssetBundle Load(string fullPath)
        {
            string bundleName = AssetsNameToBundleName(fullPath);
            if (dicAssetBundle.ContainsKey(bundleName))
            {
                return dicAssetBundle[bundleName];
            }

            string[] dependence = manifest.GetAllDependencies(bundleName);
            for (int i = 0; i < dependence.Length; ++i)
            {
                LoadInternal(dependence[i]);
            }

            return LoadInternal(bundleName);
        }

        private AssetBundle LoadInternal(string bundleName)
        {
            if (dicAssetBundle.ContainsKey(bundleName))
            {
                return dicAssetBundle[bundleName];
            }

            string bundlePath = BundleNameToBundlePath(bundleName);
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            dicAssetBundle.Add(bundleName, bundle);
            return bundle;
        }

        private IBehaviourHandler asynsHandler;
        public IEnumerator LoadAsync(string filename, Action<AssetBundle> onFinish)
        {
            string bundleName = AssetsNameToBundleName(filename);
            if (dicAssetBundle.ContainsKey(bundleName))
            {
                yield break;
            }

            string[] dependence = manifest.GetAllDependencies(bundleName);
            var wait = new WaitHandle(dependence.Length);
            for (int i = 0; i < dependence.Length; ++i)
            {
                int index = i;
                asynsHandler.StartCoroutine(LoadInternalAsync(dependence[index], (ab) => { wait.Finish(index); }));
            }

            yield return new WaitUntil(wait.Check);
            yield return asynsHandler.StartCoroutine(LoadInternalAsync(bundleName, onFinish));
        }

        private IEnumerator LoadInternalAsync(string bundleName, Action<AssetBundle> onFinish)
        {
            if (dicAssetBundle.ContainsKey(bundleName))
            {
                if (onFinish != null) { onFinish(dicAssetBundle[bundleName]); }
                yield break;
            }
            string bundlePath = BundleNameToBundlePath(bundleName);
            AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(bundlePath);
            yield return req;

            dicAssetBundle.Add(bundleName, req.assetBundle);
            if (onFinish != null) { onFinish(dicAssetBundle[bundleName]); }
        }



        public void Unload(string filename, bool force = false)
        {
            string bundleName = AssetsNameToBundleName(filename);

            AssetBundle ab = null;
            if (dicAssetBundle.TryGetValue(bundleName, out ab) == false) return;

            if (ab == null) return;

            ab.Unload(force);
            ab = null;
            dicAssetBundle.Remove(bundleName);
        }

        public void UnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }


        public class WaitHandle
        {
            public int target;
            public int now;
            public WaitHandle(int count)
            {
                target = (1 << (count)) - 1;
                now = 0;
            }

            public void Finish(int index)
            {
                now |= 1 << index;
            }
            public bool Check()
            {
                return now == target;
            }
        }
    }
}