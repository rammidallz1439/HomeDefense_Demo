using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [Header("Level Details")]
    public LevelScriptable LevelDetails;
    public Wave CurrentWave = null;

    [Space(10)]
    [Header("Data")]
    public float Timer;
    public int CurrentWaveCount;

    [Space(10)]
    [Header("UI")]
    public TMP_Text TimerCount;
    public TMP_Text WaveCount;
    public TMP_Text CoinCount;

    [Space(10)]
    [Header("GameObjects")]
    public Transform HouseSlider;
}
