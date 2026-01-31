using UnityEngine;

public class NPCTalkRange : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.Dialogue.DialogueIsPlaying) 
            {
                GameManager.Instance.Dialogue.ExitDialogueMode();
            }
        }
    }
}
