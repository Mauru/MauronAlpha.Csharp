namespace MauronAlpha.ExplainingCode.Languages
{

	//Describes a style (i.e. Language)
    public abstract class CodeStyle:MauronCode {
		
		//Constructor
		public CodeStyle():base(CodeType_subtype.Instance){}

		//Name
		public abstract string Name { get; }
    }
}
