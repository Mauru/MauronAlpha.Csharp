using System;

using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events.Units;

using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Layout.Layout2d.Units {
	
	//A "Reference" to a unit in the Layout2d Context
	public class Layout2d_unitReference : Layout2d_unit {

		//constructor
		public Layout2d_unitReference (I_eventHandler handler, Layout2d_unit unit)
			: base(UnitType_reference.Instance) {
			UNIT_source = unit;
			EVENTHANDLER_unitReference = handler;
			CONTEXT_lastValid = new Layout2d_contextSnapShot(handler.TimeStamp, unit.Context);
		}

		//The EventHandler for this unit
		private I_eventHandler EVENTHANDLER_unitReference;
		public override I_eventHandler EventHandler {
			get {
				return EVENTHANDLER_unitReference;
			}
		}

		//The unit this class is referencing
		private Layout2d_unit UNIT_source;

		//Convert a Layout2d_unit DataIndex to a unitReference Index
		public static MauronCode_dataIndex<Layout2d_unitReference> CONVERT_DataIndex_unitToReference(MauronCode_dataIndex<Layout2d_unit> dataIndex) {
			MauronCode_dataIndex<Layout2d_unitReference> result = new MauronCode_dataIndex<Layout2d_unitReference>();
			foreach(int key in dataIndex.Keys) {
				Layout2d_unit original = dataIndex.Value(key);
				Layout2d_unitReference reference=new Layout2d_unitReference(original.EventHandler, original);
				result.SetValue(key,reference);
			}
			return result;
		}

		//Return the last valid context of a unit
		private Layout2d_contextSnapShot CONTEXT_lastValid;

		public Layout2d_context LastValidContext {
			get {
				if(CONTEXT_lastValid == null) {
					throw NullError("CONTEXT_lastValid can not be null!",this,typeof(Layout2d_context));
				}
				return CONTEXT_lastValid;
			}
		}

		public bool IsEmpty {
			get { return UNIT_source==null; }
		}
		
		private bool B_isReadOnly = false;
		public override bool IsReadOnly {
			get { return B_isReadOnly; }
		}

		public bool Equals(Layout2d_unitReference other) {
			if(IsEmpty && !other.IsEmpty)
				return false;
			if(IsEmpty && other.IsEmpty)
				return true;
			return other.Equals(UNIT_source);
		}
		public bool Equals(Layout2d_unit other){
			if(IsEmpty&&!other.IsReference)
				return false;
			if(other.IsReference)
				return other.Equals(UNIT_source);
			return UNIT_source.Equals(other);
		}

		#region implementing Layout2d_unit

		public override bool Exists {
			get {
				if( UNIT_source==null )
					return false;
				if( UNIT_source.IsDynamic )
					return UNIT_source.Exists;
				return true;
			}
		}
		public override bool CanHaveParent {
			get { 
				if(!Exists) {
					Exception("ReferenceUnit does not exist!,(CanHaveParent)", this, ErrorResolution.ExpectedReturn);
					return false; 
				}
				return UNIT_source.CanHaveParent;
			}
		}
		public override bool CanHaveChildren {
			get {
				if( !Exists ) {
					Exception("ReferenceUnit does not exist!,(CanHaveChildren)", this, ErrorResolution.ExpectedReturn);
					return false;
				}
				return UNIT_source.CanHaveChildren;
			}
		}
		public override bool HasParent {
			get { 
				if(!Exists) {
					Exception("ReferenceUnit does not exist!,(HasParent)", this, ErrorResolution.ExpectedReturn);
					return false;	
				}
				return UNIT_source.HasParent;
			}
		}
		public override bool HasChildren {
			get {
				if( !Exists ) {
					Exception("ReferenceUnit does not exist!,(HasChildren)", this, ErrorResolution.ExpectedReturn);
					return false;
				}
				return UNIT_source.HasChildren;
			}
		}
		public override bool IsDynamic {
			get {
				if( !Exists ) {
					Exception("ReferenceUnit does not exist!,(IsDynamic)", this, ErrorResolution.ExpectedReturn);
					return false;
				}
				return UNIT_source.IsDynamic;
			}
		}
		public override bool IsParent {
			get {
				if( !Exists ) {
					Exception("ReferenceUnit does not exist!,(IsParent)", this, ErrorResolution.ExpectedReturn);
					return false;
				}
				return UNIT_source.IsParent;
			}
		}
		public override bool IsChild {
			get {
				if( !Exists ) {
					Exception("ReferenceUnit does not exist!,(IsChild)", this, ErrorResolution.ExpectedReturn);
					return false;
				}
				return UNIT_source.IsChild;
			}
		}
		public override bool IsReference { get { return true; } } 

		public override Layout2d_unitCollection Children {
			get {
				if( !Exists ) {
					Exception("ReferenceUnit does not exist!,(Children)", this, ErrorResolution.ExpectedReturn);
					return new Layout2d_unitCollection();
				}
				return UNIT_source.Children;
			}
		}

		public override Layout2d_unitReference Parent {
			get { 
				if(!Exists) {
					throw NullError("ReferenceUnit does not exist!,(Parent)", this, typeof(Layout2d_unitReference));
				}
				return UNIT_source.Parent;
			}
		}
		public override Layout2d_unitReference AsReference {
			get { 
				if( !Exists ) {
					Exception("ReferenceUnit does not exist!,(AsReference)",this,ErrorResolution.Delayed);
				}
				return Instance; 
			}
		}
		public override Layout2d_unitReference Instance {
			get { 
				return new Layout2d_unitReference(EventHandler,UNIT_source); 
			}
		}
		public override Layout2d_unitReference ChildByIndex (int index) {
			if( !Exists ) {
				throw NullError("ReferenceUnit does not exist!,(ChildByIndex)", this, typeof(Layout2d_unitReference));
			}
			return UNIT_source.ChildByIndex(index);
		}

		public override Layout2d_unit AddChildAtIndex (Layout2d_unitReference unit, int index) {
			if( !Exists ) {
				throw NullError("ReferenceUnit does not exist!,(ChildByIndex)", this, typeof(Layout2d_unit));
			}
			return UNIT_source.AddChildAtIndex(unit,index);
		}

		public override Layout2d_context Context {
			get { 
				if(!Exists) {
					Exception("ReferenceUnit does not exist!,(AsReference)", this, ErrorResolution.Delayed);
				}
				return LastValidContext;
			}
		}

		#endregion

	}

	//Description
	public sealed class UnitType_reference:Layout2d_unitType {
		#region singleton
			private static volatile UnitType_reference instance=new UnitType_reference();
			private static object syncRoot=new Object();
			//constructor singleton multithread safe
			static UnitType_reference ( ) { }
			public static Layout2d_unitType Instance {
				get {
					if( instance==null ) {
						lock( syncRoot ) {
							instance=new UnitType_reference();
						}
					}
					return instance;
				}
			}
		#endregion

		public override string Name { get { return "reference"; } }

		public override bool CanHaveChildren {
			get { return false; }
		}

		public override bool CanHaveParent {
			get { return false; }
		}

		public override bool CanHide {
			get { return true; }
		}

		public override bool IsDynamic {
			get { return true; }
		}

	}

}
