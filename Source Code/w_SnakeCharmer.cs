using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static WingidonExpansionPack.w_Carnicarius;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_SnakeCharmer : Role
{
    int poisonTimer = 0;
    bool globalPoisonSuccess = false;
    Character globalPoisonTarget = new Character();
    public w_SnakeCharmer() : base(ClassInjector.DerivedConstructorPointer<w_SnakeCharmer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_SnakeCharmer(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        //MelonLogger.Msg($"Ritualist has been called to act! The trigger is {trigger}");
        if (trigger == ETriggerPhase.Start)
        {
            globalPoisonSuccess = false;
            Il2CppSystem.Collections.Generic.List<Character> goodVillagers = new Il2CppSystem.Collections.Generic.List<Character>();
            goodVillagers = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            goodVillagers = Characters.Instance.FilterCharacterType(goodVillagers, ECharacterType.Villager);
            goodVillagers = Characters.Instance.FilterRealAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            goodVillagers = Characters.Instance.FilterRealCharacterType(goodVillagers, ECharacterType.Villager);
            goodVillagers = Characters.Instance.FilterCharacterMissingStatus(goodVillagers, ECharacterStatus.Corrupted);
            if (goodVillagers.Count == 0) return;
            globalPoisonSuccess = true;
            Character poisonTarget = goodVillagers[UnityEngine.Random.RandomRangeInt(0, goodVillagers.Count)];
            globalPoisonTarget = poisonTarget;
            poisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
            poisonTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            poisonTarget.statuses.AddStatus(PoisonStatus.w_poisoned, charRef);
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal) // When any character is Revealed
        {
            //MelonLogger.Msg("Ritualist received AnyReveal trigger");
            poisonTimer++;
            if (poisonTimer == 5)
            {
                if (globalPoisonSuccess)
                {
                    if (globalPoisonTarget.GetState() != ECharacterState.Dead && globalPoisonTarget.alignment == EAlignment.Good && globalPoisonTarget.statuses.Contains(PoisonStatus.w_poisoned))
                    {
                        // globalPoisonTarget.OnReveal();
                        globalPoisonTarget.onReveal.Invoke();
                        globalPoisonTarget.KillByDemon(charRef);
                        globalPoisonTarget.Reveal();
                        globalPoisonTarget.RevealReal();
                        globalPoisonTarget.RevealAllReal();
                        globalPoisonTarget.pickable.SetActive(false);
                    }
                }
                if (charRef.GetState() == ECharacterState.Dead) return;
                Health health = PlayerController.PlayerInfo.health;
                health.Damage(2);
                /*
                Il2CppSystem.Collections.Generic.List<Character> goodVillagers = new Il2CppSystem.Collections.Generic.List<Character>();
                goodVillagers = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
                goodVillagers = Characters.Instance.FilterCharacterType(goodVillagers, ECharacterType.Villager);
                goodVillagers = Characters.Instance.FilterRealAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
                goodVillagers = Characters.Instance.FilterRealCharacterType(goodVillagers, ECharacterType.Villager);
                goodVillagers = Characters.Instance.FilterCharacterMissingStatus(goodVillagers, ECharacterStatus.Corrupted);
                goodVillagers = Characters.Instance.FilterHiddenCharacters(goodVillagers);
                if (goodVillagers.Count == 0) return;
                Character poisonTarget = goodVillagers[UnityEngine.Random.RandomRangeInt(0, goodVillagers.Count)];
                poisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                poisonTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                */
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
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
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
        poisonTimer = 0;
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
        return bluff;
    }
    public static class PoisonStatus
    {
        public static ECharacterStatus w_poisoned = (ECharacterStatus)1615919151;
        [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
        public static class pvt
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.statuses.Contains(w_poisoned))
                {
                    __instance.chName.text = __instance.dataRef.name.ToUpper() + "<color=#3F8538><size=18>\n<Poisoned></color></size>";
                }
            }
        }

        [HarmonyPatch(typeof(Character), nameof(Character.ShowDescription))]
        public static class ChangeKillByDemonText
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.killedByDemon && __instance.statuses.Contains(w_poisoned))
                {
                    string colour = "#3F8538";
                    if (__instance.alignment == EAlignment.Evil) colour = "#FF0000";
                    HintInfo info = new HintInfo();
                    info.text = $"Killed by <color={colour}>Poison</color>!\nCannot use abilities.";
                    UIEvents.OnShowHint.Invoke(info, __instance.hintPivot);
                }
            }
        }
    }
}


