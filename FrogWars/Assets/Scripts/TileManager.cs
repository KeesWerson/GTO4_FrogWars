using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TileManager : MonoBehaviour {

    private GameObject currentSelctedTile;
    public List<GameObject> movableFields = new List<GameObject>();
    private GameManager gameManager;
    private UnitManager unitManager;
    //private List<Vector2> 

    //The normal shaders
    public Material player1_field;
    public Material player2_field;
    public Material default_field;
    public Material bad_field;

    //The shaders with outline
    public Material player1_glow;
    public Material player2_glow;
    public Material default_glow;
    public Material bad_glow;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        unitManager = FindObjectOfType<UnitManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// The method will toggle the current selected field and change its material.
    /// It will be called when the cursor of the player ENTERS any field.
    /// </summary>
    /// <param name="tile">The object of the tile you are hovering to</param>
    public void selectCurrentTile(GameObject tile)
    {
        currentSelctedTile = tile;
        if (currentSelctedTile != null)
        {
            string shaderName = tile.GetComponentInChildren<MeshRenderer>().material.name;
            if (shaderName.Contains(player1_field.name))
            {
                currentSelctedTile.GetComponentInChildren<MeshRenderer>().material = player1_glow;
            }
            else if (shaderName.Contains(player2_field.name))
            {
                currentSelctedTile.GetComponentInChildren<MeshRenderer>().material = player2_glow;
            }
            else if (shaderName.Contains(default_field.name))
            {
                currentSelctedTile.GetComponentInChildren<MeshRenderer>().material = default_glow;
            }
            else if (shaderName.Contains(bad_field.name))
            {
                currentSelctedTile.GetComponentInChildren<MeshRenderer>().material = bad_glow;
            }           
        }
    }

    /// <summary>
    /// The method will change the material of the field.
    /// It will be called when the cursor of the player EXITS any field.
    /// </summary>
    /// <param name="tile">The object of the tile you are hovering from</param>
    public void deSelectCurrentTile(GameObject tile)
    {
        if (tile != null)
        {
            string shaderName = tile.GetComponentInChildren<MeshRenderer>().material.name;
            if (shaderName.Contains(player1_glow.name))
            {
                tile.GetComponentInChildren<MeshRenderer>().material = player1_field;
            }
            else if (shaderName.Contains(player2_glow.name))
            {
                tile.GetComponentInChildren<MeshRenderer>().material = player2_field;
            }
            else if (shaderName.Contains(default_glow.name))
            {
                tile.GetComponentInChildren<MeshRenderer>().material = default_field;
            }
            else if (shaderName.Contains(bad_glow.name))
            {
                tile.GetComponentInChildren<MeshRenderer>().material = bad_field;
            }

            //If the tile is in the movableTileList the color will change.
            if (movableFields.Contains(tile))
            {
                Color c = tile.GetComponentInChildren<MeshRenderer>().material.color;
                c.r += .5f;
                if (unitManager.isMoving)
                {                  
                    c.g += .5f;
                    c.b += .5f;
                }
                tile.GetComponentInChildren<MeshRenderer>().material.color = c;
            }
        }
    }

    /// <summary>
    /// This method will find all fields in-game the given unit can move to.
    /// For every location in the movableFieldArray from the unit there will be a check if
    /// that position is on the board based on the units current location. The fields will lit up.
    /// Also, all found fields will be put inside an array for later use.
    /// </summary>
    /// <param name="unit">The unit you want to know the movement of</param>
    public void showMovableFields(GameObject unit)
    {
        float unitPosX = unit.GetComponent<Transform>().position.x;
        float unitPosZ = unit.GetComponent<Transform>().position.z;

        foreach (Vector2 v in unit.GetComponent<Unit>().movableFields)
        {
            if (gameManager.tilesPositions.Contains(new Vector2(unitPosX + v.x, unitPosZ + v.y)))
            {
                Debug.Log("IK HEB JE GEVONDEN: " + v.ToString());
                GameObject obj = gameManager.tiles[(int)(unitPosX + v.x) + (int)((unitPosZ + v.y) * gameManager.fieldLenght)];

                Color c = obj.GetComponentInChildren<MeshRenderer>().material.color;
                c.r += .5f;
                c.g += .5f;
                c.b += .5f;
                obj.GetComponentInChildren<MeshRenderer>().material.color = c;

                movableFields.Add(obj);
            }
        }
    }

    /// <summary>
    /// removes all fields in the current movableFields array. 
    /// All fields will also get their normal colour back.
    /// </summary>
    public void clearMovableFields()
    {
        foreach (GameObject obj in movableFields)
        {
            Color c = obj.GetComponentInChildren<MeshRenderer>().material.color;
            c.r -= .5f;
            c.g -= .5f;
            c.b -= .5f;
            obj.GetComponentInChildren<MeshRenderer>().material.color = c;
        }
        movableFields.Clear();
    }

    /// <summary>
    /// This method will find all fields in-game the given unit can attack.
    /// For every location in the attackableFieldArray from the unit there will be a check if
    /// that position is on the board based on the units current location. The fields will lit up.
    /// Also, all found fields will be put inside an array for later use.
    /// </summary>
    /// <param name="unit">The unit you want to know the attackrange of</param>
    public void showAttackableFields(GameObject unit)
    {
        float unitPosX = unit.GetComponent<Transform>().position.x;
        float unitPosZ = unit.GetComponent<Transform>().position.z;

        foreach (Vector2 v in unit.GetComponent<Unit>().attackableFields)
        {
            if (gameManager.tilesPositions.Contains(new Vector2(unitPosX + v.x, unitPosZ + v.y)))
            {
                Debug.Log("IK HEB JE GEVONDEN: " + v.ToString());
                GameObject obj = gameManager.tiles[(int)(unitPosX + v.x) + (int)((unitPosZ + v.y) * gameManager.fieldLenght)];

                Color c = obj.GetComponentInChildren<MeshRenderer>().material.color;
                c.r += .5f;
                obj.GetComponentInChildren<MeshRenderer>().material.color = c;

                movableFields.Add(obj);
            }
        }
    }

    /// <summary>
    /// removes all fields in the current attackableFields array. 
    /// All fields will also get their normal colour back.
    /// </summary>
    public void clearAttackableFields()
    {
        foreach (GameObject obj in movableFields)
        {
            Color c = obj.GetComponentInChildren<MeshRenderer>().material.color;
            c.r -= .5f;
            obj.GetComponentInChildren<MeshRenderer>().material.color = c;
        }
        movableFields.Clear();
    }
}
