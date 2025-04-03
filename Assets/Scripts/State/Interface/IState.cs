using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using UnityEngine;

namespace StatePattern.State
{
    public interface IState
    {
        public OnePunchManController Owner { get; set; }

        public void OnStateEnter();

        public void Update();
        public void OnStateExit();
    }
}

