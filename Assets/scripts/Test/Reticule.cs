using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour {
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translate = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            translate += Vector3.up;
        } else if (Input.GetKey(KeyCode.S)) {
            translate += Vector3.down;
        }

        if (Input.GetKey(KeyCode.A)) {
            translate += Vector3.left;
        } else if (Input.GetKey(KeyCode.D)) {
            translate += Vector3.right;
        }

        translate.Normalize();
        translate *= (Time.deltaTime);
        transform.position += translate;


        if (Input.GetKeyUp(KeyCode.P)) {
            Debug.Log(transform.position);
        }
    }
}
