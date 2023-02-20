using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakSound : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip[] _blockBreakSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBreakSound()
    {
        int randomNum = Random.Range(0, _blockBreakSound.Length);
        _audioSource.PlayOneShot(_blockBreakSound[randomNum]);
    }
}
