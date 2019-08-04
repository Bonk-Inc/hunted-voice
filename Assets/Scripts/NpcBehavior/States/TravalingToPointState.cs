using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TravalingToPointState : CitizenState {
    public override CitizenStateType StateName => CitizenStateType.TravelingToPoint;

    [SerializeField]
    private Transform hunterSummonPoint;

    [SerializeField]
    private NavMeshAgent agent;

    public override void EnterState() {
        agent.destination = hunterSummonPoint.position;
    }

    public override void UpdateState() {
        if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
            StateMachine.SetState(CitizenStateType.Patrolling);
        }
    }

}