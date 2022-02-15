using UnityEngine;

[CreateAssetMenu]
public class TileTypeSO : ScriptableObject
{
    [SerializeField] private Color _color;
    [SerializeField] private Sprite sprite;


    public void Paint(SpriteRenderer sp)
    {
        if (sprite != null)
            sp.sprite = sprite;
        sp.color = _color;
    }
}