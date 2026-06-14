using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using Il2CppTMPro;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Globalization.CultureInfo;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Heretic : Minion
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Start)
        {
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
            Il2CppSystem.Collections.Generic.List<CharacterData> fakeOutcastOptions = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<CharacterData> fakeMinionOptions = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<CharacterData> fakeDemonOptions = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<CharacterData> allDemons = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<CharacterData> nightDemons = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // Demons that put a night cycle in-play
            Il2CppSystem.Collections.Generic.List<CharacterData> setupDemons = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // Demons that have a setup ability or are otherwise very loud
            Il2CppSystem.Collections.Generic.List<CharacterData> supportDemons = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // Demons that don't fit the other categories
            Il2CppSystem.Collections.Generic.List<CharacterData> inPlayCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<string> blacklistMinionIDs = new Il2CppSystem.Collections.Generic.List<string>();
            Il2CppSystem.Collections.Generic.List<string> nightDemonIDs = new Il2CppSystem.Collections.Generic.List<string>();
            Il2CppSystem.Collections.Generic.List<string> setupDemonIDs = new Il2CppSystem.Collections.Generic.List<string>();
            Il2CppSystem.Collections.Generic.List<string> supportDemonIDs = new Il2CppSystem.Collections.Generic.List<string>();

            //Let's define the IDs in each list
            blacklistMinionIDs.Add("Puppet_15989619"); // Puppet is never in the Deck to begin with.
            blacklistMinionIDs.Add("Swarm_Good_WING"); // Swarm adding its counterpart to the Deck makes it far too obvious
            blacklistMinionIDs.Add("Swarm_Evil_WING"); // Swarm adding its counterpart to the Deck makes it far too obvious.
            blacklistMinionIDs.Add("Trickster_m_scm"); // Just in case.
            blacklistMinionIDs.Add("Trickster_m_register_scm"); // Just in case.
            blacklistMinionIDs.Add("Undying_WING"); // Undying is face-up. Don't add him as a fake Minion.
            blacklistMinionIDs.Add("Marionette_11628408"); // That's the wrong Marionette.
            blacklistMinionIDs.Add("Werewolf_78350415"); // Werewolf is never in the Deck to begin with.
            blacklistMinionIDs.Add("Wretch_Evil_91222191"); // That's the wrong Wretch.

            supportDemonIDs.Add("Baatender_ER"); // Baatender (ExtraRandomized)
            supportDemonIDs.Add("BeerThrower_TAVERN"); // Beer Thrower (Tavern Mode)
            supportDemonIDs.Add("Belias_EP"); // Belias (CSK Expansion Pack)
            supportDemonIDs.Add("Better Baa_ER"); // Better Baa (ExtraRandomized)
            supportDemonIDs.Add("Cackler_MaHy"); // Cakler (Mass Hysteria)
            supportDemonIDs.Add("Escapist_scm"); // Escapist (Skill Cycler's Riddles)
            supportDemonIDs.Add("Hypnotist_ER"); // Hypnotist (ExtraRandomized)
            supportDemonIDs.Add("Iris_WING"); // Iris (Wingidon's Expansion Pack)
            supportDemonIDs.Add("Kingmaker_scm"); // Kingmaker (Skill Cycler's Riddles)
            supportDemonIDs.Add("Mendaverte_WING"); // Mendaverte (Wingidon's Expansion Pack)
            supportDemonIDs.Add("Mezepheles_WING"); // Venelum (Wingidon's Expansion Pack)
            // supportDemonIDs.Add("Mutant_84675843"); // Mutant (Vanilla, Unused)
            supportDemonIDs.Add("Phantom_VP"); // Phantom (CarlzVillagePack)
            supportDemonIDs.Add("Pooka_13445289"); // Pooka (Vanilla)
            // supportDemonIDs.Add("Praesect_WING"); // Praesect (Wingidon's Expansion Pack) - The Acolytes and Zealots it add make it too obvious
            supportDemonIDs.Add("Shard_WING"); // Specularus (Wingidon's Expansion Pack)
            supportDemonIDs.Add("SoulCollector_RCol"); // Soul Collector (Roles Collection) - Could plausibly fit in as something else ig
            // supportDemonIDs.Add("TwinDemon_WING"); // Veniyon (Wingidon's Expansion Pack) - Never adding the triplets because they're too obvious
            // supportDemonIDs.Add("TwinDemonTwin_WING"); // Vidiyon (Wingidon's Expansion Pack)
            // supportDemonIDs.Add("TwinDemonTriplet_WING"); // Viciyon (Wingidon's Expansion Pack)
            supportDemonIDs.Add("Veil_scm"); // Veil (Skill Cycler's Riddles) - Its cover won't last long but it's still not obvious until you reveal *everything* that you can reveal.

            nightDemonIDs.Add("Caedoccidere_WING"); // Caedoccidere (Wingidon's Expansion Pack)
            nightDemonIDs.Add("Carnicarius_WING"); // Carnicarius (Wingidon's Expansion Pack)
            nightDemonIDs.Add("Follower_scm"); // Follower (Skill Cycler's Riddles) for all of the four seconds its cover is going to last.
            nightDemonIDs.Add("Infestation_scm"); // Infestation (Skill Cycler's Riddles)
            nightDemonIDs.Add("Lillith_90453844"); // Lilis (Vanilla)
            setupDemonIDs.Add("Minos_WING"); // Sanguitaurus (Wingidon's Expansion Pack)

            setupDemonIDs.Add("Illusionist_WING"); // Emenverax (Wingidon's Expansion Pack)
            setupDemonIDs.Add("Imp_58992273"); // Baa (Vanilla) - Here because it's kind of difficult to miss a card hidden in the deck. Might split into his own category later.
            setupDemonIDs.Add("Legion_WING"); // Agmeres (Wingidon's Expansion Pack)
            setupDemonIDs.Add("Leviathan_WING"); // Leviathan (Wingidon's Expansion Pack)
            setupDemonIDs.Add("Pandemonium_WING"); // Magnere (Wingidon's Expansion Pack)
            setupDemonIDs.Add("Pestilence_VP"); // Pestilence (CarlzVillagePack), because it's kind of difficult to miss everyone's abilities refreshing.
            setupDemonIDs.Add("shroud_rdm"); // Shroud (Reveal Dilemma) - blows its cover as soon as you reveal the first card.
            setupDemonIDs.Add("Summoner_scm"); // Summoner (Skill Cycler's Riddles)












            // Don't make Praesect, Veniyon, Vidiyon or Viciyon obvious.
            CharacterData acolyteData = charRef.dataRef;
            CharacterData zealotData = charRef.dataRef;
            CharacterData veniData = charRef.dataRef;
            CharacterData vidiData = charRef.dataRef;
            CharacterData viciData = charRef.dataRef;


            foreach (Character character in Gameplay.CurrentCharacters)
            {
                inPlayCharacters.Add(character.dataRef);
            }
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion))
            {
                inPlayCharacters.Add(character);
            }
            foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Demon))
            {
                inPlayCharacters.Add(character);
            }
            for (int j = 0; j < allDatas.Length; j++)
            {
                if (allDatas[j].type == ECharacterType.Outcast)
                {
                    if (!inPlayCharacters.Contains(allDatas[j])) fakeOutcastOptions.Add(allDatas[j]);
                }
                if (allDatas[j].type == ECharacterType.Minion && !blacklistMinionIDs.Contains(allDatas[j].characterId))
                {
                    if (!inPlayCharacters.Contains(allDatas[j])) fakeMinionOptions.Add(allDatas[j]);
                    if (allDatas[j].characterId == "Acolyte_WING") acolyteData = allDatas[j];
                    if (allDatas[j].characterId == "Zealot_WING") zealotData = allDatas[j];
                    if (allDatas[j].characterId == "TwinDemon_WING") veniData = allDatas[j];
                    if (allDatas[j].characterId == "TwinDemonTwin_WING") vidiData = allDatas[j];
                    if (allDatas[j].characterId == "TwinDemonTriplet_WING") viciData = allDatas[j];
                }
                if (allDatas[j].type == ECharacterType.Demon)
                {
                    if (!inPlayCharacters.Contains(allDatas[j]) && allDatas[j].name != "Delusion")
                    {
                        allDemons.Add(allDatas[j]);
                        if (nightDemonIDs.Contains(allDatas[j].characterId)) nightDemons.Add(allDatas[j]);
                        if (setupDemonIDs.Contains(allDatas[j].characterId)) setupDemons.Add(allDatas[j]);
                        if (supportDemonIDs.Contains(allDatas[j].characterId)) supportDemons.Add(allDatas[j]);
                    }
                }
            }

            // Now check what types of Demon are in-play
            bool foundSetup = false;
            bool foundSupport = false;
            bool foundNight = false;
            foreach (CharacterData character in inPlayCharacters)
            {
                if (setupDemonIDs.Contains(character.characterId) && !foundSetup)
                {
                    foundSetup = true;
                    foreach (CharacterData character2 in setupDemons)
                    {
                        fakeDemonOptions.Add(character2);
                    }
                }
                if (nightDemonIDs.Contains(character.characterId) && !foundNight)
                {
                    foundNight = true;
                    foreach (CharacterData character2 in nightDemons)
                    {
                        fakeDemonOptions.Add(character2);
                    }
                }
                if (supportDemonIDs.Contains(character.characterId) && !foundSupport)
                {
                    foundSupport = true;
                    foreach (CharacterData character2 in supportDemons)
                    {
                        fakeDemonOptions.Add(character2);
                    }
                }
            }

            if (!foundSetup && !foundNight && !foundSupport)
            {
                foundSupport = true;
                foreach (CharacterData character in supportDemons)
                {
                    fakeDemonOptions.Add(character);
                }
            }

            CharacterData chosenOutcast = charRef.dataRef;
            CharacterData chosenMinion = charRef.dataRef;
            CharacterData chosenDemon = charRef.dataRef;

            chosenOutcast = fakeOutcastOptions[UnityEngine.Random.RandomRangeInt(0, fakeOutcastOptions.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Outcast, chosenOutcast);
            //fakeOutcastOptions.Remove(chosenOutcast);
            //chosenOutcast = fakeOutcastOptions[UnityEngine.Random.RandomRangeInt(0, fakeOutcastOptions.Count)];
            //Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Outcast, chosenOutcast);

            chosenMinion = fakeMinionOptions[UnityEngine.Random.RandomRangeInt(0, fakeMinionOptions.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, chosenMinion);
            fakeMinionOptions.Remove(chosenMinion);
            chosenMinion = fakeMinionOptions[UnityEngine.Random.RandomRangeInt(0, fakeMinionOptions.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, chosenMinion);

            if (fakeDemonOptions.Count != 0)
            {
                chosenDemon = fakeDemonOptions[UnityEngine.Random.RandomRangeInt(0, fakeDemonOptions.Count)];
                Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, chosenDemon);
                fakeDemonOptions.Remove(chosenDemon);
                allDemons.Remove(chosenDemon);
            }
            else
            {
                chosenDemon = fakeDemonOptions[UnityEngine.Random.RandomRangeInt(0, fakeDemonOptions.Count)];
                Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, chosenDemon);
                fakeDemonOptions.Remove(chosenDemon);
                allDemons.Remove(chosenDemon);
            }

            if (fakeDemonOptions.Count != 0)
            {
                chosenDemon = fakeDemonOptions[UnityEngine.Random.RandomRangeInt(0, fakeDemonOptions.Count)];
                Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, chosenDemon);
                fakeDemonOptions.Remove(chosenDemon);
                allDemons.Remove(chosenDemon);
                if (chosenDemon.characterId == "Praesect_WING")
                {
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, acolyteData);
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, zealotData);
                }
                if (chosenDemon.characterId == "TwinDemon_WING" || chosenDemon.characterId == "TwinDemonTwin_WING" || chosenDemon.characterId == "TwinDemonTriplet_WING")
                {
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, veniData);
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, vidiData);
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, viciData);
                }
            }
            else
            {
                chosenDemon = allDemons[UnityEngine.Random.RandomRangeInt(0, allDemons.Count)];
                Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, chosenDemon);
                fakeDemonOptions.Remove(chosenDemon);
                allDemons.Remove(chosenDemon);
                if (chosenDemon.characterId == "Praesect_WING")
                {
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, acolyteData);
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, zealotData);
                }
                if (chosenDemon.characterId == "TwinDemon_WING" || chosenDemon.characterId == "TwinDemonTwin_WING" || chosenDemon.characterId == "TwinDemonTriplet_WING")
                {
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, veniData);
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, vidiData);
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, viciData);
                }
            }
        }
    }
    public w_Heretic() : base(ClassInjector.DerivedConstructorPointer<w_Heretic>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Heretic(System.IntPtr ptr) : base(ptr)
    {

    }
}


