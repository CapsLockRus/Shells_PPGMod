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

    void FixedUpdate()
    {
        if (phys.Temperature > 800f || phys.OnFire) Explode();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<HEATBehaviour>() != null) return;
        float force = col.relativeVelocity.magnitude;
        if (force > 500f) Explode();
    }

    public void Explode()
    {
        ExplosionCreator.Explode(transform.position, power);
        Destroy(gameObject);
    }
}