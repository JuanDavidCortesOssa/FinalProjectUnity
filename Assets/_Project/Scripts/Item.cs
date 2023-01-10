using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace ConquistaGO
{
    [System.Serializable]
    public class Item : MonoBehaviour
    {
        [System.Serializable]
        public class ItemData
        {
            public enum ItemType { Gold, Camouflage }
            public ItemType itemType;

            public int currentSquare;
            public int initialSquare;

            bool isGoldType => itemType == ItemType.Gold;
            [ShowIf("isGoldType")]
            public List<int> throwingSquares = new List<int>();
        }
        public ItemData itemData = new ItemData();

        /// <summary>
        /// this method is called when the item is grabbed by an entity and it is no longer in the map
        /// </summary>
        public void DisableItem()
        {
            gameObject.SetActive(false);
            if (itemData.itemType == ItemData.ItemType.Camouflage)
            {
                PlayerManager.Instance.CamouflagePlayer(true);
                Debug.Log("Camouflage acquired");
            }
            itemData.currentSquare = 0;
        }
    }
}
