//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.OnrobotRgControl
{
    [Serializable]
    public class OnRobotRGCommandRequest : Message
    {
        public const string k_RosMessageName = "onrobot_rg_control/GripperRGCommand";
        public override string RosMessageName => k_RosMessageName;

        public ushort gWidth;
        public ushort gForce;

        public OnRobotRGCommandRequest()
        {
            this.gWidth = 0;
            this.gForce = 0;
        }

        public OnRobotRGCommandRequest(ushort gWidth, ushort gForce)
        {
            this.gWidth = gWidth;
            this.gForce = gForce;
        }

        public static OnRobotRGCommandRequest Deserialize(MessageDeserializer deserializer) => new OnRobotRGCommandRequest(deserializer);

        private OnRobotRGCommandRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.gWidth);
            deserializer.Read(out this.gForce);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.gWidth);
            serializer.Write(this.gForce);
        }

        public override string ToString()
        {
            return "OnRobotRGCommandRequest: " +
            "\ngWidth: " + gWidth.ToString() +
            "\ngForce: " + gForce.ToString();
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
