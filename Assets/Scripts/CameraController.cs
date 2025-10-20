using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; // mario's transform
    public Transform endLimit; // gameobject that indicates end of map
    private float offset; // initial x offset between camera and Mario
    private float startX; // smallest x coord of the camera
    private float endX; // largest coord of the camera
    private float viewportHalfWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // assign current scene's mario
        player = GameObject.FindGameObjectWithTag("Mario").transform;
        // make camera snap to marios position
        //this.transform.position = new Vector3(player.position.x, this.transform.position.y, this.transform.position.z);

        // get coord of bottom left point of the viewport 
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); // z is the distance of the resulting plane from camera
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);

        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;
        // check if desiredX is within startX and end X
        if (desiredX > startX && desiredX < endX)
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);

    }
}
