using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //assign the button, the desired sound
    //and a audio source
    public Button button;
    public AudioClip sound;
    public AudioSource source;

    public void Start()
    {
        //attaches the audio source from the GameApp
        //game object as it persists
        source = FindObjectOfType<AudioSource>();
        button.onClick.AddListener(PlaySound);
    }

    //play the sound on button click
    public void PlaySound()
    {
        source.PlayOneShot(sound);
    }
}
