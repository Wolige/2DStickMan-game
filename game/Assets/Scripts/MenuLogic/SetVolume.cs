using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetMusicLevel(float sliderValue)
    {
        if (Mathf.Approximately(sliderValue, 0))
        {
            mixer.SetFloat("MusicVol", -80);
            return;
        }
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetUIEffectsLevel(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("MusicVol", -80);
            return;
        }
        mixer.SetFloat("UIVol", Mathf.Log10(sliderValue) * 20);
    }
}
