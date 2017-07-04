using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wsc.Behaviour;
using Wsc.Input;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    private BehaviourManager behaviourManager;
    private InputManager inputManager;

    private void Init()
    {
        behaviourManager = new BehaviourManager();
        inputManager = new InputManager(behaviourManager);
    }
}
