using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointTimer : MonoBehaviour {

    public float time = 60f;
    public bool active = true;
    
    void Update() {
        if (active) {
            time -= Time.deltaTime;
        }
    }
}
