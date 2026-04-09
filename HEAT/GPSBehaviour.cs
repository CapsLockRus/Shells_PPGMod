namespace Mod;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class GPSBehaviour : MonoBehaviour
{
    private AirfoilBehaviour air;
    private PointAtBehaviour pointer;
    private Rigidbody2D rb;
    private HEBehaviour he;
    private PointToVelocityBehaviour poin;
    private PhysicalBehaviour physicalBehaviour;
    
    private bool isGuiding = false;
    private GameObject target;
    public bool autoGuide = false;
    
    public Vector2 gpsTarget =  new Vector2(50, 0);

    private void Awake()
    {
        var comps = GetComponents<GPSBehaviour>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        air = GetComponent<AirfoilBehaviour>();
        pointer = GetComponent<PointAtBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        he =  GetComponent<HEBehaviour>();
        poin =   GetComponent<PointToVelocityBehaviour>();

        
        
        target = new GameObject("Target Marker");
        target.transform.position = gpsTarget;
        pointer.Target = target.transform;
        pointer.Force = rb.velocity.magnitude * 0.02f;
        pointer.Rigidbody = rb;
        
        physicalBehaviour = GetComponent<PhysicalBehaviour>();
        physicalBehaviour.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setCoordinate", "Set GPS Target", "Set coordinates of the target", () => {
            DialogBox dialog = (DialogBox)null;
            dialog = DialogBoxManager.TextEntry($"Enter new target coordinates as\nX;Y\n<color=orange><size=26>Current:\n{gpsTarget.x};{gpsTarget.y}</size></color>", "example: 155;1", new DialogButton("Apply", true, new UnityAction[1] {
                    (UnityAction)(() =>
                    {
                        string text = dialog.EnteredText;
                        string[] parts = text.Split(';');

                        if (parts.Length == 2)
                        {
                            float x, y;

                            if (float.TryParse(parts[0], out x) &&
                                float.TryParse(parts[1], out y))
                            {
                                if (y < 0) y = 0;
                                gpsTarget = new Vector2(x, y);
                            } else Fail(dialog);
                        } else Fail(dialog);
                    })
                }),
                new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
        }));
        physicalBehaviour.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setAutoGuidance", "Set Auto-enabling guidance on apogee", "Switch Auto-enabling guidance on apogee", () => {
            DialogBox dialog = (DialogBox)null;
            dialog = DialogBoxManager.TextEntry("Enable/disable auto-enabling guidance when shell passes apogee\n<color=blue><size=20>If enabled, when the shell gets armed, the apogee check will start to run, if vertical velocity reaches near-zero, \nguidance will be automatically enabled. It will remain disabled until both conditions will be met\nCurrently: " + autoGuide + "</size></color>", "placeholder field so i could use this preset to explain the function", new DialogButton("Enable", true, new UnityAction[1] {
                    (UnityAction)(() =>
                    {
                        if(he.armed) he.ChangeArmed();
                        isGuiding = false;
                        autoGuide = true;
                    })
                }),
                new DialogButton("Disable", true, (UnityAction)(() =>
                {
                    autoGuide = false;
                    isGuiding = false;
                })));
        }));
    }
    
    void Fail(DialogBox oldDialog)
    {
        oldDialog.Close();
        DialogBox nuDialog = (DialogBox)null;
        nuDialog = DialogBoxManager.TextEntry(
            "<color=red><size=26>you did NOT follow the example, did you?</size></color>",
            "v press the button and try again v",

            new DialogButton(
                "OK",
                true,
                new UnityAction[]
                {
                    () => {}
                }
            )
        );
    }
    
    private Vector2 position;
    private float deltaX;
    private float deltaY;
    private float speedX;
    private float speedY;
    private float travelTime;
    private float predictedX;
    void FixedUpdate()
    {
        
        if (!he.armed)
        {
            pointer.enabled = false;
            return;
        }

        if (autoGuide && !isGuiding)
        {
            if (rb.velocity.y < 0f) isGuiding = true;
            else return;
        }
        
        pointer.enabled = true;
        pointer.Force = rb.velocity.magnitude * 0.02f;

        position = transform.position;
        
        deltaX = gpsTarget.x - position.x;
        deltaY = gpsTarget.y - position.y;
        speedX = rb.velocity.x;
        speedY = rb.velocity.y;
        
        if (Mathf.Abs(speedX) < 0.01f) speedX = 0.01f;
        if (Mathf.Abs(speedY) < 0.1f) return;

        travelTime = Math.Min(deltaX / speedX, deltaY/speedY); 
        if (float.IsNaN(travelTime) || float.IsInfinity(travelTime)) return;
        predictedX = position.x + rb.velocity.x * travelTime;
        
        target.transform.position = new Vector2( gpsTarget.x - (predictedX - gpsTarget.x), gpsTarget.y );
    }

    
    void OnDestroy()
    {
        Destroy(target);
    }
}

public class GPSBeaconBehaviour : MonoBehaviour
{
    DisplayBehaviour display;
    private void Awake()
    {
        var comps = GetComponents<GPSBeaconBehaviour>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        display = GetComponent<DisplayBehaviour>();
    }

    private void FixedUpdate()
    {
        display.Value = "x: " + Mathf.RoundToInt(transform.position.x) + "\ny: " + Mathf.RoundToInt(transform.position.y);
        display.UpdateDisplay();
    }
}