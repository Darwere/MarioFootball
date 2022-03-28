using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float RightBorder;
    public float LeftBorder;

    private float yValues;
    private float zValues;
    private GameObject target;
    private static CameraManager instance;
    private void Awake()
    {
        instance = this;

        Debug.Log(instance.name);
    }

    public static void Init()
    {
        //instance.target = ball.gameObject;
        
        instance.yValues = instance.transform.position.y;
        //instance.zValues = instance.transform.position.z;
       
    }
    private void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Field.Ball.transform.position.x > RightBorder && Field.Ball.transform.position.x<LeftBorder)
        {
            Debug.Log("dans le if");
            Vector3 newCamPosition = new Vector3(Field.Ball.transform.position.x, yValues, Field.Ball.transform.position.z);
            gameObject.transform.position = newCamPosition;
        }
        else
        {
            Vector3 newCamPosition = new Vector3(transform.position.x, yValues, Field.Ball.transform.position.z);
            transform.position = newCamPosition;
        }

        
        
        //else
        //{
        //    Debug.Log("else");
        //    gameObject.transform.position = new Vector3(RightBorder - 0.1f,yValues, transform.position.z) ;
        //}
        
        
    }

}
