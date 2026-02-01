using UnityEngine;
using UnityEngine.WSA;

public class PlayScript : MonoBehaviour
{
    public static PlayScript instance;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject minigame;

    public void ActivateGame()
    {
        hud.SetActive(true);
        minigame.SetActive(true);
    }

    public void CloseGame()
    {
        hud.SetActive(false);
        minigame.SetActive(false);
    }
}
