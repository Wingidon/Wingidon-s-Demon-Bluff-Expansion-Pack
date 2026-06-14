using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using HarmonyLib;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Mezepheles : Role
{
    public w_Mezepheles() : base(ClassInjector.DerivedConstructorPointer<w_Mezepheles>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Mezepheles(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        UnityEngine.Debug.Log(string.Format("Mezepheles at #{0} acting...", charRef.id));
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Il2CppSystem.Collections.Generic.List<Character> possiblePoisonTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            possiblePoisonTargets = Characters.Instance.GetAdjacentCharacters(charRef);
            possiblePoisonTargets = Characters.Instance.FilterRealAlignmentCharacters(possiblePoisonTargets, EAlignment.Good);
            possiblePoisonTargets = Characters.Instance.FilterAlignmentCharacters(possiblePoisonTargets, EAlignment.Good);
            possiblePoisonTargets = Characters.Instance.FilterRealCharacterType(possiblePoisonTargets, ECharacterType.Villager);
            possiblePoisonTargets = Characters.Instance.FilterCharacterMissingStatus(possiblePoisonTargets, ECharacterStatus.Corrupted);
            UnityEngine.Debug.Log(string.Format("Found {0} possible poison target(s).", possiblePoisonTargets.Count));
            if (possiblePoisonTargets.Count != 0)
            {
                Character myPoisonTarget = possiblePoisonTargets[UnityEngine.Random.RandomRangeInt(0, possiblePoisonTargets.Count)];
                UnityEngine.Debug.Log(string.Format("Poisoned #{0}", myPoisonTarget.id));
                myPoisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                myPoisonTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                myPoisonTarget.GetRegisterAlignment();
            }
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if ((character.alignment == EAlignment.Good && character.GetRegisterAlignment() == EAlignment.Good && character.statuses.Contains(ECharacterStatus.Corrupted)) || character.dataRef.characterId == "Drunk_15369527" || character.dataRef.characterId == "Lunatic_WING")
                {
                    if (!sharedScripts.CheckIfAlwaysGood(character))
                    {
                        UnityEngine.Debug.Log(string.Format("Found possible Madness target at #{0}", character.id));
                        possibleTargets.Add(character);
                    }
                }
            }
            UnityEngine.Debug.Log(string.Format("Found {0} possible Madness target(s).", possibleTargets.Count));
            if (possibleTargets.Count != 0)
            {
                Character myMadnessTarget = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                UnityEngine.Debug.Log(string.Format("Maddened #{0}", myMadnessTarget.id));
                myMadnessTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                myMadnessTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                myMadnessTarget.statuses.AddStatus(w_MezephelesMadness.w_mezephelesMadness, charRef);
                myMadnessTarget.ChangeAlignment(EAlignment.Evil);
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
    /*public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        UnityEngine.Debug.Log(string.Format("Mezepheles at #{0} bluff-acting...", charRef.id));
        if (trigger == ETriggerPhase.Start)
        {
            Il2CppSystem.Collections.Generic.List<Character> possiblePoisonTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            possiblePoisonTargets = Characters.Instance.GetAdjacentCharacters(charRef);
            possiblePoisonTargets = Characters.Instance.FilterRealAlignmentCharacters(possiblePoisonTargets, EAlignment.Good);
            possiblePoisonTargets = Characters.Instance.FilterRealCharacterType(possiblePoisonTargets, ECharacterType.Villager);
            possiblePoisonTargets = Characters.Instance.FilterCharacterMissingStatus(possiblePoisonTargets, ECharacterStatus.Corrupted);
            UnityEngine.Debug.Log(string.Format("Found {0} possible poison target(s).", possiblePoisonTargets.Count));
            if (possiblePoisonTargets.Count != 0)
            {
                Character myPoisonTarget = possiblePoisonTargets[UnityEngine.Random.RandomRangeInt(0, possiblePoisonTargets.Count)];
                UnityEngine.Debug.Log(string.Format("Poisoned #{0}", myPoisonTarget.id));
                myPoisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                myPoisonTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
            }
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.alignment == EAlignment.Good && character.statuses.Contains(ECharacterStatus.Corrupted))
                {
                    UnityEngine.Debug.Log(string.Format("Found possible Madness target at #{0}", character.id));
                    possibleTargets.Add(character);
                }
            }
            UnityEngine.Debug.Log(string.Format("Found {0} possible Madness target(s).", possibleTargets.Count));
            if (possibleTargets.Count != 0)
            {
                Character myMadnessTarget = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                UnityEngine.Debug.Log(string.Format("Maddened #{0}", myMadnessTarget.id));
                myMadnessTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                myMadnessTarget.statuses.AddStatus(w_MezephelesMadness.w_mezephelesMadness, charRef);
                myMadnessTarget.ChangeAlignment(EAlignment.Evil);
            }
        }
    }*/
    public string ConjourInfo()
    {
        return "";
    }

    private void ApplyStatuses(Character charRef)
    {
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public static class w_MezephelesMadness
    {
        public static ECharacterStatus w_mezephelesMadness = (ECharacterStatus)968;

        [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
        public static class pvt
        {
            public static void Postfix(Character __instance)
            {
                if (__instance.statuses.Contains(w_mezephelesMadness))
                {
                    __instance.chName.text = __instance.dataRef.name.ToUpper() + "<color=#FF00AE><size=18>\n<Misled></color></size>";
                }
            }
        }
    }
}


