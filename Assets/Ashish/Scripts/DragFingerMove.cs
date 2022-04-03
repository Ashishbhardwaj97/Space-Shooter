using UnityEngine;
using UnityEngine.EventSystems;

public class DragFingerMove : MonoBehaviour
{

    private float deltaX;
    private float deltaZ;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchpos.x - transform.position.x;
                    deltaZ = touchpos.z - transform.position.z;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector3(touchpos.x - deltaX, 0.0f, touchpos.z - deltaZ));
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector3.zero;
                    break;
            }
        }
    }
}