using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TravelingState : HunterState
{
    public override HunterStateType StateName => HunterStateType.Traveling;

    public override void EnterState(){
        agent.SetDestination(targetManager.GetCurrentLocation().transform.position);
    } 

    public override void UpdateState() {
        if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
            StateMachine.SetState(HunterStateType.Searching);
        }
        
    }
}
