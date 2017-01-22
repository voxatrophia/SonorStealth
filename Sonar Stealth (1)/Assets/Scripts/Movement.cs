using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float maxTorque;
    public float maxSpeed;
    Rigidbody subRB;
    public ParticleSystem bubblesPrefab;
    public ParticleSystem explosionPrefab;
    AudioSource subAudio;
    public AudioClip hitRockSFX;
    public AudioClip warningSFX;

    GameOver gameOverScript;
    Health healthScript;

    bool alreadyTorquing = false;

    // Use this for initialization
    void Start() {
        subRB = GetComponent<Rigidbody>();
        subRB.maxAngularVelocity = 1;

        subAudio = GetComponent<AudioSource>();

        //gameOverScript = GameObject.Find("Manager").GetComponent<GameOver>();
        //healthScript = GameObject.Find("Manager").GetComponent<Health>();
        gameOverScript = GameObject.FindObjectOfType(typeof(GameOver)) as GameOver;
        healthScript = GameObject.FindObjectOfType(typeof(Health)) as Health;

    }

    // Update is called once per frame
    void FixedUpdate() {
        bool addedTorque = ApplyTorqueAndForce();
        if (!alreadyTorquing && addedTorque)
        {
//            subAudio.PlayOneShot(groan);
            alreadyTorquing = true;
        }

        if (!addedTorque) { alreadyTorquing = false; }
    }

    bool ApplyTorqueAndForce()
    {
        float turnRight = Input.GetAxis("LeftScrew") * Time.deltaTime;
        float turnLeft = Input.GetAxis("RightScrew") * Time.deltaTime;

        float vertical = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        subRB.AddTorque(transform.up * (turnRight - turnLeft) * maxTorque);
        subRB.AddForce(transform.forward * (turnRight + turnLeft) * maxSpeed);

        bool addedTorque = turnLeft != turnRight;
        return addedTorque;
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        Debug.Log("Hit!");
        if (collisionObject.gameObject.tag == "Rock")
        {
            HitRock();

        }

        if (collisionObject.gameObject.tag == "Mine")
        {
            HitMine(collisionObject.gameObject);
        }
    }


    void HitRock()
    {
        Debug.Log("Hit Rock");
        Vector3 loc = transform.position;
        loc.z -= 7;
        if (bubblesPrefab != null) {
            ParticleSystem bubbles = Instantiate(bubblesPrefab);
            bubbles.transform.position = loc;
        }
        subAudio.PlayOneShot(hitRockSFX);
        healthScript.DeductHealth();
    }

    void HitMine(GameObject mine)
    {
        Debug.Log("Hit Mine");
        Vector3 loc = transform.position;
        loc.z -= 5;
        if (explosionPrefab != null) {
            ParticleSystem fireExplosion = Instantiate(explosionPrefab);
            fireExplosion.transform.position = loc;
        }
        gameOverScript.TriggerGameOver();
        DestroyObject(mine);  
    }
}