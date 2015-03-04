using System;
using MauronAlpha.Text.Units;

namespace MauronAlpha.Text.Interfaces {
	
	public interface I_textUnitType:IEquatable<I_textUnitType> {
		
		string Name { get; }

		bool CanHaveChildren { get; }
		bool CanHaveParent { get; }

		I_textUnit New {
			get;
		}

		I_textUnitType ParentType { get; }
		I_textUnitType ChildType { get; }
	}

}
