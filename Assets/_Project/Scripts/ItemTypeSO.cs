using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ConquistaGO
{
    [CreateAssetMenu(fileName = "Item_", menuName = "ConquistaGO/Item")]

    [System.Serializable]
    public class ItemTypeSO : ScriptableObject
    {
        public string enemyControls;
    }
}