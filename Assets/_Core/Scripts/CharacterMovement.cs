using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core.ControllerInputs;

namespace Game.Core{
	public class CharacterMovement : MonoBehaviour {
		public float Speed = 5f;
		Rigidbody _body;
		Transform _groundChecker;
		public LayerMask Ground;
		public float GroundDistance = 0.2f;
		Vector3 _inputs = Vector3.zero;
		bool _isGrounded = false;
		ControllerBehaviour _controller;

		public void Setup(ControllerBehaviour controller)
		{
			_controller = controller;
		}

		// Use this for initialization
		void Start () 
		{
			_body = GetComponent<Rigidbody>();
			_groundChecker = GetComponentInChildren<GroundChecker>().transform;
		}
		
		// Update is called once per frame
		void Update ()
        {
            //check if near ground.
            CheckIfGrounded();

            _inputs = _controller.GetMovementInputs();

            if (_inputs != Vector3.zero)
                transform.forward = _inputs;
        }

        private void CheckIfGrounded()
        {
            _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        }

        void FixedUpdate()
		{
			_body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
		}
	}
}

