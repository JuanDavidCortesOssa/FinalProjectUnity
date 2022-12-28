using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ConquistaGO
{
    public class EnemyWarrior : Enemy
    {
        /// <summary>
        /// This parameter is true when the enemy needs to change his direction of movement
        /// </summary>
        public bool changeDirection = false;

        /// <summary>
        /// This method execute the logic of the 'Enemy Warrior' movement
        /// </summary>
        /// <param name="levelManager">The LevelManager of the game</param>
        public override void OnTurn(LevelManager levelManager)
        {
            WarriorMovementRequest(levelManager);
            if (changeDirection)
            {
                enemyData.orientation = (-1) * enemyData.orientation;
                WarriorMovementRequest(levelManager);
                changeDirection = false;
            }
        }

        /// <summary>
        /// This method contains the logic of the 'Enemy Warrior' movement
        /// </summary>
        /// <param name="levelManager">The LevelManager of the game</param>
        public void WarriorMovementRequest(LevelManager levelManager)
        {
            PlayerManager player = PlayerManager.Instance;

            int idActualPosition = levelManager.SquareReferencedId(enemyData.currentPosition);
            int idPlayerPosition = levelManager.playerSquareId;
            Vector3 solicitedPosition = enemyData.currentPosition + enemyData.orientation;
            int idSolicitedPosition = levelManager.SquareReferencedId(solicitedPosition);
            int playerSquareId = levelManager.playerSquareId;
            List<int> itemsPositions = levelManager.IdItemsPositions();

            if (enemyData.goldAttractionPath.Count != 0)
            {
                GoldAttractionMovement(levelManager, player, idPlayerPosition, itemsPositions);
                enemyData.goldAttractionPath.RemoveAt(0);
            }
            else
            {
                RegularMovement(levelManager, idSolicitedPosition, idActualPosition, solicitedPosition, playerSquareId, player);
            }

        }

        /// <summary>
        /// This method contains the logic for the enemy warrior movement when is under'GoldAttraction'
        /// </summary>
        /// <param name="levelManager">the levelManager</param>
        /// <param name="player">the playerManager</param>
        /// <param name="idPlayerPosition">the id of the actual position of the player</param>
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
        }

        /// <summary>
        /// This method contains the logic for the regular movement of the warrior
        /// </summary>
        /// <param name="levelManager">the levelManager</param>
        /// <param name="idSolicitedPosition">the id of the position where the enemy wants to move to</param>
        /// <param name="idActualPosition">the id of the actual position of the enemy</param>
        /// <param name="solicitedPosition">a vector3 with the relative position where the enemy wants to move to </param>
        /// <param name="playerSquareId">the id of the actual position of the player</param>
        /// <param name="player">the playerManager</param>
        public void RegularMovement(LevelManager levelManager, int idSolicitedPosition, int idActualPosition, Vector3 solicitedPosition, int playerSquareId, PlayerManager player)
        {
            if (idSolicitedPosition != 0)
            {
                for (int i = 0; i < levelManager.squares.Count; i++)
                {
                    if (levelManager.squares[i].GetComponent<Square>().squareData.squareId == idActualPosition)
                    {
                        Square.SquareData position = levelManager.squares[i].GetComponent<Square>().squareData;

                        for (int j = 0; j < position.connections.Count; j++)
                        {
                            if (position.connections[j] == idSolicitedPosition)
                            {

                                MoveEnemy(levelManager.ToGamePosition(solicitedPosition) + new Vector3(0f, GameSettings.enemyHeight, 0f), solicitedPosition);
                                if (playerSquareId == idSolicitedPosition &&
                                    player.playerData.camouflageAbility.state == PlayerManager.PlayerData.Abilities.State.Unavailable
                                    && gameObject.activeSelf)
                                {
                                    player.Kill(levelManager);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                ChangeDirection();
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
            transform.DORotate(GetEnemyRotationAngle(), 0.1f);
            transform.DOMove(doMovePosition, GameSettings.movementAnimationDuration).OnComplete(
                () =>
                {
                    enemyData.isMoving = false;
                });
            enemyData.currentPosition = newCurrentPosition;
        }

        /// <summary>
        /// This method sets the changeDirection attribute of the 'EnemyWarrior' to true
        /// </summary>
        public void ChangeDirection()
        {
            changeDirection = true;
        }
    }
}