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
using static UnityEngine.ParticleSystem;


public class PseudoAPBehaviour : MonoBehaviour
{
    private float time = 0f;
    private void Start()
    {
        var APLauncher = GetComponent<MachineGunBehaviour>();
        APLauncher.barrelDirection = Vector2.right;
        APLauncher.barrelPosition = new Vector2(0.1f, 0f);
        APLauncher.Effect.transform.localPosition = APLauncher.barrelPosition;
        gameObject.layer = LayerMask.NameToLayer("Debris");
        APLauncher.Use(new ActivationPropagation());
        GetComponent<SpriteRenderer>().sprite = null;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 10f) Destroy(gameObject);
    }
    
}