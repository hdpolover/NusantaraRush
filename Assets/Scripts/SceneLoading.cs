using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Image loadingBar;
    public int sceneNumber;

    private void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation battleScene = SceneManager.LoadSceneAsync(sceneNumber);
        
        while (battleScene.progress < 1)
        {
            loadingBar.fillAmount = battleScene.progress;
            yield return new WaitForEndOfFrame();
        }
    }
    
}
