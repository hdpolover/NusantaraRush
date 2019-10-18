using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStrategyScreen : MonoBehaviour
{
    public void End(int mapIndex)
    {
        SceneManager.LoadScene(mapIndex);
    }
}
