using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSound : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip _pickSound; // 블럭을 이을때 나는 소리 저장할 예정
    public float _audioVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBlockPickUpSound()
    {
        _audioSource.PlayOneShot(_pickSound, _audioVolume);
    }
}
