using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelConfig currentLevel;
    public List<LevelConfig> allLevels;
    private bool[] unlockedLevels;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize unlocked levels array
            unlockedLevels = new bool[allLevels.Count];
            unlockedLevels[0] = true; // Unlock first level
        }
        else
        {
            Destroy(gameObject);
        }
    
    }
    public void UnlockNextLevel()
    {
        // Find the index of the current level
        int currentIndex = allLevels.IndexOf(currentLevel);
        int nextIndex = currentIndex + 1;

        // If there's a next level, unlock it
        if (nextIndex < allLevels.Count)
        {
            unlockedLevels[nextIndex] = true;
            Debug.Log($"Unlocked level: {allLevels[nextIndex].levelName}");
        }
    }

    public bool IsLevelUnlocked(LevelConfig level)
    {
        int levelIndex = allLevels.IndexOf(level);
        return levelIndex >= 0 && levelIndex < unlockedLevels.Length && unlockedLevels[levelIndex];
    }
}
