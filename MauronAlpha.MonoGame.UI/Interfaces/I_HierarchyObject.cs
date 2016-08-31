namespace MauronAlpha.MonoGame.UI.Interfaces {
	using MauronAlpha.MonoGame.Collections;
	using MauronAlpha.MonoGame.UI.Collections;

	public interface I_UIHierarchyObject {

		I_UIHierarchyObject Parent { get; }
		HierarchyChildren Children { get; }

	}

}