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
            PlayerManager.Instance.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Time.timeScale = 0;
        }

    }
}
