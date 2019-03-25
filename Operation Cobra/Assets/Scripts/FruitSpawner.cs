using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public GameObject[] fruitPre;
    public GameObject destroyingObject;
    private float time = Time.deltaTime;
    private float timeLeft = 15;
    private bool status = true;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    IEnumerator SpawnFruit()
    {
        while (true)
        {
            GameObject go = Instantiate(fruitPre[Random.Range(0, fruitPre.Length)]);
            Rigidbody temp = go.GetComponent<Rigidbody>();
            temp.velocity = new Vector3(0f, 5f, .5f);
            temp.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            temp.useGravity = true;
            Vector3 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            go.transform.position = pos;
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            status = false;
        }
        //if (time > 10)
        //{
        //DestroyImmediate(fruitPre[0], true);
        //Destroy(destroyingObject, 15);
        //}
    }
}