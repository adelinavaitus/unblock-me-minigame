using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the AudioMixer for controlling audio settings

    // Function to set the volume
    public void SetVolume (float volume)
    {
        // Sets the volume in the AudioMixer using the parameter "volume"
        // "volume" is usually a value between 0 (mute) and 1 (max volume), or a logarithmic scale depending on the setup
        audioMixer.SetFloat("volume", volume);
    }
}
