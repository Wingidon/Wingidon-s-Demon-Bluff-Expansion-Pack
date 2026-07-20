using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Underling_V : Role
{
    public w_Underling_V() : base(ClassInjector.DerivedConstructorPointer<w_Underling_V>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Underling_V(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            if (charRef.dataRef.characterId == "Hypnotist_scm")
            {
                Il2CppSystem.Collections.Generic.List<Character> myself = new();
                myself.Add(charRef);
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo("I am Good", myself));
            }
            else OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (charRef.dataRef.characterId == "Hypnotist_scm")
        {
            Il2CppSystem.Collections.Generic.List<Character> myself = new();
            myself.Add(charRef);
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo("I am Good", myself));
        }
        else OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        return sharedScripts.GetRandomInfo(charRef, false, true, false);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        return sharedScripts.GetRandomInfo(charRef, true, true, false);
    }
    public string ConjourInfo()
    {
        return "";
    }
}


