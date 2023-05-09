using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Telepatia.URControl;
public class DraftIKUI : MonoBehaviour
{
    public Transform SelectedTarget;
    [SerializeField] private float m_speed = 0.01f;
    [SerializeField] private ControlModeManager m_robotManager;
    [SerializeField] private TextMeshProUGUI[] m_eePos;
    [SerializeField] private TextMeshProUGUI[] m_eeRot;
    [SerializeField] private GameObject m_scrollViewContentParent, m_robotParent;
    [SerializeField] private GameObject m_waypointUIPrefab, m_waypointPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (SelectedTarget != null)
        {
            m_eePos[0].text = (Mathf.Round(SelectedTarget.localPosition.x * 1000f) * 0.001f).ToString();
            m_eePos[1].text = (Mathf.Round(SelectedTarget.localPosition.y * 1000f) * 0.001f).ToString();
            m_eePos[2].text = (Mathf.Round(SelectedTarget.localPosition.z * 1000f) * 0.001f).ToString();

            m_eeRot[0].text = (Mathf.Round(SelectedTarget.localRotation.x * 1000f) * 0.001f).ToString();
            m_eeRot[1].text = (Mathf.Round(SelectedTarget.localRotation.y * 1000f) * 0.001f).ToString();
            m_eeRot[2].text = (Mathf.Round(SelectedTarget.localRotation.z * 1000f) * 0.001f).ToString();
            m_eeRot[3].text = (Mathf.Round(SelectedTarget.localRotation.w * 1000f) * 0.001f).ToString();

            if (Input.GetAxis("Horizontal") != 0f)
            {
                Vector3 pos = SelectedTarget.position;
                pos.x -= Input.GetAxis("Horizontal") * m_speed;
                SelectedTarget.position = pos;
            }
            if (Input.GetAxis("Vertical") != 0f)
            {
                Vector3 pos = SelectedTarget.position;
                pos.z -= Input.GetAxis("Vertical") * m_speed;
                SelectedTarget.position = pos;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                Vector3 pos = SelectedTarget.position;
                pos.y -= m_speed;
                SelectedTarget.position = pos;
            }
            if (Input.GetKey(KeyCode.E))
            {
                Vector3 pos = SelectedTarget.position;
                pos.y += m_speed;
                SelectedTarget.position = pos;
            }
        }
    }
    

    public void AddWaypoint()
    {
        GameObject waypoint = Instantiate(m_waypointPrefab, m_robotParent.transform);
        waypoint.transform.localPosition = new Vector3(0f, 0.7f, 0.6f);
        GameObject waypointUI = Instantiate(m_waypointUIPrefab, m_scrollViewContentParent.transform);
        waypointUI.transform.SetSiblingIndex(m_scrollViewContentParent.transform.childCount - 2);
        waypointUI.GetComponent<Button>().onClick.AddListener(delegate{ChangeSelectedTarget(waypoint);});
        m_robotManager.Waypoints.Add(waypoint.transform);
    }

    private void ChangeSelectedTarget(GameObject target)
    {
        SelectedTarget = m_robotManager.EETargetTransform = target.transform;
    }
}
