using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace ConquistaGO
{
    public class EnemyLancer : Enemy
    {
        /// <summary>
        /// The square the enemy is monitoring depending on his direction.
        /// </summary>
        public Vector3 monitoredPosition;

        public bool isActive = false;

        /// <summary>
        /// This method sets the initial parameters of the 'Enemy lancer'
        /// </summary>
        private void Start()
        {
            monitoredPosition = (enemyData.currentPosition + 2 * enemyData.orientation);
        }

        /// <summary>
        /// The list of positions where the enemy will move 
        /// </summary>
        public List<Vector3> MovementPositions;

        /// <summary>
        /// This method execute the logic of the 'Enemy Lancer' movement
        /// </summary>
        /// <param name="levelManager">The LevelManager of the game</param>
        public override void OnTurn(LevelManager levelManager)
        {
            LancerMovementRequest(levelManager);
        }

        /// <summary>
        /// This method contains the logic of the 'Enemy Lancer' movement
        /// </summary>
        /// <param name="levelManager">The LevelManager of the game</param>
        public void LancerMovementRequest(LevelManager levelManager)
        {
            PlayerManager player = PlayerManager.Instance;
            int idPlayerPosition = levelManager.playerSquareId;
            int idMonitoredPosition = levelManager.SquareReferencedId(monitoredPosition);
            int idSquareInFront = levelManager.SquareReferencedId(monitoredPosition - enemyData.orientation);
            List<int> itemsPositions = levelManager.IdItemsPositions();

            if (MovementPositions.Count != 0)
            {
                RegularMovement(levelManager, player, idPlayerPosition, itemsPositions);
            }
            else
            {
                if (enemyData.goldAttractionPath.Count != 0 && !isActive)
                {
                    GoldAttractionMovement(levelManager, player, idPlayerPosition, itemsPositions);
                    enemyData.goldAttractionPath.RemoveAt(0);
                }
                else
                {
                    if (player.playerData.camouflageAbility.state == PlayerManager.PlayerData.Abilities.State.Unavailable)
                    {
                        PlayerNotCamouflagedInteraction(levelManager, player, idPlayerPosition, idMonitoredPosition, idSquareInFront);
                    }
                }
            }
        }

        /// <summary>
        /// This method contains the logic for the enemy lancer movement when is under'GoldAttraction'
        /// </summary>
        /// <param name="levelManager">the levelManager</param>
        /// <param name="player">the playerManager</param>
        /// <param name="idPlayerPosition">the id of the actual position of the player</param>
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
            monitoredPosition = (enemyData.currentPosition + 2 * enemyData.orientation);
        }

        /// <summary>
        /// This method contains the logic for the regular movement
        /// </summary>
        /// <param name="levelManager">the levelManager</param>
        /// <param name="player">the playerManager</param>
        /// <param name="idPlayerPosition">the id of the actual position of the player</param>
        /// <param name="itemsPositions">A list with the id of the items positions</param>
        public void RegularMovement(LevelManager levelManager, PlayerManager player, int idPlayerPosition, List<int> itemsPositions)
        {
            Vector3 enemyMovementDistance = MovementPositions[0] - enemyData.currentPosition;

            if (Math.Abs(enemyMovementDistance.x) >= 2 || Math.Abs(enemyMovementDistance.y) >= 2)
            {
                DeactivateEnemy();
                Debug.Log("Enemy Deactivated");
            }
            else
            {
                if (player.playerData.currentPosition != MovementPositions[(MovementPositions.Count - 1)])
                {
                    MovementPositions.Add(player.playerData.currentPosition);
                }
                MoveEnemy(levelManager.ToGamePosition(MovementPositions[0]) + new Vector3(0f, GameSettings.enemyHeight, 0f), MovementPositions[0]);

                if (levelManager.SquareReferencedId(MovementPositions[0]) == idPlayerPosition
                    && gameObject.activeSelf)
                {
                    player.Kill(levelManager);
                }

                for (int i = 0; i < itemsPositions.Count; i++)
                {
                    if (levelManager.SquareReferencedId(MovementPositions[0]) == itemsPositions[i])
                    {
                        Item item = levelManager.GetItemById(itemsPositions[i]);
                        item.DisableItem();
                    }
                }

                MovementPositions.RemoveAt(0);
            }
        }

        /// <summary>
        /// This method contains the logic for the 'ennemy lancer' when the player is not camouflaged
        /// </summary>
        /// <param name="levelManager">The levelManager</param>
        /// <param name="player">The playerManager</param>
        /// <param name="idPlayerPosition">The actual position of the player</param>
        /// <param name="idMonitoredPosition">The position monitored by the lancer</param>
        /// <param name="idSquareInFront">The square one position ahead of the lancer</param>
        public void PlayerNotCamouflagedInteraction(LevelManager levelManager, PlayerManager player, int idPlayerPosition, int idMonitoredPosition, int idSquareInFront)
        {
            if (idMonitoredPosition == idPlayerPosition)
            {
                ActivateEnemy();
                Debug.Log("Enemy Activated");
            }

            if (idSquareInFront == idPlayerPosition
                && gameObject.activeSelf)
            {
                player.Kill(levelManager);
            }
        }

        /// <summary>
        /// This method moves the enemy using the doTween library
        /// </summary>
        /// <param name="doMovePosition">The real position where the enemy will be moved to</param>
        /// <param name="newCurrentPosition">The position relative to the board where the enemy will be located</param>
        public void MoveEnemy(Vector3 doMovePosition, Vector3 newCurrentPosition)
        {
            enemyData.isMoving = true;
            enemyData.orientation = newCurrentPosition - enemyData.currentPosition;
            transform.DOMove(doMovePosition, GameSettings.movementAnimationDuration).OnComplete(
                () =>
                {
                    enemyData.isMoving = false;
                });
            enemyData.currentPosition = newCurrentPosition;
        }

        /// <summary>
        /// This method add the initial squares to the MovementPositions vector of the enemy
        /// </summary>
        public void ActivateEnemy()
        {
            isActive = true;
            MovementPositions.Add(monitoredPosition - enemyData.orientation);
            MovementPositions.Add(monitoredPosition);
        }

        public void DeactivateEnemy()
        {
            isActive = false;
            monitoredPosition = (enemyData.currentPosition + 2 * enemyData.orientation);
            for (int i = MovementPositions.Count - 1; i >= 0; i--)
            {
                MovementPositions.RemoveAt(i);
            }

            for (int i = enemyData.goldAttractionPath.Count - 1; i >= 0; i--)
            {
                enemyData.goldAttractionPath.RemoveAt(i);
            }

        }
    }
}
