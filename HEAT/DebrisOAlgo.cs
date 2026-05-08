using System.Collections;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Mod;


// DebugLogDestroyer3000ErrorsPerSecondV1488

//also i have absolutely zero idea how to properly create destructible items so this is the best i could come up with and it still sucks so this is an example of how NOT to do it
public class DebrisOAlgo : MonoBehaviour
{
    PhysicalBehaviour physical;
    void Awake()
    {
        var comps = GetComponents<DebrisOAlgo>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
        
        physical = GetComponent<PhysicalBehaviour>();
    }
    
    void Start()
    {
        Debug.Log("Debris start called");
        
        var p1 = new GameObject("Debris1");
        p1.transform.position = transform.position;
        p1.transform.rotation = transform.rotation;
        p1.transform.localScale = transform.lossyScale;
        p1.AddComponent<SpriteRenderer>().sprite = Mod.RefConcreteP1;
        p1.AddComponent<BoxCollider2D>();
        
        var rb1 = p1.AddComponent<Rigidbody2D>();
        rb1.mass = 13;
        rb1.velocity = GetComponent<Rigidbody2D>().velocity;
        
        var phys1 = p1.AddComponent<PhysicalBehaviour>();
        phys1.Properties = ModAPI.FindPhysicalProperties("Concrete");
        phys1.Selectable = true;
        phys1.Temperature = physical.Temperature/4;
        phys1.SpawnSpawnParticles = false;
        p1.FixColliders();
        p1.SetLayer(9);
        p1.AddComponent<DebrisComponent>();
        
        
        var p2 = new GameObject("Debris2");
        p2.transform.position = transform.position;
        p2.transform.rotation = transform.rotation;
        p2.transform.localScale = transform.lossyScale;
        p2.AddComponent<SpriteRenderer>().sprite = Mod.RefConcreteP2;
        p2.AddComponent<BoxCollider2D>();
        
        var rb2 = p2.AddComponent<Rigidbody2D>();
        rb2.mass = 13;
        rb2.velocity = GetComponent<Rigidbody2D>().velocity;
        
        var phys2 = p2.AddComponent<PhysicalBehaviour>();
        phys2.Properties = ModAPI.FindPhysicalProperties("Concrete");
        phys2.Selectable = true;
        phys2.Temperature = physical.Temperature/4;
        phys2.SpawnSpawnParticles = false;
        p2.FixColliders();
        p2.SetLayer(9);
        p2.AddComponent<DebrisComponent>();
        
        
        var p3 = new GameObject("Debris3");
        p3.transform.position = transform.position;
        p3.transform.rotation = transform.rotation;
        p3.transform.localScale = transform.lossyScale;
        p3.AddComponent<SpriteRenderer>().sprite = Mod.RefConcreteP3;
        p3.AddComponent<BoxCollider2D>();
        
        var rb3 = p3.AddComponent<Rigidbody2D>();
        rb3.mass = 13;
        rb3.velocity = GetComponent<Rigidbody2D>().velocity;
                
        var phys3 = p3.AddComponent<PhysicalBehaviour>();
        phys3.Properties = ModAPI.FindPhysicalProperties("Concrete");
        phys3.Selectable = true;
        phys3.Temperature = physical.Temperature/4;
        phys3.SpawnSpawnParticles = false;
        p3.FixColliders();
        p3.SetLayer(9);
        p3.AddComponent<DebrisComponent>();
        
        
        var p4 = new GameObject("Debris4");
        p4.transform.position = transform.position;
        p4.transform.rotation = transform.rotation;
        p4.transform.localScale = transform.lossyScale;
        p4.AddComponent<SpriteRenderer>().sprite = Mod.RefConcreteP4;
        p4.AddComponent<BoxCollider2D>();
        
        var rb4 = p4.AddComponent<Rigidbody2D>();
        rb4.mass = 13;
        rb4.velocity = GetComponent<Rigidbody2D>().velocity;
        
        var phys4 = p4.AddComponent<PhysicalBehaviour>();
        phys4.Properties = ModAPI.FindPhysicalProperties("Concrete");
        phys4.Selectable = true;
        phys4.Temperature = physical.Temperature/4;
        phys4.SpawnSpawnParticles = false;
        p4.FixColliders();
        p4.SetLayer(9);
        p4.AddComponent<DebrisComponent>();
        
        Destroy(gameObject);
    }
    
    
}

public class ReinforcedConcreteThing : MonoBehaviour
{
    void Awake()
    {
        var comps = GetComponents<ReinforcedConcreteThing>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        var des1 = GetComponent<DestroyableBehaviour>();
        if(des1) Destroy(des1);

        var des = gameObject.GetOrAddComponent<NonRetardedDestroyableBehaviour>();

        des.OverallChance = 1f;
        des.MinimumShotDamage = 120; 
        //des.ShotBreakChance = 1f;
        des.MinimumExplosionFragmentDamage = 110; 
        des.MinimumImpactForce = 1600; 
        des.ReactToBullets = false;
        des.RestoreIncomingVelocityWhenDestroyedRatio = 0.5f;
        des.baseHealth = 300;

        //var prefabbot = new GameObject("this is stupid");

        var prefabbot = ModAPI.FindSpawnable("Brick");

        GameObject copy = Instantiate(prefabbot.Prefab);
        
        var deb = copy.GetOrAddComponent<DebrisOAlgo>();
        copy.GetOrAddComponent<Rigidbody2D>().isKinematic = false;
        var phys = copy.GetOrAddComponent<PhysicalBehaviour>();
        //phys.Properties = ModAPI.FindPhysicalProperties("Metal");
        phys.SpawnSpawnParticles = false;
        copy.layer = 0;
        Destroy(copy.GetComponent<SpriteRenderer>());
        
        des.DebrisPrefab = copy;
        
        copy.SetActive(false);
    }
}

public class NonRetardedDestroyableBehaviour : DestroyableBehaviour
{
    // why do i have to do this? because for some reason, a rocket launched from a rocket launcher doesnt do a damage check and instead just straight up broadcasts Break function destroying literally anything no matter the actual resistance
    
    private PhysicalBehaviour phys;
    private bool broken;
    private static readonly ContactPoint2D[] buffer = new ContactPoint2D[8];

    private float moddedImpactForce = 1600;
    public float baseHealth = 300;
    private float moddedHealth = 300;
    private float health = 300;
    
    protected override void Update() { }

    private void Start()
    {
        moddedHealth = baseHealth;
        moddedImpactForce = MinimumImpactForce;
    }

    private Vector2 curSize = new Vector2(0, 0);
    
    protected void FixedUpdate()
    {
        if ((Vector2)transform.localScale != curSize)
        {
            curSize = transform.localScale;
            moddedHealth = baseHealth * Mathf.Pow(curSize.x*curSize.y, 1);
            moddedImpactForce = MinimumImpactForce * Mathf.Pow(curSize.x*curSize.y, 2f/3);
            Debug.Log(moddedHealth);
            Debug.Log(moddedImpactForce);
        }
        health = moddedHealth;
    }

    private void OnCollisionStay2D(Collision2D collision) => this.EvaluateCollision(collision);

    private void OnCollisionEnter2D(Collision2D collision) => this.EvaluateCollision(collision);

    private void EvaluateCollision(Collision2D collision)
    {
        if (!this.ReactToCollision)
            return;
        int contacts = collision.GetContacts(buffer);
        if ((double) Utils.GetMinImpulse(buffer, contacts) < (double) moddedImpactForce)
            return;
        ExplosiveBehaviour component;
        if (this.TryGetComponent<ExplosiveBehaviour>(out component) && (double) component.ImpactForceThreshold > 0.0)
        {
            component.ForceImmediateExplosion();
        }
        else
        {
            PhysicalBehaviour physicalBehaviour;
            if (Global.main.PhysicalObjectsInWorldByTransform.TryGetValue(collision.collider.transform, out physicalBehaviour) && physicalBehaviour.rigidbody.bodyType == RigidbodyType2D.Dynamic)
                physicalBehaviour.rigidbody.velocity = Vector2.Lerp(physicalBehaviour.rigidbody.velocity, physicalBehaviour.GetPreviousVel(), this.RestoreIncomingVelocityWhenDestroyedRatio);
            ActualBreak(collision.otherRigidbody.velocity - collision.relativeVelocity);
        }
    }
    
    public new void OnFragmentHit(float force)
    {
        if (!this.ReactToExplosions) //  || (double) force < (double) this.MinimumExplosionFragmentDamage
            return;

        health -= force;
        
        if (health > 0) return;
        
        ExplosiveBehaviour component;
        if (this.TryGetComponent<ExplosiveBehaviour>(out component) && component.ExplodesOnFragmentHit)
            component.ForceImmediateExplosion();
        else ActualBreak();
    }
    
    public new void Shot(global::Shot shot)
    {
        if (!this.ReactToBullets || (double) shot.damage < (double) this.MinimumShotDamage || (double) this.ShotBreakChance < (double) UnityEngine.Random.value)
            return;
        ExplosiveBehaviour component;
        if (this.TryGetComponent<ExplosiveBehaviour>(out component) && (double) component.ShotExplodeChance > 1.401298464324817E-45)
            component.ForceImmediateExplosion();
        else
            this.StartCoroutine(WaitAFrame());

        IEnumerator WaitAFrame()
        {
            yield return (object) new WaitForEndOfFrame();

            ActualBreak(shot.normal * -this.MinimumShotDamage / 20f);
        }
    }

    
    public new void Break(Vector2 velocity = default (Vector2))  
    {
        Debug.Log("Fuck zooi");
    }
    
    public void ActualBreak(Vector2 velocity = default (Vector2))
    {
        if (this.broken || (double) this.OverallChance < (double) UnityEngine.Random.value)
            return;
        this.broken = true;
        GameObject brokenObject = Instantiate(DebrisPrefab, transform.position, transform.rotation);
        brokenObject.SetActive(true);
        this.OnDebrisCreated(brokenObject, velocity);
        this.OnBreak?.Invoke();
    }
}