using UnityEngine;
using HosseinSampleGame.ScriptableObjectCategories;

namespace HosseinSampleGame
{
    [DefaultExecutionOrder(-1)]
    public class AllScriptableObjects : MonoBehaviour
    {
        [SerializeField] private GameDesignSettings gameDesignSettings;
        [Space]
        [SerializeField] private AnimationSettings animationSettings;
        [Space]
        [SerializeField] private Events events;
        [Space]
        [SerializeField] private SharedData sharedData;
        [Space]
        [SerializeField] private Managers managers;
        
        private static AllScriptableObjects _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogError("More than one Instance of AllScriptableObjects");
            }
            else
            {
                _instance = this;
            }
        }

        public static Events Events
        {
            get => _instance.events;
        }

        public static Managers Managers
        {
            get => _instance.managers;
        }

        public static SharedData SharedData
        {
            get => _instance.sharedData;
        }

        public static GameDesignSettings GameDesignSettings
        {
            get => _instance.gameDesignSettings;
        }

        public static AnimationSettings AnimationSettings
        {
            get => _instance.animationSettings;
        }
    }
}
