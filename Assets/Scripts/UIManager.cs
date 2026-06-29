using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public TMP_Text remainingTimeText;
    public TMP_Text scoreText;
    public GameObject roundOverDisplay;
    public TMP_Text winScore;
    public TMP_Text winText;
    public GameObject winStar1, winStar2, winStar3;
    private Board board;
    [SerializeField]
    private GameObject pauseScreen;
    private ImageUnlockManager imageUnlockManager;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }
    private void Start()
    {
        winStar1.SetActive(false);
        winStar2.SetActive(false);
        winStar3.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void PauseUnpause()
    {
        if (!pauseScreen.activeInHierarchy)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
    public void ShuffleBoard()
    {
        board.ShuffleBoard();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LevelSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}