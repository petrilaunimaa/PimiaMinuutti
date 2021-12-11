using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOverScreen : MonoBehaviour {

    private GameStateManager gameState;
    [SerializeField] private CanvasGroup cg;

    [SerializeField] private TextMeshProUGUI winnerLabel;

    private bool detectedEnd = false;

    void Start() {
        gameState = FindObjectOfType<GameStateManager>();

        cg.interactable = false;
        cg.alpha = 0;
    }

    // Update is called once per frame
    void Update() {

        if (detectedEnd == false && gameState.state == GameStateManager.GameState.ended) {
            detectedEnd = true;
            cg.interactable = true;
            cg.alpha = 1;

            winnerLabel.text = "Pelaaja #" + gameState.winnerPlayerIndex;
        }

    }
}
