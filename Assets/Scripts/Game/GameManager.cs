using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Match debugMatch;

    private static GameManager instance;
    private Queue<Match> matches;
    private MatchResult currentResult;
    [SerializeField] private uint timer = 60;
    private GameObject characterManager;

    public static bool isPlayable = false;
    private bool validTime = true;
    private float timeLeft = 1f;

    private void Awake()
    {
        instance = this;
        matches = new Queue<Match>();
        matches.Enqueue(debugMatch);
        characterManager = GameObject.Find("CharacterManager");
        ChangeMatch(characterManager.GetComponent<Match>());


    }

    private void Start()
    {

    }

    private void Update()
    {

        if (isPlayable == true && validTime == true)
        {
            DecreaseTimer();
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            validTime = true;
            timeLeft = 1f;
        }
    }

    /// <summary>
    /// Fournit les co�quipiers � chaque �quipe, les place, et instancie le ballon
    /// </summary>
    /// <param name="team1">Spermatozo�de n�1</param>
    /// <param name="team2">Spermatozo�de n�2</param>
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

    private void DecreaseTimer()
    {
        if (isPlayable == true)
        {
            --timer;
            UIManager.ActualiseTimer(timer);
            if (timer == 0)
            {
                UIManager.TimeOut();
            }
            validTime = false;
        }
    }

    public static void ChangeMatch(Match match)
    {
        instance.debugMatch = match;
        instance.matches.Dequeue();
        instance.matches.Enqueue(match);
    }
}
