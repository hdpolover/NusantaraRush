using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject surrenderPanel;
    public GameObject pauseButton;
    
    private void Start()
    {
        pausePanel.SetActive(false);
        surrenderPanel.SetActive(false);
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Surrender()
    {
        surrenderPanel.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ConfirmSurrender()
    {
        pauseButton.SetActive(true);
        surrenderPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CancelSurrender()
    {
        pauseButton.SetActive(true);
        surrenderPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
