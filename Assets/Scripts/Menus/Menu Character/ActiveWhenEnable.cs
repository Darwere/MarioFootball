using UnityEngine;

public class ActiveWhenEnable : MonoBehaviour
{
    public GameObject ObjectToActive;

    private void OnEnable()
    {
        ObjectToActive.SetActive(true);
    }
    private void OnDisable()
    {
        ObjectToActive.SetActive(false);
    }
}
