using UnityEngine;

[CreateAssetMenu(menuName = "Events/PopUpEvent")]
public class PopUpEvent : CustomEvent<(Vector3, int)>
{
}