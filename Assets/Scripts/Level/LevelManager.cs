using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    protected LevelHandler Handler;

    #region EventHandler
    protected void InitialLevelSetupEventHandler(IntialLevelSetUpEvent e)
    {
        Handler.CurrentWave = Handler.LevelDetails.WaveData.Waves[Handler.CurrentWaveCount];
        Handler.WaveCount.text = "WaveCount: " + (Handler.CurrentWaveCount + 1).ToString();
        Handler.CoinCount.text = GlobalManager.Instance.TotalCoins.ToString();
    }
    #endregion

    #region Functions

    #endregion
}
