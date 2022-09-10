using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private GameObject player;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameOver && player != null && collision.collider.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            disablePlayerMovement();
            centrePlayerOnMe();
            showFinalScore();
            gameOver = true;
        }
    }

    private void showFinalScore()
    {
        Scorer scorer = player.GetComponent<Scorer>();
        if (scorer == null)
        {
            return;
        }

        scorer.logFinalScore();
    }

    private void disablePlayerMovement()
    {

        player.GetComponent<Mover>().enabled = false;
    }

    private void centrePlayerOnMe()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 position = transform.position;
        Vector3 newPlayerPosition = new Vector3(position.x, playerPosition.y, position.z);
        player.transform.position = newPlayerPosition;
    }
}
