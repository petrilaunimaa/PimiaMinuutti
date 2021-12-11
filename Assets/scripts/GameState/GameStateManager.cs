using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
    private HitpointTimer player1Timer;
    private HitpointTimer player2Timer;

    [SerializeField] ParticleSystem player1Particles;
    [SerializeField] ParticleSystem player2Particles;

    public GameState state;

    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioSource zapAudio;

    private float escapePressedTime = 0;

    public int winnerPlayerIndex = -1;

    public enum GameState {
        starting, ongoing, ended
    };

    private void Start() {

        HitpointTimer[] timers = FindObjectsOfType<HitpointTimer>();
        player1Timer = timers[0];
        player2Timer = timers[1];

        state = GameState.starting;

        player1Particles.Stop();
        player2Particles.Stop();

        Invoke("BeginGame", 2);
    }

    private void BeginGame() {
        state = GameState.ongoing;
    }

    void Update() {

        if (player1Timer.time <= 0 || player2Timer.time <= 0) {
            state = GameState.ended;
            winnerPlayerIndex = player1Timer.time <= 0 ? 1 : 2;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            escapePressedTime = 0;
        } else if (Input.GetKey(KeyCode.Escape)) {
            escapePressedTime += Time.deltaTime;
            if (escapePressedTime > 2f) {
                Application.Quit();
            }
        }

        if (state == GameState.ended && Input.GetKeyUp(KeyCode.Return)) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }


    public void swapTimerToPlayerIndex(int playerIndex) {
        player1Timer.active = playerIndex == 1;
        player2Timer.active = playerIndex == 2;

        if (playerIndex == 1) {
            if (player1Particles.isPlaying == false) {
                player1Particles.Play();
                zapAudio.Play();
            }
        } else if (playerIndex == 2) {
            if (player2Particles.isPlaying == false) {
                player2Particles.Play();
                zapAudio.Play();
            }
        }
    }

    public int ActivePlayerIndex() {
        if (player1Timer.active) {
            return 1;
        } else if (player2Timer.active) {
            return 2;
        } else {
            return -1;
        }
    }
}
