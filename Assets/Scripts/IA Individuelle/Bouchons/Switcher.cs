using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] public Teams.Teamcolor teamWithBall = Teams.Teamcolor.Red;
    [SerializeField] public AIPlayerData redPlayer;
    [SerializeField] public AIPlayerData bluePlayer;


    [SerializeField] bool selectSwitchedPlayer = false;
    [SerializeField] bool teamSwitch = false;
    [SerializeField] bool redPlayerSwitch = false;
    [SerializeField] bool bluePlayerSwitch = false;

    private GameObject tempGameObject = null;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayers();
    }

    private void SetPlayers()
    {
        IAManager.instance.playerWithBall = redPlayer.gameObject;
        foreach (GameObject red in Teams.redTeam)
        {
            red.GetComponent<PlacementAIBrain>().teamHasBall = (teamWithBall == Teams.Teamcolor.Red);
            red.GetComponent<PlacementAIBrain>().otherTeamHasBall = (teamWithBall == Teams.Teamcolor.Blue);

            red.GetComponent<AIPlayerData>().isAI = (red != redPlayer.gameObject);
        }
        foreach (GameObject blue in Teams.blueTeam)
        {
            blue.GetComponent<PlacementAIBrain>().teamHasBall = (teamWithBall == Teams.Teamcolor.Blue);
            blue.GetComponent<PlacementAIBrain>().otherTeamHasBall = (teamWithBall == Teams.Teamcolor.Red);

            blue.GetComponent<AIPlayerData>().isAI = (blue != bluePlayer.gameObject);
        }
    }

    private void TeamSwitch()
    {
         switch (teamWithBall)
        {
            case Teams.Teamcolor.Blue:        
                teamWithBall = Teams.Teamcolor.Red;
                IAManager.instance.playerWithBall = redPlayer.gameObject;
                break;

            /*case Teams.Teamcolor.None:
                teamWithBall = Teams.Teamcolor.Red;
                break;
                */
            case Teams.Teamcolor.Red:
                teamWithBall = Teams.Teamcolor.Blue;
                IAManager.instance.playerWithBall = bluePlayer.gameObject;
                break;
        }
               

    }

    private void PlayerSwitch(Teams.Teamcolor team)
    {
        List<GameObject> switchTeam = (team == Teams.Teamcolor.Blue) ? Teams.blueTeam : Teams.redTeam;
        
        for (int i = 0; i< switchTeam.Count; i++)
        {
            if (!switchTeam[i].GetComponent<AIPlayerData>().isAI)
            {
                int index = (i == switchTeam.Count - 1) ? 0 : i + 1;

                if (team == Teams.Teamcolor.Blue) bluePlayer = switchTeam[index].GetComponent<AIPlayerData>();
                else redPlayer = switchTeam[index].GetComponent<AIPlayerData>();

                if (selectSwitchedPlayer) Selection.activeObject = switchTeam[index];
                //switchTeam[i].GetComponent<Player>().isAI = true;
                //switchTeam[index].GetComponent<Player>().isAI = false;
                break;
            }
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayers();
        /*
        if (Input.GetKeyDown(KeyCode.Space)) TeamSwitch();

        if (Input.GetKeyDown(KeyCode.LeftShift)) PlayerSwitch(Teams.Teamcolor.Blue);
        if (Input.GetKeyDown(KeyCode.RightShift)) PlayerSwitch(Teams.Teamcolor.Red);
        */

        if (teamSwitch) { teamSwitch = false; TeamSwitch(); }

        if (redPlayerSwitch) { redPlayerSwitch = false; PlayerSwitch(Teams.Teamcolor.Red); }
        if (bluePlayerSwitch) { bluePlayerSwitch = false; PlayerSwitch(Teams.Teamcolor.Blue); }

    }
}
