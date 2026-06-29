using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool hasLastLevelCompleted = false;
   public void StartGame()
    {
        //if(PlayerPrefs.GetInt("First time",0) == 0)
        //{
        //    PlayerPrefs.SetInt("First time", 1);
        //    PlayerPrefs.DeleteAll();
        //}
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
