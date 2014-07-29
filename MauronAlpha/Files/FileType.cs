namespace MauronAlpha {

    public abstract class FileType:MauronCode_subtype {
		public abstract string Name { get; }
		public abstract string Extension { get; }
		public abstract string Description { get; }
    }

}
