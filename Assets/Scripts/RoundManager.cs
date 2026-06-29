using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public float roundTime = 60f;
    private UIManager uiManager;
    private bool endingRound = false;
    private Board board;
    public int scoreValue;
    private float displayScore;
    private float scoreTimeDelay = 5f;
    public int scoreTarget1=300, scoreTarget2=200, scoreTarget3=100;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
    }
    private void Update()
    {
        if(roundTime>0)
        {
            roundTime -= Time.deltaTime;
            if(roundTime <= 0)
            {
                roundTime = 0;
                endingRound = true;
            }
        }
        if(endingRound && board.currentState==Board.BoardState.move)
        {
            WinCheck();
            endingRound = false;
        }
        uiManager.remainingTimeText.text = "Time Remaining: " + roundTime.ToString("0.0") + "s";
        displayScore = Mathf.Lerp(displayScore, scoreValue,  scoreTimeDelay * Time.deltaTime);
        uiManager.scoreText.text = "Score: " + displayScore.ToString("0");
    }
    private void WinCheck()
    {
        uiManager.roundOverDisplay.SetActive(true);
        uiManager.winScore.text = displayScore.ToString("0");
        if(scoreValue>=scoreTarget1)
        {
            uiManager.winText.text = "Congratulation You have Earned Three Stars";
            uiManager.winStar3.SetActive(true);
            uiManager.winStar2.SetActive(true);
            uiManager.winStar1.SetActive(true);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star2", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star3", 1);
            if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_Completed"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Completed", 1);
                ImageUnlockManager.instance.NextImage();
            }
        }
        else if (scoreValue >= scoreTarget2)
        {
            uiManager.winText.text = "Congratulation You have Earned Two Stars";
            uiManager.winStar3.SetActive(true);
            uiManager.winStar1.SetActive(true);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star2", 1);
            if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_Completed"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Completed", 1);
                ImageUnlockManager.instance.NextImage();
            }
           
        }
       else if (scoreValue >= scoreTarget3)
        {
            uiManager.winText.text = "Congratulation You have Earned One Star";
            uiManager.winStar3.SetActive(true);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Star1", 1);
            if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_Completed"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Completed", 1);
                ImageUnlockManager.instance.NextImage();
            }
            
        }
        else
        {
            uiManager.winText.text = "Oh no! You Have Earned No Star!\n Try Again!";
        }
        
        SFXManager.instance.RoundOver();
    }
}
