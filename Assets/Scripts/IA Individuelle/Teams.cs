using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class Teams 
{
    public enum Teamcolor
    {
        None,
        Blue,
        Red
    }

    public static List<GameObject> blueTeam = new List<GameObject>();
    public static List<GameObject> redTeam = new List<GameObject>();

    public static List<int[]> redTriZone = new List<int[]>();
    public static List<int[]> blueTriZone = new List<int[]>();

    public static int[] free = new int[3];

    static void OccupiedZones(Teamcolor team)
    {
        List<int[]> zones = new List<int[]>();
        free = new int[3];

        if (team == Teams.Teamcolor.Blue)
        {
            zones = blueTriZone;
        }
        else if (team == Teams.Teamcolor.Red)
        {
            zones = redTriZone;
        }

        for (int i = 0; i < 3; i++)
        {
            foreach (int[] objZones in zones)
            {
                free[i] += objZones[i];
            }
        }
    }

    public static int FreeZone(int maxFilledZones, Teamcolor team)
    {
        OccupiedZones(team);


        for (int i = 0; i<3; i++)
        {
            if (free[i] == 0)
            {
                if (free.Length >= maxFilledZones)
                    return 0;
                else
                    return i + 1;
            }
        }
        return 0;
        
        
    }

    public static bool CheckZone(Teamcolor team, int zone)
    {
        OccupiedZones(team);

        if (free[zone - 1] == 0)
            return true;
        else
            return false;
    }

    /*
    public static void AssignZones()
    {
        blueTriZone = new List<int[]>();
        redTriZone = new List<int[]>();

        foreach (GameObject player in redTeam)
        {
            SimilarityAI AI = player.GetComponent<SimilarityAI>();
            GameObject ballPlayer = AI.ballPossessor;
            int[] occZone = new int[3];

            
            if (AI.teamAttacking && AI.teamHasBall && player.transform.position.z < 0)
            {
                if (player.transform.position.x > 8)
                {
                    occZone[0] = 1;
                }
                else if (player.transform.position.x < -8)
                {
                    occZone[2] = 1;
                }
                else
                {
                    occZone[1] = 1;
                }
            }
            if (AI.teamAttacking && AI.otherteamHasBall && player.transform.position.z > 0)
            {
                if (player.transform.position.x > 8)
                {
                    occZone[0] = 1;
                }
                else if (player.transform.position.x < -8)
                {
                    occZone[2] = 1;
                }
                else
                {
                    occZone[1] = 1;
                }
            }
            else if (AI.teamDefending && player.transform.position.z > 0)
            {
                if (player.transform.position.x > 8)
                {
                    occZone[0] = 1;
                }
                else if (player.transform.position.x < -8)
                {
                    occZone[2] = 1;
                }
                else
                {
                    occZone[1] = 1;
                }
            }
            

            redTriZone.Add(occZone);
        }

        foreach (GameObject player in blueTeam)
        {
            SimilarityAI AI = player.GetComponent<SimilarityAI>();
            int[] occZone = new int[3];


            if (AI.teamDefending && player.transform.position.z < 0)
            {
                if (player.transform.position.x > 8)
                {
                    occZone[0] = 1;
                }
                else if (player.transform.position.x < -8)
                {
                    occZone[2] = 1;
                }
                else
                {
                    occZone[1] = 1;
                }
            }
            else if (AI.teamAttacking && AI.teamHasBall && player.transform.position.z > 0)
            {
                if (player.transform.position.x > 8)
                {
                    occZone[0] = 1;
                }
                else if (player.transform.position.x < -8)
                {
                    occZone[2] = 1;
                }
                else
                {
                    occZone[1] = 1;
                }
            }
            else if (AI.teamAttacking && AI.otherteamHasBall && player.transform.position.z < 0)
            {
                if (player.transform.position.x > 8)
                {
                    occZone[0] = 1;
                }
                else if (player.transform.position.x < -8)
                {
                    occZone[2] = 1;
                }
                else
                {
                    occZone[1] = 1;
                }
            }


            blueTriZone.Add(occZone);
        }

    }
    */

}
