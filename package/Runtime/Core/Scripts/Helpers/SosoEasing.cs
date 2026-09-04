using UnityEngine;

namespace Soso.UI.Core.Helpers
{
	public static class SosoEasing
	{
		// Smoothstep
		public static float Cubic(float t) => t * t * (3.0f - 2.0f * t);

		// Quadratic
		public static float EaseInQuad(float t) => t * t;
		public static float EaseOutQuad(float t) => t * (2f - t);
		public static float EaseInOutQuad(float t) => t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;

		// Cubic
		public static float EaseInCubic(float t) => t * t * t;
		public static float EaseOutCubic(float t) => (t - 1f) * (t - 1f) * (t - 1f) + 1f;
		public static float EaseInOutCubic(float t) => t < 0.5f ? 4f * t * t * t : 4f * (t - 1f) * (t - 1f) * (t - 1f) + 1f;

		// Quartic
		public static float EaseInQuart(float t) => t * t * t * t;
		public static float EaseOutQuart(float t) => 1f - (t - 1f) * (t - 1f) * (t - 1f) * (t - 1f);
		public static float EaseInOutQuart(float t) => t < 0.5f ? 8f * t * t * t * t : 1f - 8f * (t - 1f) * (t - 1f) * (t - 1f) * (t - 1f);

		// Back
		private const float C1 = 1.70158f;
		private const float C2 = C1 * 1.525f;
		private const float C3 = C1 + 1f;

		public static float EaseInBack(float t) => C3 * t * t * t - C1 * t * t;
		public static float EaseOutBack(float t) => 1f + C3 * (t - 1f) * (t - 1f) * (t - 1f) + C1 * (t - 1f) * (t - 1f);
		public static float EaseInOutBack(float t) => t < 0.5f
			? ((2f * t) * (2f * t) * ((C2 + 1f) * 2f * t - C2)) / 2f
			: ((2f * t - 2f) * (2f * t - 2f) * ((C2 + 1f) * (t * 2f - 2f) + C2) + 2f) / 2f;

		// Elastic
		private const float C4 = (2f * Mathf.PI) / 3f;
		private const float C5 = (2f * Mathf.PI) / 4.5f;

		public static float EaseInElastic(float t) => t == 0f ? 0f : Mathf.Approximately(t, 1f) ? 1f :
			-Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * C4);
		public static float EaseOutElastic(float t) => t == 0f ? 0f : Mathf.Approximately(t, 1f) ? 1f :
			Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * C4) + 1f;
		public static float EaseInOutElastic(float t) => t == 0f ? 0f : Mathf.Approximately(t, 1f) ? 1f : t < 0.5f
			? -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * C5)) / 2f
			: (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * C5)) / 2f + 1f;

		// Bounce
		private const float N1 = 7.5625f;
		private const float D1 = 2.75f;

		public static float EaseOutBounce(float t)
		{
			if (t < 1f / D1)
			{
				return N1 * t * t;
			}
			if (t < 2f / D1)
			{
				float t2 = t - 1.5f / D1;
				return N1 * t2 * t2 + 0.75f;
			}
			if (t < 2.5f / D1)
			{
				float t3 = t - 2.25f / D1;
				return N1 * t3 * t3 + 0.9375f;
			}
			float t4 = t - 2.625f / D1;
			return N1 * t4 * t4 + 0.984375f;
		}
		public static float EaseInBounce(float t) => 1f - EaseOutBounce(1f - t);
		public static float EaseInOutBounce(float t) => t < 0.5f
			? (1f - EaseOutBounce(1f - 2f * t)) / 2f
			: (1f + EaseOutBounce(2f * t - 1f)) / 2f;
	}
}
