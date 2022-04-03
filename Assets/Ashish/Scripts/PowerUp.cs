using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject Power;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Boundary" || collider.tag == "Enemy" || collider.tag == "ShotSpawn" || collider.tag == "DoubleShot" || collider.tag == "Invunerability")
        {
            return;
        }

        if (collider.tag == "Player")
        {
            Power.SetActive(false);
            GameController.Instance.Halo.SetActive(true);
            PlayerController.doubleShot = true;
            PlayerController.Instance.Func();
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