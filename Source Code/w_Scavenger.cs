using Il2Cpp;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Scavenger : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("Ready when you are.", null);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("Ready when you are.", null);
    }
    public override string Description
    {
        get
        {
            return "Heals you based on dead Evils.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        int healAmount = 0;
        foreach (Character character in characters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil && character.state == ECharacterState.Dead)
            {
                healAmount += 2;
                selection.Add(character);
            }
        }
        if (healAmount == 0) return;
        Health health = PlayerController.PlayerInfo.health;
        health.AddMaxHp((healAmount/2));
        health.Heal(healAmount/2);
        string info = string.Format("I found {0} health's worth of resources.", healAmount);
        ActedInfo actedInfo = new ActedInfo(info, selection);
        onActed?.Invoke(new ActedInfo(info, selection));
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;

        Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
        int healAmount = 0;
        foreach (Character character in characters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil && character.state == ECharacterState.Dead)
            {
                healAmount += 2;
                selection.Add(character);
            }
        }
        if (healAmount == 0) return;
        EAlignment myAlignment = charRef.alignment;
        Health health = PlayerController.PlayerInfo.health;
        health.Damage(healAmount);
        if (charRef.state != ECharacterState.Dead)
        {
            // charRef.statuses.AddStatus(ScavengerText.scavengerKill, charRef);
            //charRef.statuses.AddStatus(MainMod.HiddenRoleStatus.hiddenRole, charRef);
            //charRef.ChangeAlignment(EAlignment.Good);
            //charRef.KillByDemon(charRef); Don't die anymore.
            //charRef.ChangeAlignment(myAlignment);
        }
        string info = string.Format("I took {0} damage while scavenging.", healAmount);

        onActed?.Invoke(new ActedInfo(info, selection));
        Debug.Log($"{info}");
    }
    public w_Scavenger() : base(ClassInjector.DerivedConstructorPointer<w_Scavenger>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Scavenger(System.IntPtr ptr) : base(ptr)
    {
    }
    /*
    public static class ScavengerText
    {
        public static ECharacterStatus scavengerKill = (ECharacterStatus)612;
        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(scavengerKill))
                {
                    HintInfo info = new HintInfo();
                    info.text = "Killed while out scavenging.\nTrue Role is not revealed.";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }
    */
}