using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int fieldLenght;
    public int baseLength;
    public int badFieldIndex;
    public List<GameObject> tiles = new List<GameObject>();
    public List<Vector2> tilesPositions = new List<Vector2>();
    public GameObject tile;
    public GameObject tileContainer;
    public List<GameObject> player1_Units = new List<GameObject>();
    public List<GameObject> player2_Units = new List<GameObject>();
    public Material player1_base;
    public Material player2_base;
    public Material bad_field;

    public bool isPlayerOnesTurn { get; set; }

	// Use this for initialization
	void Start () {
        isPlayerOnesTurn = true;
        if (fieldLenght > 15)
        {
            fieldLenght = 15;
        }
        generateGrid();       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Generates the base grid.
    /// This will be called at the start of a game.
    /// Random 'bad_fields' will be generated depending on the badFieldIndex.
    /// (the higher the index, the more badFields will be generated).
    /// </summary>
    public void generateGrid()
    {
        for (int z = 0; z < fieldLenght; z++)
        {
            for (int x = 0; x < fieldLenght; x++)
            {
                bool isBaseTile = false;
                Vector3 positie = new Vector3(x, 0, z);
                GameObject currentTile = (GameObject)Instantiate(tile, positie, Quaternion.identity);

                //If the tile is in player 1 base
                if (x < baseLength && z < baseLength)
                {
                    currentTile.GetComponentInChildren<MeshRenderer>().material = player1_base;
                    isBaseTile = true;
                }
                //If the tile is in player 2 base
                if (x >= fieldLenght - baseLength && z >= fieldLenght - baseLength)
                {
                    currentTile.GetComponentInChildren<MeshRenderer>().material = player2_base;
                    isBaseTile = true;
                }

                //When the tile is not marked as 'base_tile'
                //Randomly makes 'bad' fields.
                if (!isBaseTile)
                {
                    if (Random.Range(0, 25) < badFieldIndex)
                    {
                        currentTile.GetComponentInChildren<MeshRenderer>().material = bad_field;
                    }
                }
                
                tiles.Add(currentTile);
                tilesPositions.Add(new Vector2(currentTile.GetComponent<Transform>().position.x, currentTile.GetComponent<Transform>().position.z));
                currentTile.transform.SetParent(tileContainer.transform);
            }
        }
       
    }

}
