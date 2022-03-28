using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private static Field instance;

    [SerializeField] private GameObject cam;

    [SerializeField] private float width;
    [SerializeField] private float height;

    [SerializeField] private Team team1, team2;
    [SerializeField] private Team attackTeam, defTeam;



    [SerializeField] private List<Transform> attackPos;
    [SerializeField] private List<Transform> defPos;


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
    public static GameObject Cam => instance.cam;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bottomLeftCorner = new Vector3(width / 2, 0, -height / 2) + transform.position;
        bottomRightCorner = new Vector3(-width / 2, 0, -height / 2) + transform.position;
        topLeftCorner = new Vector3(-width / 2, 0, height / 2) + transform.position;
        topRightCorner = new Vector3(-width / 2, 0, height / 2) + transform.position;

        heightOneThird = topLeftCorner.x + height / 3f;
        heightTwoThirds = topLeftCorner.x + height * 2f / 3f;

        heightOneSixths = topLeftCorner.x + height / 6f;
        heightThreeSixths = topLeftCorner.x + height * 3f / 6f;
        heightFiveSixths = topLeftCorner.x + height * 5f / 6f;
        attackTeam = Team1;
        defTeam = Team2;
        GameManager.BreedMePlease(team1, team2);


    }
    /// <summary>
    /// Assigne le ballon cr�� puis le positionne ainsi que les joueurs
    /// </summary>
    /// <param name="ball">Le ballon</param>
    public static void Init(Ball ball)
    {
        instance.ball = ball;

        ball.transform.position = instance.transform.position + ball.transform.position;
        //Cam.GetComponent<CinemachineVirtualCamera>().Follow = ball.transform;
        //Cam.GetComponent<CinemachineVirtualCamera>().LookAt = ball.transform;

        SetTeamPosition(Team1);
        CameraManager.Init();
    }

    public static void SetTeamPosition(Team _attackTeam)
    {
        if (_attackTeam != instance.attackTeam)
        {
            Team temp = instance.attackTeam;
            instance.attackTeam = _attackTeam;
            instance.defTeam = temp;
        }
        
        for (int i = 0; i < Team1.Players.Length; i++)
        {
            instance.attackTeam.Players[i].transform.position = instance.attackPos[i].position;
            instance.defTeam.Players[i].transform.position = instance.XAxisSymmetry(instance.defPos[i].position);
        }
        instance.attackTeam.Goal.transform.position = instance.attackPos[instance.attackPos.Count-1].position;
        instance.defTeam.Goal.transform.position = instance.XAxisSymmetry(instance.defPos[instance.defPos.Count-1].position);

    }


    public Vector3 XAxisSymmetry(Vector3 initial)
    {
        return new Vector3(-initial.x,initial.y,initial.z);
    }

}
