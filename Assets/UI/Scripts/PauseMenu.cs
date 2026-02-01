using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenuUI;
    public static bool isPaused = false;
  

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();

            }
        }


    }

    public void PauseGame()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f; //FreezeMode
        AudioListener.pause = true;
        isPaused = true;

    }

    public void ResumeGame()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f; //Unfreeze Dawg
        AudioListener.pause = false;
        isPaused = false;
    }

    public void ReturntoMenu()
    {
       // Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("quitinggame");
        Application.Quit();
        
        
    }
}
