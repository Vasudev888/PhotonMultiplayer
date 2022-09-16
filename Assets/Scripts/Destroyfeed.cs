using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyfeed : MonoBehaviour
{
    public float destroyTime = 4f;

    private void OnEnable()
    {
        Destroy(gameObject, destroyTime);
    }
}
