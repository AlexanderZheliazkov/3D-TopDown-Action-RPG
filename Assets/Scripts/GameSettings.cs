using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public const int MIN_TARGET_FRAMERATE = 30;

    [SerializeField]
    [Min(30)]
    private int maxFrameRate = 240;

    private void Start()
    {
        if (maxFrameRate > MIN_TARGET_FRAMERATE)
        {
            Application.targetFrameRate = maxFrameRate;
        }
    }
}
