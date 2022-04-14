using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private Team team;

    protected abstract void Create(Team team);
    protected abstract void OnCollisionEnter(Collision collision);
    protected abstract void Move();
}
