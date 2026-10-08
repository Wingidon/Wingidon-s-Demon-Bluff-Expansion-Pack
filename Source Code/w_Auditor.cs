using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Auditor : Role
{
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<string> possibleInfoTypes = GetInfoTypes();

        Il2CppSystem.Collections.Generic.List<string> chosenInfoTypes = new();
        chosenInfoTypes.Add(possibleInfoTypes[UnityEngine.Random.RandomRangeInt(0, possibleInfoTypes.Count)]);
        possibleInfoTypes = RemoveRelevantInfoTypes(possibleInfoTypes, chosenInfoTypes[0]);
        chosenInfoTypes.Add(possibleInfoTypes[UnityEngine.Random.RandomRangeInt(0, possibleInfoTypes.Count)]);
        possibleInfoTypes = RemoveRelevantInfoTypes(possibleInfoTypes, chosenInfoTypes[1]);
        chosenInfoTypes.Add(possibleInfoTypes[UnityEngine.Random.RandomRangeInt(0, possibleInfoTypes.Count)]);



        int goodChars = 0;
        int evilChars = 0;
        int corruptedChars = 0;
        int lyingChars = 0;
        int truthfulChars = 0;
        int disguisedChars = 0;
        int honestChars = 0;
        int villagerChars = 0;
        int nonVillagerChars = 0;
        int outcastChars = 0;
        int minionChars = 0;
        int demonChars = 0;

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Good) goodChars++;
            if (character.GetRegisterAlignment() == EAlignment.Evil) evilChars++;
            if (character.statuses.Contains(ECharacterStatus.Corrupted)) corruptedChars++;
            if (CharacterHelper.CheckLyingAppearance(character)) lyingChars++;
            else truthfulChars++;
            if (CharacterHelper.CheckIfDisguisedAppearance(character)) disguisedChars++;
            else honestChars++;
            if (character.GetRegisterAs().type == ECharacterType.Villager) villagerChars++;
            else nonVillagerChars++;
            if (character.GetRegisterAs().type == ECharacterType.Outcast) outcastChars++;
            if (character.GetRegisterAs().type == ECharacterType.Minion) minionChars++;
            if (character.GetRegisterAs().type == ECharacterType.Demon) demonChars++;
        }


        string line = "Among all characters, ";
        for (int i = 0; i < 2; i++)
        {
            string infoType = chosenInfoTypes[i];
            if (infoType == "Good") line += $"{FormatInfoType(infoType, goodChars)}";
            if (infoType == "Evil") line += $"{FormatInfoType(infoType, evilChars)}";
            if (infoType == "Corrupted") line += $"{FormatInfoType(infoType, corruptedChars)}";
            if (infoType == "Lying") line += $"{FormatInfoType(infoType, lyingChars)}";
            if (infoType == "Truthful") line += $"{FormatInfoType(infoType, truthfulChars)}";
            if (infoType == "Disguised") line += $"{FormatInfoType(infoType, disguisedChars)}";
            if (infoType == "Honest") line += $"{FormatInfoType(infoType, honestChars)}";
            if (infoType == "Villagers") line += $"{FormatInfoType(infoType, villagerChars)}";
            if (infoType == "Non-Villagers") line += $"{FormatInfoType(infoType, nonVillagerChars)}";
            if (infoType == "Outcasts") line += $"{FormatInfoType(infoType, outcastChars)}";
            if (infoType == "Minions") line += $"{FormatInfoType(infoType, minionChars)}";
            if (infoType == "Demons") line += $"{FormatInfoType(infoType, demonChars)}";
            line += ", ";
        }
        line += "and ";
        string finalInfoType = chosenInfoTypes[2];
        if (finalInfoType == "Good") line += $"{FormatInfoType(finalInfoType, goodChars)}";
        if (finalInfoType == "Evil") line += $"{FormatInfoType(finalInfoType, evilChars)}";
        if (finalInfoType == "Corrupted") line += $"{FormatInfoType(finalInfoType, corruptedChars)}";
        if (finalInfoType == "Lying") line += $"{FormatInfoType(finalInfoType, lyingChars)}";
        if (finalInfoType == "Truthful") line += $"{FormatInfoType(finalInfoType, truthfulChars)}";
        if (finalInfoType == "Disguised") line += $"{FormatInfoType(finalInfoType, disguisedChars)}";
        if (finalInfoType == "Honest") line += $"{FormatInfoType(finalInfoType, honestChars)}";
        if (finalInfoType == "Villagers") line += $"{FormatInfoType(finalInfoType, villagerChars)}";
        if (finalInfoType == "Non-Villagers") line += $"{FormatInfoType(finalInfoType, nonVillagerChars)}";
        if (finalInfoType == "Outcasts") line += $"{FormatInfoType(finalInfoType, outcastChars)}";
        if (finalInfoType == "Minions") line += $"{FormatInfoType(finalInfoType, minionChars)}";
        if (finalInfoType == "Demons") line += $"{FormatInfoType(finalInfoType, demonChars)}";



        ActedInfo actedInfo = new ActedInfo(line);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<string> possibleInfoTypes = GetInfoTypes();

        Il2CppSystem.Collections.Generic.List<string> chosenInfoTypes = new();
        chosenInfoTypes.Add(possibleInfoTypes[UnityEngine.Random.RandomRangeInt(0, possibleInfoTypes.Count)]);
        possibleInfoTypes = RemoveRelevantInfoTypes(possibleInfoTypes, chosenInfoTypes[0]);
        chosenInfoTypes.Add(possibleInfoTypes[UnityEngine.Random.RandomRangeInt(0, possibleInfoTypes.Count)]);
        possibleInfoTypes = RemoveRelevantInfoTypes(possibleInfoTypes, chosenInfoTypes[1]);
        chosenInfoTypes.Add(possibleInfoTypes[UnityEngine.Random.RandomRangeInt(0, possibleInfoTypes.Count)]);



        int goodChars = 0;
        int evilChars = 0;
        int corruptedChars = 0;
        int lyingChars = 0;
        int truthfulChars = 0;
        int disguisedChars = 0;
        int honestChars = 0;
        int villagerChars = 0;
        int nonVillagerChars = 0;
        int outcastChars = 0;
        int minionChars = 0;
        int demonChars = 0;

        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Good) goodChars++;
            if (character.GetRegisterAlignment() == EAlignment.Evil) evilChars++;
            if (character.statuses.Contains(ECharacterStatus.Corrupted)) corruptedChars++;
            if (CharacterHelper.CheckLyingAppearance(character)) lyingChars++;
            else truthfulChars++;
            if (CharacterHelper.CheckIfDisguisedAppearance(character)) disguisedChars++;
            else honestChars++;
            if (character.GetRegisterAs().type == ECharacterType.Villager) villagerChars++;
            else nonVillagerChars++;
            if (character.GetRegisterAs().type == ECharacterType.Outcast) outcastChars++;
            if (character.GetRegisterAs().type == ECharacterType.Minion) minionChars++;
            if (character.GetRegisterAs().type == ECharacterType.Demon) demonChars++;
        }



        wx_SavedScripts sharedScripts = new();
        goodChars = sharedScripts.MakeNumberWrongByRange(goodChars, goodChars, 1, Gameplay.CurrentCharacters.Count, 1, 1);
        evilChars = sharedScripts.MakeNumberWrongByRange(evilChars, evilChars, 1, Gameplay.CurrentCharacters.Count, 1, 1);
        corruptedChars = sharedScripts.MakeNumberWrongByRange(corruptedChars, corruptedChars, 0, Gameplay.CurrentCharacters.Count, 1, 1);
        lyingChars = sharedScripts.MakeNumberWrongByRange(lyingChars, lyingChars, 0, Gameplay.CurrentCharacters.Count, 2, 2);


        string line = "Among all characters, ";
        for (int i = 0; i < 2; i++)
        {
            string infoType = chosenInfoTypes[i];
            if (infoType == "Good") line += $"{FormatInfoType(infoType, goodChars)}";
            if (infoType == "Evil") line += $"{FormatInfoType(infoType, evilChars)}";
            if (infoType == "Corrupted") line += $"{FormatInfoType(infoType, corruptedChars)}";
            if (infoType == "Lying") line += $"{FormatInfoType(infoType, lyingChars)}";
            if (infoType == "Truthful") line += $"{FormatInfoType(infoType, truthfulChars)}";
            if (infoType == "Disguised") line += $"{FormatInfoType(infoType, disguisedChars)}";
            if (infoType == "Honest") line += $"{FormatInfoType(infoType, honestChars)}";
            if (infoType == "Villagers") line += $"{FormatInfoType(infoType, villagerChars)}";
            if (infoType == "Non-Villagers") line += $"{FormatInfoType(infoType, nonVillagerChars)}";
            if (infoType == "Outcasts") line += $"{FormatInfoType(infoType, outcastChars)}";
            if (infoType == "Minions") line += $"{FormatInfoType(infoType, minionChars)}";
            if (infoType == "Demons") line += $"{FormatInfoType(infoType, demonChars)}";
            line += ", ";
        }
        line += "and ";
        string finalInfoType = chosenInfoTypes[2];
        if (finalInfoType == "Good") line += $"{FormatInfoType(finalInfoType, goodChars)}";
        if (finalInfoType == "Evil") line += $"{FormatInfoType(finalInfoType, evilChars)}";
        if (finalInfoType == "Corrupted") line += $"{FormatInfoType(finalInfoType, corruptedChars)}";
        if (finalInfoType == "Lying") line += $"{FormatInfoType(finalInfoType, lyingChars)}";
        if (finalInfoType == "Truthful") line += $"{FormatInfoType(finalInfoType, truthfulChars)}";
        if (finalInfoType == "Disguised") line += $"{FormatInfoType(finalInfoType, disguisedChars)}";
        if (finalInfoType == "Honest") line += $"{FormatInfoType(finalInfoType, honestChars)}";
        if (finalInfoType == "Villagers") line += $"{FormatInfoType(finalInfoType, villagerChars)}";
        if (finalInfoType == "Non-Villagers") line += $"{FormatInfoType(finalInfoType, nonVillagerChars)}";
        if (finalInfoType == "Outcasts") line += $"{FormatInfoType(finalInfoType, outcastChars)}";
        if (finalInfoType == "Minions") line += $"{FormatInfoType(finalInfoType, minionChars)}";
        if (finalInfoType == "Demons") line += $"{FormatInfoType(finalInfoType, demonChars)}";



        ActedInfo actedInfo = new ActedInfo(line);
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
    private Il2CppSystem.Collections.Generic.List<string> GetInfoTypes()
    {
        Il2CppSystem.Collections.Generic.List<string> possibleInfoTypes = new();
        possibleInfoTypes.Add("Good");
        possibleInfoTypes.Add("Evil");
        possibleInfoTypes.Add("Corrupted");
        possibleInfoTypes.Add("Lying");
        possibleInfoTypes.Add("Truthful");
        possibleInfoTypes.Add("Disguised");
        possibleInfoTypes.Add("Honest");
        possibleInfoTypes.Add("Villagers");
        possibleInfoTypes.Add("Non-Villagers");
        possibleInfoTypes.Add("Outcasts");
        possibleInfoTypes.Add("Minions");
        possibleInfoTypes.Add("Demons");
        return possibleInfoTypes;
    }
    private Il2CppSystem.Collections.Generic.List<string> RemoveRelevantInfoTypes(Il2CppSystem.Collections.Generic.List<string> inputList, string infoType)
    {
        Il2CppSystem.Collections.Generic.List<string> infoTypes = inputList;

        Il2CppSystem.Collections.Generic.List<string> typeInfos = new();
        typeInfos.Add("Villagers");
        typeInfos.Add("Non-Villagers");
        typeInfos.Add("Outcasts");
        typeInfos.Add("Minions");
        typeInfos.Add("Demons");

        Il2CppSystem.Collections.Generic.List<string> alignmentInfos = new();
        alignmentInfos.Add("Good");
        alignmentInfos.Add("Evil");

        Il2CppSystem.Collections.Generic.List<string> lieInfos = new();
        lieInfos.Add("Truthful");
        lieInfos.Add("Lying");

        Il2CppSystem.Collections.Generic.List<string> disguiseInfos = new();
        disguiseInfos.Add("Honest");
        disguiseInfos.Add("Disguised");

        Il2CppSystem.Collections.Generic.List<string> statusInfos = new();
        statusInfos.Add("Corrupted");

        if (typeInfos.Contains(infoType)) foreach (string listInfoType in typeInfos) infoTypes.Remove(listInfoType);
        if (alignmentInfos.Contains(infoType)) foreach (string listInfoType in alignmentInfos) infoTypes.Remove(listInfoType);
        if (lieInfos.Contains(infoType)) foreach (string listInfoType in lieInfos) infoTypes.Remove(listInfoType);
        if (disguiseInfos.Contains(infoType)) foreach (string listInfoType in disguiseInfos) infoTypes.Remove(listInfoType);
        if (statusInfos.Contains(infoType)) foreach (string listInfoType in statusInfos) infoTypes.Remove(listInfoType);

        return infoTypes;
    }
    private string FormatNum(int num)
    {
        if (num == 0) return "NONE";
        return num.ToString();
    }
    private string FormatInfoType(string infoType, int number)
    {
        string subject = "";
        if (number == 1) subject += "only 1";
        else subject = FormatNum(number);

        /*
        possibleInfoTypes.Add("Good");
        possibleInfoTypes.Add("Evil");
        possibleInfoTypes.Add("Corrupted");
        possibleInfoTypes.Add("Lying");
        possibleInfoTypes.Add("Truthful");
        possibleInfoTypes.Add("Disguised");
        possibleInfoTypes.Add("Honest");
        possibleInfoTypes.Add("Villagers");
        possibleInfoTypes.Add("Non-Villagers");
        possibleInfoTypes.Add("Outcasts");
        possibleInfoTypes.Add("Minions");
        possibleInfoTypes.Add("Demons");
        */
        if (infoType == "Good") subject += " is Good";
        if (infoType == "Evil") subject += " is Evil";
        if (infoType == "Corrupted") subject += " is Corrupted";
        if (infoType == "Lying") subject += " is Lying";
        if (infoType == "Truthful") subject += " is Truthful";
        if (infoType == "Disguised") subject += " is Disguised";
        if (infoType == "Honest") subject += " is Honest";
        if (infoType == "Villager") subject += " is a Villager";
        if (infoType == "Non-Villager") subject += " is a non-Villager";
        if (infoType == "Outcast") subject += " is an Outcast";
        if (infoType == "Minion") subject += " is a Minion";
        if (infoType == "Demon") subject += " is a Demon";

        if (number != 1) subject += "s";

        return subject;

    }
    public w_Auditor() : base(ClassInjector.DerivedConstructorPointer<w_Auditor>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Auditor(IntPtr ptr) : base(ptr)
    {
    }
}


