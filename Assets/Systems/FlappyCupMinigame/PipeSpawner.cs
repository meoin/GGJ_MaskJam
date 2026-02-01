using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private float yPosRange;
    [SerializeField] private GameObject _pipe;

    private float timer;
    private bool hasSpawnedInitialPipe;

    private void OnEnable()
    {
        timer = 0f;
        hasSpawnedInitialPipe = false;
    }

    private void PipeSpawn()
    {
        Vector3 pipePos = transform.position + new Vector3(0, Random.Range(-yPosRange, yPosRange));
        GameObject pipe = Instantiate(_pipe, pipePos, Quaternion.identity);
        Destroy(pipe, 10);
        Debug.Log("Spawned pipe at " + pipePos);
    }

    private void Update()
    {
        if (!hasSpawnedInitialPipe)
        {
            PipeSpawn();
            hasSpawnedInitialPipe = true;
            return;
        }

        if (timer > maxTime)
        {
            PipeSpawn();
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
