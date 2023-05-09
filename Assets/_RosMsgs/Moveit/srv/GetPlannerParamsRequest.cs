//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.Moveit
{
    [Serializable]
    public class GetPlannerParamsRequest : Message
    {
        public const string k_RosMessageName = "moveit_msgs/GetPlannerParams";
        public override string RosMessageName => k_RosMessageName;

        //  Name of the planning pipeline, uses default if empty
        public string pipeline_id;
        //  Name of planning config
        public string planner_config;
        //  Optional name of planning group (return global defaults if empty)
        public string group;

        public GetPlannerParamsRequest()
        {
            this.pipeline_id = "";
            this.planner_config = "";
            this.group = "";
        }

        public GetPlannerParamsRequest(string pipeline_id, string planner_config, string group)
        {
            this.pipeline_id = pipeline_id;
            this.planner_config = planner_config;
            this.group = group;
        }

        public static GetPlannerParamsRequest Deserialize(MessageDeserializer deserializer) => new GetPlannerParamsRequest(deserializer);

        private GetPlannerParamsRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.pipeline_id);
            deserializer.Read(out this.planner_config);
            deserializer.Read(out this.group);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.pipeline_id);
            serializer.Write(this.planner_config);
            serializer.Write(this.group);
        }

        public override string ToString()
        {
            return "GetPlannerParamsRequest: " +
            "\npipeline_id: " + pipeline_id.ToString() +
            "\nplanner_config: " + planner_config.ToString() +
            "\ngroup: " + group.ToString();
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