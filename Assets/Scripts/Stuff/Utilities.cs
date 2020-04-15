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

		public static Color Lerp(Color x, Color y, float w)
		{
			return new Color(
				Mathf.Lerp(x.r, y.r, w),
				Mathf.Lerp(x.g, y.g, w),
				Mathf.Lerp(x.b, y.b, w),
				Mathf.Lerp(x.a, y.a, w)
			);
		}

		public static IEnumerator LerpColorOverTime(Color fromC, Color toC, float t, System.Action<Color> setter)
		{
			float elapsed = 0;
			do {
				float w = elapsed / t;

				setter(Color.Lerp(fromC, toC, w));

				yield return new WaitForEndOfFrame();
				elapsed += Time.deltaTime;
			} while (elapsed < t);
		}
	}

	// Define Events
	public class EventDrink : UnityEvent<TeaDrink> { }
}
