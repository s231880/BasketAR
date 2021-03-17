using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BBAR
{
    public class UIManager : MonoBehaviour
    {
       
        //For testing purpose only
       private TextMeshProUGUI m_MessageLabel; 
       private TextMeshProUGUI m_ScoreLabel;
       private TextMeshProUGUI m_TimeLabel; 

       public void Initialise()
       {
            m_MessageLabel = this.transform.Find("DebugMessage").GetComponent<TextMeshProUGUI>();
            m_ScoreLabel = this.transform.Find("ScoreLabel").GetComponent<TextMeshProUGUI>();
            m_TimeLabel = this.transform.Find("TimeLabel").GetComponent<TextMeshProUGUI>();
        }

        public void ShowStartScreen(bool state) { }

        public void ShowGUI(bool state) { }

        public void ShowEndScreen(bool state) { }

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

        public void ShowTutorialCanvas()
        {

        }
    }
}
