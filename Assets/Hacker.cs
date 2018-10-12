using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hacker : MonoBehaviour {

    //Game Config
    string[] libraryPasswords = { "password", "r34d1ng", "sT0ry", "HarryPotter", "c4rd007" };
    string[] policePasswords = { "password", "b4db0y5", "fi5-0", "p0p0", "fu77" };
    string[] nasaPasswords = { "r0v3r", "sP4ce007", "asan", "mArs123", "carpet" };
    string currentPassword;

    const string menuHint = "Type menu at any time.";

    //Gamestate
    enum Screen { MAINMENU, PASSWORD, WIN };
    enum Level { LIBRARY, POLICE, NASA };
    Screen currentScreen;
    Level currentLevel;

    bool iAmBond = false; //easter egg bool

    public Text passwordHint;
    public Text hintLabel;

    private float nextActionTime = 0.0f;
    public float period = 0.5f;

    void Update()
    {
        if(currentPassword != null)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                passwordHint.text = currentPassword.Anagram();
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
        currentScreen = Screen.MAINMENU;
        passwordHint.text = "";
        hintLabel.text = "";
        ShowMainMenu();
	}

    //Get user input (magic! in Terminal Class (InputBuffer) I think)
    void OnUserInput(string input)
    {
        switch (currentScreen)
        {
            case Screen.MAINMENU:
                RunMainMenu(input);
                break;
            case Screen.PASSWORD:
                CheckPassword(input);
                break;
            case Screen.WIN:
                RunWinMenu(input);
                break;
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");

        if (isValidLevelNumber)
        {
            int level = int.Parse(input);
            currentLevel = (Level)(level-1);
            SetPassword();
            StartGame();
        }
        else
        {
            switch (input)
            {
                case "menu":
                    ShowMainMenu();
                    break;
                case "clear":
                    ShowMainMenu();
                    break;
                case "exit":
                    ExitGame();
                    break;
                case "quit":
                    ExitGame();
                    break;
                case "007": //Easter Egg
                    iAmBond = true;
                    ShowMainMenu();
                    break;
                default:
                    Terminal.WriteLine("Please make a valid selection!");
                    break;
            }
        }
    }

    void SetPassword()
    {
        switch (currentLevel)
        {
            case Level.LIBRARY:
                currentPassword = libraryPasswords[ Random.Range(0, libraryPasswords.Length) ];
                break;
            case Level.POLICE:
                currentPassword = policePasswords[ Random.Range(0, policePasswords.Length) ];
                break;
            case Level.NASA:
                currentPassword = nasaPasswords[ Random.Range(0, nasaPasswords.Length) ];
                break;
        }
    }

    void StartGame()
    {
        currentScreen = Screen.PASSWORD;
        Terminal.ClearScreen();
        Terminal.WriteLine("admin@" + currentLevel);
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("Please enter your password:");
    }

    void CheckPassword(string input)
    {
        if (input == "menu")
            ShowMainMenu();
        else if (input == "exit" || input == "quit")
            ExitGame();
        else if (input == "clear")
            StartGame();
        else
            switch (currentLevel)
            {
                case Level.LIBRARY:
                    if (input == currentPassword)
                    {
                        ShowLibraryMenu();
                        break;
                    }
                    InvalidPassword();
                    break;
                case Level.POLICE:
                    if (input == currentPassword)
                    {
                        ShowPoliceMenu();
                        break;
                    }
                    InvalidPassword();
                    break;
                case Level.NASA:
                    if (input == currentPassword)
                    {
                        ShowNASAMenu();
                        break;
                    }
                    InvalidPassword();
                    break;
            }
    }

    void InvalidPassword()
    {
        Terminal.WriteLine("ERROR!!!@@!@!@! PLEASE TRY AGAIN:");
        Terminal.WriteLine("admin@" + currentLevel + ":");
        Terminal.WriteLine(menuHint);
    }

    void RunWinMenu(string input)
    {
        switch (input)
        {
            case "1":
                ShowMainMenu();
                break;
            case "exit":
                ExitGame();
                break;
            default:
                Terminal.WriteLine("Please make a valid selection.");
                break;
        }
    }

    void ShowMainMenu(string _greeting = "Welcome [ExceptionUserNotFound]")
    {
        if (iAmBond)
            _greeting = "Welcome back Mr. Bond";

        currentScreen = Screen.MAINMENU;
        currentPassword = null;
        passwordHint.text = "";
        Terminal.ClearScreen();
        Terminal.WriteLine(_greeting);
        Terminal.WriteLine("");
        Terminal.WriteLine("What would you like to hack today?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for Library");
        Terminal.WriteLine("Press 2 for Police Station");
        Terminal.WriteLine("Press 3 NASA");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection:");
    }

    void ShowLibraryMenu()
    {
        currentScreen = Screen.WIN;
        currentPassword = null;
        passwordHint.text = "";
        Terminal.ClearScreen();
        Terminal.WriteLine("You win a book!");
        Terminal.WriteLine(@"
                  _________
                 /  007   /|
                / Golden //
               /   Eye  //
              /________//
             (________(/
        ");
        Terminal.WriteLine("");
        Terminal.WriteLine("1 Main Menu");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection:");


    }

    void ShowPoliceMenu()
    {
        currentScreen = Screen.WIN;
        currentPassword = null;
        passwordHint.text = "";
        Terminal.ClearScreen();
        //Terminal.WriteLine("Welcome to the     ___  ___");
        Terminal.WriteLine(@"
                ___  ___   _
               / _ \/ _ \ | |
               \_, /\_, / \_/
 Welcome to the /_/  /_/  ( ) 
             
");
        Terminal.WriteLine("");
        Terminal.WriteLine("1 Main Menu");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection:");
    }

    void ShowNASAMenu()
    {
        SceneManager.LoadSceneAsync(1);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SpaceInvaders"));
//        currentScreen = Screen.WIN;
//        currentPassword = null;
//        passwordHint.text = "";
//        Terminal.ClearScreen();
//        //Terminal.WriteLine("Welcome to SPACE!");
//        Terminal.WriteLine(@"
//         _____ _____ _____ _____   
//        |   | |  _  |   __|  _  |  
//        | | | |     |__   |     |  
//        |_|___|__|__|_____|__|__| 
//");
//        Terminal.WriteLine("");
//        Terminal.WriteLine("1 Main Menu");
//        Terminal.WriteLine("");
//        Terminal.WriteLine("Enter your selection:");
    }

    void ExitGame()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Logging out... Have a good day :)");
        Application.Quit();
    }


}