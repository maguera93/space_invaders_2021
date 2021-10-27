using System.Collections;
using System.Collections.Generic;
using MAG.Model;
using UnityEngine;

public class EnemySnake : Enemy
{
    private Vector3 startPosition;

    public override void Setup(EnemyModel model)
    {
        base.Setup(model);
        startPosition = transform.position;
    }

    protected override void Movement()
    {
        var pos = transform.position;
        pos.z -= model.Speed * Time.deltaTime;
        pos.x = startPosition.x + Mathf.Sin(pos.z);
        transform.position = pos;
    }
}
