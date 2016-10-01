namespace MauronAlpha.Geometry.Geometry3d.Units {

	public class Ray3d:GeometryComponent3d {
		Vector3d _origin;
		Vector3d _direction;

		public Ray3d(Vector3d origin, Vector3d vector): base() {

			_origin = origin;
			_direction = origin.RelativeDirection(vector);

		}

		public Vector3d Distance(double n) {
			return _origin.Instance.Add(_direction.Multiply(n));
		}
	}
}
