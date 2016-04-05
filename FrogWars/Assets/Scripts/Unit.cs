using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Unit : MonoBehaviour {

    private UnitManager unitManager;
    private GameManager gameManager;

    public string unitName = "Frog";
    public int playerNumber;
    public int unitHealth;
    public int attackPower;
    public List<Vector2> movableFields = new List<Vector2>();
    public List<Vector2> attackableFields = new List<Vector2>();

	// Use this for initialization
	void Start () {
        unitManager = FindObjectOfType<UnitManager>();
        gameManager = FindObjectOfType<GameManager>();
        initializeFrog();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void selectThisUnit()
    {
        Debug.Log(gameObject.name);
        unitManager.selectCurrentUnit(gameObject);   
    }

    public void dieInPeace()
    {
        Debug.Log("AAAARRRGG AUTSJH");
        if (gameManager.isPlayerOnesTurn)
        {
            gameManager.player2_Units.Remove(gameObject);
        }
        else
        {
            gameManager.player1_Units.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    public void initializeFrog()
    {
        movableFields.Clear();
        attackableFields.Clear();

        Debug.Log(unitName);
        //First you gotta determine the movable fields corresponding to the right type of frog.
        if (unitName == "Frog")
        {
            //Movement
            movableFields.Add(new Vector2(1, 1));
            movableFields.Add(new Vector2(1, 0));
            movableFields.Add(new Vector2(1, -1));
            movableFields.Add(new Vector2(0, -1));
            movableFields.Add(new Vector2(-1, -1));
            movableFields.Add(new Vector2(-1, 0));
            movableFields.Add(new Vector2(-1, 1));
            movableFields.Add(new Vector2(0, 1)); 
            //Attack
            attackableFields.Add(new Vector2(1, 1));
            attackableFields.Add(new Vector2(1, 0));
            attackableFields.Add(new Vector2(0, 1)); 
            //Other statistics
            unitHealth = 3;
        }
        if (unitName == "CheckerFrog")
        {
            movableFields.Add(new Vector2(1, 1));
            movableFields.Add(new Vector2(2, 0));
            movableFields.Add(new Vector2(1, -1));
            movableFields.Add(new Vector2(0, -2));
            movableFields.Add(new Vector2(-1, -1));
            movableFields.Add(new Vector2(-2, 0));
            movableFields.Add(new Vector2(-1, 1));
            movableFields.Add(new Vector2(0, 2));
            //Attack
            attackableFields.Add(new Vector2(1, 0));
            attackableFields.Add(new Vector2(0, -1));
            attackableFields.Add(new Vector2(-1, 0));
            attackableFields.Add(new Vector2(0, 1));
            //Other statistics
            unitHealth = 2;
        }
        if (unitName == "HorseFrog")
        {
            movableFields.Add(new Vector2(1, 2));
            movableFields.Add(new Vector2(2, 1));
            movableFields.Add(new Vector2(2, -1));
            movableFields.Add(new Vector2(1, -2));
            movableFields.Add(new Vector2(-1, -2));
            movableFields.Add(new Vector2(-2, -1));
            movableFields.Add(new Vector2(-2, 1));
            movableFields.Add(new Vector2(-1, 2));
            //Attack
            attackableFields.Add(new Vector2(1, 1));
            attackableFields.Add(new Vector2(1, 0));
            attackableFields.Add(new Vector2(0, 1));
            //Other statistics
            unitHealth = 1;
        }
    }
}
