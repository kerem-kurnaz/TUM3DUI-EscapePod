using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float _bulletSpeed = 10f;
    private bool _canMove = false;
    private Rigidbody _rb;

    private void Awake()
    {
        _canMove = false;
    }

    private void Start()
    {
        GetRb();
    }

    public void StartBulletMovement(float speed)
    {
        GetRb();
        _bulletSpeed = speed;
        _canMove = true;
        Move();
    }

    private void Move()
    {
        if (_canMove)
        {
            Debug.Log("Move!");
            var direction = transform.forward;
            _rb.velocity = direction * _bulletSpeed;
        }
    }

    private void GetRb()
    {
        if (_rb) return;
        _rb = GetComponent<Rigidbody>();
        if (_rb) return;
        Debug.LogError("Could not find bullet's rigidbody!");
        _canMove = false;
    }
}
