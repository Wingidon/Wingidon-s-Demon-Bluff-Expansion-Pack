using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;
using System.Diagnostics.Metrics;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Introvert : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character c in characters)
        {
            if (GetDistanceBetweenCharacters(charRef, c, characters.Count) < 3 && c != charRef) // Make sure we're not adding ourselves to the list!
            {
                newList.Add(c);
                selection.Add(c);
                UnityEngine.Debug.Log(string.Format("Adding character at #{0}", c.id));
            }
        }
        Character char1 = new Character();
        Character char2 = new Character();
        UnityEngine.Debug.Log(string.Format("Found {0} valid characters", newList.Count));
        char1 = newList[UnityEngine.Random.Range(0, newList.Count)];
        UnityEngine.Debug.Log(string.Format("Using {1} at #{0}", char1.id, char1.GetRegisterAs().name));
        newList.Remove(char1);
        char2 = newList[UnityEngine.Random.Range(0, newList.Count)];
        UnityEngine.Debug.Log(string.Format("Using {1} at #{0}", char2.id, char2.GetRegisterAs().name));
        string line = ConjourInfo(char1.GetRegisterAs().name, char2.GetRegisterAs().name);
        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef) // Backs up a Disguised character's Disguise, or accuses a nearby character of being a random Evil in the Deck or a random character that's Disguised somewhere.
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<CharacterData> correctInfo = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        Il2CppSystem.Collections.Generic.List<CharacterData> incorrectInfo = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        Il2CppSystem.Collections.Generic.List<CharacterData> chosenInfo = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (Character character in Gameplay.CurrentCharacters) // First we need to add our possible info
        {
            if (GetDistanceBetweenCharacters(charRef, character, Gameplay.CurrentCharacters.Count) < 3)
            {
                if (character != charRef) // If we're not looking at ourselves,
                {
                    selection.Add(character); // Add it to the Selection
                }
                correctInfo.Add(character.GetRegisterAs()); // Adds correct info to the 'Correct' list.
                if (character.bluff != null && character != charRef)
                {
                    incorrectInfo.Add(character.bluff); // Adds nearby Disguised characters' bluffs to the 'Incorrect' list.
                }
            }
            else if (character.bluff != null)
            {
                incorrectInfo.Add(character.GetRegisterAs()); // Adds non-nearby Disguised characters to the 'Incorrect' list.
            }
            foreach (CharacterData c in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
            {
                incorrectInfo.Add(c); // Adds all Evil characters in the Deck to the 'Incorrect' list.
            }
        }
        // Now we need to make sure the Introvert's not gonna say anything 'correct'
        // So I'm going to go through the 'Correct' list and remove all instances of everything from there from the 'incorrect' list.
        foreach (CharacterData correctChar in correctInfo) // For every bit of correct info,
        {
            if (incorrectInfo.Contains(correctChar)) // If the incorrect list contains it,
            {
                while (incorrectInfo.Contains(correctChar)) // For as long as the incorrect list contains it,
                {
                    incorrectInfo.Remove(correctChar); // Remove it from the incorrect list.
                }
            } // Goddamn these brackets are ugly
        }
        CharacterData targetInfo = new CharacterData();
        if (UnityEngine.Random.RandomRangeInt(0, 2) == 1)
        {
            //Yield a piece of correct info
            targetInfo = correctInfo[UnityEngine.Random.RandomRangeInt(0, correctInfo.Count)];
            chosenInfo.Add(targetInfo);
        }
        else
        {
            // All info is incorrect
            targetInfo = incorrectInfo[UnityEngine.Random.RandomRangeInt(0, incorrectInfo.Count)];
            chosenInfo.Add(targetInfo);
            while (incorrectInfo.Contains(targetInfo))
            {
                incorrectInfo.Remove(targetInfo);
            }
        }
        // Get an incorrect statement
        if (incorrectInfo.Count == 0) // Sometimes a Lying Introvert has every Disguised character *and* the characters they're Disguised as near them.
        {
            foreach (CharacterData c in Gameplay.Instance.GetScriptCharacters()) // Grabs everything currently on the script.
            {
                if (!correctInfo.Contains(c)) incorrectInfo.Add(c); // Adds every non-correct piece of info to the list.
            }
        }
        targetInfo = incorrectInfo[UnityEngine.Random.RandomRangeInt(0, incorrectInfo.Count)];
        chosenInfo.Add(targetInfo);

        // Now we need to conjure the info's wording.
        string line = "";
        if (UnityEngine.Random.RandomRangeInt(0, 2) == 1) // Start by randomising the order.
        {
            line = string.Format("The {0} and the {1} sit within 2 cards of me", chosenInfo[1].name.ToString(), chosenInfo[0].name.ToString());
        }
        else
        {
            line = string.Format("The {0} and the {1} sit within 2 cards of me", chosenInfo[0].name.ToString(), chosenInfo[1].name.ToString());
        }
        return new ActedInfo(line, selection); // Okay, so theoretically, she should be correctly getting annoyed by characters not actually near her when she's Lying. If not, she'll be annoyed by *me* when I stab her in the face.
    }
    public override string Description
    {
        get
        {
            return "Learn 2 characters within 2 cards of me.";
        }
    }
    public int GetDistanceBetweenCharacters(Character char1, Character char2, int totalCharCount)
    {
        int tempDist = char1.id - char2.id;
        if (tempDist < 0)
        {
            tempDist = tempDist + totalCharCount;
        }
        int tempDist2 = char2.id - char1.id;
        if (tempDist2 < 0)
        {
            tempDist2 = tempDist2 + totalCharCount;
        }
        if (tempDist > tempDist2)
        {
            return tempDist2;
        }
        return tempDist;
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            this.onActed.Invoke(this.GetBluffInfo(charRef));
        }
    }
    public string ConjourInfo(string charName1, string charName2)
    {
       return string.Format("The {0} and the {1} sit within 2 cards of me", charName1, charName2);
    }
    public w_Introvert() : base(ClassInjector.DerivedConstructorPointer<w_Introvert>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Introvert(IntPtr ptr) : base(ptr)
    {
    }
}


