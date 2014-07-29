using System;

namespace MauronAlpha.Settings {
	
	public interface I_hasDefaultSettings {
		I_hasDefaultSettings FromDefaults(string[] query);
		I_hasDefaultSettings FromDefaults( );
		MauronCode_defaultSettingsObject Defaults { get; }
	}

}
