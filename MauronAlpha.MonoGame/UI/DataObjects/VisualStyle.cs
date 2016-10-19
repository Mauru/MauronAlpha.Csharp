namespace MauronAlpha.MonoGame.UI.DataObjects {

	/// <summary> Generic Visual formating for any UI Component </summary>
	public class VisualStyle : UIComponent {

		Spacing _padding = new Spacing();
		public Spacing Padding {
			get { return _padding; }
		}

		Spacing _margin = new Spacing();
		public Spacing Margin {
			get { return _margin; }
		}

		public bool Equals(VisualStyle other) {
			return Padding.Equals(other.Padding) && Margin.Equals(other.Margin);
		}
	}

}