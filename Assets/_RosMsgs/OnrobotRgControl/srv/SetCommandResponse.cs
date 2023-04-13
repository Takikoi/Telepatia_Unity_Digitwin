//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.OnrobotRgControl
{
    [Serializable]
    public class SetCommandResponse : Message
    {
        public const string k_RosMessageName = "onrobot_rg_control/SetCommand";
        public override string RosMessageName => k_RosMessageName;

        public bool success;
        public string message;

        public SetCommandResponse()
        {
            this.success = false;
            this.message = "";
        }

        public SetCommandResponse(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }

        public static SetCommandResponse Deserialize(MessageDeserializer deserializer) => new SetCommandResponse(deserializer);

        private SetCommandResponse(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.success);
            deserializer.Read(out this.message);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.success);
            serializer.Write(this.message);
        }

        public override string ToString()
        {
            return "SetCommandResponse: " +
            "\nsuccess: " + success.ToString() +
            "\nmessage: " + message.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize, MessageSubtopic.Response);
        }
    }
}