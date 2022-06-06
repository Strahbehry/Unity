using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xRotation = 0.00f;
    [SerializeField] float yRotation = 0.50f;
    [SerializeField] float zRotation = 0.00f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
