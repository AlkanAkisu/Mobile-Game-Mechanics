using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static void Log(params object[] objects)
    {
        int N = objects.Length;
        string str = "";
        for (int i = 0; i < N; i++)
        {
            str += objects[i].ToString();
            if (i != N - 1)
                str += " ";
        }
        Debug.Log(str);


    }

    public static void ChangeVector(ref Vector3 vect, float x = Mathf.Infinity, float y = Mathf.Infinity, float z = Mathf.Infinity)
    {
        vect = new Vector3(x == Mathf.Infinity ? vect.x : x, y == Mathf.Infinity ? vect.y : y, z == Mathf.Infinity ? vect.z : z);
    }
    public static IEnumerable<(int, TGeneralized)> Enumerate<TGeneralized>
        (this IEnumerable<TGeneralized> array) => array.Select((item, index) => (index, item));
    public static Vector2 V2(this Vector3 vect) => vect;

    public static (float, float, float) Deconstruct(this Vector3 vect)
    {
        return (vect.x, vect.y, vect.z);
    }

    public static Vector3 ChangeVector(this Vector3 vect, float x = Mathf.Infinity, float y = Mathf.Infinity, float z = Mathf.Infinity)
    {
        return new Vector3(x == Mathf.Infinity ? vect.x : x, y == Mathf.Infinity ? vect.y : y, z == Mathf.Infinity ? vect.z : z);
    }
    public static Vector2 ChangeVector(this Vector2 vect, float x = Mathf.Infinity, float y = Mathf.Infinity)
    {
        return new Vector2(x == Mathf.Infinity ? vect.x : x, y == Mathf.Infinity ? vect.y : y);
    }
    public static bool Between(this float num, float limit) => num == Mathf.Clamp(num, -Mathf.Abs(limit), Mathf.Abs(limit));
    public static bool BetweenTwo(this float num, float upper, float lower) => num == Mathf.Clamp(num, Mathf.Min(lower, upper), Mathf.Max(lower, upper));

    public static float Abs(this float num) => Mathf.Abs(num);
    public static int Sign(this float num) => (int)Mathf.Sign(num);
    public static Color GetColor(string name)
    {
        var getProp = typeof(Color).GetProperty(name);
        if (getProp == null) return Color.green;

        return (Color)getProp?.GetValue(new Color(), null);
    }
    public static bool Up(this KeyCode key) => Input.GetKeyUp(key);
    public static bool Down(this KeyCode key) => Input.GetKeyDown(key);
    public static bool Hold(this KeyCode key) => Input.GetKey(key);
}
