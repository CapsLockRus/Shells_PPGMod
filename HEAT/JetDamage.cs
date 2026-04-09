using System.Collections;
using UnityEngine;

namespace Mod
{
    public class JetDamage : MonoBehaviour
    {
        public float penetration = 100f;
        public float origPen = 100f;
        private LineRenderer line;
        private Collider2D lastHit;

        public void Init(float nuPen, Vector2 dir)
        {
            penetration =  nuPen;
            origPen = nuPen;
            direction = dir.normalized;
        }

        Vector2 direction;
        public float speed = 70f;

        void Start()
        {
            line = GetComponent<LineRenderer>();
        }
        
        bool wasInside = false;
        private bool hitMetal = false;
        bool _inside = false;
        LayerMask mask = LayerMask.GetMask("Objects");
        void Update()
        {
            if (Time.timeScale == 0f) return;
            float step = speed * Time.deltaTime;
            float subStep = 0.03f;
            int iterations = Mathf.CeilToInt(step / subStep);
            if (iterations == 1) subStep = step;
            
            for (int i = 0; i < iterations; i++)
            {
                var hit = Physics2D.Raycast(transform.position, direction, subStep,  mask);
                Collider2D colla;
                if (hit.collider == null)
                {
                    Collider2D insideCollider = Physics2D.OverlapPoint(transform.position, mask);
                    colla = insideCollider;
                    _inside = true;
                }
                else
                {
                    colla = hit.collider;
                    _inside = false;
                }
                if (colla == null) _inside = false;
                if (wasInside && !_inside && colla == null && hitMetal)
                {
                    int scale = direction.x > 0 ? 1 : -1;
                    SpawnFragments(
                        (Vector2)transform.position,
                        direction,
                        scale
                    );
                    wasInside = false;
                }
                                                
                if (colla != null)
                {
                    hitMetal = false;
                    
                    float resistance = GetResistance(colla);
                    if (resistance == 0) continue;
                    
                    
                    var phys = colla.GetComponent<PhysicalBehaviour>();
                    var col = colla.GetComponent<Collider2D>();
                    
                    if (!colla.enabled || col == null || !col.enabled) continue;
                    
                    if (phys != null)
                    {
                        var eraBeh = colla.gameObject.GetComponent<ERABehaviour>();
                        if (eraBeh != null)
                        {
                            eraBeh.Explode();
                            penetration -= 3000;
                            continue;
                        }

                        var name = colla.gameObject.name;
                        if (name.Contains("Steel") || name.Contains("Metal") || name.Contains("Beam")) hitMetal = true;
                        
                        if (!_inside)
                        {
                            phys.Shot(new Shot(hit.normal, hit.point, speed * 5f));
                            wasInside = false;
                        }
                        else wasInside = true;

                        penetration -= resistance * subStep * 75f;
                        phys.Ignite(true);
                        phys.Temperature += 10000f * Time.deltaTime;
                        phys.rigidbody.AddForce(direction * (10000f * Time.deltaTime));

                        if (penetration <= 0f)
                        {
                            Destroy(gameObject);
                            return;
                        }

                    }
                } else
                {
                    penetration -= 100f * Time.deltaTime;
                    if (penetration <= 0f)
                    {
                        Destroy(gameObject);
                        return;
                    }
                }
                transform.position += (Vector3)(direction * subStep);
            }

            line.startColor = new Color(0.8f, 0.6f, 0.2f, 0.5f * (penetration/origPen));
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position - (Vector3)(direction * (1.5f * (penetration/origPen))));
        }

        void SpawnFragments(Vector2 position, Vector2 direction,
            int scale, int count = 6, float spreadAngle = 30f)
        {
            for (int i = 0; i < count; i++)
            {
                float angleOffset = UnityEngine.Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * direction;
                SpawnFragmentos(position, dir,scale);
            }
        }

        private void SpawnFragmentos(Vector2 position, Vector2 dir, int scale)
        {

            var obj = ModAPI.FindSpawnable("Knife");
            

            var fragment = Instantiate(obj.Prefab.gameObject, position + dir * -0.05f, Quaternion.identity);
            fragment.name = "Fragment {NON-INTER}";
            
            var rb = fragment.GetComponent<Rigidbody2D>();
            if (rb == null) rb = fragment.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0.3f;
            float speeed = UnityEngine.Random.Range(5f, 10f);
            rb.velocity = dir.normalized * speeed;
            
            if (scale == -1)  fragment.transform.rotation = Quaternion.Euler(0, 0, 90);
            else fragment.transform.rotation = Quaternion.Euler(0, 0, -90);

            

            var col = fragment.GetComponent<Collider2D>();
            col.isTrigger = false;
            col.sharedMaterial = new PhysicsMaterial2D { bounciness = 1f, friction = 0.2f };
            var sp = fragment.GetComponent<SpriteRenderer>();
            sp.sprite = Mod.Picrel;
            fragment.FixColliders();
            var phys = fragment.GetComponent<PhysicalBehaviour>();
            rb.freezeRotation = true;
            phys.ForceContinuous = true;
            phys.Temperature = 300f;
            phys.TrueInitialMass = 0.01f;
            phys.InitialMass = 0.01f;
            phys.DisplayBloodDecals = false;
            phys.HasOutline = false;
            phys.StabCausesWound = true;
            phys.SpawnSpawnParticles = false;
            phys.Selectable = false;
            rb.mass = 0.01f;
            //prop.Sharp = true;
            var beh = fragment.AddComponent<FragmentRay>();
            sp.material = ModAPI.FindMaterial("VeryBright");
            sp.color = new Color(1f, 1f, 1f, 0.2f);

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
                    new Color(1f, 1f, 0f, 0.25f);

                lineF.endColor =
                    new Color(1f, 0.3f, 0f, 0f);
            }
            private void Update()
            {
                timed +=  Time.deltaTime;
                if (phys.Temperature < 100 || timed > 3f)
                {
                    Destroy(gameObject);
                    return;
                }
                
                while (rb.velocity.magnitude > 10f) rb.velocity *= 0.5f;
                multiplier = (phys.Temperature / 250) * (rb.velocity.magnitude / 10);
                if (multiplier > 1f) multiplier = 1f;
                Vector3 backOffset = - (Vector3)rb.velocity.normalized * (0.3f * multiplier); // 0.5f = длина линии
                
                lineF.SetPosition(
                    0,
                    transform.position);
                lineF.SetPosition(1, transform.position + backOffset);

            }
        }

        float GetResistance(Collider2D col)
        {
            var name = col.gameObject.name;
            float res;

            if (name.Contains("Steel") || name.Contains("Metal") || name.Contains("Beam"))
                res = 10f;
            else if (name.Contains("Insulator"))
                res = 25f;
            else if (name.Contains("Brick") || name.Contains("Cobblestone"))
                res = 15f;
            else if (name.Contains("Wall") || name.Contains("Root") || name.Contains("Ceiling"))
                res = 99999999f;
            else if (name.Contains("{NON-INTER}"))
                res = 0f;
            else res = 5f;

            return res;
        }
    }
}
