using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ConquistaGO
{
    public class Square : MonoBehaviour
    {
        [System.Serializable]
        public class SquareData
        {
            public Vector3 position;

            public bool isSpecialSquare;
            [ShowIf("isSpecialSquare")]
            public int targetSquare;

            public bool isFinalSquare;
            public List<int> connections;

            public enum State { Available, Ocupied }
            public State state;

            [ReadOnly]
            public int squareId;

            public bool isVisited = false;
        }

        public GameObject goldThrowingTargetGO;
        public SquareData squareData = new SquareData();

        public void SetGoldTargetActive(bool setActive)
        {
            goldThrowingTargetGO.SetActive(setActive);
    }
    }
}
