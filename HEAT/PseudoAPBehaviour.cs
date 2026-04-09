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
    private float localAngle = 0f;
    private float randomSpread;
    public int RPM;

    private void Start()
    {
        /*
        randomSpread = 
        localAngle +=
            UnityEngine.Random.Range(
                -randomSpread,
                randomSpread
            );

        Vector2 dir =
            Quaternion.Euler(
                0,
                0,
                localAngle
            ) *
            Vector2.right;
            */
        
        var APLauncher = GetComponent<MachineGunBehaviour>();
        APLauncher.barrelDirection = Vector2.right;
        APLauncher.barrelPosition = new Vector2(0.1f, 0f);
        APLauncher.Effect.transform.localPosition = APLauncher.barrelPosition;
        gameObject.layer = LayerMask.NameToLayer("Debris");
        APLauncher.Use(new ActivationPropagation());
        GetComponent<SpriteRenderer>().sprite = Mod.PicrelAP;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 10f) Destroy(gameObject);
    }
    
}