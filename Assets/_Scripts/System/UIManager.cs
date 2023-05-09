using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Telepatia.GUI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Animator m_controlMenuPanelAnim;
        [SerializeField] private Animator m_waypointPanelAnim;
        [SerializeField] private Animator m_placementPanelAnim;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SetWaypointPanel(bool open) => m_waypointPanelAnim.SetBool("IsWaypointPanelOpen", open);
        public void SetPlacementPanel(bool open) => m_placementPanelAnim.SetBool("IsPlacementPanelOpen", open);
        public void SetControlMenuPanel(bool open) => m_controlMenuPanelAnim.SetBool("IsTabMenuOpen", open);

        public bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
    
        //Returns 'true' if we touched or hovering on Unity UI element.
        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.GetInstanceID() == gameObject.GetInstanceID())
                    return true;
            }
            return false;
        }
    
    
        //Gets all event system raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
    }
}