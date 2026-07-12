using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine.Playables;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Bloodseer : Role
{
    bool haveActedAlready = false;
    bool haveNewInfo = true;
    public override ActedInfo GetInfo(Character charRef)
    {
        bool infoGoodEnough = false;
        ActedInfo newInfo = GetRandomInfo(charRef);
        infoGoodEnough = true;
        if (haveActedAlready)
        {
            for (int i = 0; i < 10; i++)
            {
                if (newInfo.desc == "This village confuses me" || charRef.GetCurrentActedInfo().desc.Contains(newInfo.desc))
                {
                    infoGoodEnough = false;
                    MelonLogger.Msg("Not good enough, let's try again");
                    newInfo = GetRandomInfo(charRef);
                }
                else infoGoodEnough = true;
            }
            if (!infoGoodEnough)
            {
                newInfo.desc = "I got nothing";
            }
            if (charRef.GetCurrentActedInfo().desc != "")
            {
                newInfo.desc = charRef.GetCurrentActedInfo().desc + "\n" + newInfo.desc;
            }
        }
        else
        {
            while (newInfo.desc == "This village confuses me")
            {
                MelonLogger.Msg("Not good enough, let's try again");
                newInfo = GetRandomInfo(charRef);
            }
        }
        haveActedAlready = true;
        return newInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetInfo(charRef);
    }
    public override string Description
    {
        get
        {
            return "Learn random info for a price";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            haveActedAlready = false;
            //MelonLogger.Msg($"Bloodseer at {charRef.id} Acting AfterRoundStart");
        }
        if (trigger == ETriggerPhase.Day)
        {
            haveNewInfo = true;
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
            charRef.pickableUses += 1;
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(1);
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            haveActedAlready = false;
            //MelonLogger.Msg($"Bloodseer at {charRef.id} Bluff-Acting AfterRoundStart");
        }
        if (trigger == ETriggerPhase.Day)
        {
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(1);
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
            charRef.pickableUses += 1;
        }
    }

    public ActedInfo GetRandomInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        ActedInfo returnInfo = sharedScripts.GetRandomInfo(charRef, CharacterHelper.CheckLying(charRef), true, false);
        return returnInfo;
    }
    public w_Bloodseer() : base(ClassInjector.DerivedConstructorPointer<w_Bloodseer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Bloodseer(IntPtr ptr) : base(ptr)
    {
    }
}


