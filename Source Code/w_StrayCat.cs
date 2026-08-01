using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;
using System.ComponentModel.Design;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_StrayCat : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evilChars.Add(character);
        }
        // Knitter
        int pairs = sharedScripts.GetPairsOfCharactersInList(evilChars);
        // Hunter
        int distance = sharedScripts.GetClosestDistance(evilChars, charRef);
        // Sapper
        int sapper = 0;
        foreach (Character character in sharedScripts.GetCharactersWithinRange(charRef, 2))
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) sapper++;
        }

        Il2CppSystem.Collections.Generic.List<int> numbers = new Il2CppSystem.Collections.Generic.List<int>();
        numbers.Add(pairs);
        numbers.Add(distance);
        numbers.Add(sapper);
        sharedScripts.DebugMessage($"Stray info: {pairs} pair(s) of Evil, {distance} card(s) away from Evil, {sapper} Sapper Evil(s)");
        return new ActedInfo(ConjureInfo(numbers));
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {

        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new();
        Il2CppSystem.Collections.Generic.List<Character> fakeEvilChars = sharedScripts.GetFakeEvilTeam();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evilChars.Add(character);
        }
        // Knitter
        int truePairs = sharedScripts.GetPairsOfCharactersInList(evilChars);
        int pairs = sharedScripts.GetPairsOfCharactersInList(fakeEvilChars);
        // Hunter
        int trueDistance = sharedScripts.GetClosestDistance(evilChars, charRef);
        int distance = sharedScripts.GetClosestDistance(fakeEvilChars, charRef);
        // Sapper
        int trueSapper = 0;
        int sapper = 0;
        foreach (Character character in sharedScripts.GetCharactersWithinRange(charRef, 2))
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) trueSapper++;
            if (fakeEvilChars.Contains(character)) sapper++;
        }
        Il2CppSystem.Collections.Generic.List<int> trueNumbers = new Il2CppSystem.Collections.Generic.List<int>();
        trueNumbers.Add(truePairs);
        trueNumbers.Add(trueDistance);
        trueNumbers.Add(trueSapper);

        Il2CppSystem.Collections.Generic.List<int> falseNumbers = new Il2CppSystem.Collections.Generic.List<int>();
        falseNumbers.Add(pairs);
        falseNumbers.Add(distance);
        falseNumbers.Add(sapper);

        if (trueNumbers[0] == falseNumbers[0] && trueNumbers[1] == falseNumbers[1] && trueNumbers[2] == falseNumbers[2])
        {
            int changedNumber = UnityEngine.Random.RandomRangeInt(0, 3);
            falseNumbers[changedNumber] = sharedScripts.MakeNumberWrong(trueNumbers[changedNumber], falseNumbers[changedNumber], 1);
        }

        sharedScripts.DebugMessage($"Lying Stray info: {falseNumbers[0]} pair(s) of Evil, {falseNumbers[1]} card(s) away from Evil, {falseNumbers[2]} Sapper Evil(s)");

        return new ActedInfo(ConjureInfo(falseNumbers));
    }
    public override string Description
    {
        get
        {
            return "Learn 3 numbers but not what they mean";
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
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
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
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }

    public string ConjureInfo(Il2CppSystem.Collections.Generic.List<int> numbers)
    {
        wx_SavedScripts sharedScripts = new();
        numbers = sharedScripts.SortList(numbers);
        return $"{numbers[0]}, {numbers[1]}, {numbers[2]}";
    }
    public w_StrayCat() : base(ClassInjector.DerivedConstructorPointer<w_StrayCat>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_StrayCat(IntPtr ptr) : base(ptr)
    {
    }
}


