using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.URControl
{
    [CreateAssetMenu(fileName = "URArmSO", menuName = "ScriptableObjects/UniversalRobotArm", order = 1)]
    public class UniversalRobotArmSO : ScriptableObject
    {
        [field:SerializeField] public int NUM_MOVABLE_LINK {get; private set;}
        public URArmModel Model;
    }
    public enum URArmModel {UR3, UR3e, UR5, UR5e, UR10, UR10e, UR16e}
}