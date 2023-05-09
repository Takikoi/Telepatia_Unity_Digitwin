using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Telepatia.GUI
{
    public interface IUIHoverArea
    {
        public void OnPointerOverArea();
        public void OnPointerExitArea();
    }
    public class UIHoverArea : MonoBehaviour, IUIHoverArea
    {
        [SerializeField] private UnityEvent m_onPointerOverArea, m_onPointerExitArea;

        void Start()
        {
        }

        void Update()
        {
            if (IsPointerOverUIElement())
            {
                m_onPointerOverArea?.Invoke();
            }
            else
            {
                m_onPointerExitArea?.Invoke();
            }
        }

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

        public void OnPointerOverArea()
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerExitArea()
        {
            throw new System.NotImplementedException();
        }
    }
}
