using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Image loadingBar;
    public int sceneNumber;

    BattleSceneHandler bsh;

    private void Start()
    {
        bsh = GetComponent<BattleSceneHandler>();
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation battleScene = SceneManager.LoadSceneAsync(bsh.battleSceneChosenIndex);
        
        while (battleScene.progress < 1)
        {
            loadingBar.fillAmount = battleScene.progress;
            yield return new WaitForEndOfFrame();
        }
    }
    
}
