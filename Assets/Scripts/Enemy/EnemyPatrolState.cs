using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{
    private Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponentInParent<Enemy>();
        enemy.DetermineNextWaypoint(); // sets the destination
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.Agent.SetDestination(enemy.GetCurrentWaypoint()); // tell them where to go
        // if we reach a waypoint
        if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
        {
            animator.SetBool("isPatrolling", false); // back to idle
        }
        else if (enemy.GetDistanceFromPlayer() < enemy.ChaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
