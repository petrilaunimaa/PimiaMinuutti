using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointTimer : MonoBehaviour {

    public float time = 60f;
    public bool active = false;

    public int playerIndex = 0;
    
    void Update() {
        if (active) {
            time -= Time.deltaTime;
        }
    }
}
