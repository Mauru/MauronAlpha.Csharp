﻿using MauronAlpha.Geometry.Geometry2d.Shapes;
using MauronAlpha.MonoGame.Geometry;
using MauronAlpha.MonoGame.Collections;
using MauronAlpha.Geometry.Geometry2d.Units;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using MauronAlpha.MonoGame.Utility;


namespace MauronAlpha.MonoGame.Setup {
	
	
	public class TestObjects:MonoGameComponent {

		public static Polygon2d TestObject {
			get {
				return new Ngon2d(12,1);
			}
		}

		public void DrawTestObject(SpriteBatch batch, GraphicsDevice device) {
			LineBuilder tool = new LineBuilder(device);
			batch.Begin();
			tool.DrawCircle(batch, new Vector2d(200,200),100,20,Color.White,4);
			batch.End();

		}
		public void DrawHex(SpriteBatch batch, GraphicsDevice device){}

	}

}
