using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;

    public UnityEvent onInteraction;

    Vector3 playPos = new Vector3(2.38301849f, -0.421000242f, 6.5732131f);

    IEnumerator MoveInPause()
    {
        yield return new WaitForSeconds(1.5f);
        PlayScript.instance.ActivateGame();
        Debug.Log("2 seconds passed");
    }

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
        Time.timeScale = 1f;

        FPS_Controller.instance.player.DOLocalMove(playPos, 1.5f);

        PlayScript.instance.isPlaying = true;

        StartCoroutine(MoveInPause());

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


        if (FPS_Controller.instance == null) Debug.Log("instance null");
        if (FPS_Controller.instance.player == null) Debug.Log("player null");
    }
}
