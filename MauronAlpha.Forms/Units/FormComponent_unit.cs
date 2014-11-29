using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Forms.Units {
	
	//Base Class for a form component
	public abstract class FormComponent_unit:MauronCode_formComponent, I_layoutUnit {

        protected I_eventHandler HANDLER_events;
        public virtual I_eventHandler EventHandler { get {
            if (HANDLER_events == null) {
                throw NullError("HANDLER_events can not be null!,(EventHandler)", this, typeof(I_eventHandler));
            }
            return HANDLER_events;
        } }

        protected Layout2d_unitReference LAYOUT_parent;
        public virtual Layout2d_unitReference Parent {
            get { 
                if (LAYOUT_parent == null) {
                    throw NullError("LAYOUT_parent can not be null!,(Parent)", this, typeof(Layout2d_unitReference));
                }
                return LAYOUT_parent;
            }
        }

        protected Layout2d_unitCollection LAYOUT_children=new Layout2d_unitCollection();
        public virtual Layout2d_unitCollection Children
        {
            get { return LAYOUT_children; }
        }

        protected bool B_isReadOnly = false;
        public virtual bool IsReadOnly
        {
            get { return B_isReadOnly; }
        }

        protected Layout2d_unitType LAYOUT_unitType; 
        public virtual Layout2d_unitType UnitType
        {
            get { return LAYOUT_unitType; }
        }

        public virtual Layout2d_unitReference AsReference {
            get { return new Layout2d_unitReference(EventHandler, this); }
        }

        protected Layout2d_context LAYOUT_context;
        public virtual Layout2d_context Context
        {
            get { 
                if (LAYOUT_context == null) {
                    throw NullError("LAYOUT_context can not be null!,(Context)", this, typeof(Layout2d_context));
                }
                return LAYOUT_context;
            }
        }

        protected bool B_isStatic = false;
        public virtual bool IsStatic
        {
            get { return B_isStatic; }
        }

    }

}
