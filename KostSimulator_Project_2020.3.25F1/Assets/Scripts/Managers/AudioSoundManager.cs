using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSoundManager : MonoBehaviour
{
    public static AudioSoundManager instance;

    public AudioClip Click;
    public AudioClip Footstep;

    private AudioSource audio;

    private void Awake()
    {
        instance = this;
        audio = GetComponent<AudioSource>();
    }

    public void ClickSFX()
    {
        audio.PlayOneShot(Click);
    }

    public void FootstepSFX()
    {
        audio.PlayOneShot(Footstep);
    }


}
