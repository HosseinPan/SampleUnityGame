using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "DiceController.asset",
        menuName = GlobalSettings.SOMenuItemPath + "SharedData/DiceController")]
    public class DiceControllerSO : ScriptableObject
    {
        public DiceController Dice;
    }
}
