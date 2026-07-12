using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Shard : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> disguiseCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<CharacterData> allCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<string> jinxedCharacters = new Il2CppSystem.Collections.Generic.List<string>(); // The IDs of characters that are basically duds with Shard in-play.
            jinxedCharacters = GetJinxCharacterIDs();
            for (int j = 0; j < allDatas.Length; j++)
            {
                allCharacters.Add(allDatas[j]);
            }
            disguiseCharacters = AddLikelyCharacters(disguiseCharacters, allCharacters); // Makes the following characters more likely to appear: Bishop, Empress, Fortune Teller, Hunter, Jester, Lover, Oracle, Lookout, Cleric, Detective.
            foreach (CharacterData c in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Good))
            {
                if (!jinxedCharacters.Contains(c.characterId)) // Avoids jinxed characters being claimed.
                {
                    disguiseCharacters.Add(c);
                    disguiseCharacters.Add(c);
                    disguiseCharacters.Add(c); // Add it 3 times to ensure that there can be at most 3 of each character being claimed per village.
                }
            }
            disguiseCharacters = Characters.Instance.FilterBluffableCharacters(disguiseCharacters);
            disguiseCharacters = DupeExceptUnlikely(disguiseCharacters, allCharacters); // Makes the following characters less likely to appear: Alchemist, Bard, Dreamer, Druid, Poet, Plague Doctor, Sentinel, Tax Collector, Germaphobe, Therapist.
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character != charRef && character.dataRef.characterId != "Undying_WING") // Undying is safe from Specularus' ability.
                {
                    CharacterData disguise = disguiseCharacters[UnityEngine.Random.RandomRangeInt(0, disguiseCharacters.Count)];
                    disguiseCharacters.Remove(disguise); // Makes sure there can be at most 3 of each character being claimed per village.
                    if (character.dataRef == disguise) // Failsafe to make sure characters don't Disguise as themselves.
                    {
                        while (character.dataRef == disguise)
                        {
                            disguise = disguiseCharacters[UnityEngine.Random.RandomRangeInt(0, disguiseCharacters.Count)];
                            disguiseCharacters.Remove(disguise);
                        }
                    }
                    character.GiveBluff(disguise);
                    Debug.Log(string.Format("Gave bluff of {0} to #{1}, attempting to add it to Deck.", disguise.name.ToString(), character.id));
                    Gameplay.Instance.AddScriptCharacterIfAble(disguise.type, disguise);
                    if (charRef.alignment == EAlignment.Good)
                    {
                        character.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                    }
                    if (disguise != character.dataRef)
                    {
                        character.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
                    }
                }
            }
        }
    }
    public w_Shard() : base(ClassInjector.DerivedConstructorPointer<w_Shard>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Shard(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> disguiseCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        Il2CppSystem.Collections.Generic.List<CharacterData> allCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        Il2CppSystem.Collections.Generic.List<string> jinxedCharacters = new Il2CppSystem.Collections.Generic.List<string>(); // The IDs of characters that are basically duds with Shard in-play.
        jinxedCharacters = GetJinxCharacterIDs();
        for (int j = 0; j < allDatas.Length; j++)
        {
            allCharacters.Add(allDatas[j]);
        }
        disguiseCharacters = AddLikelyCharacters(disguiseCharacters, allCharacters); // Makes the following characters more likely to appear: Bishop, Empress, Fortune Teller, Hunter, Jester, Lover, Oracle, Lookout, Cleric, Detective.
        foreach (CharacterData c in Gameplay.Instance.GetScriptCharactersOfAlignment(EAlignment.Good))
        {
            if (!jinxedCharacters.Contains(c.characterId)) // Avoids jinxed characters being claimed.
            {
                disguiseCharacters.Add(c);
                disguiseCharacters.Add(c);
                disguiseCharacters.Add(c); // Add it 3 times to ensure that there can be at most 3 of each character being claimed per village.
            }
        }
        disguiseCharacters = Characters.Instance.FilterBluffableCharacters(disguiseCharacters);
        disguiseCharacters = DupeExceptUnlikely(disguiseCharacters, allCharacters); // Makes the following characters less likely to appear: Alchemist, Bard, Dreamer, Druid, Poet, Plague Doctor, Sentinel, Tax Collector, Germaphobe, Therapist.
        CharacterData bluff = disguiseCharacters[UnityEngine.Random.RandomRangeInt(0, disguiseCharacters.Count)];
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);

        return bluff;
    }
    public static void GetSelfCorruptionLogic(CharacterData charData, Character charRef)
    {
        if (charData.characterId == "Drunk_15369527")
        {
            charRef.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
        }
        if (charData.characterId == "Acolyte_WING")
        {
            charRef.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
        }
    }
    public static Il2CppSystem.Collections.Generic.List<CharacterData> AddLikelyCharacters(Il2CppSystem.Collections.Generic.List<CharacterData> inputList, Il2CppSystem.Collections.Generic.List<CharacterData> allCharacters)
    {
        for (int j = 0; j < allCharacters.Count; j++)
        {   // Makes the following characters more likely to appear: Bishop, Empress, Fortune Teller, Hunter, Jester, Lover, Oracle, Cleric, Detective, Arithmetician, Introvert, Performer.
            // 2.00x as likely: Bishop, Empress, Lookout, Cleric.
            // 2.33x as likely: Oracle, Detective.
            // 2.67x as likely: Jester, Fortune Teller, Introvert.
            // 3.00x as likely: Hunter, Arithmetician, Performer, Mathematician.
            // 3.67x as likely: Lover.
            if (allCharacters[j].characterId == "Bishop_58855542")
            {
                Debug.Log("Found Bishop file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Empress_13782227")
            {
                Debug.Log("Found Empress file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Fortune Teller_74565681")
            {
                Debug.Log("Found Fortune Teller file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Hunter_93427887")
            {
                Debug.Log("Found Hunter file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Jester_41367606")
            {
                Debug.Log("Found Jester file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Lover_91302708")
            {
                Debug.Log("Found Lover file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Oracle_07039445")
            {
                Debug.Log("Found Oracle file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Cleric_ER")
            {
                Debug.Log("Found Cleric file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Detective_VP")
            {
                Debug.Log("Found Detective file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Arithmetician_WING")
            {
                Debug.Log("Found Arithmetician file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Mathematician")
            {
                Debug.Log("Found Mathematician file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Introvert_WING")
            {
                Debug.Log("Found Introvert file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
            if (allCharacters[j].characterId == "Performer_WING")
            {
                Debug.Log("Found Performer file.");
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
                inputList.Add(allCharacters[j]);
            }
        }
        return inputList;
    }
    public static Il2CppSystem.Collections.Generic.List<CharacterData> DupeExceptUnlikely(Il2CppSystem.Collections.Generic.List<CharacterData> inputList, Il2CppSystem.Collections.Generic.List<CharacterData> allCharacters)
    {
        int listLength = inputList.Count;
        Il2CppSystem.Collections.Generic.List<string> unlikelyCharacters = new Il2CppSystem.Collections.Generic.List<string>();
        unlikelyCharacters.Add("Alchemist_94446803"); // Alchemist
        unlikelyCharacters.Add("Athlete_95133291"); // Bard
        unlikelyCharacters.Add("Dreamer_32014895"); // Dreamer
        unlikelyCharacters.Add("Druid_89845092"); // Druid
        unlikelyCharacters.Add("Gossip_85354100"); // Poet
        unlikelyCharacters.Add("Plague Doctor_49312486"); // Plague Doctor
        unlikelyCharacters.Add("Sentinel_WING"); // Sentinel (Wingidon's Expansion Pack)
        unlikelyCharacters.Add("TaxCollector_MaHy"); // Tax Collector (Mass Hysteria)
        unlikelyCharacters.Add("Germaphobe_VP"); // Germaphobe (CarlzVillagePack)
        unlikelyCharacters.Add("Nurse_scm"); // Nurse (Skill Cycler's Riddles)
        for (int j = 0; j < listLength; j++)
        {
            if (!unlikelyCharacters.Contains(inputList[j].characterId))
            {
                inputList.Add(inputList[j]);
                inputList.Add(inputList[j]);
            }
        }
        return inputList;
    }
    public static Il2CppSystem.Collections.Generic.List<string> GetJinxCharacterIDs()
    {
        Il2CppSystem.Collections.Generic.List<string> jinxedCharacters = new Il2CppSystem.Collections.Generic.List<string>();
        jinxedCharacters.Add("Arbiter_WING"); // Everyone is Disguised. The Arbiter is redundant.
        jinxedCharacters.Add("Baker_22847064"); // I am not willing to deal with Baker's shenanigans. She's already enough of a problem as is with Emenverax.
        jinxedCharacters.Add("Detective_WING"); // Everyone is telling the truth. The Detective is redundant.
        jinxedCharacters.Add("Jewelsmith_WING"); // Jewelsmith will *always* be confused when Specularus is in-play.
        jinxedCharacters.Add("Judge_87202475"); // Everyone is telling the truth. The Judge is redundant.
        jinxedCharacters.Add("Knight_47970624"); // Knight does *not* play nice with this concept, so nobody can Disguise as him. He can still exist under someone's bluff though!
        jinxedCharacters.Add("Witness_25155076"); // I know you're my special little guy, but you're just so inexplicably bugged. Sorry man.
        jinxedCharacters.Add("Arbiter_WING"); // Everyone is Disguising. The Arbiter is redundant.
        jinxedCharacters.Add("Prince_WING"); // Prince will *always* be confused when Specularus is in-play.
        jinxedCharacters.Add("Sheriff_WING"); // Sheriff will *always* be confused when Specularus is in-play.
        jinxedCharacters.Add("Mutant_WING"); // I don't want to think about what this disguise could do.
        jinxedCharacters.Add("Swarm_Good_WING"); // No.
        jinxedCharacters.Add("Lookout_RCol"); // Works weirdly, and I am not dealing with this today.
        jinxedCharacters.Add("Rook_RCol"); // No.
        jinxedCharacters.Add("Rook_VP"); // No.
        jinxedCharacters.Add("sabo_rdm"); // This one is just Poet but with extra steps.
        jinxedCharacters.Add("Empath_MaHy"); // This character does *not* play nice with Specularus.
        jinxedCharacters.Add("Magician_MaHy"); // Madness is a helluva thing.
        jinxedCharacters.Add("Pixie_MaHy"); // No.
        jinxedCharacters.Add("Psychic_ER"); // Same reason as the Judge.
        jinxedCharacters.Add("Therapist_VP"); // Curing the Drunk is... kinda eh.
        jinxedCharacters.Add("Giant_VP"); // I'd rather only have one dud Outcast here.
        jinxedCharacters.Add("MoonChild_VP"); // "Hi, I'm just going to sit here and do absolutely nothing". Fairly certain Moonchild's ability only works for real Moonchildren anyway. Would rather only have one dud Outcast (the Bombardier) here.
        jinxedCharacters.Add("Tinker_VP"); // The Tinker ability only works for real Tinkers. I'd rather only have one dud Outcast here.
        jinxedCharacters.Add("Lawyer_scm"); // "Learn someone that exists."
        jinxedCharacters.Add("Psychic_scm"); // Introvert is already kind of weak with Specularus in-play.
        jinxedCharacters.Add("Riddler_scm"); // Confessor but doesn't out itself as a Puppet or another truthful Evil. Dud card when everyone is truthful.
        jinxedCharacters.Add("Scanner_scm"); // Waiter, waiter, more dud Villagers please!
        return jinxedCharacters;
    }
}


