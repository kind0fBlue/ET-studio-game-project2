using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{   
    // public Slider sliderthing;

    public void fullScreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    // public float getVolumeMultiplier() {
    //     return sliderthing.value;
    // }
}
