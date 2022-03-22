using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] float PlayerSpeed;
    private float ThirdField;
    private float TwoThirdField;
    private float FieldWidth;

    
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        if (IAManager.instance.debugKeyboardControls)
        {
            /*
            if (GetComponent<AIPlayerData>().color == Teams.Teamcolor.Blue)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    this.transform.position += PlayerSpeed * Vector3.forward * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    this.transform.position += PlayerSpeed * Vector3.left * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.Z))
                {
                    this.transform.position += PlayerSpeed * Vector3.right * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    this.transform.position += PlayerSpeed * Vector3.back * Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.transform.position += PlayerSpeed * Vector3.forward * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.transform.position += PlayerSpeed * Vector3.left * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.transform.position += PlayerSpeed * Vector3.right * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.transform.position += PlayerSpeed * Vector3.back * Time.deltaTime;
                }
            }
            */
        }
        
        

    }
}
