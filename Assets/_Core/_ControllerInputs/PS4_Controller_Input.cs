using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.ControllerInputs{
	public class PS4_Controller_Input {
		const string DPAD_V = "PS4_DPAD_VERTICAL";
		const string DPAD_H = "PS4_DPAD_HORIZONTAL";
		const string ANALOG_V = "PS4_ANALOG_VERTICAL";
		const string ANALOG_H = "PS4_ANALOG_HORIZONTAL";
		const string SQUARE_BUTTON = "PS4_SQUARE";
		const string X_BUTTON = "PS4_X";
		const string TRIANGLE_BUTTON = "PS4_TRIANGLE";
		const string CIRCLE_BUTTON = "PS4_CIRCLE";
		const string R1_BUTTON = "PS4_R1";
		const string L1_BUTTON = "PS4_L1";
		const string R2_BUTTON = "PS4_R2";
		const string L2_BUTTON = "PS4_L2";
		const string L3_BUTTON = "PS4_L3";
		const string R3_BUTTON = "PS4_R3";
		const string PS4_BUTTON = "PS4_PS4BUTTON";
		const string OPTIONS_BUTTON = "PS4_OPTIONS";
		const string START_BUTTON = "PS4_START";		
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
		public float GetDPadVertical()
		{
			return Input.GetAxis(DPAD_V);
		}

		public float GetDPadHorizontal()
		{
			return Input.GetAxis(DPAD_H);
		}

		public float GetAnalogVertical()
		{
			return Input.GetAxis(ANALOG_V);
		}

		public float GetAnalogHorizontal()
		{
			return Input.GetAxis(ANALOG_H);
		}

		public bool SquarePressed()
		{
			return Input.GetButton(SQUARE_BUTTON);
		}

		public bool XPressed()
		{
			return Input.GetButton(X_BUTTON);
		}

		public bool TrianglePressed()
		{
			return Input.GetButton(TRIANGLE_BUTTON);
		}

		public bool CirclePressed()
		{
			return Input.GetButton(CIRCLE_BUTTON);
		}

		public bool L1Pressed()
		{
			return Input.GetButton(L1_BUTTON);
		}

		public bool R1Pressed()
		{
			return Input.GetButton(R1_BUTTON);
		}

		public bool PSPressed(){
			return Input.GetButton(PS4_BUTTON);
		}

		public bool L2Pressed()
		{
			return Input.GetButton(L2_BUTTON);
		}

		public bool R2Pressed()
		{
			return Input.GetButton(R2_BUTTON);
		}

		public bool L3Pressed()
		{
			return Input.GetButton(L3_BUTTON);
		}

		public bool R3Pressed()
		{
			return Input.GetButton(R3_BUTTON);
		}

		public bool OptionsPressed()
		{
			return Input.GetButton(OPTIONS_BUTTON);
		}

		public bool StartPressed()
		{
			return Input.GetButton(START_BUTTON);
		}
	}

}
