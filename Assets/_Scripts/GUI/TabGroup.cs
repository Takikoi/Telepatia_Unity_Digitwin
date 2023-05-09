using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Telepatia.GUI
{
    public class TabGroup : MonoBehaviour
    {
        [field:SerializeField] public List<TabButton> TabButtons {get; private set;}
        [field:SerializeField] public Sprite TabIdle {get; private set;}
        [field:SerializeField] public Sprite TabHover {get; private set;}
        [field:SerializeField] public Sprite TabActive {get; private set;}
        [field:SerializeField] public List<GameObject> ObjectsToSwap {get; private set;}
        private TabButton m_selectedTab;

        public void Subscribe(TabButton button)
        {
            if (TabButtons == null) TabButtons = new List<TabButton>();
            TabButtons.Add(button);
        }

        public void OnTabHover(TabButton button)
        {
            ResetTabs();
            if (m_selectedTab == null || button != m_selectedTab)
            {
                button.background.sprite = TabHover;
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
            button.background.sprite = TabIdle;
        }

        public void OnTabSelected(TabButton button)
        {
            if (m_selectedTab != null)
            {
                m_selectedTab.Deselect();
            }
            m_selectedTab = button;
            m_selectedTab.Select();
            ResetTabs();
            button.background.sprite = TabActive;
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < ObjectsToSwap.Count; i++)
            {
                ObjectsToSwap[i].SetActive(i == index);
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in TabButtons)
            {
                if (m_selectedTab != null && button == m_selectedTab) {continue;}
                button.background.sprite = TabIdle;
            }
        }
    }
}