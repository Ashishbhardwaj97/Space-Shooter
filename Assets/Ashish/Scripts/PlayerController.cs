using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public static bool doubleShot;
    public GameObject shot;
    public Transform shotSpawn;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public float fireRate;
    private float nextFire;
    public static PlayerController Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        doubleShot = false;
    }

    void OnEnable()
    {
        Pause.GameIsPaused = false;
    }

    void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = SaveStuff.inGameAudioVolume;

        if (!Pause.GameIsPaused)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                if(doubleShot)
                {
                    FireBullets();
                }
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                AudioSource audioData = GetComponent<AudioSource>();
                audioData.Play();
            }
        }
    }

    void FireBullets()
    {
        GameObject Bullet1 = (GameObject)Instantiate (shot);
        Bullet1.transform.position = shotSpawn1.transform.position;

        GameObject Bullet2 = (GameObject)Instantiate(shot);
        Bullet2.transform.position = shotSpawn2.transform.position;
    }

    public void Func()
    {
        StartCoroutine(DoubleCountDown());
    }

    IEnumerator DoubleCountDown()
    {
        yield return new WaitForSeconds(4);
        doubleShot = false;
        GameController.Instance.Halo.SetActive(false);
    }

    void FixedUpdate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );
        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);   
    }
}