using TMPro;
using System;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private AudioSource _audioSource;
    public TextMeshProUGUI UIText;
    private string _textToDisplay;
    private string _fullText;
    private float _textTimeline;
    private int _charactersToType;
    private bool _typing;
    private bool _waiting;
    public float WaitTimeAfterDialogue;
    private float _timeWaited;
    private float _defaultTypingSpeed;
    public float CharactersPerSecond;

    public AudioClip DefaultAudio;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _defaultTypingSpeed = CharactersPerSecond;
        _audioSource.clip = DefaultAudio;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            SetText("This is a test string to see if the dialogue works");
        }

        if (_fullText != "" && _fullText != _textToDisplay)
        {
            TypeText();
        }
        else if (_fullText == _textToDisplay && _typing) 
        {
            _typing = false;
            _waiting = true;
            _timeWaited = 0;
        }
        
        if (_waiting) 
        {
            _timeWaited += Time.deltaTime;

            if (_timeWaited >= WaitTimeAfterDialogue) 
            {
                _waiting = false;
                SetText("");
            }
        }
    }

    private void TypeText() 
    {
        _textTimeline = _textTimeline + (Time.deltaTime * CharactersPerSecond);

        if (Math.Floor(_textTimeline) > _charactersToType)
        {
            if (_fullText[_charactersToType] != ' ') _audioSource.Play();

            _charactersToType = Math.Min((int)Math.Floor(_textTimeline), _fullText.Length);

            _textToDisplay = _fullText.Substring(0, _charactersToType);

            UIText.text = _textToDisplay;
        }
    }

    public void SetText(string text) 
    {
        _typing = true;
        _textToDisplay = "";
        _fullText = text;
        _textTimeline = 0;
        _charactersToType = 0;
        CharactersPerSecond = _defaultTypingSpeed;
        _audioSource.clip = DefaultAudio;

        if (text == "")
        {
            UIText.text = "";
            _typing = false;
        }
    }

    public void SetText(string text, float speed)
    {
        SetText(text);
        CharactersPerSecond = speed;
    }

    public void SetText(string text, AudioClip audio)
    {
        SetText(text);
        _audioSource.clip = audio;
    }

    public void SetText(string text, float speed, AudioClip audio)
    {
        SetText(text);
        CharactersPerSecond = speed;
        _audioSource.clip = audio;
    }
}
