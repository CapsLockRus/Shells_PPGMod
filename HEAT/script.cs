using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;


//please don't read this code, it really sucks

namespace Mod
{

    public class Mod
    {
        public const float HEAT1Pen = 1200f;
        public const float HEAT2Pen = 2600f;
        public const float HEAT3Pen = 5000f;
        public const float HEAT4Pen = 1600f;
        public const float HEAT5Pen = 3200f;
        public const float HEAT6Pen = 2400f;
        public const float HEAT7Pen = 4000f;
        public const float HEAT8Pen = 2000f;

        public static Sprite HEATSpriteUnarmed;
        public static Sprite HEATSpriteArmed;
        public static Sprite RPG7SpriteUnarmed;
        public static Sprite RPG7SpriteArmed;
        public static Sprite HEAT1000SpriteUnarmed;
        public static Sprite HEAT1000SpriteArmed;
        public static Sprite HEAT2000SpriteArmed;
        public static Sprite HEAT2000SpriteUnarmed;
        public static Sprite RPG7VRSpriteArmed;
        public static Sprite RPG7VRSpriteUnarmed;
        public static Sprite TOWSpriteArmed;
        public static Sprite TOWSpriteUnarmed;
        public static Sprite ITOWSpriteArmed;
        public static Sprite ITOWSpriteUnarmed;
        public static Sprite RPG7OSpriteArmed;
        public static Sprite RPG7OSpriteUnarmed;
        public static Sprite HESpriteArmed;
        public static Sprite HESpriteUnarmed;
        public static Sprite HESHSpriteArmed;
        public static Sprite HESHSpriteUnarmed;
        public static Sprite TBG7SpriteArmed;
        public static Sprite TBG7SpriteUnarmed;
        public static Sprite BushmasterI;
        public static Sprite ERASprite;
        public static Sprite HE25mm;
        public static Sprite AP25mm;
        public static Sprite PG15VArmed;
        public static Sprite PG15VUnarmed;
        public static Sprite Mortar80mmArmed;
        public static Sprite Mortar80mmUnarmed;
        public static Sprite Howitzer155mmArmed;
        public static Sprite Howitzer155mmUnarmed;
        public static Sprite M777ExcaliburFins;
        public static Sprite M777ExcaliburThumb;
        public static Sprite RefConcrete;
        public static Sprite RefConcreteP1;
        public static Sprite RefConcreteP2;
        public static Sprite RefConcreteP3;
        public static Sprite RefConcreteP4;
        public static AudioClip BushmasterIAudio;

        //public static Sprite JetSprite;
        public static Sprite ChargeSprite;
        public static Sprite ChargeSpriteArrow;
        public static Sprite RPGChargeSprite;
        public static Sprite RPGChargeSprite1;
        public static Sprite RPGChargeSpriteBurnt;
        public static Sprite Picrel;
        public static Sprite PicrelGrey;
        public static Sprite PicrelAP;

        public static string ModTag = "[Shells++]";

        public static void OnLoad()
        {
            HEATSpriteArmed = ModAPI.LoadSprite("sprites/HEAT.png");
            HEATSpriteUnarmed = ModAPI.LoadSprite("sprites/HEAT_unarmed.png");
            RPG7SpriteArmed = ModAPI.LoadSprite("sprites/RPG-7_armed.png");
            RPG7SpriteUnarmed = ModAPI.LoadSprite("sprites/RPG-7_unarmed.png");
            HEAT1000SpriteArmed = ModAPI.LoadSprite("sprites/HEAT_1000_armed.png");
            HEAT1000SpriteUnarmed = ModAPI.LoadSprite("sprites/HEAT_1000_unarmed.png");
            HEAT2000SpriteArmed = ModAPI.LoadSprite("sprites/HEAT_2000_armed.png");
            HEAT2000SpriteUnarmed = ModAPI.LoadSprite("sprites/HEAT_2000_unarmed.png");
            RPG7VRSpriteArmed = ModAPI.LoadSprite("sprites/RPG-7VR_armed.png");
            RPG7VRSpriteUnarmed = ModAPI.LoadSprite("sprites/RPG-7VR_unarmed.png");
            TOWSpriteArmed = ModAPI.LoadSprite("sprites/TOW_armed.png");
            TOWSpriteUnarmed = ModAPI.LoadSprite("sprites/TOW_unarmed.png");
            ITOWSpriteArmed = ModAPI.LoadSprite("sprites/ITOW_armed.png");
            ITOWSpriteUnarmed = ModAPI.LoadSprite("sprites/ITOW_unarmed.png");
            RPG7OSpriteArmed = ModAPI.LoadSprite("sprites/RPG-7O_armed.png");
            RPG7OSpriteUnarmed = ModAPI.LoadSprite("sprites/RPG-7O_unarmed.png");
            TBG7SpriteArmed = ModAPI.LoadSprite("sprites/TBG-7_armed.png");
            TBG7SpriteUnarmed = ModAPI.LoadSprite("sprites/TBG-7_unarmed.png");
            HESpriteArmed = ModAPI.LoadSprite("sprites/HE_armed.png");
            HESpriteUnarmed = ModAPI.LoadSprite("sprites/HE_unarmed.png");
            HESHSpriteArmed = ModAPI.LoadSprite("sprites/HESH_armed.png");
            HESHSpriteUnarmed = ModAPI.LoadSprite("sprites/HESH_unarmed.png");
            //JetSprite = ModAPI.LoadSprite("sprites/HEAT_jet.png");
            ChargeSpriteArrow = ModAPI.LoadSprite("sprites/Charge_arrow.png");
            ChargeSprite = ModAPI.LoadSprite("sprites/Charge_noarrow.png");
            ERASprite = ModAPI.LoadSprite("sprites/ERA.png");
            RPGChargeSprite = ModAPI.LoadSprite("sprites/RPG_charge.png");
            RPGChargeSpriteBurnt = ModAPI.LoadSprite("sprites/RPG_charge_burnt.png");
            Picrel = ModAPI.LoadSprite("sprites/picrel.png", 2f);
            PicrelGrey = ModAPI.LoadSprite("sprites/picrel_grey.png");
            BushmasterI = ModAPI.LoadSprite("sprites/BushmasterI.png");
            HE25mm = ModAPI.LoadSprite("sprites/25mm.png", 2f);
            AP25mm = ModAPI.LoadSprite("sprites/25mmAP.png", 2f);
            BushmasterIAudio = ModAPI.LoadSound("audio/Bushmaster.wav");
            PicrelAP = ModAPI.LoadSprite("sprites/25mmAP.png", 5f);
            PG15VArmed = ModAPI.LoadSprite("sprites/PG-15V_armed.png");
            PG15VUnarmed = ModAPI.LoadSprite("sprites/PG-15V_unarmed.png");
            Mortar80mmArmed = ModAPI.LoadSprite("sprites/80mm_mortar_armed.png");
            Mortar80mmUnarmed = ModAPI.LoadSprite("sprites/80mm_mortar_unarmed.png");
            Howitzer155mmArmed = ModAPI.LoadSprite("sprites/M777_HE_armed.png");
            Howitzer155mmUnarmed = ModAPI.LoadSprite("sprites/M777_HE_unarmed.png");
            M777ExcaliburFins = ModAPI.LoadSprite("sprites/M777_Excalibur_fins.png");
            M777ExcaliburThumb = ModAPI.LoadSprite("sprites/M777_Excalibur_thumb.png");
            RPGChargeSprite1 = ModAPI.LoadSprite("sprites/RPG_charge_1.png");
            RefConcrete = ModAPI.LoadSprite("sprites/reinforced_concrete.png");
            RefConcreteP1 = ModAPI.LoadSprite("sprites/reinforced_concrete_P1.png");
            RefConcreteP2 = ModAPI.LoadSprite("sprites/reinforced_concrete_P2.png");
            RefConcreteP3 = ModAPI.LoadSprite("sprites/reinforced_concrete_P3.png");
            RefConcreteP4 = ModAPI.LoadSprite("sprites/reinforced_concrete_P4.png");
        }

        public static void OnUnload()
        {
        }

        public static void Main()
        {
            var thumbSprite = ModAPI.LoadSprite("thumb.png");
            ModAPI.RegisterCategory("Shells++", "Different types of tank munitions", thumbSprite);



            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HEAT <{HEAT1Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = HEATSpriteArmed,



                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = HEATSpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.5f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.3f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT1Pen;
                        behaviour.penetration = HEAT1Pen;

                        behaviour.armedS = HEATSpriteArmed;
                        behaviour.unarmedS = HEATSpriteUnarmed;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HEAT <{HEAT2Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = HEAT1000SpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = HEAT1000SpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 1f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.3f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT2Pen;
                        behaviour.penetration = HEAT2Pen;

                        behaviour.armedS = HEAT1000SpriteArmed;
                        behaviour.unarmedS = HEAT1000SpriteUnarmed;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HEAT <{HEAT3Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = HEAT2000SpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = HEAT2000SpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 1.2f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.1f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT3Pen;
                        behaviour.penetration = HEAT3Pen;

                        behaviour.armedS = HEAT2000SpriteArmed;
                        behaviour.unarmedS = HEAT2000SpriteUnarmed;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} PG-7 Grenade <{HEAT4Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = RPG7SpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = RPG7SpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.5f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.3f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT4Pen;
                        behaviour.penetration = HEAT4Pen;

                        behaviour.armedS = RPG7SpriteArmed;
                        behaviour.unarmedS = RPG7SpriteUnarmed;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });

            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} PG-7VR Tandem Grenade <{HEAT5Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "Tandem HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = RPG7VRSpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = RPG7VRSpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 1f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.4f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT5Pen;
                        behaviour.penetration = HEAT5Pen;

                        behaviour.armedS = RPG7VRSpriteArmed;
                        behaviour.unarmedS = RPG7VRSpriteUnarmed;

                        behaviour._isTandem = true;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} TOW HEAT Warhead <{HEAT6Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = TOWSpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = TOWSpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 1f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.24f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT6Pen;
                        behaviour.penetration = HEAT6Pen;

                        behaviour.armedS = TOWSpriteArmed;
                        behaviour.unarmedS = TOWSpriteUnarmed;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} ITOW Tandem HEAT Warhead <{HEAT7Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "Tandem HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = ITOWSpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ITOWSpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 1.3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.34f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT7Pen;
                        behaviour.penetration = HEAT7Pen;

                        behaviour.armedS = ITOWSpriteArmed;
                        behaviour.unarmedS = ITOWSpriteUnarmed;

                        behaviour._isTandem = true;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} PG-15V HEAT Warhead <{HEAT8Pen}mm",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HEAT warhead with changeable via context menu penetration\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = PG15VArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = PG15VUnarmed;
                        var behaviour = Instance.AddComponent<HEATBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.8f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.46f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.maxPen = HEAT8Pen;
                        behaviour.penetration = HEAT8Pen;

                        behaviour.armedS = PG15VArmed;
                        behaviour.unarmedS = PG15VUnarmed;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Metal Pole"),
                    NameOverride = $"{ModTag} Explosive Charge",
                    DescriptionOverride =
                        "Directional propellant charge for tank shells \nForce can be changed via context menu",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = ChargeSprite,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ChargeSpriteArrow;
                        Instance.FixColliders();
                        var behaviour =
                            Instance.AddComponent<ChargeBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 5f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var front = new GameObject("FrontAttach");

                        front.transform.SetParent(Instance.transform);

                        front.transform.localPosition =
                            new Vector3(0.4f, 0f, 0f);

                        behaviour.frontAttach = front.transform;
                        

                    }
                }
            );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Metal Cube"),
                    NameOverride = $"{ModTag} ERA",
                    DescriptionOverride =
                        "Explosive Reactive Armor destroys HEAT jet by exploding when hit by HEAT munition\nCan be penetrated using tandem charges",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = ERASprite,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ERASprite;
                        Instance.FixColliders();
                        var behaviour =
                            Instance.AddComponent<ERABehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                    }
                }
            );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Mini Thruster"),
                    NameOverride = $"{ModTag} RPG Rocket Booster",
                    DescriptionOverride = "Single-use rocket fuel booster\nThrust and burn time can be adjusted via context menu",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = RPGChargeSprite,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = RPGChargeSprite;
                        foreach (var c in Instance.GetComponents<Collider2D>())
                        {
                            GameObject.Destroy(c);
                        }
                        Instance.FixColliders();
                        var behaviour =
                            Instance.AddComponent<RPGChargeBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var front = new GameObject("FrontAttach");

                        front.transform.SetParent(Instance.transform);

                        front.transform.localPosition =
                            new Vector3(0.83f, 0f, 0f);

                        behaviour.frontAttach = front.transform;

                        var air = Instance.AddComponent<AirfoilBehaviour>();
                    }
                }
            );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Metal Pole"),
                    NameOverride = $"{ModTag} OG-7V Fragmentation Grenade",
                    DescriptionOverride =
                        "HE-Frag warhead designed for anti-personnel use. Most effective against enemies in open spaces\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = RPG7OSpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = RPG7OSpriteUnarmed;
                        Instance.FixColliders();
                        var stab = Instance.AddComponent<PointToVelocityBehaviour>();
                        stab.intensity = 0.001f;
                        var behaviour = Instance.AddComponent<HEBehaviour>();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.42f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.type = 0;
                        behaviour.fragCount = 20;
                        behaviour.force = 3f;
                    }
                }
                );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HE Shell",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HE-Frag shell designed for anti-personnel use. Produces a very strong blast and generates a ton of deadly shrapnel\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = HESpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = HESpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.25f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.type = 1;
                        behaviour.fragCount = 36;
                        behaviour.force = 30f;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                    });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HEP Shell",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HE Plasticized shell designed for anti-entrenchment use. Produces a very strong blast but doesnt produce much shrapnel\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = HESHSpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = HESHSpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 5f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.2f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.type = 3;
                        behaviour.fragCount = 0;
                        behaviour.force = 0f;
                        var expOld = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(expOld);
                        var exp = Instance.AddComponent<ExplosiveBehaviour>();
                        var shit = Instance.GetComponentInChildren<ActOnShot>();
                        GameObject.Destroy(shit);

                        exp.BallisticShrapnelCount = 0;
                        exp.BigExplosion = false;
                        exp.DismemberChance = 0.5f;
                        exp.ShockwaveStrength = 50f;
                        exp.ShouldCreateKillzone = false;
                        exp.ArmOnAwake = false;
                        exp.ExplodesOnFragmentHit = false;
                        exp.ArmOnUse = false;
                        exp.enabled = false;
                        exp.Delay = 0;
                        exp.FragmentForce = 30f;
                        exp.FragmentationRayCount = 32;
                        exp.DestroyOnExplode = true;
                        
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                    });
            /*
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HE Shell",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HE-Frag shell designed for anti-personnel use. Produces a very strong blast and generates a ton of deadly shrapnel\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = HESpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = HESpriteUnarmed;
                        var behaviour = Instance.AddComponent<HEBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.25f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.type = 1;
                        behaviour.fragCount = 36;
                        behaviour.force = 30f;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                    }
                    });
                    */
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HE Mortar Shell",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HE-Frag shell designed for indirect fire. Produces lots of shrapnel. Supports airburst\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = Mortar80mmArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = Mortar80mmUnarmed;
                        var behaviour = Instance.AddComponent<HEBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 1.5f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);
                        rear.transform.localPosition =
                            new Vector3(-0.2f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.type = 4;
                        behaviour.fragCount = 24;
                        behaviour.force = 12f;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                        behaviour._isAirbust = true;
                        behaviour.maxAirbustHeight = 10f;
                        behaviour.randomSpread = 0.2f;
                    }
                    });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("General Purpose Bomb"),
                    NameOverride = $"{ModTag} HE Howitzer Shell",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HE-Frag shell designed for indirect fire. Produces a very strong blast and lots of shrapnel. Supports airburst\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = Howitzer155mmArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = Howitzer155mmUnarmed;
                        var behaviour = Instance.AddComponent<HEBehaviour>();
                        Instance.FixColliders();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 5f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.6f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.type = 5;
                        behaviour.fragCount = 40;
                        behaviour.force = 36f;
                        
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                        behaviour._isAirbust = true;
                        behaviour.maxAirbustHeight = 20f;
                        behaviour.randomSpread = 0.3f;
                    }
                    });
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Metal Pole"),
                    NameOverride = $"{ModTag} TBG-7V Thermobaric Grenade",
                    DescriptionOverride =
                        "Thermobaric warhead designed for anti-personnel use. Most effective against enemies inside buildings\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = TBG7SpriteArmed,

                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = TBG7SpriteUnarmed;
                        Instance.FixColliders();
                        var stab = Instance.AddComponent<PointToVelocityBehaviour>();
                        stab.intensity = 0.001f;
                        var behaviour = Instance.AddComponent<ThermoBehaviour>();
                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 1.2f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition =
                            new Vector3(-0.37f, 0f, 0f);

                        connect.rearAttach = rear.transform;
                        behaviour.type = 0;
                        behaviour.fragCount = 0;
                        behaviour.force = 0f;
                        
                        var exp = Instance.AddComponent<ExplosiveBehaviour>();
                        exp.ShockwaveStrength = 35f;
                        exp.ShockwaveLiftForce = 20f;
                        exp.DismemberChance = 0.8f;
                        //exp.ArmOnAwake = true;
                        exp.ArmOnUse = false;
                        exp.TemperatureLimit = 9999999;
                        exp.BallisticShrapnelCount = 0;
                        exp.ImpactForceThreshold = -1;
                        exp.DestroyOnExplode = true;
                        exp.SetRange(10);
                        exp.BigExplosion = true;
                        exp.BurnPower = 6;
                        exp.ExplodesOnFragmentHit = false;
                        exp.Delay = 0;
                        
                        behaviour._isCustom = true;
                    }
                }
                ); 
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Handgrenade"),
                    NameOverride = "HE_shell_25mm_bushmaster",
                    NameToOrderByOverride = "іі",
                    DescriptionOverride = "",
                    CategoryOverride = ModAPI.FindCategory("non-existing category oojojjyhthtyj"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = HE25mm;
                        Instance.FixColliders();
                        
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        var behaviour = Instance.AddComponent<HEBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        behaviour.type = 2;
                        behaviour.fragCount = 0;
                        behaviour.force = 0.5f;
                        var exp = Instance.GetComponent<ExplosiveBehaviour>();
                        exp.BallisticShrapnelCount = 8;
                        exp.FragmentationRayCount = 16;
                        exp.FragmentForce = 1f;
                        exp.Range = 5;
                        exp.Delay = 0;
                        exp.DestroyOnExplode = true;
                        exp.ParticleEffectAndSound = true;
                        exp.ShockwaveStrength = 1f;
                        exp.ShockwaveLiftForce = 1f;

                        Instance.AddComponent<ActOnShot>();
                        
                        var pointer = Instance.AddComponent<PointToVelocityBehaviour>();
                        pointer.intensity = 0.001f;
                        behaviour.alwaysArmed = true;
                        Instance.GetComponent<PhysicalBehaviour>().ForceContinuous = true;
                        
                        var comps = Instance.GetComponents<HEBehaviour>();
                        if (comps.Length > 1)
                        {
                            GameObject.Destroy(Instance.GetComponent<HEBehaviour>());
                            return;
                        }
                    }
                }
            );
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Detached 30mm HEAT Cannon"),
                    NameOverride = "AP_shell_25mm_bushmaster",
                    DescriptionOverride = "",
                    CategoryOverride = ModAPI.FindCategory("non-existing category oojojjyhthtyj"),
                    ThumbnailOverride = AP25mm,
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 0.3f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<SpriteRenderer>().sprite = AP25mm;
                        
                        Instance.AddComponent<PseudoAPBehaviour>();
                        
                        var phys = Instance.GetComponent<PhysicalBehaviour>();

                        phys.ForceContinuous = true;
                        
                        var APLauncher = Instance.GetComponent<MachineGunBehaviour>();
                        //APLauncher.Effect = null;
                        Cartridge customCartridge = ModAPI.FindCartridge("120 mm");
                        customCartridge.name = "25mm AP Bushmaster";
                        customCartridge.Damage *= 1f;
                        customCartridge.StartSpeed = 200f;
                        customCartridge.PenetrationRandomAngleMultiplier *= 1f;
                        customCartridge.Recoil *= 0f;
                        customCartridge.ImpactForce *= 0.03f;
                        APLauncher.Cartridge = customCartridge;
                        APLauncher.ExplosiveRounds = false;
                        APLauncher.Automatic = false;
                        APLauncher.ShockwaveIntensity = 0f;
                        phys.Selectable = false;
                        phys.Deletable = false;
                        
                        var audio = Instance.GetComponent<AudioSource>();
                        GameObject.Destroy(audio);
                    }
                }
            );
                            /*
                            foreach(var c in Instance.GetComponents<Component>())
                               {
                                   Debug.Log("compo: " + c);
                               }
                               foreach(var c in Instance.GetComponentsInChildren<Component>())
                               {
                                   Debug.Log("kid compo: " + c);
                               }
                               foreach(var c in Instance.GetComponentsInParent<Component>())
                               {
                                   Debug.Log("par compo: " + c);
                               }
                            */
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Stone Brick Wall"),
                    NameOverride = $"{ModTag} Reinforced Concrete Slab",
                    NameToOrderByOverride = "іі",
                    DescriptionOverride = "Concrete reinforced with steel, can withstand much more than any other building material",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = RefConcrete,
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = RefConcrete;
                        Instance.FixColliders();
                        Instance.AddComponent<ReinforcedConcreteThing>();
                    }
                }
            );
            ModAPI.Register(
				new Modification()
				{
                    OriginalItem = ModAPI.FindSpawnable("Holographic Display"),
                    NameOverride = $"{ModTag} GPS Beacon",
                    NameToOrderByOverride = "іі",
                    DescriptionOverride = "Displays it's current coordinates so you know where exactly to send your GPS-Guided munitions",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.AddComponent<GPSBeaconBehaviour>();
                    }
                }
            );
            ModAPI.Register(
				new Modification()
				{
                    OriginalItem = ModAPI.FindSpawnable("Detached 30mm HEAT Cannon"),
                    NameOverride = "Bushmaster I",
                    NameToOrderByOverride = "іі",
                    DescriptionOverride = "25mm autocannon with changeable via context menu ammo type (HE/AP) and adjustable RPM",
                    CategoryOverride = ModAPI.FindCategory("non-existing category"),
                    ThumbnailOverride = BushmasterI,
                    AfterSpawn = (Instance) =>
                    {
                        //var machine = Instance.GetComponent<MachineGunBehaviour>();
                        
                        Instance.GetComponent<SpriteRenderer>().sprite = BushmasterI;
                        Instance.FixColliders();
                        
                        var launcher = Instance.GetComponent<ProjectileLauncherBehaviour>();

                        if (launcher == null)
                        {
                            launcher = Instance.AddComponent<ProjectileLauncherBehaviour>();
                        }
                        
                        launcher.projectileAsset = ModAPI.FindSpawnable("HE_shell_25mm_bushmaster");
                        var phys = Instance.GetComponent<PhysicalBehaviour>();

                        launcher.IsAutomatic = true;
                        launcher.AutomaticInterval = 0.5f;
                        launcher.RemoveLaunchedObjectsWithMe = true;
                        launcher.ScreenShake = 0.5f;
                        launcher.recoilMultiplier = 0.05f;
                        launcher.projectileLaunchStrength = 100f;
                        launcher.barrelDirection = Instance.transform.right;
                        launcher.barrelPosition = new Vector2(1.63f, 0.0f);
                        
                        launcher.launchSound = BushmasterIAudio;
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 15f;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass = 15f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass = 5f;
                        
                        var behaviour = Instance.AddComponent<AutocannonBehaviour>();

                        foreach (var mg in Instance.GetComponents<MachineGunBehaviour>())
                        {
                            GameObject.DestroyImmediate(mg);
                        }
                    }
                }
            );
            ModAPI.Register(
				new Modification()
				{
                    OriginalItem = ModAPI.FindSpawnable("Detached 30mm HEAT Cannon"),
                    NameOverride = $"{ModTag} Bushmaster I",
                    NameToOrderByOverride = "іі",
                    DescriptionOverride = "25mm autocannon with changeable via context menu ammo type (HE/AP) and adjustable RPM",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = BushmasterI,
                    AfterSpawn = (Instance) =>
                    {
                        //var machine = Instance.GetComponent<MachineGunBehaviour>();
                        
                        Instance.GetComponent<SpriteRenderer>().sprite = BushmasterI;
                        Instance.FixColliders();
                        
                        var launcher = Instance.GetComponent<ProjectileLauncherBehaviour>();

                        if (launcher == null)
                        {
                            launcher = Instance.AddComponent<ProjectileLauncherBehaviour>();
                        }
                        
                        
                        
                        launcher.projectileAsset = ModAPI.FindSpawnable("HE_shell_25mm_bushmaster");
                        var phys = Instance.GetComponent<PhysicalBehaviour>();

                        launcher.IsAutomatic = true;
                        launcher.AutomaticInterval = 0.5f;
                        launcher.RemoveLaunchedObjectsWithMe = true;
                        launcher.ScreenShake = 0.5f;
                        launcher.recoilMultiplier = 0.05f;
                        launcher.projectileLaunchStrength = 100f;
                        launcher.barrelDirection = Vector2.right;
                        launcher.barrelPosition = new Vector2(1.63f, 0.0f);
                        
                        launcher.launchSound = BushmasterIAudio;
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 15f;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass = 15f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass = 5f;
                        
                        var firesystem = Instance.GetComponentsInChildren<ParticleSystem>().FirstOrDefault(p => p.gameObject.name == "Tank muzzle flash Variant");
                        firesystem.transform.localPosition = new Vector3(1.6f, 0, 0);
                        
                        var behaviour = Instance.AddComponent<AutocannonBehaviour>();

                        foreach (var mg in Instance.GetComponents<MachineGunBehaviour>())
                        {
                            GameObject.DestroyImmediate(mg);
                        }
                    }
                }
            );
             ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Wing"),
                    NameOverride = $"{ModTag} GPS-Guided Howitzer Shell",
                    NameToOrderByOverride = "Z",
                    DescriptionOverride =
                        "HE-Frag shell designed for accurate indirect fire. Produces a very strong blast and lots of shrapnel. Supports airburst\n" +
                        "Input absolute coordinates via context menu for GPS guidance\nArmed via context menu or red signal",
                    CategoryOverride = ModAPI.FindCategory("Shells++"),
                    ThumbnailOverride = M777ExcaliburThumb,

                    AfterSpawn = (Instance) =>
                    {
                        var sr = Instance.GetComponent<SpriteRenderer>();
                        sr.sprite = Howitzer155mmUnarmed;
                        var behaviour = Instance.AddComponent<HEBehaviour>();


                        var connect = Instance.AddComponent<ConnectBehaviour>();
                        Instance.GetComponent<PhysicalBehaviour>().InitialMass = 5f;
                        Instance.GetComponent<PhysicalBehaviour>().rigidbody.mass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;
                        Instance.GetComponent<PhysicalBehaviour>().TrueInitialMass =
                            Instance.GetComponent<PhysicalBehaviour>().InitialMass;

                        var air = Instance.GetComponent<AirfoilBehaviour>();

                        var wing = air.CurrentWingType;
                        wing.DragMultiplier = 4f;
                        wing.LiftMultiplier = 0f;
                        wing.Lifts = false;
                        wing.Sprite = Instance.GetComponent<SpriteRenderer>().sprite;
                        
                        
                        
                        GameObject.Destroy(air);
                        
                        air = Instance.AddComponent<AirfoilBehaviour>();
                        
                        air.CurrentWingType =  wing;
                        
                        air.PhysicalBehaviour = Instance.GetComponent<PhysicalBehaviour>();
                        air.SpriteRenderer =  Instance.GetComponent<SpriteRenderer>();
                        Instance.FixColliders();
                        air.enabled = true;           

                        var rear = new GameObject("RearAttach");

                        rear.transform.SetParent(Instance.transform);

                        rear.transform.localPosition = new Vector3(-0.6f, 0f, 0f);

                        
                        behaviour.type = 5;
                        behaviour.fragCount = 40;
                        behaviour.force = 36f;
                        
                        //ar exp = Instance.GetComponent<ExplosiveBehaviour>();
                        //GameObject.Destroy(exp);
                        var shot = Instance.AddComponent<ActOnShot>();
                        behaviour._isAirbust = true;
                        behaviour.maxAirbustHeight = 20f;
                        behaviour.randomSpread = 0.3f;

                        //Instance.AddComponent<PointToVelocityBehaviour>();
                        
                        if (Mathf.Sign(Instance.transform.lossyScale.x) == -1)
                        {
                            Instance.transform.localScale = new Vector3(-Instance.transform.localScale.x, Instance.transform.localScale.y, Instance.transform.localScale.z);
                        }
                        
                        connect.rearAttach = rear.transform;
                        
                        var fins = new GameObject("Decorative fins");
                        fins.transform.SetParent(Instance.transform);
                        fins.transform.localPosition = new Vector3(0,0,0); 
                        /*
                        if (Mathf.Sign(Instance.transform.lossyScale.x) == -1)
                        {
                            fins.transform.localScale = new Vector3(-fins.transform.localScale.x, fins.transform.localScale.y, fins.transform.localScale.z);
                        }*/
                        fins.transform.rotation = Instance.transform.rotation;
                        var finsSr = fins.AddComponent<SpriteRenderer>();
                        finsSr.sprite = M777ExcaliburFins;
                        finsSr.sortingOrder = sr.sortingOrder + 1;

                        var pointer = Instance.AddComponent<PointAtBehaviour>();
                        var gps = Instance.AddComponent<GPSBehaviour>();
                    }
                 });
        }
    }
}
