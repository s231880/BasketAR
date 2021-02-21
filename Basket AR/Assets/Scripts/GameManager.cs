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
        public UIManager m_UIManager;
        private InputManager m_InputManager;
        public BasketManager m_BasketManager;

        private ObjectPool m_Pool = new ObjectPool();
        public GameObject m_ActiveBall;

        public bool m_IsTheBasketPlaced = false;

        //-----------------------------------------------------------------------
        //AR variables

        private ARPlaneManager m_PlaneManager;
        

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

            m_InputManager = this.gameObject.transform.Find("AR Session Origin").gameObject.AddComponent<InputManager>();
            m_InputManager.Initialise();

            m_BasketManager = this.gameObject.AddComponent<BasketManager>();
            m_BasketManager.Initialise();

            ARVariablesInitialisation();
            //-----------------------------------------------------------------------
            //Obj Pool creation 
            GameObject ball = Resources.Load<GameObject>("Ball");  // Loading the ball prefab
            CreateObjPool(ball);                                   // Create the pool
            m_State = GameState.Started;                           // Start the game
        }

        private void ARVariablesInitialisation()
        {
            m_PlaneManager = gameObject.transform.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            m_PlaneManager.planesChanged += PlaneStateChanged;      //Adding event when plan is detected
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
        //Getting and returning ball to the pool => Probably these functions should be moved into Ball.cs, what do you think Brad?
        public void ActivateBall()
        {
            m_ActiveBall = m_Pool.GetObject();
            m_ActiveBall.transform.position = Camera.main.transform.position + Camera.main.transform.forward; 
        }

        public void ReturnBallTothePool(GameObject thrownBall)
        {
            m_Pool.ReturnObject(thrownBall);
        }
        //-----------------------------------------------------------------------
        //Throw the ball
        public void ThrowActiveBall(Vector2 startingPos, Vector2 finalPos)
        {
            float differenceY = (startingPos.y - finalPos.y) / Screen.height * 100;

            float throwSpeed = 2f; //Random value
            // I think we should use as speed the difference between when the user has pressed the screen and when has release it
            float speed = throwSpeed * differenceY;

            float x = (startingPos.x / Screen.width) - (finalPos.x / Screen.width);

            x = Mathf.Abs(Input.mousePosition.x - finalPos.x) / Screen.width * 100 * x;

            Vector3 direction = new Vector3(x, 0f, 1f);
            direction = Camera.main.transform.TransformDirection(direction);
            //Vector3 direction = finalPos - startingPos;

            m_ActiveBall.GetComponent<Ball>().ApplyForce(direction, speed);

        }
        //-----------------------------------------------------------------------
        //Function that detects when plane state change => the state can be: Added, Updated, Removed
        //Right now the placing of the basket does not work properly so I'll keep it, in future coudl be removed if we don't find a purpose
        private void PlaneStateChanged(ARPlanesChangedEventArgs arg)
        {
            //Enabeling input when a plane has been detected
            if (arg.added != null)
            {
                m_InputManager.m_ThereIsAnActivePlane = true;
            }
        }
    }
}

