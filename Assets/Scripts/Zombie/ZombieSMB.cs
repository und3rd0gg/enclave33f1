using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public abstract class ZombieSMB : StateMachineBehaviour
    {
        [SerializeField] protected GameObject player;
        private GameObject _NPC;
        private ZombieAI _zombieAI;
        protected NavMeshAgent NavMeshAgent;
        protected GameObject WaypointsContainer;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _NPC = animator.gameObject;
            _zombieAI = _NPC.GetComponent<ZombieAI>();
            player = _zombieAI.Player;
            NavMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();
            WaypointsContainer = _zombieAI.WaypointsContainer;
        }
    }
}