namespace MauronAlpha.Layout.Layout2d.Units {

	//A class describing a type
	public abstract	class Layout2d_unitType:Layout2d_component {
		
		//The Name of the unitType
		public abstract string Name { get; }
		public string AsString {
			get { return Name; }
		}

		//The "features" of the unitType
		public abstract bool CanHaveChildren { get; }
		public abstract bool CanHaveParent { get; }
		public abstract bool CanHide { get; }
		public abstract bool IsDynamic { get; }

	}
}
