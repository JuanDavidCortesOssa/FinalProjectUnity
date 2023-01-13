using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEditor;
using System;

namespace ConquistaGO
{
    public partial class LevelManager : StateMachine
    {
        [BoxGroup("Data")]
        [InlineEditor]
        public LevelSO levelSO;

        [FoldoutGroup("World")]
        public GameObject worldPrefab;

        [FoldoutGroup("Player")]
        public GameObject playerPrefab;

        [FoldoutGroup("Enemy")]
        public GameObject enemyWarriorPrefab;
        [FoldoutGroup("Enemy")]
        public GameObject enemyLancerPrefab;
        [FoldoutGroup("Enemy")]
        public GameObject enemyDaggerPrefab;

        [FoldoutGroup("Item")]
        public GameObject goldPrefab;
        [FoldoutGroup("Item")]
        public GameObject camouflagePrefab;
        [FoldoutGroup("Item")]
        public float itemHeight;

        [FoldoutGroup("Board")]
        public Transform boardBottomLeftCorner;
        [FoldoutGroup("Board")]
        public float spacing;
        [FoldoutGroup("Board")]
        public float squareSize;
        [FoldoutGroup("Board")]
        public GameObject squarePrefab;
        [FoldoutGroup("Board")]
        public GameObject SpecialSquarePrefab;
        [FoldoutGroup("Board")]
        public GameObject FinalSquarePrefab;
        [FoldoutGroup("Board")]
        public GameObject linePrefab;
        [FoldoutGroup("Board")]

        [ReadOnly]
        public List<GameObject> squares;
        public List<GameObject> connections;

        WorldManager _worldManager;
        public WorldManager worldManager
        {
            get
            {
                if (!_worldManager)
                {
                    if (Application.isPlaying)
                        worldManager = WorldManager.Instance;
                    else
                        worldManager = GameObject.FindObjectOfType<WorldManager>();
                }
                return _worldManager;
            }
            set
            {
                _worldManager = value;
            }
        }

        public PlayerManager player;

        private void Awake()
        {
            Time.timeScale = 1;
        }

#if UNITY_EDITOR
        [Button]
        public void SetLevel()
        {

            EditorUtility.SetDirty(worldManager);
            int xHighest = (int)levelSO.level.board.squares.OrderByDescending(square => square.position.x).First().position.x;
            int yHighest = (int)levelSO.level.board.squares.OrderByDescending(square => square.position.y).First().position.y;

            Vector3 bottomLeftCorner = boardBottomLeftCorner.position;
            Vector3 topRightCorner = new Vector3(xHighest, yHighest, 0) * (squareSize + spacing);

            DestroyCurrentElements();

            //CREATE SQUARES
            for (int i = 0; i < levelSO.level.board.squares.Count; i++)
            {
                Square.SquareData squareFromList = levelSO.level.board.squares[i];
                GameObject squareObject = null;

                if (squareFromList.isSpecialSquare)
                {
                    squareObject = PrefabUtility.InstantiatePrefab(SpecialSquarePrefab) as GameObject;
                }
                else
                {
                    if (squareFromList.isFinalSquare)
                    {
                        squareObject = PrefabUtility.InstantiatePrefab(FinalSquarePrefab) as GameObject;
                    }
                    else
                    {
                        squareObject = PrefabUtility.InstantiatePrefab(squarePrefab) as GameObject;
                    }
                }

                Square square = squareObject.GetComponent<Square>();
                Vector3 squareInGamePosition = ToGamePosition(levelSO.level.board.squares[i].position);
                square.transform.position = squareInGamePosition;
                square.squareData = levelSO.level.board.squares[i];
                squares.Add(squareObject);
            }

            //CREATE CONNECTIONS
            for (int i = 0; i < levelSO.level.board.squares.Count; i++)
            {
                for (int j = 0; j < levelSO.level.board.squares[i].connections.Count; j++)
                {
                    Vector3 squareInGamePosition = squares[i].transform.position;
                    Vector3 connectionInGamePosition = ToGamePosition(SquareReferencedPosition(levelSO.level.board.squares[i].connections[j]));
                    Vector3[] LinePositions = new Vector3[2];

                    LinePositions[0] = new Vector3(squareInGamePosition.x, ((squares[i].transform.localScale.y) / 2) + 0.1f, squareInGamePosition.z);
                    LinePositions[1] = new Vector3(connectionInGamePosition.x, ((squares[i].transform.localScale.y) / 2) + 0.1f, connectionInGamePosition.z);

                    if (!IsRepeatedConnection(LinePositions))
                    {
                        GameObject line = PrefabUtility.InstantiatePrefab(linePrefab) as GameObject;
                        line.GetComponent<LineRenderer>().SetPositions(LinePositions);
                        connections.Add(line);
                    }
                }
            }

            //CREATE ENEMIES
            for (int i = 0; i < levelSO.level.enemiesData.Count; i++)
            {
                Vector3 enemyRelativePosition = SquareReferencedPosition(levelSO.level.enemiesData[i].initialSquare);
                Vector3 enemyInGamePosition = ToGamePosition(enemyRelativePosition);
                GameObject enemyGO = null;
                Enemy enemy = null;

                switch (levelSO.level.enemiesData[i].enemyType)
                {
                    case Enemy.EnemyData.EnemyType.Lancer:
                        enemyGO = PrefabUtility.InstantiatePrefab(enemyLancerPrefab) as GameObject;
                        enemy = enemyGO.GetComponent<EnemyLancer>();
                        enemyGO.transform.rotation = Quaternion.Euler(enemy.GetEnemyRotationAngle());
                        break;
                    case Enemy.EnemyData.EnemyType.Warrior:
                        enemyGO = PrefabUtility.InstantiatePrefab(enemyWarriorPrefab) as GameObject;
                        enemy = enemyGO.GetComponent<EnemyWarrior>();
                        enemyGO.transform.rotation = Quaternion.Euler(enemy.GetEnemyRotationAngle());
                        break;
                    case Enemy.EnemyData.EnemyType.dagger:
                        enemyGO = PrefabUtility.InstantiatePrefab(enemyDaggerPrefab) as GameObject;
                        enemy = enemyGO.GetComponent<EnemyDagger>();
                        enemyGO.transform.rotation = Quaternion.Euler(enemy.GetEnemyRotationAngle());
                        Debug.Log(enemy.GetEnemyRotationAngle());
                        break;
                    default:
                        break;
                }

                enemy.enemyData = levelSO.level.enemiesData[i];
                enemy.enemyData.currentPosition = enemyRelativePosition;
                enemy.enemyData.state = EntityData.State.Inactive;
                enemy.transform.position = new Vector3(enemyInGamePosition.x, GameSettings.enemyHeight, enemyInGamePosition.z);
                enemy.transform.rotation = Quaternion.Euler(enemy.GetEnemyRotationAngle());
                worldManager.worldData.enemies.Add(enemy);
            }

            Vector3 playerRelativePosition = SquareReferencedPosition(levelSO.level.playerData.initialSquare);
            Vector3 playerInGamePosition = ToGamePosition(playerRelativePosition);
            player = (PrefabUtility.InstantiatePrefab(playerPrefab) as GameObject).GetComponent<PlayerManager>();
            player.playerData = levelSO.level.playerData;
            player.playerData.currentPosition = playerRelativePosition;
            player.playerData.state = EntityData.State.Active;
            player.transform.position = new Vector3(playerInGamePosition.x, GameSettings.playerHeight, playerInGamePosition.z);
            //Debug.Log(player.playerData.camouflageAbility.state);

            //CREATE ITEMS
            for (int i = 0; i < levelSO.level.items.Count; i++)
            {
                Item.ItemData itemFromList = levelSO.level.items[i];
                GameObject itemObj = null;
                Item item = null;
                if (levelSO.level.items[i].itemType == Item.ItemData.ItemType.Camouflage)
                {
                    itemObj = PrefabUtility.InstantiatePrefab(camouflagePrefab) as GameObject;
                    item = itemObj.GetComponent<Item>();
                }
                else
                {
                    itemObj = PrefabUtility.InstantiatePrefab(goldPrefab) as GameObject;
                    item = itemObj.GetComponent<GoldThrowingItem>();
                }

                Vector3 itemInGamePosition = ToGamePosition(SquareReferencedPosition(itemFromList.initialSquare));
                item.transform.position = new Vector3(itemInGamePosition.x, itemHeight, itemInGamePosition.z);
                item.itemData = levelSO.level.items[i];
                item.itemData.currentSquare = levelSO.level.items[i].initialSquare;
                worldManager.GetComponent<WorldManager>().worldData.items.Add(item);
            }
        }
#endif

        /// <summary>
        /// This method checks if a connection has already been set on the board
        /// </summary>
        /// <param name="vectorArray">Vector3 Array with the position of the square and one of its connections</param>
        /// <returns>Returns true if the given connection is already set on the board</returns>
        private bool IsRepeatedConnection(Vector3[] vectorArray)
        {
            bool isRepeated = false;
            Vector3[] LinePositions = new Vector3[2];

            for (int k = connections.Count - 1; k >= 0; k--)
            {
                connections[k].GetComponent<LineRenderer>().GetPositions(LinePositions);
                if (LinePositions[0] == vectorArray[1] && LinePositions[1] == vectorArray[0])
                {
                    isRepeated = true;
                }
            }

            return isRepeated;
        }

        /// <summary>
        /// Destroy all the current elements of the level
        /// </summary>
        private void DestroyCurrentElements()
        {
            for (int i = squares.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(squares[i]);
                squares.RemoveAt(i);
            }

            for (int i = connections.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(connections[i]);
                connections.RemoveAt(i);
            }

            if (worldManager != null)
            {
                for (int i = worldManager.worldData.enemies.Count - 1; i >= 0; i--)
                {
                    if (worldManager.worldData.enemies[i])
                        DestroyImmediate(worldManager.worldData.enemies[i].gameObject);
                    worldManager.worldData.enemies.RemoveAt(i);
                }

                for (int i = worldManager.worldData.items.Count - 1; i >= 0; i--)
                {
                    if (worldManager.worldData.items[i])
                        DestroyImmediate(worldManager.worldData.items[i].gameObject);
                    worldManager.worldData.items.RemoveAt(i);
                }

            }

            if (player)
                DestroyImmediate(player.gameObject);
            worldManager.Reset();
        }

    }
}