using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Sightline), typeof(Animator))]

public class ChaseState : MonoBehaviour, IFSMState
{
    public float mSpeed = 2.5f;
    public float accel = 3.0f;
    public float aSpeed = 720.0f;
    public float fov = 60.0f;
    public string animRunParam = "Run";

    public FSMStateType stateName { get { return FSMStateType.Chase; } }

    private readonly float minChaseDist = 2.0f;
    private float initFOV = 0.0f;

    private NavMeshAgent agent;
    private Sightline sightline;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sightline = GetComponent<Sightline>();
        animator = GetComponent<Animator>();
    }

    public void onEnter()
    {
        initFOV = sightline.fov;
        sightline.fov = fov;

        agent.isStopped = false;
        agent.speed = mSpeed;
        agent.acceleration = accel;
        agent.angularSpeed = aSpeed;

        animator.SetBool(animRunParam, true);
    }

    public void doAction()
    {
        agent.SetDestination(sightline.lastKnownPos);
    }

    public void onExit()
    {
        agent.isStopped = true;
        sightline.fov = initFOV;
    }

    public FSMStateType shouldTransitionToState()
    {
        if(agent.remainingDistance <= minChaseDist)
        {
            Debug.Log("Target in range");
            return FSMStateType.Attack;
        }
        else if (!sightline.targetInSight)
        {
            Debug.Log("Lost sight of target");
            return FSMStateType.Patrol;
        }

        return FSMStateType.Chase;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
