using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "PlayerSettings.asset",
                    menuName = GlobalSettings.SOMenuItemPath + "GameDesign/Player Settings")]
    public class PlayerSettingsSO : ScriptableObject
    {
        public string PlayerName;
        public PlayerType PlayerType;
        public Color Color;
    }
}
