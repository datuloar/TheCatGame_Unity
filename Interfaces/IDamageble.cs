using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageble
{
    void TakeDamage(float damage);
    void Die();
    void SpawnDeathEffect();
}
