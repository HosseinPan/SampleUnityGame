using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "DiceList.asset",
            menuName = GlobalSettings.SOMenuItemPath + "SharedData/DiceList")]
    public class DiceListSO : ScriptableObject
    {
        public List<DiceController> Dices;
    }
}
