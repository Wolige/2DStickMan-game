using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    public Slider Slider;
    private static int sceneIndex;
    public void SaveIndex(int index)
    {
        sceneIndex = index;
    }
    public void LoadLevel()
    {
        StartCoroutine(LoadAsynchronously());
    }
    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progressFloat = Mathf.Clamp01(operation.progress / 0.9f);
            Slider.value = progressFloat;
            Debug.Log(progressFloat);
            yield return null;
        }
    }
}
