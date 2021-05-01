using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{
    public class BallManager : MonoBehaviour
    {
        private ObjectPool m_BallsPool = new ObjectPool();
        private GameObject m_ActiveBall;
        public static BallManager Instance;
        public void Initialise()
        {
            Instance = this;
            GameObject ball = Resources.Load<GameObject>("Ball");       // Loading the ball prefab
            CreateObjPool(ball);                                        // Create the pool
        }

        private void CreateObjPool(GameObject ball)
        {
            GameObject ballsPool = new GameObject("BallsPool");         // Pool transform creation
            ballsPool.transform.SetParent(this.transform);              // Setting this gameobject as parent of the pool
            m_BallsPool.CreatePool(ball, ballsPool.transform);          // Initialise the pool
        }

        //-----------------------------------------------------------------------
        //Getting and returning ball to the pool => Probably these functions should be moved into Ball.cs, what do you think Brad?
        public void ActivateBall()
        {
            m_ActiveBall = m_BallsPool.GetObject();
            m_ActiveBall.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 2.5f);
            m_ActiveBall.transform.position -= (m_ActiveBall.transform.up * 0.5f);
        }

        public void ReturnBallTothePool(GameObject thrownBall)
        {
            m_BallsPool.ReturnObject(thrownBall);
        }

        //-----------------------------------------------------------------------
        //Throw the ball
        public void ThrowActiveBall(Vector2 startingPos, Vector2 finalPos, float timeStart, float timeEnd)
        {
            float differenceY = (startingPos.y - finalPos.y) / Screen.height * 100;
            float timeDiff = timeEnd - timeStart;


            float throwSpeed = 2f; //Random value
            // I think we should use as speed the difference between when the user has pressed the screen and when has release it
            //float speed = Mathf.Clamp((throwSpeed * differenceY), 3f, 50f);
            float speed = throwSpeed * differenceY;

            float x = (startingPos.x / Screen.width) - (finalPos.x / Screen.width);

            x = Mathf.Abs(Input.mousePosition.x - finalPos.x) / Screen.width * 100 * x;

            Vector3 direction = new Vector3(x, 0f, -1f);
            direction = Camera.main.transform.TransformDirection(direction);
            //Vector3 direction = finalPos - startingPos;

            m_ActiveBall.GetComponent<Ball>().ApplyForce(direction, speed, timeDiff);
        }
        public void ResetBall()
        {
            if (m_ActiveBall != null)
                m_BallsPool.ReturnObject(m_ActiveBall);
        }
    }
}
