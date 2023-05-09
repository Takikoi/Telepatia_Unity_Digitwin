using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

using RosMessageTypes.Std;
using RosMessageTypes.Sensor;
using RosMessageTypes.Control;
using RosMessageTypes.Geometry;
using RosMessageTypes.Trajectory;
using RosMessageTypes.UnityMoveitPlanningInterface;
using RosMessageTypes.Moveit;


namespace Telepatia.URControl
{
    public class SynchronousMode : ControlModeBase
    {
        private JointTrajectoryPointMsg[] pointMsg;
        private RobotTrajectoryMsg trajMsg;
        private float m_awaitingResponseUntilTimestamp = -0.1f;
        public SynchronousMode(ControlModeManager manager) : base(manager) {}
        
        
        void Start() 
        {
        }

        public override void EnterMode()
        {
            Debug.Log("Enter Planning");
            Manager.ROS.RegisterRosService<MoveToPoseStateRequest, MoveToPoseStateResponse>(Manager.MoveToPoseSrv);
            Manager.ROS.RegisterRosService<MoveToJointStateRequest, MoveToJointStateResponse>(Manager.MoveToJointSrv);
            Manager.ROS.RegisterRosService<PlanCartesianPathRequest, PlanCartesianPathResponse>(Manager.PlanCartesianPathSrv);
            Manager.ROS.RegisterRosService<ExecutePathRequest, ExecutePathResponse>(Manager.ExecutePathSrv);
            Manager.ROS.Subscribe<JointStateMsg>(Manager.JointTrajControllerTopic, SynchronizeRobot);
            Manager.PlanningEvent += PlanPath;
            Manager.ExecutingEvent += ExecutePath;
        }

        public override void ExitMode()
        {
            
        }
        bool isLoopFinished = true;
        public override void UpdateMode()
        {
            // Debug.Log("Update Planning");
            if (Manager.IsAutoSync)
            {
                if (Time.time > m_awaitingResponseUntilTimestamp)
                {
                    // Debug.Log("Destination reached.");
                    MoveToPoseState();
                    // MoveToJointState();
                    // PlanPath();
                    m_awaitingResponseUntilTimestamp = Time.time + 0.1f; // don't send again for 1 second, or until we receive a response
                }
            }

            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     Debug.LogWarning("Planning");
            //     PlanPath();
            // }
            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     Debug.LogWarning("Executing");
            //     if (isLoopFinished)
            //     {
            //         ExecutePath();
            //         isLoopFinished = false;
            //     }
            // }
        }

        

        public void SynchronizeRobot(JointTrajectoryControllerStateMsg msg)
        {

        }

        public void MoveToPoseState()
        {
            // End-effector target pose
            PointMsg eeTargetPosition = new PointMsg(
                Manager.EETargetTransform.localPosition.z,
                Manager.EETargetTransform.localPosition.x * -1f,
                Manager.EETargetTransform.localPosition.y
            );
            QuaternionMsg eeTargetOrientation = new QuaternionMsg(
                Manager.EETargetTransform.localRotation.z * -1f,
                Manager.EETargetTransform.localRotation.x,
                Manager.EETargetTransform.localRotation.y,
                Manager.EETargetTransform.localRotation.w
            );
            MoveToPoseStateRequest ikRequest = new MoveToPoseStateRequest(new PoseMsg(eeTargetPosition, eeTargetOrientation));

            Manager.ROS.SendServiceMessage<MoveToPoseStateResponse>(Manager.MoveToPoseSrv, ikRequest, UpdateIK);
        }

        private void UpdateIK(MoveToPoseStateResponse response)
        {
            m_awaitingResponseUntilTimestamp = -0.1f;
        }

        public void MoveToJointState()
        {
            MoveToJointStateRequest fkRequest = new MoveToJointStateRequest(Manager.URControllerDesired.JointAngles.Select(d => (double)d).ToArray());
            Manager.ROS.SendServiceMessage<MoveToJointStateResponse>(Manager.MoveToJointSrv, fkRequest);
        }

        private void SynchronizeRobot(JointStateMsg msg)
        {
            double[] pos = msg.position;
            double dummy = pos[0];
            pos[0] = pos[2];
            pos[2] = dummy;
            Manager.URControllerDesired.MoveToPosition(pos.Select(d => (float)d).ToArray());
            Manager.URControllerActual.MoveToPosition(pos.Select(d => (float)d).ToArray());
        }

        private void PlanPath()
        {
            PoseMsg[] poses = new PoseMsg[Manager.Waypoints.Count];
            Debug.LogWarning("Length: " + Manager.Waypoints.Count);
            for (int i = 0; i < Manager.Waypoints.Count; i++)
            {
                poses[i] = new PoseMsg();
                poses[i].position = new PointMsg(
                    Manager.Waypoints[i].localPosition.z,
                    Manager.Waypoints[i].localPosition.x * -1f,
                    Manager.Waypoints[i].localPosition.y
                );
                poses[i].orientation = new QuaternionMsg(
                    Manager.Waypoints[i].localRotation.z * -1f,
                    Manager.Waypoints[i].localRotation.x,
                    Manager.Waypoints[i].localRotation.y,
                    Manager.Waypoints[i].localRotation.w
                );
            }
            PlanCartesianPathRequest planCartesianPathRequest = new PlanCartesianPathRequest(poses);
            Manager.ROS.SendServiceMessage<PlanCartesianPathResponse>(Manager.PlanCartesianPathSrv, planCartesianPathRequest, PlanningResponse);
        }
        private void ExecutePath()
        {
            ExecutePathRequest executePathRequest = new ExecutePathRequest(trajMsg);
            Manager.ROS.SendServiceMessage<ExecutePathResponse>(Manager.ExecutePathSrv, executePathRequest, ExecuteResponse);
        }

        private void ExecuteResponse(ExecutePathResponse response)
        {
            isLoopFinished = response.finished;
            // m_awaitingResponseUntilTimestamp = -0.1f;
        }
        private void PlanningResponse(PlanCartesianPathResponse response)
        {
            trajMsg = response.plannedTrajectory;
            // pointMsg = response.plannedTrajectory.joint_trajectory.points;
            m_awaitingResponseUntilTimestamp = -0.1f;
        }
    }
}