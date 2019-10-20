using UnityEngine;

public class BattleTutorial : MonoBehaviour
{
    public GameObject[] tutorials;
    public int currentIndex;

    public GameObject ite;

    private void Start()
    {
        currentIndex = 1;
        int ind = currentIndex;
        while (ind < tutorials.Length)
        {
            tutorials[ind].SetActive(false);
            ind++;
        }

        Time.timeScale = 0f;
    }

    public void MoveToNextTutorial()
    {
        tutorials[currentIndex].SetActive(true);
        tutorials[currentIndex - 1].SetActive(false);
        currentIndex++;

        if (currentIndex == tutorials.Length)
        {
            tutorials[currentIndex - 1].SetActive(false);

            Time.timeScale = 1f;
        }
    }
}
