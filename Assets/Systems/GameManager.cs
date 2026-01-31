using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("System References")]
    public Dialogue Dialogue;
    public Player Player;



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
    }
}
