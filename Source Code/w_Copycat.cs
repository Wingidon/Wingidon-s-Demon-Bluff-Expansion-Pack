using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using UnityEngine;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Copycat : Role
{
    public override string Description
    {
        get
        {
            return "Disguises, but gives you extra health.";
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("Something does not make sense");
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("Something does not make sense");
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start && !CharacterHelper.CheckLying(charRef))
        {
            Health health = PlayerController.PlayerInfo.health;
            health.AddMaxHp(3);
            charRef.statuses.AddStatus(ECharacterStatus.WorkingAbility, charRef);
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        }
        if (trigger == ETriggerPhase.Day && charRef.bluff == null)
        {
            onActed.Invoke(this.GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day && charRef.bluff == null)
        {
            onActed.Invoke(this.GetBluffInfo(charRef));
        }
    }
    public w_Copycat() : base(ClassInjector.DerivedConstructorPointer<w_Copycat>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Copycat(System.IntPtr ptr) : base(ptr)
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
        return bluff;
    }

    public CharacterData GetOverrideDuplicateBluff(Character charRef)
    {
        CharacterData bluff = new CharacterData();
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleBluffs = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.dataRef.bluffable == true && character.dataRef.startingAlignment == EAlignment.Good && character.dataRef.type == ECharacterType.Villager)
            {
                possibleBluffs.Add(character.dataRef);
            }
        }
        if (possibleBluffs.Count != 0)
        {
            bluff = possibleBluffs[UnityEngine.Random.RandomRangeInt(0, possibleBluffs.Count)];
        }
        else
        {
            return null;
        }
        if (!charRef.statuses.Contains(ECharacterStatus.Corrupted))
        {
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        }
        return bluff;
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        if (charRef.statuses.statuses.Contains(ECharacterStatus.HealthyBluff))
        {
            return charRef.bluff.role.CheckIfCanBeKilled(charRef);
        }
        else return true;
    }
}