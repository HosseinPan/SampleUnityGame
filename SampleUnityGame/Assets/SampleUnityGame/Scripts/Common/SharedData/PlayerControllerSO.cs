using UnityEngine;


namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "PlayerController.asset",
        menuName = GlobalSettings.SOMenuItemPath + "SharedData/PlayerController")]
    public class PlayerControllerSO : ScriptableObject
    {
        public PlayerController Player;
    }
}
