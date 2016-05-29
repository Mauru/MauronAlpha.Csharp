using System;

namespace MauronAlpha.MonoGameEngine {
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
			MonoGameWrapper game = new MonoGameWrapper();
			game.Run();
			while (!game.CanExit) { 
				game.CheckExitCondition();
			}
        }
    }
#endif
}
