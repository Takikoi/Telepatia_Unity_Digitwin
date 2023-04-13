using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics;

public class URFastIKSolver : MonoBehaviour
{
    public const int NUM_JOINT = 6;
    [field:SerializeField] private ArticulationBody[] m_urLinks;
    [field:SerializeField] private Transform ee;
    [field:SerializeField] private Transform target;
    [field:SerializeField] public float tolerance = 0.01f;
    [field:SerializeField] public int maxIterations = 10;
    [field:SerializeField] private float stepSize = 0.1f;

    private Vector3[] rotationAxes; 

    private float[] jointAngles;

    void Start() 
    {
        Init();
    }

    void Update() 
    {
    }

    private void Init()
    {
        GameObject[] urLinks = GameObject.FindGameObjectsWithTag("ur_link");
        Vector3[] rotateAxes = new Vector3[urLinks.Length];

        for (int i = 0; i < urLinks.Length; i++)
        {
            if (urLinks[i].GetComponent<ArticulationBody>().isRoot) continue;
            // Rotate joint global coord to anchor coord
            urLinks[i].transform.eulerAngles = urLinks[i].GetComponent<ArticulationBody>().anchorRotation.eulerAngles;
            rotateAxes[i] = urLinks[i].transform.right;
            
            Debug.Log("Link " + i + ":" +rotateAxes[i]);
        }
    }
    private Matrix4x4 CalculateRotationMatrix(Vector3 unitAxis, float angle)
    {
        /*
        Creates a 3x3 rotation matrix in 3D space from an axis and an angle.
 
        Input
        :param unitAxis: A 3 element array containing the unit axis to rotate around (kx,ky,kz) 
        :param angle: The angle (in radians) to rotate by
    
        Output
        :return: A 3x3 element matix containing the rotation matrix
        */
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);

        float kx = unitAxis[0];
        float ky = unitAxis[1];
        float kz = unitAxis[2];

        Vector3 col0 = new Vector3(
            kx * kx * (1 - cosTheta) + cosTheta,
            kx * ky * (1 - cosTheta) + kz * sinTheta,
            kx * kz * (1 - cosTheta) - ky * sinTheta
        );

        Vector3 col1 = new Vector3(
            kx * ky * (1 - cosTheta) - kz * sinTheta,
            ky * ky * (1 - cosTheta) + cosTheta,
            ky * kz * (1 - cosTheta) + kx * sinTheta
        );

        Vector3 col2 = new Vector3(
            kx * kz * (1 - cosTheta) + ky * sinTheta,
            ky * kz * (1 - cosTheta) - kx * sinTheta,
            kz * kz * (1 - cosTheta) + cosTheta
        );

        return new Matrix4x4(col0, col1, col2, Vector4.zero);
    }

    private Matrix4x4 CalculateTransformMatrix(Vector3 unitAxis, Vector3 translation, float angle)
    {
        /*
        reate the Homogenous Representation matrix that transforms a point from Frame B to Frame A.
        Using the axis-angle representation
        Input
        :param unitAxis: A 3 element array containing the unit axis to rotate around (kx,ky,kz) 
        :param translation: The translation from the current frame (e.g. Frame A) to the next frame (e.g. Frame B)
        :param angle: The rotation angle (i.e. joint angle)
    
        Output
        :return: A 4x4 Homogenous representation matrix
        */
        Matrix4x4 transformMatrix = CalculateRotationMatrix(unitAxis, angle);
        
        transformMatrix.SetColumn(3, translation);
        transformMatrix.SetRow(3, new Vector4(0, 0, 0, 1));
        return transformMatrix;
    }

    // private Matrix4x4 CalculateJacobian(float[] jointAngles, Vector3 eeTransform)
    // {
    //     // Matrix4x4 jacobian = Matrix4x4.zero;

    //     // for (int i = 0; i < 2; i++)
    //     // {
    //     //     Vector3 deltaP = ee.position - artiJoints[i].transform.position;
    //     //     jacobian.SetColumn(i, Vector3.Cross(rotationAxes[i], deltaP));
    //     // }

    //     // return jacobian;
    // }

    // private float[] PseudoIK(float[] currentAngles, Vector3 eeLastFrame, Vector3 goalBase, int maxIterations)
    // {
    //     float vStepSize = 0.05f;
    //     float thetaMaxStep = 0.2f;
    //     float[] angles = currentAngles;
    //     Vector3 pEnd = goalBase;
    //     Vector3 pJ = target.position;
    //     Vector3 deltaP = pEnd - pJ;
    //     int j = 0;
    //     while (deltaP.magnitude > tolerance && j < maxIterations)
    //     {
    //         Vector3 vP = deltaP * vStepSize / deltaP.magnitude;
    //         Matrix4x4 J = CalculateJacobian(angles, eeLastFrame);
    //         Matrix4x4 J_inv = J.inverse;
            
    //     }
    //     re
    // }

    // private Vector3 GetFramePositionWrtBase(int index, )
    // {

    // }
}