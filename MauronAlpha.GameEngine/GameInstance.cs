using MauronAlpha;
using MauronAlpha.ProjectTypes;

namespace MauronAlpha.GameEngine {
    public class GameInstance:ProgramInstance {
		public GameInstance (MauronAlphaGame game) : base(game,game.Name) { }
    }
}
