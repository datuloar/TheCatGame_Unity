using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShakeCam;

public abstract class CatEnemy : MonoBehaviour, IDamageble
{
    [SerializeField] protected GameObject _deathEffect;
    [SerializeField] protected float _health;
    [SerializeField] protected Transform[] _wayPoints;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _timeToWait;

    protected float _waitCounter;
    protected int _pointIndex;
    protected bool _isMooving = false;

    public void Die()
    {
        SpawnDeathEffect();
        Destroy(gameObject);
    }

    public void SpawnDeathEffect()
    {
        GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(1.5f, 2, 1, 1);
        Destroy(effect, 3f);
    }

    public  void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    protected abstract void Move();
    protected abstract void Wait();

    protected void OnCollisionEnter(Collision collision)
    {
        Leaper leaper = collision.gameObject.GetComponent<Leaper>();
        if (leaper != null)
        {
            leaper.Die();
        }
    }
}
