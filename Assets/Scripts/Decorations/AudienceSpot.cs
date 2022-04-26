using System.Collections.Generic;
using UnityEngine;

public class AudienceSpot : MonoBehaviour
{
    private int choice = 0;
    public bool hasSpawned = false;

    public GameObject Spawn()
    {
        GameObject spawned = this.gameObject;
        List<GameObject> options = new List<GameObject>();

        choice = Random.Range(0, 3);
        if (choice != 0)
        {
            hasSpawned = true;
            int amountOfOptions = AudienceManager.instance.Birdos.Count + AudienceManager.instance.Toads.Count + AudienceManager.instance.ShyGuys.Count+ AudienceManager.instance.Pengus.Count;
            
            choice = Random.Range(0, amountOfOptions);
            if(choice < AudienceManager.instance.Birdos.Count)
            {
                options = AudienceManager.instance.Birdos;
                spawned = options[Random.Range(0, options.Count)];
            }
            else
            {
                choice -= AudienceManager.instance.Birdos.Count;
                if(choice < AudienceManager.instance.Toads.Count)
                {
                    options = AudienceManager.instance.Toads;
                    spawned = options[Random.Range(0, options.Count)];
                }
                else
                {
                    choice -= AudienceManager.instance.Toads.Count;
                    if (choice < AudienceManager.instance.ShyGuys.Count)
                    {
                        options = AudienceManager.instance.ShyGuys;
                        spawned = options[Random.Range(0, options.Count)];
                    }
                    else
                    {
                        options = AudienceManager.instance.Pengus;
                        spawned = options[Random.Range(0, options.Count)];
                    }
                }
            }

            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            return (Instantiate(spawned, transform.position + randomOffset, Quaternion.identity));
        }


        hasSpawned = true;
        return spawned;
    }

    private void Update()
    {
        if (hasSpawned) Destroy(this.gameObject);
    }
}
