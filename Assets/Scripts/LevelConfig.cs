using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Maze/LevelConfig")]
public class LevelConfig : ScriptableObject
{
     public string levelName; // Název úrovně (např. "Level 1" nebo "Temný labyrint")
    public int mazeWidth; // Šířka generovaného labyrintu
    public int mazeDepth; // Hloubka generovaného labyrintu
    public int numberOfDiamonds; // Počet diamantů, které musí hráč najít v této úrovni
}
    
