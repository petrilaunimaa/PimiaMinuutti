using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SerializeField] private CanvasGroup creditsCanvasGroup = null;

    private void Update() {
        if (Input.anyKeyDown) {
            creditsCanvasGroup.alpha = 0;
            creditsCanvasGroup.blocksRaycasts = false;
        }
    }


    public void StartButtonPressed() {
        SceneManager.LoadScene("Combined");
    }

    public void CreditsButtonPressed() {
        creditsCanvasGroup.alpha = 1;
        creditsCanvasGroup.blocksRaycasts = true;
    }

    public void QuitButtonPressed() {
        Application.Quit();
    }
}
