using System.Collections.Generic;
using UnityEngine;

namespace HosseinSampleGame
{
    public class LevelInitializer : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject boardPrefab;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject dicePrefab;

        [Space]
        [Header("Transforms")]
        [SerializeField] private Transform dicePanel;

        //Events
        private VoidEventSO _levelInitialized;
        private VoidEventSO _initializeLevelRequested;

        //Shared Data
        private LevelSettingsSO _levelSettings;
        private DiceListSO _allDices;
        private PlayerListSO _allPlayers;

        //Managers
        private BoardManagerSO _boardManager;

        private void ReferenceScriptableObjects()
        {
            _levelInitialized = AllScriptableObjects.Events.LevelInitialized;
            _initializeLevelRequested = AllScriptableObjects.Events.InitializeLevelRequested;
            _levelSettings = AllScriptableObjects.GameDesignSettings.LevelSettings;
            _allDices = AllScriptableObjects.SharedData.AllDices;
            _allPlayers = AllScriptableObjects.SharedData.AllPlayers;
            _boardManager = AllScriptableObjects.Managers.BoardManager;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
        }

        private void OnEnable()
        {
            _initializeLevelRequested.Subscribe(OnInitializeLevelRequested);
        }

        private void OnDisable()
        {
            _initializeLevelRequested.Unsubscribe(OnInitializeLevelRequested);
        }

        private void OnInitializeLevelRequested()
        {
            InitializeBoard();
            InitializePlayers();
            InitializeDices();         

            _levelInitialized.RaiseEvent();
        }

        private void InitializeBoard()
        {
            var board = GameObject.Instantiate(boardPrefab);
            _boardManager.SetBoardGameObject(board);
        }

        private void InitializeDices()
        {
            _allDices.Dices.Clear();
            foreach (var diceSetting in _levelSettings.Dices)
            {
                var dice = GameObject.Instantiate(dicePrefab, dicePanel).GetComponent<DiceController>();
                dice.SetSettings(diceSetting);
                _allDices.Dices.Add(dice);
            }
        }

        private void InitializePlayers()
        {
            _allPlayers.Players.Clear();
            foreach (var playerSetting in _levelSettings.Players)
            {
                var player = GameObject.Instantiate(playerPrefab).GetComponent<PlayerController>();
                player.SetSettings(playerSetting);
                _allPlayers.Players.Add(player);
            }
            RandomizePlayersOrder();
        }

        private void RandomizePlayersOrder()
        {
            var orders = new List<int>();
            for (int i = 1; i <= _allPlayers.Players.Count; i++)
            {
                orders.Add(i);
            }

            foreach (var player in _allPlayers.Players)
            {
                int rndIndex = UnityEngine.Random.Range(0, orders.Count);
                player.SetPlayerOrder(orders[rndIndex]);
                orders.RemoveAt(rndIndex);
            }
        }
    }
}
