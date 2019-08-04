    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : HunterState
{
    public override HunterStateType StateName => HunterStateType.Searching;

    public override void EnterState(){
        targetManager.RetrieveObjectRegionInfo().OnRegionChanged += ChangeRegion;
        BackgroundSoundHandler.Instance.ChangeMusic(BackGroundSounds.Chased);
    }

    public override void ChangeRegion(ObjectRegionInfo objectRegionInfo){
        if(hunterRegionInfo != objectRegionInfo){
            BackgroundSoundHandler.Instance.ChangeMusic(BackGroundSounds.Normal);
            StateMachine.SetState(HunterStateType.Traveling);
        }
    }
}
