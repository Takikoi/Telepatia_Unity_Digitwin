using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Telepatia.Toolkit;
using DG.Tweening;

namespace Telepatia.GridSystem
{
    public class RobotPlacementManager : MonoBehaviour
    {
        [field:SerializeField] private GameObject m_mouseIndicator, m_cellIndicator;
        [field:SerializeField] private PlacementInput m_input;
        [field:SerializeField] private Grid m_grid;
        [field:SerializeField] private RobotDatabaseSO m_database;
        private int m_selectedObjectIndex = -1;        

        void Start()
        {
            
        }
        bool enable;
        void Update()
        {
            if (enable)
            {

                Vector3 mousePosition = m_input.GetSelectedMapPosition();
                m_mouseIndicator.transform.position = mousePosition;

                Vector3Int gridPosition = m_grid.WorldToCell(mousePosition);
                m_cellIndicator.transform.position = m_grid.CellToWorld(gridPosition);
            }

            // m_input.HitObject.GetComponent<PlacementGrid>().OnMouseHover();
            if (Input.GetMouseButtonDown(0)) InstantiateRobot();
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                Vector3 dummy = m_cellIndicator.FindComponentInChildWithTag<Transform>("robot").localEulerAngles;
                dummy.y += 90f;
                m_cellIndicator.FindComponentInChildWithTag<Transform>("robot").localEulerAngles = dummy;
            }
        }

        public void InstantiateRobot()
        {
            GameObject.Instantiate(m_cellIndicator, m_cellIndicator.transform.position, Quaternion.identity);
        }

        public void SetEnable(bool enable)
        {
            this.enable = enable;
        }

        private void StartPlacement(int ID)
        {
            m_selectedObjectIndex = m_database.RobotDatas.FindIndex(data => data.ID == ID);
            if (m_selectedObjectIndex < 0)
            {
                Debug.Log($"No ID found {ID}");
                return;
            }
            
        }
    }
}