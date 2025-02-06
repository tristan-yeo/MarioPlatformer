using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D marioBody;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;
    public GameOverManager gameOverManager;
    // Start is called before the first frame update
    void Start()
    {
        // Set game to 30 fps
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown("a") && faceRightState){
          faceRightState = false;
          marioSprite.flipX = true;
      }

      if (Input.GetKeyDown("d") && !faceRightState){
          faceRightState = true;
          marioSprite.flipX = false;
      }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0) {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {

            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState) {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with goomba!");
            Time.timeScale = 0.0f;
            gameOverManager.ShowGameOverScreen();
        }
    }

     public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-1.91f, -1.24f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        // reset score
        jumpOverGoomba.score = 0;
        // reset game over screen
        gameOverManager.HideGameOverScreen();

    }

}
