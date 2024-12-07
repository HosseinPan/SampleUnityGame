using System.Collections.Generic;
using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "PlayerList.asset",
            menuName = GlobalSettings.SOMenuItemPath + "SharedData/PlayerList")]
    public class PlayerListSO : ScriptableObject
    {
        public List<PlayerController> Players;
        public PlayerController GetPlayerByOrder(int order)
        {
            return Players.Find(p => p.PlayerOrder == order);
        }
    }
}
