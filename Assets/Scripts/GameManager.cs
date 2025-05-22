using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] HighScoreHandler highScoreHandler;
    [SerializeField] string playerName;

    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreText;
    public GameObject TimeCounterGO;

    public GameObject MainMenuPanel;
    public GameObject GameplayPanel;
    public GameObject RecordPanel;
    public GameObject PausePanel;

    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver,
        RecordScreen,
        PauseScreen,
    }

    private bool isPaused = false;

    GameManagerState GMState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:
                GameOverGO.SetActive(false);
                RecordPanel.SetActive(false);
                GameplayPanel.SetActive(false);
                MainMenuPanel.SetActive(true);
                break;
            case GameManagerState.GamePlay:
                GameplayPanel.SetActive(true);
                MainMenuPanel.SetActive(false);
                Invoke("StartGame", 0.1f);
                break;
            case GameManagerState.GameOver:
                int currentScore = scoreText.GetComponent<GameScore>().Score;
                highScoreHandler.AddHighScoreIfPossible (new HighScoreElement(playerName, currentScore));
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

                GameOverGO.SetActive(true);

                Invoke("ChangeToOpeningState", 8f);

                playerShip.SetActive(false);

                break;
            case GameManagerState.RecordScreen:
                MainMenuPanel.SetActive(false);
                RecordPanel.SetActive(true);
                
                break;
            case GameManagerState.PauseScreen:
                TogglePause();
                break;
        }
    }

    public void StartGame()
    {
        playerShip.GetComponent<PlayerControl>().Init();
        enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
        TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
        scoreText.GetComponent<GameScore>().Score = 0;
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState ();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.GamePlay;
        UpdateGameManagerState ();
    }

    public void ShowRecordScreen()
    {
        GMState = GameManagerState.RecordScreen;
        UpdateGameManagerState ();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState (GameManagerState.Opening);
    }

    public void EndGame()
    {
        PausePanel.SetActive(false);
        GMState = GameManagerState.GameOver;
        UpdateGameManagerState();
        ResumeGame();
    }

        public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        PausePanel.SetActive(isPaused);

    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
