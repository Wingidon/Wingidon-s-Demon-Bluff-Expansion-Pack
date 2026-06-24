using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Politician : Role
{
    EAlignment myStartingAlignment = EAlignment.Good;
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();

        bool infoShouldBeFalse = (charRef.alignment == EAlignment.Good);

        ActedInfo falseInfoOne = sharedScripts.GetRandomInfo(charRef, infoShouldBeFalse, false, false);
        ActedInfo falseInfoTwo = new ActedInfo("");
        falseInfoTwo = sharedScripts.GetRandomInfo(charRef, infoShouldBeFalse, false, false);
        if (falseInfoOne.desc == falseInfoTwo.desc)
        {
            while (falseInfoOne.desc == falseInfoTwo.desc)
            {
                MelonLogger.Msg("Not good enough, let's try that again");
                falseInfoTwo = sharedScripts.GetRandomInfo(charRef, true, false, false);
            }
        }

        Il2CppSystem.Collections.Generic.List<Character> realSelection = new Il2CppSystem.Collections.Generic.List<Character>();
        if (falseInfoOne.characters.Count != 0)
        {
            foreach (Character character in falseInfoOne.characters)
            {
                realSelection.Add(character);
            }
        }
        if (falseInfoTwo.characters.Count != 0)
        {
            foreach (Character character in falseInfoTwo.characters)
            {
                realSelection.Add(character);
            }
        }

        string realInfo = $"{falseInfoOne.desc}\n\n{falseInfoTwo.desc}";
        return new ActedInfo(realInfo, realSelection);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetInfo(charRef);
    }
    public override string Description
    {
        get
        {
            return "I am Corrupted and I Learn random false info.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            myStartingAlignment = charRef.dataRef.startingAlignment;
        }
        if (charRef.alignment != myStartingAlignment) charRef.ChangeAlignment(myStartingAlignment);
        if (trigger == ETriggerPhase.Start)
        {
            if (charRef.alignment == EAlignment.Good)
            {
                charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.AppearLying, charRef);
            }
            if (charRef.alignment == EAlignment.Evil)
            {
                charRef.statuses.statuses.Remove(ECharacterStatus.Corrupted);
            }
        }
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            if (charRef.alignment == EAlignment.Evil) charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
            else
            {
                charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                charRef.statuses.AddStatus(ECharacterStatus.AppearLying, charRef);
            }
            if (charRef.alignment == EAlignment.Evil)
            {
                charRef.statuses.statuses.Remove(ECharacterStatus.Corrupted);
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            onActed.Invoke(GetInfo(charRef));
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        Act(trigger, charRef);
    }
    public override bool CheckIfCanRemoveStatus(ECharacterStatus status)
    {
        if (status == ECharacterStatus.Corrupted) return false;
        return true;
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        return null;
    }
    public w_Politician() : base(ClassInjector.DerivedConstructorPointer<w_Politician>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Politician(IntPtr ptr) : base(ptr)
    {
    }
}


