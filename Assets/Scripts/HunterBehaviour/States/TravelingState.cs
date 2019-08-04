using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TravelingState : HunterState
{
    public override HunterStateType StateName => HunterStateType.Traveling;
    private Coroutine watchRoutine;

    public override void EnterState(){
        agent.SetDestination(targetManager.GetCurrentLocation().transform.position);
        watchRoutine = StartCoroutine(WatchForTarget());
    } 
    public override void ChangeRegion(ObjectRegionInfo objectRegionInfo){
        if(hunterRegionInfo.Region != objectRegionInfo.Region){
            BackgroundSoundHandler.Instance.ChangeMusic(BackGroundSounds.Normal);
            agent.SetDestination(targetManager.GetCurrentLocation().transform.position);
        }
    }
    public override void UpdateState() {
        if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
            if(hunterRegionInfo.Region == targetManager.GetCurrentLocation())
                StateMachine.SetState(HunterStateType.Searching);
            else{
                agent.SetDestination(targetManager.GetCurrentLocation().transform.position);
            }
        }
    }
    
    public override void LeaveState(){
        StopCoroutine(watchRoutine);
    }


    private IEnumerator WatchForTarget() {
        yield return null;
        while (true) {

            if(lineOfSight.IsObjectInView(targetManager.CurrentTarget))
                StateMachine.SetState(HunterStateType.Hunting);
            yield return null;
        }
    }
}
