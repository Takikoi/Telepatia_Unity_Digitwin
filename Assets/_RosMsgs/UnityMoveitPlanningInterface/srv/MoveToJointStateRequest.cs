//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityMoveitPlanningInterface
{
    [Serializable]
    public class MoveToJointStateRequest : Message
    {
        public const string k_RosMessageName = "unity_moveit_planning_interface/MoveToJointState";
        public override string RosMessageName => k_RosMessageName;

        public double[] targetJointAngles;

        public MoveToJointStateRequest()
        {
            this.targetJointAngles = new double[6];
        }

        public MoveToJointStateRequest(double[] targetJointAngles)
        {
            this.targetJointAngles = targetJointAngles;
        }

        public static MoveToJointStateRequest Deserialize(MessageDeserializer deserializer) => new MoveToJointStateRequest(deserializer);

        private MoveToJointStateRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.targetJointAngles, sizeof(double), 6);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.targetJointAngles);
        }

        public override string ToString()
        {
            return "MoveToJointStateRequest: " +
            "\ntargetJointAngles: " + System.String.Join(", ", targetJointAngles.ToList());
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
