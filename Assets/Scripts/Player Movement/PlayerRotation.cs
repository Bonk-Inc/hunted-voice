using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rb;

    public void LookAt(Vector2 position) {
        var direction = position - (Vector2) transform.position;
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        rb.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}