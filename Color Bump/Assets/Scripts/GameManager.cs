using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public ParticleSystem confetiParticle;

	public static GameManager singleton;
    public bool GameStarted { get; private set; }
    public bool GameEnded { get; private set; }

    [SerializeField]
    private float slowMotionFactor = .1f;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform goalTransform;
    [SerializeField] private BallController ball;

    private int index;

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
			confetiParticle.gameObject.SetActive(true);
            Invoke("NewLevel", 1.5f);
            //GOAL
        }
    }

    public void RestartGame()
    {
       index= SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(index);
    }
    public void NewLevel()
    {
       index = SceneManager.GetActiveScene().buildIndex;
       index++;
       if (index > 4)
        {
            SceneManager.LoadScene(0);
            index = 0;
        }
         
       else
          SceneManager.LoadScene(index);
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

        //if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        //{
        //    SceneManager.LoadScene(0);
        //}
        //else
        //{
        //    Application.Quit();
        //}
    }
}
