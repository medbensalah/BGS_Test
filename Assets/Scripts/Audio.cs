using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance { get; private set; }
    
    private AudioSource _audioSource;
    [SerializeField] public AudioClip _bgm;
    [SerializeField] public AudioClip _success;
    [SerializeField] public AudioClip _fail;
    
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
        
        _audioSource = GetComponent<AudioSource>();
        PlayAudio();
    }
    
    public void PlayOneShot(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
    
    public void PlayAudio()
    {
        _audioSource.clip = _bgm;
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
