using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.SystemManager
{
    public class ControlManager : MonoBehaviour
    {
        void Awake() 
        {
            ProgramManager.OnProgramStateChanged += ProgramManagerOnStateChange;
        }

        private void OnDestroy() 
        {
            ProgramManager.OnProgramStateChanged -= ProgramManagerOnStateChange;
        }

        private void ProgramManagerOnStateChange(ProgramManager.ProgramState newState)
        {
            if (newState != ProgramManager.ProgramState.Control) {return;}
        }
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}