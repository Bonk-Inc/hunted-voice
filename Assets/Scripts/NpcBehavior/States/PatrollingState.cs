using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingState : CitizenState {
    public override CitizenStateType StateName => CitizenStateType.Patrolling;

    [SerializeField]
    private Transform[] path;

    [SerializeField]
    private NavMeshAgent agent;

    private int currentDestination = 0;

    public override void EnterState() {
        SetPath();
    }

    public override void UpdateState() {
        if (agent.remainingDistance != Mathf.Infinity && agent.remainingDistance < 0.3f) {
            currentDestination++;
            currentDestination = (int) Mathf.Repeat((float) currentDestination, (float) path.Length - 1);
            SetPath();
        }
    }

    private void SetPath() {
        agent.destination = path[currentDestination].position;
    }

}