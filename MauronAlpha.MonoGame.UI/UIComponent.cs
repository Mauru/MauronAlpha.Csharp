using MauronAlpha.ExplainingCode;
using MauronAlpha.HandlingData;

namespace MauronAlpha.MonoGame.UI {
	public class UIComponent:MauronCode_component {
	}

	public class List<T> : MauronCode_dataList<T> { }
}

namespace MauronAlpha.MonoGame.UI.Collections {
	using MauronAlpha.MonoGame.UI.DataObjects;
	
	public class UIComponent : MauronCode_component {}
	public class UIElement : UIComponent {

		public UIElement(bool isNull) : base() {
			B_isNull = isNull;
		}

		private bool B_isNull = true;
		public bool IsNull { get {	return B_isNull; } }

		List<UIElement> DATA_children = new List<UIElement>();
		List<UIElement> Children { get { return DATA_children; } }

		UIElement DATA_parent;
		UIElement Parent { get {
			if (DATA_parent == null)
				return new UIElement(true);
			return DATA_parent; 
		} }

		public List<UIBehavior> OnSpawn() {
			return new List<UIBehavior>(){
				UIBehaviors.RequestRenderUpdate,
			};
		}

		public void Freeze() { }

		private RenderCycle DATA_lastRender = RenderCycle.Never;
		public RenderCycle LastRendered { }
	}

	//A information element which is displayed just udner the mouse layer
	public class InfoFloat : UIElement {

		public List<UIBehavior> OnUpdate;

	}
}

namespace MauronAlpha.MonoGame.UI.DataObjects {

	//A timed unit defining a timestamp of the last render operation
	public class RenderCycle : UIComponent,System.IComparable<RenderCycle> {
		public long Period=0;
		public long Step=0;

		public RenderCycle() : this(true) { }
		public RenderCycle(bool isNull) :base() {
			B_isNull = isNull;
		}
		public RenderCycle(long step): this(false) {
			Step = step;
		}
		public RenderCycle(long period, long step): this(false) {
			Step = step;
		}

		private bool B_isNull = false;
		public bool IsNull {
			get { return B_isNull;}
		}

		public bool IsStart {
			get {
				return Period == 0 && Step == 0 && !IsNull;
			}
		}
		public bool Equals(RenderCycle other) {
			if(IsNull!=other.IsNull)
				return false;
			return this.CompareTo(other) == 0;
		}
		public int CompareTo(RenderCycle other) {
			if(IsNull && other.IsNull)
				return 0;
			if(!IsNull && other.IsNull)
				return 1;
			int pr = Period.CompareTo(other.Period);
			if (pr != 0)
				return pr;
			int ps = Step.CompareTo(other.Step);
			return ps;
		}
	}

	//Returns an action report from the UI
	public class UIBehavior:UIComponent {
		
		//How this action influences other RenderLevels
		public bool AffectsLower;
		public bool AffectsUpper;
		public bool AffectsSelf;

	}
	//Basically instructions for other UIElements
	public class UIBehaviorCall:UIComponent {
		
	}
	//Filters what is targeted by a UIBehaviorCall
	public class UITargets:UIComponent {

	}

}
