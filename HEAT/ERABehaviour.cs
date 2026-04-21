namespace Mod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class ERABehaviour : MonoBehaviour
{
    private float power = 2f;
    private PhysicalBehaviour phys;
    void Awake()
    {
        var comps = GetComponents<ERABehaviour>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
        
        phys = GetComponent<PhysicalBehaviour>();
    }
/*
    public void Use(ActivationPropagation ap)
    {
        if (ap.Channel == ActivationPropagation.Blue)
        {
            Explode();
        }
    }
*/
    void FixedUpdate()
    {
        if (phys.Temperature > 800f || phys.OnFire) ExplodeERA();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<HEATBehaviour>() != null) return;
        float force = col.relativeVelocity.magnitude;
        if (force > 500f) ExplodeERA();
    }

    public void ExplodeERA()
    {
        ExplosionCreator.Explode(transform.position, power);
        Destroy(gameObject);
    }
}