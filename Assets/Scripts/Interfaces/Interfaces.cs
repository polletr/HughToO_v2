using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoDamage
{
    void DoDamage(ScriptableStats form, PlayerHealth health);
}