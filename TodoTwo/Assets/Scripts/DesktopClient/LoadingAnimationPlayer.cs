using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimationPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animation>().Play("LoadingIcon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
