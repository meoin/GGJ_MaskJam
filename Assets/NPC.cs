using UnityEngine;

public class NPC : MonoBehaviour
{
    public string Node;
    [SerializeField] private TextAsset InkJSON;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) TriggerDialogue();
    }

    private void TriggerDialogue() 
    {
        GameManager.Instance.Dialogue.EnterDialogue(InkJSON);
    }
}
