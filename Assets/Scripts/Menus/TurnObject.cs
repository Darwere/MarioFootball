using UnityEngine;

public class TurnObject : MonoBehaviour
{
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, speed, 0f));
    }
}
