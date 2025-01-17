﻿using UnityEngine;

public class Corner : MonoBehaviour
{
    public GameObject whiteBall;
    public bool solidTurn;
    public bool whiteCollided = false;
    public bool wrongBall = false;
    AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!GameMaster.instance.paused)
        {
            checkWhite();
            checkWrong();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sound.Play();
        if (collision.gameObject.CompareTag("solidBall"))
        {
            solidOnCorner(collision);
        }

        if (collision.gameObject.CompareTag("stripedBall"))
        {
            stripedOnCorner(collision);
        }

        if (collision.gameObject.CompareTag("whiteBall"))
        {
            whiteOnCorner(collision);
        }

        if (collision.gameObject.CompareTag("blackBall"))
        {
            blackOnCorner(collision);
        }
    }

    public void stripedOnCorner(Collision2D collision)
    {
        GameMaster.instance.stripedHit = true;
        collision.gameObject.GetComponent<Ball>().isScored = true;

        if (GameMaster.instance.solid)
        {
            GameMaster.instance.stripeScore++;
            whiteBall.SetActive(false);
            GameMaster.instance.whiteIn = true;
            GameMaster.instance.solidHit = false;
            wrongBall = true;
        }
        else if (GameMaster.instance.stripe)
        {
            GameMaster.instance.stripeScore++;
            
        }

        else
        {
            GameMaster.instance.stripe = true;
            GameMaster.instance.stripeScore++;
        }
    }

    public void solidOnCorner(Collision2D collision)
    {
        collision.gameObject.GetComponent<Ball>().isScored = true;
        GameMaster.instance.solidHit = true;
        if (GameMaster.instance.solid)
        {
            GameMaster.instance.solidScore++;
        }
        else if (GameMaster.instance.stripe)
        {
            GameMaster.instance.solidScore++;

            whiteBall.SetActive(false);
            GameMaster.instance.whiteIn = true;
            GameMaster.instance.stripedHit = false;
            wrongBall = true;
        }
        else
        {
            GameMaster.instance.solid = true;
            GameMaster.instance.solidScore++;
        }
    }

    public void blackOnCorner(Collision2D collision)
    {
        collision.gameObject.GetComponent<Ball>().isScored = true;
        whiteBall.SetActive(false);
    }

    public void whiteOnCorner(Collision2D collision)
    {
        whiteCollided = true;
        GameMaster.instance.whiteIn = true;
        collision.gameObject.SetActive(false);
    }

    public void checkWhite()
    {
        if (Input.GetMouseButtonUp(0) && whiteCollided)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hitColliders = Physics2D.OverlapCircle(cursorPos, 0.3f);

            if (hitColliders == null)
            {
                whiteBall.transform.position = cursorPos;
                whiteBall.SetActive(true);
                whiteCollided = false;
            }
        }
    }

    public void checkWrong()
    {
        if (Input.GetMouseButtonDown(0) && wrongBall == true)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hitColliders = Physics2D.OverlapCircle(cursorPos, 0.3f);

            if (hitColliders == null)
            {
                whiteBall.transform.position = cursorPos;
                whiteBall.SetActive(true);
                wrongBall = false;
            }
        }
    }
}
