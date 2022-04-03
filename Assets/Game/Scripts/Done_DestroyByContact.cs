using UnityEngine;

public class Done_DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject Player;
    public int scoreValue;
    private GameController gameController;
    public int health = 100;
    public static Done_DestroyByContact Instance;
    public static int playerHealth = 150;
    public static int playerLives =3;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameController.counter < 4)
        {
            if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Power" || other.tag == "Invunerability" || other.tag == "EnemyShip")
            {
                return;
            }

            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            if(other.tag == "Shot")
            {
                gameController.AddScore(scoreValue);
            }

            if (Invunerable.invincible == true)
            {
                if (other.tag == "Player")
                {
                    Instantiate(explosion, other.transform.position, other.transform.rotation);
                    gameController.AddScore(scoreValue);
                }
            }
           
            if (Invunerable.invincible == false)
            {
                if (other.tag == "Player")
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                    playerHealth -= 10;
                    Levels.Instance.PlayerSliderValue();

                    if (playerHealth == 100)
                    {
                        GameController.Instance.StartCoroutine("Blink");
                        Levels.Instance.playerHealthBar.value = 50;
                        playerLives--;
                        gameController.playerLives.text = "Lives :"+ playerLives;
                    }

                    if (playerHealth == 50)
                    {
                        GameController.Instance.StartCoroutine("Blink");
                        Levels.Instance.playerHealthBar.value = 50;
                        playerLives--;
                        gameController.playerLives.text = "Lives :" + playerLives;
                    }

                    if (playerHealth <= 0)
                    {
                        playerLives--;
                        gameController.playerLives.text = "Lives :" + playerLives;
                        Destroy(other.gameObject);
                        gameController.GameOver();
                        Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                        playerHealth = 150;
                        playerLives = 3;
                    }
                }
            }
            Destroy(gameObject);
        }

        if (gameController.counter == 4)
        {
            GameController.Instance.destroy = false;
            PlayerController.doubleShot = false;
            GameController.Instance.Halo.SetActive(false);
            if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Power" || other.tag == "Invunerability" || other.tag == "EnemyShip")
            {
                return;
            }

            if (other.tag == "Shot")
            {
                
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                gameController.AddScore(scoreValue);
                health = health - 10;
                Levels.Instance.SliderValue();
                if (health <= 0)
                {
                    Destroy(gameObject);
                    gameController.Win();
                }
            }
            if (other.tag == "Player")
            {
                Instantiate(explosion, transform.position, transform.rotation);
                playerHealth -= 10;
                Levels.Instance.PlayerSliderValue();

                if (playerHealth == 100)
                {
                    Levels.Instance.playerHealthBar.value = 50;
                    GameController.Instance.StartCoroutine("Blink");
                    playerLives--;
                    gameController.playerLives.text = "Lives :" + playerLives;
                }

                if (playerHealth == 50)
                {
                    Levels.Instance.playerHealthBar.value = 50;
                    GameController.Instance.StartCoroutine("Blink");
                    playerLives--;
                    gameController.playerLives.text = "Lives :" + playerLives;
                }

                if (playerHealth <= 0)
                {
                    playerLives--;
                    gameController.playerLives.text = "Lives :" + playerLives;
                    Destroy(other.gameObject);
                    gameController.GameOver();
                    playerHealth = 150;
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                }
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (GameController.Instance.gameOver || GameController.Instance.destroy)
        {
            if (other.tag == "Boundary")
            {
                Destroy(gameObject);
            }

            if(other.tag == "EnemyShip")
            {
                return;
            }
        }
    }
}