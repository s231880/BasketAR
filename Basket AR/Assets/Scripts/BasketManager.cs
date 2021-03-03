using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{
    public class BasketManager : MonoBehaviour
    {
        private GameObject m_BasketPrefab;
        private GameObject m_ActiveBasket;

        public void Initialise()
        {
            m_BasketPrefab = Resources.Load<GameObject>("Basket");
        }

        public void PlaceTheBasket(Vector3 position, Quaternion rotation)
        {
            m_ActiveBasket = GameObject.Instantiate(m_BasketPrefab , position, m_BasketPrefab.transform.rotation);       //Instantiate the basket
            m_ActiveBasket.transform.LookAt(Camera.main.transform);
            m_ActiveBasket.transform.SetParent(this.transform);
        }

        public void DeleteBasket()
        {
            Destroy(m_ActiveBasket);
            m_ActiveBasket = null;
        }
    }
}
