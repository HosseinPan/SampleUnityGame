using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "DiceSettings.asset",
                menuName = GlobalSettings.SOMenuItemPath + "GameDesign/Dice Settings")]
    public class DiceSettingsSO : ScriptableObject
    {
        public int MinNumber = 1;
        public int MaxNumber = 6;       
        public int RechargeInTurns = 1;
    }
}
