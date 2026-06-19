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
public class w_Ritualist : Role
{
    int damageTimer = 0;
    public w_Ritualist() : base(ClassInjector.DerivedConstructorPointer<w_Ritualist>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Ritualist(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        //MelonLogger.Msg($"Ritualist has been called to act! The trigger is {trigger}");
        if (charRef.GetState() == ECharacterState.Dead) return;
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal) // When any character is Revealed
        {
            //MelonLogger.Msg("Ritualist received AnyReveal trigger");
            damageTimer++;
            if (damageTimer >= 3)
            {
                damageTimer -= 3;
                Health health = PlayerController.PlayerInfo.health;
                health.Damage(1);
            }
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
        Act(trigger, charRef);
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
        damageTimer = 0;
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


