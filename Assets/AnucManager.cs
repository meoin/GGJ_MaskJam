using Ink.Parsed;
using UnityEngine;

public class AnucManager : MonoBehaviour
{
    public static AnucManager Instance { get; private set; }


    [Header("System References")]
    public bool AnucMode = false;
    public TextAsset AnucStory; 



    private void Awake()
    {
        // Simple singleton setup for a single-scene game
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
