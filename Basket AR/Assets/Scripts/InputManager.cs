using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


namespace BBAR
{
    public class InputManager : MonoBehaviour
    {

        private bool m_ThereIsABall = false;    //For testing purpose only
        private bool m_IsUserPlacingTheBasket = false;
        private bool m_HoldingTouch = false;

        //-----------------------------------------------------------------------
        //AR variables
        private Pose m_PlacementPose;
        private Vector2 m_TouchPosition = default;
        private ARRaycastManager m_RayCastManager;

        private static List<ARRaycastHit> m_HitsList;
        public void Initialise()
        {
            m_RayCastManager = gameObject.transform.Find("AR Session Origin").GetComponent<ARRaycastManager>();
        }
        void Update()
        {
            UpdatePlacementPose();
        }
        /// <summary>
        /// The first part (!UNITY_EDITOR) is the one which is gonna do the actual work once the build 
        /// is put on the phone, while the second one is only for you to make our life easier in development phase
        /// because allow us to test the app on the editor without doing a build each time
        /// </summary>
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
            m_TouchPosition = touchPosition;
            
            if (!GameManager.Instance.m_IsTheBasketPlaced)              //If the basket hasn't been placed yet
            {
                if (AValidPlaneHasBeenTouched())
                {
                    m_IsUserPlacingTheBasket = true;                    //The user is placing the basket
                    GameManager.Instance.m_BasketManager.PlaceTheBasket(m_PlacementPose.position, m_PlacementPose.rotation);
                }

            }
            //Else throw the ball or do all the rest
            else
            {

            }
        }


        private void OnTouchMoved(Vector3 touchPosition)
        {
            m_TouchPosition = touchPosition;
            if (!GameManager.Instance.m_IsTheBasketPlaced && m_IsUserPlacingTheBasket)
            {
                if (AValidPlaneHasBeenTouched())
                {
                    GameManager.Instance.m_BasketManager.MoveTheBasket(m_PlacementPose.position);
                }
            }
            else
            {

            }
        }

        private void OnTouchEnded(Vector3 touchPosition)
        {
            m_TouchPosition = touchPosition;
            if (!GameManager.Instance.m_IsTheBasketPlaced && m_IsUserPlacingTheBasket)
            {
                if (AValidPlaneHasBeenTouched())
                {
                    GameManager.Instance.m_IsTheBasketPlaced = true;

                }
                else
                {
                    m_IsUserPlacingTheBasket = false;
                    GameManager.Instance.m_BasketManager.DeleteBasket();
                }
            }
            else 
            {
               //Throw the ball
            }
        }

        private bool AValidPlaneHasBeenTouched()
        {
            bool result;
#if !UNITY_EDITOR
            result = (m_RayCastManager.Raycast(m_TouchPosition, m_HitsList, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon));
            if (result)
            {
                m_PlacementPose = m_HitsList[0].pose;
                GameManager.Instance.m_UIManager.SetLabelTest("Valid Plane Hit");
            }
            else
            {
                GameManager.Instance.m_UIManager.SetLabelTest("Invalid Plane Hit");
            }

#else
            result = true;
            m_PlacementPose = new Pose(Vector3.zero, Quaternion.identity);
#endif
            return result;
        }
    }
}
