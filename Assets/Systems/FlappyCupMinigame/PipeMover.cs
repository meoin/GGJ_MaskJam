using UnityEngine;

public class PipeMover : MonoBehaviour
{
    [SerializeField] private float pipeSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * pipeSpeed * Time.deltaTime;
    }
}
