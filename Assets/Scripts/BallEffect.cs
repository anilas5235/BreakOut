using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallEffect : MonoBehaviour
{
    private Ball Ball;
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;
    public AudioClip[] hitsounds;

    private void Awake()
    {
        Ball = gameObject.GetComponent<Ball>();
        _particleSystem = GameObject.Find("HitEffect").GetComponent<ParticleSystem>();
        _audioSource = Ball.GetComponentInChildren<AudioSource>();
    }
    
    private void OnEnable()
    {
        Ball.OnContactPointChange += OnContactPointChange;
    }

    private void OnDisable()
    {
        Ball.OnContactPointChange -= OnContactPointChange;
    }

    private void OnContactPointChange(Vector3 contactPoint)
    {
        
        _particleSystem.gameObject.transform.position = contactPoint;
        _particleSystem.Play();
        _audioSource.PlayOneShot(hitsounds[(int) Random.Range(0,hitsounds.Length)]); }
}
