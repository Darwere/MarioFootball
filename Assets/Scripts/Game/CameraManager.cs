using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float RightBorder;
    public float LeftBorder;
    public float TopBorder;
    public float BottomBorder;

    private float yValues;
    private float zValues;
    private static CameraManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static void Init()
    {
        instance.yValues = instance.transform.position.y;
        //instance.zValues = instance.transform.position.z;

    }
    private void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Field.Ball.transform.position.x > RightBorder && Field.Ball.transform.position.x < LeftBorder && Field.Ball.transform.position.z < TopBorder && Field.Ball.transform.position.z > BottomBorder)
        {

            Vector3 newCamPosition = new Vector3(Field.Ball.transform.position.x, yValues, Field.Ball.transform.position.z);
            gameObject.transform.position = newCamPosition;
        }
        else
        {
            if ((Field.Ball.transform.position.z > TopBorder || Field.Ball.transform.position.z < BottomBorder) && (Field.Ball.transform.position.x < RightBorder || Field.Ball.transform.position.x > LeftBorder))
            {

                Vector3 newCamPosition = new Vector3(transform.position.x, yValues, transform.position.z);
                transform.position = newCamPosition;

            }
            else if (Field.Ball.transform.position.x < RightBorder || Field.Ball.transform.position.x > LeftBorder)
            {
                
                Vector3 newCamPosition = new Vector3(transform.position.x, yValues, Field.Ball.transform.position.z);
                transform.position = newCamPosition;

            }
            else if (Field.Ball.transform.position.z > TopBorder || Field.Ball.transform.position.z < BottomBorder)
            {
                
                Vector3 newCamPosition = new Vector3(Field.Ball.transform.position.x, yValues, transform.position.z);
                transform.position = newCamPosition;

            }
        }
    }
}