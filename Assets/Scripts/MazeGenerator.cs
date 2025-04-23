using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{            
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    [SerializeField]
    private GameObject _diamondPrefab;

    [SerializeField]
    private int _numberOfDiamonds = 4;

    private MazeCell[,] _mazeGrid;

    void Start()
    {
        var level = GameManager.Instance.currentLevel;
        _mazeWidth = level.mazeWidth;
        _mazeDepth = level.mazeDepth;
        _numberOfDiamonds = level.numberOfDiamonds;

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for(int x = 0; x < _mazeWidth; x++)
        {
            for(int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x,z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        GenerateMaze(null, _mazeGrid[0,0]);

        /*if(level.hasEnemy){
            _enemyPrefab.SetActive(true);
        }else{
            _enemyPrefab.SetActive(false);
        }*/

        
        PlaceDiamonds();
        FindFirstObjectByType<LevelManager>()?.InitializeDiamondsCount();

    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);
        
        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if(nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while(nextCell != null);

       if(nextCell != null)
       {
           GenerateMaze(currentCell, nextCell);
       }
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell){
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell){
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if(x+1<_mazeWidth){
           var cellToRight = _mazeGrid[x+1,z];

           if(cellToRight.IsVisited == false){
               yield return cellToRight;
           }
        }

        if(x-1>=0){
           var cellToLeft = _mazeGrid[x-1,z];

           if(cellToLeft.IsVisited == false){
               yield return cellToLeft;
           }
        }

        if(z+1<_mazeDepth){
           var cellToFront = _mazeGrid[x,z+1];

           if(cellToFront.IsVisited == false){
               yield return cellToFront;
           }
        }

        if(z-1>=0){
           var cellToBack = _mazeGrid[x,z-1];

           if(cellToBack.IsVisited == false){
               yield return cellToBack;
           }
        }
    }

    
    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if(previousCell == null)
        {
            return;
        }

       if(previousCell.transform.position.x < currentCell.transform.position.x)
       {
           previousCell.ClearRightWall();
           currentCell.ClearLeftWall();
           return;
       }
       if(previousCell.transform.position.x > currentCell.transform.position.x)
       {
           previousCell.ClearLeftWall();
           currentCell.ClearRightWall();
           return;
       }
       if(previousCell.transform.position.z < currentCell.transform.position.z)
       {
           previousCell.ClearFrontWall();
           currentCell.ClearBackWall();
           return;
       }
         if(previousCell.transform.position.z > currentCell.transform.position.z)
         {
              previousCell.ClearBackWall();
              currentCell.ClearFrontWall();
              return;
         }
    }

    private void PlaceDiamonds()
    {
        List<MazeCell> visitedCells = new List<MazeCell>();

        foreach(var cell in _mazeGrid)
        {
            if(cell.IsVisited)
            {
                visitedCells.Add(cell);
            }
        }

        // Zamíchej seznam navštívených buněk
        for(int i = 0; i < visitedCells.Count; i++)
        {
            int rand = Random.Range(i, visitedCells.Count);
            var temp = visitedCells[i];
            visitedCells[i] = visitedCells[rand];
            visitedCells[rand] = temp;
        }

        // Spawn diamantů do prvních N buněk
        for (int i = 0; i < _numberOfDiamonds && i < visitedCells.Count; i++)
        {
            var spawnPoint = visitedCells[i].DiamondSpawnPoint;
            Instantiate(_diamondPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
        


    // Update is called once per frame
    void Update()
    {
        
    }
}
