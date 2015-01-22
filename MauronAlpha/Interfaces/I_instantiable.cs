using System;

namespace MauronAlpha.Interfaces {
	
	//Allows an object to create an instance of itself
	public interface I_instantiable<T> {

		T Instance {
			get;
		}
	
	}
}
