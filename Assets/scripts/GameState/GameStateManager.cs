using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
    private HitpointTimer player1Timer;
    private HitpointTimer player2Timer;

    [SerializeField] ParticleSystem player1ParticlesPrefab;
    [SerializeField] ParticleSystem player2ParticlesPrefab;

    [SerializeField] GameObject player1SparkNode;
    [SerializeField] GameObject player2SparkNode;

    public GameState state;

    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioSource zapAudio;

    [SerializeField] private AudioSource victoryMusic;

    [SerializeField] private AudioSource player1Music;
    [SerializeField] private AudioSource player2Music;
    private bool timerMusicPlaying = false;

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

        Invoke("BeginGame", 2);
    }

    private void BeginGame() {
        state = GameState.ongoing;
    }

    void Update() {

        if (player1Timer.time <= 0 || player2Timer.time <= 0) {
            state = GameState.ended;
            winnerPlayerIndex = player1Timer.time <= 0 ? 1 : 2;
            if (victoryMusic.isPlaying == false) {
                victoryMusic.Play();
            }
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

        if (timerMusicPlaying == false) {
            timerMusicPlaying = true;
            player1Music.Play();
            player2Music.Play();
        }

        if (playerIndex == 1) {
            ParticleSystem sparks = Instantiate<ParticleSystem>(player1ParticlesPrefab);
            sparks.transform.position = player1SparkNode.transform.position;
            player1Music.volume = 0;
            player2Music.volume = 1;
            zapAudio.Play();
        } else if (playerIndex == 2) {
            ParticleSystem sparks = Instantiate<ParticleSystem>(player2ParticlesPrefab);
            sparks.transform.position = player2SparkNode.transform.position;
            player1Music.volume = 1;
            player2Music.volume = 0;
            zapAudio.Play();
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
