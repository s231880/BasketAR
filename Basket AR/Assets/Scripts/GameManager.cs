using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

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
        private BasketManager m_BasketManager;

        private bool m_BasketBeenPlaced = false;
        private ObjectPool m_BallsPool = new ObjectPool();
        public GameObject m_ActiveBall;

        GameObject dialog = null;
        private static int m_Score = 0;
        private static int m_Timer;
        private const int FULL_TIMER = 30;
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


#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
                dialog = new GameObject();
            }
#endif
            m_UIManager = gameObject.transform.Find("UIManager").gameObject.AddComponent<UIManager>();
            m_UIManager.Initialise();

            m_InputManager = gameObject.transform.Find("AR Session Origin").gameObject.AddComponent<InputManager>();
            m_InputManager.Initialise();

            m_BasketManager = gameObject.AddComponent<BasketManager>();
            m_BasketManager.Initialise();

            ARVariablesInitialisation();
            //-----------------------------------------------------------------------
            //Obj Pool creation 
            GameObject ball = Resources.Load<GameObject>("FlameBall");  // Loading the ball prefab
            CreateObjPool(ball);                                        // Create the pool
            m_state = GameState.Started;                                // Start the game
        }

        private void ARVariablesInitialisation()
        {
            m_PlaneManager = gameObject.transform.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            m_PlaneManager.planesChanged += PlaneStateChanged;      //Adding event when plan is detected
            m_UIManager.SetLabelTest("AR Initted");
        }

        private void CreateObjPool(GameObject ball)
        {
            GameObject ballsPool = new GameObject("BallsPool");    // Pool transform creation
            ballsPool.transform.SetParent(this.transform);         // Setting this gameobject as parent of the pool
            m_BallsPool.CreatePool(ball, ballsPool.transform);          // Initialise the pool
        }

        public void PlaceTheBasket(Vector3 position, Quaternion rotation)
        {
            m_BasketManager.PlaceTheBasket(position, rotation);
            m_BasketBeenPlaced = true;
            m_state = GameState.Playing;
            Debug.LogError("PlaceTheBasket");
        }

        //-----------------------------------------------------------------------
        //Change game state
        private void NotifyManagers()
        {
            switch (m_State)
            {
                case GameState.Started:                     // The UI menu should is showned
                    //m_UIManager.ShowStartScreen();
                    break;
                case GameState.Playing:                     //The basket has been placed, the game can start
                    Debug.LogError("PLAYING");
                    ResetScoreAndTimer();                   //Set timer and score
                    StartCoroutine(Startimer());            //Starts the timer
                    ActivateBall();                         //Activate the first ball
                    m_BasketManager.EnableScoreArea(true);  //Enable the score area
                    break;
                case GameState.Ended:   // The game is ended, the user has to decide between play again and go fuck himself
                    m_BasketManager.EnableScoreArea(false);  
                    m_UIManager.ShowEndScreen(true);
                    break;

            }
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
        public void ThrowActiveBall(Vector2 startingPos, Vector2 finalPos)
        {
            float differenceY = (startingPos.y - finalPos.y) / Screen.height * 100;

            float throwSpeed = 3f; //Random value
            // I think we should use as speed the difference between when the user has pressed the screen and when has release it
            float speed = throwSpeed * differenceY;

            float x = (startingPos.x / Screen.width) - (finalPos.x / Screen.width);

            x = Mathf.Abs(Input.mousePosition.x - finalPos.x) / Screen.width * 100 * x;

            Vector3 direction = new Vector3(x, 0f, -1f);
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
        void OnGUI()
        {
#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                // The user denied permission to use the microphone.
                // Display a message explaining why you need it with Yes/No buttons.
                // If the user says yes then present the request again
                // Display a dialog here.
                dialog.AddComponent<PermissionsRationaleDialog>();
                return;
            }
            else if (dialog != null)
            {
                Destroy(dialog);
            }
#endif

            // Now you can do things with the microphone
        }

        //-----------------------------------------------------------------------
        //Score and timer functions
        public void UpdateScored()
        {
            m_Score += 1;
            m_UIManager.SetScore(m_Score);
        }

        private IEnumerator Startimer()
        {
            while (m_Timer != 0)
            {
                --m_Timer;
                m_UIManager.SetTimer(m_Timer);
                yield return new WaitForSeconds(1);
            }

            m_state = GameState.Ended; 
        }

        private void ResetScoreAndTimer()
        {
            Debug.LogError("ResetScoreAndTimer");
            m_Score = 0;
            m_UIManager.SetScore(m_Score);

            m_Timer = FULL_TIMER;
            m_UIManager.SetTimer(m_Timer);
        }
        //-----------------------------------------------------------------------
    }
}


