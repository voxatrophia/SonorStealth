using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SonarPing : MonoBehaviour {

    [Header("Ping Out")]
    public float maxRange;
    public float outDuration;
    public float outDelay;
    [Header("Ping in")]
    public float minRange; //zero
    public float inDuration;
    public float inDelay;
    [Space(5)]
    public float fadeDelay;


    public float maxBrightness;
    public float minBrightness;
    public float onDelay;
    public float offDelay;
    public float transitionTime;

    public GameObject directionalLight;

    float waitTime;
    Light lt;

    void Awake() {
        lt = GetComponent<Light>();
        lt.range = 0;

        directionalLight.SetActive(false);

        waitTime = outDelay + outDuration + inDuration;
    }

    void Start() {
        //StartCoroutine(Transition(true));
        StartCoroutine(SonarPulse());
    }

    IEnumerator SonarPulse() {
        while (true) {
            StartCoroutine(PingOut());
            yield return new WaitForSeconds(waitTime + fadeDelay);
            directionalLight.SetActive(false);
            yield return new WaitForSeconds(inDelay);
        }
    }


    IEnumerator PingOut() {
        //play sound effect
        directionalLight.SetActive(true);
        float initial = lt.range;
        float target = maxRange;
        float startTime = Time.time;
        float endTime = startTime + outDuration;

        while (endTime >= Time.time)
        {
            lt.range = Mathf.Lerp(initial, target, (Time.time - startTime) / outDuration);
            yield return null;
        }
        lt.range = target;

        yield return new WaitForSeconds(outDelay);

        StartCoroutine(PingIn());
    }

    IEnumerator PingIn()
    {
        float initial = lt.range;
        float target = 0f;
        float startTime = Time.time;
        float endTime = startTime + inDuration;

        while (endTime >= Time.time)
        {
            lt.range = Mathf.Lerp(initial, target, (Time.time - startTime) / inDuration);
            yield return null;
        }
        lt.range = 0;
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
