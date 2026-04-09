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


public class AutocannonBehaviour : MonoBehaviour, Messages.IUse
{
    public bool isAP = false;
    public int RPM = 120;
    private PhysicalBehaviour phys;
    private ProjectileLauncherBehaviour projectileLauncher;
    private float randomSpread;
    private float localAngle = 0f;

    private SpawnableAsset apShell;
    private SpawnableAsset heShell;

    private void Start()
    {
        
        apShell = ModAPI.FindSpawnable("AP_shell_25mm_bushmaster");
        heShell = ModAPI.FindSpawnable("HE_shell_25mm_bushmaster");
        
        phys = GetComponent<PhysicalBehaviour>();
        projectileLauncher = GetComponent<ProjectileLauncherBehaviour>();
        
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton(
            "ChangeAmmoType",
            "Change Ammo Type",
            "Change ammo type",
            () =>
            {
                ChangeAmmoType();
            }
        ));
        phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("setRPM", "Set RPM", "Sets Rounds Per Minute", () => {
            DialogBox dialog = (DialogBox)null;
            dialog = DialogBoxManager.TextEntry("Enter new RPM (Rounds Per Minute)\n<color=orange><size=26>Maximum: 1200\nCurrently:"+RPM+"</size></color>", "Number", new DialogButton("Apply", true, new UnityAction[1] {
                    (UnityAction)(() => {
                        float setrange;
                        if (float.TryParse(dialog.EnteredText, out setrange)) {
                            RPM = Mathf.RoundToInt(setrange);
                            if (RPM > 1200) RPM = 1200;
                        }
                        projectileLauncher.AutomaticInterval = 60f/RPM;
                    })
                }),
                new DialogButton("Cancel", true, (UnityAction)(() => dialog.Close())));
        }));
        projectileLauncher.AutomaticInterval = 60f/RPM;
    }

    private void Awake()
    {
        var comps = GetComponents<AutocannonBehaviour>();
        if (comps.Length > 1)
        {
            Destroy(this);
            return;
        }
    }

    public void Use(ActivationPropagation ap)
    {
        /*
        if (ap.Channel == ActivationPropagation.Green)
        {
            randomSpread = RPM / 120f;
            localAngle =
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
                Vector2.right;
            projectileLauncher.barrelDirection = dir;
        }
        */
        if (ap.Channel == ActivationPropagation.Red)
        {
            ChangeAmmoType();
        } else return;
    }

    public void FixedUpdate()
    {
        if (!projectileLauncher.isActiveAndEnabled)
            return;

        randomSpread = RPM / 240f;
        localAngle =
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
            Vector2.right;
        projectileLauncher.barrelDirection = dir;
    }

    void ChangeAmmoType()
    {
        isAP = !isAP;
        if (isAP)
        {
            projectileLauncher.projectileAsset = apShell;
            projectileLauncher.ScreenShake = 0.5f;
            projectileLauncher.recoilMultiplier = 0.05f;
            projectileLauncher.projectileLaunchStrength = 100f;
        }
        else
        {
            projectileLauncher.projectileAsset = heShell;
            projectileLauncher.ScreenShake = 0.5f;
            projectileLauncher.recoilMultiplier = 0.05f;
            projectileLauncher.projectileLaunchStrength = 100f;
        }
    }
}