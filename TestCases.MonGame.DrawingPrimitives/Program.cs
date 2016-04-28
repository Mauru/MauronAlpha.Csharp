using System;
using MauronAlpha.MonoGame.Scripts;

namespace MauronAlpha.MonoGame {
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			GameLogic logic = new TestLogic();
			MonoGameWrapper Game = new MonoGameWrapper(logic);
			Game.Run();
        }
    }
#endif
}
