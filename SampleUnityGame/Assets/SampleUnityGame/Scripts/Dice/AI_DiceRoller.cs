using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace HosseinSampleGame
{
    public class AI_DiceRoller : MonoBehaviour
    {
        //Events
        private VoidEventSO AI_RollDiceRequested;

        //Shared Data
        private PlayerControllerSO _currentPlayer;
        private DiceListSO _allDices;

        private List<DiceController> _readyDices = new List<DiceController>();
        private DiceController _selectedDice;

        private void ReferenceScriptableObjects()
        {
            AI_RollDiceRequested = AllScriptableObjects.Events.AI_RollDiceRequested;
            _currentPlayer = AllScriptableObjects.SharedData.CurrentPlayer;
            _allDices = AllScriptableObjects.SharedData.AllDices;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
        }

        private void OnEnable()
        {
            AI_RollDiceRequested.Subscribe(OnAI_RollDiceRequested);
        }

        private void OnDisable()
        {
            AI_RollDiceRequested.Unsubscribe(OnAI_RollDiceRequested);
        }

        private void OnAI_RollDiceRequested()
        {
            FindReadyDices();
            RollDiceWithHighesttMaxNumber();
        }

        private void FindReadyDices()
        {
            _readyDices.Clear();
            foreach (var dice in _allDices.Dices)
            {
                if (dice.IsPlayerDiceReadyToUse(_currentPlayer.Player))
                {
                    _readyDices.Add(dice);
                }
            }
        }

        private void RollDiceWithHighesttMaxNumber()
        {
            int highestMaxNumber = int.MinValue;

            foreach (var dice in _readyDices)
            {
                if (highestMaxNumber < dice.MaxNumber)
                {
                    highestMaxNumber = dice.MaxNumber;
                    _selectedDice = dice;
                }
            }

            StartCoroutine(RollDiceWithDelay());
        }

        IEnumerator RollDiceWithDelay()
        {
            yield return new WaitForSeconds(0.8f);
            _selectedDice.RollDice();
        }
    }
}
