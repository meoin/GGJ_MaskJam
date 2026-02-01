using TMPro;
using UnityEngine;

public class CupScript : MonoBehaviour
{
    public static CupScript instance;

    [SerializeField] GameObject player;
    [SerializeField] Rigidbody rb;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] GameObject defeatPanel;
    [SerializeField] TextMeshProUGUI defeatText;
    [SerializeField] TextMeshProUGUI scoreText;
    public static int score;

    private Vector3 startPos;
    private SpriteRenderer playerSpriteRenderer;
    private Sprite playerSprite;
    private Quaternion startRotation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Duplicate CupScript found – destroying duplicate.");
            Destroy(this);
        }

        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer != null)
        {
            playerSprite = playerSpriteRenderer.sprite;
        }
    }

    private void OnEnable()
    {
        startPos = player.transform.localPosition;
        startRotation = player.transform.localRotation;
    }

    void Start()
    {
        ResetGameState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            startText.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1f)
        {
            rb.linearVelocity = Vector3.up * 5f;
        }

        scoreText.text = "Score: " + score.ToString();

        if (PlayScript.instance.isDefeat && Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Boarder"))
        {
            Debug.Log("Triggered Death");

            if (playerSpriteRenderer != null)
            {
                playerSpriteRenderer.enabled = false;
            }

            scoreText.gameObject.SetActive(false);
            defeatText.text = "You lose! Score: " + score.ToString();
            defeatPanel.gameObject.SetActive(true);
            PlayScript.instance.isDefeat = true;
            Time.timeScale = 0f;
        }
    }

    public void Exit()
    {
        Debug.Log("Exited");
        defeatPanel.SetActive(false);
        PlayScript.instance.CloseGame();
        Time.timeScale = 1f;
    }

    public void ResetGameState()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (player != null)
        {
            player.SetActive(true);
            player.transform.localPosition = new Vector3(startPos.x, -294f, startPos.z);
            player.transform.localRotation = startRotation;
        }

        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.enabled = true;
        }

        score = 0;

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "Score: " + score.ToString();
        }

        if (defeatPanel != null)
            defeatPanel.SetActive(false);

        if (defeatText != null)
            defeatText.text = string.Empty;

        if (startText != null)
        {
            startText.text = "Press SPACE to start...";
            startText.gameObject.SetActive(true);
        }
    }
}
