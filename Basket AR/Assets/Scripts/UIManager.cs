using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BBAR
{
    public class UIManager : MonoBehaviour
    {
       private int m_Score = 0;
        //For testing purpose only
       [SerializeField]private TextMeshProUGUI m_MessageLabel; 

       public void Initialise()
       {
            m_MessageLabel = this.transform.Find("CanvasMessage").GetComponentInChildren<TextMeshProUGUI>();
       }

        public void ShowStartScreen(bool state) { }

        public void ShowGUI(bool state) { }

        public void ShowEndScreen(bool state) { }

        public void SetScore(int score)
        {
            m_Score = score;
        }

        public void SetLabelTest(string message)
        {
            m_MessageLabel.text = message;
        }
    }
}
