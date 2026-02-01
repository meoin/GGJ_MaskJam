using UnityEngine;

public class NPC : MonoBehaviour
{
    public string Node;
    [SerializeField] private TextAsset InkJSON;
    [SerializeField] private AudioClip TalkSound;

    public void TriggerDialogue() 
    {
        if (!GameManager.Instance.Dialogue.DialogueIsPlaying) 
        {
            if (TalkSound != null)
            {
                GameManager.Instance.Dialogue.EnterDialogue(InkJSON, Node, this, TalkSound);
            }
            else GameManager.Instance.Dialogue.EnterDialogue(InkJSON, Node, this);
        }
    }
}
