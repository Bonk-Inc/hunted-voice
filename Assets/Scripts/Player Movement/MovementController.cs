using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
    private Movement movement;
    [SerializeField]
    private PlayerRotation rotation;

    void Update() {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        movement.Move(input);
        rotation.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }
}