using SplashKitSDK;

public abstract class Food {
    protected Bitmap _FoodBitmap;
    protected Bitmap _MiniFoodBitmap;
    protected string Name { get; set; }
    public double Price { get; set; }

    public bool Compare(Food otherFood) {
        if ( otherFood.Name == Name) {
            return true;
        }
        return false;
    }

    public void Draw(Player player) {
        // Draw bitmap 
        // _FoodBitmap.Draw(player.X+player.Width/2-_FoodBitmap.Width/2, player.Y-_FoodBitmap.Height/2);
        _FoodBitmap.Draw(player.X-_FoodBitmap.Width/2, player.Y-_FoodBitmap.Height/2);
    }

    public void DrawOnPlate(Player player, double plateWidth, double plateHeight) {
        // Draw bitmap 
        double plateX = player.X + player.Width/2 - plateWidth/2;
        double plateY = player.Y - plateHeight/2;
        double plateXCenter = plateX + plateWidth/2;
        double plateYCenter = plateY + plateHeight/2;
        _MiniFoodBitmap.Draw(plateXCenter-plateWidth/4-_MiniFoodBitmap.Width/2, plateYCenter-_MiniFoodBitmap.Height/2);
    }
}

public class Chip : Food {
    public Chip() {
        _FoodBitmap = new Bitmap("Chip", "resources/images/Chip.png");
        _MiniFoodBitmap = new Bitmap("MiniChip", "resources/images/MiniChip.png");
        Name = "Chip";
        Price = 1;
    }
}

public class Nugget : Food {
    public Nugget() {
        _FoodBitmap = new Bitmap("Nugget", "resources/images/Nugget.png");
        _MiniFoodBitmap = new Bitmap("MiniChip", "resources/images/MiniNugget.png");
        Name = "Nugget";
        Price = 2;
    }
}