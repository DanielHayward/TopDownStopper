using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DKH;


public class Noise
{
    public virtual void Fart() { Debug.Log("Noise"); }
}
public class Boom : Noise
{
    public override void Fart() { Debug.Log("Big Noise"); }
}

public class Ya 
{
    public void Fart() { Debug.Log("Big Noise"); }
}


public class NoiseClass<T> where T : Noise
{
    public T noise;

    public void Play()
    { 
        noise.Fart();
    }
}

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        Ya fart = new Ya();
        fart.Fart();
        NoiseClass<Noise> noises = new NoiseClass<Noise>();
        //noises.noise = fart;
        noises.Play();
    }
}
