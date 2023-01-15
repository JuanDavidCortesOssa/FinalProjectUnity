using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConquistaGO
{
    public class PlayerTurnState : State
    {
        PlayerManager playerManager;

        public PlayerTurnState(LevelManager levelManager) : base(levelManager)
        {
        }

        /// <summary>
        /// This method waits until the player makes their move and then changes the TurnState to the world
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Start()
        {
            playerManager = PlayerManager.Instance;
            WorldManager.Instance.SetActive(false);
            playerManager.SetActive(true);
            yield return new WaitUntil(() => playerManager.playerData.isMoving);
            levelManager.SetState(new EnemyTurnState(levelManager));
        }

        public override void Update()
        {
            UpdatePlayerInput();
        }

        /// <summary>
        /// This method listens to the player movement inputs
        /// </summary>
        public void UpdatePlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(0, 1));
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(0, -1));
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(1, 0));
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(-1, 0));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerSpecialSquareMove();
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlayerGoldThrowingMove();
                SpecialSquareMouseInput();
            }

            if (Input.GetMouseButton(0))
            {
                float XAxis = Math.Abs(Input.GetAxis("Mouse X"));
                float YAxis = Math.Abs(Input.GetAxis("Mouse Y"));

                if (YAxis > XAxis)
                {
                    if (Input.GetAxis("Mouse Y") > 0)
                    {
                        PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(0, 1));
                    }
                    else if (Input.GetAxis("Mouse Y") < 0)
                    {
                        PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(0, -1));
                    }

                }
                else
                {
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(1, 0));
                    }
                    else if (Input.GetAxis("Mouse Y") < 0)
                    {
                        PlayerMovementRequest(playerManager.playerData.currentPosition + new Vector3(-1, 0));
                    }
                }
            }
        }

        /// <summary>
        /// this method contains the logic of the player movement
        /// </summary>
        /// <param name="positionRequested">The relative position of the square were the player wants to move</param>
        public void PlayerMovementRequest(Vector3 positionRequested)
        {
            Debug.Log("Player state: " + playerManager.playerData.state);
            if (playerManager.playerData.state == EntityData.State.Inactive) return;
          
            int idActualPosition = levelManager.SquareReferencedId(playerManager.playerData.currentPosition);
            int idSolicitedPosition = levelManager.SquareReferencedId(positionRequested);
            WorldManager worldManager = WorldManager.Instance;

            if (idSolicitedPosition != 0)
            {
                Square.SquareData actualSquare = levelManager.SquareReferenced(idActualPosition).squareData;

                for (int j = 0; j < actualSquare.connections.Count; j++)
                {
                    if (actualSquare.connections[j] == idSolicitedPosition)
                    {
                        List<int> enemiesSquares = levelManager.IdEnemiesPositions();
                        playerManager.MovePlayer(levelManager.ToGamePosition(positionRequested) + new Vector3(0f, GameSettings.playerHeight, 0f), positionRequested);

                        for (int i = 0; i < enemiesSquares.Count; i++)
                        {
                            if (enemiesSquares[i] == idSolicitedPosition)
                            {
                                worldManager.worldData.enemies[i].Kill();
                            }
                        }

                        for (int i = 0; i < worldManager.worldData.items.Count; i++)
                        {
                            Item item = worldManager.worldData.items[i];
                            Item.ItemData itemData = item.itemData;
                            if (itemData.currentSquare == idSolicitedPosition)
                            {
                                switch (itemData.itemType)
                                {
                                    case Item.ItemData.ItemType.Gold:
                                        playerManager.playerData.goldThrowingAbility.state = PlayerManager.PlayerData.Abilities.State.Available;
                                        playerManager.playerData.goldThrowingAbility.aimSquares = itemData.throwingSquares;
                                        levelManager.ActivateGoldThrowingTargets();
                                        break;
                                    case Item.ItemData.ItemType.Camouflage:
                                        playerManager.playerData.camouflageAbility.state = PlayerManager.PlayerData.Abilities.State.Available;
                                        playerManager.playerData.camouflageAbility.turnsAvailable = 3;

                                        item.DisableItem();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        if (levelManager.SquareReferenced(idSolicitedPosition).squareData.isFinalSquare)
                        {
                            levelManager.SetState(new WinState(levelManager));
                            return;
                        }

                        return;
                    }
                }
            }
            playerManager.OnInvalidMove();
        }

        /// <summary>
        /// This method contains the logic of the player's movement when it comes to a special square.
        /// </summary>
        public void PlayerSpecialSquareMove()
        {
            WorldManager worldManager = WorldManager.Instance;
            int idActualPosition = levelManager.SquareReferencedId(playerManager.playerData.currentPosition);
            Square square = levelManager.SquareReferenced(idActualPosition);

            if (square.squareData.isSpecialSquare)
            {
                Square targetSquare = levelManager.SquareReferenced(square.squareData.targetSquare);
                Vector3 movePosition = targetSquare.squareData.position;
                int movePositionId = levelManager.SquareReferencedId(movePosition);
                playerManager.MovePlayer(levelManager.ToGamePosition(movePosition) + new Vector3(0f, GameSettings.playerHeight, 0f), movePosition);

                //List<int> enemiesPositions = levelManager.IdEnemiesPositions();

                //for (int i = 0; i < enemiesPositions.Count; i++)
                //{
                //    if (enemiesPositions[i] == movePositionId)
                //    {
                //        worldManager.worldData.enemies[i].Kill();
                //    }
                //}

                //for (int i = 0; i < worldManager.worldData.items.Count; i++)
                //{
                //    Item item = worldManager.worldData.items[i];
                //    Item.ItemData itemData = item.itemData;
                //    if (itemData.currentSquare == movePositionId)
                //    {
                //        switch (itemData.itemType)
                //        {
                //            case Item.ItemData.ItemType.Gold:
                //                playerManager.playerData.goldThrowingAbility.state = PlayerManager.PlayerData.Abilities.State.Available;
                //                playerManager.playerData.goldThrowingAbility.aimSquares = itemData.throwingSquares;
                //                break;
                //            case Item.ItemData.ItemType.Camouflage:
                //                playerManager.playerData.camouflageAbility.turnsAvailable = 4;
                //                playerManager.playerData.camouflageAbility.state = PlayerManager.PlayerData.Abilities.State.Available;
                //                break;
                //            default:
                //                break;
                //        }
                //        item.DisableItem();
                //    }
                //}
            }
            else
            {
                playerManager.OnInvalidMove();
            }
        }

        /// <summary>
        /// This method contains the logic for the goldThrowing interaction
        /// </summary>
        public void PlayerGoldThrowingMove()
        {
            if (playerManager.playerData.goldThrowingAbility.state == PlayerManager.PlayerData.Abilities.State.Available)
            {
                WorldManager worldManager = WorldManager.Instance;
                RaycastHit info;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out info, 100f))
                {
                    if (info.transform != null && info.transform.GetComponent<Square>() != null)
                    {
                        Square square = info.transform.GetComponent<Square>();
                        List<int> aimSquares = playerManager.playerData.goldThrowingAbility.aimSquares;

                        for (int i = 0; i < aimSquares.Count; i++)
                        {
                            if (square.squareData.squareId == aimSquares[i])
                            {
                                int idActualPosition = levelManager.SquareReferencedId(playerManager.playerData.currentPosition);
                                Item item = levelManager.GetItemById(idActualPosition);
                                playerManager.ThrowItem(item, square.squareData.squareId, square.transform.position + new Vector3(0, levelManager.itemHeight, 0));
                                // Here it will deactivate the targets
                                levelManager.DectivateGoldThrowingTargets();
                                
                                item.GetComponent<GoldThrowingItem>().AttractEnemies(levelManager);
                                playerManager.playerData.goldThrowingAbility.state = PlayerManager.PlayerData.Abilities.State.Unavailable;
                            }
                        }
                    }
                }
            }
        }

        public void SpecialSquareMouseInput()
        {
            WorldManager worldManager = WorldManager.Instance;
            int idActualPosition = levelManager.SquareReferencedId(playerManager.playerData.currentPosition);
            Square currentSquare = levelManager.SquareReferenced(idActualPosition);

            if (currentSquare.squareData.isSpecialSquare)
            {
                RaycastHit info;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out info, 100f))
                {
                    if (info.transform != null && info.transform.GetComponent<Square>() != null)
                    {
                        Square pressedSquare = info.transform.GetComponent<Square>();
                        if (pressedSquare.squareData.isSpecialSquare && pressedSquare != currentSquare)
                        {
                            Debug.Log("Player: " + playerManager.playerData.currentPosition + "Square: " + pressedSquare.squareData.position);
                            //PlayerSpecialSquareMove();
                            Square targetSquare = levelManager.SquareReferenced(pressedSquare.squareData.squareId);
                            Vector3 movePosition = targetSquare.squareData.position;
                            playerManager.MovePlayer(levelManager.ToGamePosition(movePosition) + new Vector3(0f, GameSettings.playerHeight, 0f), movePosition);
                        }
                    }
                }
            }
        }
    }
}