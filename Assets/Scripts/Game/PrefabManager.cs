using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager instance;

    [SerializeField] private GameObject ball;
    public static GameObject Ball => instance.ball;

    [SerializeField] private GameObject[] prefabItemsReference;
    private Dictionary<Item, GameObject> prefabItems = new Dictionary<Item, GameObject>();
    public static Dictionary<Item, GameObject> PrefabItems => instance.prefabItems;

    private List<Item> itemReference;
    public static List<Item> Item => instance.itemReference;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach(GameObject item in prefabItemsReference)
        {
            Item script = item.GetComponent<Item>();
            prefabItems.Add(script, item);
            itemReference.Add(script);
        }
    }
}
