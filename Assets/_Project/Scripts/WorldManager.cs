using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ConquistaGO
{
    public class WorldManager : Singleton<WorldManager>
    {
        [System.Serializable]
        public class WorldData
        {
            public List<Item> items;
            public List<Enemy> enemies;
            public Turn.globalTurn currentTurn;

            public WorldData()
            {
                items = new List<Item>();
                enemies = new List<Enemy>();
            }
        }

        [ReadOnly]
        public Vector3 positionRequest;
        public WorldData worldData;

        public void SetActive(bool active)
        {

        }

        public void Reset()
        {
            worldData = new WorldData();
        }
    }

}
