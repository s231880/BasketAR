using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

/// <summary>
/// Sorry man, I'm become kind of code Nazi, if i see messy things I get mad
/// </summary>
namespace BBAR
{
    public class InputManager : MonoBehaviour
    {
        private Vector2 m_StartingPosition, m_FinalPosition;
        public bool m_ThereIsAnActivePlane = false;
        private bool m_HoldingTouch = false;

        //-----------------------------------------------------------------------
        //AR variables
        private Pose m_PlacementPose;
        private Vector2 m_TouchPosition = default;
        private ARRaycastManager m_RayCastManager;
        private Camera m_ARCamera;
        private List<ARRaycastHit> m_HitsList = new List<ARRaycastHit>();

        //-----------------------------------------------------------------------
        private GameObject m_BasketPositionCursorPrefab;
        private GameObject m_BasketCursor;
        public void Initialise()
        {
            m_RayCastManager = gameObject.GetComponent<ARRaycastManager>();
            m_ARCamera = this.GetComponentInChildren<Camera>();
            m_BasketPositionCursorPrefab = Resources.Load<GameObject>("BasketPositionCursor");
            //GameManager.Instance.m_UIManager.SetLabelTest("IM Initted");
        }
        void Update()
        {
            UpdatePlacementPose();
        }

        private void UpdatePlacementPose()
        {

//#if !UNITY_EDITOR
            if(m_ThereIsAnActivePlane)
            {
               if (Input.touchCount > 0)
               {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                    OnTouchBegan(touch.position);

                else if (touch.phase == TouchPhase.Moved)
                    OnTouchMoved(touch.position);

                else if (touch.phase == TouchPhase.Ended)
                    OnTouchEnded(touch.position);
               }  
            }
//#else
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
//#endif
        }

        private void OnTouchBegan(Vector2 touchPosition)
        {
            GameManager.Instance.m_UIManager.SetLabelTest("Touch Began");
            m_TouchPosition = touchPosition;
            
            if (!GameManager.Instance.m_IsTheBasketPlaced)              //If the basket hasn't been placed yet
            {
                if (AValidPlaneHasBeenTouched(m_TouchPosition))
                {
                    m_BasketCursor = Instantiate(m_BasketPositionCursorPrefab, m_PlacementPose.position, m_PlacementPose.rotation);
                }
            }
            //Else throw the ball or do all the rest
            else
            {
                if (ActiveBallHasBeenTouched(m_TouchPosition))
                {
                    m_StartingPosition = m_TouchPosition;
                    m_HoldingTouch = true;
                    //The ball schoud be already active is moved into a specific position by the GetObject function into ObjectPool.cs
                    //GameManager.Instance.m_ActiveBall.transform.SetParent(null);
                }

            }
        }

        private void OnTouchMoved(Vector2 touchPosition)
        {
            GameManager.Instance.m_UIManager.SetLabelTest("Touch In Progress");
            m_TouchPosition = touchPosition;
            //Placing the basket
            if (!GameManager.Instance.m_IsTheBasketPlaced)      
            {
                if (AValidPlaneHasBeenTouched(m_TouchPosition))
                {
                    m_BasketCursor.transform.position = m_PlacementPose.position;
                }
            }
            //Throwing the ball
            else
            {
                //m_LastMouseX = touchPosition.x;
                //m_LastMouseY = touchPosition.y;

                //Not sure why you want this man, the ball should move only when the the user release the touch
                
                //GameManager.Instance.m_ActiveBall.transform.localPosition = Vector3.Lerp(GameManager.Instance.m_ActiveBall.transform.localPosition, newPosition, 50f * Time.deltaTime);
            }
        }

        private void OnTouchEnded(Vector2 touchPosition)
        {
            m_TouchPosition = touchPosition;
            GameManager.Instance.m_UIManager.SetLabelTest("Touch Ended");
            if (!GameManager.Instance.m_IsTheBasketPlaced)
            {
                if (AValidPlaneHasBeenTouched(m_TouchPosition))
                {
                    Destroy(m_BasketCursor);
                    GameManager.Instance.m_BasketManager.PlaceTheBasket(m_PlacementPose.position, m_PlacementPose.rotation);    //Place the basket
                    GameManager.Instance.m_IsTheBasketPlaced = true;
                    GameManager.Instance.ActivateBall();                                                                        //Activate the first ball
                }
                else
                {
                    Destroy(m_BasketCursor);
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
                m_HoldingTouch = false;
            }
        }
        //-----------------------------------------------------------------------
        //Raycast functions

        private bool AValidPlaneHasBeenTouched(Vector2 touchPosition)
        {
#if !UNITY_EDITOR
            var ray = m_ARCamera.ScreenPointToRay(touchPosition);
            if(m_RayCastManager.Raycast(ray, m_HitsList, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                m_PlacementPose = m_HitsList[0].pose;
                return true;
            }
            else
            {
                return false;
            }
#else
            m_PlacementPose = new Pose(Vector3.zero, Quaternion.identity);
            return true;
#endif
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
        //-----------------------------------------------------------------------
    }
}
