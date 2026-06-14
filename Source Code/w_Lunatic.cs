using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Lunatic : Role
{
    public w_Lunatic() : base(ClassInjector.DerivedConstructorPointer<w_Lunatic>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Lunatic(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
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

    public override bool CheckIfCanRemoveStatus(ECharacterStatus status)
    {
        if (status == ECharacterStatus.Corrupted)
        {
            return false;
        }
        return true;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
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
    public override int GetDamageToYou()
    {
        return 3;
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        int diceRoll2 = Calculator.RollDice(10);
        if (diceRoll2 < 6 && !charRef.statuses.Contains(ECharacterStatus.Corrupted)) // If he's already Corrupted for some reason, he can't be given HealthyBluff.
        {
            //Tells the truth
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
            charRef.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
        }
        else
        {
            //Is full of shit
            charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        }
        int diceRoll = Calculator.RollDice(10);

        //List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
        //notInPlayCh = Characters.Instance.FilterAlignmentCharacters(notInPlayCh, EAlignment.Good);
        //notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);
        //return notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

        if (diceRoll < 5)
        {
            // 100% Double Claim
            return Characters.Instance.GetRandomDuplicateBluff();
        }
        else
        {
            // Become a new character
            CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();
            Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

            return bluff;
        }
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.statuses.statuses.Contains(ECharacterStatus.HealthyBluff))
            return charRef.bluff.role.CheckIfCanBeKilled(charRef);
        else
            return true;
    }
}


