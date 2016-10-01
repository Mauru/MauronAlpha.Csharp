namespace MauronAlpha.Geometry.Geometry3d.Shapes {
	using MauronAlpha.Geometry.Geometry3d.Collections;
	using MauronAlpha.Geometry.Geometry3d.Units;
	using MauronAlpha.Geometry.Geometry3d.Transformation;
	using MauronAlpha.Geometry.Geometry3d.Interfaces;

	public class Mesh:GeometryComponent3d_shape, I_Mesh3d {
		Vector3dList _points;
		public Vector3dList Points {
			get { return _points; }
		}
		public Vector3d Point(int index) {
			return _points.Value(index);
		}

		Matrix3d _matrix;
		public Matrix3d Matrix {
			get {
				if(_matrix == null)
					_matrix = Matrix3d.Identity;
				return _matrix;
			}
		}

		public Mesh(Vector3dList points) : base() {
			_points = points;
		}
		public Mesh(Vector3dList points, Matrix3d matrix):base() {
			_points = points;
			_matrix = matrix;
		}
		public Mesh(Vector3dList points, Vector3d translation): base() {
			_points = points;
			_matrix = Matrix3d.FromVector3d(translation);
		}
	
		Vector3dList _transformed;
		public Vector3dList TransformedPoints { 
			get {
				if (_transformed != null)
					return _transformed;
				Vector3dList result = new Vector3dList();
				Vector3d n;
				foreach (Vector3d v in _points)
					result.Add(v.Transformed(Matrix));

				_transformed = result;
				return result;
			} 
		}

		public virtual Vector3d Origin {
			get {
				if(_transformed != null)
					return _transformed.FirstElement;
				if(_points != null)
					return _points.FirstElement;
				return Vector3d.Zero;
			}
		}

		public virtual Vector3d Position {
			get {
				if(_matrix == null)
					return Vector3d.Zero;
				return _matrix.Translation;
			}
		}


	}
}
