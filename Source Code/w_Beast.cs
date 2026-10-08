using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using HarmonyLib;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Beast : Role
{
    public w_Beast() : base(ClassInjector.DerivedConstructorPointer<w_Beast>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Beast(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == (ETriggerPhase)251192041)
        {
            if (!Characters.Instance.FilterAliveCharacters(Gameplay.CurrentCharacters).Contains(charRef)) return;
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(3);
            Il2CppSystem.Collections.Generic.List<Character> goodChars = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            goodChars = Characters.Instance.FilterRealAlignmentCharacters(goodChars, EAlignment.Good);
            goodChars = Characters.Instance.FilterAliveCharacters(goodChars);
            goodChars.Remove(charRef);

            if (goodChars.Count == 0) return;
            Character target = goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)];
            target.KillByDemon(charRef);
            target.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            target.statuses.AddStatus((ECharacterStatus)251192011, charRef);
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
    public string ConjourInfo()
    {
        return "";
    }

    private void ApplyStatuses(Character charRef)
    {
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = new wx_SavedScripts().GetOverrideDuplicateBluff(charRef);
        return bluff;
    }
}
[HarmonyPatch(typeof(Character), nameof(Character.Act))]
public static class beastCheckExe
{
    public static ECharacterStatus leviathanExecTarg = (ECharacterStatus)1252291208;
    public static void Postfix(Character __instance, ETriggerPhase trigger)
    {
        if (trigger == ETriggerPhase.OnExecuted && __instance.dataRef.type == ECharacterType.Demon)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.dataRef.type != ECharacterType.Demon) character.Act((ETriggerPhase)251192041); // Beast execution check
            }
        }
    }
}
public static class BeastKill
{
    public static ECharacterStatus beastKill = (ECharacterStatus)251192011;
    [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
    public static class ChangeKillByDemonText
    {
        public static void Postfix(Character __instance)
        {
            if (__instance.killedByDemon && __instance.statuses.Contains(beastKill))
            {
                HintInfo info = new HintInfo();
                info.text = "Killed by the <color=#FF9999>Beast</color>.\nCannot use abilities.\nTrue Role is not revealed.";
                UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
            }
        }
    }
}

