using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BBAR
{
    public class UIManager : MonoBehaviour
    {
        //-----------------------------------------------------------------------
        //GUI variables
        private CanvasGroup m_GUI;
        private TextMeshProUGUI m_MessageLabel;
        private TextMeshProUGUI m_ScoreLabel;
        private TextMeshProUGUI m_TimeLabel;
        //-----------------------------------------------------------------------
        //StartScreen variables
        private CanvasGroup m_MainMenu;
        private CanvasGroup m_StartScreen;
        private Button m_PlayBtn;
        private Button m_ExitBtn;
        //-----------------------------------------------------------------------
        //EndScreen variables
        private CanvasGroup m_EndMenu;
        private Button m_RestartBtn;
        private Button m_QuitBtn;
        private Button m_MainMenuBtn;
        private Text m_FinalScoreText;
        private Text m_MaxScoreText;
        //-----------------------------------------------------------------------
        //Countdown variables
        private CanvasGroup m_CountDown;
        private Transform[] m_Numbers = new Transform[4];
        //-----------------------------------------------------------------------
        //Tutorial variables
        private CanvasGroup m_TutorialCanvasGroup;
        private CanvasGroup m_ScanTutorial;
        private CanvasGroup m_TapTutorial;

        //-----------------------------------------------------------------------
        //Initialise functions
        public void Initialise()
        {
            InitialiseGUI();
            InitialiseMainMenu();
            InitialiseEndMenu();
            InitialiseCoundown();
            InitialiseTutorials();
        }

        private void InitialiseMainMenu()
        {
            m_MainMenu  = this.transform.Find("MainMenu").GetComponent<CanvasGroup>();
            m_StartScreen = this.transform.Find("StartScreen").GetComponent<CanvasGroup>();

            m_PlayBtn = m_MainMenu.transform.Find("Buttons").transform.Find("PlayBtn").GetComponent<Button>();
            m_PlayBtn.onClick.AddListener(() => PlayClicked(GameManager.Instance.ReadyToPlay));
            m_ExitBtn = m_MainMenu.transform.Find("Buttons").transform.Find("ExitBtn").GetComponent<Button>();
            m_ExitBtn.onClick.AddListener(() => GameManager.Instance.m_state = GameState.Exit);
        }

        private void InitialiseGUI()
        {
            m_GUI = this.transform.Find("GUI").GetComponent<CanvasGroup>();
            m_MessageLabel = this.transform.Find("GUI/DebugMessage").GetComponent<TextMeshProUGUI>();
            m_ScoreLabel = this.transform.Find("GUI/ScoreLabel").GetComponent<TextMeshProUGUI>();
            m_TimeLabel = this.transform.Find("GUI/TimeLabel").GetComponent<TextMeshProUGUI>();
        }

        private void InitialiseEndMenu()
        {
            m_EndMenu = this.transform.Find("EndMenu").GetComponent<CanvasGroup>();
            m_FinalScoreText = m_EndMenu.transform.Find("FinalScore").GetComponent<Text>();
            m_MaxScoreText = m_EndMenu.transform.Find("MaxScore").GetComponent<Text>();

            m_RestartBtn = m_EndMenu.transform.Find("Buttons/PlayBtn").GetComponent<Button>();
            m_RestartBtn.onClick.AddListener(() => Restart(GameManager.Instance.ReadyToPlay));

            m_QuitBtn = m_EndMenu.transform.Find("Buttons/ExitBtn").GetComponent<Button>();
            m_QuitBtn.onClick.AddListener(() => GameManager.Instance.m_state = GameState.Exit);

            m_MainMenuBtn = m_EndMenu.transform.Find("Buttons/MainMenuBtn").GetComponent<Button>();
            m_MainMenuBtn.onClick.AddListener(() => GoBackToMainMenu());
        }

        private void InitialiseCoundown()
        {
            m_CountDown = this.transform.Find("CoundDown").GetComponent<CanvasGroup>();
            Transform numbers = m_CountDown.transform;
            int count = numbers.childCount;

            for(int i = 0; i < count; ++i)
            {
                m_Numbers[i] = numbers.GetChild(i);
                m_Numbers[i].gameObject.SetActive(false);
            }

            m_CountDown.alpha = 0;
            m_CountDown.blocksRaycasts = false;
            m_CountDown.interactable = false;
        }

        private void InitialiseTutorials()
        {
            m_TutorialCanvasGroup = this.transform.Find("Tutorial").GetComponent<CanvasGroup>();
            m_ScanTutorial = this.transform.Find("Tutorial/ScanTutorial").GetComponent<CanvasGroup>();
            m_TapTutorial = this.transform.Find("Tutorial/TapTutorial").GetComponent<CanvasGroup>();
        }

        //-----------------------------------------------------------------------
        //Show and Hide UI functions
        public IEnumerator ShowStartScreen()
        {
            m_StartScreen.alpha = 1;
            m_StartScreen.blocksRaycasts = true;
            yield return new WaitForSeconds(3.5f);       //Show the start screen for 3 seconds => Random value, to be changed
            m_StartScreen.blocksRaycasts = false;
            m_StartScreen.alpha = 0;
            ShowMainMenu(true);
        }

        private void ShowMainMenu(bool state)
        {
            m_MainMenu.blocksRaycasts = state;
            m_MainMenu.interactable = state;
            m_MainMenu.alpha = (state) ? 1 : 0;
        }

        public void ShowGameGUI(bool state)
        {
            m_GUI.alpha = (state) ? 1 : 0;
        }

        public void ShowEndScreen(bool state)
        {
            m_EndMenu.alpha = (state) ? 1f : 0f;
            m_EndMenu.blocksRaycasts = state;
            m_EndMenu.interactable = state;
            if (state)
            {
                m_FinalScoreText.text = $"{GameManager.Instance.m_Score}";
                m_MaxScoreText.text = $"YOUR MAX SCORE IS: {GameManager.Instance.m_MaxScore}";
            }
        }

        public void EnableTutorialCanvas(bool state)
        {
            m_TutorialCanvasGroup.alpha = state ? 1 : 0;
            if (state)
                ShowTutorial("AreaScan");
        }

        public void ShowTutorial(string tutorial)
        {
            switch(tutorial)
            {
                case "AreaScan":
                    m_ScanTutorial.alpha = 1;
                    m_TapTutorial.alpha = 0;
                    break;
                case "HoopPlacement":
                    m_ScanTutorial.alpha = 0;
                    m_TapTutorial.alpha = 1;
                    break;
            }
        }

        public void ShowCountDown(int count = 3)
        {
            m_CountDown.alpha = 1;
            Transform num = m_Numbers[count];
            num.gameObject.SetActive(true);
            this.Create<ValueTween>(1f, EaseType.Linear, () =>
            {
                num.localScale = Vector3.one;
                num.gameObject.SetActive(false);
                if (count != 0)
                {
                    ShowCountDown(--count);
                }
                else
                {
                    GameManager.Instance.m_state = GameState.Play;
                }
            }).Initialise(1, 0, (f) =>
            {
                m_CountDown.alpha = f;
                num.localScale = Vector3.one * f;
            });
        }
        //-----------------------------------------------------------------------
        //Update UI functions
        public void SetScore(int score)
        {
            m_ScoreLabel.text = $"{score}";
        }

        public void SetLabelTest(string message)
        {
            m_MessageLabel.text = message;
        }

        public void SetTimer(float time)
        {
            m_TimeLabel.text = time.ToString();
        }
        
        //-----------------------------------------------------------------------
        //UI Interactions
        private void PlayClicked(Action callback)
        {
            ShowMainMenu(false);
            ShowGameGUI(true);
            callback?.Invoke();
        }

        private void GoBackToMainMenu()
        {
            ShowEndScreen(false);
            ShowMainMenu(true);
        }

        private void Restart(Action callback)
        {
            GameManager.Instance.ResetVariables();
            ShowEndScreen(false);
            callback?.Invoke();
        }
    }
}