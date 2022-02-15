using System.Collections.Generic;
using UnityEngine;

public class TileSpawnerList : MonoBehaviour
{
    [SerializeField] private List<TileSpawner> _tileSpawners;
    [SerializeField] private Matrix field;
    [SerializeField] private TileTypesSO _tileTypes;

    public List<TileSpawner> TileSpawners => _tileSpawners;

    private void Awake()
    {
        _tileSpawners.ForEach(tSpawner => tSpawner.Field = field);
    }

    public void RequestTileSpawn(int columnNum, TileTypeSO tileType = null)
    {
        tileType = tileType ?? getRandomTileType();
        _tileSpawners[columnNum].RequestSpawnTile();
    }

    public TileTypeSO getRandomTileType()
    {
        return _tileTypes.GetRandom();
    }
}