using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeMover : MonoBehaviour {

    private Vector2 originalPosition;

    [SerializeField] private GameObject eyeSprite;

    void Start() {
        originalPosition = eyeSprite.transform.localPosition;
    }

    void Update() {
        Vector2 translation = Vector2.up * Mathf.Sin(Time.time * 2) * 0.02f;
        eyeSprite.transform.localPosition = originalPosition + translation;
    }
}
