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
        Il2CppSystem.Collections.Generic.List<Character> fakeTruthers = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> fakeLiars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> liars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> truthers = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        newList = Characters.Instance.FilterHiddenCharacters(Gameplay.CurrentCharacters);
        newList.Remove(charRef);
        string info = "Info";
        int trueInfo = 0;

        foreach (Character character in newList)
        {
            selection.Add(character);
            if (checkIfCharacterLying(character)) trueInfo++;
        }

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            fakeTruthers.Add(character);
        }

        Character fakeLiar = new Character();
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
                // fakeLiar = fakeTruthers[UnityEngine.Random.RandomRangeInt(0, fakeTruthers.Count)]; // For lying convincingly
                // fakeLiars.Add(fakeLiar);
                // fakeTruthers.Remove(fakeLiar);
            }
        }

        // If nobody is unrevealed, we go to the failsafe.
        if (newList.Count == 0)
        {
            bool canDoTruthers = false;
            bool canDoLiars = false;
            if (truthers.Count > 1) canDoTruthers = true;
            if (truthers.Count > 1) canDoLiars = true;
            if (canDoTruthers && canDoLiars)
            {
                if (UnityEngine.Random.RandomRangeInt(0, 2) == 0)
                {
                    canDoTruthers = false;
                }
                else
                {
                    canDoLiars = false;
                }
            }
            if (canDoTruthers)
            {
                selection.Add(truthers[UnityEngine.Random.RandomRangeInt(0, truthers.Count)]);
                truthers.Remove(selection[0]);
                selection.Add(truthers[UnityEngine.Random.RandomRangeInt(0, truthers.Count)]);
            }
            else
            {
                selection.Add(liars[UnityEngine.Random.RandomRangeInt(0, liars.Count)]);
                liars.Remove(selection[0]);
                selection.Add(liars[UnityEngine.Random.RandomRangeInt(0, liars.Count)]);
            }
            selection = SortList(selection); // Sort them
            info = $"One is Lying:\n#{selection[0].id}, #{selection[1].id}"; // Learn that one is lying.
            return new ActedInfo(info, selection); // Return.
        }

        /* Right, this system is fucked, I'm scrapping it for the moment. Going to just use RNG.
        int trueLiarCount = 0; // Set our liar count.
        int falseLiarCount = 0; // Set our false liar count.
        foreach (Character character in newList)
        {
            selection.Add(character); // Add all Unrevealed characters to the selection.
            if (liars.Contains(character)) // If they're Lying...
            {
                // ...increment the liar count by 1.
                trueLiarCount++;
            }
            if (fakeLiars.Contains(character)) // If they're fake-Lying...
            {
                // ...increment the false liar count by 1.
                falseLiarCount++;
            }
        }
        if (falseLiarCount == trueLiarCount) // Failsafe to avoid accidentally true info.
        {
            if (falseLiarCount == 0)
            {
                falseLiarCount += 1;
            }
            else
            {
                falseLiarCount -= 1;
            }
        }
        */

        int liarCount = liars.Count;
        int unrevealedCharCount = newList.Count;
        int upperBound = 0;
        if (liarCount < unrevealedCharCount)
        {
            upperBound = unrevealedCharCount;
        }
        else
        {
            upperBound = liarCount;
        }

        upperBound = UnityEngine.Mathf.Min(liarCount, unrevealedCharCount);

        int falseLiarCount = UnityEngine.Random.RandomRangeInt(0, upperBound);
        if (falseLiarCount == trueInfo) // Failsafe to avoid accidentally true info.
        {
            if (falseLiarCount == 0)
            {
                falseLiarCount += 1;
            }
            else
            {
                falseLiarCount -= 1;
            }
        }

        // Conjure the info
        info = ConjureInfo(selection, falseLiarCount);
        return new ActedInfo(info, selection);
    }
    public override string Description
    {
        get
        {
            return "Learn how many Lying characters aren't Revealed yet";
        }
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


