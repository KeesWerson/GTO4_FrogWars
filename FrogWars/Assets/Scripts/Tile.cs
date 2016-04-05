using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    private TileManager tileManager;
    private UnitManager unitManager;
    private ResourceManager resourceManager;

	// Use this for initialization
	void Start () {
        tileManager = FindObjectOfType<TileManager>();
        unitManager = FindObjectOfType<UnitManager>();
        resourceManager = FindObjectOfType<ResourceManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void selectThisTile()
    {
        tileManager.selectCurrentTile(gameObject);
    }

    public void de_selectThisTile()
    {
        tileManager.deSelectCurrentTile(gameObject);
    }

    public void clickedOnThisField()
    {
        if (resourceManager.buyingText.text != "")
        {
            resourceManager.buyUnit(resourceManager.buyingText.text, gameObject);
        }
        else
        {
            if (unitManager.isMoving)
            {
                unitManager.moveToField(gameObject);
            }
            else
            {
                unitManager.attackField(gameObject);
            }
        }
    }
}
