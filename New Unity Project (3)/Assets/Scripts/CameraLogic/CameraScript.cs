using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Object;
    public float speed = 2;
    private float objY;
    private void Start()
    {
        objY = Object.transform.position.y;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Object.position.x, Object.position.y - objY + 16.5f, transform.position.z), Time.deltaTime * speed);
    }
}
