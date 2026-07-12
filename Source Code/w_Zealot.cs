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
public class w_Zealot : Role
{
    public w_Zealot() : base(ClassInjector.DerivedConstructorPointer<w_Zealot>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Zealot(System.IntPtr ptr) : base(ptr)
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
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, charRef.dataRef);
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
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, charRef.dataRef);
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
        //int diceRoll = Calculator.RollDice(10);
        CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();

        //List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
        //notInPlayCh = Characters.Instance.FilterAlignmentCharacters(notInPlayCh, EAlignment.Good);
        //notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);
        //return notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

        //if (diceRoll < 5)
        //{
        //    // 100% Double Claim
        //    bluff = Characters.Instance.GetRandomDuplicateBluff();
        //}
        //else
        //{
        // Become a new character
        wx_SavedScripts savedScripts = new wx_SavedScripts();
        bluff = savedScripts.GetOverrideDuplicateBluff(charRef);
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        //}
        charRef.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
        return bluff;
    }
}


