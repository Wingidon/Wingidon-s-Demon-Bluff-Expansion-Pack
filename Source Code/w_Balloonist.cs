using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Balloonist : Role
{
    string info = "";
    Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
    int infoTimer = 0;
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        info = $"I saw, in order: {sharedScripts.MentionEveryCharacterInUnsortedList(selection, "")}";
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        info = $"I saw, in order: {sharedScripts.MentionEveryCharacterInUnsortedList(selection, "")}";
        return new ActedInfo(info, selection);
    }
    public override string Description
    {
        get
        {
            return "Learn characters of different types";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (charRef.GetState() == ECharacterState.Dead) return;
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            infoTimer = 0;
            selection.Add(Gameplay.CurrentCharacters[UnityEngine.Random.RandomRangeInt(0, Gameplay.CurrentCharacters.Count)]);
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal)
        {
            infoTimer++;
            if (infoTimer < 3) return;
            infoTimer -= 3;
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Character character = GetCharacterOfDifferentType(selection, charRef, selection[selection.Count - 1].GetCharacterType());
            if (character == charRef)
            {
                info = $"I saw, in order: {sharedScripts.MentionEveryCharacterInUnsortedList(selection, "")}, and nobody else";
                infoTimer = -99999;
                if (!Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(charRef)) OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
            }
            else
            {
                selection.Add(character);
                info = $"I saw, in order: {sharedScripts.MentionEveryCharacterInUnsortedList(selection, "")}";
                if (!Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(charRef)) OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
            }
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (charRef.GetState() == ECharacterState.Dead) return;
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            infoTimer = 0;
            selection.Add(Gameplay.CurrentCharacters[UnityEngine.Random.RandomRangeInt(0, Gameplay.CurrentCharacters.Count)]);
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal)
        {
            infoTimer++;
            if (infoTimer < 3) return;
            infoTimer -= 3;
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            if (selection.Count < 3) // use standalone logic
            {
                Character chosenPick = charRef;
                if (selection.Count == 1) // If we're grabbing our first extra character,
                {
                    chosenPick = GetRandomVillager(selection, charRef); // grab a Villager
                }
                else if (selection.Count == 2) // If we've already got two,
                {
                    if (selection[0].GetCharacterType() == ECharacterType.Villager) // If the first is a Villager,
                    {
                        chosenPick = GetRandomCharacter(selection, charRef); // Grab a random character
                    }
                    else // Otherwise,
                    {
                        chosenPick = GetRandomVillager(selection, charRef); // Grab a second Villager
                    }
                }
                else // Otherwise, grab a random Villager
                {
                    chosenPick = GetRandomVillager(selection, charRef);
                }
                if (chosenPick == charRef)
                {
                    info = $"I saw, in order: {sharedScripts.MentionEveryCharacterInUnsortedList(selection, "")}, and nobody else";
                    infoTimer = -99999;
                    if (!Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(charRef)) OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
                    return;
                }
                else selection.Add(chosenPick);
                OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
            }
            else
            {
                Character character = GetRandomCharacter(selection, charRef);
                if (character == charRef)
                {
                    info = $"I saw, in order: {sharedScripts.MentionEveryCharacterInUnsortedList(selection, "")}, and nobody else";
                    infoTimer = -99999;
                    if (!Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(charRef)) OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
                }
                else
                {
                    selection.Add(character);
                    info = $"I saw, in order: {sharedScripts.MentionEveryCharacterInUnsortedList(selection, "")}";
                    if (!Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters).Contains(charRef)) OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
                }
            }
        }
    }
    public Character GetCharacterOfDifferentType(Il2CppSystem.Collections.Generic.List<Character> avoidIfPossible, Character charRef, ECharacterType targetType)
    {
        Il2CppSystem.Collections.Generic.List<Character> diffTypeCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetCharacterType() != targetType && !avoidIfPossible.Contains(character)) diffTypeCharacters.Add(character);
        }
        diffTypeCharacters.Remove(charRef);
        if (diffTypeCharacters.Count == 0)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetCharacterType() != targetType) diffTypeCharacters.Add(character);
            }
        }
        diffTypeCharacters.Remove(charRef);
        if (diffTypeCharacters.Count == 0) return charRef;
        return diffTypeCharacters[UnityEngine.Random.RandomRangeInt(0, diffTypeCharacters.Count)];
    }
    public Character GetRandomVillager(Il2CppSystem.Collections.Generic.List<Character> avoidIfPossible, Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> villagers = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetCharacterType() == ECharacterType.Villager && !avoidIfPossible.Contains(character)) villagers.Add(character);
        }
        villagers.Remove(charRef);
        if (villagers.Count == 0)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetCharacterType() == ECharacterType.Villager) villagers.Add(character);
            }
        }
        villagers.Remove(charRef);
        if (villagers.Count == 0) return charRef;
        return villagers[UnityEngine.Random.RandomRangeInt(0, villagers.Count)];
    }
    public Character GetRandomCharacter(Il2CppSystem.Collections.Generic.List<Character> avoidIfPossible, Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (!avoidIfPossible.Contains(character)) characters.Add(character);
        }
        characters.Remove(charRef);
        if (characters.Count == 0)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                characters.Add(character);
            }
        }
        return characters[UnityEngine.Random.RandomRangeInt(0, characters.Count)];
    }
    public w_Balloonist() : base(ClassInjector.DerivedConstructorPointer<w_Balloonist>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Balloonist(IntPtr ptr) : base(ptr)
    {
    }
}


