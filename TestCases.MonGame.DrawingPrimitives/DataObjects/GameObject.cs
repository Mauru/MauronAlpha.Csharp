using System;

namespace MauronAlpha.MonoGame.DataObjects {
	public class GameObject:MonoGameComponent {

		private bool B_isDrawable = false;
		public bool IsDrawable { get { return B_isDrawable; } }

		public GameObject(bool isDrawable) : base() {
			B_isDrawable = isDrawable;
		}

	}
}
