    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : HunterState
{
    public override HunterStateType StateName => HunterStateType.Searching;
    private Coroutine watchRoutine;

    [SerializeField]
    private Transform[] waypoints;

    public override void EnterState(){
        targetManager.RetrieveObjectRegionInfo().OnRegionChanged += ChangeRegion;
        BackgroundSoundHandler.Instance.ChangeMusic(BackGroundSounds.Chased);
        watchRoutine = StartCoroutine(WatchForTarget());

        //waypoints = add function
    }

    public override void ChangeRegion(ObjectRegionInfo objectRegionInfo){
        if(hunterRegionInfo != objectRegionInfo){
            BackgroundSoundHandler.Instance.ChangeMusic(BackGroundSounds.Normal);
            StateMachine.SetState(HunterStateType.Traveling);
        }
    }

    public override void UpdateState(){
        if (agent.remainingDistance != Mathf.Infinity && agent.remainingDistance < 0.3f) {
            int number = Random.Range(0,waypoints.Length);
            agent.SetDestination(waypoints[number].position);
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
