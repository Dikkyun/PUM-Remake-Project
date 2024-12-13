using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimation1 : MonoBehaviour
{
    private Animator SCAnimation;

    // Start is called before the first frame update
    void Start()
    {
        SCAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SceneAnimation()
    {
        if (SCAnimation != null)
        {
            SCAnimation.SetTrigger("TrSceneAwal");
        }
    }

    public void SceneAnimationBuka()
    {
        if ( SCAnimation != null )
        {
            SCAnimation.SetTrigger("TrAnimBuka");
        }
    }

    public void SceneAnimationTutup()
    {
        if (SCAnimation != null)
        {
            SCAnimation.SetTrigger("TrAnimTutup");
        }
    }
}
