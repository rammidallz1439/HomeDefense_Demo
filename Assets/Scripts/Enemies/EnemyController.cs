using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

public class EnemyController : EnemyManager, IController
{
    public EnemyController(EnemyHandler handler)
    {
        Handler = handler;
    }
    public void OnInitialized()
    {
    }

    public void OnRegisterListeners()
    {
        EventManager.Instance.AddListener<EnemySpawnEvent>(EnemySpawnEventHandler);
    }

    public void OnRelease()
    {
    }

    public void OnRemoveListeners()
    {
        EventManager.Instance.RemoveListener<EnemySpawnEvent>(EnemySpawnEventHandler);

    }

    public void OnStarted()
    {
    }


    public void OnVisible()
    {
    }
}
