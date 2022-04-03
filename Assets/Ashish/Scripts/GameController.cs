using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ButtonHandler
{
    public GameObject restartButton;

    public void Restart()
    {
        SceneManager.LoadScene("Done_Main");
    }
}

public class GameController : MonoBehaviour
{
    private Levels levels;
    private Invunerable invunerable;
    public GameObject hazard;
    public GameObject hazard1;
    public GameObject levelComplete;
    public GameObject Gamebackground;
    public GameObject Aeroplane;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public ButtonHandler buttonHandler;
    public GameObject Power;
    public GameObject Invincible;
    public GameObject game;
    public GameObject game1;
    public static GameController Instance;

    public Vector3 spawnValuesPower;
    public Vector3 spawnValuesPower2;
    public GameObject Halo;
    public Slider RedPower;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text levelText;
    public Text targetText;
    public Text playerLives;
    public Text nextLevelText;
    public int score;
    public int counter = 0;
    public int speed;

    public bool gameOver;
    public bool restart;
    public bool destroy;
    private AudioSource gameBackgroundAudiosource;
    private Renderer renderer1;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        GameObject levelsObject = GameObject.FindWithTag("Levels");
        levels = levelsObject.GetComponent<Levels>();

        renderer1 = Aeroplane.GetComponent<Renderer>();

        gameBackgroundAudiosource = GetComponent<AudioSource>();
        gameBackgroundAudiosource.volume = SaveStuff.backGroundAudioVolume;
        gameBackgroundAudiosource.Play();
        gameOver = false;
        restart = false;
        destroy = false;
        restartText.text = "";
        gameOverText.text = "";
        nextLevelText.text = "";
        levelText.text = "Level: 1";
        targetText.text = "Target Score: 50";
        playerLives.text = "Lives : 3";
        score = 0;
        UpdateScore();
        StartCoroutine(MyFunction1());
        StartCoroutine(PowerGenerator());
        buttonHandler.restartButton.SetActive(false);
    }

    void Update()
    {
        if (restart && Input.anyKey)
        {
            buttonHandler.Restart();
        }
    }

    public IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                game1 = Instantiate(hazard, spawnPosition, spawnRotation);
                game1.GetComponent<Done_Mover>().speed = -speed;
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                buttonHandler.restartButton.SetActive(true);
                restart = true;
                break;
            }
        }
    }

    public IEnumerator FinalBoss()
    {
        yield return new WaitForSeconds(startWait);

        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        game1 = Instantiate(hazard1, spawnPosition, spawnRotation);
        game1.GetComponent<Done_Mover>().speed = -speed;
        yield return new WaitForSeconds(spawnWait);

        yield return new WaitForSeconds(waveWait);

        if (gameOver)
        {
           buttonHandler.restartButton.SetActive(true); 
           restart = true;
        }
    }

    IEnumerator PowerGenerator()
    {
        if(counter <= 3)
        { 
        yield return new WaitForSeconds(Random.Range(7, 10));
            while (true)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValuesPower.x, spawnValuesPower.x), 0.0f, spawnValuesPower.z);
                Quaternion spawnRotation = Quaternion.identity;
                game = Instantiate(Invincible, spawnPosition, spawnRotation);
                game.transform.eulerAngles = new Vector3(90, 0, 0);
                yield return new WaitForSeconds(Random.Range(15, 20));

                Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValuesPower2.x, spawnValuesPower2.x), 0.0f, spawnValuesPower2.z);
                Quaternion spawnRotation1 = Quaternion.identity;
                game = Instantiate(Power, spawnPosition1, spawnRotation1);
                game.transform.eulerAngles = new Vector3(90, 0, 0);
                yield return new WaitForSeconds(Random.Range(6, 10));
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        if (score == 50 || score == 120 || score == 200 || score == 250)
        {
            destroy = true;
            StartCoroutine(MyFunction(0.35f));
        }
    }

    IEnumerator MyFunction(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Aeroplane != null)
        {
            Aeroplane.SetActive(false);
            levelComplete.SetActive(true);
            Gamebackground.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    IEnumerator MyFunction1()
    {
        yield return new WaitForSeconds(5);
        Aeroplane.SetActive(true);
        nextLevelText.text = "";
        StartCoroutine("SpawnWaves");
    }

    IEnumerator MyFunction2()
    {
        yield return new WaitForSeconds(4);
        Aeroplane.SetActive(true);
        nextLevelText.text = "";
        StartCoroutine("FinalBoss");
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over !!";
        gameOver = true;
        buttonHandler.restartButton.SetActive(true);
        restart = true;
    }

    public void Win()
    {
        gameOverText.text = "You Won!!";
        gameOver = true;
        buttonHandler.restartButton.SetActive(true);
        restart = true;
        StopAllCoroutines();
        Aeroplane.SetActive(false);
    }

    public void NextLevel()
    {
        counter++;

        if (counter == 1)
        {
            StopCoroutine("SpawnWaves");
            waveWait = 6f;
            Aeroplane.SetActive(false);
            nextLevelText.text = "Next Level Coming";
            levels.Level2();
        }

        if (counter == 2)
        {
            StopCoroutine("SpawnWaves");
            waveWait = 6f;
            Aeroplane.SetActive(false);
            nextLevelText.text = "Next Level Coming";
            levels.Level3();
        }

        if (counter == 3)
        {
            waveWait = 6f;
            Aeroplane.SetActive(false);
            StopCoroutine("SpawnWaves");
            nextLevelText.text = "Next Level Coming";
            levels.Level4();
        }

        if (counter == 4)
        {
            Aeroplane.SetActive(false);
            nextLevelText.text = "Next Level Coming";
            StopAllCoroutines();
            waveWait = 6f;
            levels.Level5();
        }
        levelComplete.SetActive(false);
        Gamebackground.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Func()
    {
        StartCoroutine("Hard");
    }

    IEnumerator Hard()
    {
        yield return new WaitForSeconds(4);
        Invunerable.invincible = false;
        Halo.SetActive(false);
    }

    IEnumerator Blink()
    {
        Invunerable.invincible = true;
        Func();
        for (int i = 0; i < 9 * 2; i++)
        {
            renderer1.enabled = !renderer1.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        renderer1.enabled = true;
    }
}