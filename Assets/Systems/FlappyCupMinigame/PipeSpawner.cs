using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private float yPosRange;
    [SerializeField] private GameObject _pipe;

    private float timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PipeSpawn();
    }

    private void PipeSpawn()
    {
        Vector3 pipePos = transform.position + new Vector3(0, Random.Range(-yPosRange, yPosRange));
        GameObject pipe = Instantiate(_pipe, pipePos, Quaternion.identity);
        Destroy(pipe, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            PipeSpawn();
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
