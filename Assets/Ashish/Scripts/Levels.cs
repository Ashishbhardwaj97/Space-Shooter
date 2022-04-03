using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public GameObject levelComplete;
    public GameObject Aeroplane;
    public Text levelText;
    public Text targetText;
    private GameController gameController;
    public Slider healthBar;
    public static Levels Instance;
    public Slider playerHealthBar;

    void Start()
    {
        GameObject levelsObject = GameObject.FindWithTag("GameController");
        gameController = levelsObject.GetComponent<GameController>();
    }

    void Awake()
    {
        Instance = this;
    }

    public void Level2()
    {
        Func();
        levelText.text = "Level: 2";
        targetText.text = "Target Score: 120";
    }

    public void Level3()
    {
        Func();
        levelText.text = "Level: 3";
        targetText.text = "Target Score: 200";
    }

    public void Level4()
    {
        Func();
        levelText.text = "Level: 4";
        targetText.text = "TargetScore:250";
    }

    public void Level5()
    {
        levelComplete.SetActive(false);
        Time.timeScale = 1f;
        healthBar.gameObject.SetActive(true);
        gameController.StartCoroutine("MyFunction2");
        levelText.text = "Level: 5";
        targetText.text = "TargetScore:Infinity";
    }

    public void Func()
    {
        levelComplete.SetActive(false);
        Time.timeScale = 1f;
        gameController.StartCoroutine("MyFunction1");
        gameController.hazardCount += 5;
        gameController.speed += 4;
        gameController.destroy = false;
    }

    public void SliderValue()
    {
        healthBar.value = healthBar.value - 10;
    }

    public void PlayerSliderValue()
    {
        playerHealthBar.value = playerHealthBar.value - 10;
    }
}