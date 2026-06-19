using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Detective : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> liars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> truthers = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        newList = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        newList.Remove(charRef);
        string info = "Info";


        // First, let's sort every character into truthful and lying
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (checkIfCharacterLying(character))
            {
                truthers.Add(character);
            }
            else
            {
                liars.Add(character);
            }
        }

        // If nobody is unrevealed, we go to the failsafe.
        if (newList.Count == 0)
        {
            // If there's no truthful characters or no lying characters, the Detective has nothing.
            if (truthers.Count == 0 || liars.Count == 0)
            {
                info = "I got nothing";
                return new ActedInfo(info);
            }
            // Grab a truther and a liar
            selection.Add(truthers[UnityEngine.Random.RandomRangeInt(0, truthers.Count)]);
            selection.Add(liars[UnityEngine.Random.RandomRangeInt(0, liars.Count)]);
            selection = SortList(selection); // Sort them
            info = $"One is Lying:\n#{selection[0].id}, #{selection[1].id}"; // Learn that one is lying.
            return new ActedInfo(info, selection); // Return.
        }

        int liarCount = 0; // Set our liar count.
        foreach (Character character in newList)
        {
            selection.Add(character); // Add all Unrevealed characters to the selection.
            if (checkIfCharacterLying(character)) // If they're Lying...
            {
                // ...increment the liar count by 1.
                liarCount++;
            }
        }

        // Conjure the info
        info = ConjureInfo(selection, liarCount);
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<Character> realLiars = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (CharacterHelper.CheckLyingAppearance(character))
            {
                realLiars.Add(character);
            }
        }
        Il2CppSystem.Collections.Generic.List<Character> fakeLiars = sharedScripts.GetFakeGroup(realLiars);
        Il2CppSystem.Collections.Generic.List<Character> unrevealedChars = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        unrevealedChars.Remove(charRef);
        int trueNum = 0;
        int falseNum = 0;
        if (unrevealedChars.Count == 0) return GetLyingFailsafe(charRef);
        foreach (Character character in unrevealedChars)
        {
            if (realLiars.Contains(character)) trueNum++;
            if (fakeLiars.Contains(character)) falseNum++;
        }
        falseNum = sharedScripts.MakeNumberWrong(trueNum, falseNum, 0);
        string info = ConjureInfo(unrevealedChars, falseNum);
        return new ActedInfo(info, unrevealedChars);
    }
    public override string Description
    {
        get
        {
            return "Learn how many Lying characters aren't Revealed yet";
        }
    }
    public ActedInfo GetLyingFailsafe(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<Character> lyingChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> truthfulChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        string info = "";
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (CharacterHelper.CheckLyingAppearance(character)) lyingChars.Add(character);
            else truthfulChars.Add(character);
        }
        lyingChars.Remove(charRef);
        truthfulChars.Remove(charRef);
        bool canDoTruthful = false;
        bool canDoLying = false;
        if (truthfulChars.Count > 1) canDoTruthful = true;
        if (lyingChars.Count > 1) canDoLying = true;
        if (canDoTruthful && canDoLying)
        {
            if (sharedScripts.PercentChance(50)) canDoLying = false;
            else canDoTruthful = false;
        }

        if (canDoLying)
        {
            selection.Add(lyingChars[UnityEngine.Random.RandomRangeInt(0, lyingChars.Count)]);
            lyingChars.Remove(selection[0]);
            selection.Add(lyingChars[UnityEngine.Random.RandomRangeInt(0, lyingChars.Count)]);
        }
        else
        {
            selection.Add(truthfulChars[UnityEngine.Random.RandomRangeInt(0, truthfulChars.Count)]);
            truthfulChars.Remove(selection[0]);
            selection.Add(truthfulChars[UnityEngine.Random.RandomRangeInt(0, truthfulChars.Count)]);
        }

        selection = sharedScripts.SortList(selection);
        info = $"One is Lying:\n#{selection[0].id}, #{selection[1].id}";
        return new ActedInfo(info, selection);
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.GetBluffInfo(charRef));
        }
    }
    public w_Detective() : base(ClassInjector.DerivedConstructorPointer<w_Detective>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Detective(IntPtr ptr) : base(ptr)
    {
    }
    public Il2CppSystem.Collections.Generic.List<Character> SortList(Il2CppSystem.Collections.Generic.List<Character> list)
    {
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        for (int i = 0; i < Gameplay.CurrentCharacters.Count + 3; i++)
        {
            foreach (Character character in list)
            {
                if (character.id == i) newList.Add(character);
            }
        }
        return newList;
    }
    public bool checkIfCharacterLying(Character character)
    {
        bool lying = CharacterHelper.CheckLyingAppearance(character);
        return lying;
    }
    public string MentionEveryCharacterInList(Il2CppSystem.Collections.Generic.List<Character> characters)
    {
        string returnString = "Return";
        Il2CppSystem.Collections.Generic.List<Character> sortedCharacters = SortList(characters);
        foreach (Character character in sortedCharacters)
        {
            if (returnString == "Return")
            {
                returnString = $"#{character.id}";
            }
            else
            {
                returnString = $"{returnString}, #{character.id}";
            }
        }
        return returnString;
    }
    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<Character> characters, int liarCount)
    {
        string unrevealedCharacterInfo = MentionEveryCharacterInList(characters);
        string info = "";
        string firstWord = "Among";
        if (characters.Count == 1)
        {
            firstWord = "At";
        }
        info = $"{firstWord} {unrevealedCharacterInfo}, there are:\n{liarCount} Lying characters";
        if (liarCount == 0)
        {
            info = $"{firstWord} {unrevealedCharacterInfo}, there are:\nNO Lying characters";
        }
        if (liarCount == 1)
        {
            info = $"{firstWord} {unrevealedCharacterInfo}, there is:\n1 Lying character";
        }
        return info;
    }
}


