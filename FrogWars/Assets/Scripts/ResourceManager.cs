using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceManager : MonoBehaviour {

    public Text _moneyText;
    public Text buyingText;
    public Text generalInfo;
    private GameManager gameManager;
    private UnitManager unitManager;

    private int player1Money = 5;
    private int player2Money = 5;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        unitManager = FindObjectOfType<UnitManager>();
        updateInterface();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void selectKindOfUnit(string frogType)
    {
        if (buyingText.text == frogType)
        {
            buyingText.text = "";
            return;
        }
        buyingText.text = frogType;
    }

    public void buyUnit(string frogType, GameObject tile)
    {
        if (frogType == "Frog")
        {
            if (gameManager.isPlayerOnesTurn && player1Money >= 1 || !gameManager.isPlayerOnesTurn && player2Money >= 1)
            {
                //Checks if the player spawns the frog in base.
                if (gameManager.isPlayerOnesTurn && tile.GetComponentInChildren<MeshRenderer>().material.name.Contains("Player1_glow")
                    || !gameManager.isPlayerOnesTurn && tile.GetComponentInChildren<MeshRenderer>().material.name.Contains("player2_glow"))
                {
                    //Plays the unit on selected field
                    if (unitManager.spawnUnit(frogType, tile))
                    {
                        if (gameManager.isPlayerOnesTurn)
                        {
                            player1Money--;
                        }
                        else
                        {
                            player2Money--;
                        }
                        buyingText.text = "";
                    }
                }
                else { generalInfo.text = "Dit is niet het spawnveld"; }          
            }
            else { buyingText.text = "Niet genoeg geld"; }
        }
        else if (frogType == "HorseFrog")
        {
            if (gameManager.isPlayerOnesTurn && player1Money >= 2 || !gameManager.isPlayerOnesTurn && player2Money >= 2)
            {
                //Checks if the player spawns the frog in base.
                if (gameManager.isPlayerOnesTurn && tile.GetComponentInChildren<MeshRenderer>().material.name.Contains("Player1_glow")
                    || !gameManager.isPlayerOnesTurn && tile.GetComponentInChildren<MeshRenderer>().material.name.Contains("player2_glow"))
                {
                    //Plays the unit on selected field
                    if (unitManager.spawnUnit(frogType, tile))
                    {
                        if (gameManager.isPlayerOnesTurn)
                        {
                            player1Money-= 2;
                        }
                        else
                        {
                            player2Money-= 2;
                        }
                        buyingText.text = "";
                    }
                }
                else { generalInfo.text = "Dit is niet het spawnveld"; }
            }
            else { buyingText.text = "Niet genoeg geld"; }
        }
        else if (frogType == "CheckerFrog")
        {
            if (gameManager.isPlayerOnesTurn && player1Money >= 2 || !gameManager.isPlayerOnesTurn && player2Money >= 2)
            {
                //Checks if the player spawns the frog in base.
                if (gameManager.isPlayerOnesTurn && tile.GetComponentInChildren<MeshRenderer>().material.name.Contains("Player1_glow")
                    || !gameManager.isPlayerOnesTurn && tile.GetComponentInChildren<MeshRenderer>().material.name.Contains("player2_glow"))
                {
                    //Plays the unit on selected field
                    if (unitManager.spawnUnit(frogType, tile))
                    {
                        if (gameManager.isPlayerOnesTurn)
                        {
                            player1Money-= 2;
                        }
                        else
                        {
                            player2Money-= 2;
                        }
                        buyingText.text = "";
                    }
                }
                else { generalInfo.text = "Dit is niet het spawnveld"; }
            }
            else { buyingText.text = "Niet genoeg geld"; }
        }
        updateInterface();
    }

    public void updateInterface()
    {
        if (gameManager.isPlayerOnesTurn)
        {
            _moneyText.text = "Money: " + player1Money;
        }
        else
        {
            _moneyText.text = "Money: " + player2Money;
        }
    }
}
