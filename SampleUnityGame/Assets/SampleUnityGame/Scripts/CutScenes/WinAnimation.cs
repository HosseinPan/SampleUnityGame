using UnityEngine;

namespace HosseinSampleGame
{
    public class WinAnimation : MonoBehaviour
    {
        //Events
        private VoidEventSO _playerWon;

        private AudioSource _audioWin;

        private void ReferenceScriptableObjects()
        {
            _playerWon = AllScriptableObjects.Events.PlayerWon;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
            _audioWin = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _playerWon.Subscribe(OnPlayerWon);
        }

        private void OnDisable()
        {
            _playerWon.Unsubscribe(OnPlayerWon);
        }

        private void OnPlayerWon()
        {
            _audioWin.Play();
        }

    }
}
