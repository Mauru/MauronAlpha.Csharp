

namespace MauronAlpha.MonoGame.SpaceGame.DataObjects {
	
	public abstract class RuleSet:GameComponent {

		private RuleSetType TypeDefinition;
		public abstract RuleSetType Type {
			get;
		}
		public RuleSet(RuleSetType type) : base() {
			TypeDefinition = type;
		}
		public bool HasTypeDefinition {
			get {
				return (TypeDefinition == null);
			}
		}

	}

	public interface I_RuleSet<T> where T:RuleSetType {
		T Type { get; }
	}

	public class RuleSetType : GameComponent { }


}
