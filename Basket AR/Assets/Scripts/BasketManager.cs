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
            m_BasketPrefab = Resources.Load<GameObject>("Basket");       // Loading the basket prefab
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaceTheBasket(Vector3 position, Quaternion rotation)
        {
            m_ActiveBasket = GameObject.Instantiate(m_BasketPrefab ,position, m_BasketPrefab.transform.rotation);   //Instantiate the basket
            m_ActiveBasket.transform.position += (Vector3.back * 0.1f);                                             //Place it in front of the plane
            m_ActiveBasket.transform.rotation.eulerAngles.Set(90,180, rotation.eulerAngles.z);                      //Rotate it => Temporary due tyo wrong asset transform
        }

        public void MoveTheBasket(Vector3 position)
        {
            m_ActiveBasket.transform.position = position;
            m_ActiveBasket.transform.position += (Vector3.back * 0.1f);
        }

        public void DeleteBasket()
        {
            Destroy(m_ActiveBasket);
            m_ActiveBasket = null;
        }
    }
}
