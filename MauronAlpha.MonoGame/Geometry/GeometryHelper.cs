namespace MauronAlpha.MonoGame.Geometry {

	using MauronAlpha.Geometry.Geometry3d;

	public class GeometryHelper:MonoGameComponent {


		public static double Deg2Rad(double degree) {
			return GeometryHelper3d.DegreeToRadians(degree);
		}
		public static double Rad2Deg(double rad) {
			return GeometryHelper3d.DegreeToRadians(rad);
		}

	}
}
