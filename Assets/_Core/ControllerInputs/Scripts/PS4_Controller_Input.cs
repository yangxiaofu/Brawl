using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.ControllerInputs{
	[System.Serializable]
	public class PS4_Controller_Input {
		private readonly string _prefix;
		[SerializeField] string DPAD_V = "PS4_DPAD_VERTICAL";
		[SerializeField] string DPAD_H = "PS4_DPAD_HORIZONTAL";
		[SerializeField] string R_ANALOG_V = "PS4_R_ANALOG_VERTICAL";
		[SerializeField] string R_ANALOG_H = "PS4_R_ANALOG_HORIZONTAL";
		[SerializeField] string L_ANALOG_V = "PS4_L_ANALOG_VERTICAL";
		[SerializeField] string L_ANALOG_H = "PS4_L_ANALOG_HORIZONTAL";
		[SerializeField] string SQUARE_BUTTON = "PS4_SQUARE";
		[SerializeField] string X_BUTTON = "PS4_X";
		[SerializeField] string TRIANGLE_BUTTON = "PS4_TRIANGLE";
		[SerializeField] string CIRCLE_BUTTON = "PS4_CIRCLE";
		[SerializeField] string R1_BUTTON = "PS4_R1";
		[SerializeField] string L1_BUTTON = "PS4_L1";
		[SerializeField] string R2_BUTTON = "PS4_R2";
		[SerializeField] string L2_BUTTON = "PS4_L2";
		[SerializeField] string L3_BUTTON = "PS4_L3";
		[SerializeField] string R3_BUTTON = "PS4_R3";
		[SerializeField] string PS4_BUTTON = "PS4_PS4BUTTON";
		[SerializeField] string OPTIONS_BUTTON = "PS4_OPTIONS";
		[SerializeField] string START_BUTTON = "PS4_START";		
		public enum Button{
			TRIANGLE,
			X,
			SQUARE,
			CIRCLE,
			L1,
			R1,
			L2,
			R2,
			L3,
			R3,
			OPTIONS,
			START,
			PS
		}

		public PS4_Controller_Input(string prefix)
		{
			_prefix = prefix;
		}
		public float GetDPadVertical()
		{
			return Input.GetAxis(_prefix + "_" + DPAD_V);
		}

		public float GetDPadHorizontal()
		{
			return Input.GetAxis(_prefix + "_" + DPAD_H);
		}

		public float GetLeftAnalogVertical()
		{
			return Input.GetAxis(_prefix + "_" + L_ANALOG_V);
		}

		public float GetLeftAnalogHorizontal()
		{
			return Input.GetAxis(_prefix + "_" + L_ANALOG_H);
		}

		public float GetRightAnalogVertical()
		{
			return Input.GetAxis(_prefix + "_" + R_ANALOG_V);
		}

		public float GetRightAnalogHorizontal()
		{
			return Input.GetAxis(_prefix + "_" + R_ANALOG_H);
		}

		public bool SquarePressed()
		{
			return Input.GetButtonDown(_prefix + "_" + SQUARE_BUTTON);
		}

		public bool SquareReleased()
		{
			return Input.GetButtonUp(_prefix + "_" + SQUARE_BUTTON);
		}

		public bool XPressed()
		{
			return Input.GetButtonDown(_prefix + "_" + X_BUTTON);
		}

		public bool TrianglePressed()
		{
			return Input.GetButtonDown(_prefix + "_" + TRIANGLE_BUTTON);
		}

		public bool CirclePressed()
		{
			return Input.GetButtonDown(_prefix + "_" + CIRCLE_BUTTON);
		}

		public bool L1Pressed()
		{
			return Input.GetButtonDown(_prefix + "_" + L1_BUTTON);
		}

		public bool R1Pressed()
		{
			return Input.GetButtonDown(_prefix + "_" + R1_BUTTON);
		}

		public bool PSPressed(){
			return Input.GetButtonDown(_prefix + "_" + PS4_BUTTON);
		}

		public bool L2Pressed()
		{
			return Input.GetButtonDown(_prefix + "_" + L2_BUTTON);
		}

		public bool R2Pressed()
		{
			return Input.GetButtonDown(_prefix + "_" + R2_BUTTON);
		}

		public bool L3Pressed()
		{
			return Input.GetButtonDown(_prefix + "_" + L3_BUTTON);
		}

		public bool R3Pressed()
		{
			return Input.GetButtonDown(_prefix + "_" + R3_BUTTON);
		}

		public bool OptionsPressed()
		{
			return Input.GetButtonDown(_prefix + "_" + OPTIONS_BUTTON);
		}

		public bool StartPressed()
		{
			return Input.GetButtonDown(_prefix + "_" + START_BUTTON);
		}
	}

}
