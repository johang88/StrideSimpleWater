using Stride.Engine;

namespace SimpleWater
{
    class SimpleWaterApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
