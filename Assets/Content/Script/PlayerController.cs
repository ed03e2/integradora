using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Personaje
    Transform tr;
    Rigidbody rb;

    public float walkSpeed = 400;


    //Camara
    public Transform cameraShoulder;// eje de la camara
    public Transform cameraHolder;// La pocision y rotacion de la camara con respecto al personaje 
    public Transform cam;

    private float rotY = 0f;

    public float rotationSpeed = 200;
    public float minAngle = -45;
    public float maxAngle = 45;

    public float cameraSpeed = 200; 

    //Animaciones 

    Animator anim;

    private Vector2 anim
    

    // Start is called before the first frame update
    void Start()
    {
        tr = this.transform;

        rb = GetComponent<Rigidbody>();

        cam = Camera.main.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
        MoveControl();
        
    }

    public void MoveControl()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        float deltaT = Time.deltaTime;

        Vector3 side= walkSpeed * deltaX * deltaT * tr.right;
        Vector3 forward = walkSpeed * deltaZ * deltaT * tr.forward;
        Vector3 direction = side + forward;

        rb.velocity = direction;      
                                                                                                                        

    }
    public void CameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float deltaT = Time.deltaTime;
        
        rotY += mouseY * rotationSpeed * deltaT;

        float rotX = mouseX * rotationSpeed *  deltaT;

        tr.Rotate(0, rotX, 0);

        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

        Quaternion localRotation = Quaternion.Euler(-rotY,0,0);
        cameraShoulder.localRotation = localRotation;

        cam.position = Vector3.Lerp(cam.position, cameraHolder.position, cameraSpeed * deltaT);
        cam.rotation = Quaternion.Lerp(cam.rotation, cameraHolder.rotation, cameraSpeed * deltaT);
    }
}
