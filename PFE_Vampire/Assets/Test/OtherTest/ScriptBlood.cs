using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBlood : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    RenderTexture rt;
    [SerializeField]
    Transform target;
    void Start()
    {
        Shader.SetGlobalTexture("_GlobalEffectRT", rt);
        Shader.SetGlobalFloat("_OrthographicCamSize", GetComponent<Camera>().orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_Position", transform.position);
    }
}