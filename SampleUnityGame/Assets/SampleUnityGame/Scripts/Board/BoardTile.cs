using UnityEngine;

namespace HosseinSampleGame
{
    public class BoardTile: MonoBehaviour
    {
        [SerializeField] private TileType tileType;
        [SerializeField] private BoardTile nextTile;
        [SerializeField] private Transform jumpPoint;
        [SerializeField] private Transform secondJumpPoint;
        [SerializeField] private Transform thirdJumpPoint;

        //Shared Data
        private PlayerListSO _allPlayers;

        private void ReferenceScriptableObjects()
        {
            _allPlayers = AllScriptableObjects.SharedData.AllPlayers;
        }

        private void Awake()
        {
            ReferenceScriptableObjects();
        }

        public TileType TileType { get => tileType; }

        public BoardTile NextTile { get => nextTile; }

        public Vector3 GetJumpPoint()
        {
            int playersOnThisTile = FindNumberOfPlayersOnThisTile();
            switch (playersOnThisTile)
            {
                case 1:
                    return jumpPoint.position;
                case 2:
                    return secondJumpPoint.position;
                case 3:
                    return thirdJumpPoint.position;
                default:
                    return jumpPoint.position;
            }
        }

        public Vector3 GetJumpPointInitialize()
        {
            int playersOnThisTile = FindNumberOfPlayersOnThisTile();
            switch (playersOnThisTile)
            {
                case 0:
                    return jumpPoint.position;
                case 1:
                    return secondJumpPoint.position;
                case 2:
                    return thirdJumpPoint.position;
                default:
                    return jumpPoint.position;
            }
        }

        private int FindNumberOfPlayersOnThisTile()
        {
            int result = 0;
            foreach (var player in _allPlayers.Players)
            {
                if (player.currentTile == this)
                {
                    result++;
                }
            }
            return result;
        }
    }
}
