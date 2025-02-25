using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _lifeWay = 10.0f;

    public event Action<Enemy> EndWay;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        StartWay();
    }

    public void ResetSpeed()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void AddSpeed(Vector3 speed)
    {
        _rigidbody.velocity = speed;
    }

    private void StartWay()
    {
        Invoke(nameof(NotifyWayEnd), _lifeWay);
    }

    private void NotifyWayEnd()
    {
        EndWay?.Invoke(this);
    }
}
