using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TextMeshProUGUI gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowGameOverScreen()
    {
        // Show the Game Over Canvas
        gameOverCanvas.SetActive(true);
    }
    public void HideGameOverScreen()
    {
        gameOverCanvas.SetActive(false);
    }
}
