using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Maze/LevelConfig")]
public class LevelConfig : ScriptableObject
{
     public string levelName;
    public int mazeWidth;
    public int mazeDepth;
    public int numberOfDiamonds;
    //public bool hasEnemy;
}
