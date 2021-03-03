using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroll : MonoBehaviour
{
    //public float speed = 0.5f;
    Material material;
    Vector2 offset;

    public float xVelocity, yVelocity;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
        
        
        //MeshRenderer mr = GetComponent<MeshRenderer>();
        //Material mat = mr.material;
        //Vector2 offset = mat.mainTextureOffset;
        //offset.x += Time.deltaTime;

        //Vector2 offset = new Vector2(Time.time * speed, 0);
        //Renderer.material.GetTextureOffset = offset;

    }
}
