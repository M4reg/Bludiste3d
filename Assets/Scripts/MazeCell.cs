using UnityEngine;
using System.Collections.Generic;
public class MazeCell : MonoBehaviour
{
    // Odkazy na jednotlivé stěny buňky v bludišti
    [SerializeField]
    private GameObject _leftWall;
    [SerializeField]
    private GameObject _rightWall;
    [SerializeField]
    private GameObject _frontWall;
    [SerializeField]
    private GameObject _backWall;
    [SerializeField]
    // Odkaz na objekt, který představuje neprozkoumanou část (blok) buňky
    private GameObject _unvisitedBlock;

    // Bod, kde se může objevit diamant v buňce
    public Transform DiamondSpawnPoint { get; private set; }

    // Určuje, zda byla buňka již navštívena (pro generování bludiště)
    public bool IsVisited { get; private set; }

     // Označení buňky jako navštívené a deaktivace neprozkoumaného bloku
    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false); // Skrytí neprozkoumaného bloku
    }

    // Funkce pro odstranění jednotlivých stěn buňky
    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }

    // Funkce probíhá při inicializaci buňky (po spuštění)
    private void Awake()
    {
        // Nastavení bodu pro spawn diamantů v buňce
        DiamondSpawnPoint = transform.Find("DiamondSpawnPoint");
        float RandomEpsilon() => Random.Range(-0.001f, 0.001f); // malý náhodný offset posun stěny kvůli Z fighting

        if (_leftWall != null)
            _leftWall.transform.localPosition += new Vector3(RandomEpsilon(), 0, RandomEpsilon());

        if (_rightWall != null)
            _rightWall.transform.localPosition += new Vector3(RandomEpsilon(), 0, RandomEpsilon());

        if (_frontWall != null)
            _frontWall.transform.localPosition += new Vector3(RandomEpsilon(), 0, RandomEpsilon());

        if (_backWall != null)
            _backWall.transform.localPosition += new Vector3(RandomEpsilon(), 0, RandomEpsilon());
    }
}
