using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{
    public class UIManager : MonoBehaviour
    {
       private int m_Score = 0;
       public void Initialise()
       {

       }

        public void ShowStartScreen(bool state) { }

        public void ShowGUI(bool state) { }

        public void ShowEndScreen(bool state) { }

        public void SetScore(int score)
        {
            m_Score = score;
        }
    }
}
