using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOver;
    public GameObject scoreUIText;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }
    GameManagerState GMState;
    // Use this for initialization
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                GameOver.SetActive(false);

                playButton.SetActive(true);

                break;

            case GameManagerState.Gameplay:

                scoreUIText.GetComponent<global:: GameScore>().Score = 0;

                playButton.SetActive(false);

                playerShip.GetComponent<BasicMovement>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                break;
            case GameManagerState.GameOver:

                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

                GameOver.SetActive(true);

                Invoke("ChangeToOpeningState", 4f);

                break;
        }
    }
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
