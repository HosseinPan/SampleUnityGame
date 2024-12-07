using UnityEngine;

namespace HosseinSampleGame
{
    public class GameFlow : MonoBehaviour
    {
        //Events
        private VoidEventSO _levelInitialized;
        private VoidEventSO _initializeLevelRequested;
        private VoidEventSO _startPlayerTurnRequested;
        private VoidEventSO _playerTurnEnded;
        private VoidEventSO _playerWon;

        //Shared Data
        private PlayerControllerSO _currentPlayer;

        private void ReferenceScriptableObjects()
        {
            _levelInitialized = AllScriptableObjects.Events.LevelInitialized;
            _initializeLevelRequested = AllScriptableObjects.Events.InitializeLevelRequested;
            _startPlayerTurnRequested = AllScriptableObjects.Events.StartPlayerTurnRequested;
            _playerTurnEnded = AllScriptableObjects.Events.PlayerTurnEnded;
            _playerWon = AllScriptableObjects.Events.PlayerWon;
            _currentPlayer = AllScriptableObjects.SharedData.CurrentPlayer;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
        }

        private void Start()
        {
            _initializeLevelRequested.RaiseEvent();
        }

        private void OnEnable()
        {
            _levelInitialized.Subscribe(OnLevelInitialized);
            _playerTurnEnded.Subscribe(OnPlayerTurnEnded);
        }

        private void OnDisable()
        {
            _levelInitialized.Unsubscribe(OnLevelInitialized);
            _playerTurnEnded.Unsubscribe(OnPlayerTurnEnded);
        }

        private void OnLevelInitialized()
        {
            StartNextPlayerTurn();
        }

        private void OnPlayerTurnEnded()
        {
            if (CheckWin())
            {
                _playerWon.RaiseEvent();
            }
            else
            {
                StartNextPlayerTurn();
            }
        }

        private void StartNextPlayerTurn()
        {
            _startPlayerTurnRequested.RaiseEvent();
        }

        private bool CheckWin()
        {
            return _currentPlayer.Player.IsOnTargetTile();
        }
    }
}
