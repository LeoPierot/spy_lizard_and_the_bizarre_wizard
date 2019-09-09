using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoofyController : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float maxDistanceToCenter;
    public float maxHeight = 2;
    public float minHeight = 0;
    public Transform centerOfNode;
    public Transform otherParent;
    public float maxVelocity = 3;

    private Rigidbody rb;

    void Start()
    {
        rb = centerOfNode.GetComponent<Rigidbody>();
    }
    public float gravityModifier = 1f;
    public float pushUpForce = 10;
    public bool release = false;
    void Update()
    {
        rb.AddForce((Physics.gravity * gravityModifier) * Time.deltaTime);
        float movementMagnitude = -speed * Time.deltaTime;
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetAxisRaw("right_trigger") > 0)
        {
            release = false;
            rb.AddForce(Vector3.up * pushUpForce);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
        else
        {
            if (!release)
            {
                rb.velocity = Vector3.zero;
                release = true;
            }
        }
        //y lock
        if (centerOfNode.localPosition.y > maxHeight || centerOfNode.localPosition.y < minHeight)
        {
            centerOfNode.localPosition = new Vector3(centerOfNode.localPosition.x, Mathf.Clamp(centerOfNode.localPosition.y, 0, maxHeight), centerOfNode.localPosition.z);
            rb.velocity = Vector3.zero;
        }
        

        Vector3 movement =  (Input.GetAxisRaw("left_joystick_horizontal") * Time.deltaTime * speed) * target.right + 
                            (Input.GetAxisRaw("left_joystick_vertical") * Time.deltaTime * speed) * -target.forward;
        
        Vector3 newPos = target.position + movement;
        newPos = Vector3.ClampMagnitude(newPos - centerOfNode.position, maxDistanceToCenter);
        target.position = newPos + centerOfNode.position;
    }

    void MoveLeft()
    {
        Vector3 newPos = target.position;
        newPos += new Vector3(-speed * Time.deltaTime, 0, 0);
        newPos = Vector3.ClampMagnitude(newPos - centerOfNode.position, maxDistanceToCenter);
        target.position = newPos + centerOfNode.position;
    }

    void MoveRight()
    {
        Vector3 newPos = target.position;
        newPos += new Vector3(speed * Time.deltaTime, 0, 0);
        newPos = Vector3.ClampMagnitude(newPos - centerOfNode.position, maxDistanceToCenter);
        target.position = newPos + centerOfNode.position;
    }

    void MoveForward()
    {
        Vector3 newPos = target.position;
        newPos += new Vector3(0, 0, speed * Time.deltaTime);
        newPos = Vector3.ClampMagnitude(newPos - centerOfNode.position, maxDistanceToCenter);
        target.position = newPos + centerOfNode.position;
    }
    void MoveBack()
    {
        Vector3 newPos = target.position;
        newPos += new Vector3(0, 0, -speed * Time.deltaTime);
        newPos = Vector3.ClampMagnitude(newPos - centerOfNode.position, maxDistanceToCenter);
        target.position = newPos + centerOfNode.position;
    }
}
