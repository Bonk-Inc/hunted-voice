using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingState : CitizenState {
    public override CitizenStateType StateName => CitizenStateType.Patrolling;

    [SerializeField]
    private Transform destination;

    [SerializeField]
    private NavMeshAgent navMesh;

    public override void EnterState() {
        navMesh.destination = destination.position;
    }

}