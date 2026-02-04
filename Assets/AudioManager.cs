using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup AmbientAudioGroup;

    void Awake()
    {
        if (AmbientAudioGroup == null)
        {
            Debug.LogError("Target Mixer Group not assigned!");
            return;
        }

        // Find all Audio Sources in the scene
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        // Assign the mixer group to each Audio Source's output
        foreach (AudioSource source in allAudioSources)
        {
            source.outputAudioMixerGroup = AmbientAudioGroup;
        }

        Debug.Log($"Assigned all {allAudioSources.Length} Audio Sources to the {AmbientAudioGroup.name} group.");
    }
}
