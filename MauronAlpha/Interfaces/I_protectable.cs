namespace MauronAlpha.Interfaces {
	
	//Interface for a protectable class
	public interface I_protectable<TClass> {
		
		TClass SetIsReadOnly(bool status);

		bool IsReadOnly { get; }

	}
}
