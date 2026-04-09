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


public class ThermoBehaviour : MonoBehaviour
{
    int power = 5;
    
    public Transform rearAttach;
    [NonSerialized]
    public bool rearOccupied = false;

    public Sprite armedS;
    public Sprite unarmedS;
        
    private bool _triggered = false;
    public bool armed = true;
    private SpriteRenderer spriteRenderer;
    private PhysicalProperties physProp;
    private PhysicalBehaviour phys;
    Rigidbody2D rb;
    
    private bool isWaiting = false;
    private float timeWaiting = 0.5f;
    private float time = 0f;

    public bool _isCustom = false;

    public int type;
    public int fragCount;
    public float force;
    
    bool initialized = false;
    
    void Awake()
    {
        //Listen(gameObject);
            
        var comps = GetComponents<ThermoBehaviour>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
        var physicalBehaviour = GetComponent<PhysicalBehaviour>();
        physicalBehaviour.ContextMenuOptions.Buttons.Add(new ContextMenuButton(
            "toggleArmed",
            "Arm / Disarm",
            "Switch fuse",
            ChangeArmed
        ));
        physicalBehaviour.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setDelay", "Set Arming Delay", "Sets arming delay", () => {
            DialogBox dialog = (DialogBox)null;
            dialog = DialogBoxManager.TextEntry("Enter new arming delay (when receiving blue signal) in sec\n<color=green><size=26>Maximum: +" + 30f + ",Currently:"+timeWaiting+"</size></color>", "Number", new DialogButton("Apply", true, new UnityAction[1] {
                    (UnityAction)(() => {
                        float setrange;
                        if (float.TryParse(dialog.EnteredText, out setrange)) {
                            timeWaiting = setrange;
                            if (timeWaiting > 30f) timeWaiting = 30f;
                            if (timeWaiting < 0f) timeWaiting = 0.01f;
                        }
                    })
                }),
                new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
        }));
    }
    
    void Start()
    {
        Invoke(nameof(Init), 0f);
            
        rb =  GetComponent<Rigidbody2D>();

        phys = GetComponent<PhysicalBehaviour>();
        phys.ForceContinuous = true;

        switch (type)
        {
            case 0: armedS = Mod.TBG7SpriteArmed;
                unarmedS = Mod.TBG7SpriteUnarmed;
                break;
        }

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
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = armed ? armedS : unarmedS;
    }
    
    /*
    public void Listen(GameObject Instance)
    {
        Instance.AddComponent<UseEventTrigger>().Action = () =>
        {
            if (!armed) return;
            Detonate();
        };
    }
    */
    
    public void Use(ActivationPropagation ap)
    {
        if (ap.Channel == ActivationPropagation.Red)
        {
            ChangeArmed();
            return;
        }

        if (ap.Channel == ActivationPropagation.Blue)
        {
            if (isWaiting) return;
            isWaiting = true;
            return;
        }

        if (ap.Channel == ActivationPropagation.Green)
        {
            if (!armed) return;
            Detonate();
            return;
        }
    }

    public void ChangeArmed()
    {
        armed = !armed;
        if (armed) spriteRenderer.sprite = armedS;
        else       spriteRenderer.sprite = unarmedS;
    }

    private void FixedUpdate()
    {
        if (isWaiting)
        {
            time += Time.deltaTime;
            if (time > timeWaiting)
            {
                isWaiting = false;
                time = 0f;
                ChangeArmed();
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!armed) return;
        if (_triggered) return;

        float force = col.relativeVelocity.magnitude;
        if (force < 5f)
        {
            return; // чувствительность взрывателя (опять)
        }

        float angle = Vector2.Angle(transform.right, -col.contacts[0].normal);
        if (Mathf.Sign(transform.lossyScale.x) == 1)
        {
            if (angle > 90f) return;
        } else if (angle < 90f || angle > 270f) return;
        Detonate();
        
        _triggered = true;
    }

    public void Detonate()
    {
        var exp = GetComponent<ExplosiveBehaviour>();
        exp.enabled = true;
        exp.Activate();
        exp.Use(new ActivationPropagation());
        exp.Activate();
        Cloud();
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    void Cloud()
    {
        LayerMask mask = LayerMask.GetMask("Objects");
        List<Vector2> gasPoints = new List<Vector2>();
        Queue<Vector2> queue = new Queue<Vector2>();

        float step = 1f;     // плотность газа
        float radius = 4f;    // радиус облака

        Vector2 origin = transform.position;

        queue.Enqueue(origin);
        gasPoints.Add(origin);

        while (queue.Count > 0)
        {
            Vector2 current = queue.Dequeue();

            Vector2[] directions =
            {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right,
                Vector2.up + Vector2.left,
                Vector2.up + Vector2.right,
                Vector2.down + Vector2.left,
                Vector2.down + Vector2.right
            };

            foreach (var dir in directions)
            {
                Vector2 next = current + dir * step;
                
                if (Vector2.Distance(origin, next) > radius)
                    continue;

                if (gasPoints.Contains(next))
                    continue;
                
                RaycastHit2D hit =
                    Physics2D.Linecast(current, next, mask);

                if (hit.collider != null)
                    continue;

                gasPoints.Add(next);
                queue.Enqueue(next);
            }
        }
        foreach (var point in gasPoints)
        {
            ExplosionCreator.Explode(point, 1);
        }
    }
    
}