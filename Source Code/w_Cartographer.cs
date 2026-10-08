using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Cartographer : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> charsMinRange = sharedScripts.GetCharactersWithinRange(charRef, 2);
        charsMinRange.Add(charRef);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (!charsMinRange.Contains(character)) selection.Add(character);
        }

        if (selection.Count == 0) return new ActedInfo("Something does not make sense");

        int evils = 0;
        foreach (Character character in selection)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evils++;
        }

        string info = ConjureInfo(evils);
        return new ActedInfo(info, selection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new();
        Il2CppSystem.Collections.Generic.List<Character> charsMinRange = sharedScripts.GetCharactersWithinRange(charRef, 2);
        charsMinRange.Add(charRef);
        Il2CppSystem.Collections.Generic.List<Character> selection = new();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (!charsMinRange.Contains(character)) selection.Add(character);
        }

        if (selection.Count == 0) return new ActedInfo("Something does not make sense");

        Il2CppSystem.Collections.Generic.List<Character> fakeEvilTeam = sharedScripts.GetFakeEvilTeam();
        int evils = 0;
        int fakeEvils = 0;
        foreach (Character character in selection)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil) evils++;
            if (fakeEvilTeam.Contains(character)) fakeEvils++;
        }

        fakeEvils = sharedScripts.MakeNumberWrong(evils, fakeEvils, 0);

        string info = ConjureInfo(fakeEvils);
        return new ActedInfo(info, selection);
    }


    private string ConjureInfo(int evils)
    {
        string evilPlural = "Evils";
        if (evils == 1) evilPlural = "Evil";
        string isAre = "are";
        if (evils == 1) isAre = "is";
        string number = evils.ToString();
        if (number == "0") number = "NO";
        //if (number == "1") number = "only 1";

        return $"There {isAre} {number}\nlost {evilPlural}";
    }
    public override string Description
    {
        get
        {
            return "Learn how many Evils are far from me.";
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
    public w_Cartographer() : base(ClassInjector.DerivedConstructorPointer<w_Cartographer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Cartographer(IntPtr ptr) : base(ptr)
    {
    }
}


