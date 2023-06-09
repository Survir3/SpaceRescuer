using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.IsDie += OnDeadPlayer;
    }

    private void OnDisable()
    {
        _player.IsDie -= OnDeadPlayer;

    }

    private void OnDeadPlayer()
    {
        _audioSource.Play();
    }

}
