using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Collections {
	public class Layout2d_unitCollection : Layout2d_component, I_protectable {
		
		//constructor
		public Layout2d_unitCollection():base() {
			LAYOUT_units = new MauronCode_dataIndex<Layout2d_unitReference>();
		}
		private Layout2d_unitCollection(Layout2d_unitCollection original):base() {
			Layout2d_unitCollection instance = new Layout2d_unitCollection();
			for(int index=0; index<LAYOUT_units.CountKeys; index++) {
				if(LAYOUT_units.ContainsValueAtKey(index)){
					Layout2d_unitReference unit = UnitByIndex(index);
					instance.RegisterUnitAtIndex(index,unit);
				}
			}
		}

		public Layout2d_unitCollection Instance {
			get {
				return new Layout2d_unitCollection(this);
			}
		}

		//The data
		private MauronCode_dataIndex<Layout2d_unitReference> LAYOUT_units;

		public Layout2d_unitReference UnitByIndex(int index) {
			return LAYOUT_units.Value(index);
		}
		public Layout2d_unitCollection RegisterUnitAtIndex(int index, Layout2d_unit unit) {
			if(IsReadOnly) {
				throw Error("Index is protected!,(RegisterUnitAtIndex)",this,ErrorType_protected.Instance);
			}
			if(index<0) {
				throw Error("Index out of Bounds!,(RegisterUnitAtIndex)",this,ErrorType_bounds.Instance);
			}
			if(index>LAYOUT_units.CountKeys){
				throw Error("Index out of Bounds!,(RegisterUnitAtIndex)",this,ErrorType_bounds.Instance);
			}
			if(LAYOUT_units.ContainsKey(index)) {
				Exception("Index is in Use!,{"+index+"},(RegisterUnitAtIndex)",this,ErrorResolution.Replaced);
			}
			LAYOUT_units.SetValue(index,unit.AsReference);

			return this;			
		}

		//boolean states
		public bool IsEmpty { 
			get {
				if(LAYOUT_units.IsEmpty)
					return true;
				for(int index = 0; index<LAYOUT_units.CountKeys; index++) {
					Layout2d_unitReference unit = LAYOUT_units.Value(index);
					if(unit.Exists)
						return true;
				}
				return false;
			}
		}

		public bool ContainsIndex(int index) {
			if(LAYOUT_units == null) {
				return false;
			}
			return LAYOUT_units.ContainsValueAtKey(index);
		}

		#region I_protectable (implemented)
		private bool B_isReadOnly = false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
		public Layout2d_unitCollection SetIsReadOnly(bool status){
			B_isReadOnly = status;
			return this;
		}
		#endregion
	}
}
