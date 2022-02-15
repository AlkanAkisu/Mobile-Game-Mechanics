using UnityEngine;

[CreateAssetMenu]
public class TileTypesSO : ScriptableObject
{
    [SerializeField] private TileTypeSO[] _tileTypes;

    public TileTypeSO[] TileTypes => _tileTypes;

    public TileTypeSO GetRandom()
    {
        return _tileTypes.RandomElement();
    }
}