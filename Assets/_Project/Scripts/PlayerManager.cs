using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace ConquistaGO
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [System.Serializable]
        public class PlayerData : EntityData
        {
            [System.Serializable]
            public class Abilities
            {
                [BoxGroup("Abilities")]
                public enum State { Available, Unavailable }
                public State state = State.Unavailable;
            }

            public class GoldThrowingAbility : Abilities
            {
                public List<int> aimSquares;
            }

            public class CamouflageAbility : Abilities
            {
                public int turnsAvailable;
            }

            public GoldThrowingAbility goldThrowingAbility = new GoldThrowingAbility();
            public CamouflageAbility camouflageAbility = new CamouflageAbility();
        }

        [Header("PirateGOs")]
        public GameObject pirateGO;
        public GameObject barrelGO;
        public GameObject pirateSkin1GO;
        public GameObject pirateSkin2GO;
        public GameObject pirateSkin3GO;

        public bool isPlayerOnSpecialSquare = false;
        public PlayerData playerData;
        public int turnsPerPlay;
        private AudioManager audioManager;

        private void Start()
        {
            turnsPerPlay = 0;
            SelectSkin();
            audioManager = AudioManager.instance;
        }

        /// <summary>
        /// This method changes the state parameter of the player
        /// </summary>
        /// <param name="active">Determines if the player's state will be set as true or false</param>
        public void SetActive(bool active)
        {
            playerData.state = active ? EntityData.State.Active : EntityData.State.Inactive;
        }

        /// <summary>
        /// This method is called when the player is killed by an enemy or trap
        /// </summary>
        public void Kill(LevelManager levelManager)
        {
            Debug.Log("Player killed");
            Debug.Log("Player state: " + playerData.state);
            SetActive(false);
            Debug.Log("Player state after kill: " + playerData.state);
            levelManager.SetState(new LoseState(levelManager));
        }

        /// <summary>
        /// This method moves the player using the doTween library
        /// </summary>
        /// <param name="doMovePosition">The real position where the player will be moved to</param>
        /// <param name="relativePosition">The position relative to the board where the player will be located</param>
        public void MovePlayer(Vector3 doMovePosition, Vector3 relativePosition)
        {

            if (playerData.goldThrowingAbility.state != PlayerData.Abilities.State.Available)
            {
                turnsPerPlay++;
                Debug.Log("turn: " + turnsPerPlay);
                audioManager.PlayMoveSfx();

                playerData.isMoving = true;
                gameObject.transform.
                    DOMove(doMovePosition, GameSettings.movementAnimationDuration)
                    .OnComplete(
                    () =>
                    {
                        playerData.isMoving = false;
                    });
                playerData.currentPosition = relativePosition;

            }

                if (playerData.camouflageAbility.state == PlayerData.Abilities.State.Available)
                {
                    playerData.camouflageAbility.turnsAvailable--;
                    if (playerData.camouflageAbility.turnsAvailable == 0)
                    {
                        playerData.camouflageAbility.state = PlayerData.Abilities.State.Unavailable;
                        CamouflagePlayer(false);
                        Debug.Log("Clamouflage lost");
                    }
                }
        }

        /// <summary>
        /// This method is called when the player needs to throw an item to a certain position
        /// </summary>
        /// <param name="item">the item to be thrown</param>
        /// <param name="newCurrentSquare">the id of the new position of the item</param>
        /// <param name="doMovePosition">the real position where the item is going to be thrown to</param>
        public void ThrowItem(Item item, int newCurrentSquare, Vector3 doMovePosition)
        {
            item.itemData.currentSquare = newCurrentSquare;
            playerData.isMoving = true;
            
            item.transform.DOMove(doMovePosition, GameSettings.movementAnimationDuration)
                    .OnComplete(
                    () =>
                    {
                        StartCoroutine(item.GetComponent<GoldThrowingItem>().ActivateGoldRange());
                        playerData.isMoving = false;
                    });
        }

        /// <summary>
        /// This method is called to activate or deactivate the player camouflage
        /// </summary>
        /// <param name="useCamouflage">true to activate the camouflage or false to deactivate it</param>
        public void CamouflagePlayer(bool useCamouflage)
        {
            if (useCamouflage)
            {
                pirateGO.SetActive(false);
                barrelGO.SetActive(true);
            }
            else
            {
                pirateGO.SetActive(true);
                barrelGO.SetActive(false);
            }
        }

        /// <summary>
        /// This method is called when the move requested by the player is invalid.
        /// </summary>
        public void OnInvalidMove()
        {
            Debug.Log("Invalid move");
        }

        public void SelectSkin()
        {
            int skintype = PlayerPrefs.GetInt("skinNumber");
            if (skintype == 3)
            {
                pirateSkin3GO.SetActive(true);
                pirateSkin2GO.SetActive(false);
                pirateSkin1GO.SetActive(false);
            }
            else if (skintype == 2)
            {
                pirateSkin2GO.SetActive(true);
                pirateSkin1GO.SetActive(false);
                pirateSkin3GO.SetActive(false);
            }
            else
            {
                pirateSkin1GO.SetActive(true);
                pirateSkin2GO.SetActive(false);
                pirateSkin3GO.SetActive(false);
            }
        }
    }
}