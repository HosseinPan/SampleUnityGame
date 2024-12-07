using System;

namespace HosseinSampleGame.ScriptableObjectCategories
{
    [Serializable]
    public class SharedData
    {
        public PlayerControllerSO CurrentPlayer;
        public DiceListSO AllDices;
        public PlayerListSO AllPlayers;
        public IntSharedVariableSO RolledDiceValue;
        public DiceControllerSO SelectedDice;
        
    }
}
