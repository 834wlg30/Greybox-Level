/*
 * William Gulick
 * Created 10/27/2021
 * Last modified: 10/27/2021
 * Plays run/walk animations
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh), typeof(Animator))]

public class AnimationController : MonoBehaviour
{

    public float runVel = 0.1f;
    public string animRunParam = "Run";
    public string animSpeedParam = "Speed";
    public float maxSpeed;
    private NavMeshAgent agent = null;
    private Animator animator = null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        maxSpeed = agent.speed;
    }//end Awake()

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool(animRunParam, agent.velocity.magnitude >= runVel);
        animator.SetFloat(animSpeedParam, agent.velocity.magnitude / maxSpeed);
    }
}
