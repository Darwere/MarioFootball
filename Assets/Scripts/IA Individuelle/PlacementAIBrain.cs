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
    int[] ennemyvisibility;

    bool initialised = false;
    [SerializeField] public Vector3 TeamGoalPos;
    [SerializeField] public Vector3 EnnemyGoalPos;

    [Space]
    public bool teamHasBall = false;
    public bool otherTeamHasBall = false;
    [Space]

    [SerializeField] private List<PlacementAIBrain> RightWingPlayers = new List<PlacementAIBrain>();
    [SerializeField] private List<PlacementAIBrain> LeftWingPlayers = new List<PlacementAIBrain>();
    [SerializeField] private List<PlacementAIBrain> CenterPlayers = new List<PlacementAIBrain>();
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
    private float DesiredX;

    private List<PlacementAIBrain> SaturatedList, EmptyList;
    private bool saturated = false;

    public Placement playerplacement;
    private Vector3 Displacement;
    private Vector3 DesiredPosition;
    private bool AIAlreadyMoving;
    private bool atDesired = false;
    public bool defending = false;


    private GameObject defenseTarget;

    private Vector3 movementTarget = new Vector3();


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
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            Teams.redTeam.Add(this.gameObject);
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        FieldWidth = Mathf.Min(Mathf.Abs(Field.BottomLeftCorner.y - Field.TopRightCorner.y), Mathf.Abs(Field.BottomLeftCorner.x - Field.TopRightCorner.x)); //TopFieldWall.position.x - BottomFieldWall.position.x;
        ThirdField = Field.HeightOneThird; //Mathf.Max(Mathf.Abs(Field.BottomLeftCorner.y - Field.TopRightCorner.y), Mathf.Abs(Field.BottomLeftCorner.x - Field.TopRightCorner.x)); //BottomFieldWall.position.x + FieldWidth / 3;
        
        
        TwoThirdField = Field.HeightTwoThirds;


        initialised = true;
    }


    //===================================================

    //====================================================
    //Get Controlled Players
    private GameObject GetPlayerInTeam()
    {
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



    #region Control Player Methods

    protected override void Idle()
    {

    }

    protected override void Move()
    {
        Player.transform.position += action.direction * Time.deltaTime * Player.Species.speed;
    }

    protected override void Pass()
    {
        //Field.Ball.Pass(action.startPosition, action.bezierPoint, action.endPosition, action.duration);
        SwitchPlayer();

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Pass");
    }

    protected override void SwitchPlayer()
    {
        Player.IsPiloted = false; //last player piloted

        Player = action.target;
        Player.IsPiloted = true; //new player piloted

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Switch");
    }

    protected override void Shoot()
    {
        //Ball.Shoot(action.shootForce, action.direction, action.startPosition, action.duration);


        action.type = PlayerAction.ActionType.None;
        Debug.Log("Shoot");
    }

    public override Vector3 MoveInput()
    {
        //Ball.Shoot(action.shootForce, action.direction, action.startPosition, action.duration);


        action.type = PlayerAction.ActionType.None;
        Debug.Log("MoveInput");
        return Vector3.zero;
    }

    protected override void Tackle()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Tackle");
    }

    protected override void Dribble()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Drible");
    }

    protected override void Headbutt()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Headbutt");
    }

    protected override void SendObject()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("SendObject");
    }

    #endregion

    public override void Act()
    {
        actionMethods[action.type].DynamicInvoke();
    }

    /*
    public override Vector3 Move()
    {
        return (movementTarget-transform.position).normalized;
    }

    public override Action Act()
    {
        throw new System.NotImplementedException();
    }
    */

    //====================================================


    //====================================================
    //Value Checkup FUnctions
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

        for (int i = 0; i < Ennemies.Count-1; i++)
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

            if (PlayerPosition.x > TwoThirdField)
                playerplacement = Placement.RightWing;
            else if (PlayerPosition.x < ThirdField)
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

    public float GetDesiredX(AIPlayerData player)
    {
        switch (player.placement)
        {
            case Placement.RightWing:
                return TwoThirdField *1.5f;
            case Placement.Center:
                return 0;
            case Placement.LeftWing:
                return ThirdField*1.5f;
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
            if ((pl.transform.position- TeamGoalPos).magnitude > furthestDist)
            {
                furthestDist = (pl.transform.position - TeamGoalPos).magnitude;
                furthestPlayer = pl;
            }
        }

        for (int i = 0; i<Ennemies.Count; i++)
        {
            if (Ennemies[i] == furthestPlayer) ennemyvisibility[i] = 2;
            else
            {
                bool canSeeGoal = true;
                foreach (GameObject mate in Teammates)
                {

                    Vector3 toGoal = Ennemies[i].transform.position - ((Ennemies[i].GetComponent<PlacementAIBrain>().Data.isAI)? GetPlayerInOtherTeam().transform.position : TeamGoalPos);
                    Vector3 toMate = Ennemies[i].transform.position - mate.transform.position;

                    if (Vector3.Dot(toGoal, toMate) < 0.1f) canSeeGoal = false;
                }
                
                if (canSeeGoal) ennemyvisibility[i] = 0;
                else ennemyvisibility[i] = 1;
            }

        }
    }

    //====================================================


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
                    movementTarget -= 5f * Speed * (Displacement.normalized * DisplacementStep * otherdefending + 1f * vect.normalized) * Time.deltaTime;
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
                    movementTarget -= 1.5f * Speed * (Displacement.normalized * DisplacementStep * otherdefending + 1f * vect.normalized) * Time.deltaTime;
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
            PositionCorrecter(Displacement);
        }
        if (RightWingPlayers.Count == CenterPlayers.Count && CenterPlayers.Count == LeftWingPlayers.Count)
        {
            if (Data.isAI)
            {
                DesiredX = GetDesiredX(Data);
                DesiredPosition = new Vector3(DesiredX, 0.5f, PlayerPosition.position.z);
                Displacement = DesiredPosition - transform.position;
                PositionCorrecter(Displacement);
            }
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
                        DesiredPosition = new Vector3(TwoThirdField*1.5f, 0.5f, PlayerPosition.position.z);
                        Displacement = DesiredPosition - transform.position;
                        PositionCorrecter(Displacement);
                    }
                    else if (EmptyList == LeftWingPlayers)
                    {
                        DesiredPosition = new Vector3(ThirdField*1.5f, 0.5f, PlayerPosition.position.z);
                        Displacement = DesiredPosition - transform.position;
                        PositionCorrecter(Displacement);
                    }
                    else
                    {
                        DesiredPosition = new Vector3(0, 0.5f, PlayerPosition.position.z);
                        Displacement = DesiredPosition - transform.position;
                        PositionCorrecter(Displacement);
                    }
                }
            }
            saturated = false;
            AIAlreadyMoving = false;
            otherMoving = false;
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

    private void DefenseModeReset()
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
            defenseTarget = GetPlayerInOtherTeam();
        
    }

    private void CoverPosition()
    {
        

        if(Data.isAI)
        {
            if (defenseTarget == null)
            {
                PlayerToCover();
            }
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
                DesiredPosition = defenseTarget.transform.position + ennemyToOtherPlayer / 4;
                DesiredPosition.y = 0.5f;
            }

            Displacement = DesiredPosition - transform.position;
            PositionCorrecter(Displacement);
        }
    }


    /*
    private Vector3 Move()
    {
        return movementTarget;
    }
    */

    private void DebugPrint()
    {
        if(Data.color == Teams.Teamcolor.Blue && GetComponent<Player>().IsPiloted)
        {
            Debug.Log(transform.position.x);
            Debug.Log(transform.position.z);
            Debug.Log(Field.HeightOneThird);
            Debug.Log(Field.HeightTwoThirds);
            Debug.Log(playerplacement);
            Debug.Log("");

        }
    }

    // Update is called once per frame
    void Update()
    {

        movementTarget = Vector3.zero; //transform.position;

        debugIsAIVisual = GetComponent<Player>().IsPiloted;

        if (!initialised)
        {
            StartInit();
        }
        else if (Teammates.Count <= 0)
        {
            AssignTeamAndEnnemies();
        }
        else
        {
            Data.isAI = !GetComponent<Player>().IsPiloted;
            UpdateToOtherVects();
            AvoidOtherPlayers();
            //EnableControl();
            PlayerPosition = GetPlayerInTeam().transform;
            if (teamHasBall)
            {
                DebugPrint();
                DefenseModeReset();
                DetermineDefending();
                UpdatePlayersPlacement();
                CheckAttackPlacement();
            }
            else
            {
                UpdatePlayersPlacement();
                GetEnnemyVisibility();
                CoverPosition();
            }
        }

        movementTarget.y = 0;

        if (movementTarget.magnitude > 0.5f)
            movementTarget = movementTarget.normalized;
        else
            movementTarget = Vector3.zero;
        PlayerAction act = PlayerAction.Move(movementTarget.normalized);
        //else PlayerAction.Idle();
        action = act;


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
