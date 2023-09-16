using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Particle
{
    public string Name;
    public ParticleSystem ParticleSystem;
}



public class ParticleController : MonoBehaviour
{
    
    public List<Particle> Particles = new List<Particle>();


    public void PlayParticle(string name)
    {
        
    }
    
    public void PlayParticle(int index)
    {
        Particles[index].ParticleSystem.Play();
    }
    
}
