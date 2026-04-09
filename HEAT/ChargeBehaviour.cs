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
public class ChargeBehaviour : MonoBehaviour
{
    public Transform frontAttach;
    public float force = 200f;
    private bool attached = false;
    private Rigidbody2D rb;
    private PhysicalBehaviour phys;
    private bool _initialized = false;
    public bool _arrowSprite = true;
    private SpriteRenderer spriteRenderer;

    public bool shouldSendBlue = false;
    
    
    
    void Awake()
    {
        var comps = GetComponents<ChargeBehaviour>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
        
        rb = GetComponent<Rigidbody2D>();
        //Listen(gameObject);
        phys = GetComponent<PhysicalBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setForce", "Set Detonation Force", "Sets detonation force value", () => {
            DialogBox dialog = (DialogBox)null;
            dialog = DialogBoxManager.TextEntry("Enter new detonation force\n<color=orange><size=26>Maximum: 2500\nCurrently:"+force+"</size></color>", "Number", new DialogButton("Apply", true, new UnityAction[1] {
                    (UnityAction)(() => {
                        float setrange;
                        if (float.TryParse(dialog.EnteredText, out setrange)) {
                            force = setrange;
                            if (force > 2500f) force = 2500f;
                        }
                    })
                }),
                new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
        }));
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setBlue", "Set Auto Delay-Arm", "Switch auto delay-arm", () => {
                    DialogBox dialog = (DialogBox)null;
                    dialog = DialogBoxManager.TextEntry("Enable/disable auto delay arm\n<color=blue><size=26>If enabled, on detonating charge will automatically send blue signal to warhead starting it's delayed arming timer\nCurrently: " + shouldSendBlue + "</size></color>", "placeholder field so i could use this preset to explain the function", new DialogButton("Enable", true, new UnityAction[1] {
                            (UnityAction)(() =>
                            {
                                shouldSendBlue = true;
                            })
                        }),
                        new DialogButton("Disable", true, (UnityAction)(() =>
                        {
                            shouldSendBlue = false;
                        })));
                }));
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton(
            "nextTexture",
            "Next texture",
            "Next texture",
            () =>
            {
                _arrowSprite = !_arrowSprite;
                UpdateSprite();
            }
        ));
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton(
            "previousTexture",
            "Previous texture",
            "Previous texture",
            () =>
            {
                _arrowSprite = !_arrowSprite;
                UpdateSprite();
            }
        ));
        
        

    }

    private void Start()
    {
        attached = false;
        Invoke(nameof(Init), 0f);
        EnsureAttachPoint();
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

    void Init()
    {
        if (_initialized) return;
        _initialized = true;

        StartCoroutine(DelayedSprite());
    }

    IEnumerator DelayedSprite()
    {
        yield return null;
        UpdateSprite();
    }
    
    public void UpdateSprite()
    {
        spriteRenderer.sprite = _arrowSprite ? Mod.ChargeSpriteArrow : Mod.ChargeSprite;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 50f)
        {
            Explode();
        }
    }

    public void FixedUpdate()
    {
        if (phys.Temperature > 400f)
        {
            Explode();
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (attached)
            return;

        var shell =
            col.gameObject.GetComponent<ConnectBehaviour>();
        
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
    }
    
    void Explode()
    {
        Vector2 pos =
            transform.position;

        ExplosionCreator.Explode(pos, 5f);
        Destroy(gameObject);
    }
    
    public void Use(ActivationPropagation ap)
    {
        if (ap.Channel == ActivationPropagation.Red)
        {
            return;
        }

        if (ap.Channel == ActivationPropagation.Blue)
        {
            shouldSendBlue = true;
            return;
        }

        if (ap.Channel == ActivationPropagation.Green)
        {
            Detonate();
            return;
        }
    }

    /*
    private void Listen(GameObject Instance)
    {
        Instance.AddComponent<UseEventTrigger>().Action = () =>
        {
            Detonate();
            Destroy(gameObject);
        };
    }
    */
    
    public void Detonate()
    {
        ModAPI.CreateParticleEffect("Explosion", transform.position);
        
        var joint =
            GetComponent<FixedJoint2D>();

        if (joint == null)
        {
            Destroy(gameObject);
            return;
        }

        var shellRb =
            joint.connectedBody;

        if (shellRb == null)
            return;
        
        if (shouldSendBlue)
        {
            var heat = shellRb.GetComponent<HEATBehaviour>();

            if (heat != null)
            {
                heat.Use(
                    new ActivationPropagation(true,
                        ActivationPropagation.Blue //  <------ ChatJeetPT code, no sane person uses enter key that much
                        ) 
                );
            }

            var he = shellRb.GetComponent<HEBehaviour>();

            if (he != null)
            {
                he.Use(
                    new ActivationPropagation(true, ActivationPropagation.Blue )
                );
            }
        }

        Vector2 forward =
            transform.right * Mathf.Sign(transform.lossyScale.x);

        shellRb.AddForce(
            forward * force,
            ForceMode2D.Impulse
        );

        Destroy(gameObject);
    }
}