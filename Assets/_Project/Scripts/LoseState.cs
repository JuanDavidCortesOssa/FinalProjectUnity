using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConquistaGO
{
    public class LoseState : State
    {
        public LoseState(LevelManager levelManager) : base(levelManager)
        {
        }

        /// <summary>
        /// This method stops the game and is called when the player loses
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Start()
        {
            //PlayerManager.Instance.SetActive(false);
            PlayerManager.Instance.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            UIManagerJ.Instance.ActivateLoseCanvas();
            Time.timeScale = 0;
            //yield return null;
        }
    }
}
