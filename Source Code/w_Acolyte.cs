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
public class w_Acolyte : Role
{
    public w_Acolyte() : base(ClassInjector.DerivedConstructorPointer<w_Acolyte>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Acolyte(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
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
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
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
        //int diceRoll = Calculator.RollDice(10);
        wx_SavedScripts savedScripts = new wx_SavedScripts();
        CharacterData bluff = savedScripts.GetOverrideNotInPlayBluff(charRef, true);
        //}
        charRef.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
        return bluff;
    }
}


