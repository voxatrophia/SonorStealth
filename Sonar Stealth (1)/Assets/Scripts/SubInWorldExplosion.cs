using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubInWorldExplosion : MonoBehaviour {

	public GameObject fireball;
	public GameObject bubble;

	int explosionSize;

	IEnumerator Transition (){
		float DurationTime = 0.2f;
		float startTime = Time.time;
		float endTime = startTime + DurationTime; 
		while (endTime >= Time.time){
			fireball.transform.localScale = Vector3.Lerp(fireball.transform.localScale, (fireball.transform.localScale * explosionSize * 0.9f), (Time.time - startTime) / DurationTime); 
			yield return null;
		}
		startTime = Time.time;
		endTime = startTime + DurationTime; 
		while (endTime >= Time.time){
			fireball.transform.localScale = Vector3.Lerp(fireball.transform.localScale, Vector3.zero, (Time.time - startTime) / DurationTime); 
			yield return null;
		}
	}

	IEnumerator TransitionBubble (){
		float durationTime = 0.4f;
		float startTime = Time.time;
		float endTime = startTime + durationTime; 
		while (endTime >= Time.time){
			bubble.transform.localScale = Vector3.Lerp(bubble.transform.localScale, (bubble.transform.localScale * explosionSize), (Time.time - startTime) / durationTime); 
			yield return null;
		}



		startTime = Time.time;
		endTime = startTime + durationTime; 
		while (endTime >= Time.time){
			bubble.transform.localScale = Vector3.Lerp(bubble.transform.localScale, Vector3.zero, (Time.time - startTime) / durationTime); 
			yield return null;
		}
	}

	void Start () {
//		detonate (new Vector3(0f,0f,0f), 5);
	}

	void Update() {
		if (Input.GetKeyDown("space")) {
			detonate (this.transform.position, 5);
		}
	}

	public void detonate(Vector3 location, int size) {
		//move to vector
		//explode
//		this.transform.position = location;
		explosionSize = size;
		StartCoroutine(Transition());
//		StartCoroutine(TransitionBubble());
	}
}
