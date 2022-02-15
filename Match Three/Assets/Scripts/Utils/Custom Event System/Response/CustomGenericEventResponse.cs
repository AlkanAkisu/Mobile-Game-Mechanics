using UnityEngine;
using UnityEngine.Events;


    public class CustomGenericEventResponse<T> : MonoBehaviour
    {
        [SerializeField, NaughtyAttributes.Expandable] CustomEvent<T> Event;

        [SerializeField] UnityEvent<T> Response;

        private void OnEnable()
        {
            Event.RegisterListener(OnEventRaised);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(OnEventRaised);
        }

        public void OnEventRaised(T val)
        {
            Response.Invoke(val);
        }
    }
