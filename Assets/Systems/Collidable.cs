using UnityEngine;
using UnityEngine.Events;

public class Collidable : MonoBehaviour
{
    [SerializeField] UnityEvent onInteraction;
    public bool CanInteract = true;

    public void Interact()
    {
        if (CanInteract)
        {
            onInteraction.Invoke();
            CanInteract = false;
        }
    }

    public void CubedSphere()
    {
        Debug.Log("Collided with Cube");
    }
}
