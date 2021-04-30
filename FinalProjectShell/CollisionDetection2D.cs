using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNA2DCollisionDetection
{
	public enum UseForCollisionDetection { Triangles, Rectangles, Circles }

	public static class CollisionDetection2D
	{
		public static UseForCollisionDetection CDPerformedWith { get; set; }

		/// <summary>
		/// Similar to Rectangle.Intersects(otherrect) method
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="y1"></param>
		/// <param name="width1"></param>
		/// <param name="height1"></param>
		/// <param name="x2"></param>
		/// <param name="y2"></param>
		/// <param name="width2"></param>
		/// <param name="height2"></param>
		/// <returns></returns>
		public static bool BoundingRectangles(Rectangle rectA, Rectangle rectB)
		{
			int top = Math.Max(rectA.Top, rectB.Top);
			int bottom = Math.Min(rectA.Bottom, rectB.Bottom);
			int left = Math.Max(rectA.Left, rectB.Left);
			int right = Math.Min(rectA.Right, rectB.Right);

			if (top >= bottom || left >= right)
				return false;

			return true;
		}
	}
}