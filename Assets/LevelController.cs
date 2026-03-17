using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1;
    private Enemy[] _enemies;
    private bool _levelComplete = false;

    private void OnEnable()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    void Update()
    {
        if (_levelComplete) return;

        foreach (Enemy enemy in _enemies)
        {
            if (enemy != null)
                return;
        }

        _levelComplete = true;
        Debug.Log("You Killed all enemies!");
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
    }
}