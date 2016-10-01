namespace MauronAlpha.Geometry.Geometry3d.Interfaces {
	using MauronAlpha.Geometry.Geometry3d.Collections;
	using MauronAlpha.Geometry.Geometry3d.Transformation;
	using MauronAlpha.Geometry.Geometry3d.Units;

	public interface I_Mesh3d {

		Vector3dList Points {  get; }
		Matrix3d Matrix {  get; }

		Vector3d Origin {  get; }

		Vector3d Position { get; }

		Vector3dList TransformedPoints { get; }

	}
}
