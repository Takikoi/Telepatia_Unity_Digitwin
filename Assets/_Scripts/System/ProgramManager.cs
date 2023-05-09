using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.SystemManager
{
    public class ProgramManager : MonoBehaviour
    {
        public enum ProgramState {Menu, Exhibition, Control}
        public ProgramState CurrentState;
        public static event Action<ProgramState> OnProgramStateChanged;
        public static ProgramManager Instance;
        
        void Awake() 
        {
            Instance = this;
        }

        void Start()
        {
            UpdateProgramState(ProgramState.Menu);
        }

        
        void Update()
        {
            
        }

        public void UpdateProgramState(ProgramState newState)
        {
            CurrentState = newState;
            switch (newState)
            {
                case ProgramState.Menu:
                    break;
                case ProgramState.Exhibition:
                    break;
                case ProgramState.Control:
                    break;
                default:
                    break;
            }
            OnProgramStateChanged?.Invoke(newState);
        }
    }
}