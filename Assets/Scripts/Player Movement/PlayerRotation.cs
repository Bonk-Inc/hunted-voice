using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;

    public void LookAt(Vector3 position) {
        var direction = position - transform.position;
        direction.y = 0;
        var angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
        rb.transform.rotation = Quaternion.Euler(0, angle, rb.transform.rotation.z);
    }

}