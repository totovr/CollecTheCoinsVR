using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CurrentSceneStage
{ InTheGame }


public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager sharedInstance;

    [HideInInspector]
    public CurrentSceneStage currentScene;

    public string[] GameWorldScenes;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        currentScene = CurrentSceneStage.InTheGame;
    }

    public void GameScene()
    {
        CurrentSceneGameManager(CurrentSceneStage.InTheGame);
    }

    void CurrentSceneGameManager(CurrentSceneStage _currentScene)
    {
        if (_currentScene == CurrentSceneStage.InTheGame)
        {
            SceneManager.LoadScene(GameWorldScenes[0]);
        }
    }
}
