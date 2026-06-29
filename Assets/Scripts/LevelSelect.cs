using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject gameEndPanel;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetTheGame()
    {
        PlayerPrefs.DeleteAll();
        gameEndPanel.SetActive(false);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
