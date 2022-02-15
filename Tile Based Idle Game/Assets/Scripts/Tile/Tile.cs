using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }
    public void Select()
    {
        ColorUtility.TryParseHtmlString("#D9AFAF", out var color);
        sp.color = color;
    }
    public void Deselect()
    {
        sp.color = Color.white;

    }
}
