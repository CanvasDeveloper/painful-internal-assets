using PainfulSmile.Runtime.Utilities.AutoTimer.Core;
using UnityEngine;

public class AutoTimerSample : MonoBehaviour
{
    private AutoTimer myFirstTimer = new();
    private AutoTimer mySecondTimer = new();

    private void Start()
    {
        myFirstTimer.Start(2f);
        myFirstTimer.OnExpire += () =>
        {

            mySecondTimer.Start(1f, true);
            mySecondTimer.OnExpire += DebugLogOnTimerExpire;
        };
    }

    private void DebugLogOnTimerExpire()
    {
        Debug.Log("My Second Timer Expired");
    }
}
