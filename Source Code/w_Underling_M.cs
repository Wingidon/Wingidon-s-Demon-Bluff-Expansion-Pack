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
public class w_Underling_M : Minion
{
    public w_Underling_M() : base(ClassInjector.DerivedConstructorPointer<w_Underling_M>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Underling_M(System.IntPtr ptr) : base(ptr)
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
        sharedScripts.DebugMessage($"Underling chose bluff of {bluff.characterName}");
        return bluff;
    }
}


