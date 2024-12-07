using System.Collections;
using UnityEngine;

namespace HosseinSampleGame
{
    public class StartPlayerTurn_Animation : MonoBehaviour
    {
        //Events
        private VoidEventSO _playerTurnStart_Requested;
        private VoidEventSO _playerTurnStart_AnimationEnded;

        private StartTurn_AnimationSettingsSO _animationSettings;

        private void ReferenceScriptableObjects()
        {
            _playerTurnStart_Requested = AllScriptableObjects.Events.PlayerTurnStart_Requested;
            _playerTurnStart_AnimationEnded = AllScriptableObjects.Events.PlayerTurnStart_AnimationEnded;
            _animationSettings = AllScriptableObjects.AnimationSettings.StartTurn;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
        }

        private void OnEnable()
        {
            _playerTurnStart_Requested.Subscribe(OnPlayerTurnStart_Requested);
        }

        private void OnDisable()
        {
            _playerTurnStart_Requested.Unsubscribe(OnPlayerTurnStart_Requested);
        }

        private void OnPlayerTurnStart_Requested()
        {
            StartCoroutine(AnimationEndWithDelay());
        }

        IEnumerator AnimationEndWithDelay()
        {
            yield return new WaitForSeconds(_animationSettings.EndAnimationDelay);
            _playerTurnStart_AnimationEnded.RaiseEvent();
        }

    }
}
