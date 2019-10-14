using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public static int sceneNumber;

    void Start()
    {
        if (sceneNumber == 0)
        {
            StartCoroutine(ToTeamScreen());
        }
        else if (sceneNumber == 1)
        {
            StartCoroutine(ToStartScreen());
        }
    }

    private IEnumerator ToTeamScreen()
    {
        yield return new WaitForSeconds(2.0f);
        sceneNumber = 1;
        SceneManager.LoadScene(1);

    }

    private IEnumerator ToStartScreen()
    {
        yield return new WaitForSeconds(2.0f);
        sceneNumber = 2;
        SceneManager.LoadScene(2);

    }
}
