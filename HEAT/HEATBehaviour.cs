using FunLittleGames.Linkage;

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


[Serializable]
    public class HEATBehaviour : MonoBehaviour, Messages.IUse
    {
        int power = 8;  // сила побочного взрыва снаряда
        
        //public Transform rearAttach;
        //public bool rearOccupied = false;
        public float penetration = 500f;
        [SerializeField]
        public float maxPen = 500f;

        public bool _isTandem = false;
        
        public bool isFlamed = true;

        [SerializeField]
        public Sprite armedS;
        [SerializeField] // хуй тебе, а не сериализация, сказал мне хрюнити
        public Sprite unarmedS;
        
        private bool _triggered = false;
        private float detonationTemp = 600f;
        //private float tailDrag = 0.1f;
        public bool armed = true;
        private SpriteRenderer spriteRenderer;
        private PhysicalProperties physProp;
        private PhysicalBehaviour phys;
        Rigidbody2D rb;

        private float time = 0f;
        private bool isWaiting = false;
        public float timeWaiting = 0.5f;
        
        ActOnShot shot;
        
        HashSet<PhysicalBehaviour> linkedBodies = new HashSet<PhysicalBehaviour>();

        
        bool initialized = false;

        void Awake()
        {
            //Listen(gameObject);
            
            var comps = GetComponents<HEATBehaviour>();
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
            physicalBehaviour.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setPenetration", "Set Penetration", "Sets penetration value in mm", () => {
                DialogBox dialog = (DialogBox)null;
                dialog = DialogBoxManager.TextEntry("Enter new penetration in mm\n<color=orange><size=26>Maximum: +" + maxPen + ",Currently:"+penetration+"</size></color>", "Number", new DialogButton("Apply", true, new UnityAction[1] {
                        (UnityAction)(() => {
                            float setrange;
                            if (float.TryParse(dialog.EnteredText, out setrange)) {
                                penetration = setrange;
                                if (penetration > maxPen) penetration = maxPen;
                            }
                        })
                    }),
                    new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
            }));
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
            
            ModAPI.OnLinkCreated += OnLinkCreatedHandler;
            ModAPI.OnLinkDestroyed += OnLinkKilledHandler;
            
            switch (maxPen)
            {
                case Mod.HEAT1Pen: armedS = Mod.HEATSpriteArmed; 
                    unarmedS = Mod.HEATSpriteUnarmed;
                    break;
                case Mod.HEAT2Pen: armedS = Mod.HEAT1000SpriteArmed;
                    unarmedS = Mod.HEAT1000SpriteUnarmed;
                    break;
                case Mod.HEAT3Pen: armedS = Mod.HEAT2000SpriteArmed;
                    unarmedS = Mod.HEAT2000SpriteUnarmed;
                    break;
                case Mod.HEAT4Pen: armedS = Mod.RPG7SpriteArmed;
                    unarmedS = Mod.RPG7SpriteUnarmed;
                    break;
                case Mod.HEAT5Pen: armedS = Mod.RPG7VRSpriteArmed;
                    unarmedS = Mod.RPG7VRSpriteUnarmed;
                    break;
                case Mod.HEAT6Pen: armedS = Mod.TOWSpriteArmed;
                    unarmedS = Mod.TOWSpriteUnarmed;
                    break;
                case Mod.HEAT7Pen: armedS = Mod.ITOWSpriteArmed;
                    unarmedS = Mod.ITOWSpriteUnarmed;
                    break;
                case Mod.HEAT8Pen: armedS = Mod.PG15VArmed;
                    unarmedS = Mod.PG15VUnarmed;
                    break;
            }
            
            phys = GetComponent<PhysicalBehaviour>();
            phys.ForceContinuous = true;
            phys.Properties = ModAPI.FindPhysicalProperties("Flammable metal");
            
            var exp = GetComponent<ExplosiveBehaviour>();
            if (exp != null) Destroy(exp);

            shot = GetComponent<ActOnShot>();
            shot.Actions.AddListener(Shot);
        }

        void OnLinkCreatedHandler(object? sender, LinkDeviceBehaviour link)
        {
            PhysicalBehaviour A = link.GetComponent<PhysicalBehaviour>();
            PhysicalBehaviour B = link.Other;

            if (A != phys && B != phys) return;

            if (A == phys) linkedBodies.Add(B);
            else if (B == phys) linkedBodies.Add(A);
        }

        void OnLinkKilledHandler(object? sender, LinkDeviceBehaviour link)
        {
            PhysicalBehaviour A = link.GetComponent<PhysicalBehaviour>();
            PhysicalBehaviour B = link.Other;

            if (A != phys && B != phys) return;
        
            if(A == phys) linkedBodies.Remove(B);
            else if (B == phys) linkedBodies.Remove(A);
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
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
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
                TryExplode();
                return;
            }
        }
        
        /*
        public void Listen(GameObject Instance)
        {
            var trigger = Instance.GetComponent<UseEventTrigger>();

            if (trigger == null)
            {
                trigger = Instance.AddComponent<UseEventTrigger>();
            }

            trigger.Action = () =>
            {
                TryExplode();
            };
        }
        */
        
        void TryExplode()
        {
            if (!armed) return;

            InitJet(_isTandem);
            ExplosionCreator.Explode(transform.position, power);
            Destroy(gameObject);
        }

        public void ChangeArmed()
        {
            armed = !armed;
            spriteRenderer.sprite = armed ? armedS : unarmedS;
        }

        LayerMask mask = LayerMask.GetMask("Objects");
        
        
        void FixedUpdate()
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
            
            if (rb.velocity.magnitude > 10f)
            {
                var hit = Physics2D.Raycast(transform.position, rb.velocity.normalized, Time.deltaTime * rb.velocity.magnitude, mask);
                if (hit.collider != null && hit.collider.gameObject != gameObject)
                {
                    var hitRb = hit.collider.GetComponent<Rigidbody2D>();
                    var hitPhys = hit.collider.GetComponent<PhysicalBehaviour>();
                    
                    if(!hitRb || !hitPhys) return;
                    
                    if (Mathf.Abs(hitRb.velocity.magnitude - rb.velocity.magnitude) < 10f) return;
                    
                    if (linkedBodies.Contains(hitPhys)) return;
                    
                    var col = hit.normal;
                    if (rb.velocity.magnitude > 25f)
                    {
                        float angle = Vector2.Angle(transform.right, -col);
                        if (Mathf.Sign(transform.lossyScale.x) == 1)
                        {
                            if (angle > 90f) return;
                        }
                        else if (angle < 90f || angle > 270f) return;
                    }

                    TryExplode();
                }
            }

            if (phys != null && (phys.Temperature > detonationTemp) && isFlamed)
            {
                ExplosionCreator.Explode(transform.position, power);
                Destroy(gameObject);
            }
        }

        void Shot()
        {
            ExplosionCreator.Explode(transform.position, 3f);
            Destroy(gameObject);
        }
        
               // אוי ויי...
        void OnCollisionEnter2D(Collision2D col)
        {
            if (!armed) return;
            if (_triggered) return;

            float force = col.relativeVelocity.magnitude;
            if (force < 5f)
            {
                return; // чувствительность взрывателя (опять)
            }

            if (rb.velocity.magnitude > 15f)
            {

                float angle = Vector2.Angle(transform.right, -col.contacts[0].normal);
                if (Mathf.Sign(transform.lossyScale.x) == 1)
                {
                    if (angle > 90f) return;
                }
                else if (angle < 90f || angle > 270f) return;
            }

            _triggered = true;

            InitJet(_isTandem);
            ExplosionCreator.Explode(transform.position, power);
            Destroy(gameObject);
        }

        void InitJet(bool isTandem)
        {
            if (isTandem)
            {
                CreateJet(transform.position);
                CreateJet(transform.position + (-transform.right * Mathf.Sign(transform.lossyScale.x)));
            } else CreateJet(transform.position);
        }
        
        void CreateJet(Vector3 position)
        {
            var jet = new GameObject("HEAT_Jet");

            float speed = 50f;

            jet.transform.position = position;


            Vector2 forward = transform.right; 
            Vector2 dir = transform.right * Mathf.Sign(transform.lossyScale.x);
            var line = jet.AddComponent<LineRenderer>();

            line.positionCount = 2;
            line.startWidth = 0.03f;
            line.endWidth = 0.01f;

            line.material = ModAPI.FindMaterial("VeryBright");
            line.startColor = new Color(0.8f, 0.6f, 0.2f, 0.5f);
            line.endColor   = new Color(0.6f, 0.2f, 0f, 0f);
            
            line.startWidth = 0.05f;
            line.endWidth = 0.01f;
            
            var damageHandler = jet.AddComponent<JetDamage>();
            damageHandler.Init(penetration, dir);
        }
    } 