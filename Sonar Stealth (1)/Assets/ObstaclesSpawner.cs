using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour {

    public GameObject[] Tiles;
    public int numTiles = 5;
    public int offset = 150;

	// Use this for initialization
	void Start () {
		for (int i=0; i<numTiles; i++)
        {
            int randomTile = Random.Range(0, Tiles.Length);
            GameObject nextTile = Instantiate(Tiles[randomTile]);
            Vector3 location = nextTile.transform.position;
            location.z += i * offset;
            nextTile.transform.position = location;
        }

   
	}

}
