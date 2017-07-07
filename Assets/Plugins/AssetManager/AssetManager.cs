using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wsc.Behaviour;

namespace Wsc.Asset
{
    public class AssetManager
    {
        public AssetManager(IBehaviourHandler behaviourHandler)
        {
            abManager = new AssetBundleManager(behaviourHandler);
        }
        ~AssetManager()
        {
            abManager = null;
        }

        public AssetBundleManager abManager;
    }
}