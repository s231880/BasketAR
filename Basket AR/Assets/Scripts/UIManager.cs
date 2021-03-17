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
        //Initialise functions
        public void Initialise()
        {
            InitialiseGUI();
            InitialiseMainMenu();
        }

        private void InitialiseMainMenu()
        {
            m_MainMenu  = this.transform.Find("MainMenu").GetComponent<CanvasGroup>();
            m_StartScreen = this.transform.Find("StartScreen").GetComponent<CanvasGroup>();

            m_PlayBtn = m_MainMenu.transform.Find("Buttons").transform.Find("PlayBtn").GetComponent<Button>();
            m_PlayBtn.onClick.AddListener(() => PlayClicked());
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
        //-----------------------------------------------------------------------
        //Show and Hide UI functions
        public IEnumerator ShowStartScreen()
        {
            yield return new WaitForSeconds(3);       //Show the start screen for 3 seconds => Random value, to be changed
            this.Create<ValueTween>(1.5f, EaseType.Linear, () =>
            {
                m_StartScreen.blocksRaycasts = false;
                ShowMainMenu(true) ;                                       //Callback: enable the main menu view
            }).Initialise(1f, 0f, (f) => 
            {
                m_StartScreen.alpha = f;               //Linear decrease the alpha 
            });
        }

        private void ShowMainMenu(bool state)
        {
            float startVal = (state) ? 0f : 1f;
            float endVal = (state) ? 1f : 0f;
            this.Create<ValueTween>(1.5f, EaseType.Linear, () =>
            {
                m_MainMenu.blocksRaycasts = state;
                m_MainMenu.interactable = state;
            }).Initialise(startVal, endVal, (f) => 
            {
                m_MainMenu.alpha = f;
            });
        }

        public void ShowGameGUI(bool state)
        {
            m_GUI.alpha = (state) ? 1 : 0;
        }

        public void ShowEndScreen(bool state)
        {
        }


        public void ShowTutorialCanvas()
        {
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
        private void PlayClicked()
        {
            ShowMainMenu(false);
            ShowGameGUI(true);
        }

    }
}