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
public class w_Fugitive : Role
{
    bool haveDied = false;
    public w_Fugitive() : base(ClassInjector.DerivedConstructorPointer<w_Fugitive>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Fugitive(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            new wx_SavedScripts().DebugMessage($"Initialised truthful Fugitive at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.OnPicked)
        {
            if (haveDied) return;
            haveDied = true;
            wx_SavedScripts sharedScripts = new();
            sharedScripts.DebugMessage($"Fugitive at #{charRef.id} was picked, dying..."); // oh no i died
            charRef.onReveal?.Invoke();
            charRef.Reveal();
            charRef.RevealAllReal();
            charRef.RevealStatusesIfAble();
            if (Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(charRef))
            {
                charRef.KillByDemon(charRef);
                // OnActed(ETriggerPhase.Day, charRef, sharedScripts.ReturnInfoWithSingleSelection($"Get away from me, #{CharacterPicker.CurrentPicker.id}!", CharacterPicker.CurrentPicker));
            }
            charRef.revealed = true;
            Health health = PlayerController.PlayerInfo.health;
            if (CharacterPicker.CurrentPicker.GetRegisterAlignment() == EAlignment.Evil) health.Damage(3);
            else health.Damage(2);
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            new wx_SavedScripts().DebugMessage($"Initialised lying Fugitive at #{charRef.id}");
        }
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

        if (!charRef.statuses.Contains(ECharacterStatus.Corrupted))
        {
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        }


        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
}


