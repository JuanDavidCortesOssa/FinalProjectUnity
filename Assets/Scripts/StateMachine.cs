using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConquistaGO
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State state;

        /// <summary>
        /// This method sets the state of the game
        /// </summary>
        /// <param name="state">The new state to be assigned to the game</param>
        public void SetState(State state)
        {
            if (this.state != null)
                StartCoroutine(this.state.OnExit());

            this.state = state;
            StartCoroutine(state.Start());
        }

        /// <summary>
        /// This method execute the update method of the State
        /// </summary>
        public void UpdateState()
        {
            if (state == null) return;
            state.Update();
        }
    }
}
