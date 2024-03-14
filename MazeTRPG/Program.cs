using MazeTRPG.Maze;

namespace MazeTRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(15);

            map.InitMaze();
            while (true)
            {
                Console.Clear();
                map.Rander();

                ConsoleKeyInfo key = Console.ReadKey();
                
                bool GameEnd1 = map.InputKey(key);                               
                
                bool GameEnd2 = map.MazeExit();        
                if (GameEnd1||GameEnd2) break;
            }            
        }
    }
}
