using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenAnimationController : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Movement movement;
    private const string walkingAnimationKey = "Walking";

    private void Awake() {
        movement.IsMovingChaned += SetWalkingAnimationParameter;
    }

    private void SetWalkingAnimationParameter(bool value) {
        animator.SetBool(walkingAnimationKey, value);
    }

}