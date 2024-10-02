using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

public class LevelController : LevelManager, IController,ITick
{
    public LevelController(LevelHandler handler)
    {
        Handler = handler;
    }
    public void OnInitialized()
    {
        EventManager.Instance.TriggerEvent(new IntialLevelSetUpEvent());
    }

    public void OnRegisterListeners()
    {
        EventManager.Instance.AddListener<IntialLevelSetUpEvent>(InitialLevelSetupEventHandler);
    }

    public void OnRelease()
    {
    }

    public void OnRemoveListeners()
    {
        EventManager.Instance.RemoveListener<IntialLevelSetUpEvent>(InitialLevelSetupEventHandler);

    }

    public void OnStarted()
    {
    }

    public void OnUpdate()
    {
        EventManager.Instance.TriggerEvent(new EnemySpawnEvent(Handler.CurrentWave,Handler.Timer));
    }

    public void OnVisible()
    {
    }
}
