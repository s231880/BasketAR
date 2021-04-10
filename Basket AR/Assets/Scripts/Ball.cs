using UnityEngine;

namespace BBAR
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody m_RigidBody;
        private ParticleSystem m_Fire;

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_Fire = gameObject.GetComponentInChildren<ParticleSystem>();
            //EnableFire(true);
        }

        public void ApplyForce(Vector3 direction, float speed, float timeDiff = 1f)
        {
            m_RigidBody.useGravity = true;
            m_RigidBody.AddForce((direction * speed / 2f) + ((Vector3.up * speed * -1) / timeDiff ));
            //m_RigidBody.AddForce((direction.x * speed / 2f), (direction.y * speed/2f), (1 * speed * -1) / timeDiff );

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

        public void EnableFire(bool state)
        {
            if (state)
                m_Fire.Play();
            else
                m_Fire.Stop();
        }
    }
}