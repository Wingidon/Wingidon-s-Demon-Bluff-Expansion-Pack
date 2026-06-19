using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Jewelsmith : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        System.Collections.Generic.List<Character> honestChars = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            bool disguising = false;
            disguising = CharacterHelper.CheckIfDisguisedAppearance(character);
            if (!disguising && character != charRef)
            {
                honestChars.Add(character);
            }
        }
        string line = "info";
        if (honestChars.Count == 0)
        {
            line = "Something does not make sense";
        }
        else
        {
            Character random = honestChars[UnityEngine.Random.RandomRangeInt(0, honestChars.Count)];
            selection.Add(random);
            line = string.Format("#{0} is Honest", random.id);
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        System.Collections.Generic.List<Character> disguisedChars = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            bool disguising = false;
            disguising = CharacterHelper.CheckIfDisguisedAppearance(character);
            if (disguising && character != charRef)
            {
                disguisedChars.Add(character);
            }
        }
        string line = "info";
        if (disguisedChars.Count == 0)
        {
            line = "Something does not make sense";
        }
        else
        {
            Character random = disguisedChars[UnityEngine.Random.RandomRangeInt(0, disguisedChars.Count)];
            selection.Add(random);
            line = string.Format("#{0} is Honest", random.id);
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn an Honest character.";
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
    public w_Jewelsmith() : base(ClassInjector.DerivedConstructorPointer<w_Jewelsmith>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Jewelsmith(IntPtr ptr) : base(ptr)
    {
    }
}


