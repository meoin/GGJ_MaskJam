using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    
    public void StartGame()
    {
        //Not sure if its just a scene switch so thats what I'll do for now
        Debug.Log("StartGame");
        SceneManager.LoadScene(2);

    }
    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
