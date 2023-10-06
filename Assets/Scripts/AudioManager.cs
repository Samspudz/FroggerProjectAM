using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    public AudioClip[] soundFX;
    public AudioClip[] bgMusic;

    [SerializeField] AudioSource _audi;

    void Start()
    {
        _audi = GetComponent<AudioSource>();
    }
}
