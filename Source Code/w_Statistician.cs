using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using static MelonLoader.MelonLogger;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Statistician : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            evilChars.Add(character);
        }

        /* possible info:
        - Is odd/even
        - Is greater/less than (size+-1)
        - Is a prime number
        - Is greater/less than my number
        - Is/isn't divisible by 3
         */
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Character evilOne = sharedScripts.GetRandomItemOfList(evilChars);
        evilChars.Remove(evilOne);
        Character evilTwo = sharedScripts.GetRandomItemOfList(evilChars);

        int sum = evilOne.id + evilTwo.id;

        Il2CppSystem.Collections.Generic.List<string> trueInfo = new Il2CppSystem.Collections.Generic.List<string>();

        // First, check if odd or even
        if (sum / 2 == RoundValToInt(sum / 2)) trueInfo.Add("even");
        else trueInfo.Add("odd");

        // Next, greater or less than village size
        Il2CppSystem.Collections.Generic.List<int> villageSizeCheckerInts = new Il2CppSystem.Collections.Generic.List<int>();
        if (Gameplay.CurrentCharacters.Count + 2 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count + 2);
        if (Gameplay.CurrentCharacters.Count + 1 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count + 1);
        if (Gameplay.CurrentCharacters.Count != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count);
        if (Gameplay.CurrentCharacters.Count - 1 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count - 1);
        if (Gameplay.CurrentCharacters.Count - 2 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count - 2);
        int villageSizeChecker = sharedScripts.GetRandomItemOfList(villageSizeCheckerInts);
        if (sum < villageSizeChecker) trueInfo.Add($"less than {villageSizeChecker}");
        else trueInfo.Add($"greater than {villageSizeChecker}");

        // Next, check if prime
        if (chkprime(sum)) trueInfo.Add("prime");

        // Check if greater or less than charRef number, but only if it's less than the size of the village
        if (!trueInfo.Contains($"greater than {villageSizeChecker}"))
        {
            if (charRef.id < sum) trueInfo.Add("greater than my number");
            if (charRef.id > sum) trueInfo.Add("less than my number");
        }

        // Check if divisible by 3
        if (sum / 3 == RoundValToInt(sum / 3)) trueInfo.Add("divisible by 3");
        else trueInfo.Add("not divisible by 3");

        string infoOne = sharedScripts.GetRandomItemOfList(trueInfo);
        trueInfo.Remove(infoOne);
        string infoTwo = sharedScripts.GetRandomItemOfList(trueInfo);
        string evilNameOne = evilOne.GetRegisterAs().characterName;
        string evilNameTwo = evilTwo.GetRegisterAs().characterName;

        string info = $"The sum of the positions of {evilNameOne} and {evilNameTwo} is {infoOne} and {infoTwo}";
        return new ActedInfo(info);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            evilChars.Add(character);
        }

        /* possible info:
        - Is odd/even
        - Is greater/less than (size+-1)
        - Is a prime number
        - Is greater/less than my number
        - Is/isn't divisible by 3
         */
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Character evilOne = sharedScripts.GetRandomItemOfList(evilChars);
        evilChars.Remove(evilOne);
        Character evilTwo = sharedScripts.GetRandomItemOfList(evilChars);

        int sum = evilOne.id + evilTwo.id;

        Il2CppSystem.Collections.Generic.List<string> trueInfo = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<string> falseInfo = new Il2CppSystem.Collections.Generic.List<string>();

        // First, check if odd or even
        if (sum / 2 == RoundValToInt(sum / 2))
        {
            trueInfo.Add("even");
            falseInfo.Add("odd");
        }
        else
        {
            trueInfo.Add("odd");
            falseInfo.Add("even");
        }

        // Next, greater or less than village size
        Il2CppSystem.Collections.Generic.List<int> villageSizeCheckerInts = new Il2CppSystem.Collections.Generic.List<int>();
        if (Gameplay.CurrentCharacters.Count + 2 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count + 2);
        if (Gameplay.CurrentCharacters.Count + 1 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count + 1);
        if (Gameplay.CurrentCharacters.Count != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count);
        if (Gameplay.CurrentCharacters.Count - 1 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count - 1);
        if (Gameplay.CurrentCharacters.Count - 2 != sum) villageSizeCheckerInts.Add(Gameplay.CurrentCharacters.Count - 2);
        int villageSizeChecker = sharedScripts.GetRandomItemOfList(villageSizeCheckerInts);
        if (sum < villageSizeChecker)
        {
            trueInfo.Add($"less than {villageSizeChecker}");
            falseInfo.Add($"greater than {villageSizeChecker}");
        }
        else
        {
            trueInfo.Add($"greater than {villageSizeChecker}");
            falseInfo.Add($"less than {villageSizeChecker}");
        }

        // Next, check if prime
        if (chkprime(sum)) trueInfo.Add("prime");
        else if (!falseInfo.Contains("even")) falseInfo.Add("prime");

        // Check if greater or less than charRef number, but only if it's less than the size of the village
        if (!falseInfo.Contains($"greater than {villageSizeChecker}"))
        {
            if (charRef.id < sum)
            {
                trueInfo.Add("greater than my number");
                falseInfo.Add("less than my number");
            }
            if (charRef.id > sum)
            {
                trueInfo.Add("less than my number");
                falseInfo.Add("greater than my number");
            }
        }

        // Check if divisible by 3
        if (sum / 3 == RoundValToInt(sum / 3))
        {
            trueInfo.Add("divisible by 3");
            falseInfo.Add("not divisible by 3");
        }
        else
        {
            trueInfo.Add("not divisible by 3");
            falseInfo.Add("divisible by 3");
        }

        string infoOne = sharedScripts.GetRandomItemOfList(falseInfo);
        falseInfo.Remove(infoOne);
        string infoTwo = sharedScripts.GetRandomItemOfList(falseInfo);

        string evilNameOne = evilOne.GetRegisterAs().characterName;
        string evilNameTwo = evilTwo.GetRegisterAs().characterName;

        string info = $"The sum of the positions of {evilNameOne} and {evilNameTwo} is {infoOne} and {infoTwo}";
        return new ActedInfo(info);
    }
    public override string Description
    {
        get
        {
            return "Learn something about the sum of two Evil characters";
        }
    }

    public static bool chkprime(int num) // Code from https://www.w3resource.com/csharp-exercises/function/csharp-function-exercise-9.php, genuine legends there
    {
        // Loop to check for factors of the number
        for (int i = 2; i < num; i++)
        {
            if (num % i == 0)
            {
                return false; // If any factor found, the number is not prime, return false
            }
        }
        return true; // If no factors found, the number is prime, return true
    }
    public static int RoundValToInt(decimal val)
    {
        return (int)Math.Round(val);
    }
    public string ConjourInfo(string charName, int steps)
    {
        return "";
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
    public int GetDistanceBetweenCharacters(Character char1, Character char2, int totalCharCount)
    {
        UnityEngine.Debug.Log(string.Format("Grabbing distance from #{0} to #{1}", char1.id, char2.id));
        int tempDist = char1.id - char2.id;
        UnityEngine.Debug.Log(string.Format("Outcome is {0}", tempDist));
        if (tempDist < 0)
        {
            UnityEngine.Debug.Log(string.Format("Number is negative, adding {0}", totalCharCount));
            tempDist = tempDist + totalCharCount;
        }
        return tempDist;
    }
    public w_Statistician() : base(ClassInjector.DerivedConstructorPointer<w_Statistician>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Statistician(IntPtr ptr) : base(ptr)
    {
    }
}


