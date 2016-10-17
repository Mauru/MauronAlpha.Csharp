﻿namespace MauronAlpha.MonoGame.Geometry {

	using MauronAlpha.Geometry.Geometry3d;

	public class GeometryHelper:MonoGameComponent {


		public static double DegToRad(double degree) {
			return GeometryHelper3d.DegreeToRadians(degree);
		}
		public static double RadToDeg(double rad) {
			return GeometryHelper3d.DegreeToRadians(rad);
		}

	}
}
