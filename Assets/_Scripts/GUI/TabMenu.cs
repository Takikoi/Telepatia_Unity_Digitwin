using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    [SerializeField] private Animator m_anim;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTabMenu()
    {
        m_anim.SetBool("IsTabMenuOpen", true);
    }

    public void CloseTabMenu()
    {
        m_anim.SetBool("IsTabMenuOpen", false);
    }
}
