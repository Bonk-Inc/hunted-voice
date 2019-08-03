using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField]
    private Movement movement;
    [SerializeField]
    private PlayerRotation rotation;

    [SerializeField]
    private float runSpeedMultiplier = 2;

    void Update() {

        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = movement.transform.position.z;

        rotation.LookAt(mouseWorldPos);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            movement.Speed *= runSpeedMultiplier;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            movement.Speed /= runSpeedMultiplier;

    }

    private void FixedUpdate() {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        movement.Move(input);
    }
}