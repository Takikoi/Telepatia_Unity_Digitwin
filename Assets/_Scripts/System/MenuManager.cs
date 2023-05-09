using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.SystemManager
{
    public class MenuManager : MonoBehaviour
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
            if (newState != ProgramManager.ProgramState.Menu) {return;}
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