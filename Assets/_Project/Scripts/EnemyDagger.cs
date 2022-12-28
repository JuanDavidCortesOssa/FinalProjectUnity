using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConquistaGO
{
    public class EnemyDagger : Enemy
    {
        /// <summary>
        /// The square the enemy is monitoring depending on his direction.
        /// </summary>
        Vector3 monitoredPosition;

        /// <summary>
        /// This method sets the initial parameters of the 'Enemy Dagger'
        /// </summary>
        private void Start()
        {
            monitoredPosition = enemyData.currentPosition + enemyData.orientation;
        }

        /// <summary>
        /// This method execute the logic of the 'Enemy Dagger' movement
        /// </summary>
        /// <param name="levelManager">The LevelManager of the game</param>
        public override void OnTurn(LevelManager levelManager)
        {
            DaggerMovementRequest(levelManager);
        }

        /// <summary>
        /// This method contains the logic of the 'Enemy Dagger' movement
        /// </summary>
        /// <param name="levelManager">The LevelManager of the game</param>
        public void DaggerMovementRequest(LevelManager levelManager)
        {
            PlayerManager player = PlayerManager.Instance;
            int monitoredSquareId = levelManager.SquareReferencedId(monitoredPosition);
            List<int> itemsPositions = levelManager.IdItemsPositions();

            if (enemyData.goldAttractionPath.Count != 0)
            {
                GoldAttractionMovement(levelManager, player, levelManager.playerSquareId, itemsPositions);
                enemyData.goldAttractionPath.RemoveAt(0);
            }
            else
            {
                if (monitoredSquareId == levelManager.playerSquareId &&
                    player.playerData.camouflageAbility.state == PlayerManager.PlayerData.Abilities.State.Unavailable
                    && gameObject.activeSelf)
                {
                    PlayerManager.Instance.Kill(levelManager);
                }
            }
        }

        /// <summary>
        /// This method contains the logic for the movement when the enemy is being attracted by gold
        /// </summary>
        /// <param name="levelManager">The levelManager</param>
        /// <param name="player">The playerManager</param>
        /// <param name="idPlayerPosition">The actual position of the player</param>
        /// <param name="itemsPositions">A list with the id of the items positions</param>
        public void GoldAttractionMovement(LevelManager levelManager, PlayerManager player, int idPlayerPosition, List<int> itemsPositions)
        {
            MoveEnemy(levelManager.ToGamePosition(enemyData.goldAttractionPath[0]) + new Vector3(0f, GameSettings.enemyHeight, 0f), enemyData.goldAttractionPath[0]);
            if (levelManager.SquareReferencedId(enemyData.goldAttractionPath[0]) == idPlayerPosition
                && gameObject.activeSelf)
            {
                player.Kill(levelManager);
            }

            for (int i = 0; i < itemsPositions.Count; i++)
            {
                if (levelManager.SquareReferencedId(enemyData.goldAttractionPath[0]) == itemsPositions[i])
                {
                    Item item = levelManager.GetItemById(itemsPositions[i]);
                    item.DisableItem();
                }
            }
            monitoredPosition = (enemyData.currentPosition + enemyData.orientation);
        }

        /// <summary>
        /// This method moves the enemy using the doTween library
        /// </summary>
        /// <param name="doMovePosition">the position where the enemy is going to move, this vector is a real position in the map</param>
        /// <param name="newCurrentPosition">the new position of the enemy, this vector is in terms of a relative position</param>
        public void MoveEnemy(Vector3 doMovePosition, Vector3 newCurrentPosition)
        {
            enemyData.isMoving = true;
            enemyData.orientation = newCurrentPosition - enemyData.currentPosition;
            transform.DORotate(GetEnemyRotationAngle(), 0.1f);
            transform.DOMove(doMovePosition, GameSettings.movementAnimationDuration).OnComplete(
                () =>
                {
                    enemyData.isMoving = false;
                });
            enemyData.currentPosition = newCurrentPosition;
        }
    }
}