using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BattleSceneHandler : MonoBehaviour
{
    public Image loadingBar;

    public int[] battleSceneIndices;

    private int missionProgress;
    public int battleSceneChosenIndex;

    private void Awake()
    {
        missionProgress = PlayerManager.instance.missionProgress;
    }

    private void Start()
    {
        ChooseBattleSceneTemplate();
        StartCoroutine(LoadAsyncScene());
    }

    void ChooseBattleSceneTemplate()
    {
        switch (missionProgress)
        {
            case 1:battleSceneChosenIndex = battleSceneIndices[0];
                break;
            case 2:
            case 3:
                battleSceneChosenIndex = battleSceneIndices[1];
                break;
            case 4:
            case 5:
            case 6:
                battleSceneChosenIndex = battleSceneIndices[2];
                break;
            case 7:
            case 8:
            case 9:
                battleSceneChosenIndex = battleSceneIndices[3];
                break;
            case 10:
            case 11:
            case 12:
                battleSceneChosenIndex = battleSceneIndices[4];
                break;
            case 13:
            case 14:
            case 15:
                battleSceneChosenIndex = battleSceneIndices[5];
                break;
        }
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation battleScene = SceneManager.LoadSceneAsync(battleSceneChosenIndex);

        while (battleScene.progress < 1)
        {
            loadingBar.fillAmount = battleScene.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
