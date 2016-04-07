using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UnitManager : MonoBehaviour {

    private TileManager tileManager;
    private GameManager gameManager;
    private CameraScript cameraScript;

    public Text _currentText;
    public Text currentMode;
    private GameObject currentSelctedUnit;

    public GameObject unit;
    public GameObject unitContainer;

    public Material player1_glow;
    public Material player1_base;
    public Material player2_base;
    public Material player2_glow;

    public bool isMoving = true;

	// Use this for initialization
	void Start () {
        tileManager = FindObjectOfType<TileManager>();
        gameManager = FindObjectOfType<GameManager>();
        cameraScript = FindObjectOfType<CameraScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void toggleAttackMode()
    {
        if (isMoving)
        {
            isMoving = false;
            currentMode.text = "Change to move";
        }
        else
        {
            isMoving = true;
            currentMode.text = "Change to attack";
        }
    }

    /// <summary>
    /// Called when the user clicks on an unit.
    /// It will register the unit as current selected unit and give that player an outerglow.
    /// If there already is a selected unit, the previous selected one will be deselected.
    /// </summary>
    /// <param name="unit">The selected unit</param>
    public void selectCurrentUnit(GameObject unit)
    {
        //If there already is a selected unit, that one will be deselected.
        if (currentSelctedUnit != null)
        {
            Debug.Log("1: " + currentSelctedUnit.name);
            Debug.Log("2: " + unit.name);
            //Clear the lit up fields
            if (isMoving)
            {
                tileManager.clearMovableFields();
            }
            else
            {
                tileManager.clearAttackableFields();
            }
            //If the player selects an already selected unit, this if will be true.
            if (currentSelctedUnit.name.Equals(unit.name))
            {
                togglePlayerColor(unit, "base");
                currentSelctedUnit = null;
                return;
            }
            togglePlayerColor(unit, "base"); 
        }
        currentSelctedUnit = unit;
        if (currentSelctedUnit != null)
        {
            //Showing all movable fields
            if (togglePlayerColor(unit, "glow"))
            {
                if (isMoving)
                {
                    tileManager.showMovableFields(unit);
                }
                else
                {
                    //Show all attackable fields
                    tileManager.showAttackableFields(unit);                
                }
            }            
        }
    }

    /// <summary>
    /// Toggels the color of the frogs.
    /// Basicly changing the materials to the ones the player wants.
    /// </summary>
    /// <param name="currentUnit">The object that needs to change</param>
    /// <param name="typeOfGlow">"glow" if the player needs to glow, "base" if the player needs to get his base material</param>
    /// <returns></returns>
    public bool togglePlayerColor(GameObject currentUnit, string typeOfGlow)
    {
        Debug.Log("Player 1 turn?: " + gameManager.isPlayerOnesTurn);
        Debug.Log("Type of glow: " + typeOfGlow);
        Debug.Log("Material: " + currentUnit.GetComponentInChildren<MeshRenderer>().material.name);
        Debug.Log("Player 1 base material" + player1_base.name);

        if (gameManager.isPlayerOnesTurn && typeOfGlow == "base" && currentUnit.GetComponentInChildren<MeshRenderer>().material.name.Contains(player1_glow.name))
        {
            currentUnit.GetComponentInChildren<MeshRenderer>().material = player1_base;
            return true;
        }
        else if (!gameManager.isPlayerOnesTurn && typeOfGlow == "base" && currentUnit.GetComponentInChildren<MeshRenderer>().material.name.Contains(player2_glow.name))
        {
            currentUnit.GetComponentInChildren<MeshRenderer>().material = player2_base;
            return true;
        }
        else if (gameManager.isPlayerOnesTurn && typeOfGlow == "glow" && currentUnit.GetComponentInChildren<MeshRenderer>().material.name.Contains(player1_base.name))
        {
            currentUnit.GetComponentInChildren<MeshRenderer>().material = player1_glow;
            return true;
        }
        else if (!gameManager.isPlayerOnesTurn && typeOfGlow == "glow" && currentUnit.GetComponentInChildren<MeshRenderer>().material.name.Contains(player2_base.name))
        {
            currentUnit.GetComponentInChildren<MeshRenderer>().material = player2_glow;
            return true;
        }
        else { Debug.Log("DIT IS NIET JOUW KIKKER"); }
        return false;
    }

    /// <summary>
    /// Tries to move a selected unit to the field the player just clicked on.
    /// If none unit is selected it will display an error message.
    /// </summary>
    /// <param name="tile">The tile the player clicks on</param>
    public void moveToField(GameObject tile)
    {
        if (currentSelctedUnit == null)
        {
            _currentText.text = "Selecteer eerst een unit!";
        }
        else
        {
            if (!tileManager.movableFields.Contains(tile))
            {
                _currentText.text = "Je kunt daar niet heen!";
            }
            else
            {
                if (tile.GetComponentInChildren<MeshRenderer>().material.name.Contains("bad_glow"))
                {
                    _currentText.text = "Dat is water meneertje!";
                }
                else
                {
                    Vector3 position = tile.GetComponent<Transform>().position;
                    position.y += 1;
                    GameObject attackedFrog = null;
                    //Checks if the player would land on a frog of his own
                    if(gameManager.isPlayerOnesTurn && findFroggyOnField(gameManager.player1_Units, position) != null
                        || !gameManager.isPlayerOnesTurn && findFroggyOnField(gameManager.player2_Units, position) != null)
                    {
                        _currentText.text = "Dat is je eigen kikker, knuppel...";
                    }
                    else
                    {
                        //Checks if the player lands on an oponents frog
                        if (gameManager.isPlayerOnesTurn)
                        {
                            attackedFrog = findFroggyOnField(gameManager.player2_Units, position);
                        }
                        else
                        {
                            attackedFrog = findFroggyOnField(gameManager.player1_Units, position);
                        }
                        if (attackedFrog != null)
                        {
                            Debug.Log("KIIIIIILLLLLL");
                            attackedFrog.GetComponent<Unit>().dieInPeace();
                        }

                        //Clears all lit fields
                        tileManager.clearMovableFields();

                        //New position and ending turn
                        Vector3 newPosition = new Vector3(tile.GetComponent<Transform>().position.x, tile.GetComponent<Transform>().position.y + 1, tile.GetComponent<Transform>().position.z);
                        currentSelctedUnit.transform.position = newPosition;
                        togglePlayerColor(currentSelctedUnit, "base");
                        currentSelctedUnit = null;
                        cameraScript.nextPlayer();
                    }                
                }
            }          
        }
    }

    /// <summary>
    /// Tries to do an attack on the clicked tile.
    /// </summary>
    /// <param name="tile">The tile the player clicks on</param>
    public void attackField(GameObject tile)
    {
        Vector3 position = tile.GetComponent<Transform>().position;
        position.y += 1;
        //Vector2 xzPosition = new Vector2(position.x, position.z);

        if (currentSelctedUnit == null)
        {
            _currentText.text = "Selecteer eerst een unit!";
        }
        else
        {
            if (!tileManager.movableFields.Contains(tile))
            {
                _currentText.text = "Je kunt daar niet aanvallen!";
            }
            else
            {
                //FIND AND DESTROY FROG >:D
                GameObject attackedFrog = null;
                if (gameManager.isPlayerOnesTurn)
                {
                    attackedFrog = findFroggyOnField(gameManager.player2_Units, position);       
                }
                else
                {
                    attackedFrog = findFroggyOnField(gameManager.player1_Units, position);           
                }
                if (attackedFrog != null)
                {
                    attackedFrog.GetComponent<Unit>().dieInPeace();
                }  

                //Clears all lit fields
                tileManager.clearAttackableFields();
                togglePlayerColor(currentSelctedUnit, "base");
                currentSelctedUnit = null;
                cameraScript.nextPlayer();
            }
        }
    }

    /// <summary>
    /// Try to find in an array of frogs, a frog with the same position
    /// as the given field.
    /// </summary>
    /// <param name="froggys">List of the frogs you want to search through.</param>
    /// <param name="field">Field you want to find a frog on.</param>
    /// <returns>GameObject frog if there is a match, null if there is no match.</returns>
    public GameObject findFroggyOnField(List<GameObject> froggys, Vector3 field)
    {
        foreach (GameObject frog in froggys)
        {
            if (frog.GetComponent<Transform>().position == field)
            {
                Debug.Log("FOUND YOU " + frog.name);
                return frog;
            }
        }
        return null;
    }

    public bool spawnUnit(string frogType, GameObject selectedTile)
    {
        Debug.Log("JAJAJAJAJJAAJAJAJJAAJJAAJJA");
        Vector3 position = selectedTile.GetComponent<Transform>().position;
        position.y += 1;
        GameObject currentUnit = (GameObject)Instantiate(unit, position, Quaternion.identity);
        Debug.Log("NAME: " + frogType);
        if (gameManager.isPlayerOnesTurn)
        {
            currentUnit.GetComponentInChildren<MeshRenderer>().material = player1_base;
            currentUnit.GetComponentInChildren<Unit>().playerNumber = 1;
        }
        else
        {
            currentUnit.GetComponentInChildren<MeshRenderer>().material = player2_base;
            currentUnit.GetComponentInChildren<Unit>().playerNumber = 2;
        }
        currentUnit.GetComponent<Unit>().unitName = frogType;
        currentUnit.transform.SetParent(unitContainer.transform);

        //Adding the unit to the array of the right player
        if (gameManager.isPlayerOnesTurn)
        {
            gameManager.player1_Units.Add(currentUnit);
        }
        else
        {
            gameManager.player2_Units.Add(currentUnit);
        }
        return true;
    }
}
