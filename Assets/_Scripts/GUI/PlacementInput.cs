using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Telepatia.GridSystem
{
    public class PlacementInput : MonoBehaviour
    {
        [field:SerializeField] private Camera m_sceneCamera;
        [field:SerializeField] private LayerMask m_placementLayerMask;
        public GameObject HitObject, LastHitObject;
        public event Action OnClicked, OnExit;
        private Vector3 m_lastPosition;

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClicked?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnExit?.Invoke();
            }
        }

        private bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = m_sceneCamera.nearClipPlane;
            Ray ray = m_sceneCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, m_placementLayerMask))
            {
                m_lastPosition = hit.point;
                HitObject = hit.transform.gameObject;
                HitObject.GetComponentInParent<PlacementGrid>().OnMouseHover();
                if (LastHitObject != HitObject || LastHitObject == null)
                {
                    try {
                    LastHitObject.GetComponentInParent<PlacementGrid>().OnMouseExit();
                    }
                    catch{}
                    LastHitObject = HitObject;
                }
            }
            return m_lastPosition;
        }
    }
}