using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;


    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction?.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public void Cube()
    {
        Debug.Log("Interacted with Cube");
    }

    public void PlayMinigame()
    {
        if (PlayScript.instance == null)
        {
            PlayScript found = FindFirstObjectByType<PlayScript>();
            if (found != null)
            {
                PlayScript.instance = found;
            }
        }

        if (PlayScript.instance == null)
        {
            Debug.LogError("PlayScript.instance == null.");
            return;
        }

        PlayScript.instance.ActivateGame();
    }
}
