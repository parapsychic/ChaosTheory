using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int numberOfBullets;
    [SerializeField] float cooldownTime = 2f;
    [SerializeField] bool isRandom;
    [SerializeField] float randomOffset = 10f;


    float cooldown = 0f;
    float[] rotations;
    float randomDifference;

    /*OBJECT POOLING LOGIC*/
    [SerializeField] List<GameObject> bulletPool;
    [SerializeField] bool isPoolExpandable =false;


    // Start is called before the first frame update
    void Start()
    {
        rotations = new float[numberOfBullets];
        randomDifference = DistributeRotations();

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            SpawnBullets();
            cooldown = cooldownTime;
        }
        cooldown -= Time.deltaTime;
    }

    float DistributeRotations()
    {
        float angleDifference = 360f / numberOfBullets;
        float initialAngle = 0f;
        for(int i=0; i<numberOfBullets; i++)
        {
            rotations[i] = initialAngle + (angleDifference * i);
            print(rotations[i]);
        }
        return angleDifference;


    }

    void RandomizeAngles()
    {

        for (int i = 0; i < numberOfBullets; i++)
        {
            rotations[i] += Random.Range(-randomDifference + randomOffset, randomDifference - randomOffset);
        }
    }

    GameObject CheckForAvailableBullets()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                print("checking");
                return bulletPool[i];
            }
        }

        if (isPoolExpandable)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        return null;
    }

    void SpawnBullets()
    {
        if (isRandom)
        {
            RandomizeAngles();
        }

        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bullet = CheckForAvailableBullets();
            if (bullet!=null)
            {
                print("got bullet");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, rotations[i]);
                bullet.GetComponent<BulletBehaviour>().Activate();
            }
            

        }
       
    }
    
}
