using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{    
     ///<summary>
     /// The GameState enum will keep track of the state of the game, and will inform the manager when the state is changed
     /// </summary>
    public enum GameState
    {
        Started, // => Prepare to the game, activate 
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

        private GameObject m_ActiveBall;
        private GameObject m_Basket;

        private GameState m_State;
        public GameState m_state
        {
            get { return m_State; }
            set
            {
                m_State = value;
                NotifyManagers();
            }
        }

        void Awake()
        {
            //-----------------------------------------------------------------------
            //Variables & Managers Initialisation
            Instance = this;

            m_UIManager = this.gameObject.AddComponent<UIManager>();
            m_UIManager.Initialise();

            m_InputManager = this.gameObject.AddComponent<InputManager>();
            m_InputManager.Initialise();
            //-----------------------------------------------------------------------
            //Obj Pool creation 
            GameObject ball = Resources.Load<GameObject>("Ball");  // Loading the ball prefab
            m_Basket = Resources.Load<GameObject>("Basket");       // Loading the basket prefab

            CreateObjPool(ball);                                   // Create the pool
            m_State = GameState.Started;                           // Start the game


            //I'm writing this line into Tiziano's Branch

            //I'm writing this line into Bradley's branch

        }

        void Update()
        {
        
        }

        private void CreateObjPool(GameObject ball)
        {
            GameObject ballsPool = new GameObject("BallsPool");    // Pool transform creation
            ballsPool.transform.SetParent(this.transform);         // Setting this gameobject as parent of the pool
            m_Pool.CreatePool(ball, ballsPool.transform);          // Initialise the pool
        }

        //-----------------------------------------------------------------------
        //Change game state
        private void NotifyManagers()
        {
            switch (m_State)
            {
                case GameState.Started: // The UI menu should is showned
                    break;
                case GameState.Playing: // Actual game starts and the user is playing
                    break;
                case GameState.Ended:   // The game is ended, the user has to decide between play again and go fuck himself
                    break;

            }
        }
        //-----------------------------------------------------------------------
        //Getting and returning ball to the pool
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
        //-----------------------------------------------------------------------
    }
}

