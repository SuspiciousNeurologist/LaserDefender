using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;

    void Start(){
        myMaterial= GetComponent<Renderer>().material;
        offSet = new Vector2(0f,backgroundScrollSpeed);
    }

    void Update(){
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
