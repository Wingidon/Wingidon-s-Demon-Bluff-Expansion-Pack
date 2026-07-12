using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static WingidonExpansionPack.w_Caedoccidere;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Caedoccidere : Demon
{
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(4));
        return sr;
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Night)
        {
            if (charRef.state == ECharacterState.Dead) return;
            Il2CppSystem.Collections.Generic.List<Character> validTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Characters charInst = Characters.Instance;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                validTargets.Add(character);
            }
            validTargets.Remove(charRef);
            validTargets = Characters.Instance.FilterAliveCharacters(validTargets);
            validTargets = Characters.Instance.FilterRevealedCharacters(validTargets);
            validTargets = Characters.Instance.FilterAlignmentCharacters(validTargets, EAlignment.Good);
            validTargets = Characters.Instance.FilterCharacterMissingStatus(validTargets, ECharacterStatus.UnkillableByDemon);
            validTargets = Characters.Instance.FilterCharacterMissingStatus(validTargets, ECharacterStatus.KilledByEvil);
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(3);
            if (!(validTargets.Count == 0))
            {
                Character myTarget = validTargets[UnityEngine.Random.Range(0, validTargets.Count)];
                myTarget.statuses.AddStatus(ECharacterStatus.KilledByEvil, charRef);
                myTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                myTarget.statuses.AddStatus(CaedoKill.caedoccidereKill, charRef);
                myTarget.KillByDemon(charRef);
                if (myTarget.dataRef.picking)
                {
                    myTarget.pickable.SetActive(false);
                }
            }
        }
    }
    public w_Caedoccidere() : base(ClassInjector.DerivedConstructorPointer<w_Caedoccidere>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Caedoccidere(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }

    
    public static class CaedoKill
    {
        public static ECharacterStatus caedoccidereKill = (ECharacterStatus)132;
        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(caedoccidereKill))
                {
                    HintInfo info = new HintInfo();
                    info.text = "Killed by a <color=#FF9999>Demon</color>.\nCannot use abilities.\nTrue Role is not revealed.";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }
    

}


