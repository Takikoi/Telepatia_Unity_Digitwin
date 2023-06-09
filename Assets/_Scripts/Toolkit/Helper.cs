using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Telepatia.Toolkit
{
    public static class Helper
    {
        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag)where T:Component
        {
            Transform t = parent.transform;
            foreach(Transform tr in t)
            {
                if(tr.tag == tag)
                {
                    return tr.GetComponent<T>();
                }
            }
            return null;
        }
        public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
        {
            if(parent == null) { throw new System.ArgumentNullException(); }
            if(string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
            List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
            if(list.Count == 0) { return null; }

            for(int i = list.Count - 1; i >= 0; i--) 
            {
                if (list[i].CompareTag(tag) == false)
                {
                    list.RemoveAt(i);
                }
            }
            return list.ToArray();
        }

        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
        {
            if (parent == null) { throw new System.ArgumentNullException(); }
            if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

            T [] list = parent.GetComponentsInChildren<T>(forceActive);
            foreach(T t in list)
            {
                if (t.CompareTag(tag) == true)
                {
                    return t;
                }
            }
            return null;
        }
    }
}