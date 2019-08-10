using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;
    public bool GameStarted { get; private set; }
    public bool GameEnded { get; private set; }

    [SerializeField]
    private float slowMotionFactor = .1f;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform goalTransform;
    [SerializeField] private BallController ball;

    public float EntireDistance { get; private set; }
    public float DistanceLeft { get; private set; }

    private void Start()
    {
        EntireDistance = goalTransform.position.z - startTransform.position.z;
    }

    private void Awake()
    {
        if (singleton==null)
        {
            singleton = this;
        }
        else if (singleton!=null)
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void StarGame()
    {
        GameStarted = true;
    }
    public void EndGame(bool win)
    {
        GameEnded = true;
        if (!win)
        {
            //Restart Game
            Invoke("RestartGame",2*slowMotionFactor);
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Invoke("RestartGame", 2);//GOAL
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        DistanceLeft = Vector3.Distance(ball.transform.position, goalTransform.position);

        if (DistanceLeft>EntireDistance)
        {
            DistanceLeft = EntireDistance;
        }
        if (ball.transform.position.z > goalTransform.transform.position.z)
        {
            DistanceLeft = 0;
        }
    }
}
