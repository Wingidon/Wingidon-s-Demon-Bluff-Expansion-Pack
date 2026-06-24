using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static ExpansionPack.w_Mezepheles;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Iris : Role
{

    // TODO: CODE PLACEMENT RESTRICTIONS
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public w_Iris() : base(ClassInjector.DerivedConstructorPointer<w_Iris>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Iris(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Il2CppSystem.Collections.Generic.List<Character> possibleVictims = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> closestChars = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> furthestChars = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> evilTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            ApplyStatuses(charRef);
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.dataRef.type == ECharacterType.Villager && character.alignment == EAlignment.Good && !sharedScripts.CheckIfAlwaysGood(character) && !character.statuses.Contains(ECharacterStatus.Corrupted))
                {
                    possibleVictims.Add(character);
                }
            }
            int dist = 0;
            int closestDist = 1000;
            int furthestDist = 0;
            foreach (Character character in possibleVictims)
            {
                dist = sharedScripts.GetDistanceBetweenCharacters(character, charRef);
                if (dist == closestDist)
                {
                    closestChars.Add(character);
                }
                if (dist < closestDist)
                {
                    closestChars.Clear();
                    closestDist = dist;
                    closestChars.Add(character);
                }
                if (dist == furthestDist)
                {
                    furthestChars.Add(character);
                }
                if (dist > furthestDist)
                {
                    furthestChars.Clear();
                    furthestDist = dist;
                    furthestChars.Add(character);
                }
            }
            MelonLogger.Msg($"Closest character(s): {sharedScripts.MentionEveryCharacterInList(closestChars, "")}");
            MelonLogger.Msg($"Furthest character(s): {sharedScripts.MentionEveryCharacterInList(furthestChars, "")}");
            foreach (Character character in closestChars)
            {
                evilTargets.Add(character);
            }
            foreach (Character character in furthestChars)
            {
                evilTargets.Add(character);
            }
            if (evilTargets.Count != 0)
            {
                Character chosenEvilTarget = evilTargets[UnityEngine.Random.RandomRangeInt(0, evilTargets.Count)];
                chosenEvilTarget.ChangeAlignment(EAlignment.Evil);
                chosenEvilTarget.statuses.AddStatus(IrisStatus.w_irisTrick, charRef);
                chosenEvilTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                if (chosenEvilTarget.dataRef.characterId != "Knight_47970624") chosenEvilTarget.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                chosenEvilTarget.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
                chosenEvilTarget.statuses.AddStatus(ECharacterStatus.AppearLying, charRef);
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
    public string ConjourInfo()
    {
        return "";
    }

    private void ApplyStatuses(Character charRef)
    {
        charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
        charRef.statuses.AddStatus(ECharacterStatus.AppearHonest, charRef);
        if (charRef.dataRef.name.ToString() == "Iris") charRef.statuses.AddStatus(IrisStatus.w_irisName, charRef);
    }

    public CharacterData myBluffData;
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        if (charRef.GetRegisterAs() != charRef.dataRef)
        {
            return charRef.GetRegisterAs();
        }
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        CharacterData bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, false);
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public override CharacterData GetRegisterAsRole(Character charRef)
    {
        if (charRef.bluff == true)
        {
            return charRef.bluff;
        }
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        CharacterData bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, false);
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    public CharacterData GetIrisBluff()
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleBluffs = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
        {
            if (character.type == ECharacterType.Villager && character.bluffable == true && character.name.ToString() != "Bounty Hunter") // Iris shouldn't be Disguising as the Bounty Hunter.
            {
                possibleBluffs.Add(character);
            }
        }
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (possibleBluffs.Contains(character.dataRef))
            {
                possibleBluffs.Remove(character.dataRef);
            }
        }
        CharacterData irisBluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        if (possibleBluffs.Count != 0) irisBluff = possibleBluffs[UnityEngine.Random.RandomRangeInt(0, possibleBluffs.Count)];
        return irisBluff;
    }
}
public static class IrisStatus
{
    public static ECharacterStatus w_irisTrick = (ECharacterStatus)918919;
    public static ECharacterStatus w_irisName = (ECharacterStatus)918918;

    [HarmonyPatch(typeof(Character), nameof(Character.RevealAllReal))]
    public static class pvt
    {
        public static void Postfix(Character __instance)
        {
            if (__instance.statuses.Contains(w_irisTrick))
            {
                __instance.chName.text = __instance.dataRef.name.ToUpper() + "<color=#FF00AE><size=18>\n<Hypnotised></color></size>";
            }
            //if (__instance.statuses.Contains(w_irisName))
            //{
            //    if (__instance.name.ToString() != "Iris")
            //    {
            //        __instance.chName.text = "IRIS";
            //    }
            //}
        }
    }
}


