using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
	public class Calculator {
		private readonly ICalculate _icalculate;

		public Calculator(ICalculate icalculate)
		{
			_icalculate = icalculate;
		}

		public float Calculate(){
			return _icalculate.Calculate();
		}
	}
}
