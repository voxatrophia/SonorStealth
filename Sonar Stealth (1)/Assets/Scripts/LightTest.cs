using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTest : MonoBehaviour {

    [Header("Spotlight")]
    public float maxAngle = 40;
    public float maxIntensity = 1;
    public float spotlightDuration = 1;
    public float fadeDelay = 0.5f;
    Light spotlight;

    [Header("Point Light")]
    public Light pointLight;
    public float maxPointLight = 2;
    public float pointLightDuration = 1;

    [Header("Light Blocker")]
    public Transform blocker;
    public float maxBlockSize = 3;
    public float blockDuration = 1.5f;
    Vector3 maxBlocker;

    bool isRunning;
    float waitTime;

    void Awake() {
        spotlight = GetComponent<Light>();
        spotlight.intensity = 0;
        spotlight.spotAngle = 0;

        pointLight.intensity = 0;
        blocker.localScale = Vector3.zero;

        maxBlocker = new Vector3(maxBlockSize, maxBlockSize, maxBlockSize);
        waitTime = spotlightDuration + fadeDelay + pointLightDuration;        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Ping());
        }
    }

    IEnumerator Ping(){
        while (true) {
            StartCoroutine(MakeWave());
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MakeWave() {
        if (!isRunning) {
            isRunning = true;
            StartCoroutine(LightWave());
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ExpandBlocker());
        }
    }

    IEnumerator LightWave()
    {
        //Start wave by increasing spotlight angle over time
        spotlight.intensity = maxIntensity;
        float initial = spotlight.spotAngle;
        float startTime = Time.time;
        float endTime = startTime + spotlightDuration;

        while (endTime >= Time.time)
        {
            float t = (Time.time - startTime) / spotlightDuration;
            spotlight.spotAngle = Mathf.Lerp(initial, maxAngle, t);
            pointLight.intensity = Mathf.Lerp(0, maxPointLight, t);
            yield return null;
        }
        spotlight.spotAngle = maxAngle;
        pointLight.intensity = maxPointLight;

        //Stay lighted - need variable for this
        yield return new WaitForSeconds(fadeDelay);

        startTime = Time.time;
        endTime = startTime + spotlightDuration;

        //Fade out intensity
        StartCoroutine(FadePointLight());
        while (endTime >= Time.time)
        {
            float t = (Time.time - startTime) / spotlightDuration;
            spotlight.intensity = Mathf.Lerp(maxIntensity, 0, t);
            //pointLight.intensity = Mathf.Lerp(maxPointLight, 0, t);
            yield return null;
        }
        spotlight.intensity = 0;
        spotlight.spotAngle = 0;
        //pointLight.intensity = 0;
        //isRunning = false;
    }

    IEnumerator ExpandBlocker() {
        blocker.transform.localScale = Vector3.zero;
        float startTime = Time.time;
        float endTime = startTime + blockDuration;

        while (endTime >= Time.time)
        {
            float t = (Time.time - startTime) / blockDuration;
            blocker.localScale = Vector3.Lerp(Vector3.zero, maxBlocker, t);
            yield return null;
        }
        blocker.localScale = maxBlocker;
    }

    IEnumerator FadePointLight() {
        float startTime = Time.time;
        float endTime = startTime + pointLightDuration;

        //Fade out intensity
        while (endTime >= Time.time)
        {
            float t = (Time.time - startTime) / pointLightDuration;
            pointLight.intensity = Mathf.Lerp(maxPointLight, 0, t);
            yield return null;
        }
        pointLight.intensity = 0;
        isRunning = false;
    }

    //IEnumerator FadeOutline() {
    //    outlineLight.intensity = 0.1f;
    //    float startTime = Time.time;
    //    float endTime = startTime + spotlightDuration;

    //    while (endTime >= Time.time)
    //    {
    //        float t = (Time.time - startTime) / spotlightDuration;
    //        outlineLight.intensity = Mathf.Lerp(0.1f, 0, t);
    //        yield return null;
    //    }
    //}

}
