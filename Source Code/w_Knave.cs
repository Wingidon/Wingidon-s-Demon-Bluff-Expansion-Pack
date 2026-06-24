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
        ActedInfo checkedInfo = CheckIfUniqueCharacter(charRef);
        if (checkedInfo.desc != "False") return checkedInfo;
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        ActedInfo trueInfo = sharedScripts.GetRandomInfo(charRef, false, false, false);
        ActedInfo trueInfoTwo = sharedScripts.GetRandomInfo(charRef, false, false, false);
        ActedInfo falseInfo = sharedScripts.GetRandomInfo(charRef, true, false, false);

        if (trueInfo == trueInfoTwo)
        {
            while (trueInfo == trueInfoTwo)
            {
                MelonLogger.Msg("Not good enough, let's try again");
                trueInfoTwo = sharedScripts.GetRandomInfo(charRef, false, false, false);
            }
        }

        string line = "";
        Il2CppSystem.Collections.Generic.List<ActedInfo> infos = new Il2CppSystem.Collections.Generic.List<ActedInfo>();
        infos.Add(trueInfo);
        infos.Add(trueInfoTwo);
        infos.Add(falseInfo);

        Il2CppSystem.Collections.Generic.List<ActedInfo> randomisedOrderInfos = new Il2CppSystem.Collections.Generic.List<ActedInfo>();
        randomisedOrderInfos.Add(infos[UnityEngine.Random.RandomRangeInt(0, infos.Count)]);
        infos.Remove(randomisedOrderInfos[0]);
        randomisedOrderInfos.Add(infos[UnityEngine.Random.RandomRangeInt(0, infos.Count)]);
        infos.Remove(randomisedOrderInfos[1]);
        randomisedOrderInfos.Add(infos[0]);

        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in trueInfo.characters) selection.Add(character);
        foreach (Character character in trueInfoTwo.characters) selection.Add(character);
        foreach (Character character in falseInfo.characters) selection.Add(character);

        line = $"{randomisedOrderInfos[0].desc}\n\n"
             + $"{randomisedOrderInfos[1].desc}\n\n"
             + $"{randomisedOrderInfos[2].desc}";

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        ActedInfo checkedInfo = CheckIfUniqueCharacter(charRef);
        if (checkedInfo.desc != "False") return checkedInfo;
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        ActedInfo infoOne = new ActedInfo("");
        ActedInfo infoTwo = new ActedInfo("");
        ActedInfo infoThree = new ActedInfo("");

        bool infoFalse = false;
        string line = "";
        if (sharedScripts.PercentChance(50))
        {
            infoFalse = true;
        }
        infoOne = sharedScripts.GetRandomInfo(charRef, infoFalse, false, false);
        infoTwo = sharedScripts.GetRandomInfo(charRef, infoFalse, false, false);
        if (infoOne == infoTwo)
        {
            while (infoOne == infoTwo)
            {
                MelonLogger.Msg("Not good enough, let's try again");
                infoTwo = sharedScripts.GetRandomInfo(charRef, infoFalse, false, false);
            }
        }
        infoThree = sharedScripts.GetRandomInfo(charRef, infoFalse, false, false);
        if (infoOne == infoThree || infoTwo == infoThree)
        {
            while (infoOne == infoThree || infoTwo == infoThree)
            {
                MelonLogger.Msg("Not good enough, let's try again");
                infoThree = sharedScripts.GetRandomInfo(charRef, infoFalse, false, false);
            }
        }
        line = $"{infoOne.desc}\n\n"
             + $"{infoTwo.desc}\n\n"
             + $"{infoThree.desc}";

        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in infoOne.characters) selection.Add(character);
        foreach (Character character in infoTwo.characters) selection.Add(character);
        foreach (Character character in infoThree.characters) selection.Add(character);

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


    public ActedInfo CheckIfUniqueCharacter(Character charRef) // Checks if the character is one that gives unique info
    {
        if (charRef.dataRef.characterId == "Captivator_scm") // Learns 2 false statements and a true statement
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            ActedInfo trueInfo = sharedScripts.GetRandomInfo(charRef, false, false, false);
            ActedInfo trueInfoTwo = sharedScripts.GetRandomInfo(charRef, true, false, false);
            ActedInfo falseInfo = sharedScripts.GetRandomInfo(charRef, true, false, false);

            if (falseInfo == trueInfoTwo)
            {
                while (falseInfo == trueInfoTwo)
                {
                    MelonLogger.Msg("Not good enough, let's try again");
                    trueInfoTwo = sharedScripts.GetRandomInfo(charRef, true, false, false);
                }
            }

            string line = "";
            Il2CppSystem.Collections.Generic.List<ActedInfo> infos = new Il2CppSystem.Collections.Generic.List<ActedInfo>();
            infos.Add(trueInfo);
            infos.Add(trueInfoTwo);
            infos.Add(falseInfo);

            Il2CppSystem.Collections.Generic.List<ActedInfo> randomisedOrderInfos = new Il2CppSystem.Collections.Generic.List<ActedInfo>();
            randomisedOrderInfos.Add(infos[UnityEngine.Random.RandomRangeInt(0, infos.Count)]);
            infos.Remove(randomisedOrderInfos[0]);
            randomisedOrderInfos.Add(infos[UnityEngine.Random.RandomRangeInt(0, infos.Count)]);
            infos.Remove(randomisedOrderInfos[1]);
            randomisedOrderInfos.Add(infos[0]);

            Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in trueInfo.characters) selection.Add(character);
            foreach (Character character in trueInfoTwo.characters) selection.Add(character);
            foreach (Character character in falseInfo.characters) selection.Add(character);

            line = $"{randomisedOrderInfos[0].desc}\n\n"
                 + $"{randomisedOrderInfos[1].desc}\n\n"
                 + $"{randomisedOrderInfos[2].desc}";

            ActedInfo actedInfo = new ActedInfo(line, selection);
            return actedInfo;
        }
        return new ActedInfo("False");
    }
}


