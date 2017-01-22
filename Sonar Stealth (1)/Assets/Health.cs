using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int StartingHealth = 100;
    public int LossPerRock = 10;
    public int WarningThreshhold = 30;
    public int Death = 90;
    public int secondsBetweenHealthDeduction = 4;
    public AudioClip WarningSFX;
    public ParticleSystem explosionPrefab;
    public Slider healthSlider;
    public GameObject healthSliderFill;

    int currentHealth;
    bool healthIsLow = false;
    bool gameIsOver = false;
    AudioSource aud;

    float timeToDeduction = 0;

	// Use this for initialization
	void Start () {
        currentHealth = StartingHealth;
        aud = GetComponent<AudioSource>();
        healthSlider.value = currentHealth;
	}

    void Update()
    {
        if (gameIsOver) { return; }
        timeToDeduction += Time.deltaTime;
        if (timeToDeduction > secondsBetweenHealthDeduction)
        {
            Deduct(1);
            timeToDeduction = 0;
        }
    }
	
    public void DeductHealth()
    {
        Deduct(LossPerRock);
    }

    void Deduct(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (!healthIsLow && (currentHealth < WarningThreshhold))
        {
            aud.PlayOneShot(WarningSFX);
            healthIsLow = true;
            healthSliderFill.GetComponent<Image>().color = Color.yellow;            
        }

        if (currentHealth <= Death)
        {
            gameIsOver = true;
            GetComponent<GameOver>().TriggerGameOver();
            Vector3 loc = transform.position;
            loc.z -= 5;
            ParticleSystem fireExplosion = Instantiate(explosionPrefab);
            fireExplosion.transform.position = loc;
        }
    }
}
