using System.Collections.Generic;
using UnityEngine;

namespace HosseinSampleGame
{
    public class PlayerController : MonoBehaviour
    {
        //Events
        private VoidEventSO _playerRollDiceRequested;
        private VoidEventSO AI_RollDiceRequested;
        private VoidEventSO _playerWalk_Requested;

        //Managers
        private BoardManagerSO _boardManager;

        //Shared Data
        private PlayerControllerSO _currentPlayer;
        private IntSharedVariableSO _rolledDiceValue;

        public int PlayerOrder { get; private set; }
        public BoardTile currentTile { get; set; }

        private PlayerSettingsSO _playerSetting;
        private List<Vector3> jumpPoints = new List<Vector3>();
        private PlayerWalkAnimation walkAnimation;

        private void ReferenceScriptableObjects()
        {
            _playerRollDiceRequested = AllScriptableObjects.Events.PlayerRollDiceRequested;
            AI_RollDiceRequested = AllScriptableObjects.Events.AI_RollDiceRequested;
            _playerWalk_Requested = AllScriptableObjects.Events.PlayerWalk_Requested;
            _boardManager = AllScriptableObjects.Managers.BoardManager;
            _currentPlayer = AllScriptableObjects.SharedData.CurrentPlayer;
            _rolledDiceValue = AllScriptableObjects.SharedData.RolledDiceValue;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
            walkAnimation = GetComponent<PlayerWalkAnimation>();
        }

        private void OnEnable()
        {
            _playerRollDiceRequested.Subscribe(OnPlayerRollDiceRequested);
            _playerWalk_Requested.Subscribe(OnPlayerWalk_Requested);
        }

        private void OnDisable()
        {
            _playerRollDiceRequested.Unsubscribe(OnPlayerRollDiceRequested);
            _playerWalk_Requested.Unsubscribe(OnPlayerWalk_Requested);
        }

        public void SetSettings(PlayerSettingsSO playerSetting)
        {
            _playerSetting = playerSetting;
            gameObject.name = $"Player_{playerSetting.PlayerName}";
            SetMeshColor();
            currentTile = _boardManager.GetFirstTile();
            transform.position = currentTile.GetJumpPointInitialize();
        }

        public bool IsOnTargetTile()
        {
            return currentTile.TileType == TileType.Target;
        }

        public bool IsHuman()
        {
            return _playerSetting.PlayerType == PlayerType.Human;
        }

        public Color GetPlayerColor()
        {
            return _playerSetting.Color;
        }

        public void SetPlayerOrder(int order)
        {
            PlayerOrder = order;
        }

        private void OnPlayerRollDiceRequested()
        {
            if (this != _currentPlayer.Player)
            {
                return;
            }

            if (_playerSetting.PlayerType == PlayerType.AI)
            {
                AI_RollDiceRequested.RaiseEvent();
            }
        }

        private void OnPlayerWalk_Requested()
        {
            if (this != _currentPlayer.Player)
            {
                return;
            }

            MovePlayer();
        }

        private void SetMeshColor()
        {
            var meshRenderer = GetComponentInChildren<MeshRenderer>();
            foreach (var material in meshRenderer.materials)
            {
                material.color = _playerSetting.Color;
            }
        }

        private void MovePlayer()
        {
            int totalJumps = _rolledDiceValue.Value;
            jumpPoints.Clear();

            for (int i = 0; i < totalJumps; i++)
            {
                currentTile = currentTile.NextTile;
                jumpPoints.Add(currentTile.GetJumpPoint());
            }

            walkAnimation.PlayWalkAnimation(jumpPoints);
        }

    }
}
