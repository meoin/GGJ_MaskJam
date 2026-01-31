using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum MaskState 
    {
        Unmasked,
        Professional,
        Jester,
        Empathy
    }

    public MaskState CurrentMask = MaskState.Unmasked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) CurrentMask = MaskState.Unmasked;
        if (Input.GetKeyDown(KeyCode.Alpha2)) CurrentMask = MaskState.Professional;
        if (Input.GetKeyDown(KeyCode.Alpha3)) CurrentMask = MaskState.Jester;
        if (Input.GetKeyDown(KeyCode.Alpha4)) CurrentMask = MaskState.Empathy;
        if (Input.anyKeyDown) Debug.Log(CurrentMask);
    }
}
