using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum MaskState 
    {
        Unmasked,
        Professional,
        Jester,
        Empathy
    }

    private MaskState[] _masks = { MaskState.Unmasked, MaskState.Professional, MaskState.Jester, MaskState.Empathy };
    private int _maskIndex = 0;

    public MaskState CurrentMask = MaskState.Unmasked;

    public GameObject Masks;
    public GameObject AnucMask;
    public bool MasksOut = false;
    private bool _swappingMasks;
    private bool _animateMasks;
    private bool _equipMasks;
    private bool _rotateMasks;
    private bool _removeMasks;
    public Transform MaskSpawn;
    public Transform MaskRise;
    public Transform MaskPlace;
    public Transform MaskRotate;
    

    private float _transitionTimer;
    public float MaskRiseTime;
    public float MaskPlaceTime;
    public float MaskRotateTime;
    public float MaskRemoveTime;
    public float MaskSwapTime;

    private Quaternion _startRotation;
    private int _degreesToRotate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AnucManager.Instance.AnucMode) 
        {
            AnucMask.SetActive(true);
        }
        else AnucMask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MasksOut && !_swappingMasks) 
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.mouseScrollDelta.y > 0)
            {
                SwapMasks(1);
            }
            if (Input.GetKeyDown(KeyCode.Q) || Input.mouseScrollDelta.y < 0)
            {
                SwapMasks(-1);
            }
        }

        if (_animateMasks)
        {
            _transitionTimer += Time.deltaTime;

            Masks.transform.localScale = Vector3.Lerp(MaskSpawn.localScale, MaskRise.localScale, _transitionTimer / MaskRiseTime);
            Masks.transform.localPosition = Vector3.Lerp(MaskSpawn.localPosition, MaskRise.localPosition, _transitionTimer / MaskRiseTime);

            if (_transitionTimer > MaskRiseTime)
            {
                _animateMasks = false;
                _equipMasks = true;
                _transitionTimer = 0;
            }
        }
        else if (_equipMasks)
        {
            _transitionTimer += Time.deltaTime;

            Masks.transform.localScale = Vector3.Lerp(MaskRise.localScale, MaskPlace.localScale, _transitionTimer / MaskPlaceTime);
            Masks.transform.localPosition = Vector3.Lerp(MaskRise.localPosition, MaskPlace.localPosition, _transitionTimer / MaskPlaceTime);

            if (_transitionTimer > MaskPlaceTime)
            {
                _equipMasks = false;
                _rotateMasks = true;
                _transitionTimer = 0;
            }
        }
        else if (_rotateMasks)
        {
            _transitionTimer += Time.deltaTime;

            Masks.transform.localRotation = Quaternion.Lerp(MaskPlace.localRotation, MaskRotate.localRotation, _transitionTimer / MaskRotateTime);

            if (_transitionTimer > MaskRotateTime)
            {
                _rotateMasks = false;
                MasksOut = true;
                _transitionTimer = 0;
            }
        }
        else if (_removeMasks)
        {
            _transitionTimer += Time.deltaTime;

            Masks.transform.localScale = Vector3.Lerp(MaskPlace.localScale, MaskSpawn.localScale, _transitionTimer / MaskRemoveTime);
            Masks.transform.localPosition = Vector3.Lerp(MaskPlace.localPosition, MaskSpawn.localPosition, _transitionTimer / MaskRemoveTime);

            if (_transitionTimer > MaskRemoveTime)
            {
                _removeMasks = false;
                Masks.SetActive(false);
                _transitionTimer = 0;
            }
        }
        else if (_swappingMasks) 
        {
            _transitionTimer += Time.deltaTime;

            Quaternion targetRotation = _startRotation * Quaternion.Euler(new Vector3(0, _degreesToRotate, 0));

            Masks.transform.rotation = Quaternion.Slerp(_startRotation, targetRotation, _transitionTimer / MaskSwapTime);

            if (_transitionTimer > MaskSwapTime)
            {
                _swappingMasks = false;
                _transitionTimer = 0;
            }
        }
    }

    public void DisplayMasks() 
    {
        Masks.SetActive(true);
        _maskIndex = 0;
        CurrentMask = MaskState.Unmasked;

        Masks.transform.localScale = MaskSpawn.localScale;
        Masks.transform.localPosition = MaskSpawn.localPosition;
        Masks.transform.localRotation = MaskSpawn.localRotation;

        _animateMasks = true;
        _transitionTimer = 0;
    }

    public void HideMasks() 
    {
        MasksOut = false;

        _removeMasks = true;
        _transitionTimer = 0;
    }

    private void SwapMasks(int direction) 
    {
        _maskIndex += direction;
        if (_maskIndex >= _masks.Length) _maskIndex = 0;
        else if (_maskIndex < 0) _maskIndex = _masks.Length - 1;

        CurrentMask = _masks[_maskIndex];
        _degreesToRotate = 90 * direction;
        _startRotation = Masks.transform.rotation;
        _swappingMasks = true;
        _transitionTimer = 0;
        Debug.Log(CurrentMask);
    }
}
