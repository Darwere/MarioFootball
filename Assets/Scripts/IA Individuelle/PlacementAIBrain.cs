using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Placement
{
    Unassigned,
    RightWing,
    LeftWing,
    Center
}

public class PlacementAIBrain : PlayerBrain
{
    public AIPlayerData Data = new AIPlayerData();
    //public bool IsAI = true;


    public bool debugIsAIVisual;
    List<GameObject> Teammates = new List<GameObject>();
    List<GameObject> Ennemies = new List<GameObject>();
    Vector3[] toTeamVects;
    Vector3[] toEnnemiesVects;
    public int[] ennemyvisibility;

    bool initialised = false;
    [SerializeField] public Vector3 TeamGoalPos;
    [SerializeField] public Vector3 EnnemyGoalPos;

    [Space]
    public bool teamHasBall = false;
    public bool otherTeamHasBall = false;
    [Space]

    private List<PlacementAIBrain> RightWingPlayers = new List<PlacementAIBrain>();
    private List<PlacementAIBrain> LeftWingPlayers = new List<PlacementAIBrain>();
    private List<PlacementAIBrain> CenterPlayers = new List<PlacementAIBrain>();
    public Vector3 WingOccupancy;
    [Space]
    [SerializeField] private Transform PlayerPosition;
    [SerializeField] private float Speed;
    [SerializeField] private float DisplacementStep;
    [Space]
    [SerializeField] private float acceptanceDistance;
    private float ThirdField;
    private float TwoThirdField;
    private float FieldWidth;
    private float DesiredZ;

    private List<PlacementAIBrain> SaturatedList, EmptyList;
    private bool saturated = false;

    public bool offensivePosition = false;
    public bool defensivePosition = false;

    public Placement playerplacement;
    int uncoverDir = -1;
    private Vector3 Displacement;
    private Vector3 DesiredPosition;
    private bool AIAlreadyMoving;
    private bool atDesired = false;
    public bool defending = false;

    private int ballSeekingRadius = 5;
    private GameObject playerWithBall = null;

    int defenseMode = 0;
    public GameObject defenseTarget;

    private Vector3 movementTarget = new Vector3();
    List<Vector3> debugMovement = new List<Vector3>();


    //Default functions

    //===================================================
    //Initialisation
    private void AssignTeamAndEnnemies()
    {

        // if (Team == Field.Team1)
        // {
        //  foreach ("brain" brain in Field.Team1.Brains)
        //      Teammates.Add(brain.gameObject);
        //
        //  Teammates.Remove(this.gameObject);
        //  foreach ("brain" brain in Field.Team2.brains)
        //      Ennemies.Add(brain.gameObject)
        // if (Team == Field.Team2)

        if (Data.color == Teams.Teamcolor.Blue)
        {

            foreach (GameObject teammate in Teams.blueTeam)
            {
                Teammates.Add(teammate);
            }
            foreach (GameObject ennemy in Teams.redTeam)
            {
                Ennemies.Add(ennemy);
            }
        }
        else
        {
            foreach (GameObject teammate in Teams.redTeam)
            {
                Teammates.Add(teammate);
            }
            foreach (GameObject ennemy in Teams.blueTeam)
            {
                Ennemies.Add(ennemy);
            }
        }


        Teammates.Remove(this.gameObject);
        //if (gameObject.name == "Cube (2)") print(Teammates.Count); print(Teams.redTeam.Count);
        toTeamVects = new Vector3[Teammates.Count - 1];

        toEnnemiesVects = new Vector3[Ennemies.Count - 1];

        ennemyvisibility = new int[Ennemies.Count - 1];
    }

    void StartInit()
    {
        if (Player.Team == Field.Team1) { Data.color = Teams.Teamcolor.Blue; TeamGoalPos = Field.Team1.transform.position; EnnemyGoalPos = Field.Team2.transform.position; }
        else { Data.color = Teams.Teamcolor.Red; TeamGoalPos = Field.Team2.transform.position; EnnemyGoalPos = Field.Team1.transform.position; }

        Speed = 5;
        DisplacementStep = 2;
        acceptanceDistance = 3;
        /*Debug.LogWarning(Field.Team1);
        Debug.LogWarning(Player.Team);
        Debug.LogWarning(Data.color);
        Debug.LogWarning("");*/


        if (Data.color == Teams.Teamcolor.Blue)
        {
            Teams.blueTeam.Add(this.gameObject);
            //GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            Teams.redTeam.Add(this.gameObject);
            //GetComponent<MeshRenderer>().material.color = Color.red;
        }

        FieldWidth = Mathf.Min(Mathf.Abs(Field.BottomLeftCorner.x - Field.TopRightCorner.x), Mathf.Abs(Field.BottomLeftCorner.z - Field.TopRightCorner.z)); //TopFieldWall.position.x - BottomFieldWall.position.x;
        ThirdField = Field.HeightOneThird; //Mathf.Max(Mathf.Abs(Field.BottomLeftCorner.y - Field.TopRightCorner.y), Mathf.Abs(Field.BottomLeftCorner.x - Field.TopRightCorner.x)); //BottomFieldWall.position.x + FieldWidth / 3;


        TwoThirdField = Field.HeightTwoThirds;


        initialised = true;
    }


    //===================================================

    //====================================================
    //Get Controlled Players
    private GameObject GetPlayerInTeam()
    {
        if (playerWithBall != null)
            if (playerWithBall.GetComponent<PlacementAIBrain>().Data.color == Data.color)
                return (playerWithBall);

        if (!Data.isAI)
            return this.gameObject;
        else
        {
            foreach (GameObject player in Teammates)
            {
                if (player.GetComponent<PlacementAIBrain>().Data.isAI == false)
                {
                    return player;
                }
            }
        }
        return this.gameObject;
    }

    private GameObject GetPlayerInOtherTeam()
    {
        if (playerWithBall != null)
            if (playerWithBall.GetComponent<PlacementAIBrain>().Data.color != Data.color)
                return (playerWithBall);

        if (!Data.isAI)
            return this.gameObject;
        else
        {
            foreach (GameObject player in Ennemies)
            {
                if (!player.GetComponent<PlacementAIBrain>().Data.isAI)
                {
                    return player;
                }
            }
        }
        return null;
    }

    //====================================================


    //====================================================
    // PlayerBrain Function Requirements

    //====================================================


    //====================================================
    //Value Checkup FUnctions

    #region Value Checkup Functions

    private void GetTeamSituation()
    {
        Vector3 referencePoint = ((teamHasBall || otherTeamHasBall) && playerWithBall) ? playerWithBall.transform.position : Field.Ball.gameObject.transform.position;
        Vector3 referenceToGoal = TeamGoalPos - referencePoint;
        Vector3 referenceToOtherGoal = EnnemyGoalPos - referencePoint;


        offensivePosition = (referenceToGoal.magnitude >= referenceToOtherGoal.magnitude);
        defensivePosition = (referenceToGoal.magnitude < referenceToOtherGoal.magnitude);

    }

    private void GameSituation()
    {
        Vector3 referencePoint = ((teamHasBall || otherTeamHasBall) && playerWithBall) ? playerWithBall.transform.position : Field.Ball.gameObject.transform.position;
        Vector3 referenceToGoal = TeamGoalPos - referencePoint;
        Vector3 referenceToOtherGoal = EnnemyGoalPos - referencePoint;

        if (offensivePosition && (referenceToGoal.magnitude < 35))
        {
            defensivePosition = true; offensivePosition = false;
        }
        else if (defensivePosition && (referenceToOtherGoal.magnitude < 35))
        {
            defensivePosition = false; offensivePosition = true;
        }
        else if (!defensivePosition && !offensivePosition)
        {
            GetTeamSituation();
        }

        if (Data.color == Teams.Teamcolor.Red && !Data.isAI)
        {
            Debug.Log("");
            Debug.Log(offensivePosition);
            Debug.Log(defensivePosition);
            Debug.Log(referenceToOtherGoal.magnitude);
            Debug.Log(referenceToGoal.magnitude);
        }
    }

    private void GetPlayerWithBall()
    {
        playerWithBall = null;

        foreach (GameObject ennemy in Ennemies)
        {
            if (ennemy.GetComponent<Player>().HasBall)
            {
                playerWithBall = ennemy;
                break;
            }
        }

        if (playerWithBall == null)
        {
            foreach (GameObject mate in Teammates)
            {
                if (mate.GetComponent<Player>().HasBall)
                {
                    playerWithBall = mate;
                    break;
                }
            }
        }
    }

    private void CheckBallPossession()
    {
        GetPlayerWithBall();
        if (playerWithBall != null)
        {
            if (playerWithBall.GetComponent<PlacementAIBrain>())
            {
                if (playerWithBall.GetComponent<PlacementAIBrain>().Data.color == Data.color)
                {
                    if (!teamHasBall)
                    {
                        GetTeamSituation();
                    }
                    teamHasBall = true;
                    otherTeamHasBall = false;
                }
                else
                {
                    if (teamHasBall)
                    {
                        GetTeamSituation();
                    }
                    teamHasBall = false;
                    otherTeamHasBall = true;
                }
            }
        }
    }

    private void DetermineDefending()
    {
        if (Data.isAI)
        {
            Vector3 selfToGoal = TeamGoalPos - transform.position;
            defending = true;
            foreach (GameObject teammate in Teammates)
            {
                Vector3 themToGoal = TeamGoalPos - teammate.transform.position;
                if ((themToGoal.magnitude < selfToGoal.magnitude - 2 && teammate.GetComponent<PlacementAIBrain>().Data.isAI) || teammate.GetComponent<PlacementAIBrain>().defending)
                {
                    defending = false;
                }

            }

        }
        else
        {
            defending = false;
        }
    }

    private void UpdateToOtherVects()
    {
        for (int i = 0; i < Teammates.Count - 1; i++)
        {
            toTeamVects[i] = Teammates[i].transform.position - transform.position;
        }

        for (int i = 0; i < Ennemies.Count - 1; i++)
        {
            toEnnemiesVects[i] = Ennemies[i].transform.position - transform.position;
        }

    }

    private void UpdatePlayersPlacement()
    {

        Vector3 PlayerPosition = transform.position;
        if (!defending)
        {
            playerplacement = Placement.Center;

            if (PlayerPosition.z > TwoThirdField)
                playerplacement = Placement.RightWing;
            else if (PlayerPosition.z < ThirdField)
                playerplacement = Placement.LeftWing;
        }
        else
        {
            playerplacement = Placement.Unassigned;
        }


        /*if (GetComponent<Player>().placement != playerplacement)
        {*/
        Data.placement = playerplacement;

        AssignPlacement(this, playerplacement);

        /*}*/

        foreach (GameObject player in Teammates)
        {
            AssignPlacement(player.GetComponent<PlacementAIBrain>(), player.GetComponent<PlacementAIBrain>().playerplacement);
        }

    }

    private void AssignPlacement(PlacementAIBrain player, Placement placement)
    {
        switch (placement)
        {
            case Placement.RightWing:
                {
                    RightWingPlayers.Remove(player);
                    LeftWingPlayers.Remove(player);
                    CenterPlayers.Remove(player);
                    RightWingPlayers.Add(player);
                    break;
                }
            case Placement.LeftWing:
                {
                    RightWingPlayers.Remove(player);
                    LeftWingPlayers.Remove(player);
                    CenterPlayers.Remove(player);
                    LeftWingPlayers.Add(player);
                    break;
                }
            case Placement.Center:
                {
                    RightWingPlayers.Remove(player);
                    LeftWingPlayers.Remove(player);
                    CenterPlayers.Remove(player);
                    CenterPlayers.Add(player);
                    break;
                }
            case Placement.Unassigned:
                {
                    RightWingPlayers.Remove(player);
                    LeftWingPlayers.Remove(player);
                    CenterPlayers.Remove(player);
                    break;
                }
        }
    }

    public float GetDesiredZ(AIPlayerData player)
    {
        switch (player.placement)
        {
            case Placement.RightWing:
                return TwoThirdField * 1.5f;
            case Placement.Center:
                return 0;
            case Placement.LeftWing:
                return ThirdField * 1.5f;
            default:
                return 0f;
        }
    }

    private void GetEnnemyVisibility()
    {
        ennemyvisibility = new int[Ennemies.Count];
        // Get furthest player (not taken into consideration)
        //List<GameObject> playersToCheck = new List<GameObject>();
        float furthestDist = 0;
        GameObject furthestPlayer = null;
        foreach (GameObject pl in Ennemies)
        {
            if ((pl.transform.position - TeamGoalPos).magnitude > furthestDist)
            {
                furthestDist = (pl.transform.position - TeamGoalPos).magnitude;
                furthestPlayer = pl;
            }
        }

        for (int i = 0; i < Ennemies.Count; i++)
        {
            if (Ennemies[i] == furthestPlayer) ennemyvisibility[i] = 2;
            else
            {
                bool canSeeGoal = true;
                foreach (GameObject mate in Teammates)
                {
                    Vector3 toGoal = Ennemies[i].transform.position - ((Ennemies[i].GetComponent<PlacementAIBrain>().Data.isAI) ? GetPlayerInOtherTeam().transform.position : TeamGoalPos);
                    Vector3 toMate = Ennemies[i].transform.position - mate.transform.position;

                    if (Vector3.Angle(toGoal, toMate) < 5f) canSeeGoal = false;
                }

                if (canSeeGoal) ennemyvisibility[i] = 0;
                else ennemyvisibility[i] = 1;
            }

        }
    }

    private List<PlacementAIBrain> GetSaturatedList()
    {
        if (RightWingPlayers.Count > 1)
            return RightWingPlayers;

        else if (LeftWingPlayers.Count > 1)
            return LeftWingPlayers;

        else
            return CenterPlayers;
    }

    private List<PlacementAIBrain> GetEmptyList()
    {
        if (RightWingPlayers.Count == 0)
            return RightWingPlayers;

        else if (LeftWingPlayers.Count == 0)
            return LeftWingPlayers;

        else
            return CenterPlayers;
    }

    private void DefenseMode()
    {
        //if (Random.Range(0, 1000f) <= 5f) defenseMode++;
        if (defenseMode >= 2) defenseMode = 0;

        switch (defenseMode)
        {
            case 0:
                CoverPosition();
                break;
            case 1:
                AverageCovering();
                break;
        }
    }

    private Vector3 AveragePositionList(Vector3[] list)
    {
        Vector3 average = new Vector3();
        foreach (Vector3 toEnnemy in list)
        {
            average += toEnnemy;
        }
        average /= toEnnemiesVects.Length;
        return average;
    }

    #endregion

    //====================================================

    #region Movement Control Functions

    private void EnableControl()
    {
        if (!Data.isAI)
        {
            GetComponent<PlayerManager>().enabled = true;
        }
        else
        {
            GetComponent<PlayerManager>().enabled = false;
        }
    }

    private void AvoidOtherPlayers()
    {
        if (Data.isAI)
        {
            for (int i = 0; i < toTeamVects.Length; i++)
            {
                Vector3 vect = toTeamVects[i];
                if (vect.magnitude < 2 && !Teammates[i].GetComponent<PlacementAIBrain>().AIAlreadyMoving /*&& !Teammates[i].GetComponent<AIController>().defending*/)
                {
                    Displacement = Vector3.Cross(Vector3.up, vect.normalized) + Vector3.Cross(Vector3.up, vect.normalized);

                    //Displacement = /*vect.normalized +*/ Random.Range(-0.1f, 0.1f) *Vector3.Cross(vect.normalized, Vector3.up);
                    Displacement.y = 0;
                    int otherdefending = Teammates[i].GetComponent<PlacementAIBrain>().defending ? 0 : 1;
                    movementTarget -= 10f * Speed * (Displacement.normalized * DisplacementStep * otherdefending + 1f * vect.normalized);
                    debugMovement.Add(-10f * Speed * (Displacement.normalized * DisplacementStep * otherdefending + 1f * vect.normalized));
                }
            }

            for (int i = 0; i < toEnnemiesVects.Length; i++)
            {
                Vector3 vect = toEnnemiesVects[i];
                if (vect.magnitude < 2 && !Ennemies[i].GetComponent<PlacementAIBrain>().AIAlreadyMoving /*&& !Teammates[i].GetComponent<AIController>().defending*/)
                {
                    Displacement = Vector3.Cross(Vector3.up, vect.normalized) + Vector3.Cross(Vector3.up, vect.normalized);

                    //Displacement = /*vect.normalized +*/ Random.Range(-0.1f, 0.1f) *Vector3.Cross(vect.normalized, Vector3.up);
                    Displacement.y = 0;
                    int otherdefending = Ennemies[i].GetComponent<PlacementAIBrain>().defending ? 1 : 1;
                    movementTarget -= 5f * Speed * (Displacement.normalized * DisplacementStep * otherdefending + 1f * vect.normalized);
                    debugMovement.Add(-5f * Speed * (Displacement.normalized * DisplacementStep * otherdefending + 1f * vect.normalized));
                }
            }


        }
    }

    private void CheckAttackPlacement()
    {
        if (defending)
        {
            playerplacement = Placement.Unassigned;
            DesiredPosition = PlayerPosition.position + (TeamGoalPos - PlayerPosition.position) / 2;
            DesiredPosition.y = 0.5f;
            Displacement = DesiredPosition - transform.position;
            //PositionCorrecter(Displacement);
            movementTarget += Displacement * 2;
            debugMovement.Add(Displacement * 2);
        }
        if (RightWingPlayers.Count == CenterPlayers.Count && CenterPlayers.Count == LeftWingPlayers.Count)
        {
            DesiredZ = GetDesiredZ(Data);
            DesiredPosition = new Vector3(PlayerPosition.position.z, 0.5f, DesiredZ);
            Displacement = DesiredPosition - transform.position;
            //PositionCorrecter(Displacement);
            movementTarget += Displacement * 5;
            debugMovement.Add(Displacement * 5);
        }
        else
        {
            SaturatedList = GetSaturatedList();
            if ((RightWingPlayers == GetSaturatedList() && playerplacement == Placement.RightWing) ||
                (LeftWingPlayers == GetSaturatedList() && playerplacement == Placement.LeftWing) ||
                (CenterPlayers == GetSaturatedList() && playerplacement == Placement.Center))
            {
                saturated = true;
                //Debug.Log(this.gameObject.name + " is saturated \n");
            }

            SaturatedList.Remove(this);
            EmptyList = GetEmptyList();
            AIAlreadyMoving = false;
            bool otherMoving = false;
            foreach (PlacementAIBrain player in SaturatedList)
            {
                if (AIAlreadyMoving)
                {
                    otherMoving = true;
                }
            }


            if (saturated)
            {

                AIPlayerData player = Data;
                if (player.isAI && !otherMoving)
                {
                    AIAlreadyMoving = true;
                    if (EmptyList == RightWingPlayers)
                    {
                        DesiredPosition = new Vector3(PlayerPosition.position.x, 0.5f, TwoThirdField * 1.5f);
                    }
                    else if (EmptyList == LeftWingPlayers)
                    {
                        DesiredPosition = new Vector3(PlayerPosition.position.x, 0.5f, ThirdField * 1.5f);
                    }
                    else
                    {
                        DesiredPosition = new Vector3(PlayerPosition.position.x, 0.5f, 0);
                    }
                    Displacement = DesiredPosition - transform.position;
                    //PositionCorrecter(Displacement);
                    movementTarget += Displacement * 10;
                    debugMovement.Add(Displacement * 10);
                }
            }
            saturated = false;
            AIAlreadyMoving = false;
            otherMoving = false;
        }


    }

    private void AttackKeepSeeingTarget()
    {

        foreach (GameObject ennemy in Ennemies)
        {
            Vector3 toEnnemy = ennemy.transform.position - transform.position;
            Vector3 toPlayer = PlayerPosition.position - transform.position;

            float dot = Vector3.Angle(toEnnemy, toPlayer);
            if (Mathf.Abs(dot) < 10f && toEnnemy.magnitude < toPlayer.magnitude)
            {
                //Debug.Log("not seeable");
                if (Random.Range(0, 750f) <= 5f) uncoverDir *= -1;

                //Vector3 perp = uncoverDir*(toEnnemy - Vector3.Project(toEnnemy, toPlayer)).normalized;
                Vector3 perp = uncoverDir * (Vector3.Cross(Vector3.up, toPlayer.normalized)).normalized;

                movementTarget += perp * 5;
                debugMovement.Add(perp * 5);
            }
        }
    }

    private void PositionCorrecter(Vector3 Displacement)
    {
        if (Displacement.magnitude > acceptanceDistance)
        {
            atDesired = false;
        }
        if (Displacement.magnitude < 0.5f)
        {
            atDesired = true;
        }
        //Displacement *= Displacement.magnitude > DisplacementStep ? DisplacementStep / Displacement.magnitude : 1;
        if (!atDesired)
        {
            movementTarget += Speed * (Displacement.normalized * DisplacementStep) * Time.deltaTime;
            debugMovement.Add(Speed * (Displacement.normalized * DisplacementStep) * Time.deltaTime);
        }
    }

    private void DefenseReset()
    {
        defenseTarget = null;
    }

    private void PlayerToCover()
    {

        for (int i = 0; i < ennemyvisibility.Length; i++)
        {
            int visibility = ennemyvisibility[i];
            if (visibility == 0)
            {
                bool alreadyTaken = false;
                foreach (GameObject mate in Teammates)
                {
                    if (mate.GetComponent<PlacementAIBrain>().defenseTarget == Ennemies[i]) alreadyTaken = true;
                }
                //return Ennemies[i];
                if (!alreadyTaken)
                {
                    defenseTarget = Ennemies[i];
                    break;
                }
            }
        }

        if (defenseTarget == null)
        {
            GameObject player = GetPlayerInOtherTeam();
            bool alreadyTaken = false;
            foreach (GameObject mate in Teammates)
            {
                if (mate.GetComponent<PlacementAIBrain>().defenseTarget == player) alreadyTaken = true;
            }
            if (!alreadyTaken)
            {
                defenseTarget = GetPlayerInOtherTeam();
            }

        }


    }

    private void CoverPosition()
    {

        if (defenseTarget == null)
        {
            PlayerToCover();
        }

        if (defenseTarget != null)
        {
            GameObject otherPlayer = GetPlayerInOtherTeam();
            bool coveringPlayer = (defenseTarget == otherPlayer);

            if (coveringPlayer)
            {
                Vector3 playerToGoal = TeamGoalPos - otherPlayer.transform.position;
                DesiredPosition = otherPlayer.transform.position + playerToGoal / 4;
                DesiredPosition.y = 0.5f;
            }
            else
            {
                Vector3 ennemyToOtherPlayer = otherPlayer.transform.position - defenseTarget.transform.position;
                DesiredPosition = defenseTarget.transform.position + ennemyToOtherPlayer / 3;
                DesiredPosition.y = 0.5f;
            }
        }
        else
        {
            Vector3 averageEnnemies = AveragePositionList(toEnnemiesVects);


            DesiredPosition = (averageEnnemies + TeamGoalPos) / 2;
            DesiredPosition.y = 0.5f;
        }


        Displacement = DesiredPosition - transform.position;
        //PositionCorrecter(Displacement);
        movementTarget += Displacement * 2;
        debugMovement.Add(Displacement * 2);

    }
    /*
    private void AverageCovering()
    {
        if (defenseTarget == null)
        {
            PlayerToCover();
        }

        GameObject otherPlayer = GetPlayerInOtherTeam();
        float minDist = float.MaxValue;
        GameObject closest = null;
        foreach (GameObject ennemy in Ennemies)
        {
            if (ennemy != otherPlayer && ennemy != defenseTarget)
            {
                Vector3 toEnnemy = ennemy.transform.position - transform.position;
                if (toEnnemy.magnitude < minDist)
                {
                    closest = ennemy;
                    minDist = toEnnemy.magnitude;
                }
            }
        }
        if (defenseTarget == null) defenseTarget = this.gameObject;

        Vector3 toAverage = ((otherPlayer.transform.position + defenseTarget.transform.position + closest.transform.position) / 3) - transform.position;
        movementTarget += toAverage;

    }
    */

    private void AverageCovering() // V2
    {
        Vector3 averageTeam = new Vector3();
        foreach (GameObject mate in Teammates) averageTeam += mate.transform.position;
        averageTeam += transform.position;
        averageTeam /= Teammates.Count + 1;

        Vector3 furthestCorner = new Vector3();
        float furthestDist = 0;

        Vector3 corner = Field.BottomLeftCorner;
        if ((corner - averageTeam).magnitude > furthestDist)
        {
            furthestDist = (corner - averageTeam).magnitude;
            furthestCorner = corner;
        }

        corner = Field.BottomRightCorner;
        if ((corner - averageTeam).magnitude > furthestDist)
        {
            furthestDist = (corner - averageTeam).magnitude;
            furthestCorner = corner;
        }

        corner = Field.TopLeftCorner;
        if ((corner - averageTeam).magnitude > furthestDist)
        {
            furthestDist = (corner - averageTeam).magnitude;
            furthestCorner = corner;
        }

        corner = Field.TopRightCorner;
        if ((corner - averageTeam).magnitude > furthestDist)
        {
            furthestDist = (corner - averageTeam).magnitude;
            furthestCorner = corner;
        }

        DesiredPosition = (furthestCorner - averageTeam) / 2;
        movementTarget += (DesiredPosition - transform.position) / 4;
        debugMovement.Add((DesiredPosition - transform.position) / 4);

    }

    private void GoFurtherThanPlayer()
    {
        GameObject player = GetPlayerInTeam();

        float playerXDist = Mathf.Abs(player.transform.position.x - TeamGoalPos.x);
        float selfXDist = Mathf.Abs(transform.position.x - TeamGoalPos.x);

        if ((playerXDist + 5f) > selfXDist)
        {
            Vector3 addedMove = (TeamGoalPos - transform.position);
            addedMove = new Vector3(addedMove.x, 0, 0);
            //Debug.DrawRay(transform.position, -addedMove.normalized * 10, Color.green, 0.1f);
            movementTarget -= addedMove.normalized * 10f;
            debugMovement.Add(-addedMove.normalized * 10f);
        }
        else if ((playerXDist + 10f) < selfXDist)
        {
            Vector3 addedMove = (TeamGoalPos - transform.position);
            addedMove = new Vector3(addedMove.x, 0, 0);
            //Debug.DrawRay(transform.position, -addedMove.normalized * 10, Color.green, 0.1f);
            movementTarget += addedMove.normalized * 20f;
            debugMovement.Add(addedMove.normalized * 20f);

        }

    }

    private void WallAvoidance()
    {
        Vector3 avoider = new Vector3();
        Vector3 nextPos = transform.position + (movementTarget - transform.position).normalized;
        if(nextPos.x > Field.BottomLeftCorner.x 
            && nextPos.x > Field.BottomRightCorner.x 
            && nextPos.x > Field.TopLeftCorner.x 
            && nextPos.x > Field.TopRightCorner.x)
        {
            avoider += Vector3.right * 50f;
        }
        else if (nextPos.x < Field.BottomLeftCorner.x
            && nextPos.x < Field.BottomRightCorner.x
            && nextPos.x < Field.TopLeftCorner.x
            && nextPos.x < Field.TopRightCorner.x)
        {
            avoider += Vector3.right * -50f;
        }

        
        if (nextPos.z > Field.BottomLeftCorner.z
            && nextPos.z > Field.BottomRightCorner.z
            && nextPos.z > Field.TopLeftCorner.z
            && nextPos.z > Field.TopRightCorner.z)
        {
            avoider += Vector3.forward * 50f;
        }
        else if (nextPos.z < Field.BottomLeftCorner.z
            && nextPos.z < Field.BottomRightCorner.z
            && nextPos.z < Field.TopLeftCorner.z
            && nextPos.z < Field.TopRightCorner.z)
        {
            avoider += Vector3.forward * -50f;
        }

        movementTarget += avoider;
        debugMovement.Add(avoider);
    }

    private void CheckMove()
    {
        Vector3 moveDir = movementTarget - transform.position;
        //if (Data.color == Teams.Teamcolor.Blue && this.gameObject == Teams.blueTeam[1]) Debug.Log(moveDir.magnitude);

        moveDir.y = 0;

        if (moveDir.magnitude > 1.5f)
        {
            PlayerAction act = PlayerAction.Move(moveDir.normalized);
            //Debug.Log(movementTarget);
            //else PlayerAction.Idle();
            action = act;
        }
        else
        {
            action = PlayerAction.Move(Vector3.zero);
        }





    }

    private void BallRadius()
    {
        if (playerWithBall != this.gameObject)
        {
            Vector3 toBall = Field.Ball.gameObject.transform.position - transform.position;
            if (toBall.magnitude <= ballSeekingRadius)
            {
                movementTarget += toBall.normalized * 2;
                debugMovement.Add(toBall.normalized * 2);
            }
        }

    }

    #endregion

    /*
    private Vector3 Move()
    {
        return movementTarget;
    }
    */

    private void DebugPrint()
    {
        if (Data.color == Teams.Teamcolor.Blue && GetComponent<Player>().IsPiloted)
        {
            Debug.Log(transform.position.z);
            Debug.Log(transform.position.x);
            Debug.Log(Field.HeightOneThird);
            Debug.Log(Field.HeightTwoThirds);
            Debug.Log(playerplacement);
            Debug.Log("");

        }
    }

    private void DebugRay(bool forceColor)
    {
        Color rayColor = (forceColor) ? Color.green : ((Data.color == Teams.Teamcolor.Blue) ? Color.blue : Color.red);
        Vector3 position = transform.position;
        foreach(Vector3 thought in debugMovement)
        {
            Debug.DrawRay(position, thought, rayColor, 0.1f);
            position += thought;
        }
        Debug.DrawRay(transform.position, movementTarget-transform.position, new Color(0.5f, 0.5f, 0.5f, 0.5f), 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        if(Field.Ball.transform.parent != null)
        {
            foreach(Player player in Allies.Players)
            {
                if (Field.Ball.transform.parent == player.transform)
                    teamHasBall = true;
                else
                    teamHasBall = false;
            }
        }

        DebugRay(false);
        CheckBallPossession();
        movementTarget = transform.position;

        debugIsAIVisual = GetComponent<Player>().IsPiloted;
        debugMovement = new List<Vector3>();

        if (!initialised)
        {
            StartInit();
        }
        else if (Teammates.Count <= 0)
        {
            AssignTeamAndEnnemies();
            GetTeamSituation();
        }
        else
        {
            Data.isAI = !GetComponent<Player>().IsPiloted;
            UpdateToOtherVects();
            AvoidOtherPlayers();
            GameSituation();
            //EnableControl();
            PlayerPosition = GetPlayerInTeam().transform;
            if (teamHasBall)
            {
                //DebugPrint();
                DefenseReset();
                DetermineDefending();
                UpdatePlayersPlacement();
                CheckAttackPlacement();
                AttackKeepSeeingTarget();
                if (defensivePosition)
                {

                    GoFurtherThanPlayer();
                    //DebugRay(true);
                }
            }
            else if (otherTeamHasBall)
            {
                UpdatePlayersPlacement();
                GetEnnemyVisibility();
                DefenseMode();
                BallRadius();
            }
            else
            {
                BallRadius();
            }
            
        }

        WallAvoidance();
        CheckMove();



        WingOccupancy = new Vector3(LeftWingPlayers.Count, CenterPlayers.Count, RightWingPlayers.Count);

        //transform.position += movementTarget;
    }

}

public class AIPlayerData
{
    public Teams.Teamcolor color;
    public Placement placement = Placement.Unassigned;
    public bool isAI;
}
