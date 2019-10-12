using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleController : MonoBehaviour
{
    private Rigidbody2D catPlayer, dogPlayer;
    GameObject obj;
    public Vector2 vel1, vel2;
    public float y = 10.0f;
    public float boundTop = 3.8f;
    public float boundBottom = 3.8f;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.FindWithTag("catPlayer");
        catPlayer = obj.GetComponent<Rigidbody2D>();
        obj = GameObject.FindWithTag("dogPlayer");
        dogPlayer = obj.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
      this.catPlayerInput();
      this.dogPlayerInput();
      var pos = transform.position;
      if (pos.y > boundTop) { // sets the bound so it does not go over the right wall
        pos.y = boundTop;
      }
      if (pos.y < boundBottom) { // sets the bound so it does not go over the left wall
        pos.y = boundBottom;
      }
      transform.position = pos;
    }

    void catPlayerInput() {
      vel1 = catPlayer.velocity;
      if(Input.GetKey(KeyCode.W)) { // to go up for player 1
        vel1.y = y;
        vel1.x = 0.0f;
      }
      if(Input.GetKey(KeyCode.S)) { // to go down for player 1
        vel1.y = -y;
        vel1.x = 0.0f;
      }

      if(!Input.anyKey) { // if there is no input both of the paddles then should stop moving
        vel1.y = 0.0f;
        vel1.x = 0.0f;
      }
      catPlayer.velocity = vel1;
    }

    void dogPlayerInput() {
      vel2 = dogPlayer.velocity;

      if(Input.GetKey(KeyCode.UpArrow)) { // to go up for player 2
        vel2.y = y;
        vel2.x = 0.0f;
      }
      if(Input.GetKey(KeyCode.DownArrow)) { // to go down for player 2
        vel2.y = -y;
        vel2.x = 0.0f;
      }

      if(!Input.anyKey) { // if there is no input both of the paddles then should stop moving
        vel2.y = 0.0f;
        vel2.x = 0.0f;
      }
      dogPlayer.velocity = vel2;
    }
}
