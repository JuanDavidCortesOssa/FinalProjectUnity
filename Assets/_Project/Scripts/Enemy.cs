using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConquistaGO
{
    public class Enemy : MonoBehaviour
    {
        [System.Serializable]
        public class EnemyData : EntityData
        {
            public enum EnemyType { Lancer, Warrior, dagger }
            public EnemyType enemyType;
            public List<Vector3> goldAttractionPath = new List<Vector3>();
        }
        public EnemyData enemyData = new EnemyData();

        public virtual void OnTurn(LevelManager levelManager) { }

        /// <summary>
        /// this method is called when the enemy gets killed
        /// </summary>
        public void Kill()
        {
            this.enemyData.currentPosition = new Vector3(-1, -1, -1);
            enemyData.orientation = new Vector3(0, 0, 0);
            Debug.Log("Enemy killed");
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// this method is called when the enemy is being attracted by gold
        /// </summary>
        /// <param name="attractionSquareId">the id of the square where the gold is located</param>
        /// <param name="levelManager">the levelManager</param>
        public void GetAttracted(int attractionSquareId, LevelManager levelManager)
        {
            enemyData.goldAttractionPath = levelManager.PathFinding(levelManager.SquareReferencedId(enemyData.currentPosition), attractionSquareId);
            Debug.Log(enemyData.enemyType + "Attracted to" + attractionSquareId);
        }
    }
}