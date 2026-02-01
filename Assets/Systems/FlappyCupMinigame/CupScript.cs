using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CupScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody rb;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] GameObject defeatPanel;
    [SerializeField] TextMeshProUGUI defeatText;
    [SerializeField] TextMeshProUGUI scoreText;
    public static int score;
    Vector3 startPos;

    private void Awake()
    {
        startPos = player.transform.position;
    }

    void Start()
    {
        player.transform.position = startPos;
        score = 0;
        defeatPanel.gameObject.SetActive(false);
        startText.text = "Press SPACE to start...";
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            startText.gameObject.SetActive(false);
            rb.AddForce(new Vector3(0, 250, 0));
        }

        scoreText.text = "Score: " + score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Triggered Death");
            Destroy(player.GetComponent<SpriteRenderer>());
            scoreText.gameObject.SetActive(false);
            defeatText.text = "You lose! Score: " + score.ToString();
            defeatPanel.gameObject.SetActive(true);

        }
    }

    public void Exit()
    {
        Debug.Log("Exited");
    }
}
