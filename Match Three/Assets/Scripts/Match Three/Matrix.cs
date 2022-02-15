using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Matrix : MonoBehaviour
{
    [SerializeField] private TileTypesSO types;
    [SerializeField] private float waitSeconds;
    [SerializeField] private TileSpawnerList tileSpawnerList;
    [SerializeField] private LayerMask TileLayer;
    [SerializeField] private PopUpEvent popUpEvent;


    private Vector3 _leftTop;
    private int M;
    private int N;

    public Tile[,] Field { get; private set; }

    private void Awake()
    {
        M = transform.GetChild(0).childCount;
        N = transform.childCount;
        Field = new Tile[N, M];

        _leftTop = transform.GetChild(0).GetChild(0).position;

        for (var i = 0; i < N; i++)
        {
            var row = transform.GetChild(i);
            for (var j = 0; j < M; j++)
            {
                var elem = row.GetChild(j);
                Field[i, j] = elem.GetComponent<Tile>();
            }
        }

        LoopElements(
            (tile, i, j) =>
            {
                do
                {
                    AssignRandomType(tile);
                    var typedField = new TileTypeSO[M, N];
                    LoopElements((elem, i, j) =>
                        {
                            if (elem == null)
                                typedField[i, j] = null;
                            else
                                typedField[i, j] = elem.Type;

                            return false;
                        }
                    );

                    var matchNum = MatchUtils.CheckIfMatch3(tile.Type, (i, j), typedField);

                    if (matchNum < 3)
                        break;
                } while (true);


                return false;
            }
        );
    }


    public void AssignRandomType(Tile tile)
    {
        var type = GetRandomType();
        tile.GetComponent<TileType>().Assign(type);
    }

    public TileTypeSO GetRandomType()
    {
        return types.GetRandom();
    }


    public Tile GetNeighborTile(Tile currentTile, Vector3 dir)
    {
        var (x, y) = dir.V2().Deconstruct();
        var (i, j) = GetTileIndex(currentTile);
        var idx = (i - (int) y, j + (int) x);
        return Get(idx);
    }

    public void SwitchTiles(Tile tile1, Tile tile2)
    {
        var idx1 = tile1.TileIndex;
        var idx2 = tile2.TileIndex;

        Set(idx2, tile1);
        Set(idx1, tile2);

        tile1.CheckForMatch();
        tile2.CheckForMatch();
    }

    public void CheckForMatch(Tile tile)
    {
        CheckForMatch(tile.Type, tile.TileIndex);
    }

    public void CheckForMatch(TileTypeSO type, (int, int) idx)
    {
        var typedField = new TileTypeSO[M, N];
        LoopElements((elem, i, j) =>
            {
                if (elem == null)
                    typedField[i, j] = null;
                else
                    typedField[i, j] = elem.Type;

                return false;
            }
        );

        var matchNum = MatchUtils.CheckIfMatch3(type, idx, typedField);
        // Debug.Log($"{type.name} has {matchNum} matches");
        if (matchNum < 3)
            return;
        AudioPlayer.i.GetSound("coin")
            .WithPitch(Random.Range(0.95f, 1.05f))
            .Play();
        var matches = MatchUtils.GetMatches();
        popUpEvent.Raise((Get(idx).transform.position, matchNum));

        matches.ForEach(idx =>
            {
                tileSpawnerList.RequestTileSpawn(idx.Item2);
                Get(idx).DestroyTile();
                Set(idx, null);
            }
        );

        StartCoroutine(MatchHappened());
    }

    private IEnumerator MatchHappened()
    {
        yield return new WaitForSeconds(waitSeconds);
        RearrangeField();
    }

    [Button]
    private void RearrangeField()
    {
        // TODO find a better solution
        var blockSize = 0.5f;
        Field = new Tile[M, N];
        var idxChangedTiles = new List<Tile>();
        LoopElements(
            (_, i, j) =>
            {
                var pos = _leftTop + Vector3.right * j * blockSize + Vector3.down * i * blockSize;

                var hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, TileLayer);

                if (hit.collider == null)
                {
                    Set((i, j), null);
                    return false;
                }

                var tile = hit.transform.GetComponent<Tile>();
                var newIdx = (i, j);
                if (newIdx != tile.TileIndex)
                    idxChangedTiles.Add(tile);
                Set(newIdx, tile);
                return false;
            }
        );
        idxChangedTiles.ForEach(tile => tile.CheckForMatch());
    }


    // ------------------------------------      UTILS       ------------------------------------


    public (int, int) GetTileIndex(Tile tile)
    {
        var returnVal = (-1, -1);
        LoopElements((curTile, i, j) =>
        {
            if (curTile == tile)
            {
                returnVal = (i, j);
                return true;
            }

            return false;
        });

        return returnVal;
    }

    public Tile Get((int, int) idx)
    {
        var (i, j) = idx;
        if (i < 0 || i >= Field.GetLength(0))
            return null;
        if (j < 0 || j >= Field.GetLength(1))
            return null;
        return Field[i, j];
    }

    private void Set((int, int) idx, Tile tile)
    {
        var (i, j) = idx;
        // callback if idx changed
        if (tile != null)
            tile.TileIndex = idx;

        Field[i, j] = tile;
    }

    private void LoopElements(Func<Tile, int, int, bool> callback)
    {
        var isBreakLoop = false;
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < M; j++)
            {
                isBreakLoop = callback(Get((i, j)), i, j);
                if (isBreakLoop)
                    break;
            }

            if (isBreakLoop)
                break;
        }
    }
}