//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.Collections;
//using UnityEngine.UI;

//[System.Serializable]
//public class ButtonHandler
//{
//    public GameObject restartButton;
        
//    public void Restart()
//    {
//        SceneManager.LoadScene("Done_Main");
//    }
//}

//public class Done_GameController : MonoBehaviour
//{
//    public GameObject[] hazards;
//    public Vector3 spawnValues;
//    public int hazardCount;
//    public float spawnWait;
//    public float startWait;
//    public float waveWait;

//    public Text scoreText;
//    public Text restartText;
//    public Text gameOverText;

//    private bool gameOver;
//    private bool restart;
//    private int score;

//    void Start()
//    {
//        gameOver = false;
//        restart = false;
//        restartText.text = "";
//        gameOverText.text = "";
//        score = 0;
//        UpdateScore();
//        StartCoroutine(SpawnWaves());
//        buttonHandler.restartButton.SetActive(false);
//    }
//       public ButtonHandler buttonHandler; 

//    void Update()
//    {
//        if (restart && Input.anyKey)
//        {
//            buttonHandler.Restart();
//            //            //if (Input.GetKeyDown(KeyCode.R))
//            //            //{
//            //            //    SceneManager.LoadScene("GamePlay");
//            //            //}
//        }
//    }

//    IEnumerator SpawnWaves()
//    {
//        yield return new WaitForSeconds(startWait);
//        while (true)
//        {
//            for (int i = 0; i < hazardCount; i++)
//            {
//                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
//                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
//                Quaternion spawnRotation = Quaternion.identity;
//                Instantiate(hazard, spawnPosition, spawnRotation);
//                yield return new WaitForSeconds(spawnWait);
//            }
//            yield return new WaitForSeconds(waveWait);

//            if (gameOver)
//            {
//                //restartText.text = "Press 'R' for Restart";
//                buttonHandler.restartButton.SetActive(true);
//                restart = true;
//                break;
//            }
//        }
//    }

//    public void AddScore(int newScoreValue)
//    {
//        score += newScoreValue;
//        UpdateScore();
//    }

//    void UpdateScore()
//    {
//        scoreText.text = "Score: " + score;
//    }

//    public void GameOver()
//    {
//        gameOverText.text = "Game Over!";
//        gameOver = true;
//    }
//}