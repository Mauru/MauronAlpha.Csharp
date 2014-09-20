using MauronAlpha.HandlingData;
using MauronAlpha.HandlingErrors;
using MauronAlpha.Text.Utility;

using System.Collections.Generic;
using System;

namespace MauronAlpha.Text.Units {

	//A word
	public class TextComponent_word : MauronCode_textComponent, I_textComponent<TextComponent_word>, IEquatable<string> {
		
		//constructor
		public TextComponent_word(TextComponent_line parent, TextContext context) {}
	
		#region Instance (creates clone)
		public TextComponent_word Instance {
			get {
				TextComponent_word result = new TextComponent_word(Parent,Context.Instance);
				foreach(TextComponent_character c in Characters){
					result.AddCharacter(c.Instance);
				}
				return this;
			}
		}
		#endregion

		#region The Characters in this Word
		private MauronCode_dataList<TextComponent_character> DATA_characters;
		public MauronCode_dataList<TextComponent_character> Characters {
			get {
				if(DATA_characters==null){
					DATA_characters = new MauronCode_dataList<TextComponent_character>();
				}
				return DATA_characters;
			}
		}
		#endregion

		#region Relatives By Context
		private TextContext NextCharacterContext {
			get {
				return Context.Instance.SetCharacterOffset(CharacterCount);
			}
		}

		public TextComponent_word NextWord {
			get {
				
				//The next Word is not on this line
				if( IsAtLineEnd ) {

					//There is no next word !return
					if( Parent.IsLastLine ) {
						Exception("No next Word!,{"+Context.AsString+"},(NextWord)", this, ErrorResolution.Correct_maximum);
						return this;
					}
					
					return Parent.Parent.LineByIndex(Parent.Index+1).FirstWord;
				}
				
				return Parent.WordByIndex(Index+1);

			}
		}
		public TextComponent_word PreviousWord {
			get {
				
				//not on this Line
				if( IsAtLineStart ){
					
					#region Is a MultiWord	
					if(IsMultiWord){

						//starts multiword
						if( MultiWordIndex == 0 ){
							
							//Is there a previous Line
							if( Parent.IsAtTextStart ) {
								Exception("There is no previous word!,{"+Context.AsString+"}",this,ErrorResolution.Correct_minimum);
								return this;
							}

							return Parent.Parent.LineByIndex(Parent.Index-1).LastWord;

						}

						//ends multiword
						if( MultiWordIndex >= Partners.Count ){
							
							//there are other multiword parts before this part, return by offset

							if( Parent.IsAtTextStart ) {
								Error("There is no previous word! Also, the MultiWord is broken!,{"+Context.AsString+"}", this, ErrorType_bug.Instance);
								return this;
							}

							return PreviousPartner.PreviousWord;

						}
					}
					#endregion

				}

			}
		}

		#endregion

		#region Boolean States by Context

		public bool IsAtLineEnd {
			get {
				if( Parent.WordCount==1 ){
					return true;
				}
				if( TerminatesLine ) {
					return true;
				}
				if( Parent.WordCount-1==Index ){
					return true;
				}
				return false;
			}
		}

		#endregion

		#region ReadOnly
		private bool B_isReadOnly=false;
		public bool IsReadOnly {
			get { return B_isReadOnly; }
		}
		public TextComponent_word SetReadOnly (bool status) {
			B_isReadOnly=status;
			return this;
		}
		#endregion

		#region Modification of the word

		#region Set all Characters at once
		/// <summary>Set the characters of a word
		/// <remarks>Clears Characters then applies AddCharacter to each</remarks></summary>
		public TextComponent_word SetCharacters (MauronCode_dataList<TextComponent_character> characters) {
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(SetCharacters)", this, ErrorType_protected.Instance);
			}
			#endregion
			//1: reset internal list
			DATA_characters=new MauronCode_dataList<TextComponent_character>();
			foreach( TextComponent_character c in characters ) {
				AddCharacter(c);
			}
			return this;
		}
		#endregion

		#region Add a character to the word
		/// <summary>Add a character to the end of the string
		/// <remarks>Adding \0 (null) will throw an Exception</remarks>
		/// </summary>
		public TextComponent_word AddCharacter(TextComponent_character c){
			#region ReadOnlyCheck : !Error
			if( IsReadOnly ) {
				Error("Is protected!,(AddCharacter)", this, ErrorType_protected.Instance);
			}
			#endregion
			
			return InsertCharacterAtIndex(CharacterCount,c);
		}
		public TextComponent_word InsertCharacterAtIndex(int index,TextComponent_character c) {
			#region ReadOnlyCheck : !Error
			if( IsReadOnly ) {
				Error("Is protected!,(AddCharacterAtIndex)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region ExceptionCheck: empty
			if(IsEmpty){
				Exception("Characters are empty!(AddCharacterAtIndex)",this, ErrorResolution.Function("AddCharacter"));
				index=CharacterCount;
			}
			#endregion
			#region ExceptionCheck: c == \0 : do nothing !returns
			if( c.IsEmpty ) {
				Exception("Can not add \0 to a word!,(AddCharacterAtIndex)", this, ErrorResolution.DoNothing);
				return this;
			}
			#endregion
			
			#region Get all characters after index ?!returns
			#region ExceptionCheck: index out of Bounds ?!returns
			if(index<0){
				Exception("Index out of Bounds!,{"+index+"},(AddCharacterByIndex)",this,ErrorResolution.Correct_minimum);
				index=0;
			}
			else if(index>Characters.NextIndex){
				Exception("Index out of Bounds!,{"+index+"},(AddCharacterByIndex)",this,ErrorResolution.Function("AddCharacter"));
				return AddCharacter(c);
			}
			#endregion
			MauronCode_dataList<TextComponent_character> characters=Characters.Range(index);
			#endregion

			#region New character ends word
			if(characters.Count>0 && c.EndsWord){

				//create new word
				TextComponent_word newWord=Instance.SetCharacters(characters);
				Parent.InsertWordAtIndex(Context.LineOffset+1,newWord);
					
				//Remove the character from this word
				for(int n=0;n<characters.Count;n++){
					RemoveCharacterAtIndex(n+index);
				}

				//Add new character
				index=0;
			}
			#endregion

			#region Insert character and offset following characters !returns

			c.SetContext(Context.Instance.SetCharacterOffset(index));
			Characters.InsertValueAt(index,c);
			
			//offset context of following characters
			foreach(TextComponent_character ch in characters) {
				c.Context.Add(new TextContext(0,0,1));
			}
			return this;
			#endregion
		}		
		#endregion

		#region Remove from word //TODO: potentially update parent, on empty remove word from parent
		//Remove the last character
		private TextComponent_word RemoveLastCharacter() {
			#region ReadOnly Check
			if( IsReadOnly ) {
				Error("Is protected!,(RemoveLastCharacter)", this, ErrorType_protected.Instance);
			}
			#endregion
			#region Error Check
			if(RealCount==0){
				Error("Characters is empty (RemoveLastCharacter)",this, ErrorType_index.Instance);
			}
			#endregion
			Characters.RemoveLastElement();
			return this;
		}
		public TextComponent_word RemoveCharacterAtIndex(int n){
			if(n<0||n>=CharacterCount){
				Error("CharacterIndex out of bounds!,{"+n+"},(RemoveCharacterAtIndex)",this,ErrorType_bounds.Instance);
			}

			TextComponent_character ch=Characters.Value(n);

			//all following characters
			MauronCode_dataList<TextComponent_character> characters=Characters.Range(n);

			//remove character
			Characters.RemoveByKey(n);

			foreach(TextComponent_character c in characters){
				c.Context.Add(0,0,-1);
			}

			//character ends word
			if(ch.EndsWord){
				
				//character ends line
				if(ch.TerminatesLine){
					//There is a next line
					if(Parent.HasOffsetNeighbor(1,0,0)){
						
						MauronCode_dataList<TextComponent_word> words = Parent.NextLine.Words;
					}
				}
			
			}

			return this;
		}
		#endregion

		#endregion

		#region The TextComponent_line this element belongs to
		private TextComponent_line TXT_parent;
		public TextComponent_line Parent {
			get {
				#region Error Check
				if(TXT_parent==null){
					NullError("Parent can not be null!,(Parent)",this,typeof(TextComponent_line));
				}
				#endregion
				return TXT_parent;
			}
		}
		private TextComponent_word SetParent(TextComponent_line line){
			TXT_parent=line;
			return this;
		}
		#endregion
		#region The TextComponent_text this element belongs to
		public TextComponent_text Source {
			get {
				return Parent.Source;
			}
		}
		#endregion

		#region The Context of this TextComponent
		private TextContext TXT_context;
		public TextContext Context {
			get {
				#region Error Check:null
				if(TXT_context==null){
					NullError("Context can not be null!,(Context)",this,typeof(TextContext));
				}
				#endregion
				return TXT_context;
			}
		}

		private TextComponent_word SetContext(TextContext context){
			TXT_context=context;
			return this;
		}
		public TextComponent_word OffsetContext (TextContext context) {
			Context.Add(context);
			foreach( TextComponent_character c in Characters ) {
				c.OffsetContext(context);
			}
			return this;
		}
		public TextComponent_word OffsetContext (int line, int word, int character) {
			Context.Add(line, word, character);
			foreach( TextComponent_character c in Characters ) {
				c.OffsetContext(line, word, character);
			}
			return this;
		}
		#endregion

		#region The Index {Word Number} (by context)
		public int Index {
			get {
				return Context.WordOffset;
			}
		}
		#endregion

		#region The Content (Characters in this Word)

		//Strict! First Character
		public TextComponent_character FirstCharacter {
			get {
				#region Error Check
				if( CharacterCount<1 ) {
					Error("Character Index out of bounds!,(FirstCharacter)", this, ErrorType_index.Instance);
				}
				#endregion
				return Characters.FirstElement;
			}
		}
		
		//Strict! Last Character
		public TextComponent_character LastCharacter {
			get {
				#region Error Check
				if( CharacterCount<1 ) {
					Error("Character Index out of bounds!,(LastCharacter)", this, ErrorType_index.Instance);
				}
				#endregion
				return Characters.LastElement;
			}
		}

		/// <summary>
		/// Get a character By Context
		/// <remarks>Uses CharacterByIndex, can throw OutofBoundsError</remarks>
		/// </summary>
		public TextComponent_character CharacterByContext(TextContext context) {
			return CharacterByIndex(context.CharacterOffset);
		}
		/// <summary>
		/// Character By Index
		/// <remarks>Throws Error if Empty or out of bounds</remarks>
		/// </summary>
		public TextComponent_character CharacterByIndex(int index){
			#region ErrorCheck: Empty
			if(IsEmpty||!Characters.ContainsKey(index)){
				Error("Index out of bounds!,{"+index+"},(CharacterByIndex)",this);
			}
			#endregion
			return Characters.Value(index);
		}
		
		#region Get a range of characters
		/// <summary>
		/// Get a range of characters
		/// <remarks>Ignores Texthelper.Empty (null) characters</remarks>
		/// </summary>
		public MauronCode_dataList<TextComponent_character> CharactersByRange (int index, int count) {
			MauronCode_dataList<TextComponent_character> result=new MauronCode_dataList<TextComponent_character>();

			#region ExceptionCheck: empty !returns
			if( IsEmpty ) {
				Exception("Characters are empty!,{"+index+"},(CharactersByRange)", this, ErrorResolution.ReturnEmpty);
				return result;
			}
			#endregion
			#region ExceptionCheck: index out of bounds
			if( index<0 ) {
				Exception("Index out of bounds!,{"+index+"},(CharactersByRange)", this, ErrorResolution.Correct_minimum);
				index=0;
			}
			if( index>CharacterCount ) {
				Exception("Index out of bounds!,{"+index+"},(CharactersByRange)", this, ErrorResolution.Correct_maximum);
				index=Characters.LastIndex;
			}
			#endregion
			#region ExceptionCheck: count ?!returns
			if( count<0 ) {
				Exception("Invalid Count!,{"+count+"},(CharactersByRange)", this, ErrorResolution.ReturnEmpty);
				return result;
			}
			if( index+count>=CharacterCount ) {
				Exception("Invalid Count!,{"+count+"},(CharactersByRange)", this, ErrorResolution.Correct_maximum);
				count=CharacterCount-index;
			}
			#endregion

			for( int n=0; n<count; n++ ) {
				int searchIndex=n+index;
				if( Characters.ContainsKey(searchIndex) ) {
					TextComponent_character c=Characters.Value(searchIndex);
					if( !c.IsEmpty ) {
						result.AddValue(c);
					}
				}
			}
			return result;
		}
		#endregion

		#endregion

		#region Count Characters in word
		/// <summary> Number of characters in the word 
		/// <remarks>
		/// If the ONLY character in this word is TextHelper.Empty, this function will still return 0
		/// </remarks>
		/// </summary>
		public int CharacterCount {
			get { 
				if(Characters.Count==1&&LastCharacter.IsEmpty){
					return 0;
				}
				return Characters.Count;
			}
		}
		/// <summary>Counts the real contents of the characters array (i.e. null)</summary>
		internal int RealCount {
			get {
				return Characters.Count;
			}
		}
		#endregion

		#region MultiWords (Partners)
		
		private int INT_multiWordIndex=0;
		public int MultiWordIndex {
			get {
				return INT_multiWordIndex;
			}
		}
		private MauronCode_dataList<TextComponent_word> MULTIWORD_partners;
		public MauronCode_dataList<TextComponent_word> Partners {
			get {
				if(MULTIWORD_partners==null){
					MULTIWORD_partners=new MauronCode_dataList<TextComponent_word>();
				}
				return MULTIWORD_partners;
			}
		}
		
		/// <summary>
		/// Gets the Last Part of a Multiword
		/// </summary>
		public TextComponent_word LastPartner {
			get {
				if(Partners.Count<1){
					Error("No Partners!",this,ErrorType_bounds.Instance);
				}
				return Partners.LastElement;
			}
		}
		public TextComponent_word PreviousPartner {
			get {
				if(!Partners.ContainsKey(MultiWordIndex-1)){
					Error("MultiWordIndex out of bounds!{"+(MultiWordIndex-1)+"},(PreviousPartner)",this,ErrorType_bounds.Instance);
					return this;
				}
				return Partners.Value(MultiWordIndex-1);
			}
		}
		public TextComponent_word NextPartner {
			get {
				if(!Partners.ContainsKey(MultiWordIndex+1)){
					Error("MultiWordIndex out of bounds!{"+(MultiWordIndex+1)+"},(NextPartner)", this, ErrorType_bounds.Instance);
					return this;
				}
				return Partners.Value(MultiWordIndex+1);
			}
		}

		#endregion

		#region Boolean States
		
		public bool IsMultiWord {
			get {
				if(Parent.CanBreak){
					return false;
				}
				return Partners.Count>0;
			}
		}
		public bool EndsLine {
			get {
				if( Characters.Count<1 ) {
					return false;
				}
				if( IsMultiWord ){
					if(!Parent.CanBreak){
						return false;
					}
					return(LastPartner.Parent.Index==Parent.Index);
				}
				return ( TerminatesLine );

			}
		}
		public bool TerminatesLine {
			get {
				if( Characters.Count<1 ){
					return false;
				}
				return Characters.LastElement.TerminatesLine;
			}
		}
		public bool IsLastOnLine {
			get {
				return Parent.ContainsWordIndex(Context.WordOffset+1);
			}
		}
		public bool IsAtTextStart {
			get {
				return Context.LineOffset==0 && Context.WordOffset==0;
			}
		}
		public bool IsAtLineStart {
			get {
				return Context.WordOffset==0;
			}
		}
		public bool IsComplete {
			get {
				return (Characters.Count>0&&LastCharacter.EndsWord);
			}
		}
		///<summary>Is the word empty (no characters) or contains only a null character</summary>
		public bool IsEmpty {
			get {
				if(CharacterCount<1) return true;
				if(LastCharacter.Equals(TextHelper.Empty)) return true;
				return false;
			}
		}
		public bool HasLineBreak {
			get {
				foreach(TextComponent_character c in Characters){
					if(c.IsLineBreak){ return true; }
				}
				return false;
			}
		}
		public bool HasWhiteSpace {
			get {
				foreach( TextComponent_character c in Characters ) {
					if( c.IsWhiteSpace ) { return true; }
				}
				return false;	
			}
		}
		public bool HasWordBreak {
			get {
				foreach(TextComponent_character c in Characters){
					if(c.IsWordBreak){ return true; }
				}
				return false;	
			}
		}
		#endregion
		#region Boolean State checks involving Characters
		public bool ContainsCharacterIndex (int n) {
			return n>=0&&n<CharacterCount;
		}
		#endregion
		#region Boolean States by context
		public bool HasContext {
			get {
				return TXT_context==null;
			}
		}
		public bool ContainsContext (TextContext context) {
			if(
				(Context.LineOffset!=context.LineOffset)
				||(Context.WordOffset!=context.WordOffset)
				||(FirstCharacter.Context.CharacterOffset>context.CharacterOffset)
				||(LastCharacter.Context.CharacterOffset<context.CharacterOffset)
				) {
				return false;
			}
			return true;
		}
		#endregion

		//Output as string, ignore TextHelper.Empty
		public string AsString {
			get {
				string result="";
				foreach( TextComponent_character c in Characters ) {
					if(!c.IsEmpty){
						result+=c.AsString;
					}
				}
				return result;
			}
		}

		#region I_textComponent
		string I_textComponent<TextComponent_word>.AsString {
			get { return AsString; }
		}
		bool I_textComponent<TextComponent_word>.IsEmpty {
			get { return IsEmpty; }
		}
		bool I_textComponent<TextComponent_word>.IsComplete {
			get { return IsComplete; }
		}
		bool I_textComponent<TextComponent_word>.HasWhiteSpace {
			get { return HasWhiteSpace; }
		}
		bool I_textComponent<TextComponent_word>.HasWordBreak {
			get { return HasWordBreak; }
		}
		bool I_textComponent<TextComponent_word>.HasLineBreak {
			get { return HasLineBreak; }
		}
		bool I_textComponent<TextComponent_word>.HasContext {
			get { return HasContext; }
		}
		TextComponent_word I_textComponent<TextComponent_word>.SetContext (TextContext context) {
			return SetContext(context);
		}
		TextContext I_textComponent<TextComponent_word>.Context {
			get { return Context; }
		}
		TextComponent_text I_textComponent<TextComponent_word>.Source {
			get { return Source; }
		}
		TextComponent_word I_textComponent<TextComponent_word>.Instance {
			get { return Instance; }
		}
		#endregion
		#region IEquatable<string>
		bool IEquatable<string>.Equals (string other) {
			return other.Equals(other);
		}
		#endregion

	}
}