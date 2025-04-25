using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class MazeGenerator : MonoBehaviour
{            
    // Prefab pro jednotlivé buňky bludiště
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

    // Matice pro uchování jednotlivých buněk bludiště
    private MazeCell[,] _mazeGrid;

    void Start()
    {
        // Načítání aktuální úrovně z GameManageru
        var level = GameManager.Instance.currentLevel;
        _mazeWidth = level.mazeWidth;
        _mazeDepth = level.mazeDepth;
        _numberOfDiamonds = level.numberOfDiamonds;

        // Vytvoření mřížky bludiště
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        // Instancování jednotlivých buněk bludiště na základě rozměrů
        for(int x = 0; x < _mazeWidth; x++)
        {
            for(int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x,z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        // Generování bludiště začíná od první buňky (0,0)
        GenerateMaze(null, _mazeGrid[0,0]);

        // Vytvoření navmesh pro navigaci AI (pokud je potřeba)
        GetComponent<NavMeshSurface>().BuildNavMesh();
        
        // Umístění diamantů do bludiště
        PlaceDiamonds();
        // Inicializace počtu diamantů ve hře
        FindFirstObjectByType<LevelManager>()?.InitializeDiamondsCount();

    }

    // Rekurzivní metoda pro generování bludiště (backtracking)
    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit(); // Označí aktuální buňku jako navštívenou
        ClearWalls(previousCell, currentCell); // Odstraní stěny mezi buňkami
        
        MazeCell nextCell;

         // Pokračování v generování sousedních buněk
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell); // Vybereme náhodnou sousední buňku

            if(nextCell != null)
            {
                GenerateMaze(currentCell, nextCell); // Rekurzivně generujeme další buňky
            }
        } while(nextCell != null); // Opakujeme, dokud existují sousední neprozkoumané buňky

       if(nextCell != null)
       {
           GenerateMaze(currentCell, nextCell);
       }
    }

    // Vybere náhodnou neprozkoumanou sousední buňku
    private MazeCell GetNextUnvisitedCell(MazeCell currentCell){
        var unvisitedCells = GetUnvisitedCells(currentCell); // Získáme seznam neprozkoumaných sousedů
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault(); // Náhodně vybereme jednu buňku
    }

    // Vrátí všechny sousední buňky, které ještě nebyly navštíveny
    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell){
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        // Kontrola pravé, levé, přední a zadní buňky
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

    // Odstraní stěny mezi dvěma sousedními buňkami
    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if(previousCell == null)
        {
            return;
        }
    
        // Rozpoznání směru mezi sousedními buňkami a odstranění příslušné stěny
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

    // Rozmístí diamanty na náhodná místa v navštívených buňkách
    private void PlaceDiamonds()
    {
        List<MazeCell> visitedCells = new List<MazeCell>();

        // Seznam všech navštívených buněk
        foreach(var cell in _mazeGrid)
        {
            if(cell.IsVisited)
            {
                visitedCells.Add(cell);
            }
        }

        // Odstraníme buňku (0, 0) z možnosti spawnování diamantů
        visitedCells = visitedCells.Where(cell => cell.transform.position != new Vector3(0, 0, 0)).ToList();

        // Zamíchání seznamu navštívených buněk
        for(int i = 0; i < visitedCells.Count; i++)
        {
            int rand = Random.Range(i, visitedCells.Count);
            var temp = visitedCells[i];
            visitedCells[i] = visitedCells[rand];
            visitedCells[rand] = temp;
        }

        // Spawn diamantů v prvních N navštívených buňkách
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
