using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    void Update()
    {

        RotateonAxis();

    }

    public void RotateonAxis()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 600);
    }
}
