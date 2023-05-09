using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Control;
using RosMessageTypes.Sensor;

// sometimes msg files are diff even with samew name so check your msg file
namespace Telepatia.URControl
{
    public class SimulateMode : ControlModeBase
    {
        [field:SerializeField] public string JointStateTopicName {get; private set;}
        [field:SerializeField] public string[] JointNames {get; private set;}
        [field:SerializeField] public double[] DesiredJointAngles {get; private set;}
        [field:SerializeField] public double[] ActualJointAngles {get; private set;}
        [field:SerializeField] public double[] JointAnglesError {get; private set;}

        public SimulateMode(ControlModeManager magaer) : base(magaer)
        {
        }
    
        public override void EnterMode()
        {
            Debug.Log("Enter Mirroring");
            this.JointStateTopicName = "scaled_pos_joint_traj_controller/state";
            // ROSConnection.GetOrCreateInstance().Subscribe<JointTrajectoryControllerStateMsg>(JointStateTopicName, Mirror);
        }

        public override void ExitMode()
        {
            
        }

        public override void UpdateMode()
        {
            Debug.Log("Update Mirroring");
            // https://stackoverflow.com/questions/69805135/how-does-c-sharp-convert-double-arrays-to-float-arrays-or-int-arrays

            // Manager.URControllerDesired.MoveToPosition(DesiredJointAngles.Select(d => (float)d).ToArray());
            Manager.URControllerActual.MoveToPosition(DesiredJointAngles.Select(d => (float)d).ToArray());
        }

        private void Mirror(JointTrajectoryControllerStateMsg msg)
        {
            JointNames = msg.joint_names;
            DesiredJointAngles = msg.desired.positions;
            ActualJointAngles = msg.actual.positions;
            JointAnglesError = msg.error.positions;
        }
    }
}