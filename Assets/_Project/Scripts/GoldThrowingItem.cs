using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ConquistaGO
{
    [System.Serializable]
    public class GoldThrowingItem : Item
    {
        public GameObject rangeGO;

        /// <summary>
        /// this method attracts the enemies in a determined area around the gold 
        /// </summary>
        /// <param name="levelManager">the levelManager</param>
        public void AttractEnemies(LevelManager levelManager)
        {
            WorldManager worldManager = WorldManager.Instance;
            Square square = levelManager.SquareReferenced(itemData.currentSquare);
            Vector3 centerSquarePosition = square.squareData.position;

            for (int i = 0; i < worldManager.worldData.enemies.Count; i++)
            {
                Enemy enemy = worldManager.worldData.enemies[i];
                if (enemy.enemyData.currentPosition.x >= centerSquarePosition.x - 1 &&
                    enemy.enemyData.currentPosition.x <= centerSquarePosition.x + 1 &&
                    enemy.enemyData.currentPosition.y >= centerSquarePosition.y - 1 &&
                    enemy.enemyData.currentPosition.y <= centerSquarePosition.y + 1)
                {
                    enemy.GetAttracted(square.squareData.squareId, levelManager);
                }
            }
        }

        public IEnumerator ActivateGoldRange()
        {
            rangeGO.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            rangeGO.SetActive(false);
        }
    }
}
