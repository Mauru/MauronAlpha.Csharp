namespace MauronAlpha.HandlingErrors {
	
	//Class describing an error
	public abstract class ErrorType:MauronCode_subtype {

		public abstract string Name { get; }

		public virtual bool IsFatal { get { return true; } }
		public virtual bool IsException { get { return false; } }

		public virtual bool ThrowOnCreation { get { return false; } }
	
	}

}
