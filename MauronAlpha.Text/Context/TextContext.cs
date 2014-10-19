using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;
using MauronAlpha.HandlingErrors;

using System;

namespace MauronAlpha.Text.Context {

	public class TextContext:MauronCode_textComponent, IEquatable<TextContext> {

		//DataTrees
		private MauronCode_dataTree<string,int> TREE_index = new MauronCode_dataTree<string,int>(SharedDataKeys, SharedDefaultValues);
		private MauronCode_dataTree<string,int> TREE_offset = new MauronCode_dataTree<string, int>(SharedDataKeys, SharedDefaultValues);
		//DataKeys
		public static string[] SharedDataKeys = new string[] { "line", "word", "character" };
		public static int[] SharedDefaultValues = new int[] {0,0,0};
		
		//The Index 
		public TextContextResult Index {
			get {
				return new TextContextResult(TREE_index);
			}
		}
		public TextContextResult Offset {
			get {
				return new TextContextResult(TREE_offset);
			}
		}
		

		#region Constructors
		public TextContext() {}
		public TextContext(int line, int word, int character):this(){
			SetLineOffset(line);
			SetWordOffset(word);
			SetCharacterOffset(character);
		}
		public TextContext (int line, int word):this() {
			SetLineOffset(line);
			SetWordOffset(word);
		}
		public TextContext (int line):this() {
			SetLineOffset(line);
		}
		#endregion

		#region Cloning, instances
		public TextContext Instance {
			get {
				return TextContext.NewEmpty.SetLineOffset(LineOffset).SetWordOffset(WordOffset).SetCharacterOffset(CharacterOffset);
			}
		}
		public static TextContext NewEmpty {
			get {
				return new TextContext();
			}
		}
		public TextContext New(int line, int word, int character){
			return new TextContext(line,word,character);
		}
		#endregion

		#region Context information
		#region line
		private int INT_lineOffset;
		public int LineOffset {
			get { return INT_lineOffset; }
		}
		public TextContext SetLineOffset(int n) {
			INT_lineOffset=n;
			return this;
		}
		#endregion
		#region word
		private int INT_wordOffset;
		public int WordOffset {
			get { return INT_wordOffset; }
		}
		public TextContext SetWordOffset(int n){
			INT_wordOffset=n;
			return this;
		}
		#endregion
		#region character
		private int INT_characterOffset;
		public int CharacterOffset {
			get { return INT_characterOffset; }
		}
		public TextContext SetCharacterOffset(int n) {
			INT_characterOffset=n;
			return this;
		}
		#endregion
		#endregion

		#region Static context
		public static TextContext End {
			get {
				return new TextContext(-1,-1,-1);
			}
		}
		public static TextContext Start {
			get {
				return new TextContext(0, 0, 0);
			}
		}
		#endregion

		#region Set All Values of the context at once
		public TextContext SetOffset(int line, int word, int character) {
			SetLineOffset(line);
			SetWordOffset(word);
			SetCharacterOffset(character);
			return this;
		}
		public TextContext SetOffset(TextContext context) {
			SetOffset(context.LineOffset,context.WordOffset,context.CharacterOffset);
			return this;
		}
		#endregion

		#region Values as string
		public string AsString {
			get {
				return "{'"+LineOffset+"','"+WordOffset+"'+'"+CharacterOffset+"'}";
			}
		}
		#endregion
	
		#region Math (modification)

		#region Add
		public TextContext Add(int line, int word, int character){
			SetLineOffset(LineOffset+line);
			SetWordOffset(WordOffset+word);
			SetCharacterOffset(CharacterOffset+character);
			return this;
		}
		public TextContext Add(TextContext context){
			return Add(context.LineOffset,context.WordOffset,context.CharacterOffset);
		}
		#endregion
		#region Subtract
		public TextContext Subtract(int line, int word, int character){
			SetLineOffset(LineOffset-line);
			SetWordOffset(WordOffset-word);
			SetCharacterOffset(CharacterOffset-character);
			return this;
		}
		public TextContext Subtract(TextContext context){
			return Subtract(context.LineOffset,context.WordOffset,context.CharacterOffset);
		}
		#endregion
		#region Multiply
		/// <summary>
		/// Multiply by value
		/// <remarks>Ignores multiply by 0 (keeps original value)</remarks>
		/// </summary>
		public TextContext Multiply(int line, int word, int character, bool ignoreZero){
			if( ignoreZero&&line==0 ) {
				Exception("Ignoring Multiply by 0, {P:line},(Multiply)", this, ErrorResolution.DoNothing);
			} else { SetLineOffset(LineOffset*line); }

			if( ignoreZero&&word==0 ) {
				Exception("Ignoring Multiply by 0, {P:word},(Multiply)", this, ErrorResolution.DoNothing);
			}else{ SetWordOffset(WordOffset*word); }

			if( ignoreZero&&character==0 ) {
				Exception("Ignoring Multiply by 0, {P:character},(Multiply)", this, ErrorResolution.DoNothing);
			}else{ SetCharacterOffset(CharacterOffset*character); }

			return this;
		}
		public TextContext Multiply(int line, int word, int character){
			return Multiply(line,word,character,false);
		}
		public TextContext Multiply (TextContext context, bool ignoreZero) {
			return Multiply(context.LineOffset,context.WordOffset,context.CharacterOffset,ignoreZero);
		}		
		#endregion
		#region Divide
		public TextContext Divide(TextContext context){
			return Divide(context.LineOffset,context.WordOffset,context.CharacterOffset);
		}
		/// <summary>
		/// Divide by value
		/// <remarks>Ignores divide by 0 (keeps original value)</remarks>
		/// </summary>
		public TextContext Divide(int line, int word, int character){

			#region Line
			if(line==0){
				Exception("Can not divide by 0!, {P:line},(Divide)",this,ErrorResolution.DoNothing);
			}else{ SetLineOffset(LineOffset/line); }
			#endregion
			#region word
			if(word==0){
				Exception("Can not divide by 0!, {P:word},(Divide)",this,ErrorResolution.DoNothing);
			}else{ SetWordOffset(WordOffset/word); }
			#endregion
			#region character
			if(character==0){
				Exception("Can not divide by 0!, {P:character},(Divide)",this,ErrorResolution.DoNothing);
			}else{ SetCharacterOffset(CharacterOffset*character); }
			#endregion
			
			return this;
		}
		#endregion
		
		#endregion

		#region Comparison and other refactorting
		/// <summary>
		/// Inverts all properties
		/// </summary>
		public TextContext Inverted {
			get {
				return Instance.Multiply(-1,-1,-1);
			}
		}

		/// <summary>
		/// Turns all negative properties into positives
		/// </summary>
		public TextContext Normalized {
			get {
				TextContext result=Instance;
				if( result.LineOffset<0 ){ result.Multiply(-1,0,0,true); }
				if( result.WordOffset<0 ) { result.Multiply(0, -1, 0, true); }
				if( result.CharacterOffset<0 ) { result.Multiply(0, 0, -1, true); }
				return result;

			}
		}
		#endregion


			
		#region Boolean States
		
		#region @return: BOOL; Context, with TextComponent, is Offset ("leaves" the component range)
		public bool IsWordOffset(TextUnit_word word){
			if(LineOffset!=word.Context.LineOffset){
				return true;
			}
			if(WordOffset!=word.Index){
				return true;
			}
			if(CharacterOffset<0&&Math.Abs(CharacterOffset)>word.CharacterCount) return true;
			if(CharacterOffset>=word.CharacterCount){
				return true;
			}
			return false;
		}
		public bool IsWordOffset(TextUnit_line line) {
			if(LineOffset!=line.Index){ return true; }
			if(WordOffset<0||CharacterOffset<0) {
				return true;
			}
			if(WordOffset>=line.WordCount){
				return true;
			}
			TextUnit_word word=line.WordByIndex(WordOffset);
			if(CharacterOffset>=word.CharacterCount){
				return true;
			}
			return false;			
		}
		public bool IsLineOffset(TextUnit_line line){
			if(LineOffset!=line.Index) return true;
			if(WordOffset<0&&Math.Abs(WordOffset)>line.WordCount) return true;
			if(WordOffset>=line.WordCount) return true;
			if(CharacterOffset<0&&Math.Abs(CharacterOffset)>line.CharacterCount) return true;
			TextUnit_word word=line.WordByIndex(WordOffset);
			if( CharacterOffset>=word.CharacterCount ) return true;
			return false;
		}
		public bool IsCharacterOffset (TextUnit_character ch) {
			if( LineOffset<0 ) {
				return true;
			}
			if( LineOffset!=ch.Context.LineOffset ) {
				return true;
			}
			if( WordOffset<0||CharacterOffset<0 ) {
				return true;
			}
			if( ch.Context.WordOffset!=WordOffset ) {
				return true;
			}
			if( ch.Context.CharacterOffset!=CharacterOffset ) {
				return true;
			}
			return false;
		}
		public bool IsTextOffset(TextUnit_text text){
			if(LineOffset<0){
				return true;
			}
			if(LineOffset>=text.LineCount){
				return true;
			}
			if(WordOffset<0||CharacterOffset<0) {
				return true;
			}
			TextUnit_line line = text.LineByIndex(LineOffset);
			if(WordOffset>=line.WordCount){
				return true;
			}
			TextUnit_word word = line.WordByIndex(WordOffset);
			if(word.CharacterCount>=CharacterOffset){
				return true;
			}
			return false;
		}
		#endregion
		#region @return: BOOL; STATIC; Check TextComponent for offset
		public static bool IsLineOffset(TextUnit_line line, int lineOffset,int wordOffset, int characterOffset){
			TextContextOffset offsetResult = new TextContextOffset(0,0,0);
			if(lineOffset<0) {
				
			}
		}
		#endregion

		public bool IsStart {
			get {
				return this.Equals(Start);
			}
		}
		public bool IsEndOf (TextUnit_text text) {
			//1: Get the last item of text
			TextContext context=text.Context;
			if( text.IsEmpty&&!IsStart ) {
				return false;
			}
			if( text.IsEmpty&&IsStart ) {
				return true;
			}
			if( Equals(-1, -1, -1) ) {
				return true;
			}
			//solve edgecases
			TextContext solved=SolveWith(text);
			return text.LastCharacter.Context.Equals(solved);
		}
		public bool IsEnd {
			get {
				return Equals(-1,-1,-1);
			}
		}
		#endregion

		#region Comparison Equals
		/// <summary>
		/// Checks if a context's numerical values equal another
		/// <remarks> use ignoreZero to skip comparing parts of the context </remarks>
		/// </summary>
		public bool Equals (int line, int word, int ch, bool ignoreZero) {
			
			if(!ignoreZero){ return Equals(line,word,ch); }
			
			if( line!=0 && LineOffset!=line )	return false;
			if( word!=0 && WordOffset!=word )	return false;
			if( ch != 0 && CharacterOffset!=ch ) return false;
			
			return true;
			
		}
		public bool Equals (int line, int word, int ch) {
			if( LineOffset!=line )
				return false;
			if( WordOffset!=word )
				return false;
			if( CharacterOffset!=ch )
				return false;
			return true;
		}
		public bool Equals (TextContext other, bool ignoreZero) {
			return
			Equals(other.LineOffset, other.WordOffset, other.CharacterOffset, ignoreZero);
		}
		#endregion
		
		#region Comparison int
		/// <summary>
		/// Checks how a context's numerical values equal another
		/// <remarks> use ignoreZero to skip comparing parts of the context </remarks>
		/// </summary>
		public int CompareTo (int line, int word, int ch, bool ignoreZero ) {
			if(!ignoreZero){
				return CompareTo(line,word,ch);
			}

			if(Equals(0,0,0)){ return 0; }

			if( line!=0 && line<LineOffset ) {
				return -1;
			}

			if( line != 0 && line!=LineOffset ) {
				return LineOffset.CompareTo(line);
			}

			if( word !=0 && word!=WordOffset ) {
				return WordOffset.CompareTo(word);
			}

			if( ch != 0 && ch != CharacterOffset ) {
				return CharacterOffset.CompareTo(ch);
			}

			return 0;
		}
		public int CompareTo(int line, int word, int ch){
			if(line<LineOffset){
				return -1;
			}
			if(line!=LineOffset){
				return LineOffset.CompareTo(line);
			}
			if(word!=WordOffset){
				return WordOffset.CompareTo(word);
			}
			return CharacterOffset.CompareTo(ch);
		}
		public int CompareTo(TextContext other, bool ignoreZero){
			return CompareTo(other.LineOffset,other.WordOffset,other.CharacterOffset, ignoreZero);
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