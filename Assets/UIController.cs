using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // +++++++++++++++++++++++++ ATTRIBUTES +++++++++++++++++++++++++
    [SerializeField] private Button playButton;


    // +++++++++++++++++++++++++ METHODS +++++++++++++++++++++++++
    private void Start()
    {
        playButton.onClick.AddListener(PlayButtonOnClick);
    }

    private void PlayButtonOnClick()
    {
        // Take to scene level1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        Debug.Log("Play button clicked");

    }
}
