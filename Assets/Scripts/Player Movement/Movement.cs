using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float speed = 1;

    private bool isMoving;
    public bool IsMoving {
        get { return isMoving; } private set {
            if (isMoving == value)
                return;

            isMoving = value;
            IsMovingChanged?.Invoke(value);
        }
    }

    public float Speed { get => speed; set => speed = value; }

    public event Action<bool> IsMovingChanged;

    public void Move(Vector3 direction) {
        IsMoving = direction != Vector3.zero;
        rb.AddForce(direction.normalized * speed, ForceMode.Force);
    }

}