//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.OnrobotRgControl
{
    [Serializable]
    public class OnRobotRGOutputMsg : Message
    {
        public const string k_RosMessageName = "onrobot_rg_control/OnRobotRGOutput";
        public override string RosMessageName => k_RosMessageName;

        //  rGFR : The target force to be reached when gripping and holding a workpiece.
        //         It must be provided in 1/10th Newtons.
        //         The valid range is 0 to 400 for the RG2 and 0 to 1200 for the RG6.
        public ushort rGFR;
        //  rGWD : The target width between the finger to be moved to and maintained.
        //         It must be provided in 1/10th millimeters.
        //         The valid range is 0 to 1100 for the RG2 and 0 to 1600 for the RG6.
        //         Please note that the target width should be provided corrected for any fingertip offset,
        //         as it is measured between the insides of the aluminum fingers.
        public ushort rGWD;
        //  rCTR : The control field is used to start and stop gripper motion.
        //         Only one option should be set at a time.
        //         Please note that the gripper will not start a new motion
        //         before the one currently being executed is done (see busy flag in the Status field).
        //  0x0001 - grip
        //           Start the motion, with the preset target force and width.
        //           Width is calculated without the fingertip offset.
        //           Please note that the gripper will ignore this command
        //           if the busy flag is set in the status field.
        //  0x0008 - stop
        //           Stop the current motion.
        //  0x0010 - grip_w_offset
        //           Same as grip, but width is calculated with the set fingertip offset.
        public byte rCTR;

        public OnRobotRGOutputMsg()
        {
            this.rGFR = 0;
            this.rGWD = 0;
            this.rCTR = 0;
        }

        public OnRobotRGOutputMsg(ushort rGFR, ushort rGWD, byte rCTR)
        {
            this.rGFR = rGFR;
            this.rGWD = rGWD;
            this.rCTR = rCTR;
        }

        public static OnRobotRGOutputMsg Deserialize(MessageDeserializer deserializer) => new OnRobotRGOutputMsg(deserializer);

        private OnRobotRGOutputMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.rGFR);
            deserializer.Read(out this.rGWD);
            deserializer.Read(out this.rCTR);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.rGFR);
            serializer.Write(this.rGWD);
            serializer.Write(this.rCTR);
        }

        public override string ToString()
        {
            return "OnRobotRGOutputMsg: " +
            "\nrGFR: " + rGFR.ToString() +
            "\nrGWD: " + rGWD.ToString() +
            "\nrCTR: " + rCTR.ToString();
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
