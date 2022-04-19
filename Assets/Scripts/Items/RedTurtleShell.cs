using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedTurtleShell : Item
{
    public Sprite sprite;

    [SerializeField]
    private float speed = 2f;

    private Vector3 direction;
    private Team teamEnemy;
    private int indexPlayer;
    private int indexPlayerSave = int.MaxValue;
    private bool isInited = false;

    public override void Init(Team team)
    {
        this.team = team;
        teamEnemy = Field.Team1 == team ? Field.Team2 : Field.Team1;
        isInited = true;
    }
    private void Start()
    {
        ColliderOff(0.2f);
        ChangeTarget();
    }
    protected override void Move()
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    private void Update()
    {
        if (isInited)
        {
            Move();
            FollowTarget();
        }
    }

    private void SearchPlayer()
    {
        float distance = float.MaxValue;
        for (int i = 0; i < teamEnemy.Players.Length; i++)
        {
            float distanceTmp = Vector3.Distance(transform.position, teamEnemy.Players[i].transform.position);

            if (distanceTmp < distance && indexPlayerSave != i)
            {
                distance = distanceTmp;
                indexPlayer = i;
            }
        }
        indexPlayerSave = indexPlayer;
    }
    private void ChangeTarget()
    {
        SearchPlayer();

        direction = teamEnemy.Players[indexPlayer].transform.position - transform.position;
        direction.y = 0;
        transform.LookAt(transform.position + direction);
        direction = direction.normalized;
    }

    private void FollowTarget()
    {
        direction = teamEnemy.Players[indexPlayer].transform.position - transform.position;
        direction.y = 0;
        transform.LookAt(transform.position + direction);
        direction = direction.normalized;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.GetTackled();
            Destroy(this.gameObject);
        }
    }
}
