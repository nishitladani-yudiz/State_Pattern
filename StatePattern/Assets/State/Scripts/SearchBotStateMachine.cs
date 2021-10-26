using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Examples.State
{
    public class SearchBotStateMachine : StateMachineMonoBehaviour
    {
        public IdleState IdleState { get; private set; }
        public SearchState SearchState { get; private set; }
        public FoundState FoundState { get; private set; }

        [SerializeField] TargetAssigner _targetAssigner = null;
        [SerializeField] Rigidbody _rb = null;


        [SerializeField] float _rotateSpeed = 5;
        public float RotateSpeed => _rotateSpeed;
        [SerializeField] float _moveSpeed = 5;
        public float MoveSpeed => _moveSpeed;

        // if we have data that multiple states need access to, we can keep it here for them all to see
        public Vector3 TargetPosition { get; set; }

        private void Awake()
        {
            IdleState = new IdleState(this, _targetAssigner);       //send component through constructor
            SearchState = new SearchState();
            FoundState = new FoundState();
        }

        private void OnEnable()
        {
            _targetAssigner.NewTargetAcquired += OnNewTargetAcquired;
        }

        private void OnDisable()
        {
            _targetAssigner.NewTargetAcquired -= OnNewTargetAcquired;
        }

        private void Start()
        {
            ChangeState(IdleState);
        }

        void OnNewTargetAcquired(Vector3 newTarget)
        {
            Debug.Log("Acquired new target: " + newTarget.ToString());
            TargetPosition = newTarget;
        }
    }
}
