using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEditorInternal;

public class GameManager : MonoBehaviour
{
    public int score;
    public int goal;
    private bool timerActive = true;
    public float minutes;
    public int nextSceneBuildIndex;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Timer Guide: 
    //https://medium.com/@eveciana21/creating-a-stopwatch-timer-in-unity-f4dff748030d
    //Singleton guide : https://gamedevbeginner.com/singletons-in-unity-the-right-way/


    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        minutes *= 60;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timerActive)
        {
            minutes -= Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(minutes);
        timerText.text = time.Minutes.ToString() + " : "+  time.Seconds.ToString() + "." + time.Milliseconds.ToString();
        
        
        if (score >= goal)
        {
            SceneManager.LoadScene(nextSceneBuildIndex); 
        }
        if (minutes <= 0)
        {
            resetScene();
        }
        
    }

    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
