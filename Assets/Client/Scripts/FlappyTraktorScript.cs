using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyTraktorScript : MonoBehaviour
{
    public float HSpeed;
    public float VSpeed;
    public Rigidbody Body;
    
    void Start()
    {
        Body.velocity = new Vector3(HSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var v = Input.GetAxis("Vertical") * VSpeed;
        Body.velocity = new Vector3(HSpeed, v, 0);
    }
}
