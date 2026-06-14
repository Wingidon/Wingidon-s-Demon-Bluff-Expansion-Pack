using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Fanatic : Role
{
    public w_Fanatic() : base(ClassInjector.DerivedConstructorPointer<w_Fanatic>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Fanatic(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, charRef.dataRef);
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("");
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("");
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, charRef.dataRef);
        }
    }
    public string ConjourInfo()
    {
        return "";
    }

    private void ApplyStatuses(Character charRef)
    {
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();
        wx_SavedScripts savedScripts = new wx_SavedScripts();
        if (UnityEngine.Random.RandomRangeInt(0,2) == 0)
        {
            bluff = savedScripts.GetOverrideNotInPlayBluff(charRef, true);
        }
        else
        {
            bluff = savedScripts.GetOverrideDuplicateBluff(charRef);
        }


        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        charRef.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
        return bluff;
    }
}


