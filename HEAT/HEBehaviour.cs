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


public class HEBehaviour : MonoBehaviour, Messages.IUse
{
    
    public Transform rearAttach;
    [NonSerialized]
    public bool rearOccupied = false;

    public Sprite armedS;
    public Sprite unarmedS;

    public bool isFlamed = true;
    
    public bool alwaysArmed = false;
        
    private bool _triggered = false;
    public bool armed = true;
    private SpriteRenderer spriteRenderer;
    private PhysicalProperties physProp;
    private PhysicalBehaviour phys;
    Rigidbody2D rb;
    
    ActOnShot shot;

    public bool _isCustom = false;

    public int type;
    public int fragCount;
    public float force = 1f;
    
    private float time = 0f;
    private bool isWaiting = false;
    public float timeWaiting = 0.5f;
    
    bool initialized = false;
    
    public bool _isAirbust = false;
    public float airbustHeight = 0f;
    public float maxAirbustHeight = 0f;

    private bool buttonInit = false;
    
    public float randomSpread = 0.7f;
    
    LayerMask mask = LayerMask.GetMask("Objects");
    
    //HashSet<PhysicalBehaviour> connected = new HashSet<PhysicalBehaviour>();
    
    void Awake()
    {
        //Listen(gameObject);
            
        var comps = GetComponents<HEBehaviour>();
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
        physicalBehaviour.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setAirburstHeight", "Set Airburst Height", "Set airburst height", () => {
            DialogBox dialog = (DialogBox)null;
            dialog = DialogBoxManager.TextEntry("Enter new airburst height (0 to disable) in meters\n<color=orange><size=26>Maximum: +" + maxAirbustHeight + "\n(if it's zero then this munition doesn't support airburst)" + ",Currently:"+airbustHeight+"</size></color>", "Number", new DialogButton("Apply", true, new UnityAction[1] {
                    (UnityAction)(() => {
                        float setrange;
                        if (float.TryParse(dialog.EnteredText, out setrange)) {
                            airbustHeight = setrange;
                            if (airbustHeight > maxAirbustHeight) airbustHeight = maxAirbustHeight;
                            if (airbustHeight < 0f) airbustHeight = 0f;
                        }
                    })
                }),
                new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
        }));
        physicalBehaviour.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setFlammability", "Set Explode On Heated", "Set Explode On Temperature", () => {
            DialogBox dialog = (DialogBox)null;
            dialog = DialogBoxManager.TextEntry("Change whether the shell should explode when heated\n<color=green><size=26>Current: +" + isFlamed+ "</size></color>", "placeholder, this field doesn't do anything", new DialogButton("Enable", true, new UnityAction[1] {
                    (UnityAction)(() =>
                    {
                        isFlamed = true;
                    })
                }),
                new DialogButton("Disable", true, (UnityAction)(() => isFlamed = false)));
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
            case 0: 
                armedS = Mod.RPG7OSpriteArmed; 
                unarmedS = Mod.RPG7OSpriteUnarmed;
                break;
            case 1:
                armedS = Mod.HESpriteArmed;
                unarmedS = Mod.HESpriteUnarmed;
                break;
            case 2: 
                armedS = Mod.HE25mm;
                unarmedS = Mod.HE25mm;
                break;
            case 3: 
                armedS = Mod.HESHSpriteArmed;
                unarmedS = Mod.HESHSpriteUnarmed;
                break;
            case 4:
                armedS = Mod.Mortar80mmArmed;
                unarmedS = Mod.Mortar80mmUnarmed;
                break;
            case 5:
                armedS = Mod.Howitzer155mmArmed;
                unarmedS = Mod.Howitzer155mmUnarmed;
                break;
        }
        
        var exp = GetComponent<ExplosiveBehaviour>();
        if (exp != null && force >= 3f) Destroy(exp);

        shot = GetComponent<ActOnShot>();
        shot.Actions.AddListener(Shot);
        shot.CartridgeDamageThreshold = 20f;
        
        var physicalBehaviour = GetComponent<PhysicalBehaviour>();

        phys.Properties = ModAPI.FindPhysicalProperties("Flammable metal");

    }

    void Shot()
    {
        if (force < 3f)
        {
            var exp = GetComponent<ExplosiveBehaviour>();
            exp.enabled = true;
            exp.Activate();
            exp.Use(new ActivationPropagation());
            exp.Activate();
        }
        else
        {
            ExplosionCreator.Explode(transform.position, force);
            Destroy(gameObject);
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
            Detonate();
            return;
        }
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
        
        if (_isAirbust && airbustHeight > 0f && rb.velocity.magnitude > 5f && transform.right.y < 0)
        {
            if (transform.position.y <= airbustHeight && armed) Detonate();
        }

        if (phys != null && (phys.Temperature > 400f) && isFlamed)
        {
            if (force < 3f)
            {
                var exp = GetComponent<ExplosiveBehaviour>();
                exp.enabled = true;
                exp.Activate();
                exp.Use(new ActivationPropagation());
                exp.Activate();
            }
            else
            {
                ExplosionCreator.Explode(transform.position, force);
                Destroy(gameObject);
            }
        }
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

    public void ChangeArmed()
    {
        if (alwaysArmed) return;
        armed = !armed;
        if (armed) spriteRenderer.sprite = armedS;
        else       spriteRenderer.sprite = unarmedS;
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

        //float sector = 90f;
        if (rb.velocity.magnitude < 10f)
        {
            float angle = Vector2.Angle(transform.right, -col.contacts[0].normal);
            if (Mathf.Sign(transform.lossyScale.x) == 1)
            {
                if (angle > 90f) return;
            }
            else if (angle < 90f || angle > 270f) return;
        }

        Detonate();
        
        _triggered = true;
    }

    public void Detonate()
    {
        if(!armed || _triggered) return;
        
        SpawnFragmentsRadial(transform.position, fragCount, default,randomSpread);
        if (force < 3f)
        {
            var exp = GetComponent<ExplosiveBehaviour>();
            exp.enabled = true;
            exp.Activate();
            exp.Use(new ActivationPropagation());
            exp.Activate();
            //StartCoroutine(DelayedDestroy());
        }
        else
        {
            ExplosionCreator.Explode(transform.position, force);
            Destroy(gameObject);
        }
    }
    
    void SpawnFragmentsRadial(
    Vector2 position,
    int count = 24,
    float sectorSize = 100f,     // размер верх/нижнего сектора
    float mainBias = 0.7f,      // сколько % вверх/вниз
    float randomSpread = 5f    // доп. рандом спред
)
{

    int mainCount =
        Mathf.RoundToInt(count * mainBias);

    int sideCount =
        count - mainCount;

    int halfMain =
        mainCount / 2;

    float halfSector =
        sectorSize / 2f;

    // ===== ВЕРХНИЙ СЕКТОР =====
    for (int i = 0; i < halfMain; i++)
    {
        float localAngle =
            UnityEngine.Random.Range(
                -halfSector,
                halfSector
            );

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
            transform.up;

        SpawnFragmentos(position, dir);
    }

    // ===== НИЖНИЙ СЕКТОР =====
    for (int i = 0; i < halfMain; i++)
    {
        float localAngle =
            UnityEngine.Random.Range(
                180f - halfSector,
                180f + halfSector
            );

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
            transform.up;

        SpawnFragmentos(position, dir);
    }

    // ===== БОКОВЫЕ (меньшинство) =====
    for (int i = 0; i < sideCount; i++)
    {
        float angle =
            UnityEngine.Random.Range(
                0f,
                360f
            );

        Vector2 dir =
            Quaternion.Euler(
                0,
                0,
                angle
            ) *
            transform.up;

        SpawnFragmentos(position, dir);
    }
}
    
    /*
    void SpawnFragmentsRadial(Vector2 position, int count = 24)
    {
        Debug.Log("SpawnFragmentsRadial");
        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i;
            
            angle += UnityEngine.Random.Range(-10f, 10f);

            Vector2 dir =
                Quaternion.Euler(0, 0, angle) *
                Vector2.right;

            SpawnFragmentos(position, dir);
        }
    }
    */
    
    private void SpawnFragmentos(Vector2 position, Vector2 dir)
    {
        
        var obj = ModAPI.FindSpawnable("Crossbow Bolt");

        var fragment = Instantiate(obj.Prefab.gameObject, position + dir * 0.3f, Quaternion.identity
        );
        
        fragment.name = "Fragment HE {NON-INTER}";

        var rb = fragment.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = fragment.AddComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0.1f;

        float speed =
            UnityEngine.Random.Range(35f, 45f);

        rb.velocity = dir.normalized * speed;
        
        
        var ptv = fragment.AddComponent<PointToVelocityBehaviour>();
        ptv.intensity = 0.001f;

        var col =
            fragment.GetComponent<Collider2D>();

        col.isTrigger = false;

        col.sharedMaterial =
            new PhysicsMaterial2D
            {
                bounciness = 0f,
                friction = 0.3f
            };

        var sp =
            fragment.GetComponent<SpriteRenderer>();

        sp.sprite = Mod.PicrelGrey;

        fragment.FixColliders();

        var phys =
            fragment.GetComponent<PhysicalBehaviour>();

        phys.StabWoundSizeMultiplier = 3f;
        phys.ForceContinuous = true;
        phys.Temperature = 100f;
        phys.TrueInitialMass = 0.1f;
        phys.InitialMass = 0.1f;
        phys.DisplayBloodDecals = false;
        phys.HasOutline = false;
        phys.StabCausesWound = true;
        phys.SpawnSpawnParticles = false;
        phys.Selectable = false;

        rb.mass = 0.1f;

        var beh =
            fragment.AddComponent<FragmentRay>();

        sp.color =
            new Color(1f, 1f, 1f, 0.2f);
        
    }
    
    public class FragmentRay : MonoBehaviour
    {
        private LineRenderer lineF;
        private PhysicalBehaviour phys;
        private Rigidbody2D rb;
        private float spawnTime;
        private float timed;

        private float multiplier;
        private bool _spawned = false;

        void Start()
        {
            phys = GetComponent<PhysicalBehaviour>();
            lineF = gameObject.AddComponent<LineRenderer>();
            rb = GetComponent<Rigidbody2D>();
            lineF.positionCount = 2;

            lineF.startWidth = 0.025f;
            lineF.endWidth = 0.01f;

            lineF.material = ModAPI.FindMaterial("VeryBright");

            lineF.startColor =
                new Color(1f, 1f, 0f, 0.05f);

            lineF.endColor =
                new Color(1f, 0.3f, 0f, 0f);
            
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
        private void Update()
        {
            timed +=  Time.deltaTime;
            if (timed > 5f)
            {
                Destroy(gameObject);
                return;
            }
                
            while (rb.velocity.magnitude > 50f) rb.velocity *= 0.5f;
            multiplier = (phys.Temperature / 250) * (rb.velocity.magnitude / 10);
            if (multiplier > 1f) multiplier = 1f;
            Vector3 backOffset = - (Vector3)rb.velocity.normalized * (0.3f * multiplier); // 0.3f - макс длина
                
            lineF.SetPosition(
                0,
                transform.position);
            lineF.SetPosition(1, transform.position + backOffset);
            float rayLength = rb.velocity.magnitude * Time.deltaTime;
            var hit = Physics2D.Raycast(transform.position, rb.velocity, rayLength + 0.05f);
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                var phys = hit.collider.GetComponent<PhysicalBehaviour>();
                phys.Shot(new Shot(hit.normal, hit.point, 30f));
                var name = hit.collider.gameObject.name;
                if (name.Contains("Left wall") || name.Contains("Right wall") || name.Contains("Ceiling") || name.Contains("Root")) Destroy(gameObject);
            }
        }
        
        /*
        private void OnCollisionEnter2D(Collision2D col)
        {
            phys = col.collider.GetComponent<PhysicalBehaviour>();
            phys.Shot(new Shot(-rb.velocity, col.transform.position, 50));
            
            //if (col.collider.gameObject.name == "Wall") Destroy(gameObject);
        }
        */
    }

}