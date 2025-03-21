using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem attackEffect;
    public ParticleSystem argumentEffect;
    public ParticleSystem silenceEffect;

    public void PlayParticleEffect(string effectName)
    {
        switch (effectName)
        {
            case "attack":
                attackEffect.Play();
                break;
            case "argument":
                argumentEffect.Play();
                break;
            case "silence":
                silenceEffect.Play();
                break;
        }
    }
}

