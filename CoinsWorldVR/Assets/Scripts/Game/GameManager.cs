using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    menu,
    inTheGame,
    gameOver,
    wonTheGame
}

public class GameManager : MonoBehaviour
{
    // Actual GameState
    public GameState currentGameState;

    // singleton
    public static GameManager sharedInstance;

    public Canvas menuCanvas;
    public Canvas gameCanvas;
    // public Canvas gameCanvasVR;
    public Canvas gameOverCanvas;
    public Canvas gameWonCanvas;

    [HideInInspector]
    public bool theGameStart = false, thePlayerWon = false; // this is used to stop the timer 

    void Awake()
    {
        // Initialize the singleton and share all the GameManager fields and methods with it
        sharedInstance = this;
    }

    void Start()
    {
        currentGameState = GameState.menu;
        // Setup the canvas behaviour
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        // gameCanvasVR.enabled = false;
        gameOverCanvas.enabled = false;
        gameWonCanvas.enabled = false;
    }

    void Update()
    {
        // If the user want to quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitTheGame();
        }

        // This will display the menu if is the first time of the player in the game
        if (currentGameState != GameState.inTheGame && currentGameState != GameState.wonTheGame && GlobalStaticVariables.theUserResetTheGame == false)
        {
            if (OVRInput.GetUp(OVRInput.Button.Three))
            {
                StartGame();
            }
        } // If the player was kill or the time was over and he wants to continue playing
        else if (currentGameState != GameState.inTheGame && currentGameState != GameState.wonTheGame && GlobalStaticVariables.theUserResetTheGame == true)
        {
            StartGame();
        } // If the player won the game 
        else if (currentGameState == GameState.wonTheGame)
        {
            if (OVRInput.GetUp(OVRInput.Button.Three))
            {
                // Reload the scene
                GameSceneManager.sharedInstance.GameScene();
            }
        }

    }

    // Use this for start the game
    public void StartGame()
    {
        GlobalStaticVariables.theUserResetTheGame = false; 
        UICountDown.sharedInstance.StartTheCountDown();
        UICountDown.sharedInstance.PlayerMovement();
        theGameStart = true;
        ChangeGameState(GameState.inTheGame);
    }

    // Called when the player dies
    public void GameOver()
    {
        theGameStart = false;
        GameObject.FindGameObjectWithTag("HealthBar").SendMessage("ResetTheHealthBar");
        GameObject.FindGameObjectWithTag("ReloadBar").SendMessage("ResetTheReloadBar");
        ChangeGameState(GameState.gameOver);
    }

    // Called when the player dies
    public void GameWon()
    {
        GlobalStaticVariables.theUserResetTheGame = true;
        theGameStart = false;
        thePlayerWon = true;
        ChangeGameState(GameState.wonTheGame);
    }

    void QuitTheGame()
    {
        Application.Quit();
    }

    // This method will manage the states of the game
    void ChangeGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            // The logic of the principal menu
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            // gameCanvasVR.enabled = false;
            gameOverCanvas.enabled = false;
            gameWonCanvas.enabled = false;
        }
        else if (newGameState == GameState.inTheGame)
        {
            // This is the current scene or level of the game
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            // gameCanvasVR.enabled = true;
            gameOverCanvas.enabled = false;
            gameWonCanvas.enabled = false;
        }
        else if (newGameState == GameState.gameOver)
        {
            // Gameover
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            // gameCanvasVR.enabled = false;
            gameOverCanvas.enabled = true;
            gameWonCanvas.enabled = false;
        }
        else if (newGameState == GameState.wonTheGame)
        {
            // Gamewon
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            // gameCanvasVR.enabled = false;
            gameOverCanvas.enabled = false;
            gameWonCanvas.enabled = true;
        }

        // This is the new state after the change 
        currentGameState = newGameState;

    }
}
