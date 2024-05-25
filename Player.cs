using SplashKitSDK;

public class Player {
    // Basic player information
    private Bitmap _PlayerBitmap;
    private const double SPEED = 5.0;
    public double X { get; private set; }
    public double Y { get; private set; }
    public int Width { get { return _PlayerBitmap.Width; } }
    public int Height { get { return _PlayerBitmap.Height; } }

    // Extra fields
    public double LAND_HEIGHT { get; set; }
    public bool Quit { get; private set; }
    public int TotalServe { get; private set; }
    public int TimeRecord { get; private set; }
    public double TotalEarn { get; private set; }

    // Special fiels for Food Serving game
    public Food HoldingFood { get; set; } = null;
    public Drink HoldingDrink { get; set; } = null;
    public Plate HoldingPlate { get; set; } = null;

    public Player(Window gameWindow) {
        // Load the player image to _PlayerBitmap
        _PlayerBitmap = new Bitmap("Player", "resources/images/Player.png");

        // The player start in middle of the screen
        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Width) / 2;

        TotalServe = 0;                  
        TimeRecord = 0;
        TotalEarn = 0;
    }

    public void Draw() {
        // Draw bitmap 
        _PlayerBitmap.Draw(X, Y);
        if ( HoldingFood!=null ) {
            HoldingFood.Draw(this);
        }
        if ( HoldingDrink!=null ) {
            HoldingDrink.Draw(this);
        }
        if ( HoldingPlate!=null ) {
            HoldingPlate.Draw(this);
        }
    }

    public void HandleInput() {
        // Speed up for dashing
        double actual_spd = SPEED; 
        if ( SplashKit.KeyDown(KeyCode.SpaceKey) ) {
            actual_spd *= 2;
        }

        // Basic move
        if ( SplashKit.KeyDown(KeyCode.UpKey) ) {
            Y -= actual_spd;
        }
        if ( SplashKit.KeyDown(KeyCode.DownKey) ) {
            Y += actual_spd;
        }
        if ( SplashKit.KeyDown(KeyCode.LeftKey) ) {
            X -= actual_spd;
        }
        if ( SplashKit.KeyDown(KeyCode.RightKey) ) {
            X += actual_spd;
        }

        // Option to quit the game
        if ( SplashKit.KeyDown(KeyCode.EscapeKey) ) {
            Quit = true;
        }
    }


    // Let the player stayed in the kitchen
    public void StayOnArea(int[] limitArea) {
        // Define the gap between player and window frames
        const int GAP = 10;
        int GAP_left = limitArea[0] + GAP;
        int GAP_top = limitArea[1] + GAP;
        int GAP_right = limitArea[2] - GAP;
        int GAP_bottom = limitArea[3] - GAP;

        // Keep the player in the window
        if ( X < GAP_left ) {
            X = GAP_left;
        }
        if ( (X+Width) > GAP_right ) {
            X = GAP_right - Width;
        }
        if ( Y < GAP_top ) {
            Y = GAP_top;
        }
        if ( (Y+Height) > GAP_bottom ) {
            Y = GAP_bottom - Height;
        }
    }

    // ====== Overloadding methods for checking interaction ====== //
    public bool CheckInteract(Bench bench) {
        // Check if the item fall on the player
        return _PlayerBitmap.CircleCollision(X, Y, bench.CollisionCircle);
    }

    public bool CheckInteract(Customer customer) {
        // Check if the item fall on the player
        return _PlayerBitmap.CircleCollision(X, Y, customer.CollisionCircle);
    }

    public bool CheckInteract(Bin bin) {
        // Check if the item fall on the player
        return _PlayerBitmap.CircleCollision(X, Y, bin.CollisionCircle);
    }

    // ====== Overloadding methods for handling interaction ====== //
    public void HandleFoodInteraction(Bench bench) {
        if ( SplashKit.KeyDown(KeyCode.AKey) ) {
            // Only hold food/drink when not holding plate
            if ( HoldingPlate==null ) {
                HoldingFood = bench.generateFood();
            }
        }
    }

    public void HandleDrinkInteraction(Bench bench) {
        if ( SplashKit.KeyDown(KeyCode.AKey) ) {
            // Only hold food/drink when not holding plate
            if ( HoldingPlate==null ) {
                HoldingDrink = bench.generateDrink();
            }
        }
    }

    public void HandlePlateInteraction(Bench bench) {
        if ( SplashKit.KeyDown(KeyCode.AKey) ) {
            // Only hold plate if it has both food and drink
            if ( HoldingFood!=null && HoldingDrink!=null ) {
                HoldingPlate = bench.generatePlate(HoldingFood, HoldingDrink);
                HoldingDrink = null;
                HoldingFood = null;
            }
        }
    }

    public void HanldeBinInteraction() {
        if ( SplashKit.KeyDown(KeyCode.DKey) ) {
            // Clear all holding items
            HoldingPlate = null;
            HoldingDrink = null;
            HoldingFood = null;
        }
    }

    // ====== Serving food -> Check if it is the same ====== //
    public bool ServeFood(Customer customer) {
        if ( SplashKit.KeyDown(KeyCode.AKey) ) {
            if ( customer.checkOrder(HoldingPlate) ) {
                return true;
            }
        }
        return false;
    }
    
    public void updateScore() {
        TotalServe += 1;
        TotalEarn += HoldingPlate._Price;

        HoldingPlate = null;
    }

    // ====== Updating game progress ====== //
    public void UpdateProgress(SplashKitSDK.Timer timer) {
        TimeRecord = Convert.ToInt32(timer.Ticks / 1000);
    }

    public void DrawProgress(Window gameWindow) {
        SplashKit.DrawText($"TimeRecord: {TimeRecord} second(s)", SplashKitSDK.Color.Black, "BOLD_FONT", 15, gameWindow.Width/2-100, 20);
        SplashKit.DrawText($"Total serve: {TotalServe} serve", SplashKitSDK.Color.Black, "BOLD_FONT", 15, gameWindow.Width/2-100, 40);
        SplashKit.DrawText($"Total earn: ${TotalEarn}", SplashKitSDK.Color.Black, "BOLD_FONT", 15, gameWindow.Width/2-100, 60);
    }
}