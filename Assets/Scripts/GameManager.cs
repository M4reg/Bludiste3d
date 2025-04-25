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
            DontDestroyOnLoad(gameObject); // Zachová objekt mezi scénami

            // Inicializace pole pro odemčené úrovně
            unlockedLevels = new bool[allLevels.Count];
            unlockedLevels[0] = true; // První úroveň je automaticky odemčená
        }
        else
        {
            Destroy(gameObject); // Pokud už instance existuje, znič tuto novou
        }
    
    }
    // Odemkne další úroveň v pořadí po aktuální
    public void UnlockNextLevel()
    {
        // Find the index of the current level
        int currentIndex = allLevels.IndexOf(currentLevel); // Najde index aktuální úrovně
        int nextIndex = currentIndex + 1;

        // Pokud existuje další úroveň
        if (nextIndex < allLevels.Count) 
        {
            unlockedLevels[nextIndex] = true; // Odemkne ji
            Debug.Log($"Unlocked level: {allLevels[nextIndex].levelName}");
        }
    }

    // Vrací, zda je daná úroveň odemčená
    public bool IsLevelUnlocked(LevelConfig level)
    {
        int levelIndex = allLevels.IndexOf(level);
        return levelIndex >= 0 && levelIndex < unlockedLevels.Length && unlockedLevels[levelIndex];
    }
}
