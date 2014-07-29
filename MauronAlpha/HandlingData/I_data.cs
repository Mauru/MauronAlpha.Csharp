using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauronAlpha.HandlingData {

	//Classes implementing this interface hold a single value that has a string as name
	public interface I_data {
		string Name { get; }
	}
}
