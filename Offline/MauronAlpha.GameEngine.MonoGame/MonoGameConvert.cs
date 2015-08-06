using MauronAlpha.Geometry;
using Microsoft.Xna.Framework;
using System;

namespace MauronAlpha.GameEngine.MonoGame {
	public class MonoGameConvert : MauronCode_utility {
		public static Vector2 Vector(Vector2d v) { 
			return new Vector2((float) v.X, (float) v.Y);
		}
		public static TimeSpan GameTime2TimeSpan(GameTime gametime) {
			return gametime.ElapsedGameTime;
		}
		public static Rectangle2d Rectangle2Rectangle2d(Rectangle source) {
			return new Rectangle2d(source.X,source.Y,source.Width,source.Height);
		}
		public static Rectangle Rectangle2d2Rectangle (Rectangle source) {
			return new Rectangle(source.X, source.Y, source.Width, source.Height);
		}
	}
}
