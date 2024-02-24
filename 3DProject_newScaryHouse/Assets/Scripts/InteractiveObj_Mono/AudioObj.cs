using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObj : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audios;

    public AudioClip GetAudiosByInt(int num)
    {
        return audios[num];
    }
    public AudioClip GetAudiosByName(string audioName)
    {
        AudioClip audio = null;
        for(int i = 0; i < audios.Length; i++)
        {
            if (audios[i].name == audioName)
            {
                audio = audios[i];
            }
        }
        return audio;
    }
}
