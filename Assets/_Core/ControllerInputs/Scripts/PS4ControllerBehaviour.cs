using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core.ControllerInputs;
using Game.Characters;

namespace Game.Core.ControllerInputs{
	public class PS4ControllerBehaviour : ControllerBehaviour
	{

		[SerializeField] PS4_Controller_Input ps4Controller;

		public delegate void ButtonPressed(PS4_Controller_Input.Button button);
		public event ButtonPressed OnButtonPressed;
		void Start()
		{
			ps4Controller = new PS4_Controller_Input(_prefix);
			OnButtonPressed += _character.OnButtonPressed;
		}

		void Update()
		{
			_inputs = Vector3.zero;
			_inputs.x = GetLeftStickHorizontal();
			_inputs.z = GetLeftStickVertical();

			if (ps4Controller.SquarePressed()) {

				if (OnButtonPressed != null)
					OnButtonPressed(PS4_Controller_Input.Button.SQUARE);

				return;
			}

			if (ps4Controller.XPressed()) 
			{
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
        public override float GetRightStickVertical()
        {
            return ps4Controller.GetRightAnalogVertical();
        }

        public override float GetRightStickHorizontal()
        {
            return ps4Controller.GetRightAnalogHorizontal();
        }

        public override float GetDigitalPadVertical()
        {
            return ps4Controller.GetDPadVertical();
        }

        public override float GetDigitalPadHorizontal()
        {
            return ps4Controller.GetDPadHorizontal();
        }

        public override float GetLeftStickVertical()
        {
            return ps4Controller.GetLeftAnalogVertical();
        }

        public override float GetLeftStickHorizontal()
        {
            return ps4Controller.GetLeftAnalogHorizontal();
        }
    }
}


