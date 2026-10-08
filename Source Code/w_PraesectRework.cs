using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Globalization.CultureInfo;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_PraesectRework : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public Il2CppSystem.Collections.Generic.List<Il2Cpp.CharacterData> scriptCharacters = Gameplay.Instance.GetScriptCharacters();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new();
            CharacterData acolyteData = sharedScripts.GrabCharacterDataByID("Acolyte_WING");
            CharacterData zealotData = sharedScripts.GrabCharacterDataByID("Zealot_WING");
            Il2CppSystem.Collections.Generic.List<Character> goodVillagers = Characters.Instance.FilterAlignmentCharacters(Gameplay.CurrentCharacters, EAlignment.Good);
            goodVillagers = Characters.Instance.FilterCharacterType(Gameplay.CurrentCharacters, ECharacterType.Villager);
            Character acolyteTarget = goodVillagers[UnityEngine.Random.RandomRangeInt(0, goodVillagers.Count)];
            goodVillagers.Remove(acolyteTarget);
            Character zealotTarget = goodVillagers[UnityEngine.Random.RandomRangeInt(0, goodVillagers.Count)];
            sharedScripts.DebugMessage($"Creating Acolyte at #{acolyteTarget.id} and Zealot at #{zealotTarget.id}");
            acolyteTarget.Init(acolyteData);
            acolyteTarget.RefreshCharacter();
            zealotTarget.Init(zealotData);
            zealotTarget.RefreshCharacter();


            // Now, grab closest Minions
            Il2CppSystem.Collections.Generic.List<Character> minions = Characters.Instance.FilterRealCharacterType(Gameplay.CurrentCharacters, ECharacterType.Minion);
            Il2CppSystem.Collections.Generic.List<Character> misregisterers = new();
            foreach (Character character in minions)
            {
                if (character.dataRef.characterId == "Professional_WING")
                {
                    misregisterers.Add(character);
                    sharedScripts.DebugMessage($"Found Professional at #{character.id}, ignoring...");
                }
            }

            if (misregisterers.Count != 0) foreach (Character character in misregisterers) minions.Remove(character);

            if (minions.Count == 0)
            {
                sharedScripts.DebugMessage($"No valid targets! How did this happen, shouldn't there be an Acolyte & a Zealot?");
            }

            int dist = 100;
            foreach (Character character in minions)
            {
                int checkDist = sharedScripts.GetDistanceBetweenCharacters(charRef, character);
                if (checkDist < dist) dist = checkDist;
            }

            Il2CppSystem.Collections.Generic.List<Character> students = Characters.Instance.GetCharactersAtRange(dist, charRef);
            sharedScripts.DebugMessage($"Closest Minions are {dist} card(s) away. This encompasses {sharedScripts.MentionEveryCharacterInList(students, "and")}.");
            foreach (Character character in students) if (character.GetRegisterAs().type == ECharacterType.Minion)
                {
                    character.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                    character.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                }
        }
    }
    public w_PraesectRework() : base(ClassInjector.DerivedConstructorPointer<w_PraesectRework>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_PraesectRework(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        return bluff;
    }
    }


