using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimation : MonoBehaviour
{
    public float offset = 0;
    public int property = 1;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += Random.Range(-8*Time.deltaTime, 10 * Time.deltaTime);
        //offset = Random.Range(0f, 1);
        mat.SetTextureOffset(property, new Vector2(offset, 0));
            //SetTextureOffset("_Emmission", new Vector2(offset,0));

        if (offset > 1) offset = 0;
        if (offset < 0) offset = 1;

    }
}
