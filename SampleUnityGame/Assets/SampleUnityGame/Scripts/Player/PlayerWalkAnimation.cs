using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HosseinSampleGame
{
    public class PlayerWalkAnimation : MonoBehaviour
    {
        //Events
        private VoidEventSO _playerwalkEnded;

        private PlayerWalk_AnimationSettingsSO _animationSettings;
        private AudioSource _audioWalk;

        private void ReferenceScriptableObjects()
        {
            _playerwalkEnded = AllScriptableObjects.Events.PlayerwalkEnded;
            _animationSettings = AllScriptableObjects.AnimationSettings.PlayerWalk;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
            _audioWalk = GetComponent<AudioSource>();
        }

        public void PlayWalkAnimation(List<Vector3> JumpPositions)
        {
            Sequence moveSequence = DOTween.Sequence();

            moveSequence.AppendInterval(_animationSettings.DelayStartAnimation);
            foreach (var JumpPosition in JumpPositions)
            {
                moveSequence.Append(transform.DOJump(JumpPosition,
                                                    _animationSettings.JumpMaxHeight,
                                                    1,
                                                    _animationSettings.JumpDuration));
                moveSequence.AppendCallback(PlayJumpSound);
                moveSequence.AppendInterval(_animationSettings.DelayBetweenJumps);
            }
            moveSequence.AppendInterval(_animationSettings.DelayAfterLastJump);
            moveSequence.AppendCallback(OnAnimationEnded);
        }

        private void PlayJumpSound()
        {
            _audioWalk.Play();
        }

        private void OnAnimationEnded()
        {
            _playerwalkEnded.RaiseEvent();
        }
    }
}
