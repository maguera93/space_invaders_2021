using System;
using MAG.Model;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy, GameObject> onImpact = delegate { };
    public event Action<Enemy> onDie = delegate { };

    protected Transform cachedTransform;
    protected EnemyModel model;
    public EnemyModel Model => model;
    
    public virtual void Setup(EnemyModel model)
    {
        this.model = model;
        model.die += OnModelDie;
        cachedTransform = transform;
    }

    private void OnModelDie(EnemyModel obj)
    {
        onDie(this);
    }

    private void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        var pos = cachedTransform.position;
        pos.z -= model.Speed * Time.deltaTime;
        cachedTransform.position = pos;
    }

    public void TakeDamage(float damage)
    {
        model.TakeDamage(damage);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        onImpact.Invoke(this, other.gameObject);
    }
}