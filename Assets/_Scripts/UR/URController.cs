using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.UrdfImporter;
using Unity.Robotics.UrdfImporter.Control;
// using Unity.Robotics.UrdfImporter.

namespace Telepatia.URControl
{
    public class URController : MonoBehaviour
    {
        
        [field:Header ("Universal Robot Arm Info")]
        [field:SerializeField] public UniversalRobotArmSO URArmSO { get; private set; }
        [field:SerializeField] public ArticulationBody[] ArmArticulationLinks { get; private set; }
        [field:SerializeField] public ArticulationBody FlangeLink { get; private set; }
        [field:SerializeField] public ArticulationBody ToolLink { get; private set; }
        [field:SerializeField] public UrdfJointRevolute[] UrdfJoints {get; private set; }
        [field:SerializeField] public float[] JointAngles {get; private set;}
        /* ####
        
        Force equation applied on robotic arm (PD controllter):
        Stiffness is like a spring
        F = stiffness * (currentPosition - target) - damping * (currentVelocity - targetVelocity)
        https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/d324209baa4ed58f883d93c38b30c942e9b63fbf/tutorials/urdf_importer/urdf_appendix.md
        
        ##### */
        [field:Header ("Simulation Param")]
        [field:SerializeField] 
        [field:Tooltip(
            "Determine how fast the arm approaches target posotion, but susceptible to overshooting"
        )] public float Stiffness { get; private set; } = 10000f;

        [field:SerializeField] 
        [field:Tooltip(
            "Slow down the arm if reaching target position, speed it up if overshooting"
        )] public float Damping { get; private set; } = 100f;

        [field:SerializeField] 
        [field:Tooltip(
            "Constrain the output force applied when operating"
        )] public float ForceLimit { get; private set; } = 330f;
        [field:SerializeField] [field:Tooltip("degree/second")] public float Speed { get; private set; } = 10f;
        [field:SerializeField] [field:Tooltip("newton*meter")] public float Torque { get; private set; } = 100f;
        [field:SerializeField] [field:Tooltip("meter/second^2 or degree/second^2")] public float Acceleration { get; private set; } = 5f;
        
        private float m_speed;
        private float m_torque;
        private float m_acceleration;

        
        void Start()
        {
            Init();            
        }

        void FixedUpdate()
        {
            FK();
        }

        void Init()
        {
            ArmArticulationLinks = new ArticulationBody[URArmSO.NUM_MOVABLE_LINK];
            UrdfJoints = new UrdfJointRevolute[URArmSO.NUM_MOVABLE_LINK];

            // Init Joint Angles
            JointAngles = new float[URArmSO.NUM_MOVABLE_LINK];

            // Get Tool link
            ToolLink = GameObject.FindWithTag("ur_tool").GetComponent<ArticulationBody>();
            try {FlangeLink = GameObject.FindWithTag("ur_flange").GetComponent<ArticulationBody>();}
            catch {}

            // Get UR Articulation links
            GameObject[] urLinks = GameObject.FindGameObjectsWithTag("ur_link");
            for (int i = 0; i < urLinks.Length; i++)
            {
                ArmArticulationLinks[i] = urLinks[i].GetComponent<ArticulationBody>();
            }

            for (int i = 0; i < URArmSO.NUM_MOVABLE_LINK; i++)
            {
                // Set drive params
                ArticulationDrive drive = ArmArticulationLinks[i].xDrive;
                drive.stiffness = Stiffness;
                drive.damping = Damping;
                drive.forceLimit = ForceLimit;
                ArmArticulationLinks[i].xDrive = drive;

                // Get UrdfJointRevoluts
                UrdfJoints[i] = ArmArticulationLinks[i].GetComponent<UrdfJointRevolute>();
            }
        }

        void FK()
        {
            ArmArticulationLinks[0].SetDriveTargets(new List<float>(JointAngles));
        }
    }
}