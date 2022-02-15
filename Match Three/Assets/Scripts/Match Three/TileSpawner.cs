using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private float minWaitTimeBetweenSpawns;
    [SerializeField] private GameObject tilePf;
    [SerializeField] private TileSpawnerList _tileSpawnerList;
    [SerializeField] private int consecutiveDifferentType;
    [SerializeField] private float consecutiveSameTypeProbabilty;
    private TileTypeSO[] _spawnedTileTypes;
    private int colummnNo;
    private float lastTimeSpawned;
    private int remainingTileToSpawn;
    public Matrix Field { get; set; }


    private void Awake()
    {
        remainingTileToSpawn = 0;
        lastTimeSpawned = Time.time;
        _spawnedTileTypes = new TileTypeSO[consecutiveDifferentType];
        colummnNo = _tileSpawnerList.TileSpawners.IndexOf(this);
    }

    private void Start()
    {
        for (var i = 0; i < _spawnedTileTypes.Length; i++) _spawnedTileTypes[i] = Field.Get((i, colummnNo)).Type;
    }

    private void Update()
    {
        if (remainingTileToSpawn == 0)
            return;

        if (Time.time - lastTimeSpawned > minWaitTimeBetweenSpawns)
        {
            SpawnTile();
            remainingTileToSpawn--;
            lastTimeSpawned = Time.time;
        }
    }

    public void RequestSpawnTile()
    {
        remainingTileToSpawn++;
    }

    private void SpawnTile()
    {
        var tile =
            Instantiate(tilePf, transform.position, quaternion.identity).GetComponent<Tile>();
        tile.AssignField(Field);
        TileTypeSO type;

        bool IsNotCameConsecutive(TileTypeSO type)
        {
            return !_spawnedTileTypes.Contains(type);
        }

        var sameConsecutiveProbHappened = Random.value < consecutiveSameTypeProbabilty;

        bool ShouldNotRandomiseAgain(TileTypeSO tileTypeSo)
        {
            return IsNotCameConsecutive(tileTypeSo) || sameConsecutiveProbHappened;
        }

        do
        {
            type = _tileSpawnerList.getRandomTileType();
        } while (!ShouldNotRandomiseAgain(type));


        tile.GetComponent<TileType>().Assign(type);

        for (var i = _spawnedTileTypes.Length - 1; i < 1; i++) _spawnedTileTypes[i] = _spawnedTileTypes[i - 1];

        _spawnedTileTypes[0] = type;
    }
}