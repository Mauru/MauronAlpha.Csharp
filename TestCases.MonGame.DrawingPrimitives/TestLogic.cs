using MauronAlpha.MonoGame.Scripts;
using MauronAlpha.MonoGame.Utility;

namespace MauronAlpha.MonoGame {
	public class TestLogic:GameLogic {

		public TestLogic() : base() { 
		}
		//Initialize
		public override void Initialize(GameManager manager) {
			base.Initialize(manager);
			Engine.PrepareStartUpAssetts();
		}
	}
}
