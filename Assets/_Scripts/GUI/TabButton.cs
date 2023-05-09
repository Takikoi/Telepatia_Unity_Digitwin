using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Telepatia.GUI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [field:SerializeField] public TabGroup TabGroup {get; private set;}
        [field:SerializeField] public Image background {get; private set;}
        [field:SerializeField] public UnityEvent OnTabSelected {get; private set;}
        [field:SerializeField] public UnityEvent OnTabDeselected {get; private set;}

        void Start() 
        {
            background = GetComponent<Image>();
            TabGroup.Subscribe(this);   
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TabGroup.OnTabHover(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TabGroup.OnTabExit(this);
        }

        public void Select()
        {
            OnTabSelected?.Invoke();
        }

        public void Deselect()
        {
            OnTabDeselected?.Invoke();
        }
    }
}