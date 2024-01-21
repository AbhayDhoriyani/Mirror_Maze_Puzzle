using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum GameState
    {
        Started,
        Ongoing,
        Ended
    }

    GameState currentGameState;
    [SerializeField] GameObject LevelCompletePanel;
    [SerializeField] GameObject LevelStartPanel;

    void Start()
    {
        currentGameState = GameState.Started;
        Instance = this;
        LevelStartPanel.SetActive(true);
    }
    
    public void OnClickLevelStart()
    {
        LevelStartPanel.SetActive(false);
        currentGameState = GameState.Ongoing;
    }

    public void OnClickReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeGameState(GameState state)
    {
        currentGameState = state;
        if(currentGameState == GameState.Ended)
        {
            LevelCompletePanel.SetActive(true);
        }
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }
}
