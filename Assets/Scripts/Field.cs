using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private static Field instance;

    [SerializeField] private float width;
    [SerializeField] private float height;

    [SerializeField] private Team team1, team2;
    [SerializeField] private Team attackTeam, defTeam;

    [SerializeField] private List<Transform> attackPos;
    [SerializeField] private List<Transform> defPos;

    public static int test =2;
    public static Team Team1 => instance.team1;
    public static Team Team2 => instance.team2;

    private Vector3 bottomLeftCorner;
    public static Vector3 BottomLeftCorner => instance.bottomLeftCorner;

    private Vector3 bottomRightCorner;
    public static Vector3 BottomRightCorner => instance.bottomRightCorner;

    private Vector3 topLeftCorner;
    public static Vector3 TopLeftCorner => instance.topLeftCorner;

    private Vector3 topRightCorner;
    public static Vector3 TopRightCorner => instance.topRightCorner;

    private float heightOneThird;
    public static float HeightOneThird => instance.heightOneThird;

    private float heightTwoThirds;
    public static float HeightTwoThirds => instance.heightTwoThirds;

    private float heightOneSixths;
    public static float HeightOneSixths => instance.heightOneSixths;

    private float heightThreeSixths;
    public static float HeightThreeSixths => instance.heightThreeSixths;

    private float heightFiveSixths;
    public static float HeightFiveSixths => instance.heightFiveSixths;

    // TODO Bryan : R�cup�rer positions initiales

    private Ball ball;
    public static Ball Ball => instance.ball;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bottomLeftCorner = new Vector3(width / 2, 0, -height / 2) + transform.position;
        bottomRightCorner = new Vector3(-width / 2, 0, -height / 2) + transform.position;
        topLeftCorner = new Vector3(width / 2, 0, height / 2) + transform.position;
        topRightCorner = new Vector3(-width / 2, 0, height / 2) + transform.position;

        heightOneThird = bottomLeftCorner.z + height / 3f;
        heightTwoThirds = bottomLeftCorner.z + height * 2f / 3f;

        heightOneSixths = topLeftCorner.x + height / 6f;
        heightThreeSixths = topLeftCorner.x + height * 3f / 6f;
        heightFiveSixths = topLeftCorner.x + height * 5f / 6f;
        attackTeam = Team1;
        defTeam = Team2;
        GameManager.Init(team1, team2);


    }
    /// <summary>
    /// Assigne le ballon cr�� puis le positionne ainsi que les joueurs
    /// </summary>
    /// <param name="ball">Le ballon</param>
    public static void Init(Ball ball)
    {
        instance.ball = ball;

        ball.transform.position = instance.transform.position + ball.transform.position;

        SetTeamPosition(Team1);
        CameraManager.Init();
    }

    public static void SetTeamPosition(Team attackTeam)
    {
        List<Transform> attackPos = instance.attackPos;
        List<Transform> defPos = instance.defPos;

        Team defTeam = attackTeam == Team1 ? Team2 : Team1;

        Field.Ball.AttachToPlayer(attackTeam.Players[0]);
        attackTeam.ChangePilotedPlayer(attackTeam.Players[0]);
        defTeam.ChangePilotedPlayer(defTeam.Players[0]);

        attackTeam.WaitKickOff();
        defTeam.WaitKickOff();

        if (attackTeam == Team1)
        {
            for (int i = 0; i < attackTeam.Players.Length; i++)
            {
                attackTeam.Players[i].transform.position = attackPos[i].position;
                attackTeam.Players[i].transform.rotation = Quaternion.Euler(0, 90f, 0);

                defTeam.Players[i].transform.position = instance.XAxisSymmetry(defPos[i].position);
                defTeam.Players[i].transform.rotation = Quaternion.Euler(0, -90f, 0);
            }

            attackTeam.Goal.transform.position = attackPos[attackPos.Count - 1].position;
            attackTeam.Goal.transform.rotation = Quaternion.Euler(0, 90f, 0);

            defTeam.Goal.transform.position = instance.XAxisSymmetry(defPos[defPos.Count - 1].position);
            defTeam.Goal.transform.rotation = Quaternion.Euler(0, -90f, 0);
        }
        else
        {
            for (int i = 0; i < Team1.Players.Length; i++)
            {
                attackTeam.Players[i].transform.position = instance.XAxisSymmetry(attackPos[i].position);
                attackTeam.Players[i].transform.rotation = Quaternion.Euler(0, -90f, 0);

                defTeam.Players[i].transform.position = defPos[i].position;
                defTeam.Players[i].transform.rotation = Quaternion.Euler(0, 90f, 0);
            }

            attackTeam.Goal.transform.position = instance.XAxisSymmetry(attackPos[attackPos.Count - 1].position);
            attackTeam.Goal.transform.rotation = Quaternion.Euler(0, -90f, 0);

            defTeam.Goal.transform.position = defPos[defPos.Count - 1].position;
            defTeam.Goal.transform.rotation = Quaternion.Euler(0, 90f, 0);
        }

    }

    public Vector3 XAxisSymmetry(Vector3 initial)
    {
        return new Vector3(-initial.x, initial.y, initial.z);
    }

    public static void SetPause()
    {
        for(int i =0; i < instance.team1.Players.Length; i++)
        {
            instance.team1.Players[i].Pause();
            instance.team2.Players[i].Pause();
        }
        instance.team1.Goal.Pause();
        instance.team2.Goal.Pause();
    }

    public static void UnPause()
    {
        for (int i = 0; i < instance.team1.Players.Length; i++)
        {
            instance.team1.Players[i].UnPause();
            instance.team2.Players[i].UnPause();
        }
        instance.team1.Goal.UnPause();
        instance.team2.Goal.UnPause();
    }

}
