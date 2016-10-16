namespace MauronAlpha.MonoGame.Assets.DataObjects {
	
	/// <summary> Text specific visual formating</summary>
	public class TextFormat : MonoGameComponent {
		public double ParagraphSpacing = 0;
		public double LineSpacing = 0;

		public double TabLength = 0;
		public double LetterSpacing = 2;

		public double WordSpacing = 4;

		public static TextFormat Default {
			get { return new TextFormat(); }
		}
	}

}
