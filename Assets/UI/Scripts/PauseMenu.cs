using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausemenuUI;
    public static bool isPaused = false;
    public AudioSource AudioManager;
    public AudioSource PauseAudio;

   
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
        AudioManager.Pause();
        PauseAudio.Play();

        FPS_Controller.instance.Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausemenuUI.SetActive(true);
        //Time.timeScale = 0f; //FreezeMode
        //AudioListener.pause = true;
        isPaused = true;

    }

    public void ResumeGame()
    {
        AudioManager.UnPause();
        FPS_Controller.instance.Paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f; //Unfreeze Dawg
        //AudioListener.pause = false;
        isPaused = false;
    }

    public void ReturntoMenu()
    {
        FPS_Controller.instance.Paused = false;
        Debug.Log("Returning to menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("quitinggame");
        Application.Quit();
        
        
    }
}
