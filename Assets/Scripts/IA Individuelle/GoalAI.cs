using UnityEngine;

public class GoalAI : MonoBehaviour
{
    [SerializeField] private Vector3 goalCenter;
    [SerializeField] IAManager gameManager;

    [SerializeField] public Teams.Teamcolor teamcolor;
    [Range(1,10)]
    [SerializeField] float goalRadius;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = IAManager.instance;
        if (teamcolor == Teams.Teamcolor.Blue)
        {
            goalCenter = Teams.blueTeam[0].GetComponent<PlacementAIBrain>().TeamGoalPos;
            goalCenter.y = 0.5f;
        }
        else
        {
            goalCenter = Teams.redTeam[0].GetComponent<PlacementAIBrain>().TeamGoalPos;
            goalCenter.y = 0.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gameManager.playerWithBall.transform);
        transform.position = goalCenter + (gameManager.playerWithBall.transform.position - goalCenter).normalized * goalRadius;
    }
}
