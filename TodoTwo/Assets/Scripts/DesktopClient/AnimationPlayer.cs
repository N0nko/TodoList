using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayForwardTransition()
    {
        this.gameObject.GetComponent<Animation>().Play("ForwardTransition");
    }
    public void PlayBackwardTransition()
    {
        this.gameObject.GetComponent<Animation>().Play("BackwardTransition");
    }
}
