using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Spy : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList2 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList3 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        newList = Characters.Instance.FilterHiddenCharacters(characters);
        newList2 = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Evil);
        newList2 = Characters.Instance.FilterHiddenCharacters(newList2);
        if (newList.Contains(charRef))
        {
            newList.Remove(charRef);
        }
        if (newList2.Contains(charRef))
        {
            newList2.Remove(charRef);
        }
        foreach (Character character in newList)
        {
            selection.Add(character);
        }
        string unrevealedChars = "#1";
        if (newList.Count == 0)
        {
            newList = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Good);
            newList2 = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Evil);
            Character myTargetChar1 = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            Character myTargetChar2 = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            ActedInfo myInfo = new ActedInfo("All characters have been Revealed", selection);
            if ((myTargetChar1.id > myTargetChar2.id))
            {
                myInfo = new ActedInfo(String.Format("Among #{0}, #{1}, there is: {2}", myTargetChar2.id, myTargetChar1.id, myTargetChar1.GetRegisterAs().name), selection);
            }
            else
            {
                myInfo = new ActedInfo(String.Format("Among #{0}, #{1}, there is: {2}", myTargetChar1.id, myTargetChar2.id, myTargetChar1.GetRegisterAs().name), selection);
            }
            return myInfo;
        }
        else if (newList.Count == 1)
        {
            unrevealedChars = string.Format("At #{0}", newList[0].id);
        }
        else
        {
            foreach (Character character in newList)
            {
                newList3.Insert(0, character);
            }
            newList.Clear();
            foreach (Character character in newList3)
            {
                newList.Add(character);
            }
            unrevealedChars = string.Format("Among #{0}", newList[0].id);
            newList.RemoveAt(0);
            int w_MyCounter = 0;
            foreach (Character character in newList)
            {
                unrevealedChars = string.Format("{0}, #{1}", unrevealedChars, newList[w_MyCounter].id);
                w_MyCounter++;
            }
        }
        string line = "info";
        Character learnedRole = characters[UnityEngine.Random.RandomRangeInt(0, characters.Count)];
        if (newList2.Count == 0)
        {
            line = string.Format("{0}, there are:\nNO Evils", unrevealedChars);
        }
        else
        {
            learnedRole = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            line = string.Format("{0}, there is:\n{1}", unrevealedChars, learnedRole.GetRegisterAs().name);
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList2 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> newList3 = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<CharacterData> evilCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData c in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
        {
            evilCharacters.Add(c);
        }
        Characters charInst = Characters.Instance;
        newList = Characters.Instance.FilterHiddenCharacters(characters);
        newList2 = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Evil);
        newList2 = Characters.Instance.FilterRevealedCharacters(newList2);
        if (!newList2.Contains(charRef))
        {
            newList2.Add(charRef);
        }
        if (newList.Contains(charRef))
        {
            newList.Remove(charRef);
        }
        foreach (Character character in newList)
        {
            selection.Add(character);
        }
        string unrevealedChars = "#1";
        if (newList.Count == 0)
        {
            newList = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Good);
            newList2 = Characters.Instance.FilterAlignmentCharacters(characters, EAlignment.Evil);
            CharacterData myTargetChar1 = evilCharacters[UnityEngine.Random.RandomRangeInt(0, evilCharacters.Count)];
            Character myTargetChar2 = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            newList.Remove(myTargetChar2);
            Character myTargetChar3 = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            ActedInfo myInfo = new ActedInfo("All characters have been Revealed", selection);
            if ((myTargetChar3.id > myTargetChar2.id))
            {
                myInfo = new ActedInfo(String.Format("Among #{0}, #{1}, there is: {2}", myTargetChar2.id, myTargetChar3.id, myTargetChar1.name.ToString()), selection);
            }
            else
            {
                myInfo = new ActedInfo(String.Format("Among #{0}, #{1}, there is: {2}", myTargetChar3.id, myTargetChar2.id, myTargetChar1.name.ToString()), selection);
            }
            return myInfo;
        }
        else if (newList.Count == 1)
        {
            unrevealedChars = string.Format("At #{0}", newList[0].id);
        }
        else
        {
            foreach (Character character in newList) // For some reason, the list is in descending order by default, and List.Reverse(), List.Sort() and List.OrderBy() don't work, so I'm using my own jank method instead.
            {
                newList3.Insert(0, character);
            }
            newList.Clear();
            foreach (Character character in newList3)
            {
                newList.Add(character);
            }
            unrevealedChars = string.Format("Among #{0}", newList[0].id);
            newList.RemoveAt(0);
            int w_MyCounter = 0;
            foreach (Character character in newList)
            {
                unrevealedChars = string.Format("{0}, #{1}", unrevealedChars, newList[w_MyCounter].id);
                w_MyCounter++;
            }
        }
        string line = "info";
        CharacterData learnedRole = new CharacterData();
        Il2CppSystem.Collections.Generic.List<CharacterData> deckEvils = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        //UnityEngine.Debug.Log(string.Format("Beginning Spy list for #{0}", charRef.id));
        foreach (CharacterData c in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Evil))
        {
            deckEvils.Add(c);
            //UnityEngine.Debug.Log(string.Format("Added {0} to Spy list. There are now {1} items in it.", c.name.ToString(), deckEvils.Count));
        }
        Il2CppSystem.Collections.Generic.List<Character> allCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character c in Gameplay.CurrentCharacters)
        {
            allCharacters.Add(c);
        }
        allCharacters = Characters.Instance.FilterHiddenCharacters(allCharacters);
        foreach (Character c in allCharacters)
        {
            if (c != charRef)
            {
                while (deckEvils.Contains(c.GetRegisterAs()))
                {
                    deckEvils.Remove(c.GetRegisterAs());
                    //UnityEngine.Debug.Log(string.Format("Found {0} at #{1}, removing it from Spy list. There are now {2} items in it.", c.GetRegisterAs().name, c.id, deckEvils.Count));
                }
            }
        }
        if (deckEvils.Count == 0)
        {
            line = string.Format("{0}, there are:\nNO Evils", unrevealedChars);
        }
        else
        {
            learnedRole = deckEvils[UnityEngine.Random.RandomRangeInt(0, deckEvils.Count)];
            line = string.Format("{0}, there is:\n{1}", unrevealedChars, learnedRole.name.ToString());
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn an Unrevealed Evil";
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
    public w_Spy() : base(ClassInjector.DerivedConstructorPointer<w_Spy>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Spy(IntPtr ptr) : base(ptr)
    {
    }
}


