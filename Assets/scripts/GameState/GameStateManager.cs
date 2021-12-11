using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    [SerializeField] private HitpointTimer player1Timer;
    [SerializeField] private HitpointTimer player2Timer;

    public GameState state;

    public enum GameState {
        starting, ongoing, ended
    };

    private void Start() {
        state = GameState.starting;

        Invoke("BeginGame", 1);
    }

    private void BeginGame() {
        state = GameState.ongoing;
    }

    void Update() {

        if (player1Timer.time <= 0 || player2Timer.time <= 0) {
            state = GameState.ended;
        }

    }
}
