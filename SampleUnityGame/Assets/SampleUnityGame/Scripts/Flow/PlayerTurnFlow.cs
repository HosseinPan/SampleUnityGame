using UnityEngine;

namespace HosseinSampleGame
{
    public class PlayerTurnFlow : MonoBehaviour
    {
        //Events
        private VoidEventSO _startPlayerTurnRequested;
        private VoidEventSO _playerTurnEnded;
        private VoidEventSO _playerTurnStart_Requested;
        private VoidEventSO _playerTurnStart_AnimationEnded;
        private VoidEventSO _playerRollDiceRequested;
        private VoidEventSO _diceRolled;
        private VoidEventSO _playerWalk_Requested;
        private VoidEventSO _playerwalkEnded;

        //Shared Data
        private PlayerListSO _allPlayers;
        private PlayerControllerSO _currentPlayer;
        private DiceListSO _allDices;

        private int currentPlayerOrder = 0;

        private void ReferenceScriptableObjects()
        {
            _startPlayerTurnRequested = AllScriptableObjects.Events.StartPlayerTurnRequested;
            _playerTurnEnded = AllScriptableObjects.Events.PlayerTurnEnded;
            _playerTurnStart_Requested = AllScriptableObjects.Events.PlayerTurnStart_Requested;
            _playerTurnStart_AnimationEnded = AllScriptableObjects.Events.PlayerTurnStart_AnimationEnded;
            _playerRollDiceRequested = AllScriptableObjects.Events.PlayerRollDiceRequested;
            _diceRolled = AllScriptableObjects.Events.DiceRolled;
            _playerWalk_Requested = AllScriptableObjects.Events.PlayerWalk_Requested;
            _playerwalkEnded = AllScriptableObjects.Events.PlayerwalkEnded;
            _allPlayers = AllScriptableObjects.SharedData.AllPlayers;
            _currentPlayer = AllScriptableObjects.SharedData.CurrentPlayer;
            _allDices = AllScriptableObjects.SharedData.AllDices;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
        }

        private void OnEnable()
        {
            _startPlayerTurnRequested.Subscribe(OnStartPlayerTurnRequested);
            _playerTurnStart_AnimationEnded.Subscribe(OnPlayerTurnStart_AnimationEnded);
            _diceRolled.Subscribe(OnDiceRolled);
            _playerwalkEnded.Subscribe(OnPlayerwalkEnded);
        }

        private void OnDisable()
        {
            _startPlayerTurnRequested.Unsubscribe(OnStartPlayerTurnRequested);
            _playerTurnStart_AnimationEnded.Unsubscribe(OnPlayerTurnStart_AnimationEnded);
            _diceRolled.Unsubscribe(OnDiceRolled);
            _playerwalkEnded.Unsubscribe(OnPlayerwalkEnded);
        }

        private void OnStartPlayerTurnRequested()
        {
            FindNextPlayer();
            _playerTurnStart_Requested.RaiseEvent();
        }

        private void FindNextPlayer()
        {
            currentPlayerOrder++;
            if (currentPlayerOrder > _allPlayers.Players.Count)
            {
                currentPlayerOrder = 1;
            }
            _currentPlayer.Player = _allPlayers.GetPlayerByOrder(currentPlayerOrder);
        }

        private void OnPlayerTurnStart_AnimationEnded()
        {
            if (CheckHaveAnyAvailableDice())
            {
                _playerRollDiceRequested.RaiseEvent();
            }
            else
            {
                _playerwalkEnded.RaiseEvent();
            }
        }

        private void OnDiceRolled()
        {
            _playerWalk_Requested.RaiseEvent();
        }

        private void OnPlayerwalkEnded()
        {
            _playerTurnEnded.RaiseEvent();
        }

        private bool CheckHaveAnyAvailableDice()
        {
            bool result = false;
            foreach (var dice in _allDices.Dices)
            {
                if (dice.IsPlayerDiceReadyToUse(_currentPlayer.Player))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
