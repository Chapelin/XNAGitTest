using System;

namespace TTRPG_case
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
            //GameForm form = new GameForm();
            //form.Show();
            //Game1 game = new Game1();
            //game.Run();      
        }
    }
#endif
}

