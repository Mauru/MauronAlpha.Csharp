using System;
using MauronAlpha.Interfaces;
using MauronAlpha.Events.Interfaces;

using MauronAlpha.Layout.Layout2d.Units;
using MauronAlpha.Layout.Layout2d.Collections;
using MauronAlpha.Layout.Layout2d.Context;

namespace MauronAlpha.Layout.Layout2d.Interfaces {
    
    public interface I_layoutUnit:IEquatable<I_layoutUnit>,I_protectable<I_layoutUnit> {
        
		I_eventHandler EventHandler { get; }

		Layout2d_unitType UnitType { get; }

		Layout2d_context Context { get;	}

		I_layoutUnit SetContext( Layout2d_context context );
		I_layoutUnit SetEventHandler( I_eventHandler handler );

		I_layoutUnit Parent { get; }
		I_layoutUnit SetParent( I_layoutUnit parent );
		
		bool CanHaveChildren { get; }
		bool CanHaveParent {	get; }
		
		Layout2d_unitCollection Children {
			get;
		}

    }

}
