using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HosseinSampleGame
{
    public class InfoUI : MonoBehaviour
    {
        [SerializeField] private Image playerAvatar;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private GameObject exitButton;

        //Events
        private VoidEventSO _playerTurnStart_Requested;
        private VoidEventSO _playerWon;

        //Shared Data
        private PlayerControllerSO _currentPlayer;      

        private void ReferenceScriptableObjects()
        {
            _playerTurnStart_Requested = AllScriptableObjects.Events.PlayerTurnStart_Requested;
            _playerWon = AllScriptableObjects.Events.PlayerWon;
            _currentPlayer = AllScriptableObjects.SharedData.CurrentPlayer;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
            infoText.text = "Turn";
            exitButton.SetActive(false);
        }

        private void OnEnable()
        {
            _playerTurnStart_Requested.Subscribe(OnPlayerTurnStart_Requested);
            _playerWon.Subscribe(OnPlayerWon);
        }

        private void OnDisable()
        {
            _playerTurnStart_Requested.Unsubscribe(OnPlayerTurnStart_Requested);
            _playerWon.Unsubscribe(OnPlayerWon);
        }

        private void OnPlayerTurnStart_Requested()
        {
            playerAvatar.color = _currentPlayer.Player.GetPlayerColor();
        }

        private void OnPlayerWon()
        {
            infoText.text = "Wins!";
            exitButton.SetActive(true);
        }

        public void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}
