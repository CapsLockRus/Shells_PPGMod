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

public class RPGChargeBehaviour : MonoBehaviour, Messages.IUse
{
    public Transform frontAttach;
    private bool attached = false;
    private Rigidbody2D rb;
    private PhysicalBehaviour phys;
    private bool _initialized = false;
    private bool _isActivated = false;
    private ThrusterbedBehaviour thrusterbed;
    private SpriteRenderer sr;
    private DamagableMachineryBehaviour dam;
    private ParticleSystem ps;
    private PhysicalBehaviour shellPhys;
    private bool initialized = false;

    public bool spritev2 = false;
    
    private bool burnedOut = false;

    public float burnTime = 2.5f;
    public float thrust = 200f;
    private float burnTimer;
    
    HEATBehaviour shell;
    ThrusterbedBehaviour thruster;

    void Awake()
    {
        var comps = GetComponents<RPGChargeBehaviour>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        //Listen(gameObject);
        phys = GetComponent<PhysicalBehaviour>();

        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setTimer", "Set Burnout Time",
            "Sets burnout time", () =>
            {
                DialogBox dialog = (DialogBox)null;
                dialog = DialogBoxManager.TextEntry(
                    "Enter new burnout time\n<color=orange><size=26>Maximum: 30\nCurrently:" +
                    burnTime + "</size></color>", "Number", new DialogButton("Apply", true, new UnityAction[1]
                    {
                        (UnityAction)(() =>
                        {
                            float setrange;
                            if (float.TryParse(dialog.EnteredText, out setrange))
                            {
                                burnTime = setrange;
                                if (burnTime > 30f) burnTime = 30f;
                            }
                        })
                    }),
                    new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
            }));
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setThrust", "Set Thrust",
            "Sets thrust", () =>
            {
                DialogBox dialog = (DialogBox)null;
                dialog = DialogBoxManager.TextEntry(
                    "Enter new thrust\n<color=orange><size=26>Maximum: 2500\nCurrently:" +
                    thrust + "</size></color>", "Number", new DialogButton("Apply", true, new UnityAction[1]
                    {
                        (UnityAction)(() =>
                        {
                            float setrange;
                            if (float.TryParse(dialog.EnteredText, out setrange))
                            {
                                thrust = setrange;
                                if (thrust > 2500f) thrust = 2500f;
                                thrusterbed.ThrustingForce = thrust;
                            }
                        })
                    }),
                    new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
            }));
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton(
            "nextTexture",
            "Next texture",
            "Next texture",
            () =>
            {
                spritev2 = !spritev2;
                UpdateSprite();
            }
        ));
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton(
            "previousTexture",
            "Previous texture",
            "Previous texture",
            () =>
            {
                spritev2 =  !spritev2;
                UpdateSprite();
            }
        ));
    }
    
    private void Start()
    {
        
        Invoke(nameof(Init), 0f);
        attached = false;
        EnsureAttachPoint();
        thrusterbed =  GetComponent<ThrusterbedBehaviour>();
        thrusterbed.TemperatureTarget = 100f;
        thrusterbed.ThrustingForce = thrust;
        phys.ChargeBurns = false;
        sr = GetComponent<SpriteRenderer>();
        thrusterbed.Activated = false;
        thrusterbed.enabled = true;
        dam.Indestructible = true;
        thrusterbed.BurnRayLength = 10f;
        thrusterbed.BurnRayOffset = new Vector2(1f, 0f);
        thrusterbed.DoBurnOthers = false;
        thrusterbed.TemperatureTarget = 20f;

    }
    
    void Init()
    {
        if (initialized) return;
        initialized = true;

        StartCoroutine(DelayedSprite());
    }

    IEnumerator DelayedSprite()
    {
        yield return null;
        UpdateSprite();
    }
    
    public void UpdateSprite()
    {
        sr.sprite = spritev2 ? Mod.RPGChargeSprite1 : Mod.RPGChargeSprite;
    }

    void EnsureAttachPoint()
    {
        if (frontAttach == null)
        {
            var t =
                transform.Find("FrontAttach");

            if (t != null)
            {
                frontAttach = t;
            }
            else
            {
            }
        }
    }
    
    /*
    private void Listen(GameObject Instance)
    {
        Instance.AddComponent<UseEventTrigger>().Action = () =>
        {
            if (!burnedOut)  _isActivated = true;
            else Use(new ActivationPropagation());
        };
    }
    */
    
    public void Use(ActivationPropagation ap)
    {
        if (_isActivated) thrusterbed.Use(ap);
        if (!burnedOut)  _isActivated = true;
    }
    
    
    void Update()
    {
        if (shellPhys == null && attached)
        {
            Destroy(gameObject);
            return;
        }

        if (burnedOut)
            return; 

        if (_isActivated)
        {
            burnTimer += Time.deltaTime;

            if (burnTimer >= burnTime)
            {
                _isActivated = false;
                burnedOut = true;
                thrusterbed.enabled = false;
                //Use(new ActivationPropagation());
                if (spritev2) return;
                sr.sprite = Mod.RPGChargeSpriteBurnt;
            }
        }
    }
    
    void OnCollisionStay2D(Collision2D col)
    {
        if (attached)
            return;

        var shell = col.gameObject.GetComponent<ConnectBehaviour>();
        
        if (shell == null)
            return;
        
        if (shell.rearOccupied)
            return;

        AttachToShell(shell);
    }

    void AttachToShell(ConnectBehaviour shell)
    {
        var shellRear = shell.rearAttach;

        if (shellRear == null)
            return;

        Rigidbody2D shellRb =
            shell.GetComponent<Rigidbody2D>();

        Rigidbody2D myRb =
            GetComponent<Rigidbody2D>();
        
        Vector3 offset =
            frontAttach.localPosition;

        float sign =
            Mathf.Sign(transform.lossyScale.x);

        offset.x *= sign;

        //transform.position = shellRear.position - offset;
        
        shell.transform.rotation =
            transform.rotation;
        
        shell.rearOccupied = true;
        
        var joint =
            gameObject.AddComponent<FixedJoint2D>();

        joint.connectedBody = shellRb;
        
        joint.autoConfigureConnectedAnchor = false;
        
        joint.anchor =
            frontAttach.localPosition;
        
        joint.connectedAnchor =
            shellRear.localPosition;

        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        attached = true;
        
        myRb.velocity = Vector2.zero;
        myRb.angularVelocity = 0f;

       shellPhys = shell.GetComponent<PhysicalBehaviour>();
    }
}