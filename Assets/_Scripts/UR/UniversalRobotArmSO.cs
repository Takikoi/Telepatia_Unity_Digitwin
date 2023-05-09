using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.URControl
{
    [CreateAssetMenu(fileName = "URArmSO", menuName = "ScriptableObjects/UniversalRobotArm", order = 1)]
    public class UniversalRobotArmSO : ScriptableObject
    {
        public URArmModel Model;
        [field:SerializeField] public int NumMoveableLink {get; private set;} = 6;

        [field:Header ("For Simulation")]
        [field:SerializeField] 
        [field:Tooltip(
            "Determine how fast the arm approaches target posotion, but susceptible to overshooting"
        )] public float Stiffness {get; private set;} = 10000f;

        [field:SerializeField] 
        [field:Tooltip(
            "Slow down the arm if reaching target position, speed it up if overshooting"
        )] public float Damping {get; private set;} = 1000f;

        [field:SerializeField] 
        [field:Tooltip(
            "Constrain the output force applied when operating"
        )] public float ForceLimit {get; private set;} = 330f;
    }
    public enum URArmModel {UR3, UR3e, UR5, UR5e, UR10, UR10e, UR16e}
}