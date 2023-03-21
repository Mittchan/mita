using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    public float score;
    public float bestScore;
    private bool m_GameOver = false;

    public static MainManager Instance;

    public Text Pointstext;
    public Text nameText;

    private string hiscoreKey = "highScore";
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = PlayerPrefs.GetString("PlayerName");
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);

            }
        }
        bestScore =  PlayerPrefs.GetFloat("highScore");
        LoadScore();
    }


    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        score += point;
        ScoreText.text = $"Score : {score}";
        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("highScore", score);
            print(bestScore);
            Pointstext.text = $"HighScore: {bestScore}";
        }
    }
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveScore();
        DisplayScore();
    }

    public void DisplayScore()
    {
        Pointstext.text = $"HighScore {bestScore}";

    }

    public void SaveScore()
    {
        PlayerPrefs.SetFloat("highScore", bestScore);
        PlayerPrefs.Save();
        LoadScore();
    }

    public void LoadScore()
    {
        Pointstext.text = $"HighScore {bestScore}";

    }
}
