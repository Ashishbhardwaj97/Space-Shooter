using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBackground : MonoBehaviour
{
    void Start()
    {
        lastpos = camera.position;
    }

    public new Transform camera;
    public float speedCoefficient;
    Vector3 lastpos;

    void Update()
    {
        transform.position -= ((lastpos - camera.position) * speedCoefficient);
        lastpos = camera.position;
    }
}