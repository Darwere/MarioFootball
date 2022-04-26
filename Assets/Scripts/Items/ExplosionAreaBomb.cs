using UnityEngine;

public class ExplosionAreaBomb : MonoBehaviour
{
    private float timeLeft = 1.5f;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.GetTackled();
        }
    }
}
