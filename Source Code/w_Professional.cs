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
public class w_Professional : Role
{
    public w_Professional() : base(ClassInjector.DerivedConstructorPointer<w_Professional>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Professional(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
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
    }
    public string ConjourInfo()
    {
        return "";
    }

    private void ApplyStatuses(Character charRef)
    {
    }

    public CharacterData myBluffData;
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        if (charRef.GetRegisterAs() != charRef.dataRef)
        {
            return charRef.GetRegisterAs();
        }
        int diceRoll = Calculator.RollDice(10);
        CharacterData bluff = Characters.Instance.GetRandomDuplicateBluff();

        if (diceRoll < 5)
        {
            // 100% Double Claim
            bluff = Characters.Instance.GetRandomDuplicateBluff();
        }
        else
        {
            // Become a new character
            bluff = Characters.Instance.GetRandomUniqueBluff();
            Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        }
        myBluffData = bluff;
        return bluff;
    }
    public override CharacterData GetRegisterAsRole(Character charRef) // May have yoinked this from Linear's expansion pack because fml this guy is *not* working with my code for some reason. Thanks and sorry Linear.
    {
        if (charRef.bluff == true)
        {
            return charRef.bluff;
        }

        Il2CppSystem.Collections.Generic.List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
        notInPlayCh = Characters.Instance.FilterCharacterType(notInPlayCh, ECharacterType.Villager);
        notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);

        myBluffData = notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

        return myBluffData;
    }
}


