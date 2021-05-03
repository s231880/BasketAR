using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

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
        Start,
        SetUp,
        Ready,
        Play,
        End,
        Exit
    }

    public class GameManager : MonoBehaviour
    {
        //-----------------------------------------------------------------------
        //Application variables
        public static GameManager Instance;

        public UIManager m_UIManager;
        private InputManager m_InputManager;
        private BasketManager m_BasketManager;
        private AudioManager m_AudioManager;
        private BallManager m_BallManager;

        private bool m_BasketBeenPlaced = false; 

        private GameObject dialog = null;
        private static int m_Score = 0;
        private static int m_Timer;
        private const int FULL_TIMER = 10; //60

        private Dictionary<string, ParticleSystem> m_ConfettiDictionary = new Dictionary<string, ParticleSystem>();
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

        private void Awake()
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

            m_AudioManager = gameObject.transform.Find("AR Session Origin/AR Camera").gameObject.AddComponent<AudioManager>();
            m_AudioManager.Initialise();

            m_BallManager = gameObject.AddComponent<BallManager>();
            m_BallManager.Initialise();

            ARVariablesInitialisation();
            //InitialiseConfetti();
            m_state = GameState.Start;                                  // Start the game
        }

        private void ARVariablesInitialisation()
        {
            m_PlaneManager = gameObject.transform.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            m_PlaneManager.planesChanged += PlaneStateChanged;      //Adding event when plan is detected
            EnablePlaneManager(false);
        }

        public void PlaceTheBasket(Vector3 position, Quaternion rotation)
        {
            m_BasketManager.PlaceTheBasket(position, rotation);
            m_BasketBeenPlaced = true;
            m_state = GameState.Ready;
        }

        private void InitialiseConfetti()
        {
            GameObject confetti = Camera.main.gameObject.transform.Find("ConfettiParticleSystems").gameObject;
            foreach(Transform system in confetti.transform)
                m_ConfettiDictionary.Add(system.name,system.gameObject.GetComponent<ParticleSystem>());
        }

        //-----------------------------------------------------------------------
        //Change game state
        private void NotifyManagers()
        {
            switch (m_State)
            {
                case GameState.Start:                     // The UI menu should is showned
                    StartGame();
                    break;
                case GameState.SetUp:
                    StartCoroutine(SetUpMatch());
                    break;
                case GameState.Ready:
                    m_UIManager.ShowCountDown();
                    m_AudioManager.PlayCountdown();
                    break;
                case GameState.Play:                     //The basket has been placed, the game can start
                    PlayMatch();
                    break;
                case GameState.End:                     // The game is ended, the user has to decide between play again and go fuck himself
                    StartCoroutine(EndMatch());
                    break;
                case GameState.Exit:
                    CloseApplication();
                    break;
            }
        }
        
        //-----------------------------------------------------------------------
        //Handle Game parts

        private void StartGame()
        {
            StartCoroutine(m_UIManager.ShowStartScreen());
        }

        private IEnumerator SetUpMatch()
        {
#if !UNITY_EDITOR
            m_UIManager.EnableTutorialCanvas(true);
            yield return new WaitForSeconds(1.5f);
#else
            yield return new WaitForSeconds(0);
#endif
            EnablePlaneManager(true);
        }

        private void PlayMatch()
        {
            ResetScoreAndTimer();                   //Set timer and score
            m_AudioManager.PlayCheering();
            StartCoroutine(Startimer());            //Starts the timer
            m_BallManager.ActivateBall();           //Activate the first ball
            m_BasketManager.EnableScoreArea(true);  //Enable the score area
        }

        private IEnumerator EndMatch()
        {
            m_BasketManager.EnableScoreArea(false);
            PlayConfetti(true);
            PlayEndGameSounds();
            yield return new WaitForSeconds(3);
            m_UIManager.ShowEndScreen(true, m_Score);
        }
        private void CloseApplication()
        {
            Application.Quit();
        }

        //-----------------------------------------------------------------------
        //Function that detects when plane state change => the state can be: Added, Updated, Removed
        //Right now the placing of the basket does not work properly so I'll keep it, in future coudl be removed if we don't find a purpose
        private void PlaneStateChanged(ARPlanesChangedEventArgs arg)
        {
            if(m_PlaneManager.trackables.count >= 3)
            {
                m_UIManager.ShowTutorial("HoopPlacement");
            }

            //if (arg.added != null)
            //{
            //    foreach(var plane in m_PlaneManager.trackables)
            //    {
            //        if (plane.transform.position.y != 0)
            //            Destroy(plane);
            //    }
            //}
        }

        private void EnablePlaneManager(bool state)
        {
            m_PlaneManager.enabled = state;
        }

        //-----------------------------------------------------------------------
        //CAN THIS FUNCTION BE MOVED BRAD?
        private void OnGUI()
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

        public void NotifyInput(Vector2 startingPos, Vector2 finalPos, float timeStart, float timeEnd)
        {
            m_BallManager.ThrowActiveBall(startingPos, finalPos, timeStart, timeEnd);
            m_BallManager.ActivateBall();
        }

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
                if (m_Timer == 0)
                    m_AudioManager.PlayHorn();
                yield return new WaitForSeconds(1);
            }
            m_state = GameState.End;
        }

        private void ResetScoreAndTimer()
        {
            m_Score = 0;
            m_UIManager.SetScore(m_Score);

            m_Timer = FULL_TIMER;
            m_UIManager.SetTimer(m_Timer);
        }

        //-----------------------------------------------------------------------

        public void ReadyToPlay()
        {
            switch  (m_BasketBeenPlaced)
            {
                case false:
                    m_state = GameState.SetUp;
                    break;
                case true:
                    m_state = GameState.Ready;
                    break;
            }
        }

        public void ResetVariables()
        {
            PlayConfetti(false);
            m_BallManager.ResetBall();
        }

        //-----------------------------------------------------------------------
        private void PlayConfetti(bool state)
        {
            if (state)
            {
                m_AudioManager.PlayConfettiPop();
                foreach (string key in m_ConfettiDictionary.Keys)
                  m_ConfettiDictionary[key].Play();
            }
            else
            {
                foreach (string key in m_ConfettiDictionary.Keys)
                {
                    m_ConfettiDictionary[key].Stop();
                }
            }

        }

        public void PlayEndGameSounds()
        {
            if(m_Score < 5)
                m_AudioManager.PlayBooing();
            else
                m_AudioManager.PlayCheering();
        }
    }
}