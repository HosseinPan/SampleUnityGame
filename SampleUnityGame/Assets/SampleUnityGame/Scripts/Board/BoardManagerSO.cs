using UnityEngine;

namespace HosseinSampleGame
{
    [CreateAssetMenu(fileName = "BoardManager.asset",
            menuName = GlobalSettings.SOMenuItemPath + "Managers/BoardManager")]
    public class BoardManagerSO : ScriptableObject
    {
        private GameObject _board;

        public void SetBoardGameObject(GameObject board)
        {
            _board = board;
        }

        public BoardTile GetFirstTile()
        {
            return _board.transform.GetChild(0).GetComponent<BoardTile>();
        }
    }
}
