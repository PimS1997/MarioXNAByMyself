using System;

/*
This game is made as a school project.
All the sprites used belong to Nintendo.
This project is by no means to make any form of profit and is purely a learing exercise.
*/

namespace SuperMarioWorldInXNA
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
#endif
}

