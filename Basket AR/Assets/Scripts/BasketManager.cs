using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{
    public class BasketManager : MonoBehaviour
    {
        private GameObject m_BasketPrefab;

        public void Initialise()
        {
            m_BasketPrefab = Resources.Load<GameObject>("Basket");       // Loading the basket prefab
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaceBasket(Vector3 position)
        {
            var basket = GameObject.Instantiate(m_BasketPrefab ,position, m_BasketPrefab.transform.rotation);
            Debug.LogError($"{position} Basket position");
        }
    }
}
