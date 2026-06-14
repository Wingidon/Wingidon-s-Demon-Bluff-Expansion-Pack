using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Arithmetician : Role
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
            if (character.GetRegisterAlignment() == EAlignment.Evil)
            {
                newList.Add(character);
            }
        }
        int myMathNumber = 0;
        foreach (Character character in newList)
        {
            myMathNumber += character.id;
        }
        string line = ConjureInfo(myMathNumber);
        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<Character> fakeEvilTeam = new Il2CppSystem.Collections.Generic.List<Character>();
        int trueSum = 0;
        int fakeSum = 0;
        int attempts = 0; // If we struggle too much, abort the loop and do something else
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Evil)
            {
                trueSum += character.id;
            }
        }
        MelonLogger.Msg($"Trying to get fake math number. True number is {trueSum}");
        fakeSum = trueSum;
        while (fakeSum == trueSum && attempts < 10)
        {
            attempts++;
            fakeSum = 0;
            fakeEvilTeam = sharedScripts.GetFakeEvilTeam();
            foreach (Character character in fakeEvilTeam)
            {
                fakeSum += character.id;
            }
            MelonLogger.Msg($"Attempt {attempts}: Gathered sum of {fakeSum}");
        }
        if (fakeSum == trueSum)
        {
            MelonLogger.Msg("Forget this, let's just go with the plus-or-minus one tactic.");
            if (fakeSum == 0) fakeSum++;
            else fakeSum--;
        }
        string line = ConjureInfo(fakeSum);
        ActedInfo actedInfo = new ActedInfo(line);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Learn the sum of all Evils.";
        }
    }
    public string ConjureInfo(int number)
    {
        if (number == 0)
        {
            Il2CppSystem.Collections.Generic.List<string> goodRemarks = new Il2CppSystem.Collections.Generic.List<string>();
            goodRemarks.Add("By my calculations, everyone here is Good!");
            goodRemarks.Add("This village is too Good to find anyone Evil!");
            goodRemarks.Add("Wait a minute, everyone is Good, why is the Executioner here?");
            goodRemarks.Add("Uh... Pardon me, who suggested this ritual? Everyone here is Good!");
            goodRemarks.Add("Did the Jester call you here? Or that old Trickster who hangs out in the tavern?");
            goodRemarks.Add("The... the sum of... Hang on, no, there must've been a mistake... The sum of... I... Zero. It's zero. What the hell.");
            return goodRemarks[UnityEngine.Random.RandomRangeInt(0, goodRemarks.Count)];
        }
        return $"The sum of all Evil is: {number}";
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            onActed.Invoke(GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Day)
        {
            onActed.Invoke(GetBluffInfo(charRef));
        }
    }
    public w_Arithmetician() : base(ClassInjector.DerivedConstructorPointer<w_Arithmetician>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Arithmetician(IntPtr ptr) : base(ptr)
    {
    }
}


