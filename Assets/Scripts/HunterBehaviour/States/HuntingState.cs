using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntingState : HunterState
{
    [SerializeField]
    private float minDistance = 3;

    [SerializeField]
    private Canvas gameOverScreen;

    public override HunterStateType StateName => HunterStateType.Hunting;

    public override void UpdateState() {
        agent.SetDestination(targetManager.CurrentTarget.transform.position);
    }

    public override void Reason(){
        if (agent.remainingDistance != Mathf.Infinity && agent.remainingDistance <= minDistance) {
            if(!targetManager.CheckIfNpc()){
                gameOverScreen.enabled = true;
                BackgroundSoundHandler.Instance.ChangeMusic(BackGroundSounds.GameOver);
                return;
            }
            var oldTarget = targetManager.CurrentTarget;
            targetManager.CurrentTarget = targetManager.GetNewTarget(); 
            Destroy(oldTarget);

            BackgroundSoundHandler.Instance.ChangeMusic(BackGroundSounds.Normal);
            StateMachine.SetState(HunterStateType.Traveling);
        }
    }
}
