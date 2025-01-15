using UnityEngine;

public class BuffButton : MonoBehaviour
{
    public Buffs currentBuff { get; private set; }

    public void SetCurrentBuff(Buffs scrObject)
    {
        currentBuff = scrObject;
    }
}
