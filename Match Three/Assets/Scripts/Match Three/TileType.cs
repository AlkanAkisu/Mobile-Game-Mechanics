using System;
using UnityEngine;

public class TileType : MonoBehaviour
{
    [SerializeField] private TileTypeSO _typeSo;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public TileTypeSO Type => _typeSo;
    
    public void Assign(TileTypeSO type)
    {
        _typeSo = type;
        Paint();
    }

    private void OnValidate() => Paint();

    private void Paint()
    {
        if(_typeSo != null)
            _typeSo.Paint(spriteRenderer);
    }
}
