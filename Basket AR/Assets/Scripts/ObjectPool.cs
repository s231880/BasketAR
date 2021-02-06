using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The obj pool is a child of the Transform parent passed into the CreatePool function.
/// It's divided in two transform, in the first one there are the disabled ball (m_PoolTransform), in the second one the
/// active one (m_ActiveObjTransform). Once a ball is active it;s changed of parent, and then it goes back once disabled.
/// The value of COUNT it is just a random number that looked reasonable to me, but we could change it in future 
/// </summary>

namespace BBAR
{
    public class ObjectPool : MonoBehaviour
    {
        private const int COUNT = 10;                                        
        private Transform m_ActiveObjTransform;
        private Transform m_PoolTransform;

        public void CreatePool(GameObject cachedObj, Transform  parent)
        {
            GameObject pool = new GameObject("Pool");
            GameObject activePool = new GameObject("ActivePool");
            pool.transform.SetParent(parent);
            activePool.transform.SetParent(parent);
            m_ActiveObjTransform = activePool.transform;
            m_PoolTransform = pool.transform;

            for (int i = 0; i < COUNT; ++i)
            {
                var obj = Object.Instantiate(cachedObj);
                obj.name = cachedObj.name + i;
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.transform.SetParent(m_PoolTransform);
                obj.SetActive(false);
            }
        }
        
        public GameObject GetObject()
        {
            if (m_PoolTransform.childCount != 0)
            {
                GameObject obj = m_PoolTransform.GetChild(0).gameObject;
                obj.transform.SetParent(m_ActiveObjTransform);
                obj.SetActive(true);
                return obj;
            }
            else
                return null;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.transform.SetParent(m_PoolTransform);
            obj.SetActive(false);
        }
    }
}
