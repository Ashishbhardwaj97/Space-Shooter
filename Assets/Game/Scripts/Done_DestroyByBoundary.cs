using UnityEngine;

public class Done_DestroyByBoundary : MonoBehaviour
{
    public GameObject Power;
    public Vector3 spawnValues;
    public GameObject Invulnerable;

    void OnTriggerExit (Collider other) 
	{
        if (other.tag == "Power" || other.tag == "Invunerability")
        {
            return;
        }
    }
}