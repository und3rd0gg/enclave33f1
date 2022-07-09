using UnityEngine;

namespace Zombie
{
    public class Patrol : ZombieSMB
    {
        private int _currentPatrolPoint;
        private Transform[] _waypointsTransforms;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            _waypointsTransforms = new Transform[WaypointsContainer.transform.childCount];
            for (var i = 0; i < WaypointsContainer.transform.childCount; i++)
                _waypointsTransforms[i] = WaypointsContainer.transform.GetChild(i);
            _currentPatrolPoint = Random.Range(0, WaypointsContainer.transform.childCount);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            NavMeshAgent.SetDestination(_waypointsTransforms[_currentPatrolPoint].position);
            if (Vector2.Distance(animator.gameObject.transform.position,
                _waypointsTransforms[_currentPatrolPoint].position) < 0.3f)
            {
                _currentPatrolPoint++;
                if (_currentPatrolPoint == WaypointsContainer.transform.childCount) _currentPatrolPoint = 0;
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        //
        // }

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
}