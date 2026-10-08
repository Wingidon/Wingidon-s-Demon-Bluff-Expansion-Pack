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
public class w_Widow : Minion
{
    public w_Widow() : base(ClassInjector.DerivedConstructorPointer<w_Widow>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Widow(System.IntPtr ptr) : base(ptr)
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
            Il2CppSystem.Collections.Generic.List<Character> possibleTargets = Characters.Instance.FilterCharactersWithoutResistance(Gameplay.CurrentCharacters, ECharacterStatus.Silenced);
            possibleTargets = Characters.Instance.FilterCharacterMissingStatus(possibleTargets, ECharacterStatus.Silenced);
            if (possibleTargets.Count == 0) return; // How would this be possible?
            Character target = possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)];
            target.statuses.AddStatus(ECharacterStatus.Silenced, charRef);
            target.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
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

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        int diceRoll = Calculator.RollDice(10);
        CharacterData bluff = Characters.Instance.GetRandomDuplicateBluff();

        //List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
        //notInPlayCh = Characters.Instance.FilterAlignmentCharacters(notInPlayCh, EAlignment.Good);
        //notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);
        //return notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];


        if (diceRoll < 5)
        {
         //100% Double Claim
        bluff = sharedScripts.GetOverrideDuplicateBluff(charRef);
        }
        else
        {
            // Become a new character
            bluff = sharedScripts.GetOverrideNotInPlayBluff(charRef, true);
            Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        }
        if (bluff == null) bluff = Characters.Instance.GetRandomDuplicateBluff();
        sharedScripts.DebugMessage($"Widow chose bluff of {bluff.characterName}");
        return bluff;
    }
}


