﻿using MauronAlpha.Geometry.Geometry2d.Units;

using MauronAlpha.Layout.Layout2d.Position;
using MauronAlpha.Layout.Layout2d.Units;

namespace MauronAlpha.Layout.Layout2d.Context {
	
	//A class describing A LayoutUnit's Position in the Action/Reaction-dependency tree	
	public class Layout2d_context:Layout2d_component {

		//constructor
		public Layout2d_context() {}

		public Layout2d_context (Layout2d_unitReference anchor)
			: this() {
			LAYOUT_anchor = anchor;
			LAYOUT_position = new Layout2d_position(anchor);
		}
		public Layout2d_context(Layout2d_unitReference anchor, Vector2d position, Vector2d size, bool b_isStatic):this() {
			LAYOUT_position=new Layout2d_position(anchor, position, b_isStatic);
			LAYOUT_size = new Layout2d_size(size, b_isStatic);
			LAYOUT_anchor = anchor;
			B_isStatic = b_isStatic;
		}
		public Layout2d_context (Vector2d position, Vector2d size, bool b_isStatic)	: this() {
			LAYOUT_position=new Layout2d_position(position, b_isStatic);
			LAYOUT_size=new Layout2d_size(size, b_isStatic);
			B_isStatic=b_isStatic;
		}

		private Layout2d_unitReference LAYOUT_anchor;
		public bool HasAnchor {
			get {
				return (LAYOUT_anchor!=null);
			}
		}
		public Layout2d_unitReference Anchor {
			get {
				if(LAYOUT_anchor==null){
					throw NullError("Anchor can not be null!,(Anchor)",this,typeof(Layout2d_unitReference));
				}
				return LAYOUT_anchor;
			}
		}
		public Layout2d_context SetAnchor(Layout2d_unitReference unit){
			LAYOUT_anchor = unit;
			return this;
		}

		private Layout2d_position LAYOUT_position;
		private Layout2d_size LAYOUT_size;

		private bool B_isStatic = true;
		public bool IsStatic { get {
			return B_isStatic;
		} }
		
	}

}
