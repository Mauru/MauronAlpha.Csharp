﻿using System;

namespace MauronAlpha.Text.Interfaces {
	
	public interface I_textUnitType:IEquatable<I_textUnitType> {
		
		string Name { get; }

		bool CanHaveChildren { get; }
		bool CanHaveParent { get; }

	}

}
