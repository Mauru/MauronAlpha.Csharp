namespace MauronAlpha.GameEngine.Text {

	//base class
	public abstract class TextAlignment : MauronCode_subtype {
		public abstract string Name { get; }
	}
	//Left
	public class TextAlignment_Left : TextAlignment {
		public override string Name { get { return "Left"; } }
	}
	//center
	public class TextAlignment_Center : TextAlignment {
		public override string Name { get { return "Center"; } }
	}
	//right
	public class TextAlignment_Right : TextAlignment {
		public override string Name { get { return "Right"; } }
	}

}
