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
            basket.transform.LookAt(Camera.main.transform);

            Vector3 basketRot = basket.transform.rotation.eulerAngles;
            basketRot.x = 90;
            basket.transform.rotation.eulerAngles.Set(90, basket.transform.rotation.eulerAngles.y, basket.transform.rotation.eulerAngles.z); 
        }
    }
}
