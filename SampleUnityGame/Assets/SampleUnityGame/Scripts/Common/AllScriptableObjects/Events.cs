using System;

namespace HosseinSampleGame.ScriptableObjectCategories
{
    [Serializable]
    public class Events
    {
        public VoidEventSO AI_RollDiceRequested;
        public VoidEventSO DiceRolled;
        public VoidEventSO LevelInitialized;
        public VoidEventSO InitializeLevelRequested;
        public VoidEventSO StartPlayerTurnRequested;
        public VoidEventSO PlayerTurnEnded;
        public VoidEventSO PlayerWon;
        public VoidEventSO PlayerTurnStart_Requested;
        public VoidEventSO PlayerTurnStart_AnimationEnded;
        public VoidEventSO PlayerRollDiceRequested;
        public VoidEventSO PlayerwalkEnded;
        public VoidEventSO PlayerWalk_Requested;
    }
}
