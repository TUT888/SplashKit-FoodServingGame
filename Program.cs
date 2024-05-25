using System;
using SplashKitSDK;

namespace CustomeProgramCode
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Game Window", 800, 700);
            
            FoodServingGame foodServingGame = new FoodServingGame(gameWindow);
            while ( !foodServingGame.Quit ) {
                SplashKit.ProcessEvents();

                foodServingGame.HandleInput();
                foodServingGame.Update();
                foodServingGame.Draw();

                if ( gameWindow.CloseRequested ) {
                    break;
                }
            }
            gameWindow.Close();
            
            Console.WriteLine($"Time record:    {foodServingGame.TimeRecord} second(s)");
            Console.WriteLine($"Total Serve:    {foodServingGame.TotalServe} serve(s)");
            Console.WriteLine($"Total Earn:     ${foodServingGame.TotalEarn}");
        }
    }
}
