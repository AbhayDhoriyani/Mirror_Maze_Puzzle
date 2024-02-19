using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] GameObject LevelCompletePanel;
    [SerializeField] GameObject[] LevelsPrefab;
    int currentLevelIndex = 0;
    public bool canMove { private set; get; } = false;
    GameObject currentSpawnedLevel;

    void Start()
    {
        Instance = this;
        currentLevelIndex = 0;
        SpawnLevel();
    }

    public void OnClickReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OnLevelComplate()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= LevelsPrefab.Length)
        {
            LevelCompletePanel.SetActive(true);
        }
        else
        {
            canMove = false;
            Invoke(nameof(SpawnLevel), 2);
        }
    }

    void SpawnLevel()
    {
        if (currentSpawnedLevel != null)
        {
            Destroy(currentSpawnedLevel);
        }
        currentSpawnedLevel = Instantiate(LevelsPrefab[currentLevelIndex], Vector3.zero, Quaternion.identity);
        canMove = true;
    }
}
