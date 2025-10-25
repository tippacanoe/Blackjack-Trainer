using UnityEngine;

public class ModeTracker : MonoBehaviour
{

    public enum mode
    {
        standard,
        strategy,
        counting
    };

    private static mode currentMode;

    public static mode getCurrentMode()
    {
        return currentMode;
    } // getMode

    public static void setStandardMode()
    {
        currentMode = mode.standard;
    } // setStandardMode

    public static void setStrategyMode()
    {
        currentMode = mode.strategy;
    } // setStrategyMode

    public static void setCountingMode()
    {
        currentMode = mode.counting;
    } // setCountingMode
}
