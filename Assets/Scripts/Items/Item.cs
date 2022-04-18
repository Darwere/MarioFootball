using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Team team;
    public abstract void Init(Team team);
    protected abstract void OnCollisionEnter(Collision collision);
    protected abstract void Move();
}
