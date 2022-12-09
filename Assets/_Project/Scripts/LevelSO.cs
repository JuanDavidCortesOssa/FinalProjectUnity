using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace ConquistaGO
{
    [CreateAssetMenu(fileName = "Level_", menuName = "ConquistaGO/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [System.Serializable]
        public class LevelSnapshot
        {
            public Board board;
            public PlayerManager.PlayerData playerData;
            public List<Enemy.EnemyData> enemiesData;
            public List<Item.ItemData> items;
        }

        [System.Serializable]
        public class Board
        {
            public List<Square.SquareData> squares;

            [Button]
            private void SetSquareId()
            {
                for (int i = 0; i < squares.Count; i++)
                {
                    squares[i].squareId = i + 1;
                }
            }
        }

        public LevelSnapshot level;

        [BoxGroup("Json")]
        [TextArea]
        public string jSon;
        [BoxGroup("Json")]
        [Button]
        public void SerializeJson()
        {
            jSon = JsonUtility.ToJson(level);
        }
        [BoxGroup("Json")]
        [Button]
        public void DeSerializeJson()
        {
            level = JsonUtility.FromJson<LevelSnapshot>(jSon);
            EditorUtility.SetDirty(this);
        }
    }
}
