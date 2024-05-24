using SplashKitSDK;

public class Plate {
    private Bitmap _PlateBitmap;
    public Food _Food { get; private set; }
    public Drink _Drink { get; private set; }
    public double _Price { get { return _Food.Price + _Drink.Price; } }

    public Plate(Food food, Drink drink) {
        _PlateBitmap = new Bitmap("Plate", "resources/images/Plate.png");
        _Food = food;
        _Drink = drink;
    }

    public bool Compare(Plate otherPlate) {
        Food otherFood = otherPlate._Food;
        Drink otherDrink = otherPlate._Drink;
        if ( _Food.Compare(otherFood) && _Drink.Compare(otherDrink) ) {
            return true;
        }
        return false;
    }

    public void Draw(Player player) {
        // Draw bitmap 
        _PlateBitmap.Draw(player.X+player.Width/2-_PlateBitmap.Width/2, player.Y-_PlateBitmap.Height/2);
        _Food.DrawOnPlate(player, _PlateBitmap.Width, _PlateBitmap.Height);
        _Drink.DrawOnPlate(player, _PlateBitmap.Width, _PlateBitmap.Height);
    }
}