using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;

using MauronAlpha.Events.Interfaces;
using MauronAlpha.Events;

using MauronAlpha.Layout.Layout2d.Interfaces;
using MauronAlpha.Layout.Layout2d.Context;
using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Collections;

namespace MauronAlpha.Layout.Layout2d.Units {	
	
	//A layout unit in 2d space
	public abstract class Layout2d_unit : Layout2d_component, 
	I_layoutUnit {

		//Constructors
		public Layout2d_unit(Layout2d_unitType unitType):base(){
			SUB_type = unitType;
		}

		//Type description
		private Layout2d_unitType SUB_type;
		public virtual Layout2d_unitType UnitType {
			get {
				return SUB_type;
			}
		}

		//Booleans
		private bool B_isReadOnly = false;
		public virtual bool Equals( I_layoutUnit other ) {
			if( !UnitType.Equals( other.UnitType ) )
				return false;
			if( !Context.Equals( other.Context ) )
				return false;
			if( CanHaveParent )
				return Parent.Equals( other.Parent );
			if( CanHaveChildren )
				return Children.Equals( other.Children );
			if( !EventHandler.Equals( other.EventHandler ) )
				return false;
			return true;
		}
		public bool CanHaveChildren {
			get {
				return UnitType.CanHaveChildren;
			}
		}
		public bool CanHaveParent {
			get {
				return UnitType.CanHaveParent;
			}
		}
		public bool IsReadOnly {
			get {
				return B_isReadOnly;
			}
		}
		public bool IsParent {
			get {
				return CanHaveChildren&&!DATA_children.IsEmpty;
			}
		}
		public bool IsChild {
			get {
				return (CanHaveParent&&UNIT_parent!=null);
			}
		}
		public bool HasChildren { 
			get {
				return CanHaveChildren 
				&& ( DATA_children == null || DATA_children.IsEmpty );
			}
		}
		public bool HasParent {
			get {
				return CanHaveParent 
				&& ( UNIT_parent == null );
			}
		}
		public virtual bool HasOwnRenderManager { 
			get { 
				return (CTRL_render != null);
			}
		}

		//Context
		private Layout2d_context CONTEXT_layout;
		public Layout2d_context Context {
			get {
				if( CONTEXT_layout == null )
					CONTEXT_layout = new Layout2d_context();
				return CONTEXT_layout.SetIsReadOnly(true);
			}
		}
		
		//Event Handler
		private I_eventHandler EVENT_handler;
		public I_eventHandler EventHandler {
			get {
				if( EVENT_handler == null )
					EVENT_handler = new EventHandler();
				return EVENT_handler;
			}
		}

		//Double Sizes
		public virtual double Height { get { return Context.Size.Height; } }
		public virtual double Width { get { return Context.Size.Width; } }
		public virtual double MaxHeight { get { return Context.MaxSize.Height; } }
		public virtual double MaxWidth { get { return Context.MaxSize.Width; } }

		//Relational Data > Children
		private Layout2d_unitCollection DATA_children;
		public Layout2d_unitCollection Children {
			get {
				if( !CanHaveChildren )
					return new Layout2d_unitCollection();
				if( DATA_children==null )
					DATA_children = new Layout2d_unitCollection();
				return DATA_children.SetIsReadOnly(IsReadOnly);
			}
		}

		//Relational Data > Parent
		private I_layoutUnit UNIT_parent;

		//Methods
		public I_layoutUnit SetIsReadOnly( bool status ) {
			B_isReadOnly = status;
			return this;
		}
		public I_layoutUnit SetEventHandler(I_eventHandler handler) {
			if( IsReadOnly )
				throw Error( "Is protected!,(SetEventhandler)", this, ErrorType_protected.Instance );
				
			EVENT_handler = handler;
			return this;
		}
		public I_layoutUnit SetContext( Layout2d_context context ) {
			if( IsReadOnly )
				throw Error( "Is protected!,(SetContext)", this, ErrorType_protected.Instance );
			CONTEXT_layout = context;
			return this;
		}

		public I_layoutUnit SetHeight (int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetHeight)", this, ErrorType_protected.Instance);
			Context.Size.SetIsReadOnly(false).SetHeight(n);
			return this;
		}
		public I_layoutUnit SetWidth (int n) {
			if( IsReadOnly )
				throw Error("Is protected!,(SetWidth)", this, ErrorType_protected.Instance);
			Context.Size.SetIsReadOnly(false).SetWidth(n);
			return this;
		}

		public Layout2d_position Position { get { return Context.Position; } }

		public Layout2d_size Size { get { return Context.Size; } }

		//Relational Data > Index Information
		public long NextChildIndex {
			get {
				if (DATA_children == null)
					return 0;
				return DATA_children.NextIndex;
			}
		}

		//Virtuals
		//Root is the master container a unit is situated in
		public virtual I_layoutUnit Root {
			get {
				I_layoutUnit result=this;
				while (result.IsChild)
					result = result.Parent;
				return result;
			}
		}
		public virtual I_layoutUnit ChildByIndex (long index) {
			if( !DATA_children.IsEmpty||!DATA_children.ContainsIndex(index) )
				throw Error("Invalid index!,{"+index+"},(ChildByIndex)", this, ErrorType_index.Instance);
			return DATA_children.UnitByIndex(index);
		}
		public virtual I_layoutUnit AddChildAtIndex (long index, I_layoutUnit unit, bool b_setDependencies) {
			if(IsReadOnly)
				throw Error("Protected!,(AddChildAtIndex)",this,ErrorType_protected.Instance);
			if(DATA_children == null)
				DATA_children = new Layout2d_unitCollection();
			if( DATA_children.ContainsIndex(index) )
				throw Error("Unit allready has a child at index!,{"+index+"},(AddChildAtIndex)", this, ErrorType_index.Instance);
			
			DATA_children.RegisterUnitAtIndex(index, unit);

			if(b_setDependencies)
				unit.SetParent(this, false);

			return this;
		}
		public virtual I_layoutUnit Parent {
			get {
				if( !CanHaveParent )
					throw Error("Unit can not have a parent!", this, ErrorType_scope.Instance);
				return UNIT_parent;
			}
		}
		public virtual I_layoutUnit SetParent (I_layoutUnit parent, bool updateRelations) {
			if( IsReadOnly )
				throw Error("Protected!,(SetParent)", this, ErrorType_protected.Instance);
			if( !parent.CanHaveChildren )
				throw Error("Unit can not have any children!,(SetParent)", this, ErrorType_scope.Instance);
			
			UNIT_parent=parent;
			if( updateRelations )
				parent.AddChildAtIndex(parent.NextChildIndex, this, false);

			return this;
		}
		//Request a Render update
		public virtual I_layoutUnit RequestRender(Layout2d_renderChain chain) {
			if (HasOwnRenderManager)
				RenderManager.HandleRenderRequest(chain.Add(this));
			if (!IsChild)
				return this;
			Parent.RequestRender(chain.Add(this));
			return this;
		}
		public virtual I_layoutUnit RequestRender() {
			if (HasOwnRenderManager)
				RenderManager.HandleRenderRequest(new Layout2d_renderChain().Add(this));
			if (!IsChild)
				return this;
			Parent.RequestRender(new Layout2d_renderChain().Add(this));
			return this;
		}

		private I_layoutRenderer CTRL_render;
		public I_layoutRenderer RenderManager {
			get {
				if (CTRL_render == null)
					throw NullError("RenderManager is undefined!,(RenderManager)", this, typeof(I_layoutRenderer));
				return CTRL_render;
			}			
		}
		public Layout2d_unit SetRenderManager(I_layoutRenderer renderManager) {
			if (IsReadOnly)
				throw Error("Is protected!,(SetRenderManager)", this, ErrorType_protected.Instance);
			CTRL_render = renderManager;
			return this;
		}
	}	

}
