using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Knave : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        ActedInfo trueInfo = sharedScripts.GetRandomInfo(charRef, false, false, false);
        ActedInfo falseInfo = sharedScripts.GetRandomInfo(charRef, true, false, false);

        string line = "";
        if (sharedScripts.PercentChance(50))
        {
            line = $"{trueInfo.desc}\nOR\n{falseInfo.desc}";
        }
        else
        {
            line = $"{falseInfo.desc}\nOR\n{trueInfo.desc}";
        }

        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in trueInfo.characters) selection.Add(character);
        foreach (Character character in falseInfo.characters) selection.Add(character);

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        ActedInfo infoOne = new ActedInfo("");
        ActedInfo infoTwo = new ActedInfo("");

        string line = "";
        if (sharedScripts.PercentChance(50))
        {
            infoOne = sharedScripts.GetRandomInfo(charRef, false, false, false);
            infoTwo = sharedScripts.GetRandomInfo(charRef, false, false, false);
        }
        else
        {
            infoOne = sharedScripts.GetRandomInfo(charRef, true, false, false);
            infoTwo = sharedScripts.GetRandomInfo(charRef, true, false, false);
        }
        line = $"{infoOne.desc}\nOR\n{infoTwo.desc}";

        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in infoOne.characters) selection.Add(character);
        foreach (Character character in infoTwo.characters) selection.Add(character);

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn a true statement and a false statement";
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
    public w_Knave() : base(ClassInjector.DerivedConstructorPointer<w_Knave>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Knave(IntPtr ptr) : base(ptr)
    {
    }
}


