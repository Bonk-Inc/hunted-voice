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

    public bool IsRunning { get; private set; }

    void Update() {

        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.y = movement.transform.position.y;

        rotation.LookAt(mouseWorldPos);

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            movement.Speed *= runSpeedMultiplier;
            IsRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            movement.Speed /= runSpeedMultiplier;
            IsRunning = false;
        }

    }

    private void FixedUpdate() {
        Vector3 input = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
        );

        movement.Move(input);
    }
}