using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigs
{
}


#region Configs

[Serializable]
public class WaveData
{
    public List<Wave> Waves;
}

[Serializable]
public class Wave
{
    public List<EnemyData> EnemyData;
    public int WaveTime;
}


[Serializable]
public class EnemyData
{
    public EnemyScriptable EnemyScriptable;
    public float MaxCount;
    public float TimeToStart;

}

#endregion

#region Enums
public enum EnemyType
{
    None = 0,
    Minion = 1,
    Elite = 2,
    Boss = 3
}

#endregion