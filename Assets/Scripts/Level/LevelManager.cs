using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    protected LevelHandler Handler;

    #region EventHandler
    protected void InitialLevelSetupEventHandler(IntialLevelSetUpEvent e)
    {
        Handler.CurrentWave = Handler.LevelDetails.WaveData.Waves[Handler.CurrentWaveCount - 1];
        Handler.WaveCount.text = "Wave: " + Handler.CurrentWaveCount + "/" + Handler.LevelDetails.WaveData.Waves.Count;
        Handler.CoinCount.text = GlobalManager.Instance.TotalCoins.ToString();
        Handler.Timer = Handler.CurrentWave.WaveTime;


    }

    protected void BaseSelectedEventHandler(BaseSelectedEvent e)
    {
        if (Handler.CurrentSelectedBase == null)
        {
            Handler.CurrentSelectedBase = e.BaseHandler;
        }
        {
            Handler.CurrentSelectedBase.transform.DOMoveY(Handler.CurrentSelectedBase.InitialYPos, 0.5f);
            Handler.CurrentSelectedBase.transform.GetComponent<MeshRenderer>().material.color = Handler.CurrentSelectedBase.GetColor();
            Handler.CurrentSelectedBase = e.BaseHandler;

        }

        e.BaseHandler.transform.DOMoveY(1f, 0.5f);
        e.BaseHandler.transform.GetComponent<MeshRenderer>().material.color = Color.green;
        Handler.SelectionPanel.gameObject.SetActive(true);
    }

    protected void SpawnTurretEventHandler(SpawnTurretEvent e)
    {
        if (Handler.CurrentSelectedBase != null && Handler.CurrentSelectedBase.Occupied == false)
        {
            Handler.CurrentSelectedBase.transform.DOMoveY(Handler.CurrentSelectedBase.InitialYPos, 0.5f).OnComplete(() =>
            {
                GameObject obj = MonoHelper.Instance.InstantiateObject(e.Turret, Handler.CurrentSelectedBase.SpawnPoint.transform.position, Quaternion.identity);
                Handler.CurrentSelectedBase.Occupied = true;
                ShootingMachine machine = e.Turret.transform.GetComponent<ShootingMachine>();
                Vault.ObjectPoolManager.Instance.InitializePool(machine.TurretDataScriptable.Bullet.gameObject, 15);
            });

        }
    }

    protected void UpdateTimerEventHandler(UpdateTimerEvent e)
    {
        if (Handler.CurrentWaveCount <= Handler.LevelDetails.WaveData.Waves.Count)
        {
            if (Handler.Timer > 0)
            {
                Handler.Timer -= Time.deltaTime;
                UpdateTimerDisplay(Handler.Timer);
            }
            else
            {

                Handler.Timer = 0;
                TimerEnded();
                UpdateTimerDisplay(Handler.Timer);
            }

        }

    }

    #endregion

    #region Functions
    void UpdateTimerDisplay(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        Handler.TimerCount.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        Handler.CurrentWaveCount++;
        Handler.WaveCount.text = "Wave: " + Handler.CurrentWaveCount + "/" + Handler.LevelDetails.WaveData.Waves.Count;
        Handler.CurrentWave = Handler.LevelDetails.WaveData.Waves[Handler.CurrentWaveCount - 1];
        Handler.Timer = Handler.CurrentWave.WaveTime;
    }
    #endregion
}
