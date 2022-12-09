using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConquistaGO
{
    public partial class LevelManager : StateMachine
    {
        private void Start()
        {
            SetState(new InitialState(this));
        }

        void Update()
        {
            UpdateState();
        }

        /// <summary>
        /// This method gets the position relative to the board of a square by entering its id
        /// </summary>
        /// <param name="id">Id of the solicited square</param>
        /// <returns>Vector3 with the relative position of the square</returns>
        private Vector3 SquareReferencedPosition(int id)
        {
            Vector3 squarePosition = new Vector3();

            for (int i = 0; i < squares.Count; i++)
            {

                if (squares[i].GetComponent<Square>().squareData.squareId == id)
                {
                    squarePosition = squares[i].GetComponent<Square>().squareData.position;
                    return squarePosition;
                }
            }
            return squarePosition;
        }

        public int playerSquareId => SquareReferencedId(player.playerData.currentPosition);

        /// <summary>
        /// This method obtains the id of a square when it is given its relative position
        /// </summary>
        /// <param name="relativePosition">The position relative to the board of the square</param>
        /// <returns>Int with the id of the solicited square</returns>
        public int SquareReferencedId(Vector3 relativePosition)
        {
            int squareId = 0;
            for (int i = 0; i < squares.Count; i++)
            {
                if (squares[i].GetComponent<Square>().squareData.position == relativePosition)
                {
                    squareId = squares[i].GetComponent<Square>().squareData.squareId;
                    return squareId;
                }
            }
            return squareId;
        }

        /// <summary>
        /// This method calculates the real position of a square when it is given its position relative to the board.
        /// </summary>
        /// <param name="squarePosition"> Vector3 with a position relative to the board</param>
        /// <returns>Vector3 with the real position of the given vector</returns>
        public Vector3 ToGamePosition(Vector3 squarePosition)
        {
            Vector3 gamePosition = boardBottomLeftCorner.position + new Vector3(squarePosition.x * (squareSize + spacing) + spacing,
                0f, squarePosition.y * (squareSize + spacing) + spacing);

            return gamePosition;
        }

        /// <summary>
        /// This method returns the square associated with an id
        /// </summary>
        /// <param name="squareId">The id of the solicited square</param>
        /// <returns>The solicited square</returns>
        public Square SquareReferenced(int squareId)
        {
            Square square = null;
            for (int i = 0; i < squares.Count; i++)
            {
                square = squares[i].GetComponent<Square>();
                if (square.squareData.squareId == squareId)
                {
                    break;
                }
            }

            return square;
        }

        /// <summary>
        /// This method returns a list with the enemies positions id
        /// </summary>
        /// <returns>A list with the enemies position id</returns>
        public List<int> IdEnemiesPositions()
        {
            List<int> enemiesPositions = new List<int>();
            List<Enemy> enemies = worldManager.worldData.enemies;

            for (int i = 0; i < enemies.Count; i++)
            {
                int enemyId = SquareReferencedId(enemies[i].enemyData.currentPosition);
                enemiesPositions.Add(enemyId);
            }

            return enemiesPositions;
        }

        /// <summary>
        /// This method returns a list with the items positions id
        /// </summary>
        /// <returns>A list with the items position id</returns>
        public List<int> IdItemsPositions()
        {
            List<int> itemsPositions = new List<int>();
            List<Item> items = worldManager.worldData.items;

            for (int i = 0; i < items.Count; i++)
            {
                itemsPositions.Add(items[i].itemData.currentSquare);
            }
            return itemsPositions;
        }

        /// <summary>
        /// This method returns an item when given its id
        /// </summary>
        /// <param name="searchId">The id of the searched item</param>
        /// <returns></returns>
        public Item GetItemById(int searchId)
        {
            Item item = null;
            List<Item> items = WorldManager.Instance.worldData.items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemData.currentSquare == searchId)
                {
                    item = items[i];
                    break;
                }
            }
            if (item == null)
            {
                Debug.Log("Item Not found");
            }
            return item;
        }

        /// <summary>
        /// This method determines if two squares are connected
        /// </summary>
        /// <param name="square1">The first square</param>
        /// <param name="square2">The second squeare</param>
        /// <returns>true if the squares are connected, false if not</returns>
        public bool AreConnectedSquares(int square1, int square2)
        {
            Square parentSquare = SquareReferenced(square1);
            bool isConnected = false;

            for (int i = 0; i < parentSquare.squareData.connections.Count; i++)
            {
                if (parentSquare.squareData.connections[i] == square2)
                {
                    isConnected = true;
                }
            }
            return isConnected;
        }

        /// <summary>
        /// this method calculates the shortest path between two squares
        /// </summary>
        /// <param name="actualId">The id of the start square</param>
        /// <param name="finalId">The id of the final square</param>
        /// <returns>A list of vector 3 with the path in terms of relative positions</returns>
        public List<Vector3> PathFinding(int actualId, int finalId)
        {
            Square actualSquare = SquareReferenced(actualId);
            List<int> idpath = new List<int>();
            List<Vector3> vectorPath = new List<Vector3>();
            Queue<Square> visitedElements = new Queue<Square>();
            visitedElements.Enqueue(actualSquare);

            while (visitedElements.Count != 0)
            {
                Square currentElement = visitedElements.Dequeue();
                idpath.Add(currentElement.squareData.squareId);
                currentElement.squareData.isVisited = true;

                if (currentElement.squareData.squareId != finalId)
                {
                    for (int i = 0; i < currentElement.squareData.connections.Count; i++)
                    {
                        Square squareConnection = SquareReferenced(currentElement.squareData.connections[i]);

                        if (squareConnection.squareData.isVisited == false)
                        {
                            visitedElements.Enqueue(squareConnection);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            for (int i = idpath.Count - 1; i >= 1; i--)
            {
                SquareReferenced(idpath[i]).squareData.isVisited = false;
                if (!AreConnectedSquares(idpath[i], idpath[i - 1]))
                {
                    idpath.RemoveAt(i - 1);
                }
            }
            idpath.RemoveAt(0);

            for (int i = 0; i < idpath.Count; i++)
            {
                vectorPath.Add(SquareReferencedPosition(idpath[i]));
            }
            return vectorPath;
        }
    }
}
