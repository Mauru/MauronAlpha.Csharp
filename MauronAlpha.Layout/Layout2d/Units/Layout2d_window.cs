using System;

using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Interfaces;

using MauronAlpha.HandlingErrors;

using MauronAlpha.Events;
using MauronAlpha.Events.Interfaces;

namespace MauronAlpha.Layout.Layout2d.Units {
	
	//Describes a window
	public class Layout2d_window : Layout2d_unit, I_layoutUnit {

		//constructor
		public Layout2d_window(string name, I_layoutController controller, Layout2d_context context):base(UnitType_window.Instance) {
			STR_name = name;
			EVENT_handler = new MauronAlpha.Events.EventHandler(controller.EventHandler);
            LAYOUT_context = context;

			//make sure we set the anchor
			if( !context.HasAnchor ) {
				context.SetAnchor(AsReference);
			}

		}

		private string STR_name;
		public string Name {
			get {
				return STR_name;
			}
		}

		private bool B_isReadOnly = false;
        private bool B_isStatic = false;
        public override bool IsStatic {
            get { return B_isStatic; }
        }

		#region Boolean states
		public override bool Exists {
			get { return true; }
		}

		public override bool CanHaveParent {
			get { return false; }
		}
		public override bool CanHaveChildren {
			get { return true; }
		}

		public override bool HasParent {
			get { return false; }
		}
		public override bool HasChildren {
			get { return !LAYOUT_children.IsEmpty; }
		}

		public override bool IsDynamic {
			get { return true; }
		}
		public override bool IsParent {
			get { return !LAYOUT_children.IsEmpty; }
		}
		public override bool IsChild {
			get { return false; }
		}
		public override bool IsReference {
			get { return false; }
		}
		public override bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}

		public bool Equals (Layout2d_window other) {
			if( Name!=other.Name )
				return false;

			return true;
		}
		
		#endregion

		private Layout2d_unitCollection LAYOUT_children=new Layout2d_unitCollection();
		public override Layout2d_unitCollection Children {
			get { return LAYOUT_children; }
		}
		
		public override Layout2d_unitReference ChildByIndex (int index) {
			return LAYOUT_children.UnitByIndex(index);
		}
		public override Layout2d_unitReference Parent {
			get { throw Error("Windows can not have a parent!,(Parent)",this,ErrorType_nullError.Instance); }
		}
		public override Layout2d_unitReference AsReference {
			get { return new Layout2d_unitReference(EventHandler,this); }
		}
		public override Layout2d_unitReference Instance {
			get { return AsReference; }
		}

		public override I_layoutUnit AddChildAtIndex (int index,Layout2d_unitReference unit) {
			LAYOUT_children.RegisterUnitAtIndex(index,unit);
			return this;
		}

		private Layout2d_context LAYOUT_context;
		public override Layout2d_context Context {
			get {
                if (LAYOUT_context == null)
                    throw NullError("Context can not be null!,(Context)", this, typeof(Layout2d_context));
                return LAYOUT_context;
			}
		}

		private I_eventHandler EVENT_handler;
		public override I_eventHandler EventHandler {
			get { 
				return EVENT_handler; 
			}
		}


	}

	//Description
	public sealed class UnitType_window : Layout2d_unitType {
		#region singleton
		private static volatile UnitType_window instance=new UnitType_window();
		private static object syncRoot=new Object();
		//constructor singleton multithread safe
		static UnitType_window ( ) { }
		public static Layout2d_unitType Instance {
			get {
				if( instance==null ) {
					lock( syncRoot ) {
						instance=new UnitType_window();
					}
				}
				return instance;
			}
		}
		#endregion

		public override string Name { get { return "window"; } }

		public override bool CanHaveChildren {
			get { return true; }
		}

		public override bool CanHaveParent {
			get { return false; }
		}

		public override bool CanHide {
			get { return false; }
		}

		public override bool IsDynamic {
			get { return true; }
		}
	}

}
