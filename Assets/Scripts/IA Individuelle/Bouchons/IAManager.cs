using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAManager : MonoBehaviour
{
    public static IAManager instance;
    [SerializeField] public Teams.Teamcolor whoHasBall;

    [SerializeField] GameObject PrefabBall;
    public GameObject Ball;
    [Space]
    [Space]
    [SerializeField] public bool redHasBall = false;
    [SerializeField] public bool blueHasBall = false;
    [SerializeField] public GameObject playerWithBall;
    [SerializeField] public bool debugKeyboardControls = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        /*GameObject ball = Instantiate(PrefabBall, transform.position, Quaternion.identity);
        Ball = ball;
        foreach (GameObject player in Teams.blueTeam)
        {
            player.GetComponent<PlayerIA>().ball = Ball;
        }
        foreach (GameObject player in Teams.redTeam)
        {
            player.GetComponent<PlayerIA>().ball = Ball;
        }*/


    }

    // Update is called once per frame
    void Update()
    {

        //Teams.blueTeam[0].transform.position += Vector3.up / 100;
        if (playerWithBall)
        {
            //whoHasBall = playerWithBall.GetComponent<PlayerIA>() ? playerWithBall.GetComponent<PlayerIA>().color : Teams.Teamcolor.None;
            if (whoHasBall == Teams.Teamcolor.None) { redHasBall = false; blueHasBall = false; }
            else if (whoHasBall == Teams.Teamcolor.Blue) { redHasBall = false; blueHasBall = true; }
            else if (whoHasBall == Teams.Teamcolor.Red)
            {
                redHasBall = true; blueHasBall = false;
            }
        }
        else { redHasBall = false; blueHasBall = false; }

        /*
        if (redHasBall)
        {
            foreach (GameObject player in Teams.redTeam)
            {
                player.GetComponent<PlayerIA>().teamHasBall = true;
                player.GetComponent<PlayerIA>().otherteamHasBall = false;
                player.GetComponent<PlayerIA>().ballPossessor = playerWithBall;
            }
            foreach (GameObject player in Teams.blueTeam)
            {
                player.GetComponent<PlayerIA>().teamHasBall = false;
                player.GetComponent<PlayerIA>().otherteamHasBall = true;
                player.GetComponent<PlayerIA>().ballPossessor = playerWithBall;
            }
        }

        if (blueHasBall)
        {
            foreach (GameObject player in Teams.redTeam)
            {
                player.GetComponent<PlayerIA>().teamHasBall = false;
                player.GetComponent<PlayerIA>().otherteamHasBall = true;
                player.GetComponent<PlayerIA>().ballPossessor = playerWithBall;
            }
            foreach (GameObject player in Teams.blueTeam)
            {
                player.GetComponent<PlayerIA>().teamHasBall = true;
                player.GetComponent<PlayerIA>().otherteamHasBall = false;
                player.GetComponent<PlayerIA>().ballPossessor = playerWithBall;
            }
        }

        if (!blueHasBall && !redHasBall)
        {
            foreach (GameObject player in Teams.redTeam)
            {
                player.GetComponent<PlayerIA>().teamHasBall = false;
                player.GetComponent<PlayerIA>().otherteamHasBall = false;
                player.GetComponent<PlayerIA>().ballPossessor = null;
            }
            foreach (GameObject player in Teams.blueTeam)
            {
                player.GetComponent<PlayerIA>().teamHasBall = false;
                player.GetComponent<PlayerIA>().otherteamHasBall = false;
                player.GetComponent<PlayerIA>().ballPossessor = playerWithBall;
            }
        }
        */
    }
}
