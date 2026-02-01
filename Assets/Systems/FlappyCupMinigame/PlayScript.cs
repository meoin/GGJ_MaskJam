using UnityEngine;

public class PlayScript : MonoBehaviour
{
    public static PlayScript instance;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject minigame;

    public bool isPlaying { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Duplicate PlayScript found – destroying duplicate.");
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    public void ActivateGame()
    {
        if (hud == null || minigame == null)
        {
            Debug.LogError("PlayScript: 'hud' or 'minigame' are not assigned.");
            return;
        }

        DestroyAllPipes();

        hud.SetActive(true);
        minigame.SetActive(true);

        if (CupScript.instance != null)
        {
            CupScript.instance.ResetGameState();
        }

        Time.timeScale = 0f;

        isPlaying = true;
    }

    public void CloseGame()
    {
        if (hud == null || minigame == null)
        {
            Debug.LogError("PlayScript: 'hud' or 'minigame' are not assigned.");
            return;
        }

        DestroyAllPipes();

        hud.SetActive(false);
        minigame.SetActive(false);

        Time.timeScale = 1f;

        isPlaying = false;
    }

    private void DestroyAllPipes()
    {
        GameObject[] allPipes = GameObject.FindGameObjectsWithTag("Pipe");
        GameObject[] allScoringPipes = GameObject.FindGameObjectsWithTag("ScoringPipe");
        foreach (GameObject pipe in allPipes)
        {
            DestroyImmediate(pipe);
        }
        foreach (GameObject scoringPipe in allScoringPipes)
        {
            DestroyImmediate(scoringPipe);
        }
    }
}
