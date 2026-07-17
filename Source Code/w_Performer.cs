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
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> goodChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new Il2CppSystem.Collections.Generic.List<Character>();
        bool shouldPingEvil = true;
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evilChars.Add(character);
            else goodChars.Add(character);
        }


        goodChars.Remove(charRef);
        evilChars.Remove(charRef);
        foreach (Character character in sharedScripts.GetCharacterNeighbours(charRef))
        {
            goodChars.Remove(character);
            evilChars.Remove(character);
            if (character.GetRegisterAlignment() == EAlignment.Evil) shouldPingEvil = false;
        }


        if (shouldPingEvil)
        {
            if (evilChars.Count == 0)
            {
                return new ActedInfo("Something does not make sense");
            }
            selection.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);
        }
        else
        {
            if (goodChars.Count == 0)
            {
                return new ActedInfo("Something does not make sense");
            }
            selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
        }

        return new ActedInfo($"#{selection[0].id} is Evil", selection);


    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> goodChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new Il2CppSystem.Collections.Generic.List<Character>();
        bool shouldPingEvil = true;
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evilChars.Add(character);
            else goodChars.Add(character);
        }


        goodChars.Remove(charRef);
        evilChars.Remove(charRef);
        foreach (Character character in sharedScripts.GetCharacterNeighbours(charRef))
        {
            goodChars.Remove(character);
            evilChars.Remove(character);
            if (character.GetRegisterAlignment() == EAlignment.Evil) shouldPingEvil = false;
        }


        if (!shouldPingEvil)
        {
            if (evilChars.Count == 0)
            {
                return new ActedInfo("Something does not make sense");
            }
            selection.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);
        }
        else
        {
            if (goodChars.Count == 0)
            {
                return new ActedInfo("Something does not make sense");
            }
            selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
        }

        return new ActedInfo($"#{selection[0].id} is Evil", selection);
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


