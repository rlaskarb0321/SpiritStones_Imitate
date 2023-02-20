using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSound : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip[] _attackAudioClip; // Warrior, Archer, Thief, Magician 순서로 공격시 사용할 AudioClip 저장
    public float _attackSoundVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAttackSound(eNormalBlockType job)
    {
        _audioSource.PlayOneShot(_attackAudioClip[(int)job], _attackSoundVolume);
    }
}
