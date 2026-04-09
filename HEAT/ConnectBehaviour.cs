using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

namespace Mod;

public class ConnectBehaviour() : MonoBehaviour 
{
    [NonSerialized]
    public bool rearOccupied = false;
    public Transform rearAttach;

    void Start()
    {
        EnsureAttachPoint();
    }
    
        
    void EnsureAttachPoint()
    {
        if (rearAttach == null)
        {
            var t =
                transform.Find("RearAttach");

            if (t != null)
            {
                rearAttach = t;
            }
        }
    }
}