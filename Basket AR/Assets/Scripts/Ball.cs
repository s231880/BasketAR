using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float throwSpeed;
    private float speed;
    private float lastMouseX, lastMouseY;

    private bool thrown, holding;
    private Rigidbody _rigidBody;
    private Vector3 newPosition;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Reset();
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (holding)
            OnTouch();

        if (thrown)
            return;

// I had to comment these lines out because they gave me error when making a build!

//#if !UNITY_EDITOR
//        if (Input.touchCount == 1)
//        {
//            Touch touch_ = Input.GetTouch(0);

//            if (touch_.phase == TouchPhase.Began)
//                OnTouchBegan(touch_.position);

//            else if (touch_.phase == TouchPhase.Moved)
//                OnTouchMoved(touch_.position);

//            else if (touch_.phase == TouchPhase.Ended)
//                OnTouchEnded(touch_.position);
//        }

//        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit, 100f))
//            {
//                if (hit.transform == transform)
//                {
//                    holding = true;
//                    transform.SetParent(null);
//                }
//            }
//        }

//        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
//        {
//            if (lastMouseY < Input.GetTouch(0).position.y)
//            {
//                ThrowBall(Input.GetTouch(0).position);
//            }
//        }

//        if (Input.touchCount == 1)
//        {
//            lastMouseX = Input.GetTouch(0).position.x;
//            lastMouseY = Input.GetTouch(0).position.y;

//            transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 50f * Time.deltaTime);
//        }
//#else
            if (true == Input.GetMouseButtonDown(0))
            {
                //OnTouchBegan(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform == transform)
                    {
                        holding = true;
                        transform.SetParent(null);
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

                transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 50f * Time.deltaTime);
            }
//#endif

    }

    void Reset()
    {
        CancelInvoke();
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane * 7.5f));
        newPosition = transform.position;

        thrown = holding = false;
        _rigidBody.useGravity = false;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0f, 200f, 0f);
        transform.SetParent(Camera.main.transform);
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
        _rigidBody.useGravity = true;
        float differenceY = (mousePos.y - lastMouseY) / Screen.height * 100;
        speed = throwSpeed * differenceY;
        float x = (mousePos.x / Screen.width) - (lastMouseX / Screen.width);
#if !UNITY_EDITOR
        x = Mathf.Abs(Input.GetTouch(0).position.x - lastMouseX) / Screen.width * 100 * x;
#else
       x = Mathf.Abs(Input.mousePosition.x - lastMouseX) / Screen.width * 100 * x;
#endif


        Vector3 direction = new Vector3(x, 0f, 1f);
        direction = Camera.main.transform.TransformDirection(direction);

        _rigidBody.AddForce((direction * speed / 2f) + (Vector3.up * speed));
        holding = false;
        thrown = true;

        Invoke("Reset", 5.0f);
    }
}
