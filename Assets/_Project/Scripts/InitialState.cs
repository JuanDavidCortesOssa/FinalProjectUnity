using System.Collections;
using UnityEngine;

namespace ConquistaGO
{
    public class InitialState : State
    {
        public InitialState(LevelManager levelManager) : base(levelManager)
        {
        }

        /// <summary>
        /// This method waits 1 second and then assigns the player's turn.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Start()
        {
            PlayerManager.Instance.SetActive(false);

            yield return new WaitForSeconds(1);

            PlayerManager.Instance.SetActive(true);
            levelManager.SetState(new PlayerTurnState(levelManager));
        }
    }
}