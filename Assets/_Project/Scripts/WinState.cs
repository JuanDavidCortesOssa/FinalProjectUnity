using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConquistaGO
{
    public class WinState : State
    {
        public WinState(LevelManager levelManager) : base(levelManager)
        {
        }

        /// <summary>
        /// This method stops the game and is called when the player wins
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Start()
        {
            Debug.Log("Win");
            AudioManager.instance.PlayVictorySfx();
            PlayerManager.Instance.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            UIManagerJ.Instance.ActivateWinCanvas(PlayerManager.Instance.turnsPerPlay);
            //Time.timeScale = 0;
            //yield return null;
        }

    }
}
