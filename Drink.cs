using SplashKitSDK;

public abstract class Drink {
    protected Bitmap _DrinkBitmap;
    protected Bitmap _MiniDrinkBitmap;
    protected string Name { get; set; }
    public double Price { get; set; }

    public bool Compare(Drink otherDrink) {
        if ( otherDrink.Name == Name) {
            return true;
        }
        return false;
    }

    public void Draw(Player player) {
        // Draw bitmap 
        // _FoodBitmap.Draw(player.X+player.Width/2-_FoodBitmap.Width/2, player.Y-_FoodBitmap.Height/2);
        _DrinkBitmap.Draw(player.X+player.Width-_DrinkBitmap.Width/2, player.Y-_DrinkBitmap.Height/2);
    }

    public void DrawOnPlate(Player player, double plateWidth, double plateHeight) {
        // Draw bitmap 
        double plateX = player.X + player.Width/2 - plateWidth/2;
        double plateY = player.Y - plateHeight/2;
        double plateXCenter = plateX + plateWidth/2;
        double plateYCenter = plateY + plateHeight/2;
        _MiniDrinkBitmap.Draw(plateXCenter+plateWidth/4-_MiniDrinkBitmap.Width/2, plateYCenter-_MiniDrinkBitmap.Height/2);
    }
}

public class CocaCola : Drink {
    public CocaCola() {
        _DrinkBitmap = new Bitmap("CocaCola", "resources/images/CocaCola.png");
        _MiniDrinkBitmap = new Bitmap("MiniCocaCola", "resources/images/MiniCocaCola.png");
        Name = "CocaCola";
        Price = 1;
    }
}

public class Coffee : Drink {
    public Coffee() {
        _DrinkBitmap = new Bitmap("Coffee", "resources/images/Coffee.png");
        _MiniDrinkBitmap = new Bitmap("MiniCoffee", "resources/images/MiniCoffee.png");
        Name = "Coffee";
        Price = 2;
    }
}