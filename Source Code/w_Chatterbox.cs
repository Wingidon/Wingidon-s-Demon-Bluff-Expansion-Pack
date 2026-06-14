using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using HarmonyLib;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Chatterbox : Role
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public Character chatterPoisonTarget = new Character();
    public w_Chatterbox() : base(ClassInjector.DerivedConstructorPointer<w_Chatterbox>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Chatterbox(System.IntPtr ptr) : base(ptr)
    {

    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            chatterPoisonTarget = charRef;
            Il2CppSystem.Collections.Generic.List<Character> unrevealedCharacters = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
            unrevealedCharacters = Characters.Instance.FilterCharacterType(unrevealedCharacters, ECharacterType.Villager);
            unrevealedCharacters = Characters.Instance.FilterAlignmentCharacters(unrevealedCharacters, EAlignment.Good);
            unrevealedCharacters = Characters.Instance.FilterCharacterMissingStatus(unrevealedCharacters, ECharacterStatus.Corrupted);
            onActed.Invoke(GetInfo(charRef));
            if (unrevealedCharacters.Count == 0) return;
            Character poisonTarget = unrevealedCharacters[UnityEngine.Random.RandomRangeInt(0, unrevealedCharacters.Count)];
            chatterPoisonTarget = poisonTarget;
            poisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
            if (poisonTarget.dataRef.characterId == "Copycat_WING") poisonTarget.statuses.statuses.Remove(ECharacterStatus.HealthyBluff);
        }
    }
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        string info = "Info";
        Il2CppSystem.Collections.Generic.List<Character> unrevealedCharacters = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        unrevealedCharacters.Remove(charRef);
        if (unrevealedCharacters.Count == 0)
        {
            info = "All characters have been revealed";
        }
        else if (unrevealedCharacters.Count == 1)
        {
            info = string.Format("I spoke to #{0}", unrevealedCharacters[0].id);
        }
        else
        {
            info = $"I spoke to {new wx_SavedScripts().MentionEveryCharacterInList(unrevealedCharacters, "or")}";
        }
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return new ActedInfo("");
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger != ETriggerPhase.Day) return;
        onActed.Invoke(GetInfo(charRef));
    }
    public string ConjourInfo()
    {
        return "";
    }

    public override CharacterData GetBluffIfAble(Character charRef)
    {
        return null;
    }
}


