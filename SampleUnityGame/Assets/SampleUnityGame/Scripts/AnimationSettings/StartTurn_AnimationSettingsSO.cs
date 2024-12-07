using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "StartTurn_AnimationSettings.asset",
        menuName = GlobalSettings.SOMenuItemPath + "Animation/StartTurn Settings")]
    public class StartTurn_AnimationSettingsSO : ScriptableObject
    {
        public float EndAnimationDelay = 1f;
    }
}
