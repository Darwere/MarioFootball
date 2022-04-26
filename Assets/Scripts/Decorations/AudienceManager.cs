using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public static AudienceManager instance;
    public List<GameObject> Audience = new List<GameObject>();
    [SerializeField] public Transform CenterPoint;
    [SerializeField] public Transform AudienceParent;
    public bool everyoneClap = false;
    private bool previousClapState = false;

    [Header("Audience Prefabs : ")]
    [SerializeField] public List<GameObject> Birdos;
    [SerializeField] public List<GameObject> Toads;
    [SerializeField] public List<GameObject> ShyGuys;
    [SerializeField] public List<GameObject> Pengus;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudienceSpot spot in FindObjectsOfType<AudienceSpot>())
        {
            GameObject newAudience = spot.Spawn();
            if(!newAudience.GetComponent<AudienceSpot>()) Audience.Add(newAudience);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(everyoneClap != previousClapState)
        {
            foreach (GameObject member in Audience)
            {
                member.GetComponent<AudienceMember>().Clap = everyoneClap;
            }
        }


        previousClapState = everyoneClap;
    }
}
