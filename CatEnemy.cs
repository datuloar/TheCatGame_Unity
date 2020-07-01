using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public abstract class BaseEnemy : MonoBehaviour, IDamageble
{
    [SerializeField] protected GameObject DeathEffect;
    [SerializeField] protected float Health;
    [SerializeField] protected Transform[] WayPoints;
    [SerializeField] protected float Speed;
    [SerializeField] protected float TimeToWait;

    protected float WaitCounter;
    protected int PointIndex;
    protected bool IsMooving = false;

    public void Die()
    {
        SpawnDeathEffect();
        Destroy(gameObject);
    }

    public void SpawnDeathEffect()
    {
        GameObject effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(1.5f, 2, 1, 1);
        Destroy(effect, 3f);
    }

    public  void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
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
