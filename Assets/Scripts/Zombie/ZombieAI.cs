using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class ZombieAI : MonoBehaviour
    {
        private static readonly int IsChasing = Animator.StringToHash("IsChasing");
        private static readonly int ChasingDistance = Animator.StringToHash("ChasingDistance");
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject waypointsContainer;
        private Animator _animator;

        public GameObject WaypointsContainer => waypointsContainer;

        public GameObject Player => player;

        private void Start()
        {
            var navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updateUpAxis = false;
            navMeshAgent.updateRotation = false;
            _animator = gameObject.GetComponent<Animator>();
            gameObject.transform.rotation = quaternion.identity;
        }

        private void Update()
        {
            _animator.SetFloat(ChasingDistance,
                Vector2.Distance(gameObject.transform.position, player.transform.position));
        }
    }
}