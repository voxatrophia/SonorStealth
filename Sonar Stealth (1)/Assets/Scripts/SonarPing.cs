using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SonarPing : MonoBehaviour {

    public float maxBrightness;
    public float minBrightness;
    public float onDelay;
    public float offDelay;
    public float transitionTime;

    Light lt;

    void Awake() {
        lt = GetComponent<Light>();
        lt.range = 0;
    }

    void Start() {
        StartCoroutine(Transition(true));
    }

    IEnumerator Transition(bool turnOn)
    {
        float initialBrightness = lt.range;
        float targetBrightness = turnOn ? maxBrightness : minBrightness;
        float startTime = Time.time;
        float endTime = startTime + transitionTime;

        while (endTime >= Time.time)
        {
            lt.range = Mathf.Lerp(initialBrightness, targetBrightness, (Time.time - startTime) / transitionTime);
            yield return null;
        }

        if (turnOn)
        {
            yield return new WaitForSeconds(onDelay);
        }
        else {
            yield return new WaitForSeconds(offDelay);
        }


        StartCoroutine(Transition(!turnOn));
    }

}
