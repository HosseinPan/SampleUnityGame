using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "IntSharedVariable.asset",
        menuName = GlobalSettings.SOMenuItemPath + "SharedData/Int")]
    public class IntSharedVariableSO : ScriptableObject
    {
        public int Value;
    }
}
