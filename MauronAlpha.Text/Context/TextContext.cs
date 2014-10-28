using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;
using MauronAlpha.HandlingErrors;

using System;

namespace MauronAlpha.Text.Context {

	public class TextContext:MauronCode_textComponent, IEquatable<TextContext> {

		//DataTrees
		private MauronCode_dataTree<string,int> TREE_index = new MauronCode_dataTree<string,int>(SharedDataKeys, SharedDefaultValues);

		//DataKeys
		public static string[] SharedDataKeys = new string[] { "paragraph", "line", "word", "character" };
		public static int[] SharedDefaultValues = new int[] {0,0,0,0};
		
		//The Index 
		public TextContextResult Index {
			get {
				return new TextContextResult(TREE_index);
			}
		}

		#region Constructors
		public TextContext():base() {}
		public TextContext(int paragraph, int line, int word, int character):this(){
			SetIndexByKey("paragraph",paragraph);
			SetIndexByKey("line",line);
			SetIndexByKey("word",word);
			SetIndexByKey("character", character);
		}
		public TextContext (int paragraph, int line, int word):this() {
			SetIndexByKey("paragraph", paragraph);
			SetIndexByKey("line", line);
			SetIndexByKey("word", word);
		}
		public TextContext (int paragraph, int line):this() {
			SetIndexByKey("paragraph", paragraph);
			SetIndexByKey("line", line);
		}
		public TextContext(int paragraph):this() {
			SetIndexByKey("paragraph", paragraph);
		}
		#endregion

		#region Modification
		private TextContext SetIndexByKey(string key, int value) {
			if(value<0){
				Error("Index must be >= 0!", this ,ErrorType_bounds.Instance);
			}
			TREE_index.SetValue(key, value);
			return this;
		}
		#endregion

		#region Cloning, instances
		
		public TextContext Instance {
			get {
				return new TextContext(Index.Paragraph,Index.Line,Index.Word,Index.Character);
			}
		}
		public TextContext New(int paragraph, int line, int word, int character){
			return new TextContext(paragraph, line, word,character);
		}

		public static TextContext Empty {
			get {
				return new TextContext();
			}
		}
		public static TextContext Start {
			get {
				return New(0,0,0,0);
			}
		}

		#endregion

		#region Context information
		public int Paragraph {
			get {
				return TREE_index.Value("paragraph");
			}
		}
		public int Line {
			get {
				return TREE_index.Value("line");
			}
		}
		public int Word {
			get {
				return TREE_index.Value("word");
			}
		}
		public int Character {
			get {
				return TREE_index.Value("character");
			}
		}
		#endregion

		#region Values as string
		public string AsString {
			get {
				return "{'"+Paragraph+"','"+Line+"','"+Word+"','"+Character+"'}";
			}
		}
		#endregion
	
		#region Math (modification)

		#region Add
		public TextContext Add(int paragraph, int line, int word, int character){
			SetIndexByKey("paragraph", Paragraph+paragraph);
			SetIndexByKey("line", Line+line);
			SetIndexByKey("word", Word+word);
			SetIndexByKey("character", Character+character);
			return this;
		}
		public TextContext Add(TextContext other){
			return Add(other.Paragraph, other.Line, other.Word, other.Character);
		}
		#endregion
		#region Subtract
		public TextContext Subtract(int paragraph, int line, int word, int character){
			SetIndexByKey("paragraph", Paragraph-paragraph);
			SetIndexByKey("line", Line-line);
			SetIndexByKey("word", Word-word);
			SetIndexByKey("character", Character-character);
			return this;
		}
		public TextContext Subtract (TextContext other) {
			return Subtract(other.Paragraph, other.Line, other.Word, other.Character);
		}
		#endregion
		#region Multiply
		/// <summary>
		/// Multiply by value
		/// <remarks>Ignores multiply by 0 (keeps original value)</remarks>
		/// </summary>
		public TextContext Multiply(int paragraph, int line, int word, int character, bool ignoreZero){
			if( ignoreZero&&paragraph==0 ) {
				Exception("Ignoring Multiply by 0, {P:paragraph},(Multiply)", this, ErrorResolution.DoNothing);
			}
			else { SetIndexByKey("paragraph", paragraph); }

			if( ignoreZero&&line==0 ) {
				Exception("Ignoring Multiply by 0, {P:line},(Multiply)", this, ErrorResolution.DoNothing);
			} else { SetIndexByKey("line", line); }

			if( ignoreZero&&word==0 ) {
				Exception("Ignoring Multiply by 0, {P:word},(Multiply)", this, ErrorResolution.DoNothing);
			}else{ SetIndexByKey("word", word); }

			if( ignoreZero&&character==0 ) {
				Exception("Ignoring Multiply by 0, {P:character},(Multiply)", this, ErrorResolution.DoNothing);
			}else{ SetIndexByKey("character",character); }

			return this;
		}
		public TextContext Multiply(int paragraph, int line, int word, int character){
			return Multiply(paragraph, line, word, character, false);
		}
		public TextContext Multiply (TextContext other, bool ignoreZero) {
			return Multiply(other.Paragraph,other.Line, other.Word, other.Character, ignoreZero);
		}
		public TextContext Multiply(TextContext other)	{
			return Multiply(other,false);
		}
		#endregion
		#region Divide
		public TextContext Divide(TextContext other){
			return Divide(other,false);
		}
		public TextContext Divide (TextContext other, bool ignoreZero) {
			return Divide(other.Paragraph, other.Line, other.Word, other.Character, false);
		}
		/// <summary>
		/// Divide by value
		/// <remarks>Ignores divide by 0 (keeps original value)</remarks>
		/// </summary>
		public TextContext Divide(int paragraph, int line, int word, int character, bool ignoreZero){

			#region Paragraph
			if(paragraph==0){
				if(!ignoreZero) {
					Error("Can not divide by 0!, {P:paragraph},(Divide)",this,ErrorType_divideByZero.Instance);
				}
				Exception("Ignoring 0!, {P:paragraph},(Divide)",this,ErrorResolution.DoNothing);
			}else{ SetIndexByKey("paragraph",Paragraph/paragraph); }
			#endregion
			#region Line
			if( line==0 ) {
				if( !ignoreZero ) {
					Error("Can not divide by 0!, {P:line},(Divide)", this, ErrorType_divideByZero.Instance);
				}
				Exception("Ignoring 0!, {P:line},(Divide)", this, ErrorResolution.DoNothing);
			}
			else { SetIndexByKey("line", Line/line); }
			#endregion
			#region Word
			if( word==0 ) {
				if( !ignoreZero ) {
					Error("Can not divide by 0!, {P:word},(Divide)", this, ErrorType_divideByZero.Instance);
				}
				Exception("Ignoring 0!, {P:word},(Divide)", this, ErrorResolution.DoNothing);
			}
			else { SetIndexByKey("word", Word/word); }
			#endregion
			#region Character
			if( character==0 ) {
				if( !ignoreZero ) {
					Error("Can not divide by 0!, {P:character},(Divide)", this, ErrorType_divideByZero.Instance);
				}
				Exception("Ignoring 0!, {P:character},(Divide)", this, ErrorResolution.DoNothing);
			}
			else { SetIndexByKey("character", Character/character); }
			#endregion
			
			
			return this;
		}
		#endregion
		
		#endregion

		#region Boolean States
		
		public bool IsStart {
			get {
				return Equals(0,0,0,0);
			}
		}
		public bool IsStartOf(TextComponent_unit unit) {
			return unit.FirstCharacter.Context.Equals(this);
		}

		public bool IsEndOf (TextUnit_text text) {
			//1: Get the last item of text
			TextContext context=text.Context;

			if( text.IsEmpty && !IsStart ) {
				return false;
			}
			if( text.IsEmpty && IsStart ) {
				return true;
			}
			
			return text.LastCharacter.Context.Equals(this,false);
		}

		#endregion

		#region Comparison Equals
		/// <summary>
		/// Checks if a context's numerical values equal another
		/// <remarks> use ignoreZero to skip comparing parts of the context </remarks>
		/// </summary>
		public bool Equals (int paragraph, int line, int word, int character, bool ignoreZero) {

			if( !ignoreZero ) 
				return Equals(paragraph, line, word, character);
			if( paragraph!=0 && Line!=line )
				return false;			
			if( line!=0 && Line!=line )	
				return false;
			if( word!=0 && Word!=word )	
				return false;
			if( character!=0 && Character!=character )
				return false;
			
			return true;
			
		}
		public bool Equals (int paragraph, int line, int word, int character) {
			
			if( Paragraph != paragraph )
				return false;
			if( Line != line )
				return false;
			if( Word != word )
				return false;
			if( Character != character )
				return false;

			return true;

		}
		public bool Equals (TextContext other, bool ignoreZero) {
			return
			Equals(other.Paragraph, other.Line, other.Word, other.Character, ignoreZero);
		}
		public bool Equals(TextContext other) {
			return Equals(other,false);
		}
		#endregion
		
		#region Comparison int
		/// <summary>
		/// Checks how a context's numerical values equal another
		/// <remarks> use ignoreZero to skip comparing parts of the context </remarks>
		/// </summary>
		public int CompareTo (int paragraph, int line, int word, int ch, bool ignoreZero ) {
			if(!ignoreZero)
				return CompareTo(paragraph,line,word,ch);

			if(Equals(0,0,0,0))
				return 0;

			if( paragraph!=0 && paragraph<Paragraph )
				return -1;				
			if( paragraph!=0 && paragraph!=Paragraph )		
				return Paragraph.CompareTo(paragraph);

			if( line != 0 && line!=Line )
				return Line.CompareTo(line);

			if( word !=0 && word!=Word )
				return Word.CompareTo(word);

			if( ch != 0 && ch != Character )
				return Character.CompareTo(ch);

			return 0;
		}
		public int CompareTo(int paragraph, int line, int word, int ch){
			if(paragraph<Paragraph)
				return -1;

			if(paragraph!=Paragraph)
				return Paragraph.CompareTo(paragraph);

			if(line!=Line)
				return Line.CompareTo(line);

			if(word!=Word)
				return Word.CompareTo(word);

			return Character.CompareTo(ch);
		}
		public int CompareTo(TextContext other, bool ignoreZero){
			return CompareTo(other.Paragraph, other.Line, other.Word, other.Character, ignoreZero);
		}
		#endregion

		#region IEquatable<TextContext>
		bool IEquatable<TextContext>.Equals (TextContext other) {
			return Equals(other);
		}
		#endregion

		//Validator
		public TextContextValidator Validate {
			get {
				return new TextContextValidator(this);
			}
		}
	}

}