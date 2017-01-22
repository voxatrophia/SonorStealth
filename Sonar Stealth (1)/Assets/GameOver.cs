using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public GameObject submarine;
    public Camera mainCamera;
    public AudioClip destroySFX;
    AudioSource aud;

	// Use this for initialization
	void Start () {
        aud = GetComponent<AudioSource>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerGameOver()
    {
        mainCamera.transform.parent = transform;
        aud.PlayOneShot(destroySFX);
        DestroyObject(submarine, .2f);
    }
}
