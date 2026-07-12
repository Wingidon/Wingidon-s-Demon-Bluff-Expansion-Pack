using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;
using System.Diagnostics.Metrics;
using System.ComponentModel.Design;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Visionary : Role
{
    string info = "Last night, you would not believe the things I saw";
    Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
    string lastInfo = "Last night, you would not believe the things I saw";
    int infoTimer = 0;
    int infoCount = 0;
    bool shouldAct = false;
    public override ActedInfo GetInfo(Character charRef)
    {
        string newInfo = "";
        Il2CppSystem.Collections.Generic.List<Character> validTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newSelection = new Il2CppSystem.Collections.Generic.List<Character>();

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character != charRef && !selection.Contains(character))
            {
                validTargets.Add(character);
            }
        }
        if (validTargets.Count == 0) return new ActedInfo("Something does not make sense");

        Character chosenTarget = validTargets[UnityEngine.Random.RandomRangeInt(0, validTargets.Count)];
        newSelection.Add(chosenTarget);

        CharacterData chosenGoodRole = new CharacterData();
        CharacterData chosenEvilRole = new CharacterData();
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleFakeGoods = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleFakeEvils = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().usuallyDisguised)
            {
                if (character.GetRegisterAs().startingAlignment == EAlignment.Good) possibleFakeGoods.Add(character.GetRegisterAs());
                if (character.GetRegisterAs().startingAlignment == EAlignment.Evil) possibleFakeEvils.Add(character.GetRegisterAs());
            }
        }
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (character.usuallyDisguised)
            {
                if (character.startingAlignment == EAlignment.Good) possibleFakeGoods.Add(character);
                if (character.startingAlignment == EAlignment.Evil) possibleFakeEvils.Add(character);
            }
        }
        wx_SavedScripts sharedScripts = new wx_SavedScripts();

        Il2CppSystem.Collections.Generic.List<CharacterData> possibleHiddenRoles = sharedScripts.GetPossibleHiddenRoles();
        if (possibleHiddenRoles.Count != 0)
        {
            foreach (CharacterData character in possibleHiddenRoles)
            {
                if (character.startingAlignment == EAlignment.Good) possibleFakeGoods.Add(character);
                if (character.startingAlignment == EAlignment.Evil) possibleFakeEvils.Add(character);
            }
        }
        if (possibleFakeGoods.Count == 0) possibleFakeGoods = Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Good);
        if (possibleFakeEvils.Count == 0) possibleFakeEvils = Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil);

        string goodRoleName = "Fog";
        string evilRoleName = "Fog";
        string theGoodRole = "";
        string theEvilRole = "";

        goodRoleName = possibleFakeGoods[UnityEngine.Random.RandomRangeInt(0, possibleFakeGoods.Count)].characterName;
        evilRoleName = possibleFakeEvils[UnityEngine.Random.RandomRangeInt(0, possibleFakeEvils.Count)].characterName;

        if (chosenTarget.bluff) // If they're Disguised, back up their Disguise
        {
            if (chosenTarget.bluff.characterName != chosenTarget.GetRegisterAs().characterName)
            {
                if (chosenTarget.bluff.startingAlignment == EAlignment.Good)
                {
                    goodRoleName = chosenTarget.bluff.characterName;
                }
                if (chosenTarget.bluff.startingAlignment == EAlignment.Evil) // Edge-cases
                {
                    evilRoleName = chosenTarget.bluff.characterName;
                }
            }
        }

        theGoodRole = sharedScripts.CheckIfThe(goodRoleName) + goodRoleName;
        theEvilRole = sharedScripts.CheckIfThe(evilRoleName) + evilRoleName;


        if (chosenTarget.GetRegisterAs().startingAlignment == EAlignment.Good) // Check their role's starting alignment, not their current alignment.
        {
            theGoodRole = sharedScripts.CheckIfThe(chosenTarget.GetRegisterAs().characterName) + chosenTarget.GetRegisterAs().characterName;
        }
        else
        {
            theEvilRole = sharedScripts.CheckIfThe(chosenTarget.GetRegisterAs().characterName) + chosenTarget.GetRegisterAs().characterName;
        }

        newInfo = $"#{chosenTarget.id} is {theGoodRole} or {theEvilRole}";
        return new ActedInfo(newInfo, newSelection);

    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        string newInfo = "";
        Il2CppSystem.Collections.Generic.List<Character> validTargets = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newSelection = new Il2CppSystem.Collections.Generic.List<Character>();

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character != charRef && !selection.Contains(character))
            {
                validTargets.Add(character);
            }
        }
        if (validTargets.Count == 0) return new ActedInfo("Something does not make sense");

        Character chosenTarget = validTargets[UnityEngine.Random.RandomRangeInt(0, validTargets.Count)];
        newSelection.Add(chosenTarget);

        CharacterData chosenGoodRole = new CharacterData();
        CharacterData chosenEvilRole = new CharacterData();
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleFakeGoods = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        Il2CppSystem.Collections.Generic.List<CharacterData> possibleFakeEvils = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().usuallyDisguised)
            {
                if (character.GetRegisterAs().startingAlignment == EAlignment.Good) possibleFakeGoods.Add(character.GetRegisterAs());
                if (character.GetRegisterAs().startingAlignment == EAlignment.Evil) possibleFakeEvils.Add(character.GetRegisterAs());
            }
        }
        foreach (CharacterData character in Gameplay.Instance.GetScriptCharacters())
        {
            if (character.usuallyDisguised)
            {
                if (character.startingAlignment == EAlignment.Good) possibleFakeGoods.Add(character);
                if (character.startingAlignment == EAlignment.Evil) possibleFakeEvils.Add(character);
            }
        }

        Il2CppSystem.Collections.Generic.List<CharacterData> possibleHiddenRoles = sharedScripts.GetPossibleHiddenRoles();
        if (possibleHiddenRoles.Count != 0)
        {
            foreach (CharacterData character in possibleHiddenRoles)
            {
                if (character.startingAlignment == EAlignment.Good) possibleFakeGoods.Add(character);
                if (character.startingAlignment == EAlignment.Evil) possibleFakeEvils.Add(character);
            }
        }
        if (possibleFakeGoods.Contains(chosenTarget.GetRegisterAs()))
        {
            while (possibleFakeGoods.Contains(chosenTarget.GetRegisterAs())) possibleFakeGoods.Remove(chosenTarget.GetRegisterAs());
        }
        if (possibleFakeEvils.Contains(chosenTarget.GetRegisterAs()))
        {
            while (possibleFakeEvils.Contains(chosenTarget.GetRegisterAs())) possibleFakeEvils.Remove(chosenTarget.GetRegisterAs());
        }
        sharedScripts.DebugMessage($"Lying Visionary finished gathering possible roles. Goods: {possibleFakeGoods.Count}. Evils: {possibleFakeEvils.Count}");
        if (possibleFakeGoods.Count == 0) possibleFakeGoods = Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Good);
        if (possibleFakeEvils.Count == 0) possibleFakeEvils = Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil);
        sharedScripts.DebugMessage($"Lying Visionary finished failsafe. Goods: {possibleFakeGoods.Count}. Evils: {possibleFakeEvils.Count}");
        if (possibleFakeGoods.Contains(chosenTarget.GetRegisterAs()))
        {
            while (possibleFakeGoods.Contains(chosenTarget.GetRegisterAs())) possibleFakeGoods.Remove(chosenTarget.GetRegisterAs());
        }
        if (possibleFakeEvils.Contains(chosenTarget.GetRegisterAs()))
        {
            while (possibleFakeEvils.Contains(chosenTarget.GetRegisterAs())) possibleFakeEvils.Remove(chosenTarget.GetRegisterAs());
        }
        sharedScripts.DebugMessage($"Lying Visionary finished making sure info was false. Goods: {possibleFakeGoods.Count}. Evils: {possibleFakeEvils.Count}");
        if (possibleFakeEvils.Count == 0)
        {
            possibleFakeEvils = sharedScripts.GetScriptRoles(EAlignment.Evil, ECharacterType.Minion);
            sharedScripts.DebugMessage($"Literally why is there only one Evil role in the Deck. New failsafe has resulted in: {possibleFakeEvils.Count} possible Evils");
        }

        string goodRoleName = "Fog";
        string evilRoleName = "Fog";
        string theGoodRole = "";
        string theEvilRole = "";

        goodRoleName = possibleFakeGoods[UnityEngine.Random.RandomRangeInt(0, possibleFakeGoods.Count)].characterName;
        evilRoleName = possibleFakeEvils[UnityEngine.Random.RandomRangeInt(0, possibleFakeEvils.Count)].characterName;

        if (chosenTarget.bluff) // If they're Disguised, back up their Disguise
        {
            if (chosenTarget.bluff.characterName != chosenTarget.GetRegisterAs().characterName)
            {
                if (chosenTarget.bluff.startingAlignment == EAlignment.Good)
                {
                    goodRoleName = chosenTarget.bluff.characterName;
                }
                if (chosenTarget.bluff.startingAlignment == EAlignment.Evil) // Edge-cases
                {
                    evilRoleName = chosenTarget.bluff.characterName;
                }
            }
        }

        theGoodRole = sharedScripts.CheckIfThe(goodRoleName) + goodRoleName;
        theEvilRole = sharedScripts.CheckIfThe(evilRoleName) + evilRoleName;

        newInfo = $"#{chosenTarget.id} is {theGoodRole} or {theEvilRole}";
        return new ActedInfo(newInfo, newSelection);
    }
    public override string Description
    {
        get
        {
            return "Basically just BotC Dreamer";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (charRef.GetState() == ECharacterState.Dead) return;
        if (trigger == ETriggerPhase.Init)
        {
            infoTimer = 0;
            infoCount = 0;
            info = "Last night, you would not believe the things I saw";
            selection.Clear();
            shouldAct = false;
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            infoTimer++;
            sharedScripts.DebugMessage($"Visionary at #{charRef.id} gathering info; infoTimer: {infoTimer}. infoCount: {infoCount}");
            if (infoTimer >= 3)
            {
                infoTimer -= 3;
                infoCount++;
                sharedScripts.DebugMessage($"Info timer is greater than or equal to three; new infoTimer: {infoTimer}. New infoCount: {infoCount}");
                ActedInfo newInfo = GetInfo(charRef);
                sharedScripts.DebugMessage($"New info: {newInfo.desc}");
                ActedInfo oldInfo = new ActedInfo(info, selection);
                string combinedInfo = "";
                Il2CppSystem.Collections.Generic.List<Character> combinedSelection = new Il2CppSystem.Collections.Generic.List<Character>();
                if (infoCount == 1)
                {
                    combinedInfo = newInfo.desc;
                    combinedSelection = newInfo.characters;
                    info = combinedInfo;
                    selection = combinedSelection;
                }
                else
                {
                    combinedInfo = $"{info}\n\n{newInfo.desc}";
                    foreach (Character character in selection)
                    {
                        combinedSelection.Add(character);
                    }
                    combinedSelection.Add(newInfo.characters[0]);
                }
                info = combinedInfo;
                selection = combinedSelection;
                if (shouldAct) charRef.Act(ETriggerPhase.Day);
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            shouldAct = true;
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (charRef.GetState() == ECharacterState.Dead) return;
        if (trigger == ETriggerPhase.Init)
        {
            infoTimer = 0;
            infoCount = 0;
            info = "Last night, you would not believe the things I saw";
            selection.Clear();
            shouldAct = false;
        }
        if (trigger == wx_SavedScripts.w_AnyRevealPatch.AnyReveal)
        {
            infoTimer++;
            if (infoTimer >= 3)
            {
                infoTimer -= 3;
                infoCount++;
                ActedInfo newInfo = new ActedInfo("");
                ActedInfo oldInfo = new ActedInfo(info, selection);
                string combinedInfo = "";
                Il2CppSystem.Collections.Generic.List<Character> combinedSelection = new Il2CppSystem.Collections.Generic.List<Character>();
                if (infoCount == 1)
                {
                    newInfo = GetBluffInfo(charRef);
                    combinedInfo = newInfo.desc;
                    combinedSelection = newInfo.characters;
                    info = combinedInfo;
                    selection = combinedSelection;
                }
                else
                {
                    if (new wx_SavedScripts().PercentChance(50)) newInfo = GetInfo(charRef);
                    else newInfo = GetBluffInfo(charRef);
                    combinedInfo = $"{info}\n\n{newInfo.desc}";
                    foreach (Character character in selection)
                    {
                        combinedSelection.Add(character);
                    }
                    combinedSelection.Add(newInfo.characters[0]);
                }
                info = combinedInfo;
                selection = combinedSelection;
                if (shouldAct) charRef.Act(ETriggerPhase.Day);
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            shouldAct = true;
            OnActed(ETriggerPhase.Day, charRef, new ActedInfo(info, selection));
        }
    }
    public w_Visionary() : base(ClassInjector.DerivedConstructorPointer<w_Visionary>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Visionary(IntPtr ptr) : base(ptr)
    {
    }
}


