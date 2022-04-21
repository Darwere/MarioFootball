using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private Match debugMatch;
    [SerializeField] private uint timer = 560;

    private static GameManager instance;
    private Queue<Match> matches;
    private Queue<MatchResult> results;
    private MatchResult currentResult;
   

    private void Awake()
    {
        instance = this;

        matches = new Queue<Match>();
        matches.Enqueue(debugMatch);
    }

    private void Start()
    {
        StartCoroutine(DecreaseTimer());
    }

    /// <summary>
    /// Fournit les coéquipiers à chaque équipe, les place, et instancie le ballon
    /// </summary>
    /// <param name="team1">Spermatozoïde n°1</param>
    /// <param name="team2">Spermatozoïde n°2</param>
    /// <returns>RIENG</returns>
    public static void BreedMePlease(Team team1, Team team2)
    {
        Match match = instance.matches.Dequeue();

        instance.currentResult = new MatchResult();
        instance.currentResult.match = match;

        Player[] teammates = new Player[4];

        teammates[0] = Player.CreatePlayer(match.captain1.prefab, team1);
        teammates[0].IsPiloted = true;

        for (int i = 1; i < 4; ++i)
            teammates[i] = Player.CreatePlayer(match.mate1.prefab, team1);

        Player goal1 = Player.CreatePlayer(match.goalKeeper1.prefab, team1, true);

        team1.Init(teammates, goal1);

        teammates = new Player[4];

        teammates[0] = Player.CreatePlayer(match.captain2.prefab, team2);
        teammates[0].IsPiloted = true;

        for (int i = 1; i < 4; ++i)
            teammates[i] = Player.CreatePlayer(match.mate2.prefab, team2);

        Player goal2 = Player.CreatePlayer(match.goalKeeper2.prefab, team2, true);

        team2.Init(teammates, goal2);

        Field.Init(Instantiate(PrefabManager.Ball).GetComponent<Ball>());
    }

    private IEnumerator DecreaseTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            --timer;
            if (timer == 0)
            {
                UIManager.TimeOut();
            }
            UIManager.ActualiseTimer(timer);
        }
    }

    
}
