using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tests{
	public class PhysicsApplyForce : MonoBehaviour {
		[SerializeField ] Vector3 _force = new Vector3(10, 0, 10);
		Vector3 _startPos;
		Rigidbody _rb;
		Animator _anim;
		void Start()
		{
			_rb = GetComponent<Rigidbody>();
			_anim = GetComponent<Animator>();
			
			_startPos = this.transform.position;
		}
		public void ApplyForce()
		{
			_anim.enabled = false;
			_rb.AddForce(_force, ForceMode.Impulse);
		}

		public void Reset()
		{
			_anim.enabled = true;
			this.transform.position = new Vector3(_startPos.x, _startPos.y, _startPos.z);
		}
	}
}


