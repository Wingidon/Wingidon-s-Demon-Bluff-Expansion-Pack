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

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Tenecaligo : Demon
{
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(4));
        return sr;
    }
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public Il2CppSystem.Collections.Generic.List<Il2Cpp.CharacterData> scriptCharacters = Gameplay.Instance.GetScriptCharacters();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
            Il2CppSystem.Collections.Generic.List<string> possibleOutcasts = new Il2CppSystem.Collections.Generic.List<string>(); // What Outcasts can Tenecaligo add? (By ID)
            Il2CppSystem.Collections.Generic.List<string> possibleMinions = new Il2CppSystem.Collections.Generic.List<string>(); // What Minions can Tenecaligo add? (By ID)
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleOutcastDatas = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // What Outcasts can Tenecaligo add? (Datas
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleMinionDatas = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // What Minions can Tenecaligo add? (Datas)
            Il2CppSystem.Collections.Generic.List<CharacterData> chosenConversions = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // What will be in-play?
            Il2CppSystem.Collections.Generic.List<int> counts = new Il2CppSystem.Collections.Generic.List<int>(); // counts[0] = Outcast count, counts[1] = Minion count.

            counts = getOutcastMinionCount();
            int outcastCount = counts[0];
            UnityEngine.Debug.Log($"Chose Outcast count of {outcastCount}.");
            int minionCount = counts[1];
            UnityEngine.Debug.Log($"Chose Minion count of {minionCount}.");

            possibleOutcasts = getWhitelistCharacters(ECharacterType.Outcast);
            possibleMinions = getWhitelistCharacters(ECharacterType.Minion);


            if (allDatas.Length == 0)
            {
                var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
                if (loadedCharList != null)
                {
                    allDatas = new CharacterData[loadedCharList.Length];
                    for (int j = 0; j < loadedCharList.Length; j++)
                    {
                        allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                    }
                }
            }

            for (int j = 0; j < allDatas.Length; j++)
            {
                if (possibleOutcasts.Contains(allDatas[j].characterId))
                {
                    possibleOutcastDatas.Add(allDatas[j]);
                }
                if (possibleMinions.Contains(allDatas[j].characterId))
                {
                    possibleMinionDatas.Add(allDatas[j]);
                }
            }

            CharacterData chosenConversion = new CharacterData();
            Il2CppSystem.Collections.Generic.List<Character> conversionTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.dataRef.type == ECharacterType.Villager)
                {
                    conversionTargets.Add(character);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                chosenConversion = possibleOutcastDatas[UnityEngine.Random.RandomRangeInt(0, possibleOutcastDatas.Count)];
                possibleOutcastDatas.Remove(chosenConversion);
                Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Outcast, chosenConversion);
                if (i < outcastCount)
                {
                    Character conversionTarget = conversionTargets[UnityEngine.Random.RandomRangeInt(0, conversionTargets.Count)];
                    conversionTarget.Init(chosenConversion);
                    conversionTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                    conversionTargets.Remove(conversionTarget);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                chosenConversion = possibleMinionDatas[UnityEngine.Random.RandomRangeInt(0, possibleMinionDatas.Count)];
                possibleMinionDatas.Remove(chosenConversion);
                Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, chosenConversion);
                if (i < minionCount)
                {
                    Character conversionTarget = conversionTargets[UnityEngine.Random.RandomRangeInt(0, conversionTargets.Count)];
                    conversionTarget.Init(chosenConversion);
                    conversionTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                    conversionTargets.Remove(conversionTarget);
                }
            }
        }
    }
    public Il2CppSystem.Collections.Generic.List<string> getWhitelistCharacters(ECharacterType type)
    {
        Il2CppSystem.Collections.Generic.List<string> returnList = new Il2CppSystem.Collections.Generic.List<string>();
        if (type == ECharacterType.Outcast)
        {
            // Allow, Basegame: Bombardier, Doppelganger, Drunk, Plague Doctor, Wretch
            returnList.Add("Bombardier_79093372"); // Bombardier
            returnList.Add("Doppleganger_52694042"); // Doppelganger
            returnList.Add("Drunk_15369527"); //  Drunk
            returnList.Add("Plague Doctor_49312486"); // Plague Doctor
            returnList.Add("Wretch_80988916"); // Wretch
            // Allow, Wingidon's Expansion: Chatterbox, Lunatic, Marionette, Mutant, Pilgrim, Revolutionary, Renegade
            returnList.Add("Chatterbox_WING"); // Chatterbox
            returnList.Add("Lunatic_WING"); // Lunatic
            returnList.Add("Marionette_WING"); // Marionette
            returnList.Add("Mutant_WING"); // Mutant
            returnList.Add("Pilgrim_WING"); // Pilgrim
            returnList.Add("Revolutionary_WING"); // Revolutionary
            returnList.Add("Renegade_WING"); // Renegade
            // Allow, Reveal Dilemma: Saboteur
            returnList.Add("sabo_rdm"); // Saboteur
            // Allow, Mass Hysteria: Magician, Pixie
            returnList.Add("Magician_MaHy"); // Magician
            returnList.Add("Pixie_MaHy"); // Pixie
            // Allow, CarlzVilliagePack: Amnesiac, Good Twin, Mayor, Moonchild, Rook, Executioner
            returnList.Add("Amnesiac_VP"); // Amnesiac
            returnList.Add("GoodTwin_VP"); // Good Twin
            returnList.Add("Mayor_VP"); // Mayor
            returnList.Add("MoonChild_VP"); // Moonchild
            returnList.Add("Rook_VP"); // Rook
            returnList.Add("Executioner_VP"); // Executioner
        }
        if (type == ECharacterType.Minion)
        {
            // Allow, Basegame: Chancellor, Minion, Poisoner, Puppeteer, Shaman, Twin Minion, Witch
            returnList.Add("Baron_04539999"); // Chancellor
            returnList.Add("Minion_71804875"); // Minion
            returnList.Add("Poisoner_64796285"); // Poisoner
            returnList.Add("Mezepheles_09511163"); // Puppeteer
            returnList.Add("Shaman_26945607"); // Shaman
            returnList.Add("Twin Minion_15695218"); // Twin Minion
            returnList.Add("Witch_25286521"); // Witch
            // Allow, Wingidon's Expansion: Swarm (Good), Professional, Saboteur, Turncoat, Undying
            returnList.Add("Swarm_Good_WING"); // Good Swarm
            returnList.Add("Professional_WING"); // Professional
            returnList.Add("Saboteur_WING"); // Saboteur
            returnList.Add("Turncoat_WING"); // Turncoat
            returnList.Add("Undying_WING"); // Undying
            // Allow, Tavern Mod: Florist, Gangster, Strategist, Trickster
            returnList.Add("Florist_TAVERN"); // Florist
            returnList.Add("Gangster_TAVERN"); // Gangster
            returnList.Add("Strategist_TAVERN"); // Strategist
            returnList.Add("Trickster_TAVERN"); // Trickster
            // Allow, Reveal Dilemma: Ambusher, Martyr
            returnList.Add("Ambush_rdm"); // Ambusher
            returnList.Add("Martyr_rdm"); // Martyr
            // Allow, Mass Hysteria: Siren
            returnList.Add("Siren_MaHy"); // Siren
            // Allow, ExtraRandomized: Purifier
            returnList.Add("Purifier_ER"); // Purifier
            // Allow, CarlzVilliagePack: Blackmailer, Lycaon
            returnList.Add("Husher_VP"); // Blackmailer
            returnList.Add("Lycaon_VP"); // Lycaon
            // Allow, CSK's Expansion: Cavalier
            returnList.Add("Cavalier_EP"); // Cavalier
        }
        return returnList;
    }
    public Il2CppSystem.Collections.Generic.List<int> getOutcastMinionCount()
    {
        Il2CppSystem.Collections.Generic.List<int> outcastCounts = new Il2CppSystem.Collections.Generic.List<int>();
        Il2CppSystem.Collections.Generic.List<int> minionCounts = new Il2CppSystem.Collections.Generic.List<int>();
        Il2CppSystem.Collections.Generic.List<int> returnList = new Il2CppSystem.Collections.Generic.List<int>();

        /*
        I consulted Wheel of Names.
        4/0 has 4 groups. (7%)
        3/1 has 9 groups. (17%)
        2/2 has 21 groups. (39%)
        1/3 has 14 groups. (26%)
        0/4 has 6 groups. (11%)
        */

        // 4 Outcasts, 0 Minions. This should be the least common.
        for (int i = 0; i < 4; i++)
        {
            outcastCounts.Add(4);
            minionCounts.Add(0);
        }

        // 3 Outcasts, 1 Minion. This should be fairly uncommon.
        for (int i = 0; i < 9; i++)
        {
            outcastCounts.Add(3);
            minionCounts.Add(1);
        }

        // 2 Outcasts, 2 Minions. This should be the most likely.
        for (int i = 0; i < 21; i++)
        {
            outcastCounts.Add(2);
            minionCounts.Add(2);
        }

        // 1 Outcast, 3 Minions. This should be relatively common.
        for (int i = 0; i < 14; i++)
        {
            outcastCounts.Add(1);
            minionCounts.Add(3);
        }

        // 0 Outcasts, 4 Minions. This should be the second-least common.
        for (int i = 0; i < 6; i++)
        {
            outcastCounts.Add(0);
            minionCounts.Add(4);
        }

        int choice = UnityEngine.Random.RandomRangeInt(0, outcastCounts.Count);
        returnList.Add(outcastCounts[choice]);
        returnList.Add(minionCounts[choice]);
        return returnList;
    }
    public w_Tenecaligo() : base(ClassInjector.DerivedConstructorPointer<w_Tenecaligo>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Tenecaligo(System.IntPtr ptr) : base(ptr)
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


