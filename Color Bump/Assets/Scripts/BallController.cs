using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    /**********************************************************************************************/
    /* Private fields                                                                             */
    /**********************************************************************************************/

    #region Private fields

   
    [SerializeField]
    private float thrust = 150f;

    private Rigidbody rb;

    private Vector2 lastMousePos;


    [SerializeField]
    private float wallDistance = 5f;
    [SerializeField]
    private float minCamDistance = 3f;

    #endregion // Private fields

    /**********************************************************************************************/
    /* MonoBehaviour                                                                              */
    /**********************************************************************************************/

    #region MonoBehaviour


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 deltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;

            if (lastMousePos==Vector2.zero)           
                lastMousePos = currentMousePos;
            
            deltaPos = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;

            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y)*thrust;
            rb.AddForce(force);

        }
        else
        {
            lastMousePos = Vector2.zero;
        }
        
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;

        if (transform.position.x<-wallDistance)
        {
            pos.x = -wallDistance;
        }
        else if (transform.position.x>wallDistance)
        {
            pos.x = wallDistance;
        }
        if (transform.position.z<Camera.main.transform.position.z+minCamDistance)
        {
            pos.z = Camera.main.transform.position.z + minCamDistance;
        }

        transform.position = pos;
    }
    #endregion // MonoBehaviour
}
