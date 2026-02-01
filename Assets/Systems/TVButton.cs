using UnityEngine;
using UnityEngine.Video;

public class TVButton : MonoBehaviour
{
    public VideoPlayer video;
    public MeshRenderer screen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        screen.enabled = false;
    }
    private void OnMouseUpAsButton()
    {
        Debug.Log("TV Button Pressed");
        screen.enabled = true;
        video.Play();
    }
}
