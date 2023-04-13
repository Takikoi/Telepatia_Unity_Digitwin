using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.URControl
{
    public class URWavePoint : MonoBehaviour
    {
        [field:SerializeField] public float[] JointAngles {get; private set;}
        [field:SerializeField] public Transform EETargetTransform {get; private set;}

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public URWavePoint(float[] angles)
        {
            JointAngles = angles;
        }
    }
}