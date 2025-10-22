using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class HUDManager : Singleton<HUDManager>
{
    // public GameObject highestscoreText;
    // public IntVariable gameScore;
    // index 0 is gameplay pos, 1 is game over menu pos
    private Vector3[] scoreTextPosition =
    {
        new Vector3(-234,153, 0),
        new Vector3(0, 0, 0)
    };
    private Vector3[] restartButtonPosition =
    {
        new Vector3(332, 174, 0),
        new Vector3(0, -32, 0)
    };

    public GameObject scoreText;
    public Transform restartButton;

    public GameObject gameOverPanel;
    public IntVariable gameScore;

    override public void Awake()
    {
        base.Awake();
        // GameManager.instance.gameStart.AddListener(GameStart);
        // GameManager.instance.gameRestart.AddListener(GameStart);
        // GameManager.instance.scoreChange.AddListener(SetScore);
        // GameManager.instance.gameOver.AddListener(GameOver);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        //hide gameover panel
        gameOverPanel.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.transform.localPosition = restartButtonPosition[0];
    }

    public void SetScore()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + gameScore.Value.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.transform.localPosition = restartButtonPosition[1];

        // set highscore
        // highestscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
        // // show
        // highestscoreText.SetActive(true);
    }
}
