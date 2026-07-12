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
public class w_Turncoat : Role
{
    public w_Turncoat() : base(ClassInjector.DerivedConstructorPointer<w_Turncoat>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Turncoat(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            ApplyStatuses(charRef);
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
        if (trigger == ETriggerPhase.Start)
        {
        }
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
        int diceRoll = Calculator.RollDice(10);
        CharacterData bluff = Characters.Instance.GetRandomDuplicateBluff();

        //List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
        //notInPlayCh = Characters.Instance.FilterAlignmentCharacters(notInPlayCh, EAlignment.Good);
        //notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);
        //return notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];


        //The commented out sections from here are what their bluff used to be.
        //if (diceRoll < 5)
        //{
        // 100% Double Claim
        bluff = GetOverrideDuplicateBluff(charRef);
        //}
        //else
        //{
            // Become a new character
            //bluff = Characters.Instance.GetRandomUniqueBluff();
            //Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        //}
        if (bluff.name != "Knight")
        {
            if (bluff.name != "Bombardier")
            {
                if (bluff.name != "Undying")
                {
                    charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                    charRef.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
                }
            }
        }
        if (bluff.name == "Swarm")
        {
            //charRef.statuses.RemoveStatusIfAble(ECharacterStatus.WorkingAbility);
            charRef.statuses.AddStatus(ECharacterStatus.BrokenAbility, charRef);
        }
        return bluff;
    }

    public CharacterData GetOverrideDuplicateBluff(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomDuplicateBluff();
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleBluffs = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if ((character.dataRef.bluffable == true && character.dataRef.startingAlignment == EAlignment.Good) || character.dataRef.characterId == "Swarm_Good_WING")
            {
                possibleBluffs.Add(character.dataRef);
            }
        }
        if (possibleBluffs.Count != 0)
        {
            bluff = possibleBluffs[UnityEngine.Random.RandomRangeInt(0, possibleBluffs.Count)];
        }
        return bluff;
    }
}


