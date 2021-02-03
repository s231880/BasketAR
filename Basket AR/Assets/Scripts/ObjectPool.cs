using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{
    public class ObjectPool : MonoBehaviour
    {
        //private List<GameObject> m_Pool = new List<GameObject>();
        private const int COUNT = 10; //temporary number  
        private Transform m_ActiveObjParent;
        private Transform m_PoolParent;

        public void CreatePool(GameObject cachedObj, Transform poolParent, Transform activeParent)
        {
            m_ActiveObjParent = activeParent;
            m_PoolParent = poolParent;

            for (int i = 0; i < COUNT; ++i)
            {
                var obj = Object.Instantiate(cachedObj);
                obj.name = cachedObj.name + i;
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.SetParent(m_PoolParent);
                obj.SetActive(false);
            }
        }
        
        public GameObject GetObject()
        {
            if (m_PoolParent.childCount != 0)
            {
                GameObject obj = m_PoolParent.GetChild(0).gameObject;
                obj.transform.SetParent(m_ActiveObjParent);
                obj.SetActive(true);
                return obj;
            }
            else
                return null;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.transform.SetParent(m_PoolParent);
            obj.SetActive(false);
        }



    }
}
