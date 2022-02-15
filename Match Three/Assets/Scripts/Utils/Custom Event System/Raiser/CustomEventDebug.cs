using NaughtyAttributes;
using UnityEngine;

public class CustomGenericEventRaiser<T> : MonoBehaviour
{
    [SerializeField] CustomEvent<T> _event;
    
    [SerializeField] private bool isDebug;
    [SerializeField,ShowIf(nameof(isDebug))] T debugValue;

    [Button,ShowIf(nameof(isDebug))]
    private void Raise()
    {
        _event.Raise(debugValue);
    }

    public void Raise(T val)
    {
        _event.Raise(val);
    }
}
