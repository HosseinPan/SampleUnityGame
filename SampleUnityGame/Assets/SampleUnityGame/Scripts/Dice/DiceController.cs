using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace HosseinSampleGame
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject rollPanel;
        [SerializeField] private GameObject valuePanel;
        [SerializeField] private GameObject lockPanel;
        [SerializeField] private TextMeshProUGUI rollRangeText;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private TextMeshProUGUI lockText;
        [SerializeField] private Image valueBackground;
        [SerializeField] private Image lockBackground;
        [SerializeField] private Image rollBackground;
        [SerializeField] private Button rollButton;

        //Events
        private VoidEventSO _diceRolled;
        private VoidEventSO _playerTurnStart_Requested;
        private VoidEventSO _playerTurnStart_AnimationEnded;

        //Shared Data
        private PlayerListSO _allPlayers;
        private PlayerControllerSO _currentPlayer;
        private IntSharedVariableSO _rolledDiceValue;
        private DiceControllerSO _selectedDice;

        private DiceSettingsSO _diceSetting;
        private Dictionary<PlayerController, int> PlayerRechargeDiceInTurn = new Dictionary<PlayerController, int>();
        private AudioSource _audioDiceRoll;

        private void ReferenceScriptableObjects()
        {
            _diceRolled = AllScriptableObjects.Events.DiceRolled;
            _playerTurnStart_Requested = AllScriptableObjects.Events.PlayerTurnStart_Requested;
            _playerTurnStart_AnimationEnded = AllScriptableObjects.Events.PlayerTurnStart_AnimationEnded;
            _allPlayers = AllScriptableObjects.SharedData.AllPlayers;
            _currentPlayer = AllScriptableObjects.SharedData.CurrentPlayer;
            _rolledDiceValue = AllScriptableObjects.SharedData.RolledDiceValue;
            _selectedDice = AllScriptableObjects.SharedData.SelectedDice;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
            _audioDiceRoll = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _playerTurnStart_Requested.Subscribe(OnPlayerTurnStart_Requested);
            _diceRolled.Subscribe(OnDiceRolled);
            _playerTurnStart_AnimationEnded.Subscribe(OnPlayerTurnStart_AnimationEnded);
        }

        private void OnDisable()
        {
            _playerTurnStart_Requested.Unsubscribe(OnPlayerTurnStart_Requested);
            _diceRolled.Unsubscribe(OnDiceRolled);
            _playerTurnStart_AnimationEnded.Unsubscribe(OnPlayerTurnStart_AnimationEnded);
        }

        public int MaxNumber 
        { 
            get => _diceSetting.MaxNumber;
        }

        public void SetSettings(DiceSettingsSO diceSetting)
        {
            _diceSetting = diceSetting;
            Initialize();
        }

        public void RollDice()
        {
            _audioDiceRoll.Play();
            _rolledDiceValue.Value = UnityEngine.Random.Range(_diceSetting.MinNumber, _diceSetting.MaxNumber);
            _selectedDice.Dice = this;
            ResetRechargeTurns();
            _diceRolled.RaiseEvent();            
        }

        public bool IsPlayerDiceReadyToUse(PlayerController player)
        {
            int rechargeInturn = PlayerRechargeDiceInTurn[player];
            return rechargeInturn <= 0;
        }

        private void OnPlayerTurnStart_Requested()
        {
            RechargeOneTurn();
            UpdateState_OnStartTurn();
        }

        private void OnDiceRolled()
        {
            UpdateState_OnDiceRolled();
        }

        private void OnPlayerTurnStart_AnimationEnded()
        {
            rollButton.interactable = _currentPlayer.Player.IsHuman();
        }

        private void Initialize()
        {
            foreach (var player in _allPlayers.Players)
            {
                PlayerRechargeDiceInTurn.Add(player, 0);
            }
            rollRangeText.text = $"{_diceSetting.MinNumber} - {_diceSetting.MaxNumber}";
            lockPanel.SetActive(false);
            valuePanel.SetActive(false);
            rollPanel.SetActive(true);
        }

        private void UpdateState_OnStartTurn()
        {
            mainPanel.SetActive(true);
            rollButton.interactable = false;

            if (IsPlayerDiceReadyToUse(_currentPlayer.Player))
            {
                rollBackground.color = _currentPlayer.Player.GetPlayerColor();
                rollPanel.SetActive(true);
                lockPanel.SetActive(false);
                valuePanel.SetActive(false);
            }
            else
            {
                int remainTurnToUnlock = PlayerRechargeDiceInTurn[_currentPlayer.Player];
                lockText.text = $"{remainTurnToUnlock} turn to unlock";
                lockBackground.color = _currentPlayer.Player.GetPlayerColor();

                lockPanel.SetActive(true);
                rollPanel.SetActive(false);
                valuePanel.SetActive(false);
            }
        }

        private void UpdateState_OnDiceRolled()
        {
            if (_selectedDice.Dice == this)
            {
                valueBackground.color = _currentPlayer.Player.GetPlayerColor();
                valueText.text = _rolledDiceValue.Value.ToString();
                valuePanel.SetActive(true);
                rollPanel.SetActive(false);
                lockPanel.SetActive(false);               
            }
            else
            {
                mainPanel.SetActive(false);
            }
        }

        private void ResetRechargeTurns()
        {
            PlayerRechargeDiceInTurn[_currentPlayer.Player] = _diceSetting.RechargeInTurns;
        }

        private void RechargeOneTurn()
        {
            int currentRemainTurn = PlayerRechargeDiceInTurn[_currentPlayer.Player];
            currentRemainTurn--;
            if (currentRemainTurn < 0)
                currentRemainTurn = 0;

            PlayerRechargeDiceInTurn[_currentPlayer.Player] = currentRemainTurn;
        }

    }
}
