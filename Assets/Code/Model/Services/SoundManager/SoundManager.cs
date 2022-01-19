using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _effects;
    [SerializeField] private AudioSource _music;

    [SerializeField] private List<AudioClip> _audios;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private AudioClip GetClip(string soundName)
    {
        foreach (var audio in _audios)
        {
            if (audio.name == soundName)
                return audio;
        }

        return null;
    }

    public void PlaySound(string soundName)
    {
        var audio = GetClip(soundName);
        if(audio != null)
            _effects.PlayOneShot(audio);
    }

    public void PlayMusic(string musicName)
    {
        _music.clip = GetClip(musicName);
        _music.Play();
    }

    public void Toggle(bool toggle)
    {
        _effects.mute = toggle;
        _music.mute = toggle;
    }
}