using UnityEngine;

public class Match : MonoBehaviour
{
    public PlayerSpecs captain1;
    public PlayerSpecs captain2;
    public PlayerSpecs mate1;
    public PlayerSpecs mate2;

    public PlayerSpecs goalKeeper1;
    public PlayerSpecs goalKeeper2;

    public static Match instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
