using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : BaseEnemy
{
    private void Start()
    {
        int direction = Random.value > .5f? -1: 1;
        Speed *= direction;
    }

    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.RotateAround(WayPoints[0].position, Vector3.back, Speed * Time.deltaTime);
    }

    protected override void Wait()
    {
        throw new System.NotImplementedException();
    }
}
