using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core.ControllerInputs;
using Game.Characters;
using System;

namespace Game.Core.ControllerInputs{
	public class PS4ControllerBehaviour : ControllerBehaviour
	{
		[SerializeField] PS4_Controller_Input ps4Controller;
		
		public delegate void ButtonDown(PS4_Controller_Input.Button button);
		public event ButtonDown OnButtonDown;

        public delegate void ButtonUp(PS4_Controller_Input.Button button);
        public event ButtonUp OnButtonUp;
        
		void Start()
		{
			InitializeControllerPrefix();

			ps4Controller = new PS4_Controller_Input(_prefix);

            if (_character) {//TODO: Remove after debuggin movement. 
			    OnButtonDown += _character.OnButtonDown;
                OnButtonUp += _character.OnButtonUp;
            }
		}

        void Update()
        {
            UpdateControllerInput();

            if (ps4Controller.SquarePressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.SQUARE);

                return;
            }

            if (ps4Controller.SquareReleased())
            {
                if (OnButtonUp != null)
                    OnButtonUp(PS4_Controller_Input.Button.SQUARE);
                
                return;
            }

            if (ps4Controller.XPressed())
            {
                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.X);

                return;
            }

            if (ps4Controller.TrianglePressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.TRIANGLE);

                return;
            }

            if (ps4Controller.CirclePressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.CIRCLE);

                return;
            }

            if (ps4Controller.L1Pressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.L1);

                return;
            }

            if (ps4Controller.R1Pressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.R1);

                return;
            }

            if (ps4Controller.L2Pressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.L2);

                return;
            }

            if (ps4Controller.R2Pressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.R2);

                return;
            }

            if (ps4Controller.L3Pressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.L3);

                return;
            }

            if (ps4Controller.R3Pressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.R3);

                return;
            }

            if (ps4Controller.OptionsPressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.OPTIONS);

                return;
            }

            if (ps4Controller.StartPressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.START);

                return;
            }

            if (ps4Controller.PSPressed())
            {

                if (OnButtonDown != null)
                    OnButtonDown(PS4_Controller_Input.Button.PS);

                return;
            }
        }

        private void UpdateControllerInput()
        {
            _inputs = Vector3.zero;
            _inputs.x = GetLeftStickHorizontal();
            _inputs.z = GetLeftStickVertical();
			
			Assert.IsTrue(Mathf.Abs(_inputs.x) <= 1.0f, "The magnitude of inputs is " + _inputs.magnitude);
			Assert.IsTrue(Mathf.Abs(_inputs.z) <= 1.0f, "The magnitude of inputs is " + _inputs.magnitude);
			Assert.IsTrue(_inputs.y == 0, "The y axis of the left stick should always remain at zero.");
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


