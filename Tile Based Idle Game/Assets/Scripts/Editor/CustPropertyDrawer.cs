using UnityEngine;
using UnityEditor;
using System.Collections;

#if UNITY_EDITOR


[CustomPropertyDrawer(typeof(BuildingLayout))]
public class CustPropertyDrawer : PropertyDrawer
{


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        Rect newposition = position;
        newposition.y += 18f;
        SerializedProperty data = property.FindPropertyRelative("rows");
        //data.rows[0][]
        for (int j = 0; j < 3; j++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
            newposition.height = 18f;
            if (row.arraySize != 3)
                row.arraySize = 3;
            newposition.width = 18;
            for (int i = 0; i < 3; i++)
            {
                EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(i), GUIContent.none);
                newposition.x += newposition.width;
            }

            newposition.x = position.x;
            newposition.y += 18f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 4;
    }
}
#endif