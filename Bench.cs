using SplashKitSDK;

public class Bench {
    // Food info
    private Bitmap _FnDBitmap;
    public string FnDType { get; set; }
    // Table info
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get { return 100; } }
    public double Height { get { return 100; } }
    public SplashKitSDK.Color MainColor { get; set; }
    public Circle CollisionCircle { get { return SplashKit.CircleAt(X+Width/2, Y+Height/2, 70); } }

    public Bench(Window gameWindow, double inputX, double inputY, string inputType) {
        MainColor = Color.AntiqueWhite;

        X = inputX;
        Y = inputY;
        FnDType = inputType;
        if ( FnDType=="Chip" ) {
            _FnDBitmap = new Bitmap("ChipOnBench", "resources/images/Chip.png");
        } else if ( FnDType=="Nugget" ) {
            _FnDBitmap = new Bitmap("NuggetOnBench", "resources/images/Nugget.png");
        } else if ( FnDType=="CocaCola" ) {
            _FnDBitmap = new Bitmap("CocaColaOnBench", "resources/images/CocaCola.png");
        } else if ( FnDType=="Coffee" ) {
            _FnDBitmap = new Bitmap("CoffeeOnBench", "resources/images/Coffee.png");
        } else if ( FnDType=="Plate" ) {
            _FnDBitmap = new Bitmap("PlateOnBench", "resources/images/Plate.png");
        }
    }

    public void Draw() {
        SplashKit.FillRectangle(MainColor, X, Y, Width, Height);
        double centerX = X + Width/2;
        double centerY = Y + Height/2;
        _FnDBitmap.Draw(centerX-_FnDBitmap.Width/2, centerY-_FnDBitmap.Height/2);
    }

    public Food generateFood() {
        if ( FnDType=="Chip" ) {
            return new Chip();
        } else if ( FnDType=="Nugget" ) {
            return new Nugget();
        }
        return null;
    }

    public Drink generateDrink() {
        if ( FnDType=="CocaCola" ) {
            return new CocaCola();
        } else if ( FnDType=="Coffee" ) {
            return new Coffee();
        }
        return null;
    }

    public Plate generatePlate(Food food, Drink drink) {
        if ( food!=null && drink!=null ) {
            return new Plate(food, drink);
        }
        return null;
    }
}