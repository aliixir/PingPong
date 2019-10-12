using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ballController : MonoBehaviour
{
    public Rigidbody2D rbBall;
    public SpriteRenderer scoreCat_0, scoreDog_0;
    private AudioSource bark, meow, paddleWallSound;
    public AudioSource[] audioSource;
    public float constSpeed = 10;
    public bool inPlay, lastHitLeft, lastHitRight;
    public int range;
    Vector2[] direction = {
      new Vector2(2, 1),
      new Vector2(-2, 1)
    };
    public Text catScore, dogScore, instructions, catPlay, dogPlay;
    private int catCount, dogCount;

    // Start is called before the first frame update
    void Start()
    {
      range = Random.Range(0, 2); // return 0 or 1, second param is exclusive
      rbBall = gameObject.GetComponent<Rigidbody2D>();
      scoreCat_0.enabled = false;
      scoreDog_0.enabled = false;
      audioSource = GetComponents<AudioSource>();
      meow = audioSource[0];
      bark = audioSource[1];
      paddleWallSound = audioSource[2];
    }

    // Update is called once per frame
    void Update()
    {
      rbBall.velocity = constSpeed * (rbBall.velocity.normalized);
      // will not initially move
      if (!inPlay) {
        transform.position = new Vector2(0.0f, 2.0f);
      }
      // only will start when you press space
      if(Input.GetKey(KeyCode.Space)) {
        // initially make "Press space" text invisible
        inPlay = true;
        if (lastHitLeft) {
          rbBall.AddForce (direction[1]); // go left since they made the score
        } else if (lastHitRight) {
          rbBall.AddForce (direction[0]); // go right since they made the score
        } else {
          instructions.gameObject.SetActive(false);
          catPlay.gameObject.SetActive(false);
          dogPlay.gameObject.SetActive(false);
          rbBall.AddForce(direction[range]); // the beginning, will chose a random direction to go
        }
        scoreCat_0.enabled = false;
      }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {
      GameObject ball = collisionInfo.gameObject;
      this.onHitPaddle(ball);
      this.onHitWall(ball);
      // if ball goes constantly vertical or horizontal reset velocity
      if (System.Math.Round(rbBall.velocity.x) <= 1 && System.Math.Round(rbBall.velocity.x) >= -1 || System.Math.Round(rbBall.velocity.y) == 0) {
        if (lastHitLeft) {
          rbBall.velocity = direction[0]; // go right if the ball last hit the left paddle
        }
        if (lastHitRight) {
          rbBall.velocity = direction[1]; // go left if the ball last hit the right paddle
        }
      }
    }

    void onHitPaddle(GameObject ball) {
      bool leftPaddle = ball.CompareTag ("catPlayer");
      bool rightPaddle = ball.CompareTag ("dogPlayer");
      bool topBorder = ball.CompareTag ("wall");
      if (topBorder) {
        paddleWallSound.Play();
      }
      if (leftPaddle) {
        meow.Play();
        lastHitLeft = true;
        lastHitRight = false;
      }

      if (rightPaddle) {
        bark.Play();
        lastHitRight = true;
        lastHitLeft = false;
      }
    }

    void onHitWall(GameObject ball) {
      bool rightBorder = ball.CompareTag ("right-border");
      bool leftBorder = ball.CompareTag ("left-border");
      // bool bottomBorder = ball.CompareTag ("bottom-border");


      if (rightBorder) {
        meow.Play();
        inPlay = false;
        catCount++;
        catScore.text = catCount.ToString();
        StartCoroutine(WaitForCat());
      }

      if (leftBorder) {
        bark.Play();
        inPlay = false;
        dogCount++;
        dogScore.text = dogCount.ToString();
        StartCoroutine(WaitForDog());
      }

      if(dogCount == 5) {
        SceneManager.LoadScene("DogWins");
      }
      if(catCount == 5) {
        SceneManager.LoadScene("CatWins");
      }
    }

    IEnumerator WaitForCat()
    {
        scoreCat_0.enabled = true;
      yield return new WaitForSeconds(1);
        scoreCat_0.enabled = false;

    }

        IEnumerator WaitForDog()
    {
        scoreDog_0.enabled = true;
      yield return new WaitForSeconds(1);
        scoreDog_0.enabled = false;

    }
}
