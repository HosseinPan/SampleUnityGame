using System;
using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "VoidEvent.asset",
            menuName = GlobalSettings.SOMenuItemPath + "Events/VoidEvent")]
    public class VoidEventSO : ScriptableObject
    {
        private Action OnEventRaised;

        public void Subscribe(Action listener)
        {
            OnEventRaised += listener;
        }

        public void Unsubscribe(Action listener)
        {
            OnEventRaised -= listener;
        }

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
            Debug.Log($"Event {this.name} Raised!");
        }
    }
}
