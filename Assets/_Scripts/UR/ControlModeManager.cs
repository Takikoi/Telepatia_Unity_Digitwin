using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.UrdfImporter;
using RosMessageTypes.Control;
using RosMessageTypes.Sensor;
using Telepatia.Toolkit;
using RosMessageTypes.UnityMoveitPlanningInterface;

namespace Telepatia.URControl
{
    public class ControlModeManager : MonoBehaviour
    {
        public enum ControlMode {Simulate, Synchronous}
        public enum ControlKinematicType {FK, IK}

        public GameObject URRobotObj {get; internal set;}
        public URController URControllerDesired {get; internal set;}
        public URController URControllerActual {get; internal set;}
        public ControlMode CurrentMode {get; internal set;}
        [field:SerializeField] public ControlKinematicType KinematicType {get; private set;} = ControlKinematicType.FK;
        public List<Transform> Waypoints;
        

        public ROSConnection ROS {get; internal set;}
        public string JointTrajControllerTopic = "/eff_joint_traj_controller/state";
        public string MoveToPoseSrv = "/telepatia/move_to_pose_state";
        public string MoveToJointSrv = "/telepatia/move_to_joint_state";
        public string PlanCartesianPathSrv = "/telepatia/plan_cartesian_path";
        public string ExecutePathSrv = "/telepatia/execute_path";

        public Transform EETargetTransform;
        public delegate void Action();
        internal event Action PlanningEvent, ExecutingEvent;


        private ControlModeBase m_currentMode;
        private SimulateMode m_simulateMode;
        private SynchronousMode m_synchronousMode;
        private bool m_isSimulationOn;
        internal bool IsAutoSync;

        public void SetAutoSync(bool allowed) => IsAutoSync = allowed; 
        public void PlanTraj() => PlanningEvent?.Invoke();
        public void ExecuteTraj() => ExecutingEvent?.Invoke();

        void Start()
        {
            ROS = ROSConnection.GetOrCreateInstance();
            // ROS.RegisterRosService<ComputeIKRequest, ComputeIKResponse>(TestSrv);
            // ROS.RegisterRosService<MoveToPoseStateRequest, MoveToPoseStateResponse>("/telepatia/move_to_pose_state");
            

            URRobotObj = GameObject.FindGameObjectWithTag("ur_robot");
            URControllerDesired = Helper.FindComponentInChildWithTag<URController>(URRobotObj, "ur_robot_desired");
            URControllerActual = Helper.FindComponentInChildWithTag<URController>(URRobotObj, "ur_robot_actual");

            m_simulateMode = new SimulateMode(this);
            m_synchronousMode = new SynchronousMode(this);

            CurrentMode = ControlMode.Synchronous;
            m_currentMode = m_synchronousMode;
            m_currentMode.EnterMode();
        }

        void Update()
        {
            m_currentMode.UpdateMode();
        }

        public void SwitchToSynchronous()
        {
            if (CurrentMode == ControlMode.Synchronous) {return;}
            m_isSimulationOn = false;
            m_currentMode.ExitMode();
            CurrentMode = ControlMode.Synchronous;
            m_currentMode = m_synchronousMode;
            m_currentMode.EnterMode();
        }
        public void SwitchToSimulate()
        {
            if (CurrentMode == ControlMode.Simulate) {return;}
            m_isSimulationOn = true;
            m_currentMode.ExitMode();
            CurrentMode = ControlMode.Simulate;
            m_currentMode = m_simulateMode;
            m_currentMode.EnterMode();
        }
        private void SynchronizeRobot(JointStateMsg msg)
        {
            // if (!m_isSimulationOn) {return;} 
            double[] pos = msg.position;
            double dummy = pos[0];
            pos[0] = pos[2];
            pos[2] = dummy;
            // URControllerDesired.MoveToPosition(pos.Select(d => (float)d).ToArray());
            URControllerActual.MoveToPosition(pos.Select(d => (float)d).ToArray());
        }
    }
}