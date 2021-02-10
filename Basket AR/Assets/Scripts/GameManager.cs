using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace BBAR
{    
     ///<summary>
     /// The GameState enum will keep track of the state of the game, and will inform the manager when the state is changed
     /// </summary>
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

        private ARPlaneManager m_PlaneManager;
        private ARRaycastManager m_RayCastManager;
        //-----------------------------------------------------------------------

        private GameObject m_ActiveBall;
        [SerializeField]private GameObject m_Basket;

        private GameState m_State;
        public GameState State
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

            m_PlaneManager = this.transform.Find("AR Session Origin").GetComponent<ARPlaneManager>();

            //-----------------------------------------------------------------------
            //Loading the prefab 
            GameObject ball = Resources.Load<GameObject>("Ball");  // Loading the ball prefab
            m_Basket = Resources.Load<GameObject>("Basket");       // Loading the basket

            //-----------------------------------------------------------------------
            //Obj Pool creation 
            GameObject ballsPool = new GameObject("BallsPool");    // Pool transform creation
            ballsPool.transform.SetParent(this.transform);         // Setting this gameobject as parent of the pool
            m_Pool = ballsPool.AddComponent<ObjectPool>();
            m_Pool.CreatePool(ball, ballsPool.transform);          // Create the pool
            State = GameState.Started;                             // Start the game
        }

        void Update()
        {
            PlaceBasket();
        }

        //-----------------------------------------------------------------------
        //Change game state
        private void NotifyManagers()
        {
            switch (m_State)
            {
                case GameState.Started: // The UI menu is showned
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

        private void PlaceBasket()
        {
            if(m_PlaneManager.trackables.count != 0)
            {
                var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);    //Find the screen center
                var hitsRaycastManager = new List<ARRaycastHit>();
                m_RayCastManager.Raycast(screenCenter, hitsRaycastManager);
                var placementPose = hitsRaycastManager[0].pose;                         //Get best raycast position
                var basket = Instantiate(m_Basket, placementPose.position, placementPose.rotation);
            }
        }
    }
}

