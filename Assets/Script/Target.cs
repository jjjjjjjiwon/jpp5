using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    public ParticleSystem explosionParticle;

    GameManager gm;
    private Rigidbody targetRb;
    private float minSpeed = 20f;
    private float MaxSpped = 25;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -6f;
    private bool isCreate;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        if (!isCreate)
        {
            transform.position = RandomSpawnPos();
        }

    }


    public void SetPosition(Vector3 pos)
    {
        isCreate = true;
        transform.position = pos;
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, MaxSpped);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    void OnMouseDown()
    {
        if (gm.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gm.UpdateScore(pointValue);
            if (gameObject.CompareTag("CREATE"))
            {
                gm.CreateTarget(transform.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Bad"))
        {
            gm.GameOver(true);
        }
    }

}
