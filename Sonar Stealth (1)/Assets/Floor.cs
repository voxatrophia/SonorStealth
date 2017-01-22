using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {



	public void explode() {
		GameObject.FindObjectOfType<Explosion>().detonate (new Vector3 (0f, 5f, 0f), 10);
	}
}
