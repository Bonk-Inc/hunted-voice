using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private bool rotateLocally;

    private bool isMoving;
    public bool IsMoving {
        get { return isMoving; } private set {
            if (isMoving == value)
                return;

            isMoving = value;
            IsMovingChaned?.Invoke(value);
        }
    }

    public event Action<bool> IsMovingChaned;

    public void Move(Vector2 direction) {
        IsMoving = direction != Vector2.zero;
        var movement = direction * speed * Time.deltaTime;

        rb.AddForce(direction.normalized * speed, ForceMode2D.Force);
        //rb.transform.position += (Vector3) movement;
    }

}