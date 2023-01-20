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
        private AudioManager audioManager = AudioManager.instance;
        [SerializeField] private GameObject exclamationIcon; 
        public virtual void OnTurn(LevelManager levelManager) { }

        /// <summary>
        /// this method is called when the enemy gets killed
        /// </summary>
        public void Kill()
        {
            enemyData.currentPosition = new Vector3(-1, -1, -1);
            enemyData.orientation = new Vector3(0, 0, 0);
            Debug.Log("Enemy killed");
            audioManager.PlayAttackSfx();
            //StartCoroutine(DeactivateEnemy());
            gameObject.SetActive(false);
        }

        /// <summary>
        /// this method is called when the enemy is being attracted by gold
        /// </summary>
        /// <param name="attractionSquareId">the id of the square where the gold is located</param>
        /// <param name="levelManager">the levelManager</param>
        public void GetAttracted(int attractionSquareId, LevelManager levelManager)
        {
            enemyData.goldAttractionPath = levelManager.PathFinding(levelManager.SquareReferencedId(enemyData.currentPosition), attractionSquareId);
            StartCoroutine(ActivateExclamationForTime());
            Debug.Log(enemyData.enemyType + "Attracted to" + attractionSquareId);
        }

        public Vector3 GetEnemyRotationAngle()
        {
            Vector3 angle = Vector3.zero; 
            if (enemyData.orientation == Vector3.left)
            {
                angle.y = 180;
            }
            else if (enemyData.orientation == Vector3.up)
            {
                angle.y = 270;
            }
            else if (enemyData.orientation == Vector3.down)
            {
                angle.y = 90;
            }

            return angle;
        }

        public void SetActiveExclamationIcon(bool activate)
        {
            exclamationIcon.SetActive(activate);
        }

        public IEnumerator ActivateExclamationForTime()
        {
            SetActiveExclamationIcon(true);
            yield return new WaitForSeconds(1f);
            SetActiveExclamationIcon(false);
        }

        //public IEnumerator DeactivateEnemy()
        //{
        //    yield return new WaitForSeconds(GameSettings.movementAnimationDuration-0.5f);
        //    gameObject.SetActive(false);
        //}
    }
}