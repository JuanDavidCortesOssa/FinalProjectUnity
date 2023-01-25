using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConquistaGO
{
    [System.Serializable]
    public class EntityData
    {
        public enum State { Active, Inactive }
        public State state;
        public Vector3 orientation;
        public Turn currentTurn;

        public List<ItemTypeSO> entityItems;
        public int initialSquare;
        public Vector3 currentPosition;
        public bool isMoving;
    }
}

