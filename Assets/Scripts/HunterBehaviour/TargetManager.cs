using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private HunterStateMachine stateMachine;

    private CitizenInfo citizenInfo;
    [SerializeField]
    private GameObject currentTarget;
    public GameObject CurrentTarget{get{return currentTarget;} 
    set{ChangeTarget(value);}}

    [SerializeField]
    private Region backupRegion;

    public Region GetCurrentLocation(){
        if(RetrieveObjectRegionInfo().Region == null)
            return backupRegion;
        return RetrieveObjectRegionInfo().Region;
    }

    public ObjectRegionInfo RetrieveObjectRegionInfo(){
        return currentTarget.GetComponent<ObjectRegionInfo>();
    }

    public bool CheckIfNpc(){
        bool isNpc = true;
        if(citizenInfo != null)
            isNpc = citizenInfo.IsNpc;
        return isNpc;
    }

    public GameObject GetNewTarget(){
        return citizenInfo.NewTarget;
    }

    private void ChangeTarget(GameObject newtarget){
        currentTarget = newtarget;
        citizenInfo = newtarget.GetComponent<CitizenInfo>();
        RetrieveObjectRegionInfo().OnRegionChanged -= stateMachine.CurrentState.ChangeRegion;
    }
}
