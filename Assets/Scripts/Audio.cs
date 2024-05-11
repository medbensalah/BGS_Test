using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance { get; private set; }
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayAudio(int index)
    {
        _audioSource.clip = _audioClips[index];
        _audioSource.Play();
    }
    
    public void StopAudio()
    {
        _audioSource.Stop();
    }
    
    public void PauseAudio()
    {
        _audioSource.Pause();
    }
    
    public void UnPauseAudio()
    {
        _audioSource.UnPause();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
