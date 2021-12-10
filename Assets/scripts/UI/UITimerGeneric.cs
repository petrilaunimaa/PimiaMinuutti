using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimerGeneric : MonoBehaviour {

    public int playerIndex = 0;

    protected HitpointTimer hp;

    void Start() {
        FindHitpointTimerFromScene();
    }

    void Update() {

    }


    void FindHitpointTimerFromScene() {
        HitpointTimer[] timers = FindObjectsOfType<HitpointTimer>();
        if (timers[0].playerIndex == playerIndex) {
            hp = timers[0];
        } else {
            hp = timers[1];
        }
    }
}
