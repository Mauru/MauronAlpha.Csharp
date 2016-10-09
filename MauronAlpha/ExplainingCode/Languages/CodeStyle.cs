namespace MauronAlpha.ExplainingCode.Languages
{

	//Describes a style (i.e. Language)
    public abstract class CodeStyle:MauronCode {
		
		//Constructor
		public CodeStyle():base(){}

		//Name
		public abstract string Name { get; }
    }
}
