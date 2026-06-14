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
public class w_Marionette : Role
{
    public w_Marionette() : base(ClassInjector.DerivedConstructorPointer<w_Marionette>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Marionette(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            SitNextToDemon(charRef);
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        return new ActedInfo("There's no Demons!\nI'm free!");
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("There's no Demons, but I still feel a bit off");
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
        //if (satNextToDemon(charRef))
        //{
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
        //}
        //else return null;
    }
    private void SitNextToDemon(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> checkDemons = new Il2CppSystem.Collections.Generic.List<Character>();
        checkDemons = Characters.Instance.FilterRealCharacterType(Gameplay.CurrentCharacters, ECharacterType.Demon);

        Character pickedDemon = checkDemons[UnityEngine.Random.Range(0, checkDemons.Count)];

        Il2CppSystem.Collections.Generic.List<Character> adjacentCharacters = Characters.Instance.GetAdjacentAliveCharacters(pickedDemon);
        Character pickedSwapCharacter = adjacentCharacters[UnityEngine.Random.Range(0, adjacentCharacters.Count)];
        CharacterData pickedData = pickedSwapCharacter.dataRef;
        pickedSwapCharacter.Init(charRef.dataRef);
        charRef.Init(pickedData);
    }
    public override CharacterData GetRegisterAsRole(Character charRef)
    {
        //Il2CppSystem.Collections.Generic.List<CharacterData> allChars = Gameplay.Instance.GetScriptCharacters();
        //allChars = Characters.Instance.FilterAlignmentCharacters(allChars, EAlignment.Evil);

        //CharacterData randomMinion = allChars[UnityEngine.Random.Range(0, allChars.Count)];

        //return randomMinion;

        var marionetteRegisterPuppet = ProjectContext.Instance.gameData.GetCharacterDataOfId("Puppet_15989619");
        return marionetteRegisterPuppet;
    }
    private bool satNextToDemon(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> adjacentCharacters = Characters.Instance.GetAdjacentCharacters(charRef);
        bool adjacentDemon = false;
        foreach (Character character in adjacentCharacters)
        {
            if (character.dataRef.type == ECharacterType.Demon)
            {
                adjacentDemon = true;
            }
        }
        return adjacentDemon;
    }
}


