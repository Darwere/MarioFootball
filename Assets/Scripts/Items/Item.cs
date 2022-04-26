using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Team team;

    public Sprite sprite;
    public abstract void Init(Player player);
    protected abstract void OnCollisionEnter(Collision collision);
    protected abstract void Move();

    protected void ColliderOff(float delay)
    {
        Collider collider = GetComponent<BoxCollider>();
        collider.enabled = false;
        StartCoroutine(ActiveCollision(delay));
    }

    protected IEnumerator ActiveCollision(float delay)
    {
        yield return new WaitForSeconds(delay);
        Collider collider = GetComponent<BoxCollider>();
        collider.enabled = true;
    }
}
