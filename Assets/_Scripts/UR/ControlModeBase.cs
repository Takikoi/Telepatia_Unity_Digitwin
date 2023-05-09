using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.URControl
{
    public abstract class ControlModeBase : MonoBehaviour
    {
        protected ControlModeManager Manager; 
        public abstract void EnterMode();
        public abstract void ExitMode();
        public abstract void UpdateMode();
        
        public ControlModeBase(ControlModeManager manager) 
        {
            this.Manager = manager;
        }
    }
}