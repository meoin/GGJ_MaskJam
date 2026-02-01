using Ink.Runtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI TextUI;
    [SerializeField] private TextMeshProUGUI MicrotextUI;
    [SerializeField] private GameObject ResponseTimerBar;
    [SerializeField] private float WaitTimeAfterDialogue;
    [SerializeField] private float CharactersPerSecond;
    [SerializeField] private float WaitTimeForResponse;

    private Story _currentStory;
    public bool DialogueIsPlaying;
    private NPC _currentNPC;

    private string _textToDisplay;
    private string _fullText;
    private float _textTimeline;
    private int _charactersToType;
    private bool _typing;
    private bool _waiting;
    private bool _responding;

    public float MicrotextTime;
    public float MicrotextSpeed;
    private float _microtextTimer;
    private bool _microtextShowing;
    private Vector3 _microtextStart;
    
    private float _timeWaited;
    private float _defaultTypingSpeed;

    private AudioSource _audioSource;
    public AudioClip DefaultAudio;
    private AudioClip _npcAudio;

    private GetAnimaticImage _animatics;
    public Image AnimaticBackground;
    public Image AnimaticImage;
    private bool _endingAnimaticPlaying;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animatics = GetComponent<GetAnimaticImage>();
        _defaultTypingSpeed = CharactersPerSecond;
        _audioSource.clip = DefaultAudio;
    }

    void Start()
    {
        DialogueIsPlaying = false;
        TextUI.gameObject.SetActive(false);
        ResponseTimerBar.SetActive(false);
        _microtextStart = MicrotextUI.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Don't do anything if dialogue isn't playing
        if (!DialogueIsPlaying) return;

        if (_microtextShowing) 
        {
            _microtextTimer += Time.deltaTime;

            Color textColor = MicrotextUI.color;
            textColor.a = Mathf.Min(_microtextTimer, 1);
            MicrotextUI.color = textColor;

            MicrotextUI.transform.position = new Vector3(_microtextStart.x, _microtextStart.y + _microtextTimer * MicrotextSpeed, _microtextStart.z);

            if (_microtextTimer >= MicrotextTime) 
            {
                MicrotextUI.text = "";
                _microtextShowing = false;
                ContinueStory();
            }
        }

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
                //Debug.Log($"Percentage: {timeLeftPercentage}");
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

    private void SetMicrotext(string text) 
    {
        MicrotextUI.text = text;
        Color textColor = MicrotextUI.color;
        textColor.a = 0;
        MicrotextUI.color = textColor;
        _microtextTimer = 0;
        _microtextShowing = true;
        MicrotextUI.transform.position = _microtextStart;
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

    public void EnterDialogue(TextAsset inkJSON, string node, NPC currentNPC) 
    {
        if (!GameManager.Instance.Player.MasksOut) 
        {
            GameManager.Instance.Player.DisplayMasks();
        }

        _endingAnimaticPlaying = false;
        _currentNPC = currentNPC;
        if (AnucManager.Instance.AnucMode) inkJSON = AnucManager.Instance.AnucStory;
        _currentStory = new Story(inkJSON.text);

        DialogueIsPlaying = true;
        TextUI.gameObject.SetActive(true);

        if (node == "") node = "Start";

        _currentStory.ChoosePathString(node);

        ContinueStory();
    }

    public void EnterDialogue(TextAsset inkJSON, string node, NPC currentNPC, AudioClip talkSound)
    {
        _npcAudio = talkSound;

        EnterDialogue(inkJSON, node, currentNPC);
    }

    public void ExitDialogueMode()
    {
        if (GameManager.Instance.Player.MasksOut)
        {
            GameManager.Instance.Player.HideMasks();
        }

        Debug.Log("Exited dialogue");
        SaveNode();
        SetText("");
        DialogueIsPlaying = false;
        TextUI.gameObject.SetActive(false);
        ResponseTimerBar.SetActive(false);
        _currentNPC = null;
        _npcAudio = null;

        if (_endingAnimaticPlaying) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void ContinueStory() 
    {
        
        //Debug.Log("Can continue? " +  _currentStory.canContinue);

        if (_currentStory.canContinue)
        {
            string text = _currentStory.Continue();
            if (_currentStory.currentTags.Contains("mt"))
            {
                SetText("");
                SetMicrotext(text);
            }
            else if (_currentStory.currentTags.Contains("narration"))
            {
                SetText(text, DefaultAudio);
            }
            else if (_currentStory.currentTags.Contains("animatic")) 
            {
                
                Sprite sprite = _animatics.GetImage(text);
                AnimaticBackground.gameObject.SetActive(true);
                AnimaticImage.sprite = sprite;
                ContinueStory();
            }
            else
            {
                if (_npcAudio != null)
                {
                    SetText(text, _npcAudio);
                }
                else SetText(text);

            }
                
            SaveNode();
        }
        else if (_currentStory.currentChoices.Count > 0) 
        {
            PromptResponse();
        }
        else
        {
            ExitDialogueMode();
            SaveNode();
        }
    }

    private void SaveNode() 
    {
        string node = GetCurrentNode();
        if (node != null)
        {
            Debug.Log(node);
            _currentNPC.Node = node;
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
        SaveNode();
    }

    public string GetCurrentNode()
    {
        string currentPath = _currentStory.state.currentPathString;

        if (!string.IsNullOrEmpty(currentPath))
        {
            string[] pathComponents = currentPath.Split('.');
            if (pathComponents.Length > 0)
            {
                return pathComponents[0];
            }
        }

        return null;
    }
}
