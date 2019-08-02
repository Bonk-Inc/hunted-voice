using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 1;

    public void Move(Vector2 direction) {
        var movement = direction * speed * Time.deltaTime;
        rb.transform.Translate(movement);
    }

}