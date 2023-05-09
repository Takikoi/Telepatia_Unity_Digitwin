using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosMessageTypes.Moveit;
using RosMessageTypes.Geometry;
using RosMessageTypes.UnityMoveitPlanningInterface;

namespace Telepatia.URControl
{
    [CreateAssetMenu(fileName = "URArmSO", menuName = "ScriptableObjects/UniversalRobotArm", order = 3)]
    public class RobotTrajectorySO : MonoBehaviour
    {
        public RobotTrajectorySO[] Poses;
    }
}