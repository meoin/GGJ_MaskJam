using TMPro;
using System;
using UnityEngine;
using Ink.Runtime;

public class Dialogue : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI TextUI;
    [SerializeField] private GameObject ResponseTimerBar;
    [SerializeField] private float WaitTimeAfterDialogue;
    [SerializeField] private float CharactersPerSecond;
    [SerializeField] private float WaitTimeForResponse;

    private Story _currentStory;
    public bool DialogueIsPlaying;

    private string _textToDisplay;
    private string _fullText;
    private float _textTimeline;
    private int _charactersToType;
    private bool _typing;
    private bool _waiting;
    private bool _responding;
    
    private float _timeWaited;
    private float _defaultTypingSpeed;

    private AudioSource _audioSource;
    public AudioClip DefaultAudio;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _defaultTypingSpeed = CharactersPerSecond;
        _audioSource.clip = DefaultAudio;
    }

    void Start()
    {
        DialogueIsPlaying = false;
        TextUI.gameObject.SetActive(false);
        ResponseTimerBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Don't do anything if dialogue isn't playing
        if (!DialogueIsPlaying) return;

        if (_fullText != "" && _fullText != _textToDisplay)
        {
            TypeText();
        }
        else if (_fullText == _textToDisplay && _typing) 
        {
            if (_currentStory.currentChoices.Count > 0)
            {
                _typing = false;
                ContinueStory();
            }
            else 
            {
                _typing = false;
                _waiting = true;
                _timeWaited = 0;
            }
        }
        
        if (_waiting || _responding) 
        {
            _timeWaited += Time.deltaTime;

            if (_responding)
            {
                float timeLeftPercentage = (WaitTimeForResponse - _timeWaited) / WaitTimeForResponse;
                Debug.Log($"Percentage: {timeLeftPercentage}");
                ResponseTimerBar.transform.localScale = new Vector3(timeLeftPercentage, 1, 1);
            }

            if (_waiting && _timeWaited >= WaitTimeAfterDialogue)
            {
                _waiting = false;
                ContinueStory();
            }
            else if (_responding && _timeWaited >= WaitTimeForResponse) 
            {
                _responding = false;
                ResponseTimerBar.SetActive(false);
                ReadResponse();
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

            TextUI.text = _textToDisplay;
        }
    }

    public void SetText(string text) 
    {
        _typing = true;
        _waiting = false;
        _textToDisplay = "";
        _fullText = text;
        _textTimeline = 0;
        _charactersToType = 0;
        CharactersPerSecond = _defaultTypingSpeed;
        _audioSource.clip = DefaultAudio;

        if (text == "")
        {
            TextUI.text = "";
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

    public void EnterDialogue(TextAsset inkJSON, string node) 
    {
        _currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        TextUI.gameObject.SetActive(true);

        if (node == "") node = "Start";

        _currentStory.ChoosePathString(node);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        Debug.Log("Exited dialogue");

        SetText("");
        DialogueIsPlaying = false;
        TextUI.gameObject.SetActive(false);
    }

    private void ContinueStory() 
    {
        Debug.Log("Can continue? " +  _currentStory.canContinue);

        if (_currentStory.canContinue)
        {
            SetText(_currentStory.Continue());
        }
        else if (_currentStory.currentChoices.Count > 0) 
        {
            PromptResponse();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void PromptResponse() 
    {
        ResponseTimerBar.SetActive(true);
        ResponseTimerBar.transform.localScale = new Vector3(1, 1, 1);
        _responding = true;
        _timeWaited = 0;
    }

    private void ReadResponse() 
    {
        Debug.Log(GameManager.Instance.Player.CurrentMask);

        _currentStory.ChooseChoiceIndex((int)GameManager.Instance.Player.CurrentMask);
        SetText(_currentStory.Continue());
    }
}
