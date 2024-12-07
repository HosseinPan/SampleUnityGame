using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "LevelSettings.asset",
            menuName = GlobalSettings.SOMenuItemPath + "GameDesign/Level Settings")]
    public class LevelSettingsSO: ScriptableObject
    {
        public PlayerSettingsSO[] Players;
        public DiceSettingsSO[] Dices;
    }
}
