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
public class w_Echo : Role
{
    public w_Echo() : base(ClassInjector.DerivedConstructorPointer<w_Echo>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Echo(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            /*
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            int trues = 0;
            int falses = 0;
            for (int i = 0; i < 100; i++)
            {
                if (sharedScripts.PercentChance(33)) trues++;
                else falses++;
            }
            sharedScripts.DebugMessage($"33% chance resulted in {trues} trues and {falses} falses.");
            */
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal) // When any character is Revealed
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            bool onlyEvil = sharedScripts.PercentChance(33); // Artificial chance to Register only as Evil.
            if (onlyEvil && Characters.Instance.FilterRealAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil).Count != 0)
            {
                foreach (Character character in Characters.Instance.FilterRealAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Evil))
                {
                    if (character.dataRef.characterId != "Echo_WING")
                    {
                        possibleTargets.Add(character);
                    }
                }
            }
            else
            {
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.dataRef.characterId != "Echo_WING")
                    {
                        possibleTargets.Add(character);
                    }
                }
            }
            if (possibleTargets.Count != 0)
            {
                Character chosenTarget = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
                sharedScripts.DebugMessage($"Echo at #{charRef.id} updating registration to {chosenTarget.dataRef.characterName} at #{chosenTarget.id}. Artificially Evil?: {onlyEvil}");
                charRef.UpdateRegisterAsRole(chosenTarget.dataRef);
            }
            else
            {
                sharedScripts.DebugMessage($"Echo at #{charRef.id} found nothing to Register as! This shouldn't be happening, is everyone an Echo or something?");
            }
            if (sharedScripts.PercentChance(50))
            {
                if (charRef.statuses.Contains(ECharacterStatus.AppearLying))
                {
                    while (charRef.statuses.Contains(ECharacterStatus.AppearLying)) charRef.statuses.statuses.Remove(ECharacterStatus.AppearLying);
                }
                charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
            }
            else
            {
                if (charRef.statuses.Contains(ECharacterStatus.AppearTruthfull))
                {
                    while (charRef.statuses.Contains(ECharacterStatus.AppearTruthfull)) charRef.statuses.statuses.Remove(ECharacterStatus.AppearTruthfull);
                }
                charRef.statuses.AddStatus(ECharacterStatus.AppearLying, charRef);
            }
            if (sharedScripts.PercentChance(50))
            {
                if (charRef.statuses.Contains(ECharacterStatus.AppearDisguised))
                {
                    while (charRef.statuses.Contains(ECharacterStatus.AppearDisguised)) charRef.statuses.statuses.Remove(ECharacterStatus.AppearDisguised);
                }
                charRef.statuses.AddStatus(ECharacterStatus.AppearHonest, charRef);
            }
            else
            {
                if (charRef.statuses.Contains(ECharacterStatus.AppearHonest))
                {
                    while (charRef.statuses.Contains(ECharacterStatus.AppearHonest)) charRef.statuses.statuses.Remove(ECharacterStatus.AppearHonest);
                }
                charRef.statuses.AddStatus(ECharacterStatus.AppearDisguised, charRef);
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
        Act(wx_SavedScripts.w_AnyRevealPatch.AnyReveal, charRef);
        return null;
    }
}


