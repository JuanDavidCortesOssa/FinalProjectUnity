using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConquistaGO
{
    public abstract class State
    {
        protected LevelManager levelManager;
        public State(LevelManager levelManager)
        {
            this.levelManager = levelManager;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual void Update() { }

        public virtual IEnumerator OnExit()
        {
            yield break;
        }
    }
}