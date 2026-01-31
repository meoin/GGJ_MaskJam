using UnityEngine;

public class NPC : MonoBehaviour
{
    public string Node;
    [SerializeField] private TextAsset InkJSON;

    public void TriggerDialogue() 
    {
        if (!GameManager.Instance.Dialogue.DialogueIsPlaying) 
        {
            GameManager.Instance.Dialogue.EnterDialogue(InkJSON, Node);
        }
    }
}
