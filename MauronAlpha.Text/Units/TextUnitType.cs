using System.Text;

namespace MauronAlpha.Text.Units {

	//A Class defining the subtype of a textunit
	public abstract class TextUnitType : MauronCode_subtype {

		//constructor
		public TextUnitType ( ) : base() { }

		//Name
		public abstract string Name { get; }

		public virtual bool CanHaveParent {
			get { return true; }
		}
		public virtual bool CanHaveChildren {
			get { return true; }
		}

	}
}
