using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public Transform player;
    public float Mousesens = 200f;

    private float xRotation;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseXpos = Input.GetAxis("Mouse X") * Mousesens * Time.deltaTime;
        float mouseYpos = Input.GetAxis("Mouse Y") * Mousesens * Time.deltaTime;
        xRotation -= mouseYpos;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseXpos);
    }
}
