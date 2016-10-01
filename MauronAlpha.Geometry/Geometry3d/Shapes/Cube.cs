namespace MauronAlpha.Geometry.Geometry3d.Shapes {
	using MauronAlpha.Geometry.Geometry3d.Collections;
	
	public class Cube:Mesh {

		public Cube(double width):base(CreatePointsFromWidth(width)) { }
		static Vector3dList CreatePointsFromWidth(double width) {
			
			Vector3dList result = new Vector3dList();

			Rectangle3d front = new Rectangle3d(width, width);
			Rectangle3d bottom = front.Copy.RotateX(90).Translate(0, 0, width*-1);
			Rectangle3d left = front.Copy.RotateY(90).Translate(0, 0, width * -1);
			Rectangle3d right = front.Copy.RotateY(-90).Translate(width, 0, 0);
			Rectangle3d top = front.Copy.RotateX(-90).Translate(0, width, 0);

			result.Append(front.TransformedPoints);
			result.Append(bottom.TransformedPoints);
			result.Append(left.TransformedPoints);
			result.Append(right.TransformedPoints);
			result.Append(top.TransformedPoints);
			return result;

		}
	}
}
