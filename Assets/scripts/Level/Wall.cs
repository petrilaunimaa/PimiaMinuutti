using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public Rigidbody2D rb;
    public Collider2D col;

    void Start() {
        
    }



    void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "bullet") {
            Destroy(collision.gameObject);
        }
    }

}
