using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;
using System.ComponentModel.Design;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Performer : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            if (character.GetRegisterAlignment() != EAlignment.Good)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
            }
        }
        if (newList.Count > 1)
        {
            newList.Remove(charRef);
        }
        if (newList2.Count > 1)
        {
            newList2.Remove(charRef);
        }
        int adjacentEvils = CheckAdjacentEvils(charRef);
        Character random = newList[0];
        if (adjacentEvils == 0)
        {
            random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
        }
        else
        {
            random = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
        }
        selection.Add(random);
        string line = string.Format("#{0} is Evil", random.id);

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            if (character.GetRegisterAlignment() != EAlignment.Good)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
            }
        }
        if (newList.Count > 1)
        {
            newList.Remove(charRef);
        }
        if (newList2.Count > 1)
        {
            newList2.Remove(charRef);
        }
        int adjacentEvils = CheckAdjacentEvils(charRef);
        Character random = newList[0];
        if (adjacentEvils != 0)
        {
            random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
        }
        else
        {
            random = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
        }
        selection.Add(random);
        string line = string.Format("#{0} is Evil", random.id);

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn an Evil character, unless I sit next to Evil.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Day)
        {
            onActed.Invoke(GetInfo(charRef));
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
            onActed.Invoke(GetBluffInfo(charRef));
        }
    }
    public w_Performer() : base(ClassInjector.DerivedConstructorPointer<w_Performer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Performer(IntPtr ptr) : base(ptr)
    {
    }
    public int CheckAdjacentEvils(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> adjacentCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character ch in Gameplay.CurrentCharacters)
            if (charRef == ch)
            {
                adjacentCharacters = Characters.Instance.GetAdjacentCharacters(ch);
                break;
            }

        int evils = 0;

        foreach (Character ch in adjacentCharacters)
        {
            if (ch.GetRegisterAlignment() == EAlignment.Evil)
                evils++;
        }

        return evils;
    }
}


