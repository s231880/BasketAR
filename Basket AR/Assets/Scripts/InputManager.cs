using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// Sorry man, I'm become kind of code Nazi, if i see messy things I get mad
/// </summary>
namespace BBAR
{
    public class InputManager : MonoBehaviour
    {
        private Vector2 m_StartingPosition, m_FinalPosition;

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

        private void UpdatePlacementPose()
        {

#if !UNITY_EDITOR
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                    OnTouchBegan(touch.position);

                else if (touch.phase == TouchPhase.Moved)
                    OnTouchMoved(touch.position);

                else if (touch.phase == TouchPhase.Ended)
                    OnTouchEnded(touch.position);
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
                    GameManager.Instance.m_BasketManager.PlaceTheBasket(m_PlacementPose.position, m_PlacementPose.rotation);
                }

            }
            //Else throw the ball or do all the rest
            else
            {
                if (ActiveBallHasBeenTouched(m_TouchPosition))
                {
                    m_StartingPosition = m_TouchPosition;
                    //The ball schoud be already active is moved into a specific position by the GetObject function into ObjectPool.cs
                    //GameManager.Instance.m_ActiveBall.transform.SetParent(null);   
                }

            }
        }

        private void OnTouchMoved(Vector3 touchPosition)
        {
            m_TouchPosition = touchPosition;
            if (!GameManager.Instance.m_IsTheBasketPlaced)
            {
                if (AValidPlaneHasBeenTouched())
                {
                    GameManager.Instance.m_BasketManager.MoveTheBasket(m_PlacementPose.position);
                }
            }
            else
            {
                //m_LastMouseX = touchPosition.x;
                //m_LastMouseY = touchPosition.y;

                //Not sure why you want this man, the ball should move only when the the user release the touch
                //GameManager.Instance.m_ActiveBall.transform.localPosition = Vector3.Lerp(GameManager.Instance.m_ActiveBall.transform.localPosition, newPosition, 50f * Time.deltaTime);
            }
        }

        private void OnTouchEnded(Vector3 touchPosition)
        {
            m_TouchPosition = touchPosition;
            if (!GameManager.Instance.m_IsTheBasketPlaced)
            {
                if (AValidPlaneHasBeenTouched())
                {
                    GameManager.Instance.m_IsTheBasketPlaced = true;        //Place the basket
                    GameManager.Instance.ActivateBall();                    //Activate the first ball
                }
                else
                {
                    GameManager.Instance.m_BasketManager.DeleteBasket();
                }
            }
            else //Throw the ball
            {
                if (m_StartingPosition.y < m_TouchPosition.y)
                {
                    m_FinalPosition = m_TouchPosition;
                    GameManager.Instance.ThrowActiveBall(m_StartingPosition, m_FinalPosition);  //Throw the ball
                    GameManager.Instance.ActivateBall();                                        //Active a new ball
                }
                else
                {
                    ResetVariables();
                }
            }
        }
        //-----------------------------------------------------------------------
        //Raycast functions

        private bool AValidPlaneHasBeenTouched()
        {
#if !UNITY_EDITOR
            GameManager.Instance.m_UIManager.SetLabelTest("QUI??");
            bool result = (m_RayCastManager.Raycast(m_TouchPosition, m_HitsList, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon));
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
            bool result = true;
            m_PlacementPose = new Pose(Vector3.zero, Quaternion.identity);
#endif
            return result;

        }

        private bool ActiveBallHasBeenTouched(Vector2 touchPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform == GameManager.Instance.m_ActiveBall.transform)  //I'll do this check with the collision instead of the pos because I'm afraid that this could cause errors
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        //-----------------------------------------------------------------------

        private void ResetVariables()
        {
            m_StartingPosition = Vector2.zero;
            m_FinalPosition = Vector2.zero;
        }
    }
}
