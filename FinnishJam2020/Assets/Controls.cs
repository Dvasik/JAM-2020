using System;
using UnityEngine;
using System.Collections;
 
public class Controls : MonoBehaviour
{
    private float XRot = 0f, YRot = 0f;
    [SerializeField]
    float speed = 30f;
    float camSens = 4f; // How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); // Kind of in the middle of the screen, rather than at the top (play)
    private Vector3 Pos = new Vector3(0, 0, 0);

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3
            F = new Vector3(gameObject.transform.forward.x, 0, gameObject.transform.forward.z),
            R = new Vector3(gameObject.transform.right.x, 0, gameObject.transform.right.z);
        
        if (Input.GetKeyDown(KeyCode.Space))
            gameObject.GetComponent<Rigidbody>().AddForce(0f, 30000f, 0f);
        
        if (Input.GetKey(KeyCode.W))
            gameObject.transform.position += F * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            gameObject.transform.position -= F * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.A))
            gameObject.transform.position -= R * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.D))
            gameObject.transform.position += R * Time.deltaTime * speed;
    }
    void FixedUpdate ()
    {
        XRot -= Input.GetAxis("Mouse Y") * camSens;
        YRot += Input.GetAxis("Mouse X") * camSens;
        
        transform.rotation = Quaternion.Euler(XRot, YRot, 0);
    }
}