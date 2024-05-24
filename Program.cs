using System;
using SplashKitSDK;

namespace TutorialGame
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Game Window", 800, 700);
            
            CookingDashGame cookingDashGame = new CookingDashGame(gameWindow);
            while ( !cookingDashGame.Quit ) {
                SplashKit.ProcessEvents();

                cookingDashGame.HandleInput();
                cookingDashGame.Update();
                cookingDashGame.Draw();

                if ( gameWindow.CloseRequested ) {
                    break;
                }
            }
            gameWindow.Close();
            
            Console.WriteLine($"Time record:    {cookingDashGame.TimeRecord} second(s)");
            Console.WriteLine($"Total Serve:    {cookingDashGame.TotalServe} serve(s)");
            Console.WriteLine($"Total Earn:     {cookingDashGame.TotalEarn} $");
        }
    }
}
