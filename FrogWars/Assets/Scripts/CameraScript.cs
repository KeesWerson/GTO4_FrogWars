﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameManager gameManager;
    private ResourceManager resourceManager;

    private Vector3 player1_lastPosition = new Vector3();
    private Vector3 player2_lastPosition = new Vector3();

    private bool isFirstRound = true;

    public float cameraSpeed;

	// Use this for initialization
	void Start () {
        resourceManager = FindObjectOfType<ResourceManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            //Moving forwards
            if (gameManager.isPlayerOnesTurn)
            {
                transform.Translate(new Vector3(1, 0, 1) * Time.deltaTime * cameraSpeed, Space.World);
            }
            else
            {
                transform.Translate(new Vector3(-1, 0, -1) * Time.deltaTime * cameraSpeed, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Moving left
            if (gameManager.isPlayerOnesTurn)
            {
                transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime * cameraSpeed, Space.World);
            }
            else
            {
                transform.Translate(new Vector3(1, 0, -1) * Time.deltaTime * cameraSpeed, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Moving backwards
            if (gameManager.isPlayerOnesTurn)
            {
                transform.Translate(new Vector3(-1, 0, -1) * Time.deltaTime * cameraSpeed, Space.World);
            }
            else
            {
                transform.Translate(new Vector3(1, 0, 1) * Time.deltaTime * cameraSpeed, Space.World);
            }         
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Moving right
            if (gameManager.isPlayerOnesTurn)
            {
                transform.Translate(new Vector3(1, 0, -1) * Time.deltaTime * cameraSpeed, Space.World);
            }
            else
            {
                transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime * cameraSpeed, Space.World);
            }
        }
             
	}

    /// <summary>
    /// Change the turn of the players
    /// Also turns the camera to the other players perspective.
    /// </summary>
    public void nextPlayer()
    {
        Debug.Log("NEXTPLAYAAR");
        if (gameManager.isPlayerOnesTurn)
        {
            //Saving last position
            player1_lastPosition = gameObject.transform.position;
            if (isFirstRound)
            {
                transform.position = (new Vector3(gameManager.fieldLenght + 1, 4, gameManager.fieldLenght + 1));
                isFirstRound = false;
            }
            else
            {
                transform.position = player2_lastPosition;
            }
            transform.rotation = Quaternion.Euler(30, 225, 0);
            gameManager.isPlayerOnesTurn = false;
            Debug.Log("After player ones turn: " + gameManager.isPlayerOnesTurn);
        }
        else
        {
            //Saving last position
            player2_lastPosition = gameObject.transform.position;
            //transform.position = (new Vector3(-2, 4, -2));   
            transform.position = player1_lastPosition;
            transform.rotation = Quaternion.Euler(30, 45, 0);
            gameManager.isPlayerOnesTurn = true;
            Debug.Log("After player twos turn: " + gameManager.isPlayerOnesTurn);
        }
        resourceManager.updateInterface();
    }
}
