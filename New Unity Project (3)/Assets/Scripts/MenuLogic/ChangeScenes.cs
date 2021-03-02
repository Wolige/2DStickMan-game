using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public GameObject[] scenes;
    public void OnNextButton()
    {
        if (scenes == null) return;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].activeSelf)
            {
                scenes[i].SetActive(false);
                if (i + 1 < scenes.Length)
                {
                    scenes[i + 1].SetActive(true);
                    return;
                }
                else scenes[0].SetActive(true);
            }
        }
    }
    public void OnBackButton()
    {
        if (scenes == null) return;
        for (int i = scenes.Length - 1; i >= 0; i--)
        {
            if (scenes[i].activeSelf)
            {
                scenes[i].SetActive(false);
                if (i - 1 >= 0)
                {
                    scenes[i - 1].SetActive(true);
                    return;
                }
                else scenes[scenes.Length - 1].SetActive(true);
            }
        }
    }
}
