using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{
    public enum GameState
    {
        Started,
        Playing,
        Ended
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private UIManager m_UIManager;
        private InputManager m_InputManager;
        private ObjectPool m_Pool;
        //-----------------------------------------------------------------------

        private GameObject m_ActiveBall; // temporary

        private GameState m_State
        {
            set { NotifyManagers(); }
        }

        void Awake()
        {
            Instance = this;
            //Managers Initialisation
            m_UIManager = this.gameObject.AddComponent<UIManager>();
            m_UIManager.Initialise();

            m_InputManager = this.gameObject.AddComponent<InputManager>();
            m_InputManager.Initialise();

            //Obj Pool creation
            GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject ballsPool = new GameObject("BallsPool");
            GameObject activeBalls = new GameObject("ActiveBalls");

            m_Pool.CreatePool(ball, ballsPool.transform, activeBalls.transform);
            m_State = GameState.Started;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void NotifyManagers() { }

        public void ActivateBall()
        {
            m_ActiveBall = m_Pool.GetObject();
            m_ActiveBall.transform.position = Vector3.zero;
        }

        public void DisableBall()
        {
            m_Pool.ReturnObject(m_ActiveBall);
            m_ActiveBall = null;
        }
    }
}

