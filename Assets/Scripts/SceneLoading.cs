using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Image loadingBar;

    private void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation battleScene = SceneManager.LoadSceneAsync(12);
        
        while (battleScene.progress < 1)
        {
            loadingBar.fillAmount = battleScene.progress;
            yield return new WaitForEndOfFrame();
        }
    }
    
}
