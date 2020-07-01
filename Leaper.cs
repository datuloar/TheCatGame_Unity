using System;
using System.Collections.Generic;
using UnityEngine;
using ShakeCam;

[RequireComponent(typeof(Rigidbody))]
public class Leaper : MonoBehaviour, IDamageble
{
    public event Action OnLeaperDeath;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Light _light;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _deathEffect;

    public void Shoot()
    {
        Instantiate(_projectile, transform.position + Vector3.right, Quaternion.identity);
    }

    public void Die()
    {
        OnLeaperDeath?.Invoke();
        SpawnDeathEffect();
        CameraShaker.Instance.ShakeOnce(1.5f, 2, 1, 1);
        _renderer.enabled = false;
        _light.enabled = false;
        _rigidbody.isKinematic = true; ;
        enabled = false;
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void SpawnDeathEffect()
    {
        GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, Game.EffectLifeTime);
    }
}
