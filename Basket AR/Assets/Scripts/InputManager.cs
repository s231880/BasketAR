using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BBAR
{
    public class InputManager : MonoBehaviour
    {

        private bool m_ThereIsABall = false;

        public void Initialise()
        {

        }
        void Update()
        {
            UpdatePlacementPose();
        }

        private void UpdatePlacementPose()
        {

#if !UNITY_EDITOR
            if (Input.touchCount == 1)
            {
                Touch touch_ = Input.GetTouch(0);

                if (touch_.phase == TouchPhase.Began)
                    OnTouchBegan(touch_.position);

                else if (touch_.phase == TouchPhase.Moved)
                    OnTouchMoved(touch_.position);

                else if (touch_.phase == TouchPhase.Ended)
                    OnTouchEnded(touch_.position);

            }
#else
            if (true == Input.GetMouseButtonDown(0))
            {
                OnTouchBegan(Input.mousePosition);
            }
            else if (true == Input.GetMouseButton(0))
            {
                OnTouchMoved(Input.mousePosition);
            }
            else if (true == Input.GetMouseButtonUp(0))
            {
                OnTouchEnded(Input.mousePosition);
            }
#endif
        }

        private void OnTouchBegan(Vector3 touchPosition)
        {
            if (!m_ThereIsABall)
                GameManager.Instance.ActivateBall();
            else
                GameManager.Instance.DisableBall();

            m_ThereIsABall = !m_ThereIsABall;
        }

        private void OnTouchMoved(Vector3 touchPosition) { }

        private void OnTouchEnded(Vector3 touchPosition) { }
    }
}
