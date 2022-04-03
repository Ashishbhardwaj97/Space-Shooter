using UnityEngine;

public class Invunerable : MonoBehaviour
{
    public GameObject Invincible;
    public static bool invincible;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Boundary" || collider.tag == "Enemy" || collider.tag == "ShotSpawn" || collider.tag == "DoubleShot" || collider.tag == "Power")
        {
            return;
        }

        if (collider.tag == "Player")
        {
            Invincible.SetActive(false);
            invincible = true;
            GameController.Instance.Func();
            GameController.Instance.Halo.SetActive(true);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (GameController.Instance.gameOver)
        {
            if (other.tag == "Boundary")
            {
                Destroy(gameObject);
            }
        }
    }
}