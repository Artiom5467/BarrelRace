using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class BarrelControlActive : MonoBehaviour
{
    //(3, 2, 1, go)
    //"Countdown"
    private FreezeBarrel[] FreezeScripts;
    private UnFreezeBarrel[] UnfreezeScripts;
    private CarAIControl[] CarAIControlScripts;
    private BarreAICarBehaviour[] AICarBehaviourScripts;

    void Start()
    {
        FreezeScripts = FindObjectsOfType(typeof(FreezeBarrel)) as FreezeBarrel[];
        UnfreezeScripts = FindObjectsOfType(typeof(UnFreezeBarrel)) as UnFreezeBarrel[];
        CarAIControlScripts = FindObjectsOfType(typeof(CarAIControl)) as CarAIControl[];
        AICarBehaviourScripts = FindObjectsOfType(typeof(BarreAICarBehaviour)) as BarreAICarBehaviour[];
        foreach (FreezeBarrel fp in FreezeScripts)
            fp.enabled = false;
        foreach (UnFreezeBarrel up in UnfreezeScripts)
            up.enabled = true;
        foreach (CarAIControl cc in CarAIControlScripts)
            cc.enabled = true;
        foreach (BarreAICarBehaviour ac in AICarBehaviourScripts)
            ac.enabled = true;
    }
}