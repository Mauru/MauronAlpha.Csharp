﻿using System;
using System.Collections.Generic;

using MauronAlpha.Interfaces;
using MauronAlpha.Text.Units;
using MauronAlpha.Text.Context;

namespace MauronAlpha.Text.Interfaces {
	
	//Interface for TextUnits	
	public interface I_textUnit:
	IEquatable<I_textUnit>,
	I_protectable<I_textUnit> {

		TextUnitType UnitType { get; }

		TextContext Context { get; }

		string AsString { get; }

		bool IsEmpty { get; }
		bool IsParent { get; }
		bool IsChild { get; }
		bool CanHaveChildren { get; }
		bool CanHaveParent { get; }
		
		ICollection<I_textUnit> Children { get; }
		ICollection<I_textUnit> Neighbors { get; }

		I_textUnit Parent { get; }

		I_textEncoding Encoding { get; }


	}


}
