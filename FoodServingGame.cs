using SplashKitSDK;

public class FoodServingGame {
    // The player and customer
    private Player _Player;
    private int[] _PlayerArea;
    private Customer _Customer;
    // Kitchen objects to get food
    private Bench _ChipFoodBench;
    private Bench _NuggetFoodBench;
    private Bench _CocaColaDrinkBench;
    private Bench _CoffeeDrinkBench;
    private Bench _PlateBench;
    // The bin to discard wrong food combination
    private Bin _Bin;

    private Window _GameWindow;
    public bool Quit { get { return _Player.Quit; } }
    private SplashKitSDK.Timer _GameTimer;
    public int TimeRecord { get { return _Player.TimeRecord; } }
    public int TotalServe { get { return _Player.TotalServe; } }
    public double TotalEarn { get { return _Player.TotalEarn; } }


    public FoodServingGame(Window gameWindow) {
        _GameWindow = gameWindow;
        _GameTimer = new SplashKitSDK.Timer("Game Timer");
        _GameTimer.Start();

        // Initialize Player
        _Player = new Player(_GameWindow);
        // Restriction on the area the player can move
        _PlayerArea = [100, 0, _GameWindow.Width-200, _GameWindow.Height]; //startX startY endX endY

        // Initialize Customer
        _Customer = new Customer(_GameWindow);

        // Initialize Food bench
        _ChipFoodBench = new Bench(_GameWindow, 0, 0, "Chip");
        _NuggetFoodBench = new Bench(_GameWindow, 0, 120, "Nugget");
        _CocaColaDrinkBench = new Bench(_GameWindow, 0, 240, "CocaCola");
        _CoffeeDrinkBench = new Bench(_GameWindow, 0, 360, "Coffee");
        _PlateBench = new Bench(_GameWindow, 0, 480, "Plate");
        // Initialize Bin
        _Bin = new Bin(20, 620);
    }

    public void HandleInput() {
        _Player.HandleInput();
        _Player.StayOnArea(_PlayerArea);
    }

    public void Update() {
        _Player.UpdateProgress(_GameTimer);
        CheckInteraction();
    }
    
    public void Draw() {
        _GameWindow.Clear(Color.White);

        // Draw the table
        SplashKit.FillRectangle(
            SplashKitSDK.Color.SandyBrown, 
            _PlayerArea[2], 0, 
            50, _GameWindow.Height
        );
        // Draw kitchen items
        _ChipFoodBench.Draw();
        _NuggetFoodBench.Draw();
        _CocaColaDrinkBench.Draw();
        _CoffeeDrinkBench.Draw();
        _PlateBench.Draw();
        _Bin.Draw();

        // Draw all Items
        _Player.Draw();
        _Customer.Draw();
        
        // Draw game progress
        _Player.DrawProgress(_GameWindow);
        // Refresh window to make changes
        _GameWindow.Refresh(60);
    }

    private void CheckInteraction() {
        // Interact with food benches
        if ( _Player.CheckInteract(_ChipFoodBench) ) {
            _Player.HandleFoodInteraction(_ChipFoodBench);
        } else if ( _Player.CheckInteract(_NuggetFoodBench) ) {
            _Player.HandleFoodInteraction(_NuggetFoodBench);
        } 
        // Interact with drink benches
        else if ( _Player.CheckInteract(_CocaColaDrinkBench) ) {
            _Player.HandleDrinkInteraction(_CocaColaDrinkBench);
        } else if ( _Player.CheckInteract(_CoffeeDrinkBench) ) {
            _Player.HandleDrinkInteraction(_CoffeeDrinkBench);
        } 
        // Interact with plate bench
        else if ( _Player.CheckInteract(_PlateBench) ) {
            _Player.HandlePlateInteraction(_PlateBench);
        }

        // Interact with bin
        if ( _Player.CheckInteract(_Bin) ) {
            _Player.HanldeBinInteraction();
        }

        // Interact with customer & serve food
        if ( _Player.CheckInteract(_Customer) ) {
            bool result = _Player.ServeFood(_Customer);
            // If serving successful, then create other new customer
            if ( result ) {
                _Player.updateScore();
                _Customer = new Customer(_GameWindow);
            }
        } 
    }
}