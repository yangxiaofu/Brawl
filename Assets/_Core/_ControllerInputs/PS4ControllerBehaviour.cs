using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core.ControllerInputs;

namespace Game.Core.ControllerInputs{
	public class PS4ControllerBehaviour : ControllerBehaviour
	{
		public delegate void ButtonPressed(PS4_Controller_Input.Button button);
		public event ButtonPressed OnButtonPressed;
		PS4_Controller_Input ps4Controller = new PS4_Controller_Input();
        public override void RegisterToController(CharacterControl control)
        {
			OnButtonPressed += control.OnButtonPressed;	
        }

		void Update()
		{
			_inputs = Vector3.zero;
			_inputs.x = Input.GetAxis("Horizontal");
			_inputs.z = Input.GetAxis("Vertical");

			if (ps4Controller.SquarePressed()) {

				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.SQUARE);

				return;
			}

			if (ps4Controller.XPressed()) {

				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.X);

				return;
			}

			if (ps4Controller.TrianglePressed()) {
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.TRIANGLE);

				return;
			}

			if (ps4Controller.CirclePressed()) {
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.CIRCLE);

				return;
			}

			if (ps4Controller.L1Pressed()){

				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.L1);

				return;
			}

			if (ps4Controller.R1Pressed()){

				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.R1);

				return;
			}

			if (ps4Controller.L2Pressed()){
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.L2);

				return;
			}

			if (ps4Controller.R2Pressed()){
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.R2);

				return;
			}

			if (ps4Controller.L3Pressed()){
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.L3);

				return;
			}

			if (ps4Controller.R3Pressed()){

				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.R3);

				return;
			}

			if (ps4Controller.OptionsPressed()){
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.OPTIONS);

				return;
			}

			if (ps4Controller.StartPressed()){
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.START);

				return;
			}

			if (ps4Controller.PSPressed()){
				
				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.PS);
					
				return;
			}
		}

    }
}


