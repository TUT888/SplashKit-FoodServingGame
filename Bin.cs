using SplashKitSDK;

public class Bin {
    private Bitmap _BinBitmap;
    public double X { get; private set; }
    public double Y { get; private set; }
    public int Width { get { return _BinBitmap.Width; } }
    public int Height { get { return _BinBitmap.Height; } }

    public Circle CollisionCircle { get { return SplashKit.CircleAt(X+Width/2, Y+Height/2, 80); } }

    public Bin(double inputX, double inputY) {
        _BinBitmap = new Bitmap("Bin", "resources/images/Bin.png");
        X = inputX;
        Y = inputY;
    }

    public void Draw() {
        _BinBitmap.Draw(X, Y);
    }
}