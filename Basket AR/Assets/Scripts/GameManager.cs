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
        Started, // => Prepare to the game, activate 
        Playing,
        Ended
    }

    public class GameManager : MonoBehaviour
    {
        //-----------------------------------------------------------------------
        //Application variables
        public static GameManager Instance;
        private UIManager m_UIManager;
        private InputManager m_InputManager;
        private BasketManager m_BasketManager;

        private ObjectPool m_Pool = new ObjectPool();
        private GameManager m_BallPrefab;
        private GameObject m_ActiveBall;

        private bool m_isTheBasketPlaced = false;

        //-----------------------------------------------------------------------
        //AR variables

        private ARRaycastManager m_ARRaycastManager;
        private Pose m_PlacementPose;

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

            m_BasketManager = this.gameObject.AddComponent<BasketManager>();
            m_BasketManager.Initialise();

            //-----------------------------------------------------------------------
            //Obj Pool creation 
            GameObject ball = Resources.Load<GameObject>("Ball");  // Loading the ball prefab
            CreateObjPool(ball);                                   // Create the pool
            m_State = GameState.Started;                           // Start the game
        }

        void Update()
        {
#if !UNITY_EDITOR
            if (!m_isTheBasketPlaced)
            {
                if (ThereIsAValidPlane())
                {
                    m_BasketManager.PlaceBasket(m_PlacementPose.position);
                    m_isTheBasketPlaced = true;
                }
            }
#else
            if (!m_isTheBasketPlaced)
            {
                Vector3 basketPos = Camera.main.transform.forward * 20;
                transform.InverseTransformDirection(basketPos);
                m_BasketManager.PlaceBasket(basketPos);
                m_isTheBasketPlaced = true;
            }

#endif
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

        private bool ThereIsAValidPlane()
        {
            bool result = false;
            return result;
        }
    }
}

