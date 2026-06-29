using UnityEngine;
using UnityEngine.UI;

public class ImageUnlockManager : MonoBehaviour
{
    public static ImageUnlockManager instance;
    public GameObject[] imagetoUnlock;
    public  int currentIndex = 0;
    [SerializeField]
    private GameObject gameEndPanel;
    
    private void Start()
    {
        MainMenu.hasLastLevelCompleted = PlayerPrefs.GetInt("HasLastLevelCompleted", 0) == 1;
        instance = this;
        currentIndex = PlayerPrefs.GetInt("ImageUnlockIndex", currentIndex);
        ActivateCurrentImage();
    }
    private void Update()
    {
        if(currentIndex >= 0 && MainMenu.hasLastLevelCompleted)
        {
            gameEndPanel.SetActive(true);
        }
    }

    private void ActivateCurrentImage()
    {
        for (int i = 0; i < currentIndex; i++)
            {
                if (imagetoUnlock[i] != null)
                {

                    imagetoUnlock[i].SetActive(false);
                }
            }
        if (currentIndex < imagetoUnlock.Length && currentIndex >= 0)
        {
            if (imagetoUnlock[currentIndex] != null)
            {
                imagetoUnlock[currentIndex].SetActive(true);
            }
        }       
    }
    public void NextImage()
    {

        if (currentIndex >= 0 && currentIndex < (imagetoUnlock.Length))
        {
            currentIndex++;
            PlayerPrefs.SetInt("ImageUnlockIndex", currentIndex);
            ActivateCurrentImage();
        }
        else
        {
            MainMenu.hasLastLevelCompleted= true;
            PlayerPrefs.SetInt("HasLastLevelCompleted", MainMenu.hasLastLevelCompleted ? 1 : 0);
        }
    }
}