using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.GridSystem
{
    public class RobotDatabaseSO : MonoBehaviour
    {
        public List<RobotData> RobotDatas;
        
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

    [Serializable]
    public class RobotData
    {
        [field: SerializeField] public string Name {get; private set;}
        [field: SerializeField] public int ID {get; private set;}
        [field: SerializeField] public Vector2Int Size {get; private set;}
        [field: SerializeField] public GameObject Prefab {get; private set;}
    }
}