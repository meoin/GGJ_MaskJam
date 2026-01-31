using UnityEngine;
using UnityEngine.Events;

public class Collidable : MonoBehaviour
{
    [SerializeField] UnityEvent onInteraction;

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void CubedSphere()
    {
        Debug.Log("Collided with Cube");
    }
}
