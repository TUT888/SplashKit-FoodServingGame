using SplashKitSDK;

public class Customer {
    private Bitmap _CustomerBitmap;
    public double X { get; private set; }
    public double Y { get; private set; }
    public int Width { get { return _CustomerBitmap.Width; } }
    public int Height { get { return _CustomerBitmap.Height; } }
    public Circle CollisionCircle { get { return SplashKit.CircleAt(X+Width/2, Y+Height/2, Width+80); } }
    private Bitmap _FoodBitmap;
    private Bitmap _DrinkBitmap;

    public Plate orderedFnDPlate { get; set; }

    public Customer(Window gameWindow) {
        // Load the player image to _PlayerBitmap
        _CustomerBitmap = new Bitmap("Customer", "resources/images/Customer.png");

        // The player start in middle of the screen
        X = (gameWindow.Width - 75) - Width / 2;
        Y = (gameWindow.Height/2) - Height / 2;

        // Randomly order food
        orderedFnDPlate = RandomOrder();
    }

    public Plate RandomOrder() {
        Food orderedFood;
        Drink orderedDrink;
        int rndNum1 = SplashKit.Rnd(2);
        if ( rndNum1==0 ) {
            orderedFood = new Chip();
            _FoodBitmap = new Bitmap("OrderChip", "resources/images/Chip.png");
        } else {
            orderedFood = new Nugget();
            _FoodBitmap = new Bitmap("OrderNugget", "resources/images/Nugget.png");
        }
        // Randomly order drink
        int rndNum2 = SplashKit.Rnd(2);
        if ( rndNum2==0 ) {
            orderedDrink = new CocaCola();
            _DrinkBitmap = new Bitmap("OrderCocaCola", "resources/images/CocaCola.png");
        } else {
            orderedDrink = new Coffee();
            _DrinkBitmap = new Bitmap("OrderCoffee", "resources/images/Coffee.png");
        }
        return new Plate(orderedFood, orderedDrink);
    }

    public void Draw() {
        _CustomerBitmap.Draw(X, Y);
        double centerX = X + Width/2;
        double centerY = Y - _CustomerBitmap.Height/2 - 20;
        _FoodBitmap.Draw(centerX-_FoodBitmap.Width/2, centerY-_FoodBitmap.Height/2-_DrinkBitmap.Height-20);
        _DrinkBitmap.Draw(centerX-_DrinkBitmap.Width/2, centerY-_DrinkBitmap.Height/2);
    }

    public bool checkOrder(Plate givenPlate) {
        if ( givenPlate==null ) {
            return false;
        }
        if ( orderedFnDPlate.Compare(givenPlate) ) {
            return true;
        }
        return false;
    }
}