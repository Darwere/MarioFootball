using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledIndicators : MonoBehaviour
{
    [SerializeField] Teams.Teamcolor color = Teams.Teamcolor.None;
    List<GameObject> players;
    GameObject target = null;
    //[SerializeField] public int range = 30;
    //int initialRange;
    // Start is called before the first frame update
    void Start()
    {
        players = (color == Teams.Teamcolor.Blue) ? Teams.blueTeam : Teams.redTeam;
        //initialRange = range;

    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject pl in players)
        {
            if (!pl.GetComponent<AIPlayerData>().isAI)
            {
                target = pl;
                break;
            }
        }

        //GetComponent<Light>().range = (target.GetComponent<AIController>().teamHasBall) ? 1.5f * initialRange : initialRange;
        GetComponent<Light>().intensity = (target.GetComponent<PlacementAIBrain>().teamHasBall) ? 2f : 1;


        transform.position = target.transform.position;
    }
}
