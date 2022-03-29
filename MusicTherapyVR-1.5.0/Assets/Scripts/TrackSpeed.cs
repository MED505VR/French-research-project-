using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSpeed : MonoBehaviour
{
    private int sens = 2;   //sens for how hard you need to hit
    private Vector3 lastPosition; //Here we will store the last position
    public float speed; //The speed is what we will set the volume with a max of 1 (Volume cant be more than 1)


    private void Start()
    {
        lastPosition = transform.position; //We take the position from where we spawn and put it to lastPosition 
    }
    private void FixedUpdate()  //Instaed of Update() that runs on every frame FixedUpdate runs every 0.2 sec
    {
        speed = (((transform.position - lastPosition).magnitude) /sens); //How much have we moved
        lastPosition = transform.position; //set the current position to the lastPosition
    }
}