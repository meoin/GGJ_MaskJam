using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KonamiCode : MonoBehaviour
{

    public Vector2[] konami;
    int currentKonami = 0;
    bool canKonami = true;

    public string cheatCode;

    public TMP_InputField textField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textField.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (input == konami[currentKonami] && canKonami)
        {
            //Debug.Log($"AAAAAAAAA THE INPUT IS {input.x}, {input.y} USE THIS TO SEE WHAT THE INPUT YOU ARE GETTING IS AND POPULATE THE konami ARRAY");

            canKonami = false;

            currentKonami++;

            if (currentKonami == konami.Length)
            {
                //Open the TextBox
                currentKonami = 0;
                textField.gameObject.SetActive(true);
            }
        }
        else if (input != Vector2.zero && canKonami)
        {
            //Debug.Log($"BBBBBBBBB THE INPUT IS {input.x}, {input.y} USE THIS TO SEE WHAT THE INPUT YOU ARE GETTING IS AND POPULATE THE konami ARRAY");

            canKonami = false;

            currentKonami = 0;
        }
        else if (input == Vector2.zero)
        {
            canKonami = true;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            var text = textField.text;

            if (text.ToLower() == cheatCode.ToLower())
            {
                Debug.Log("ENTERED ANUC MODE");
                AnucManager.Instance.AnucMode = true;
            }

            textField.text = "";
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            textField.gameObject.SetActive(false);
        }
    }
}