namespace MauronAlpha.Geometry.Geometry2d.Collections {

	using MauronAlpha.HandlingData;

	using MauronAlpha.Geometry.Geometry2d.Transformation;

	using MauronAlpha.Geometry.Geometry2d.Utility;

	/// <summary> A List of Matrix2d </summary>
	public class Matrix2dChain:MauronCode_dataList<Matrix2d> {

		/// <summary> Calculate Multiplied Matrix by identity/result * (index+) </summary>
		public Matrix2d CalculateAppliedResult() {
			if (IsEmpty)
				return Matrix2d.Identity;

			Matrix2d result = Matrix2d.Identity;
			foreach (Matrix2d matrix in this) {

				result = result.MultipliedCopy(matrix);

			}

			return result;
		}

	}

}
