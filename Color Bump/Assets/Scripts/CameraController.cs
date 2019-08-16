using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    /**********************************************************************************************/
    /* Private fields                                                                             */
    /**********************************************************************************************/

    #region Private fields

    [SerializeField]
    private float wallDistance = 5f;
    [SerializeField]
    private float minCamDistanceVsBall = 0.05f;
    [SerializeField]
    private float maxCamDistanceVsBall = 0.2f;
    [SerializeField]
    private float distanceToBall=17;
    private GameObject ball;

    #endregion //Private Fields

    /**********************************************************************************************/
    /* MonoBehaviour                                                                              */
    /**********************************************************************************************/

    #region MonoBehaviour

    private void Start()
    {
        ball = GameObject.Find("Ball");
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 newPos = transform.position;

        //Stay in the bound of the walls
        if (transform.position.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        if (transform.position.x > wallDistance)
        {
            pos.x = wallDistance;
        }

        //Camera velocity depend of the ball position
        if (transform.position.z < transform.position.z + minCamDistanceVsBall)
        {
            pos.z = transform.position.z + minCamDistanceVsBall;
            transform.position = Vector3.Lerp(newPos, pos, 2);
        }
        
        if ((ball.transform.position - transform.position).magnitude > distanceToBall)
        {
            newPos.z = transform.position.z + maxCamDistanceVsBall;
            transform.position = Vector3.Lerp(pos, newPos, 2);
        }
    }
    #endregion //MonoBehaviour

}
