using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Paperboy : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        int myChain = 0;
        Il2CppSystem.Collections.Generic.List<Character> tripleVillage = new Il2CppSystem.Collections.Generic.List<Character>();
        for (int i = 0; i < 3; i++)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                tripleVillage.Add(character);
            }
        }
        bool foundSelfOnce = false;
        bool foundSelfTwice = false;
        bool foundEvilAfterSelfTwice = false;
        foreach (Character character in tripleVillage)
        {
            if (character != charRef && character.GetRegisterAlignment() == EAlignment.Evil && foundSelfTwice)
            {
                foundEvilAfterSelfTwice = true;
                break;
            }
            if (character == charRef)
            {
                if (!foundSelfOnce) foundSelfOnce = true;
                else foundSelfTwice = true;
            }
            if (character.GetRegisterAlignment() == EAlignment.Good || character == charRef)
            {
                myChain += 1;
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil && character != charRef)
            {
                myChain = 0;
            }
        }
        if (!foundEvilAfterSelfTwice) return new ActedInfo("Something does not make sense");
        string cardPlural = "cards";
        if (myChain == 1) cardPlural = "card";

        ActedInfo actedInfo = new ActedInfo($"My chain of Good is:\n{myChain} {cardPlural} long");
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        int myChain = 0;
        int myFakeChain = 0;
        Il2CppSystem.Collections.Generic.List<Character> tripleVillage = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> fakeGoods = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> fakeEvils = new Il2CppSystem.Collections.Generic.List<Character>();
        fakeEvils = sharedScripts.GetFakeEvilTeam();
        if (fakeEvils.Count == 0) return new ActedInfo($"My chain of Good is:\n{UnityEngine.Random.RandomRangeInt(2, 6)} cards long");
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (!fakeEvils.Contains(character)) fakeGoods.Add(character);
        }
        for (int i = 0; i < 3; i++)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                tripleVillage.Add(character);
            }
        }
        bool foundSelfOnce = false;
        bool foundSelfTwice = false;
        bool foundEvilAfterSelfTwice = false;
        foreach (Character character in tripleVillage)
        {
            if (character != charRef && character.GetRegisterAlignment() == EAlignment.Evil && foundSelfTwice)
            {
                foundEvilAfterSelfTwice = true;
                break;
            }
            if (character == charRef)
            {
                if (!foundSelfOnce) foundSelfOnce = true;
                else foundSelfTwice = true;
            }
            if (character.GetRegisterAlignment() == EAlignment.Good || character == charRef)
            {
                myChain += 1;
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil && character != charRef)
            {
                myChain = 0;
            }
        }

        foundSelfOnce = false;
        foundSelfTwice = false;
        foundEvilAfterSelfTwice = false;
        foreach (Character character in tripleVillage)
        {
            if (character != charRef && fakeEvils.Contains(character) && foundSelfTwice)
            {
                foundEvilAfterSelfTwice = true;
                break;
            }
            if (character == charRef)
            {
                if (!foundSelfOnce) foundSelfOnce = true;
                else foundSelfTwice = true;
            }
            if (fakeGoods.Contains(character) || character == charRef)
            {
                myFakeChain += 1;
            }
            if (fakeEvils.Contains(character) && character != charRef)
            {
                myFakeChain = 0;
            }
        }
        if (!foundEvilAfterSelfTwice) return new ActedInfo("Something does not make sense");
        myFakeChain = sharedScripts.MakeNumberWrong(myChain, myFakeChain, 1);
        string cardPlural = "cards";
        if (myFakeChain == 1) cardPlural = "card";

        ActedInfo actedInfo = new ActedInfo($"My chain of Good is:\n{myFakeChain} {cardPlural} long");
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn the length of my chain of Good characters";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }
    public w_Paperboy() : base(ClassInjector.DerivedConstructorPointer<w_Paperboy>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Paperboy(IntPtr ptr) : base(ptr)
    {
    }
}


