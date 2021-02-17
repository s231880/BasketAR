using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BBAR
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private float throwSpeed;
        private float speed;
        private float lastMouseX, lastMouseY;

        private bool thrown, holding;
        private Rigidbody _rigidBody;
        private Vector3 newPosition;

        private bool m_ThereIsABall = false;    //For testing purpose only

        public void Initialise()
        {
            
        }
        void Update()
        {
            if (holding)
                OnTouch();

            if (thrown)
                return;

            

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

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform == GameManager.Instance.m_ActiveBall.transform)
                    {
                        holding = true;
                        GameManager.Instance.m_ActiveBall.transform.SetParent(null);
                    }
                }
            }

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (lastMouseY < Input.GetTouch(0).position.y)
                {
                    ThrowBall(Input.GetTouch(0).position);
                }
            }

            if (Input.touchCount == 1)
            {
                lastMouseX = Input.GetTouch(0).position.x;
                lastMouseY = Input.GetTouch(0).position.y;

                GameManager.Instance.m_ActiveBall.transform.localPosition = Vector3.Lerp(GameManager.Instance.m_ActiveBall.transform.localPosition, newPosition, 50f * Time.deltaTime);
            }
#else
            if (true == Input.GetMouseButtonDown(0))
            {
                //OnTouchBegan(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform == GameManager.Instance.m_ActiveBall.transform)
                    {
                        holding = true;
                        GameManager.Instance.m_ActiveBall.transform.SetParent(null);
                    }
                }
            }
            if (true == Input.GetMouseButtonUp(0))
            {
                //OnTouchEnded(Input.mousePosition);
                if (lastMouseY < Input.mousePosition.y)
                {
                    ThrowBall(Input.mousePosition);
                }
            }

             if (true == Input.GetMouseButton(0))
            {
                //OnTouchMoved(Input.mousePosition);
                lastMouseX = Input.mousePosition.x;
                lastMouseY = Input.mousePosition.y;

                GameManager.Instance.m_ActiveBall.transform.localPosition = Vector3.Lerp(GameManager.Instance.m_ActiveBall.transform.localPosition, newPosition, 50f * Time.deltaTime);
            }

           
#endif
        }

        private void OnTouchBegan(Vector3 touchPosition)
        {
            //TIP: Do a raycast to check if the user tapped the ball, if yes store the touch position
            //-----------------------------------------------------------------------
            //This code below is only for testing purposes

            //if (!m_ThereIsABall)
            //    GameManager.Instance.ActivateBall();
            //else
            //    GameManager.Instance.DisableBall();

            //m_ThereIsABall = !m_ThereIsABall;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform == GameManager.Instance.m_ActiveBall.transform)
                {
                    holding = true;
                    GameManager.Instance.m_ActiveBall.transform.SetParent(null);
                }
            }
        }


        private void OnTouchMoved(Vector3 touchPosition)
        {
            //TIP: to calculate the amount of dragging (and then the power which the ball is thrown) 
            //you have to keep in mind that different devices have different screen size, for this reason every calculation
            //has to be done in relation to the screen width and height

            lastMouseX = touchPosition.x;
            lastMouseY = touchPosition.y;

            GameManager.Instance.m_ActiveBall.transform.localPosition = Vector3.Lerp(GameManager.Instance.m_ActiveBall.transform.localPosition, newPosition, 50f * Time.deltaTime);
        }

        private void OnTouchEnded(Vector3 touchPosition)
        {
            //Throw the ball
            if (lastMouseY < touchPosition.y)
            {
                ThrowBall(touchPosition);
            }
        }

        void Reset()
        {
            CancelInvoke();
            GameManager.Instance.m_ActiveBall.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane * 7.5f));
            newPosition = GameManager.Instance.m_ActiveBall.transform.position;

            thrown = holding = false;
            GameManager.Instance.m_ActiveBallScript._rigidBody.useGravity = false;
            GameManager.Instance.m_ActiveBallScript._rigidBody.velocity = Vector3.zero;
            GameManager.Instance.m_ActiveBallScript._rigidBody.angularVelocity = Vector3.zero;
            GameManager.Instance.m_ActiveBall.transform.rotation = Quaternion.Euler(0f, 200f, 0f);
            GameManager.Instance.m_ActiveBall.transform.SetParent(Camera.main.transform);
        }

        void OnTouch()
        {
#if !UNITY_EDITOR
            Vector3 mousePos = Input.GetTouch(0).position;
#else
        Vector3 mousePos = Input.mousePosition;
#endif
            mousePos.z = Camera.main.nearClipPlane * 7.5f;

            newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        }

        void ThrowBall(Vector2 mousePos)
        {
            GameManager.Instance.m_ActiveBallScript._rigidBody.useGravity = true;
            float differenceY = (mousePos.y - lastMouseY) / Screen.height * 100;
            speed = GameManager.Instance.m_ActiveBallScript.throwSpeed * differenceY;
            float x = (mousePos.x / Screen.width) - (lastMouseX / Screen.width);
#if !UNITY_EDITOR
            x = Mathf.Abs(Input.GetTouch(0).position.x - lastMouseX) / Screen.width * 100 * x;
#else
            x = Mathf.Abs(Input.mousePosition.x - lastMouseX) / Screen.width * 100 * x;
#endif


            Vector3 direction = new Vector3(x, 0f, 1f);
            direction = Camera.main.transform.TransformDirection(direction);

            GameManager.Instance.m_ActiveBallScript._rigidBody.AddForce((direction * speed / 2f) + (Vector3.up * speed));
            holding = false;
            thrown = true;

            Invoke("Reset", 5.0f);
        }
    }
}
