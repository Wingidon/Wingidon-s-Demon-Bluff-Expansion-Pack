using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine.Playables;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Bloodseer : Role
{
    bool haveActedAlready = false;
    bool haveNewInfo = true;
    public override ActedInfo GetInfo(Character charRef)
    {
        bool infoGoodEnough = false;
        ActedInfo newInfo = GetRandomInfo(charRef);
        infoGoodEnough = true;
        if (haveActedAlready)
        {
            for (int i = 0; i < 10; i++)
            {
                if (newInfo.desc == "This village confuses me" || charRef.GetCurrentActedInfo().desc.Contains(newInfo.desc))
                {
                    infoGoodEnough = false;
                    MelonLogger.Msg("Not good enough, let's try again");
                    newInfo = GetRandomInfo(charRef);
                }
                else infoGoodEnough = true;
            }
            if (!infoGoodEnough)
            {
                newInfo.desc = "I got nothing";
            }
            if (charRef.GetCurrentActedInfo().desc != "")
            {
                newInfo.desc = charRef.GetCurrentActedInfo().desc + "\n" + newInfo.desc;
            }
        }
        else
        {
            while (newInfo.desc == "This village confuses me")
            {
                MelonLogger.Msg("Not good enough, let's try again");
                newInfo = GetRandomInfo(charRef);
            }
        }
        haveActedAlready = true;
        return newInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        return GetInfo(charRef);
    }
    public override string Description
    {
        get
        {
            return "Learn random info for a price";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            haveActedAlready = false;
            //MelonLogger.Msg($"Bloodseer at {charRef.id} Acting AfterRoundStart");
        }
        if (trigger == ETriggerPhase.Day)
        {
            haveNewInfo = true;
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
            charRef.pickableUses += 1;
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(1);
        }
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            haveActedAlready = false;
            //MelonLogger.Msg($"Bloodseer at {charRef.id} Bluff-Acting AfterRoundStart");
        }
        if (trigger == ETriggerPhase.Day)
        {
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(1);
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
            charRef.pickableUses += 1;
        }
    }

    public ActedInfo GetRandomInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> goodChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> evilChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> dishonestChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> honestChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> lyingChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> truthfulChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> corruptedChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> pureChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> villagerChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> outcastChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> minionChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> demonChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> charactersNotMe = new Il2CppSystem.Collections.Generic.List<Character>();
        int goodytwoshoes = 0;
        int baddietwoshoes = 0;
        int dishonest = 0;
        int honest = 0;
        int lying = 0;
        int truthful = 0;
        int corruption = 0;
        int villagers = 0;
        int outcasts = 0;
        int minions = 0;
        int demons = 0;
        Il2CppSystem.Collections.Generic.List<string> possibleInfos = new Il2CppSystem.Collections.Generic.List<string>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAlignment() == EAlignment.Good)
            {
                goodytwoshoes++;
                goodChars.Add(character);
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil)
            {
                baddietwoshoes++;
                evilChars.Add(character);
            }
            if (CharacterHelper.CheckIfDisguisedAppearance(character) == true)
            {
                dishonest++;
                dishonestChars.Add(character);
            }
            if (CharacterHelper.CheckIfDisguisedAppearance(character) == false)
            {
                honest++;
                honestChars.Add(character);
            }
            if (CharacterHelper.CheckLyingAppearance(character) == true)
            {
                lying++;
                lyingChars.Add(character);
            }
            if (CharacterHelper.CheckLyingAppearance(character) == false)
            {
                truthful++;
                truthfulChars.Add(character);
            }
            if (character.statuses.Contains(ECharacterStatus.Corrupted))
            {
                corruption++;
                corruptedChars.Add(character);
            }
            if (!character.statuses.Contains(ECharacterStatus.Corrupted))
            {
                pureChars.Add(character);
            }
            if (character.GetRegisterAs().type == ECharacterType.Villager)
            {
                villagers++;
                villagerChars.Add(character);
            }
            if (character.GetRegisterAs().type == ECharacterType.Outcast)
            {
                outcasts++;
                outcastChars.Add(character);
            }
            if (character.GetRegisterAs().type == ECharacterType.Minion)
            {
                minions++;
                minionChars.Add(character);
            }
            if (character.GetRegisterAs().type == ECharacterType.Demon)
            {
                demons++;
                demonChars.Add(character);
            }
            if (character != charRef) charactersNotMe.Add(character);
        }
        // Always have Confessor info available
        possibleInfos.Add("Confessor");

        // If there's at least 2 Goods & 1 Evil, add Gemcrafter as an option.
        if (goodytwoshoes > 1 && baddietwoshoes > 0) possibleInfos.Add("Gemcrafter");

        // If there's at least 3 Goods & 2 Evils, add Empress as an option.
        if (goodytwoshoes > 2 && baddietwoshoes > 1) possibleInfos.Add("Empress");

        // If there's at least 1 Evil, add Hunter, Jester & Lover as possibilities
        if (baddietwoshoes > 0) possibleInfos.Add("FortuneTeller"); possibleInfos.Add("Hunter"); possibleInfos.Add("Jester"); possibleInfos.Add("Lover"); possibleInfos.Add("Ranger");

        // If there's at least 2 Evils, add Arithmetician, Knitter, Scout & Mathematician as options. (Mathematician learns the sum of 2 Evils)
        if (baddietwoshoes > 1) possibleInfos.Add("Arithmetician"); possibleInfos.Add("Knitter"); // possibleInfos.Add("Scout"); Fuck that

        // If there's at least 2 Goods and at least 1 Minion, add Oracle as an option.
        if (goodytwoshoes > 1 && minions > 0) possibleInfos.Add("Oracle");

        // If there's at least 1 of each alignment, add Skeptic (Dupery) to the list
        if (goodytwoshoes != 0 && baddietwoshoes != 0) possibleInfos.Add("Skeptic");

        // If there's at least 2 of each alignment, add Seamstress (BotC) as an option.
        if (goodytwoshoes > 1 && baddietwoshoes > 1) possibleInfos.Add("Seamstress");

        // If there's at least 1 Villager, add Forager as an option.
        if (villagers > 0) possibleInfos.Add("Forager");

        // If there's at least 1 Corrupted character, add Bard & Sentinel as options.
        if (corruption > 0) possibleInfos.Add("Sentinel"); // possibleInfos.Add("Bard"); Couldn't be arsed to deal with edge-cases

        // If there's at least 1 Outcast, add Librarian (BotC), Lamb & Devout as options.
        if (outcasts > 0) possibleInfos.Add("Lamb"); possibleInfos.Add("Devout"); // possibleInfos.Add("Librarian"); Bugged

        // If there's at least 1 liar & at least 1 truther, add Judge & Detective as options (Detective here learns that 1 of 2 chars is Lying)
        if (outcasts > 0) possibleInfos.Add("Judge"); // possibleInfos.Add("Detective"); FORGOR, WILL ADD LATER

        // If there's at least 1 disguiser & at least 1 honest char, add Arbiter & Prince as options.
        if (outcasts > 0) possibleInfos.Add("Arbiter"); possibleInfos.Add("Prince");

        // If there's at least 1 of each type, add Warden & Director as options. (Director here learns 2 characters and whether they share a type)
        if (villagers != 0 && outcasts != 0 && minions != 0 && demons != 0) possibleInfos.Add("Director"); // possibleInfos.Add("Warden"); Nah

        /* In total, the options are:
        - Confessor - Learns "I am Good" or "I am dizzy"
        - Gemcrafter - Learns 1 Good character
        - FortuneTeller - Learns 2 characters and whether either is Evil.
        - Hunter - Learns distance to nearest Evil
        - Jester - Learns 3 characters and how many are Evil.
        - Lover - Learns how many neighbours are Evil
        - Ranger - Learns distance to furthest Evil
        - Arithmetician - Learn sum of all Evils
        - Knitter - Learn number of pairs of Evil
        - Scout - Learn in-play Evil and its distance to its nearest Evil
        - Oracle - Learn that 1 of 2 characters is a particular Minion
        - Skeptic - Learn a character & its alignment
        - Seamstress - Learn 2 characters & whether or not they're allies.
        - Forager - Learn a character & whether or not they're a Villager.
        - Bard - Learn distance to nearest Corruption
        - Sentinel - Learn that 1 of 2 characters is Corrupted
        - Librarian - Learn that 1 of 2 characters is a particular Outcast.
        - Lamb - Learn distance to a particular Outcast
        - Devout - Learn 1 Outcast character.
        - Judge - Learn 1 character & whether or not they're Lying
        - Detective - Learn that 1 of 2 characters is Lying
        - Arbiter - Learn 1 character & whether or not they're Disguised
        - Prince - Learn that 1 of 2 characters is Disguised
        */

        string chosenType = possibleInfos[UnityEngine.Random.RandomRangeInt(0, possibleInfos.Count)];
        string info = $"Info of type {chosenType} isn't working, please notify Wingidon ASAP!";
        // Let's go through these one-by-one.
        ActedInfo returnInfo = new ActedInfo("");
        wx_SavedScripts sharedScripts = new wx_SavedScripts();

        MelonLogger.Msg($"Bloodseer chose info type of {chosenType}, attempting to get info");

        Role copiedRole = new Confessor();

        bool copycat = false;

        // Let's just get all the copied abilities out of the way now.
        if (chosenType == "Confessor")
        {
            selection.Add(charRef);
            info = "I am Good";
            if (CharacterHelper.CheckLying(charRef) || charRef.GetRegisterAlignment() == EAlignment.Evil) info = "I am dizzy";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }
        if (chosenType == "Gemcrafter")
        {
            if (goodChars.Count != 1) goodChars.Remove(charRef);
            if (evilChars.Count != 1) evilChars.Remove(charRef);
            if (CharacterHelper.CheckLying(charRef) == false) selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
            else selection.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);
            MelonLogger.Msg($"Lying?: {CharacterHelper.CheckLying(charRef)}");
            MelonLogger.Msg($"Picked: {selection[0].id}");
            info = $"#{selection[0].id} is Good";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }
        if (chosenType == "Hunter")
        {
            int trueDistance = sharedScripts.GetClosestDistance(evilChars, charRef);
            string cardPlural = "cards";
            if (!CharacterHelper.CheckLying(charRef))
            {
                if (trueDistance == 1) cardPlural = "card";
                selection = Characters.Instance.GetCharactersAtRange(trueDistance, charRef);
                info = $"I am {trueDistance} {cardPlural} away from closest Evil";
            }
            else
            {
                Il2CppSystem.Collections.Generic.List<Character> fakeEvilTeam = sharedScripts.GetFakeEvilTeam();
                int falseDistance = sharedScripts.GetClosestDistance(fakeEvilTeam, charRef);
                if (falseDistance == trueDistance)
                {
                    if (falseDistance == 1) falseDistance++;
                    else falseDistance--;
                }
                if (falseDistance == 1) cardPlural = "card";
                selection = Characters.Instance.GetCharactersAtRange(falseDistance, charRef);
                info = $"I am {falseDistance} {cardPlural} away from closest Evil";
            }
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }
        if (chosenType == "Lover")
        {
            selection = sharedScripts.GetCharacterNeighbours(charRef);
            int evilsFound = 0;
            foreach (Character character in selection)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil) evilsFound++;
            }
            string evilsPlural = "Evils";
            if (!CharacterHelper.CheckLying(charRef))
            {
                if (evilsFound == 1) evilsPlural = "Evil";
                info = $"{evilsFound} {evilsPlural} adjacent to me";
            }
            else
            {
                Il2CppSystem.Collections.Generic.List<int> availableLies = new Il2CppSystem.Collections.Generic.List<int>();
                if (evilsFound != 0)
                {
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                }
                if (evilsFound != 1)
                {
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                }
                if (evilsFound != 2)
                {
                    availableLies.Add(2);
                    availableLies.Add(2);
                    availableLies.Add(2);
                }
                int chosenLie = availableLies[UnityEngine.Random.RandomRangeInt(0, availableLies.Count)];
                if (chosenLie == 1) evilsPlural = "Evil";
                info = $"{chosenLie} {evilsPlural} adjacent to me";
            }
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }
        if (chosenType == "Ranger")
        {
            copiedRole = new w_Ranger();
            copycat = true;
        }
        if (chosenType == "Arithmetician")
        {
            copiedRole = new w_Arithmetician();
            copycat = true;
        }
        if (chosenType == "Knitter")
        {
            Il2CppSystem.Collections.Generic.List<Character> allCharsPlusOne = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> fakeEvilTeam = sharedScripts.GetFakeEvilTeam();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                allCharsPlusOne.Add(character);
            }
            allCharsPlusOne.Add(Gameplay.CurrentCharacters[0]);
            bool prevCharEvil = false;
            bool prevCharFakeEvil = false;
            int evilPairs = 0;
            int fakeEvilPairs = 0;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                MelonLogger.Msg($"Checking #{character.id}...");
                if (prevCharEvil && character.GetRegisterAlignment() == EAlignment.Evil) evilPairs++; MelonLogger.Msg($"Previous character was Evil and this character is Evil! Evil pairs: {evilPairs}");
                if (prevCharFakeEvil && character.GetRegisterAlignment() == EAlignment.Evil) fakeEvilPairs++; MelonLogger.Msg($"Previous character was (fake) Evil and this character is (fake) Evil! Fake Evil pairs: {fakeEvilPairs}");
                if (character.GetRegisterAlignment() == EAlignment.Evil) prevCharEvil = true;
                else prevCharEvil = false;
                if (fakeEvilTeam.Contains(character)) prevCharFakeEvil = true;
                else prevCharFakeEvil = false;
            }
            if (fakeEvilPairs == evilPairs)
            {
                if (fakeEvilPairs == 0) fakeEvilPairs++;
                else fakeEvilPairs--;
            }
            string pairsPlural = "pairs";
            string isAre = "are";
            if (!CharacterHelper.CheckLying(charRef))
            {
                MelonLogger.Msg("Grabbing Truthful Knitter info");
                if (evilPairs == 1)
                {
                    pairsPlural = "pair"; isAre = "is";
                }
                info = $"There {isAre} {evilPairs} {pairsPlural} of Evil";
            }
            else
            {
                MelonLogger.Msg("Grabbing Lying Knitter info");
                if (fakeEvilPairs == 1)
                {
                    pairsPlural = "pair"; isAre = "is";
                }
                info = $"There {isAre} {fakeEvilPairs} {pairsPlural} of Evil";
            }
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }
        //if (chosenType == "Scout")
        //{
        //    copiedRole = new Scout();
        //    copycat = true;
        //}
        if (chosenType == "Oracle")
        {
            w_Chiromancer chiromancer = new w_Chiromancer();
            string minionName = "Minion";
            if (!CharacterHelper.CheckLying(charRef))
            {
                selection.Add(minionChars[UnityEngine.Random.RandomRangeInt(0, minionChars.Count)]);
                goodChars.Remove(selection[0]);
                selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
                minionName = selection[0].GetRegisterAs().GetCharacterName();
            }
            else
            {
                Il2CppSystem.Collections.Generic.List<CharacterData> allMinions = Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion);
                minionName = allMinions[UnityEngine.Random.RandomRangeInt(0, allMinions.Count)].GetCharacterName();
                Il2CppSystem.Collections.Generic.List<Character> possibleFakeMinions = new Il2CppSystem.Collections.Generic.List<Character>();
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.GetRegisterAs().GetCharacterName() != minionName)
                    {
                        possibleFakeMinions.Add(character);
                    }
                }
                selection.Add(possibleFakeMinions[UnityEngine.Random.RandomRangeInt(0, possibleFakeMinions.Count)]);
                possibleFakeMinions.Remove(selection[0]);
                selection.Add(possibleFakeMinions[UnityEngine.Random.RandomRangeInt(0, possibleFakeMinions.Count)]);
            }
            selection = sharedScripts.SortList(selection);
            info = $"#{selection[0].id} or #{selection[1].id} {chiromancer.CheckIfThe(minionName)} {minionName}";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }
        //if (chosenType == "Bard")
        //{
        //   copiedRole = new Acrobat2();
        //   copycat = true;
        //}
        if (chosenType == "Sentinel")
        {
            copiedRole = new w_Sentinel();
            copycat = true;
        }
        if (chosenType == "Lamb")
        {
            copiedRole = new w_Lamb();
            copycat = true;
        }
        if (chosenType == "Devout")
        {
            copiedRole = new w_Devout();
            copycat = true;
        }
        if (chosenType == "Prince")
        {
            copiedRole = new w_Prince();
            copycat = true;
        }

        if (chosenType == "Empress")
        {
            if (!CharacterHelper.CheckLying(charRef))
            {
                goodChars.Remove(charRef);
                evilChars.Remove(charRef);
                MelonLogger.Msg($"Gathering truthful Empress info. Selection currently contains {selection.Count} characters.");
                selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
                MelonLogger.Msg($"Chose first Good character of #{selection[0].id}.");
                goodChars.Remove(selection[0]);
                selection.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
                MelonLogger.Msg($"Chose second Good character of #{selection[1].id}.");
                goodChars.Remove(selection[1]);
                selection.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);
                MelonLogger.Msg($"Chose Evil character of #{selection[2].id}.");
                selection = sharedScripts.SortList(selection);
                returnInfo.desc = info;
                returnInfo.characters = selection;
            }
            else
            {
                Il2CppSystem.Collections.Generic.List<Character> possibleTargets = new Il2CppSystem.Collections.Generic.List<Character>();
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    possibleTargets.Add(character);
                }
                possibleTargets.Remove(charRef);
                Il2CppSystem.Collections.Generic.List<Character> chosenTargets = new Il2CppSystem.Collections.Generic.List<Character>();
                chosenTargets.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                possibleTargets.Remove(chosenTargets[0]);
                goodChars.Remove(chosenTargets[0]);
                evilChars.Remove(chosenTargets[0]);
                chosenTargets.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                possibleTargets.Remove(chosenTargets[1]);
                goodChars.Remove(chosenTargets[1]);
                evilChars.Remove(chosenTargets[1]);
                chosenTargets.Add(possibleTargets[UnityEngine.Random.RandomRangeInt(0, possibleTargets.Count)]);
                possibleTargets.Remove(chosenTargets[2]);
                goodChars.Remove(chosenTargets[2]);
                evilChars.Remove(chosenTargets[2]);
                int evilsPicked = 0;
                foreach (Character character in chosenTargets)
                {
                    goodChars.Remove(character);
                    evilChars.Remove(character);
                    if (character.GetRegisterAlignment() == EAlignment.Evil) evilsPicked++;
                }
                if (evilsPicked == 1)
                {
                    goodChars.Remove(charRef);
                    evilChars.Remove(charRef);
                    if (chosenTargets[0].GetRegisterAlignment() == EAlignment.Evil)
                    {
                        chosenTargets.Add(goodChars[UnityEngine.Random.RandomRangeInt(0, goodChars.Count)]);
                        chosenTargets.Remove(chosenTargets[0]);
                    }
                    else
                    {
                        chosenTargets.Add(evilChars[UnityEngine.Random.RandomRangeInt(0, evilChars.Count)]);
                        chosenTargets.Remove(chosenTargets[0]);
                    }
                }
                selection = sharedScripts.SortList(chosenTargets);
            }
            info = $"Exactly one is Evil: #{selection[0].id}, #{selection[1].id}, #{selection[2].id}";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        if (copycat)
        {
            if (CharacterHelper.CheckLying(charRef)) returnInfo = copiedRole.GetBluffInfo(charRef);
            else returnInfo = copiedRole.GetInfo(charRef);
            MelonLogger.Msg($"Gathered copycat info of {copiedRole.ToString()}");
        }


        if (chosenType == "Director")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            charactersNotMe.Remove(selection[0]);
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            selection = sharedScripts.SortList(selection);
            bool shareType = false;
            if (selection[0].GetRegisterAs().type == selection[1].GetRegisterAs().type) shareType = true;
            if (!CharacterHelper.CheckLying(charRef) == shareType) info = $"#{selection[0].id} and #{selection[1].id} are the same Type"; // If Truthful & they share a type OR Lying and they don't share a type,
            else info = $"#{selection[0].id} and #{selection[1].id} are different Types";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // Pick characters can't be replicated nearly as easily and most of the others are from mods, so I'm on my own now.
        /* Remaining:
        - FortuneTeller - Learns 2 characters and whether either is Evil.
        - Jester - Learns 3 characters and how many are Evil.
        - Skeptic - Learn a character & its alignment
        - Seamstress - Learn 2 characters & whether or not they're allies.
        - Forager - Learn a character & whether or not they're a Villager.
        - Librarian - Learn that 1 of 2 characters is a particular Outcast.
        - Judge - Learn 1 character & whether or not they're Lying
        - Detective - Learn that 1 of 2 characters is Lying
        - Arbiter - Learn 1 character & whether or not they're Disguised
        */

        // Fortune Teller
        if (chosenType == "FortuneTeller")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            charactersNotMe.Remove(selection[0]);
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            selection = sharedScripts.SortList(selection);
            bool eitherEvil = false;
            foreach (Character character in selection)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil)
                {
                    eitherEvil = true;
                }
            }
            if (!CharacterHelper.CheckLying(charRef) == eitherEvil) info = $"At least one is Evil: #{selection[0].id}, #{selection[1].id}"; // If Truthful & one is Evil OR Lying & both are Good
            else info = $"Both are Good: #{selection[0].id}, #{selection[1].id}";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // Jester
        if (chosenType == "Jester")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            charactersNotMe.Remove(selection[0]);
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            charactersNotMe.Remove(selection[1]);
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            selection = sharedScripts.SortList(selection);
            int evilsFound = 0;
            int chosenNum = 0;
            Il2CppSystem.Collections.Generic.List<int> availableLies = new Il2CppSystem.Collections.Generic.List<int>();
            foreach (Character character in selection)
            {
                if (character.GetRegisterAlignment() == EAlignment.Evil)
                {
                    evilsFound++;
                }
            }

            string isare = "are";
            if (evilsFound == 1) isare = "is";
            if (!CharacterHelper.CheckLying(charRef)) info = $"#{selection[0].id}, #{selection[1].id}, #{selection[2].id}:\n{evilsFound} {isare} Evil";
            else
            {
                if (evilsFound != 0)
                {
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                    availableLies.Add(0);
                }
                if (evilsFound != 1)
                {
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                    availableLies.Add(1);
                }
                if (evilsFound != 2)
                {
                    availableLies.Add(2);
                    availableLies.Add(2);
                    availableLies.Add(2);
                    availableLies.Add(2);
                    availableLies.Add(2);
                }
                if (evilsFound != 3)
                {
                    availableLies.Add(3);
                    availableLies.Add(3);
                }
                chosenNum = availableLies[UnityEngine.Random.RandomRangeInt(0, availableLies.Count)];
                isare = "are";
                if (chosenNum == 1) isare = "is";
                info = $"#{selection[0].id}, #{selection[1].id}, #{selection[2].id}:\n{chosenNum} {isare} Evil";
            }
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // That was a lot.
        /* Remaining:
        - Skeptic - Learn a character & its alignment
        - Seamstress - Learn 2 characters & whether or not they're allies.
        - Forager - Learn a character & whether or not they're a Villager.
        - Judge - Learn 1 character & whether or not they're Lying
        - Arbiter - Learn 1 character & whether or not they're Disguised
        - Librarian - Learn that 1 of 2 characters is a particular Outcast.
        - Detective - Learn that 1 of 2 characters is Lying
        */

        // Skeptic
        if (chosenType == "Skeptic")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            if (!CharacterHelper.CheckLying(charRef) == (selection[0].GetRegisterAlignment() == EAlignment.Good)) info = $"#{selection[0].id} is Good"; // If Truthful & target is Good OR Lying & target is Evil
            else info = $"#{selection[0].id} is Evil";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // Seamstress
        if (chosenType == "Seamstress")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            charactersNotMe.Remove(selection[0]);
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            selection = sharedScripts.SortList(selection);
            bool enemies = false;
            if (selection[0].GetRegisterAlignment != selection[1].GetRegisterAlignment) enemies = true;
            MelonLogger.Msg($"Are they enemies? {enemies}. Are we Lying? {CharacterHelper.CheckLying(charRef)}. Should we declare them enemies? {(!CharacterHelper.CheckLying(charRef) == enemies)}");
            if (!CharacterHelper.CheckLying(charRef) == enemies) info = $"#{selection[0].id} and #{selection[1].id} are enemies";
            else info = $"#{selection[0].id} and #{selection[1].id} are allies";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // Forager
        if (chosenType == "Forager")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            if (!CharacterHelper.CheckLying(charRef) == (selection[0].GetRegisterAs().type == ECharacterType.Villager)) info = $"#{selection[0].id} is a Villager"; // If Truthful & target is a Villager OR Lying & target is a non-Villager
            else info = $"#{selection[0].id} is not a Villager";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // Judge
        if (chosenType == "Judge")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            if (!CharacterHelper.CheckLying(charRef) == (CharacterHelper.CheckLyingAppearance(selection[0]))) info = $"#{selection[0].id} is Lying"; // If Truthful & target is a Lying OR Lying & target is Truthful
            else info = $"#{selection[0].id} is Truthful";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // Arbiter
        if (chosenType == "Arbiter")
        {
            selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
            if (!CharacterHelper.CheckLying(charRef) == (CharacterHelper.CheckIfDisguisedAppearance(selection[0]))) info = $"#{selection[0].id} is Disguised"; // If Truthful & target is a Disguising OR Lying & target is Honest
            else info = $"#{selection[0].id} is Honest";
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }

        // Librarian
        if (chosenType == "Librarian")
        {
            Il2CppSystem.Collections.Generic.List<CharacterData> allDeckOutcasts = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // All Outcasts in the Deck
            Il2CppSystem.Collections.Generic.List<CharacterData> deckOutcastsAvoided = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // All Deck Outcasts, not including those we want to avoid
            Il2CppSystem.Collections.Generic.List<CharacterData> deckOutcastsBluffing = new Il2CppSystem.Collections.Generic.List<CharacterData>(); // Bluffing Deck Outcasts
            Il2CppSystem.Collections.Generic.List<string> bluffOutcastIDs = new Il2CppSystem.Collections.Generic.List<string>();
            Il2CppSystem.Collections.Generic.List<string> avoidIfPossibleIDs = new Il2CppSystem.Collections.Generic.List<string>();

            bluffOutcastIDs.Add("Drunk_15369527");
            bluffOutcastIDs.Add("Doppleganger_52694042");
            bluffOutcastIDs.Add("Lunatic_WING");
            bluffOutcastIDs.Add("Renegade_WING");
            bluffOutcastIDs.Add("Revolutionary_WING");
            bluffOutcastIDs.Add("Hitman_scm");
            avoidIfPossibleIDs.Add("Wretch_80988916");
            avoidIfPossibleIDs.Add("Marionette_WING");
            avoidIfPossibleIDs.Add("Mutant_WING");
            avoidIfPossibleIDs.Add("MadScientist_scm");
            avoidIfPossibleIDs.Add("Ghost_scm");
            avoidIfPossibleIDs.Add("Confectioner_scm");
            if (!CharacterHelper.CheckLying(charRef)) // If Truthful
            {
                outcastChars.Remove(charRef);
                if (outcastChars.Count == 0) info = "I am the only Outcast";
                else
                {
                    selection.Add(outcastChars[UnityEngine.Random.RandomRangeInt(0, outcastChars.Count)]);
                    string chosenName = selection[0].GetRegisterAs().GetCharacterName();
                    charactersNotMe.Remove(selection[0]);
                    selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
                    selection = sharedScripts.SortList(selection);
                    info = $"#{selection[0].id} or #{selection[1].id} is the {chosenName}";
                }
            }
            else
            {
                foreach (CharacterData character in Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Outcast))
                {
                    allDeckOutcasts.Add(character);
                    if (bluffOutcastIDs.Contains(character.characterId))
                    {
                        deckOutcastsBluffing.Add(character);
                    }
                    if (!avoidIfPossibleIDs.Contains(character.characterId))
                    {
                        deckOutcastsAvoided.Add(character);
                    }
                }
                string chosenName = "";
                if (deckOutcastsBluffing.Count != 0) chosenName = deckOutcastsBluffing[UnityEngine.Random.RandomRangeInt(0, deckOutcastsBluffing.Count)].GetCharacterName();
                else if (deckOutcastsAvoided.Count != 0) chosenName = deckOutcastsAvoided[UnityEngine.Random.RandomRangeInt(0, deckOutcastsAvoided.Count)].GetCharacterName();
                else chosenName = allDeckOutcasts[UnityEngine.Random.RandomRangeInt(0, allDeckOutcasts.Count)].GetCharacterName();
                foreach (Character character in Gameplay.CurrentCharacters)
                {
                    if (character.GetRegisterAs().GetCharacterName() == chosenName)
                    {
                        charactersNotMe.Remove(character);
                    }
                }
                selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
                charactersNotMe.Remove(selection[0]);
                selection.Add(charactersNotMe[UnityEngine.Random.RandomRangeInt(0, charactersNotMe.Count)]);
                selection = sharedScripts.SortList(charactersNotMe);
                info = $"#{selection[0].id} or #{selection[1].id} is the {chosenName}";
            }
            MelonLogger.Msg($"Finished gathering info.");
            MelonLogger.Msg($"Info text:\n {info}");
            MelonLogger.Msg($"Info selection:\n {sharedScripts.MentionEveryCharacterInList(selection, "and")}");
            returnInfo.desc = info;
            returnInfo.characters = selection;
        }
        MelonLogger.Msg($"Finished gathering random info.");
        MelonLogger.Msg($"Info type:\n{chosenType}");
        MelonLogger.Msg($"Info:\n{returnInfo.desc}");
        return returnInfo;
    }
    public w_Bloodseer() : base(ClassInjector.DerivedConstructorPointer<w_Bloodseer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Bloodseer(IntPtr ptr) : base(ptr)
    {
    }
}


