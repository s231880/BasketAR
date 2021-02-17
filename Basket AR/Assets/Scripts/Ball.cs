using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{

    public class Ball : MonoBehaviour
    {
        private Rigidbody m_RigidBody;

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();
        }

        public void ApplyForce(Vector3 direction, float speed)
        {
            m_RigidBody.useGravity = true;
            m_RigidBody.AddForce((direction * speed / 2f) + (Vector3.up * speed));

            Invoke("ResetBall", 3.0f);
        }

        public void ResetBall()
        {
            m_RigidBody.useGravity = false;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.angularVelocity = Vector3.zero;
            gameObject.transform.rotation = Quaternion.Euler(0f, 200f, 0f);
            gameObject.transform.position = Vector3.zero;
            GameManager.Instance.ReturnBallTothePool(gameObject);
        }
    }
}