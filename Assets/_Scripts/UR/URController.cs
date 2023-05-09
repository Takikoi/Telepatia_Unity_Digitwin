using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.UrdfImporter;
using Unity.Robotics.UrdfImporter.Control;
using Telepatia.Toolkit;

namespace Telepatia.URControl
{
    public class URController : MonoBehaviour
    {        
        [field:Header ("Universal Robot Arm Info")]
        [field:SerializeField] public bool IsDummy {get; private set;}
        [field:SerializeField] public UniversalRobotArmSO URArmSO { get; private set; }
        [field:SerializeField] public ArticulationBody[] ArmArticulationLinks { get; private set; }
        [field:SerializeField] public ArticulationBody FlangeLink { get; private set; }
        [field:SerializeField] public ArticulationBody ToolLink { get; private set; }
        [field:SerializeField] public UrdfJoint[] UrdfJoints {get; private set; }
        [field:SerializeField] public float[] JointAngles {get; private set;}

        [field:Header ("Visual")]
        [field:SerializeField] public Material Hologram {get; private set;}
        [field:SerializeField] public Material Highlight {get; private set;}

        /* ####
        
        Force equation applied on robotic arm (PD controllter):
        Stiffness is like a spring
        F = stiffness * (currentPosition - target) - damping * (currentVelocity - targetVelocity)
        https://github.com/Unity-Technologies/Unity-Robotics-Hub/blob/d324209baa4ed58f883d93c38b30c942e9b63fbf/tutorials/urdf_importer/urdf_appendix.md
        
        ##### */

        [field:SerializeField] public float Speed {get; private set;} = 10f;
        [field:SerializeField] public float Torque {get; private set;} = 100f;
        [field:SerializeField] public float Acceleration {get; private set;} = 5f;
        
        private float m_speed;
        private float m_torque;
        private float m_acceleration;

        
        void Start() => Init();

        void FixedUpdate()
        {
        }

        void Init()
        {
            // Init Joint Articulation joints and urdf
            ArmArticulationLinks = new ArticulationBody[URArmSO.NumMoveableLink];
            UrdfJoints = new UrdfJointRevolute[URArmSO.NumMoveableLink];

            // Init Joint Angles
            JointAngles = new float[URArmSO.NumMoveableLink];

            // Get Tool link
            ToolLink = GameObject.FindWithTag("ur_tool").GetComponent<ArticulationBody>();
            FlangeLink = GameObject.FindWithTag("ur_flange").GetComponent<ArticulationBody>();

            // Get UR Articulation links
            ArmArticulationLinks = Helper.FindComponentsInChildrenWithTag<ArticulationBody>(transform.gameObject, "ur_link");

            for (int i = 0; i < URArmSO.NumMoveableLink; i++)
            {
                // Set drive params
                ArticulationDrive drive = ArmArticulationLinks[i].xDrive;
                drive.stiffness = URArmSO.Stiffness;
                drive.damping = URArmSO.Damping;
                drive.forceLimit = URArmSO.ForceLimit;
                ArmArticulationLinks[i].xDrive = drive;

                // Get UrdfJoint
                UrdfJoints[i] = ArmArticulationLinks[i].GetComponent<UrdfJoint>();
            }
        }
        
        public void MoveToPosition(float[] angles)
        {
            if (angles.Length != URArmSO.NumMoveableLink)
            {
                Debug.LogError("Number of desired angles exceeds number of moveable joints.");
                return;
            }
            JointAngles = angles;
            ArmArticulationLinks[0].SetDriveTargets(new List<float>(JointAngles));
        }

    }
}