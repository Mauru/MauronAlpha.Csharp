using MauronAlpha.HandlingData;
using MauronAlpha.Text.Units;
using MauronAlpha.HandlingErrors;

using System;

namespace MauronAlpha.Text {

	public class TextContext:MauronCode_textComponent, IEquatable<TextContext> {

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
				return TextContext.New.SetLineOffset(LineOffset).SetWordOffset(WordOffset).SetCharacterOffset(CharacterOffset);
			}
		}
		public static TextContext New {
			get {
				return new TextContext();
			}
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

		#region Solving ContextOffsets
		/// <summary>
		/// Evalutes a context relative to a TextComponent, turning it to absolute numbers
		/// </summary>
		public static TextContext SolveContext (TextContext context, TextComponent_text text) {
			return context.SolveWith(text);
		}


		public bool TrySolveWith(TextComponent_text text){
			
			//set a backup
			TextContext result = Instance;

			//any preconditions- is the text empty? /!return
			if(text.IsEmpty){
				SetOffset(Start);
				return true;
			}

			//are we talking about the end of the text?
			if(IsEnd){
				SetOffset(text.Context.Instance);
				return true;
			}


			// 0: Starting off with the character, here it gets comlicated

			//a: try and find a line
			result.SolveLineOffset(text);

			// 1: We start off with the line
			result = SolveLineOffset(text);
			TextComponent_line line = text.LineByIndex(result.LineOffset);
			int lineIndex = result.LineOffset;

			//word
			result = result.SolveWordOffset(line,false);
					
			//did line number change?
			if(result.LineOffset!=lineIndex){
				line=text.LineByIndex(result.LineOffset);
			}
			TextComponent_word word=line.WordByIndex(result.WordOffset);

			//character
			result = result.SolveCharacterOffset(word,false).SolveWordOffset();

								
		}
		public TextContext SolveWith(TextComponent_text text){
		}
		public TextContext SolveWith(TextComponent_word word) {
			
		}
		//generic solve as TextComponent_*
		public T SolveAs<T,R>(R textComponent, TextContext offset) {
			TextContext context=Instance.Add(offset).SolveWith(textComponent);

		}

		//Solve the LineOffset
		public TextContext SolveLineOffset (TextComponent_text text) {

			//text is empty
			if( text.IsEmpty ) {
				Exception("Text is empty!,{"+LineOffset+"},(SolveLineOffset)", this, ErrorResolution.Correct_minimum);
				return Start;
			}

			TextContext result=Instance;

			//Out of bounds positive !return
			if( LineOffset>0 && LineOffset>=text.LineCount ) {
				Exception("LineIndex out of bounds!,{"+LineOffset+"},(SolveLineOffset)", this, ErrorResolution.Correct_maximum);
				return text.LastCharacter.Context.Instance;
			}

			//Out of bounds negative
			if( LineOffset<0 ) {
				
				//line offset out of bounds
				if( Math.Abs(LineOffset)>=text.LineCount ) {
					Exception("LineIndex out of bounds!,{"+LineOffset+"},(SolveLineOffset)", this, ErrorResolution.Correct_minimum);
					return Start;
				}

				result.SetLineOffset(text.LineCount+LineOffset);

			}

			return result;
		}

		//Solve the WordOffset
		public TextContext SolveWordOffset (TextComponent_line line, bool stayOnLine) {

			//line is empty !return
			if( line.IsEmpty && WordOffset > 0 ) {
				Exception("Line is empty!,{"+WordOffset+"},(SolveWordOffset)", this, ErrorResolution.Correct_maximum);
				return line.LastCharacter.Context.Instance;
			}

			TextContext result=Instance;

			//Out of bounds positive !return
			if( WordOffset>0 && WordOffset >= line.WordCount ) {

				//we stay on the line, return last context of line
				if( stayOnLine ) {
					return line.LastCharacter.Context.Instance;
				}
				//Line ends Text, return last context
				if( line.Index==line.Parent.LastLine.Index ) {
					Exception("WordIndex out of bounds!,{"+WordOffset+"},(SolveWordOffset)", this, ErrorResolution.Correct_maximum);
					return line.LastCharacter.Context.Instance;
				}

				//Advance line count
				result.Add(1, -line.WordCount, 0);
				
				//cycle with new line and context
				return result.SolveWordOffset(line.NextLine, stayOnLine);

			}

			//Out of bounds negative
			if( WordOffset < 0 ) {
				
				//negative offset larger than wordcount !return
				if( Math.Abs(WordOffset)>=line.WordCount ) {
					Exception("LineIndex out of bounds!,{"+WordOffset+"},(SolveWordOffset)", this, ErrorResolution.Correct_minimum);
					return result.Add(-1, -line.WordCount, 0).SolveWordOffset(line.PreviousLine,stayOnLine);
				}

				//correct offset
				result.SetWordOffset(line.WordCount+result.WordOffset);

			}

			return result;
		}

		// Character offset
		public TextContext SolveCharacterOffset(TextComponent_word word, bool stayOnWord){
			
			//word is empty
			if( word.IsEmpty && CharacterOffset > 0 ) {
				Exception("Word is empty!,{"+WordOffset+"},(SolveCharacterOffset)", this, ErrorResolution.Correct_maximum);
				return word.LastCharacter.Context.Instance;
			}

			TextContext result = Instance;

			#region Out of bounds positive !return
			if(CharacterOffset>word.CharacterCount) {
				
				//we stay on the word, return last context !return
				if(stayOnWord) {
					return word.LastCharacter.Context.Instance;
				}
				
				//the word ends the line, cycle with the first word of the next line !return
				if(word.IsLastOnLine){
					result.Add(1,0,-word.CharacterCount);
					return result.SolveCharacterOffset(word.Parent.NextLine.FirstWord,stayOnWord);
				}

				//simply solve with the next word !return
				result.Add(0,1,-word.CharacterCount);
				
				#region ExceptionCheck next word? !return
				//next word ends text
				if(!word.Source.ContainsCharacterIndex(result.WordOffset)){
					Exception("WordIndex out of Bounds!,{"+result.WordOffset+"},(SolveCharacterOffset)",this,ErrorResolution.Correct_maximum);
					return word.Source.LastCharacter.Context.Instance;
				}
				#endregion
				
				return result.SolveCharacterOffset(word.NextWord,stayOnWord);

			}
			#endregion

			//Out of bounds negative
			if(CharacterOffset<0){

				//negative offset larger than characterCount
				if(Math.Abs(CharacterOffset)>=word.CharacterCount){
					Exception("LineIndex out of bounds!,{"+CharacterOffset+"},(SolveCharacterOffset)", this, ErrorResolution.Correct_minimum);
					result.Add(0, -1, -word.CharacterCount);

					if(!word.Source.ContainsWordIndex(result.WordOffset)){
						return word.Source.FirstWord.Context;
					}
					return result.SolveCharacterOffset(word.PreviousWord, stayOnWord);
				}

				result.Add (0, 0, word.CharacterCount - WordOffset);
				return result;
			}

			return this;

		}
		#endregion
			
		#region Boolean States
		public bool IsStart {
			get {
				return this.Equals(Start);
			}
		}
		public bool IsEndOf (TextComponent_text text) {
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

	}

}