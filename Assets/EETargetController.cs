using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EETargetController : MonoBehaviour
{
    [SerializeField] private float m_speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0f)
        {
            Vector3 pos = transform.position;
            pos.x -= Input.GetAxis("Horizontal") * m_speed;
            transform.position = pos;
        }
        if (Input.GetAxis("Vertical") != 0f)
        {
            Vector3 pos = transform.position;
            pos.z -= Input.GetAxis("Vertical") * m_speed;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 pos = transform.position;
            pos.y -= m_speed;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.E))
        {
            Vector3 pos = transform.position;
            pos.y += m_speed;
            transform.position = pos;
        }
    }
}
