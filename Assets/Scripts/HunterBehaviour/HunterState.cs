using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class HunterState : State<HunterStateType>
{
    [SerializeField]
    protected TargetManager targetManager;

    [SerializeField]
    protected ObjectRegionInfo hunterRegionInfo; 

    [SerializeField]
    protected NavMeshAgent agent;

    
}
