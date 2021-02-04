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
        private ObjectPool m_Pool = new ObjectPool();
        //-----------------------------------------------------------------------

        [SerializeField]private GameObject m_ActiveBall;

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

            //Obj Pool 
            GameObject ball = Resources.Load<GameObject>("Ball");
            GameObject ballsPool = new GameObject("BallsPool");
            ballsPool.transform.SetParent(this.transform);

            m_Pool.CreatePool(ball, ballsPool.transform);
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

