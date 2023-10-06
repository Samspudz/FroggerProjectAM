using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] soundFX;
    [SerializeField] AudioClip[] bgMusic;

    [SerializeField] AudioSource _audi;

    void Start()
    {
        _audi = GetComponent<AudioSource>();
    }
}
