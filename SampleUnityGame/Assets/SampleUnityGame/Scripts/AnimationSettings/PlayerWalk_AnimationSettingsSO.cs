using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "PlayerWalk_AnimationSettings.asset",
        menuName = GlobalSettings.SOMenuItemPath + "Animation/PlayerWalk Settings")]
    public class PlayerWalk_AnimationSettingsSO : ScriptableObject
    {
        public float JumpMaxHeight = 0.5f;
        public float JumpDuration = 0.3f;
        public float DelayBetweenJumps = 0.1f;
        public float DelayStartAnimation = 0.2f;
        public float DelayAfterLastJump = 0.8f;
    }
}
