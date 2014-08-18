namespace MauronAlpha.Text.Units {

	public interface I_textComponent<T> {
		string AsString {get;}

		bool IsEmpty {get;}
		bool IsComplete {get;}
		bool HasWhiteSpace {get;}
		bool HasWordBreak {get;}
		bool HasLineBreak {get;}
		
		bool HasContext {get;}
		T SetContext(TextContext context);
		TextContext Context { get; }

		TextComponent_text Source { get; }
		
		T Instance { get; }
	}
}
