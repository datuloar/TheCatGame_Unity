using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExplosionPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _explosions;
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        int i = Random.Range(0, _explosions.Length);
        _source.clip = _explosions[i];
        _source.Play();
    }
}
