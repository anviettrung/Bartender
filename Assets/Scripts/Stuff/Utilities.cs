using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BM // Stand for Bartender Master
{
	public static class Utility
	{

		public static bool CompareColor(Color x, Color y)
		{
			if (Mathf.Abs(x.r - y.r) < Mathf.Epsilon
			 && Mathf.Abs(x.g - y.g) < Mathf.Epsilon
			 && Mathf.Abs(x.b - y.b) < Mathf.Epsilon) {
				return true;
			}

			return false;
		}
	}

	// Define Events
	public class EventDrink : UnityEvent<TeaDrink> { }
}
