using ExpansionPack;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Il2CppSystem.Runtime.Remoting.Messaging;
using MelonLoader;
using MelonLoader.Utils;
using System;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using UnityEngine;
using UnityEngine.Playables;
using static Il2Cpp.GameplayEvents;
using static Il2CppSystem.Array;
using static MelonLoader.Modules.MelonModule;

[assembly: MelonInfo(typeof(MainMod), "Wingidon's Expansion Pack", "1.0", "Wingidon")]
[assembly: MelonGame("UmiArt", "Demon Bluff")]

namespace ExpansionPack;
public class MainMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        // Villagers
        ClassInjector.RegisterTypeInIl2Cpp<w_Arbiter>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Arithmetician>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Cardshark>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Chiromancer>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Clairvoyant>();
        ClassInjector.RegisterTypeInIl2Cpp<w_SlayerRework>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Copycat>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Detective>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Devout>();
        ClassInjector.RegisterTypeInIl2Cpp<w_FiDragonfly>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Forager>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Gossip>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Gravekeeper>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Introvert>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Jewelsmith>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Lamb>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Performer>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Prince>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Ranger>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Scavenger>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Sentinel>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Sheriff>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Spy>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Warden>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Bloodseer>();

        // Outcasts
        ClassInjector.RegisterTypeInIl2Cpp<w_Chatterbox>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Lunatic>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Marionette>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Mutant>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Pilgrim>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Revolutionary>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Renegade>();

        // Minions
        ClassInjector.RegisterTypeInIl2Cpp<w_Acolyte>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Fanatic>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Professional>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Saboteur>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Swarm_Good>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Swarm_Evil>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Turncoat>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Undying>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Zealot>();

        // Demons

        ClassInjector.RegisterTypeInIl2Cpp<w_Legion>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Caedoccidere>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Illusionist>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Leviathan>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Pandemonium>();
        ClassInjector.RegisterTypeInIl2Cpp<w_InvertDemon>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Praesect>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Minos>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Shard>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Tenecaligo>();
        ClassInjector.RegisterTypeInIl2Cpp<w_Mezepheles>();
        ClassInjector.RegisterTypeInIl2Cpp<w_TwinDemon>();
        ClassInjector.RegisterTypeInIl2Cpp<w_TwinDemonTwin>();
        ClassInjector.RegisterTypeInIl2Cpp<w_TwinDemonThree>();
    }


    public MelonPreferences_Category configCategory = null!;
    public override void OnLateInitializeMelon()
    {
        GameObject content = GameObject.Find("Game/Gameplay/Content");
        NightPhase nightPhase = content.GetComponent<NightPhase>();
        Statics.GetStartingRoles();


        configCategory = MelonPreferences.CreateCategory("WingModSettings");
        configCategory.CreateEntry("Agmeres_Weight", 2, description: "How likely Agmeres is to be in-play.");
        configCategory.CreateEntry("Veni-Vidi-Vici_Weight", 2, description: "How likely the Hellspawn Triplets are to be in-play.");
        configCategory.CreateEntry("Caedoccidere_Weight", 2, description: "How likely Caedoccidere is to be in-play.");
        configCategory.CreateEntry("Praesect_Weight", 2, description: "How likely Praesect is to be in-play.");
        configCategory.CreateEntry("Mendaverte_Weight", 2, description: "How likely Mendaverte is to be in-play.");
        configCategory.CreateEntry("Venelum_Weight", 2, description: "How likely Venelum is to be in-play.");
        configCategory.CreateEntry("Sanguitaurus_Weight", 2, description: "How likely Sanguitaurus is to be in-play.");
        configCategory.CreateEntry("Tenecaligo_Weight", 2, description: "How likely Tenecaligo is to be in-play.");
        configCategory.CreateEntry("Leviathan_Weight", 2, description: "How likely Leviathan is to be in-play.");
        configCategory.CreateEntry("Iris_Weight", 2, description: "How likely Iris is to be in-play.");
        configCategory.CreateEntry("Carni_Weight", 2, description: "How likely Carnicarius is to be in-play.");
        configCategory.CreateEntry("Misc_Weight", 2, description: "How likely any Demon I've forgotten is to be in-play.");
        configCategory.SetFilePath(Path.Combine(MelonEnvironment.UserDataDirectory, "WingModConfig.cfg"));
        configCategory.SaveToFile();





        // To potentially add later:
        // Auditor - Learn how many characters fit a particular descriptor (e.g. Good, Evil, Villager, Outcast, Minion, Demon, Corruption, etc)
        // Bounty Hunter - 1 random Good Villager is Evil & Corrupted. Learn 1 Evil character.
        // Investigator - Learn how many Unrevealed characters are Disguised.
        // Paperboy - Learn how long my chain of Good characters is.
        // Partisan - Good characters adjacent to me can't die.
        // Saint - I am always Good.
        // Writer - Learn a Villager role and its distance to its nearest Outcast.

        // Occultist - Has a Minion ability (Make them become a Good Minion that Disguises as Occultist?)
        // Provocateur - Kills a Villager each night, lose if all Villagers die.

        // Follower - Created by a Demon. Deals damage when executed if the Demon still lives.
        // Goblin - Each night, swaps the roles of 2 unrevealed characters if possible.
        // Hypnotist - Forces its neighbours to Disguise, but they don't Lie unless they already would.
        // Prosecutor - Makes a Villager Register as the Prosecutor. When Executed, Learns the role that it prosecuted.
        // Warlock - I have the Demon's ability.
        // ??? - Lies and Disguises as or like a fake Outcast added to the Deck.

        // Aries - Creates an Evil Minion out of a Good Villager. Buff to something better, maybe? Feels a little weak, esp. compared to Praesect. Maybe add fake Minions to the Deck too? Disguise unusually?
        // Clavinus - Name derived from Latin "Pampinus" meaning "Tendril" and "Clavicula" also meaning "Tendril". Sits adjacent to only Villagers & Minions, and Corrupts adjacent Villagers. Tells the truth.
        // Poenitte - Name derived from Latin "Poenitere" meaning "Repent" and "Dimitte" meaning "Forgive". Good Demon. Adds a second Demon - Pick 1: if a Demon picked, execute it. Might also call it Vivian?
        // Sect Leader - Creates a second Demon adjacent to an Evil character. Need to think of a proper name for this. Perhaps could also become a different Demon similar to Magnere?
        // Tempriam - Dead Minions appear as Tempriams. Need to think about how I'd implement something like this. Could overwrite the CharacterData of Minions with Tempriam when their true role is revealed, maybe.


        CharacterData w_prince = newCharacter("Prince", EAlignment.Good, ECharacterType.Villager, true, false, "\"Secretly wishes that his mother was more trusting.\"");
        w_prince.role = new w_Prince();
        w_prince.description = "Learn that exactly 1 of 2 characters is Disguised.";
        w_prince.ifLies = $"Both characters in my info are {formattedKeyText("Honest")}.";
        w_prince.gender = EGender.Male;

        CharacterData w_clairvoyant = newCharacter("Clairvoyant", EAlignment.Good, ECharacterType.Villager, true, false, "\"Sees a friendship in the future. Doesn't always understand why.\"");
        w_clairvoyant.role = new w_Clairvoyant();
        w_clairvoyant.description = "Learn 2 characters that share an alignment.";
        w_clairvoyant.gender = EGender.Female;

        CharacterData w_forager = newCharacter("Forager", EAlignment.Good, ECharacterType.Villager, true, false, "\"Her instructions are clear, it's just that her assistants don't follow them.\"");
        w_forager.role = new w_Forager();
        w_forager.description = "<b>Pick 1 character:</b>\nLearn if they are a Villager.";
        w_forager.hints = customHint("Ability Refresh Hint", "Each Night") + $"\n\nArt by {formattedKeyText("WeekendWolf")} ({formattedKeyText("@weekendwolf")}) on {formattedKeyText("Discord")}";
        w_forager.gender = EGender.Female;
        w_forager.picking = true;
        w_forager.abilityUsage = EAbilityUsage.ResetAfterNight;

        /*
        CharacterData w_gambler = new CharacterData();
        w_gambler.role = new w_Gambler();
        w_gambler.name = "Gambler";
        w_gambler.description = "<b>Pick 1 character:</b>\nThey die, but don't reveal their <color=#FFFFFF>True Role</color>.\nFor each <b>Unrevealed</b> character, deal 1 damage to you if Villager picked, or 2 healing otherwise.";
        w_gambler.flavorText = "\"She likes her odds, perhaps a little too much.\"";
        w_gambler.hints = abilityRefreshHint(EAbilityUsage.Once);
        w_gambler.ifLies = "I heal you when you pick Villagers and damage you when you don't.";
        w_gambler.picking = true;
        w_gambler.startingAlignment = EAlignment.Good;
        w_gambler.type = ECharacterType.Villager;
        w_gambler.abilityUsage = EAbilityUsage.Once;
        w_gambler.bluffable = true;
        w_gambler.characterId = "Gambler_WING";
        w_gambler.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_gambler.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_gambler.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_gambler.color = new Color(1f, 0.935f, 0.7302f);
        */

        CharacterData w_sentinel = newCharacter("Sentinel", EAlignment.Good, ECharacterType.Villager, true, false, "\"She rarely ever rejects anyone from the village.\nShe does have some suspicions though.\"");
        w_sentinel.role = new w_Sentinel();
        w_sentinel.description = "Learn that exactly 1 of 2 characters is Corrupted.";
        w_sentinel.hints = $"Art by {formattedKeyText("WeekendWolf")} ({formattedKeyText("@weekendwolf")}) on {formattedKeyText("Discord")}";
        w_sentinel.ifLies = $"Both characters in my info are {formattedKeyText("Pure")}.";
        w_sentinel.gender = EGender.Male;

        CharacterData w_spy = newCharacter("Spy", EAlignment.Good, ECharacterType.Villager, true, false, "\"Thinks he's extremely stealthy. He's not.\"");
        w_spy.role = new w_Spy();
        w_spy.description = $"Learn an Evil role that has not been {formattedKeyText("Revealed")} yet (or that all have been).";
        w_spy.hints = $"If I am {formattedKeyText("Revealed")} last, Learn that 1 of 2 characters is a particular Evil.\n\nArt by {formattedKeyText("Panda")} ({formattedKeyText("@Panda")}) on {formattedKeyText("Discord")}";
        w_spy.ifLies = $"If I am {formattedKeyText("Revealed")} last, both characters in my info are Good.";
        w_spy.gender = EGender.Male;

        CharacterData w_arithmetician = newCharacter("Arithmetician", EAlignment.Good, ECharacterType.Villager, true, false, "\"Her accusations are so complicated that nobody even knows what they're being accused of.\"");
        w_arithmetician.role = new w_Arithmetician();
        w_arithmetician.description = "Learn the sum of all Evil characters' positions.";
        w_arithmetician.hints = $"Art by {formattedKeyText("Blue Cheesed")} ({formattedKeyText("@Blue Cheesed")}) on {formattedKeyText("Discord")}";
        w_arithmetician.gender = EGender.Female;
        w_arithmetician.usuallyDisguised = false;
        w_arithmetician.additionalFlavorTexts = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(1);
        w_arithmetician.additionalFlavorTexts[0] = w_arithmetician.flavorText;

        CharacterData w_scavenger = newCharacter("Scavenger", EAlignment.Good, ECharacterType.Villager, true, false, "\"They've always hated the term 'loot goblin', despite how perfectly it describes them.\"");
        w_scavenger.role = new w_Scavenger();
        w_scavenger.description = $"<b>Activate Me:</b>\nFor every {formattedKeyText("Dead")} Evil character, gain 1 {formattedKeyText("Max Health")} and {formattedKeyText("Heal")} you for 2 {formattedKeyText("Health")}.";
        w_scavenger.hints = $"{customHint("Ability Refresh Hint", "Once Per Game")}\n\nIf no Evil characters are {formattedKeyText("Dead")}, my ability fails & is refunded.\n\nArt by {formattedKeyText("LostIllustrator")} ({formattedKeyText("@lostillustrator")}) on {formattedKeyText("Discord")}";
        w_scavenger.ifLies = $"<b>Activate Me:</b>\nFor every {formattedKeyText("Dead")} Evil character, deal 2 {formattedKeyText("Damage")} to you.";
        w_scavenger.picking = true;
        w_scavenger.gender = EGender.Female;
        w_scavenger.abilityUsage = EAbilityUsage.Once;


        /*
            //CharacterData w_pirate = new CharacterData();
            //w_pirate.role = new w_Pirate();
            //w_pirate.name = "Pirate";
            //w_pirate.description = "<b>At Night</b>\nKill and Reveal 1 <color=#99FF99>Pure</color> Good character.\nDeal 1 damage to you.";
            //w_pirate.flavorText = "\"Her accent is authentic, but she's never actually been at sea.\"";
            //w_pirate.hints = "'<color=#99FF99>Pure</color>' means 'Not Corrupted'.";
            //w_pirate.ifLies = "I do not kill.";
            //w_pirate.picking = false;
            //w_pirate.startingAlignment = EAlignment.Good;
            //w_pirate.type = ECharacterType.Villager;
            //w_pirate.bluffable = true;
            //w_pirate.characterId = "Pirate_WING";
            //w_pirate.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
            //w_pirate.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
            //w_pirate.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
            //w_pirate.color = new Color(1f, 0.935f, 0.7302f);
            //nightPhase.nightCharactersOrder.Add(w_pirate);
        */


        /*
        CharacterData w_devout = newCharacter("Devout", EAlignment.Good, ECharacterType.Villager, true, false, "\"Your greatest follower, or so she claims.\"");
        w_devout.role = new w_Devout();
        w_devout.description = $"<b>Game Start:</b>\n{formattedKeyText("Reveal")} me.\nLearn 1 Outcast character.";
        w_devout.hints = $"I can bypass misregistration.";
        w_devout.ifLies = $"I am still {formattedKeyText("Revealed")} on <b>Game Start</b>, but my info is false.";
        w_devout.gender = EGender.Female;
        */

        CharacterData w_devoutNew = newCharacter("Devout", EAlignment.Good, ECharacterType.Villager, true, false, "\"Your greatest follower, or so she claims.\"");
        w_devoutNew.role = new w_DevoutRework();
        w_devoutNew.description = $"<b>Pick 1 {formattedKeyText("Revealed")} character:</b>\nIf they have an Active Ability, they gain another use of it.\nOtherwise, they Act again.";
        w_devoutNew.hints = $"I cannot help other {roleColour("Villager")}Devout</color> claims.";
        w_devoutNew.ifLies = $"My ability still works, but I Corrupt the character I pick.";
        w_devoutNew.gender = EGender.Female;
        w_devoutNew.picking = true;
        w_devoutNew.abilityUsage = EAbilityUsage.Once;

        CharacterData w_sheriff = newCharacter("Sheriff", EAlignment.Good, ECharacterType.Villager, true, false, "\"Paid to deal with common criminals, not demons.\"");
        w_sheriff.role = new w_Sheriff();
        w_sheriff.description = "Learn 1 role that is being used as a Disguise.";
        w_sheriff.gender = EGender.Male;

        /*
        CharacterData w_slayerRework = new CharacterData();
        w_slayerRework.role = new w_SlayerRework();
        w_slayerRework.name = "Convict";
        w_slayerRework.description = $"<b>Pick 1 character:</b>\nThey {formattedKeyText("Die")}, but don't reveal their {formattedKeyText("True Role")}.\nLearn their alignment.\nIf Good picked, deal 2 {formattedKeyText("Damage")} to you.\nIf Evil picked, deal 1 {formattedKeyText("Damage")} to you.";
        w_slayerRework.flavorText = "\"Was scheduled for a trial, but then the Bombardier got sentenced.\"";
        w_slayerRework.hints = $"I cannot {formattedKeyText("Kill")} other {roleColour("Villager")}Convict</color> claims.\n\n{customHint("Ability Refresh Hint", "Once Per Game")}";
        w_slayerRework.ifLies = $"I still {formattedKeyText("Kill")} my target, but Lie about their alignment & deal {formattedKeyText("Damage")} accordingly.";
        w_slayerRework.picking = true;
        w_slayerRework.startingAlignment = EAlignment.Good;
        w_slayerRework.type = ECharacterType.Villager;
        w_slayerRework.abilityUsage = EAbilityUsage.Once;
        w_slayerRework.bluffable = true;
        w_slayerRework.characterId = "Slayer_WING";
        w_slayerRework.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_slayerRework.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_slayerRework.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_slayerRework.color = new Color(1f, 0.935f, 0.7302f);
        w_slayerRework.gender = EGender.Male;
        w_slayerRework.usuallyDisguised = false;
        w_slayerRework.additionalFlavorTexts = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(1);
        w_slayerRework.additionalFlavorTexts[0] = w_slayerRework.flavorText;
        */

        CharacterData w_performer = newCharacter("Performer", EAlignment.Good, ECharacterType.Villager, true, false, "\"Puts on a good show, but relies heavily on her background actors.");
        w_performer.role = new w_Performer();
        w_performer.description = "Learn 1 Evil character.\nIf I am adjacent to Evil, Learn false info.";
        w_performer.hints = "If I Learn false info due only to my ability, it does not count as Lying, as my ability is functioning as intended.";
        w_performer.ifLies = "My info is the opposite of what I would normally Learn.";
        w_performer.gender = EGender.Female;
        // w_performer.flavorText = "\"What a wonderful performance!\n...is what I would say if her background characters weren't saboteurs.\"";
        // w_performer.flavorText = "\"So what we learned today is the Muppets always lie.\"\n-@docomputernerd4096, YouTube"; // comment on https://www.youtube.com/watch?v=i3AV7WdGVVs

        CharacterData w_lamb = newCharacter("Lamb", EAlignment.Good, ECharacterType.Villager, true, false, "\"Looking for a shepherd.\nAn adequate one, that is.\"");
        w_lamb.role = new w_Lamb();
        w_lamb.description = "Learn how far from me to a particular Outcast.";
        w_lamb.hints = $"If an Outcast Disguised as me is {formattedKeyText("Truthful")} and is also the only Outcast, they will say that there are no Outcasts.";
        w_lamb.gender = EGender.Male;

        CharacterData w_introvert = newCharacter("Introvert", EAlignment.Good, ECharacterType.Villager, true, false, "\"People think she's nice to be around.\nShe hates it.\"");
        w_introvert.role = new w_Introvert();
        w_introvert.description = "Learn 2 characters that sit within 2 seats of me.";
        w_introvert.ifLies = "At least one of my statements is incorrect.";
        w_introvert.gender = EGender.Female;

        CharacterData w_chiromancer = newCharacter("Chiromancer", EAlignment.Good, ECharacterType.Villager, true, false, "\"She sees several possible futures, and none of them are good.\"");
        w_chiromancer.role = new w_Chiromancer();
        w_chiromancer.description = "Learn 3 characters and which Evil role each is.\nOnly 1 is correct.";
        w_chiromancer.ifLies = "None are correct.";
        w_chiromancer.gender = EGender.Female;

        CharacterData w_warden = newCharacter("Warden", EAlignment.Good, ECharacterType.Villager, true, false, "\"His interrogation skills are unmatched.\nHis note-taking, on the other hand...\"");
        w_warden.role = new w_Warden();
        w_warden.description = "<b>Pick 4 characters:</b>\nLearn which character type is the most common among them.";
        w_warden.hints = "If multiple types are tied for most common, you Learn this instead.\n\n" + customHint("Ability Refresh Hint", "Each Night") + $"\n\nArt by {formattedKeyText("Hiraeth")} ({formattedKeyText("@hiraeth")}) on {formattedKeyText("Discord")}";
        w_warden.picking = true;
        w_warden.abilityUsage = EAbilityUsage.ResetAfterNight;
        w_warden.gender = EGender.Male;

        CharacterData w_gossip = newCharacter("Gossip", EAlignment.Good, ECharacterType.Villager, true, false, "\"Did you hear? No?\nWell she certainly did.\"");
        w_gossip.role = new w_Gossip();
        w_gossip.description = "<b>Pick 1 character:</b>\nLearn random info about them.";
        w_gossip.hints = customHint("Ability Refresh Hint", "Each Night");
        w_gossip.picking = true;
        w_gossip.abilityUsage = EAbilityUsage.ResetAfterNight;
        w_gossip.gender = EGender.Female;

        CharacterData w_jewelsmith = newCharacter("Jewelsmith", EAlignment.Good, ECharacterType.Villager, true, false, "\"Lives with the Gemcrafter.\nThey share stories over their morning coffee.\"");
        w_jewelsmith.role = new w_Jewelsmith();
        w_jewelsmith.name = "Jewelsmith";
        w_jewelsmith.description = $"Learn 1 {formattedKeyText("Honest")} character.";
        w_jewelsmith.hints = $"{formattedKeyText("Honest")} is a keyword that means \"Not Disguised\".\n\nArt edit by {formattedKeyText("Astery")} ({formattedKeyText("@astery")}) on {formattedKeyText("Discord")}";
        w_jewelsmith.gender = EGender.Female;

        CharacterData w_ranger = newCharacter("Ranger", EAlignment.Good, ECharacterType.Villager, true, false, "\"He can sense his targets.\nBut only at optimal range.\"");
        w_ranger.role = new w_Ranger();
        w_ranger.description = "Learn how far from me to my furthest Evil.";
        w_ranger.gender = EGender.Male;

        CharacterData w_cardshark = newCharacter("Cardshark", EAlignment.Good, ECharacterType.Villager, true, false, "\"They say fortune favours the bold.\"");
        w_cardshark.role = new w_Cardshark();
        w_cardshark.description = $"<b>Pick 3 characters:</b>\nFor each Villager picked, deal 1 {formattedKeyText("Damage")} to you.\nFor each non-Villager picked, {formattedKeyText("Heal")} you for 1 {formattedKeyText("Health")}.";
        w_cardshark.hints = customHint("Ability Refresh Hint", "Once Per Game");
        w_cardshark.ifLies = "My ability works based on wrong info.";
        w_cardshark.picking = true;
        w_cardshark.abilityUsage = EAbilityUsage.Once;
        w_cardshark.gender = EGender.Female;
        w_cardshark.characterId = "Gambler_WING";

        CharacterData w_arbiter = newCharacter("Arbiter", EAlignment.Good, ECharacterType.Villager, true, false, "\"The scales tip, but in whose favour?\"");
        w_arbiter.role = new w_Arbiter();
        w_arbiter.description = "<b>Pick 1 character:</b>\nLearn if they are Disguised.";
        w_arbiter.hints = customHint("Ability Refresh Hint", "Each Night");
        w_arbiter.picking = true;
        w_arbiter.abilityUsage = EAbilityUsage.ResetAfterNight;
        w_arbiter.gender = EGender.Male;

        /*Faerie
        CharacterData w_fiDragonfly = new CharacterData();
        w_fiDragonfly.role = new w_FiDragonfly();
        w_fiDragonfly.name = "Faerie";
        w_fiDragonfly.description = "<b>Pick 1 character:</b>\nThey Disguise and are <b>Activated</b> again.";
        w_fiDragonfly.flavorText = "\"This cheeky little trickster is too cute for anyone to be mad at her.\"";
        w_fiDragonfly.hints = $"{customHint("Ability Refresh Hint", "Once Per Game")}\n\nCharacters who are both {formattedKeyText("Tricked")} and Corrupted appears as \"{formattedKeyText("Bewildered")}\".\n\nBased on {formattedKeyText("Fi")}'s OC, <color=#96EAFF>Fi</color>. ({formattedKeyText("@fithedragonfly")} on {formattedKeyText("Discord")})\nArt is a commission owned by them.";
        w_fiDragonfly.ifLies = "My target still Disguises, but they also become Corrupted and Lie.";
        w_fiDragonfly.picking = true;
        w_fiDragonfly.startingAlignment = EAlignment.Good;
        w_fiDragonfly.type = ECharacterType.Villager;
        w_fiDragonfly.abilityUsage = EAbilityUsage.Once;
        w_fiDragonfly.bluffable = true;
        w_fiDragonfly.characterId = "FiDragonfly_WING";
        w_fiDragonfly.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_fiDragonfly.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_fiDragonfly.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_fiDragonfly.color = new Color(1f, 0.935f, 0.7302f);
        w_fiDragonfly.gender = EGender.Female;
        w_fiDragonfly.usuallyDisguised = false;
        w_fiDragonfly.additionalFlavorTexts = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(1);
        w_fiDragonfly.additionalFlavorTexts[0] = w_fiDragonfly.flavorText;
        */

        CharacterData w_gravekeeper = newCharacter("Gravekeeper", EAlignment.Good, ECharacterType.Villager, true, false, "\"The spirits like him more than the Medium for some reason.\"");
        w_gravekeeper.role = new w_Gravekeeper();
        w_gravekeeper.description = $"<b>Activate Me:</b>\nLearn random info about every {formattedKeyText("Dead")} character.";
        w_gravekeeper.hints = customHint("Ability Refresh Hint", "Once Per Game") + $"\n\nArt by {formattedKeyText("LostIllustrator")} ({formattedKeyText("@lostillustrator")}) on {formattedKeyText("Discord")}";
        w_gravekeeper.ifLies = "At least one of my statements is false.";
        w_gravekeeper.picking = true;
        w_gravekeeper.abilityUsage = EAbilityUsage.Once;
        w_gravekeeper.gender = EGender.Male;

        CharacterData w_detective = newCharacter("Detective", EAlignment.Good, ECharacterType.Villager, true, false, "\"Nobody knows how she comes to her conclusions, but results are results.\"");
        w_detective.role = new w_Detective();
        w_detective.description = $"Learn how many Lying characters have not been {formattedKeyText("Revealed")} yet.";
        w_detective.hints = $"If I am {formattedKeyText("Revealed")} last, Learn that 1 of 2 characters is Lying.\nIn this situation, if everyone is Lying or everyone is telling the {formattedKeyText("Truth")}, \"I got nothing\".";
        w_detective.gender = EGender.Female;

        /* In hindsight: No, absolutely not.
        CharacterData w_agent = new CharacterData();
        w_agent.role = new w_Agent();
        w_agent.name = "Secret Agent";
        w_agent.description = $"Learn how many Evil characters are in each trisector.";
        w_agent.flavorText = "\"His bugs pick up on Evil meetings.\nThe Demon is beginning to notice.\"";
        w_agent.hints = $"\"Trisector\" refers to the three equal groups I split villages into. For instance, in a 9 card village, the groups are #1 to #3, #4 to #6 and #7 to #9.";
        w_agent.ifLies = "";
        w_agent.picking = false;
        w_agent.startingAlignment = EAlignment.Good;
        w_agent.type = ECharacterType.Villager;
        w_agent.bluffable = true;
        w_agent.characterId = "Agent_WING";
        w_agent.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_agent.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_agent.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_agent.color = new Color(1f, 0.935f, 0.7302f);
        */

        CharacterData w_copycat = newCharacter("Copycat", EAlignment.Good, ECharacterType.Villager, false, true, "\"Once challenged the Doppelganger to a copy contest.\nThe Witness is still having nightmares about it.\"");
        w_copycat.role = new w_Copycat();
        w_copycat.description = $"<b>Game Start:</b>\nGain 3 {formattedKeyText("Max Health")}.\n\nI Disguise as an in-play Villager.";
        w_copycat.hints = $"If I am turned into the {roleColour("Minion")}Puppet</color>, I {formattedKeyText("Reveal")} as the {roleColour("Villager")}Copycat</color>.\n\nArt by {formattedKeyText("Blue Cheesed")} ({formattedKeyText("@Blue Cheesed")}) on {formattedKeyText("Discord")}";
        w_copycat.gender = EGender.They;
        // w_copycat.flavorText = "\"The Doppelganger's twin sibling.\nOnly the Druid has noticed so far.\"";


        CharacterData w_bloodseer = newCharacter("Bloodseer", EAlignment.Good, ECharacterType.Villager, true, false, "\"Gathers a lot of info... for a price.\"");
        w_bloodseer.role = new w_Bloodseer();
        w_bloodseer.description = $"<b>Activate Me:</b>\nDeal 1 {formattedKeyText("Damage")} to you.\nLearn random info.";
        w_bloodseer.ifLies = $"I still deal {formattedKeyText("Damage")} to you, but my info is wrong.\n\nNote that I do not follow the usual Lying rules of the characters whose info I copy.";
        w_bloodseer.hints = $"Don't be afraid to sacrifice {formattedKeyText("Health")} for info! My ability may look intimidating, but if you're stumped, I can sometimes lend you a hand in exchange for a small sacrifice.\n\nMy ability can be used more than once.";
        w_bloodseer.gender = EGender.They;
        w_bloodseer.picking = true;
        w_bloodseer.abilityUsage = EAbilityUsage.ResetAfterNight;


        //CharacterData w_saint = newCharacter("Saint", EAlignment.Good, ECharacterType.Villager, false, false, "\"Wisdom begets peace. Patience begets wisdom. Fear not, for the time shall come when fear too shall pass.\nLet us pray, and may the unity of our vision make saints of us all.\"");
        //w_saint.role = new SaintVillager();
        //w_saint.description = $"I am always Good.";
        //w_saint.gender = EGender.Female;

        /*
        CharacterData w_greeter = new CharacterData();
        w_greeter.role = new w_Greeter();
        w_greeter.name = "Greeter";
        w_greeter.description = $"<b>Game Start:</b>\n1 random Outcast becomes a not-in-play Good Villager.\n\nLearn who I converted and what their previous role was.";
        w_greeter.flavorText = "\"Welcomes everyone with open arms.\nYes, <b>everyone</b>.\"";
        w_greeter.hints = $"The Demon can double-claim the role of the Outcast I converted.";
        w_greeter.ifLies = "";
        w_greeter.picking = false;
        w_greeter.startingAlignment = EAlignment.Good;
        w_greeter.type = ECharacterType.Villager;
        w_greeter.bluffable = true;
        w_greeter.characterId = "Greeter_WING";
        w_greeter.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_greeter.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_greeter.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_greeter.color = new Color(1f, 0.935f, 0.7302f);

        CharacterData w_guardsman = new CharacterData();
        w_guardsman.role = new w_Guardsman();
        w_guardsman.name = "Guardsman";
        w_guardsman.description = $"Learn 1 role that is not in-play.";
        w_guardsman.flavorText = "\"Pretends to know what he's doing.\nNot very good at his job.\"";
        w_guardsman.hints = $"";
        w_guardsman.ifLies = "";
        w_guardsman.picking = false;
        w_guardsman.startingAlignment = EAlignment.Good;
        w_guardsman.type = ECharacterType.Villager;
        w_guardsman.bluffable = true;
        w_guardsman.characterId = "Guardsman_WING";
        w_guardsman.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_guardsman.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_guardsman.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_guardsman.color = new Color(1f, 0.935f, 0.7302f);

        CharacterData w_bartender = new CharacterData();
        w_bartender.role = new w_Bartender();
        w_bartender.name = "Bartender";
        w_bartender.description = $"Learn which roles sit adjacent to a random Corrupted character.";
        w_bartender.flavorText = "\"Beginning to grow concerned about his most frequent customer.\"";
        w_bartender.hints = $"";
        w_bartender.ifLies = "";
        w_bartender.picking = false;
        w_bartender.startingAlignment = EAlignment.Good;
        w_bartender.type = ECharacterType.Villager;
        w_bartender.bluffable = true;
        w_bartender.characterId = "Bartender_WING";
        w_bartender.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_bartender.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_bartender.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_bartender.color = new Color(1f, 0.935f, 0.7302f);
        */

        // WILL IMPLEMENT THESE LATER





        /*

        CharacterData w_dreamernt = new CharacterData();
        w_dreamernt.role = new w_DreamerConcept();
        w_dreamernt.name = "Dreamern't";
        w_dreamernt.description = "Ability, yippee";
        w_dreamernt.flavorText = "\"Something or other\"";
        w_dreamernt.hints = "I'm like a Dreamer, but not quite";
        w_dreamernt.ifLies = "";
        w_dreamernt.picking = true;
        w_dreamernt.startingAlignment = EAlignment.Good;
        w_dreamernt.type = ECharacterType.Villager;
        w_dreamernt.abilityUsage = EAbilityUsage.ResetAfterNight;
        w_dreamernt.bluffable = true;
        w_dreamernt.characterId = "Dreamer_WING";
        w_dreamernt.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_dreamernt.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_dreamernt.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_dreamernt.color = new Color(1f, 0.935f, 0.7302f);
        w_dreamernt.gender = EGender.Female;
        w_dreamernt.usuallyDisguised = false;


        */





        /*CharacterData w_insomniac = new CharacterData(); // Changing the character state to ECharacterState.Hidden doesn't work :(
        w_insomniac.role = new w_Insomniac();
        w_insomniac.name = "Insomniac";
        w_insomniac.description = "<b>Pick 1 alive revealed character:</b>\n<b>Hide</b> them.";
        w_insomniac.flavorText = "\"Wishes the village would shut up so that she can get a good night's sleep.\"";
        w_insomniac.hints = abilityRefreshHint(EAbilityUsage.Once);
        w_insomniac.ifLies = "I still <b>Hide</b> my target, but I also Corrupt them.";
        w_insomniac.picking = true;
        w_insomniac.startingAlignment = EAlignment.Good;
        w_insomniac.type = ECharacterType.Villager;
        w_insomniac.abilityUsage = EAbilityUsage.Once;
        w_insomniac.bluffable = true;
        w_insomniac.characterId = "Insomniac_WING";
        w_insomniac.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_insomniac.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_insomniac.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_insomniac.color = new Color(1f, 0.935f, 0.7302f);
        */

        /* Couldn't figure out how to make him detect current remaining health.
        CharacterData w_sidekick = new CharacterData();
        w_sidekick.role = new w_Sidekick();
        w_sidekick.name = "Sidekick";
        w_sidekick.description = "<b>Activate Me:</b>\nLearn 1 Good character.\nIf you have less than 5 health, Learn 1 Evil character instead.";
        w_sidekick.flavorText = "\"He's forever adaptive and always there to help his friends in need!\"";
        w_sidekick.hints = "I only Learn an Evil character if you have <i>less than</i> 5 health.";
        w_sidekick.ifLies = "";
        w_sidekick.picking = true;
        w_sidekick.startingAlignment = EAlignment.Good;
        w_sidekick.type = ECharacterType.Villager;
        w_sidekick.abilityUsage = EAbilityUsage.Once;
        w_sidekick.bluffable = true;
        w_sidekick.characterId = "Sidekick_WING";
        w_sidekick.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_sidekick.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_sidekick.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_sidekick.color = new Color(1f, 0.935f, 0.7302f);
        */

        /*
        CharacterData w_politician = new CharacterData();
        w_politician.role = new w_Politician();
        w_politician.name = "Politician";
        w_politician.description = "<b>Game Start:</b>\nI am Corrupted.\n\nBeing Corrupted does not make me Lie.\nLearn random false info.";
        w_politician.flavorText = "\"He just showed up one day and started bossing people around.\"";
        w_politician.hints = "I cannot be Cured.";
        w_politician.ifLies = "I do not become Corrupted.\nMy info is true instead.\nI still Register as Lying.";
        w_politician.picking = false;
        w_politician.startingAlignment = EAlignment.Good;
        w_politician.type = ECharacterType.Villager;
        w_politician.bluffable = true;
        w_politician.characterId = "Politician_WING";
        w_politician.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_politician.cardBgColor = new Color(0.26f, 0.1519f, 0.3396f);
        w_politician.cardBorderColor = new Color(0.7133f, 0.339f, 0.8679f);
        w_politician.color = new Color(1f, 0.935f, 0.7302f);
        */

        /*
        //CharacterData w_caravan = new CharacterData();
        //w_caravan.role = new w_Caravan();
        //w_caravan.name = "Caravan";
        //w_caravan.description = "2 characters Register as each other's role and alignment.\nLearn 1 of their roles.\n\n<b>Execute Me:</b>\nLearn the other role I affected.";
        //w_caravan.flavorText = "\"Nobody knows where the caravan goes.\nExcept the driver, that is.\"";
        //w_caravan.hints = "";
        //w_caravan.ifLies = "";
        //w_caravan.picking = false;
        //w_caravan.startingAlignment = EAlignment.Good;
        //w_caravan.type = ECharacterType.Outcast;
        //w_caravan.bluffable = false;
        //w_caravan.characterId = "Caravan_WING";
        //w_caravan.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_caravan.cardBgColor = new Color(0.1020f, 0.0667f, 0.0392f);
        //w_caravan.cardBorderColor = new Color(0.7843f, 0.6471f, 0.0f);
        //w_caravan.color = new Color(0.9647f, 1f, 0.4471f);
        //Characters.Instance.startGameActOrder = insertAfterAct("Chancellor", w_caravan);

        //CharacterData w_henchman = new CharacterData();
        //w_henchman.role = new w_Henchman();
        //w_henchman.name = "Henchman";
        //w_henchman.description = "I start Evil, then become the alignment of the 1st character you Execute.";
        //w_henchman.flavorText = "\"His favourite phrases include 'Got it, boss!', 'You're the boss, boss.' and 'You got a problem with the boss?!'\"";
        //w_henchman.hints = "I only Lie if I am Corrupted.\n\nI have a 50% chance to be Corrupted.\n\nTake 2 less damage when you Execute me.";
        //w_henchman.ifLies = "";
        //w_henchman.picking = false;
        //w_henchman.startingAlignment = EAlignment.Good;
        //w_henchman.type = ECharacterType.Outcast;
        //w_henchman.bluffable = false;
        //w_henchman.characterId = "Henchman_WING";
        //w_henchman.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_henchman.cardBgColor = new Color(0.1020f, 0.0667f, 0.0392f);
        //w_henchman.cardBorderColor = new Color(0.7843f, 0.6471f, 0.0f);
        //w_henchman.color = new Color(0.9647f, 1f, 0.4471f);
        */

        CharacterData w_chatterbox = newCharacter("Chatterbox", EAlignment.Good, ECharacterType.Outcast, true, false, "\"Some people can whisper secrets.\nSome people never shut up.\"");
        w_chatterbox.role = new w_Chatterbox();
        w_chatterbox.description = $"<b>Reveal:</b>\n1 {formattedKeyText("Unrevealed")} Good Villager becomes Corrupted, if possible.";
        w_chatterbox.gender = EGender.Female;

        CharacterData w_lunatic = newCharacter("Lunatic", EAlignment.Good, ECharacterType.Outcast, false, true, "\"Constantly attempts to search everyone's shadows for the truth.\nMight have taken the Poet's writings a bit too literally.\"");
        w_lunatic.role = new w_Lunatic();
        w_lunatic.description = "I Disguise.\nI have a 50% chance to be Corrupted.";
        w_lunatic.hints = $"I only Lie if I am Corrupted.\nThe {roleColour("Villager")}Alchemist</color> cannot {formattedKeyText("Cure")} me.\n\nTake 2 less {formattedKeyText("Damage")} when you Execute me.\n\n" + customHint("Outcast Disguise Hint", "Advanced") + $"\n\nArt by {formattedKeyText("WeekendWolf")} ({formattedKeyText("@weekendwolf")}) on {formattedKeyText("Discord")}";
        w_lunatic.gender = EGender.Male;

        CharacterData w_marionette = newCharacter("Marionette", EAlignment.Good, ECharacterType.Outcast, false, true, "\"You ever feel like you're losing control over your life?\"");
        w_marionette.role = new w_Marionette();
        w_marionette.description = "<b>Game Start:</b>\nI sit next to the Demon, if possible.\n\nI Register as a <color=#FF9999>Puppet</color>.\nI Lie and Disguise.";
        w_marionette.hints = $"You take 2 less {formattedKeyText("Damage")} if you Execute me.\n\nIf the {roleColour("Minion")}Chancellor</color> creates me, I immediately move to be next to the Demon.\nThis can result in a {roleColour("Minion")}Chancellor</color> with no Outcast neighbours.\n\n" + customHint("Outcast Disguise Hint", "Simple");
        w_marionette.gender = EGender.They;

        /*
        //CharacterData w_occultist = new CharacterData();
        //w_occultist.role = new w_Occultist();
        //w_occultist.name = "Occultist";
        //w_occultist.description = "<b>Game Start:</b>\nI become a random Good Minion.\nYou have 2 extra Max Health.";
        //w_occultist.flavorText = "\"They tampered with forces beyond their control and got what they deserved.\"";
        //w_occultist.hints = "I have the ability of the Minion I become, and also copy their Lying and Disguising behaviour.\nWhen I am Executed, I show up with the Minion's Evil colours and art, but with the name \'Occultist\'. Don't get confused.";
        //w_occultist.ifLies = "";
        //w_occultist.picking = false;
        //w_occultist.startingAlignment = EAlignment.Good;
        //w_occultist.type = ECharacterType.Outcast;
        //w_occultist.bluffable = false;
        //w_occultist.characterId = "Occultist_WING";
        //w_occultist.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_occultist.cardBgColor = new Color(0.1020f, 0.0667f, 0.0392f);
        //w_occultist.cardBorderColor = new Color(0.7843f, 0.6471f, 0.0f);
        //w_occultist.color = new Color(0.9647f, 1f, 0.4471f);
        //Characters.Instance.startGameActOrder = insertAfterAct("Baa", w_occultist);
        */

        CharacterData w_renegade = newCharacter("Renegade", EAlignment.Evil, ECharacterType.Outcast, false, true, "\"She doesn't like the village, but she doesn't like the demons either.\"");
        w_renegade.role = new w_Renegade();
        w_renegade.description = "I am Evil.\nI Lie and Disguise.";
        w_renegade.hints = customHint("Outcast Disguise Hint", "Advanced");
        w_renegade.gender = EGender.Female;

        CharacterData w_revolutionary = newCharacter("Revolutionary", EAlignment.Good, ECharacterType.Outcast, false, true, "\"Has attempted to kill the Empress on multiple occasions.\nUsually gets held back by the Knight or Slayer.\"");
        w_revolutionary.role = new w_Revolutionary();
        w_revolutionary.description = $"<b>On Death:</b>\nI Execute an {formattedKeyText("Alive")} Villager, if possible.\n\nI Disguise.";
        w_revolutionary.hints = $"You take 3 {formattedKeyText("Damage")} instead of 5 when you Execute me.\nI may attempt to Execute a <color=#C080FF>Knight</color>, but I cannot {formattedKeyText("Kill")} him.\n\n" + customHint("Outcast Disguise Hint", "Advanced");
        w_revolutionary.gender = EGender.Male;

        /*                                                                                         DISABLED FOR NOW DUE TO ISSUES
        CharacterData w_pilgrim = new CharacterData();
        w_pilgrim.role = new w_Pilgrim();
        w_pilgrim.name = "Pilgrim";
        w_pilgrim.description = $"<b>Game Start:</b>\n1 random character Disguises as the {roleColour("Outcast")}Pilgrim</color>.";
        w_pilgrim.flavorText = "\"Their religion makes them suspects, but they don't have any useful abilities or information.\"";
        w_pilgrim.hints = "I cannot be Disguised as naturally.\nThe only way for a character to Disguise as me is to be forced by me to do so.";
        w_pilgrim.ifLies = "";
        w_pilgrim.picking = false;
        w_pilgrim.startingAlignment = EAlignment.Good;
        w_pilgrim.type = ECharacterType.Outcast;
        w_pilgrim.bluffable = false;
        w_pilgrim.characterId = "Pilgrim_WING";
        w_pilgrim.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_pilgrim.cardBgColor = new Color(0.1020f, 0.0667f, 0.0392f);
        w_pilgrim.cardBorderColor = new Color(0.7843f, 0.6471f, 0.0f);
        w_pilgrim.color = new Color(0.9647f, 1f, 0.4471f);
        w_pilgrim.gender = EGender.They;
        w_pilgrim.usuallyDisguised = false;
        */

        CharacterData w_mutant = newCharacter("Mutant", EAlignment.Good, ECharacterType.Outcast, false, false, "\"It seems volatile.\nNo, not he, <b>it</b> seems volatile.\"");
        w_mutant.role = new w_Mutant();
        w_mutant.description = $"<b>Game Start:</b>\n1 Minion is added to the {formattedKeyText("Deck")}.\nIt is only in-play if I am Good.\nI have a 50% chance to become Evil.";
        w_mutant.hints = "I cannot be Disguised as.";
        w_mutant.gender = EGender.Male;
        w_mutant.additionalPossibleCharacters.count.Add(NewPossibleCharacterCount(ECharacterType.Minion, 1));


        CharacterData w_turncoat = newCharacter("Turncoat", EAlignment.Evil, ECharacterType.Minion, false, true, "\"Hurt me with the truth, but don't comfort me with a lie.\"");
        w_turncoat.role = new w_Turncoat();
        w_turncoat.description = "I Disguise as an in-play character.";
        w_turncoat.hints = $"I am uniquely capable of Disguising as {roleColour("GoodMinion")}Swarm</color>. I do not copy its <b>Game Start</b> ability, however.\n\nI Register as Lying if (and only if) I am Disguised as the {roleColour("Villager")}Knight</color> or the {roleColour("Outcast")}Bombardier</color>, whose abilities I cannot gain.\nOtherwise, I Register as {formattedKeyText("Truthful")}.";
        w_turncoat.gender = EGender.Male;
        w_turncoat.usuallyDisguised = true;

        CharacterData w_swarm_good = newCharacter("Swarm", EAlignment.Good, ECharacterType.Minion, false, false, "\"This one seems defective.\nPerhaps they can help you.\"");
        w_swarm_good.role = new w_Swarm_Good();
        w_swarm_good.description = $"<b>Game Start:</b>\n2 other Good Villagers become {roleColour("Minion")}Swarm</color>. Both are Evil.\n\nLearn 4 characters, 2 of which are {roleColour("Minion")}Swarm</color>.";
        w_swarm_good.hints = customHint("Interactions", "Good Swarm");
        w_swarm_good.ifLies = "I stay silent.";
        w_swarm_good.characterId = "Swarm_Good_WING";
        w_swarm_good.gender = EGender.They;
        w_swarm_good.additionalPossibleCharacters.count.Add(NewPossibleCharacterCount(ECharacterType.Minion, 2));

        CharacterData w_swarm_evil = newCharacter("Swarm", EAlignment.Evil, ECharacterType.Minion, false, true, "\"Keep your guard up, or they'll outnumber and overwhelm you.\"");
        w_swarm_evil.role = new w_Swarm_Evil();
        w_swarm_evil.description = $"<b>Game Start:</b>\n2 other Good Villagers become {roleColour("Minion")}Swarm</color>. One is Good, one is Evil.\n\nI Lie and Disguise.";
        w_swarm_evil.hints = customHint("Interactions", "Clone Evil");
        w_swarm_evil.characterId = "Swarm_Evil_WING";
        w_swarm_evil.gender = EGender.They;

        /*
        //CharacterData w_snakecharmer = new CharacterData();
        //w_snakecharmer.role = new w_SnakeCharmer();
        //w_snakecharmer.name = "Snake Charmer";
        //w_snakecharmer.description = "<b>Game Start:</b>\n1 Good Villager is Corrupted.\n\n<b>Execute Me:</b>\nLearn the role of the Villager I Corrupted.\n\nI Lie and Disguise.";
        //w_snakecharmer.flavorText = "\"The snakes aren't deadly, but their venom still hurts.\"";
        //w_snakecharmer.hints = "";
        //w_snakecharmer.ifLies = "";
        //w_snakecharmer.picking = false;
        //w_snakecharmer.startingAlignment = EAlignment.Evil;
        //w_snakecharmer.type = ECharacterType.Minion;
        //w_snakecharmer.bluffable = false;
        //w_snakecharmer.characterId = "SnakeCharmer_WING";
        //w_snakecharmer.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_snakecharmer.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        //w_snakecharmer.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        //w_snakecharmer.color = new Color(1f, 0.3804f, 0.3804f);
        //Characters.Instance.startGameActOrder = insertAfterAct("Poisoner", w_snakecharmer);
        */

        CharacterData w_undying = newCharacter("Undying", EAlignment.Evil, ECharacterType.Minion, false, false, "\"Laughs in the face of death.\nThat is, until it has no teammates to fall back on.\"");
        w_undying.role = new w_Undying();
        w_undying.description = $"<b>Game Start:</b>\n1 Good Villager might become a {roleColour("Minion")}Fanatic</color>.\n\n<b>Execute Me:</b>\nIf other Evil characters still live, I don't die; deal 4 {formattedKeyText("Damage")} to you.";
        w_undying.hints = $"Attempting to {formattedKeyText("Kill")} me using a character ability counts as Executing me.\n<i>You're not getting me that easily.</i>";
        w_undying.gender = EGender.Male;
        w_undying.additionalPossibleCharacters.count.Add(NewPossibleCharacterCount(ECharacterType.Minion, 1));

        CharacterData w_professional = newCharacter("Professional", EAlignment.Evil, ECharacterType.Minion, false, true, "\"If you're going to be committing crimes, you really ought to clean up the mess behind you.\"");
        w_professional.role = new w_Professional();
        w_professional.description = "I Lie and Disguise.\nI Register as Good and as my Disguise.";
        w_professional.hints = $"If I am Disguised as the {roleColour("Villager")}Confessor</color>, I will say \"I am Good\".\nI can also be converted by the {roleColour("Villager")}Baker</color>. If this happens, I will appear 'Evil' when Executed, and will not convert anyone else.";
        w_professional.gender = EGender.Male;

        CharacterData w_saboteur = newCharacter("Saboteur", EAlignment.Evil, ECharacterType.Minion, false, true, "\"The Architect has been trying to work out what was wrong with the Gemcrafter's house for weeks.\nHere's a hint: Sabotage.\"");
        w_saboteur.role = new w_Saboteur();
        w_saboteur.description = "<b>Game Start:</b>\n1 Good Villager furthest away from me is Corrupted.\n\nI Lie and Disguise.";
        w_saboteur.hints = "I Corrupt the Good Villager furthest away from me, but they do not need to be directly opposite to me.";
        w_saboteur.gender = EGender.Female;

        CharacterData w_heretic = newCharacter("Heretic", EAlignment.Evil, ECharacterType.Minion, false, true, "The Bishop is wondering who keeps turning all the church's crucifixes upside-down");
        w_heretic.role = new w_Heretic();
        w_heretic.description = $"<b>Game Start:</b>\n2 Minions & 2 Demons are added to the {formattedKeyText("Deck")}.\n\nI Lie and Disguise.";
        w_heretic.gender = EGender.Female;

        /*CharacterData w_toxomancer = new CharacterData();
        w_toxomancer.role = new w_Toxomancer();
        w_toxomancer.name = "Toxomancer";
        w_toxomancer.description = "For every 3 cards you <b>Reveal</b>, I <color=#009900>Poison</color> 1 Good Villager.\nDeal 1 damage to you when they die.\n\nI Lie and Disguise.";
        w_toxomancer.flavorText = "\"Most Minions like to keep it subtle.\nThis one prefers to go loud.\"";
        w_toxomancer.hints = "<color=#009900>Poisoned</color> characters Lie and are Corrupted.\nAfter 2 cards are Revealed, they die, even if the <color=#FF9999>Toxomancer</color> is dead.\n\nIf a character is <color=#009900>Poisoned</color> after their ability has already been used, their information does not change.";
        w_toxomancer.ifLies = "";
        w_toxomancer.picking = false;
        w_toxomancer.startingAlignment = EAlignment.Evil;
        w_toxomancer.type = ECharacterType.Minion;
        w_toxomancer.bluffable = false;
        w_toxomancer.characterId = "Toxomancer_WING";
        w_toxomancer.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_toxomancer.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        w_toxomancer.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        w_toxomancer.color = new Color(0.8510f, 0.4549f, 0.0f);*/

        /*//CharacterData w_disguiser = new CharacterData();
        //w_disguiser.role = new w_Disguiser();
        //w_disguiser.name = "Disguiser";
        //w_disguiser.description = "1 Good Villager Disguises.\n\nI Lie and Disguise.";
        //w_disguiser.flavorText = "\"A silver tongue, some sweet words and a good disguise are all they need to trick someone.\"";
        //w_disguiser.hints = "The Villager caused to Disguise by me follows standard Minion Disguise rules.\nThey do NOT Lie unless they for some reason already would.";
        //w_disguiser.ifLies = "";
        //w_disguiser.picking = false;
        //w_disguiser.startingAlignment = EAlignment.Evil;
        //w_disguiser.type = ECharacterType.Minion;
        //w_disguiser.bluffable = false;
        //w_disguiser.characterId = "Disguiser_WING";
        //w_disguiser.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_disguiser.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        //w_disguiser.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        //w_disguiser.color = new Color(1f, 0.3804f, 0.3804f);
        //Characters.Instance.startGameActOrder = insertAfterAct("Shaman", w_disguiser);

        //CharacterData w_chronomancer = new CharacterData();
        //w_chronomancer.role = new w_Chronomancer();
        //w_chronomancer.name = "Chronomancer";
        //w_chronomancer.description = "I Lie and Disguise. The <color=#C080FF>toaster</color> couldn't figure out how to get my ability working.";
        //w_chronomancer.flavorText = "\"Oh, how the time flies by... or does it?\"";
        //w_chronomancer.hints = "";
        //w_chronomancer.ifLies = "";
        //w_chronomancer.picking = false;
        //w_chronomancer.startingAlignment = EAlignment.Evil;
        //w_chronomancer.type = ECharacterType.Minion;
        //w_chronomancer.bluffable = false;
        //w_chronomancer.characterId = "Chronomancer_WING";
        //w_chronomancer.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_chronomancer.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        //w_chronomancer.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        //w_chronomancer.color = new Color(0.8510f, 0.4549f, 0.0f);
        
        //CharacterData w_cardshark = new CharacterData();
        //w_cardshark.role = new w_Cardshark();
        //w_cardshark.name = "Cardshark";
        //w_cardshark.description = "<b>Game Start:</b>\nAll Minions are in the Deck.\n\nI Lie and Disguise.";
        //w_cardshark.flavorText = "\"Her sleight of hand got her accused of being a witch once.\nClose, but not quite.\"";
        //w_cardshark.hints = "";
        //w_cardshark.ifLies = "";
        //w_cardshark.picking = false;
        //w_cardshark.startingAlignment = EAlignment.Evil;
        //w_cardshark.type = ECharacterType.Minion;
        //w_cardshark.bluffable = false;
        //w_cardshark.characterId = "Cardshark_WING";
        //w_cardshark.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_cardshark.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        //w_cardshark.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        //w_cardshark.color = new Color(0.8510f, 0.4549f, 0.0f);
        //Characters.Instance.startGameActOrder = insertAfterAct("Shaman", w_cardshark);
        */

        CharacterData w_acolyte = newCharacter("Acolyte", EAlignment.Evil, ECharacterType.Minion, false, true, "\"If you ask the Acolytes, the Demons are the good guys.\nThey really aren't.\"");
        w_acolyte.role = new w_Acolyte();
        w_acolyte.description = "I am an Evil created by another character's ability.\nI Lie and Disguise as an out-of-play character.";
        w_acolyte.hints = customHint("Interactions", "Clone Evil");
        w_acolyte.gender = EGender.They;

        CharacterData w_fanatic = newCharacter("Fanatic", EAlignment.Evil, ECharacterType.Minion, false, true, "\"How many of these guys <i>are</i> there?!\"");
        w_fanatic.role = new w_Fanatic();
        w_fanatic.description = "I am an Evil created by another character's ability.\nI Lie and Disguise.";
        w_fanatic.hints = customHint("Interactions", "Clone Evil");
        w_fanatic.gender = EGender.They;

        CharacterData w_zealot = newCharacter("Zealot", EAlignment.Evil, ECharacterType.Minion, false, true, "\"You got what you wished for, genius.\nNow you live with a demon. Happy now?\"");
        w_zealot.role = new w_Zealot();
        w_zealot.description = "I am an Evil created by another character's ability.\nI Lie and Disguise as an in-play character.";
        w_zealot.hints = $"Art by {formattedKeyText("Blue Cheesed")} ({formattedKeyText("@Blue Cheesed")}) on {formattedKeyText("Discord")}";
        w_zealot.gender = EGender.They;

        CharacterData w_legion = newCharacter("Agmeres", EAlignment.Evil, ECharacterType.Demon, false, true, "\"A legion of Evil rises up against the village.\nCan Good pull through?\"");
        w_legion.role = new w_Legion();
        //w_legion.name = "Agmeres"; // Name derived from Latin 'Agmen' meaning 'Army' and 'Plures' meaning 'Outnumber'.
        w_legion.description = $"<b>Setup:</b>\nMost characters are Minions.\n\n<b>Game Start:</b>\nYou have 2 less {formattedKeyText("Max Health")}.\n\n<b>At Night:</b>\nDeal 2 {formattedKeyText("Damage")} to you. Lose 2 {formattedKeyText("Max Health")}.\n\n{formattedKeyText("Lose")} if all Good characters {formattedKeyText("Die")}, even if I'm {formattedKeyText("Dead")}.\n\nI Lie and Disguise.";
        //w_legion.flavorText = "\"They are the chill wind on a winter's day. They are the shadow in the moonless night. They are the poison in your tea and the whisper in your ear. They are everywhere.\"";
        w_legion.hints = customHint("Keyword", "Setup");
        w_legion.characterId = "Legion_WING";
        w_legion.gender = EGender.They;
        nightPhase.nightCharactersOrder.Add(w_legion);

        CharacterData w_praesect = newCharacter("Praesect", EAlignment.Evil, ECharacterType.Demon, false, true, "\"Nobody knows if its acolytes are hypnotised, or if they're just morons.\"");
        w_praesect.role = new w_Praesect();
        //w_praesect.name = "Praesect"; // Name derived from Latin "praefectus" meaning "officer" and "sectator" meaning "follower"
        w_praesect.description = $"<b>Game Start:</b>\n2 Good Villagers become an {roleColour("Minion")}Acolyte</color> and a {roleColour("Minion")}Zealot</color>.\n\n<b>Execute Me:</b>\nDeal 2 {formattedKeyText("Damage")} to you per {formattedKeyText("Living")} {roleColour("Minion")}Acolyte</color>, {roleColour("Minion")}Fanatic</color> or {roleColour("Minion")}Zealot</color>.\n\nI Lie and Disguise.";
        w_praesect.flavorText = "\"Nobody knows if its acolytes are hypnotised, or if they're just morons.\"";
        w_praesect.gender = EGender.They;
        w_praesect.additionalPossibleCharacters.count.Add(NewPossibleCharacterCount(ECharacterType.Minion, 2));

        CharacterData w_twindemon = newCharacter("Veniyon", EAlignment.Evil, ECharacterType.Demon, false, true, "\"Minion and Twin Minion's older siblings.\nThe younger twins see their siblings as role models.\"");
        w_twindemon.role = new w_TwinDemon();
        w_twindemon.description = $"<b>Game Start:</b>\n1 Good Villager closest to me becomes Corrupted.\n\nI Lie and Disguise like a Minion.";
        // w_twindemon.hints = "TRIVIA:\nUsed to be called 'Twin Demon'.\nThen, she and her brother were renamed to Hellspawn and Twin Hellspawn, before eventually being renamed again.";
        w_twindemon.hints = $"When going up against us, try to work out which of us are in-play and which of us is which. The order of your operations will be especially important here...";
        w_twindemon.characterId = "TwinDemon_WING";
        //nightPhase.nightCharactersOrder.Add(w_twindemon);
        w_twindemon.gender = EGender.Female;

        CharacterData w_twindemontwin = newCharacter("Vidiyon", EAlignment.Evil, ECharacterType.Demon, false, true, "\"Claims to be independent.\nHis twin sister doesn't believe him.\"");
        w_twindemontwin.role = new w_TwinDemonTwin();
        w_twindemontwin.description = $"<b>Game Start:</b>\nCorrupted Good Villagers become Evil.\nIf none are, 1 Good Villager becomes Evil & Corrupted.\n\nI Lie and Disguise.";
        //w_twindemontwin.hints = "TRIVIA:\nWasn't originally planned to have a unique ability.\nHe has no horns due to a birth defect. He likes it, it means he can fit through doors and blend in more easily.";
        w_twindemontwin.hints = $"Characters turned Evil by my ability will appear \"{formattedKeyText("Misled")}\" when Executed.\n\nThe {roleColour("Villager")}Alchemist</color> will not try to {formattedKeyText("Cure")} the character I mislead if nobody is Corrupted.";
        w_twindemontwin.characterId = "TwinDemonTwin_WING";
        w_twindemontwin.gender = EGender.Male;

        CharacterData w_twindemontriplet = newCharacter("Viciyon", EAlignment.Evil, ECharacterType.Demon, false, true, "\"The eldest of the Hellspawn.\nShe doesn't like to associate with her siblings.\"");
        w_twindemontriplet.role = new w_TwinDemonThree();
        // w_twindemontriplet.description = $"<b>At Night:</b>\nDeal 1 {formattedKeyText("Damage")} to you.\nIf it's my first night, deal 2 instead.\n\nWe Lie and Disguise.";
        w_twindemontriplet.description = $"<b>On Death:</b>\nTake 1 {formattedKeyText("Damage")}, then 1 extra per {formattedKeyText("Living")} Evil character.\n\nI Lie and Disguise.";
        //w_twindemontriplet.hints = "I can only be in-play if both <color=#FF9999>Veniyon</color> and <color=#FF9999>Viciyon</color> are too.\n\nTRIVIA:\nUsed to be called 'Triplet Hellspawn' before being renamed alongside the other two.\nWas created when <color=#C080FF>Wingidon</color> saw <color=#FF9999>Summoner</color> (a character from a different mod) copy <color=#FF9999>Veniyon</color>'s ability, creating two instances of <color=#d88c8b>Vidiyon</color>.";
        w_twindemontriplet.hints = $"My ability ignores misregistration.\nThe {roleColour("Minion")}Undying</color> does not count for my ability.\n\n{roleColour("Demon")}Veniyon</color>, {roleColour("Demon")}Vidiyon</color> and {roleColour("Demon")}I</color> are the Hellspawn Triplets. We are all in the {formattedKeyText("Deck")}, but some of us might not be in-play.";
        w_twindemontriplet.characterId = "TwinDemonTriplet_WING";
        //var w_twinDemonTwinCharacter2 = ProjectContext.Instance.gameData.GetCharacterDataOfId("TwinDemonTriplet_WING");
        //nightPhase.nightCharactersOrder.Add(w_twindemontriplet);
        w_twindemontriplet.gender = EGender.Female;

        /*                                                                                         DISABLED FOR NOW DUE TO ISSUES
        CharacterData w_illusionist = new CharacterData();
        w_illusionist.role = new w_Illusionist();
        w_illusionist.name = "Emenverax"; // Name derived from Latin "ementior" meaning 'Feign' and 'Verax' meaning 'True'. Was once planned to be named "Emenumbra", deriving the second half from "Umbra" meaning shadow, darkness, etc.
        w_illusionist.description = $"1 Good Villager Disguises as {roleColour("Demon")}Emenverax</color>.\nTake 3 extra {formattedKeyText("Damage")} if you Execute them.";
        w_illusionist.flavorText = "\"Everyone hates them, but nobody is sure who to hate.\"";
        w_illusionist.hints = $"I add 3 fake Villagers and 1 fake Minion to the Deck to compensate for how loud I am.\n\nIf the {roleColour("Villager")}Baker</color> converts the character I cast my illusion over, my illusion is shattered.\n<i>I do not know <b>what</b> is up with that woman's power, but she scares me.</i>";
        w_illusionist.ifLies = "";
        w_illusionist.picking = false;
        w_illusionist.startingAlignment = EAlignment.Evil;
        w_illusionist.type = ECharacterType.Demon;
        w_illusionist.bluffable = false;
        w_illusionist.characterId = "Illusionist_WING";
        w_illusionist.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_illusionist.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        w_illusionist.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        w_illusionist.color = new Color(1f, 0.3804f, 0.3804f);
        w_illusionist.gender = EGender.They;
        w_illusionist.usuallyDisguised = false;
        */

        CharacterData w_caedoc = newCharacter("Caedoccidere", EAlignment.Evil, ECharacterType.Demon, false, true, "\"Most demons wait until they have enough power to destroy the entire town at once.\nThis one doesn't.\"");
        w_caedoc.role = new w_Caedoccidere();
        //w_caedoc.name = "Caedoccidere"; // Name derived from Latin 'caedes' (meaning "slaughter) and 'occidere' (meaning "to kill")
        w_caedoc.description = $"<b>At Night:</b>\n{formattedKeyText("Kill")} 1 {formattedKeyText("Revealed")} character.\nDeal 3 {formattedKeyText("Damage")} to you.\n\nI Lie and Disguise.";
        w_caedoc.hints = $"I do not {formattedKeyText("Kill")} Evil characters, even if they are my only valid targets.";
        w_caedoc.gender = EGender.They;
        nightPhase.nightCharactersOrder.Add(w_caedoc);

        CharacterData w_invertDemon = newCharacter("Mendaverte", EAlignment.Evil, ECharacterType.Demon, false, true, "\"You and your so-called Good and Evil...\nSuch arbitrary divisors, is it really that black-and-white to you?\"");
        w_invertDemon.role = new w_InvertDemon();
        //w_invertDemon.name = "Mendaverte"; // Name derived from Latin "Mendacium", meaning "Lie", and "Subvertere" meaning "Subvert"
        w_invertDemon.description = $"Good Villagers are Corrupted.\nEvil characters tell the {formattedKeyText("Truth")}.\nI Disguise.";
        //w_invertDemon.flavorText = "You and your so-called Good and Evil...\nSuch arbitrary divisors, is it really that black-and-white to you?"; //Used to be: "\"You and your 'Good' and 'Evil', like that Plague Doctor is pure Good but the Poisoner is pure Evil.\nIs it really so black-and-white to you?\"";
        w_invertDemon.hints = $"Characters I Corrupt Register as affected by Evil.\nEvils who tell the {formattedKeyText("Truth")} due to my ability do not.\n\nMy <b>Game Start</b> ability is unaffected by misregistration.";
        w_invertDemon.gender = EGender.They;

        CharacterData w_mezepheles = newCharacter("Venelum", EAlignment.Evil, ECharacterType.Demon, false, true, "\"Sign this contract and it will all fall into place...\n Oh, how naive you truly are.\""); ; // This was a Minion, but wound up being too strong. Whoops.
        w_mezepheles.role = new w_Mezepheles();
        // w_mezepheles.name = "Venelum"; // Was 'Proselytiser'. New name derived from Latin "Malum" meaning Evil, and "Venenum" meaning Poison.
        w_mezepheles.description = $"<b>Game Start:</b>\n1 Good Villager adjacent to me becomes Corrupted, if possible. They <i>cannot be {formattedKeyText("Cured")}</i>.\n1 Good Corrupted character becomes Evil.\n\nI Lie and Disguise.";
        w_mezepheles.hints = $"The character turned Evil by my ability will appear \"{formattedKeyText("Misled")}\" when Executed.";
        w_mezepheles.characterId = "Mezepheles_WING";
        w_mezepheles.gender = EGender.They;

        /*                                                                                         DISABLED FOR NOW DUE TO ISSUES
        CharacterData w_shard = new CharacterData();
        w_shard.role = new w_Shard();
        w_shard.name = "Specularus"; // Derived from Latin "Speculum" meaning "A mirror", "Celare" meaning "To conceal", and "Improsperus" meaning "Unfortunate".
        w_shard.description = $"Everyone Disguises, but nobody Lies.\n\nMany out-of-play Villagers and Outcasts are added to the {formattedKeyText("Deck")}.";
        w_shard.flavorText = "\"A glass shard on its own doesn't mean much, but a shattered mirror...\"";
        w_shard.hints = $"I override other characters' Disguise rules and force them to choose a random Good character from the {formattedKeyText("Deck")} as their Disguise.\n\nRarely, a character might remain {formattedKeyText("Honest")}. If they do, their ability will not work. This is a known bug.\n\nThe {roleColour("Minion")}Undying</color> is safe from my ability.";
        w_shard.ifLies = "";
        w_shard.picking = false;
        w_shard.startingAlignment = EAlignment.Evil;
        w_shard.type = ECharacterType.Demon;
        w_shard.bluffable = false;
        w_shard.characterId = "Shard_WING";
        w_shard.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_shard.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        w_shard.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        w_shard.color = new Color(1f, 0.3804f, 0.3804f);
        w_shard.gender = EGender.They;
        w_shard.usuallyDisguised = true;
        */

        CharacterData w_minos = newCharacter("Sanguitaurus", EAlignment.Evil, ECharacterType.Demon, false, true, "\"As sharp as claws can be, having a bladed weapon still tends to speed up the process.\"");
        w_minos.role = new w_Minos();
        // w_minos.name = "Sanguitaurus"; // Name derived from Latin 'Sanguis' meaning 'Blood', and 'Minotaurus' meaning 'Minotaur'
        w_minos.description = $"<b>Game Start:</b>\nYou have more or less {formattedKeyText("Max Health")} depending on the size of the village.\nNight takes <color=#FF9999>half as long to fall</color>.\n\n<b>At Night:</b>\n{formattedKeyText("Kill")} & {formattedKeyText("Reveal")} 1 Good character.\nDeal 2 {formattedKeyText("Damage")} to you.\n\n{formattedKeyText("Lose")} if all Good characters {formattedKeyText("Die")}.\n\nI Lie and Disguise.";
        w_minos.hints = $"You Learn the {formattedKeyText("True Role")} of characters I {formattedKeyText("Kill")}, but you <color=#FF9999>do not Learn what status effects they have</color>.\n\nIf I attack a {roleColour("Villager")}Knight</color>, I {formattedKeyText("Reveal")} him but do not {formattedKeyText("Kill")} him.";
        w_minos.characterId = "Minos_WING";
        w_minos.gender = EGender.They;
        nightPhase.nightCharactersOrder.Add(w_minos);

        /* Will re-add when I figure out larger villages again
        CharacterData w_pandemonium = new CharacterData();
        w_pandemonium.role = new w_Pandemonium();
        w_pandemonium.name = "Magnere"; // Name derived from Latin 'Magna' meaning 'Large', and 'Fallere' meaning 'Deceive'
        w_pandemonium.description = $"<b>Setup:</b>\nVillages are much bigger than usual.\n\n<b>Game Start:</b>\nYour {formattedKeyText("Max Health")} is equal to the village size.\n2 Demons are added to the {formattedKeyText("Deck")}; I become one of them.";
        w_pandemonium.flavorText = "\"Too much information and no idea what to do with it.\"";
        w_pandemonium.hints = $"{customHint("Keyword", "Setup")}\n\nI put a Night Cycle in-play, even if nothing uses it.\n\nHuge thanks to <color=#FFFF00>WWW Is Not Taken</color> on <color=#9999FF>Discord</color> for helping me get this ability working, I could <i>not</i> have done this without you.";
        w_pandemonium.ifLies = "";
        w_pandemonium.picking = false;
        w_pandemonium.startingAlignment = EAlignment.Evil;
        w_pandemonium.type = ECharacterType.Demon;
        w_pandemonium.bluffable = false;
        w_pandemonium.characterId = "Pandemonium_WING";
        w_pandemonium.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        w_pandemonium.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        w_pandemonium.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        w_pandemonium.color = new Color(1f, 0.3804f, 0.3804f);
        w_pandemonium.gender = EGender.They;
        w_pandemonium.usuallyDisguised = true;
        w_pandemonium.additionalFlavorTexts = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(1);
        w_pandemonium.additionalFlavorTexts[0] = w_pandemonium.flavorText;
        */

        CharacterData w_leviathan = newCharacter("Leviathan", EAlignment.Evil, ECharacterType.Demon, false, true, "\"Its arrival sews chaos within any village. Beware of it.\"");
        w_leviathan.role = new w_Leviathan();
        //w_leviathan.name = "Leviathan"; // Leviathan. Just... Leviathan.
        w_leviathan.description = $"<b>Setup:</b>\nMost Villagers are replaced with Outcasts.\nThere are additional Outcasts and Minions in the {formattedKeyText("Deck")}.\n\n{formattedKeyText("Lose")} if you Execute a Good Villager.\n\nI Lie and Disguise.";
        w_leviathan.hints = $"{customHint("Keyword", "Setup")}\n\nArt by {formattedKeyText("Derpy_Feesh")} ({formattedKeyText("@derpy_feesh")}) on {formattedKeyText("Discord")}";
        w_leviathan.gender = EGender.They;

        CharacterData w_fogDemon = newCharacter("Tenecaligo", EAlignment.Evil, ECharacterType.Demon, false, true, "\"In the looming mist, a Demon preys on the worst fear:\nthe fear of the unknown.\"");
        w_fogDemon.role = new w_Tenecaligo();
        //w_fogDemon.name = "Tenecaligo"; // Name derived from Latin "Tenebrae" and "Caligo", both meaning "Darkness"
        w_fogDemon.description = $"<b>Setup:</b>\nThere are no starting Outcasts or Minions.\n\n<b>Game Start:</b>\n4 Outcasts and 4 Minions are added to the {formattedKeyText("Deck")}.\n4 Good Villagers become a random combination of Outcasts and Minions.\n\nI Lie and Disguise.";
        w_fogDemon.hints = $"All Minions and Outcasts created by me Register as affected by Evil to the {roleColour("Villager")}Witness</color>.";
        w_fogDemon.gender = EGender.They;
        w_fogDemon.additionalPossibleCharacters.count.Add(NewPossibleCharacterCount(ECharacterType.Outcast, 4));
        w_fogDemon.additionalPossibleCharacters.count.Add(NewPossibleCharacterCount(ECharacterType.Minion, 4));

        CharacterData w_iris = newCharacter("Iris", EAlignment.Evil, ECharacterType.Demon, false, true, "\"When the village discusses who did it, 'It couldn't have possibly been Iris!', they say.\nHow foolish they are...\"");
        w_iris.role = new w_Iris();
        //w_iris.name = "Iris"; // Named after the part of the eye.
        w_iris.description = $"<b>Game Start:</b>\n1 Good Villager becomes Evil.\nThey {formattedKeyText("Bluff")} Lying.\n\nI Lie and Disguise.\nI Register as Good and as my Disguise.\nI {formattedKeyText("Bluff")} being {formattedKeyText("Truthful")} and {formattedKeyText("Honest")}.";
        w_iris.hints = "The Villager turned Evil by my ability will appear <color=#FF00AE>Hypnotised</color> when Executed.\nIf there are no Minions, some characters may get confused and remain silent.\n\n" + customHint("Keyword", "Bluff");
        w_iris.characterId = "Iris_WING";
        w_iris.gender = EGender.Female;



        CharacterData w_carnicarius = newCharacter("Carnicarius", EAlignment.Evil, ECharacterType.Demon, false, true, "\"The village idiot has nothing to worry about.\nThe Bishop, on the other hand...\"");
        // Derived from Latin "Sicarius" meaning "Assassin" and "Carnifex" meaning "Executioner"
        w_carnicarius.role = new w_Carnicarius();
        w_carnicarius.description = $"<b>At Night:</b>\nDeal 2 {formattedKeyText("Damage")} to you.\n{formattedKeyText("Kill")} a Good character.\nOn my first night, {formattedKeyText("Kill")} two.\nI pick my targets wisely.\n\nI Lie and Disguise.";
        w_carnicarius.hints = $"I am more likely to kill higher-priority targets, but not guaranteed. It may be true that the {roleColour("Villager")}Bishop</color> didn't die before you revealed her, but I might've just chosen to spare her.";
        w_carnicarius.characterId = "Carnicarius_WING";
        w_carnicarius.gender = EGender.Male;
        nightPhase.nightCharactersOrder.Add(w_carnicarius);



        //CharacterData w_switchDemon = new CharacterData();
        //w_switchDemon.role = new w_SwitchDemon();
        //w_switchDemon.name = "Furtamu"; // Name derived from latin "Furta" meaning "Trickster" and "Mutatio" meaning "Change".
        //w_switchDemon.description = "I Lie unless both of my neighbours are Villagers.\nI Disguise.";
        //w_switchDemon.flavorText = "\"Wants nothing more than to fit in with those around it.\"";
        //w_switchDemon.hints = "Idea for this one was by @sequest on Discord.";
        //w_switchDemon.ifLies = "";
        //w_switchDemon.picking = false;
        //w_switchDemon.startingAlignment = EAlignment.Evil;
        //w_switchDemon.type = ECharacterType.Demon;
        //w_switchDemon.bluffable = false;
        //w_switchDemon.characterId = "Furtamu_WING";
        //w_switchDemon.artBgColor = new Color(0.111f, 0.0833f, 0.1415f);
        //w_switchDemon.cardBgColor = new Color(0.0941f, 0.0431f, 0.0431f);
        //w_switchDemon.cardBorderColor = new Color(0.8196f, 0.0f, 0.0275f);
        //w_switchDemon.color = new Color(1f, 0.3804f, 0.3804f);
























        MelonLogger.Msg($"Doing act order");


        // Vanilla order: Baa, Chancellor, Pooka, Poisoner, Witch, Puppeteer, Plague Doctor, Shaman, Alchemist, Puppet, Lilis

        Characters.Instance.startGameActOrder = InsertAtStartOfActOrder(w_fogDemon);
        //Characters.Instance.startGameActOrder = InsertAtStartOfActOrder(w_pandemonium);


        Characters.Instance.startGameActOrder = InsertAfterAct("Baa", w_minos);


        Characters.Instance.startGameActOrder = InsertAfterAct("Chancellor", w_marionette);
        Characters.Instance.startGameActOrder = InsertAfterAct("Chancellor", w_swarm_good);
        Characters.Instance.startGameActOrder = InsertAfterAct("Chancellor", w_undying);
        Characters.Instance.startGameActOrder = InsertAfterAct("Chancellor", w_praesect);
        // Characters.Instance.startGameActOrder = insertAfterAct("Chancellor", w_twindemontriplet); // No longer needs to act on start after rework
        Characters.Instance.startGameActOrder = InsertAfterAct("Chancellor", w_mutant);

        //Characters.Instance.startGameActOrder = insertBeforeAct("Pooka", w_politician);
        //Characters.Instance.startGameActOrder = insertBeforeAct("Pooka", w_twindemon);
        //Characters.Instance.startGameActOrder = insertBeforeAct("Pooka", w_saboteur);
        Characters.Instance.startGameActOrder = InsertAfterAct("Pooka", w_saboteur);
        Characters.Instance.startGameActOrder = InsertAfterAct("Pooka", w_twindemon);

        Characters.Instance.startGameActOrder = InsertAfterAct("Shaman", w_acolyte);
        Characters.Instance.startGameActOrder = InsertAfterAct("Shaman", w_fanatic);
        Characters.Instance.startGameActOrder = InsertAfterAct("Shaman", w_zealot);
        Characters.Instance.startGameActOrder = InsertAfterAct("Shaman", w_legion);

        Characters.Instance.startGameActOrder = InsertAfterAct("Alchemist", w_heretic);
        Characters.Instance.startGameActOrder = InsertAfterAct("Alchemist", w_mezepheles); // This makes it uncurable by Alchemist but it might still have issues with other roles later on.
        Characters.Instance.startGameActOrder = InsertAfterAct("Alchemist", w_iris);
        Characters.Instance.startGameActOrder = InsertAfterAct("Alchemist", w_twindemontwin);

        //Characters.Instance.startGameActOrder = InsertAtEndOfActOrder(w_illusionist);
        //Characters.Instance.startGameActOrder = InsertAtEndOfActOrder(w_shard);
        //Characters.Instance.startGameActOrder = InsertAtEndOfActOrder(w_pilgrim);
        Characters.Instance.startGameActOrder = InsertAtEndOfActOrder(w_invertDemon);
        Characters.Instance.startGameActOrder = InsertAtEndOfActOrder(w_copycat);
        //Characters.Instance.startGameActOrder = InsertAtEndOfActOrder(w_devout);


        MelonLogger.Msg($"Act order done.");



        // New act order: Pandemonium, Tenecaligo, Baa, Sanguitaurus, Chancellor, Mutant, Praesect, Undying, Good Swarm, Marionette, Politician, Veniyon, Saboteur, Pooka, Poisoner, Witch, Puppeteer, Plague Doctor, Shaman, Agmeres, Zealot, Fanatic, Acolyte, Vidiyon, Venelum, Copycat, Emenverax, Specularus, Pilgrim, Mendaverte, Devout.







        /*
        foreach (CharacterData character in Characters.Instance.startGameActOrder)
        {
            MelonLogger.Msg($"Game Start order: {character.name.ToString()}");
        }
        */

















        MelonLogger.Msg($"Preparing scripts");


        Il2CppSystem.Collections.Generic.List<CharacterData> emptyCharacterDataList = new Il2CppSystem.Collections.Generic.List<CharacterData>();

        CustomScriptData legionScriptData = new CustomScriptData();
        legionScriptData.name = "Legion_1";
        ScriptInfo legionScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> legionList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        legionList.Add(w_legion);
        legionScript.mustInclude = legionList;
        legionScript.startingDemons = legionList;
        legionScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        legionScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        legionScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount legionCounter1 = new CharactersCount(5, 1, 1, 1, 2);
        legionCounter1.dTown = legionCounter1.town + 3;
        legionCounter1.dOuts = legionCounter1.outs + 1;
        CharactersCount legionCounter2 = new CharactersCount(6, 1, 1, 1, 3);
        legionCounter1.dTown = legionCounter2.town + 3;
        legionCounter2.dOuts = legionCounter2.outs + 1;
        CharactersCount legionCounter3 = new CharactersCount(7, 2, 1, 1, 3);
        legionCounter1.dTown = legionCounter3.town + 4;
        legionCounter3.dOuts = legionCounter3.outs + 1;
        CharactersCount legionCounter4 = new CharactersCount(8, 2, 1, 1, 4);
        legionCounter1.dTown = legionCounter4.town + 4;
        legionCounter4.dOuts = legionCounter4.outs + 1;
        CharactersCount legionCounter5 = new CharactersCount(9, 2, 1, 1, 5);
        legionCounter1.dTown = legionCounter5.town + 5;
        legionCounter4.dOuts = legionCounter5.outs + 1;
        CharactersCount legionCounter6 = new CharactersCount(10, 2, 1, 1, 6);
        legionCounter1.dTown = legionCounter6.town + 5;
        legionCounter4.dOuts = legionCounter6.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> legionCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        legionCounterList.Add(legionCounter1);
        legionCounterList.Add(legionCounter2);
        legionCounterList.Add(legionCounter2);
        legionCounterList.Add(legionCounter3);
        legionCounterList.Add(legionCounter3);
        legionCounterList.Add(legionCounter3);
        legionCounterList.Add(legionCounter4);
        legionCounterList.Add(legionCounter4);
        legionCounterList.Add(legionCounter4);
        legionCounterList.Add(legionCounter4);
        legionCounterList.Add(legionCounter5);
        legionCounterList.Add(legionCounter5);
        legionCounterList.Add(legionCounter5);
        legionCounterList.Add(legionCounter5);
        legionCounterList.Add(legionCounter5);
        legionCounterList.Add(legionCounter6);
        legionCounterList.Add(legionCounter6);
        legionCounterList.Add(legionCounter6);
        legionCounterList.Add(legionCounter6);
        legionCounterList.Add(legionCounter6);
        legionCounterList.Add(legionCounter6);
        legionScript.characterCounts = legionCounterList;
        legionScriptData.scriptInfo = legionScript;



        /*
        CustomScriptData twinDemonScriptData = new CustomScriptData();
        twinDemonScriptData.name = "TwinDemon_1";
        ScriptInfo twinDemonScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> twinDemonList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        twinDemonList.Add(w_twindemon);
        twinDemonScript.mustInclude = twinDemonList;
        twinDemonScript.startingDemons = twinDemonList;
        twinDemonScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        twinDemonScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        twinDemonScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount twinDemonCounter1 = new CharactersCount(7, 4, 1, 1, 1);
        twinDemonCounter1.dOuts = twinDemonCounter1.outs + 1;
        CharactersCount twinDemonCounter2 = new CharactersCount(8, 4, 1, 2, 1);
        twinDemonCounter2.dOuts = twinDemonCounter2.outs + 1;
        CharactersCount twinDemonCounter3 = new CharactersCount(9, 5, 1, 1, 2);
        twinDemonCounter3.dOuts = twinDemonCounter3.outs + 1;
        CharactersCount twinDemonCounter4 = new CharactersCount(10, 5, 1, 2, 2);
        twinDemonCounter4.dOuts = twinDemonCounter4.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> twinDemonCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        twinDemonCounterList.Add(twinDemonCounter1);
        twinDemonCounterList.Add(twinDemonCounter2);
        twinDemonCounterList.Add(twinDemonCounter3);
        twinDemonCounterList.Add(twinDemonCounter4);
        twinDemonScript.characterCounts = twinDemonCounterList;
        twinDemonScriptData.scriptInfo = twinDemonScript;
        */



        /*
        Il2CppSystem.Collections.Generic.List<CharacterData> illusionistJinxes = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        CustomScriptData illusionistScriptData = new CustomScriptData();
        illusionistScriptData.name = "Illusionist_1";
        ScriptInfo illusionistScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> illusionistList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        illusionistList.Add(w_illusionist);
        illusionistScript.mustInclude = illusionistList;
        illusionistScript.startingDemons = illusionistList;
        illusionistScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        illusionistScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        illusionistScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount illusionistCounter2 = new CharactersCount(8, 4, 1, 2, 1);
        illusionistCounter2.dOuts = illusionistCounter2.outs + 1;
        CharactersCount illusionistCounter3 = new CharactersCount(9, 5, 1, 1, 2);
        illusionistCounter3.dOuts = illusionistCounter3.outs + 1;
        CharactersCount illusionistCounter4 = new CharactersCount(10, 5, 1, 2, 2);
        illusionistCounter4.dOuts = illusionistCounter4.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> illusionistCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        illusionistCounterList.Add(illusionistCounter2);
        illusionistCounterList.Add(illusionistCounter3);
        illusionistCounterList.Add(illusionistCounter4);
        illusionistScript.characterCounts = illusionistCounterList;
        illusionistScriptData.scriptInfo = illusionistScript;
        */


        /*
        Il2CppSystem.Collections.Generic.List<CharacterData> tripleDemonJinxes = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        CustomScriptData tripleDemonScriptData = new CustomScriptData();
        tripleDemonScriptData.name = "TwinDemon_2";
        ScriptInfo tripleDemonScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> tripleDemonList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        tripleDemonList.Add(w_twindemon);
        tripleDemonList.Add(w_twindemontriplet);
        tripleDemonScript.mustInclude = tripleDemonList;
        tripleDemonScript.startingDemons = tripleDemonList;
        tripleDemonScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        tripleDemonScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        tripleDemonScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        JinxCharacter(legionScript.startingTownsfolks, "Bishop_58855542");
        JinxCharacter(legionScript.startingTownsfolks, "Empress_13782227");
        JinxCharacter(legionScript.startingTownsfolks, "Oracle_07039445");
        JinxCharacter(legionScript.startingTownsfolks, "Prince_WING");
        JinxCharacter(legionScript.startingMinions, "Swarm_Good_WING");
        CharactersCount tripleDemonCounter1 = new CharactersCount(7, 4, 2, 0, 1);
        tripleDemonCounter1.dOuts = tripleDemonCounter1.outs + 1;
        CharactersCount tripleDemonCounter2 = new CharactersCount(8, 4, 2, 1, 1);
        tripleDemonCounter2.dOuts = tripleDemonCounter2.outs + 1;
        CharactersCount tripleDemonCounter3 = new CharactersCount(9, 5, 2, 1, 1);
        tripleDemonCounter3.dOuts = tripleDemonCounter3.outs + 1;
        CharactersCount tripleDemonCounter4 = new CharactersCount(10, 5, 2, 1, 2);
        tripleDemonCounter4.dOuts = tripleDemonCounter4.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> tripleDemonCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        tripleDemonCounterList.Add(tripleDemonCounter1);
        tripleDemonCounterList.Add(tripleDemonCounter2);
        tripleDemonCounterList.Add(tripleDemonCounter3);
        tripleDemonCounterList.Add(tripleDemonCounter4);
        tripleDemonScript.characterCounts = tripleDemonCounterList;
        tripleDemonScriptData.scriptInfo = tripleDemonScript;
        */




        CustomScriptData caedoccidereScriptData = new CustomScriptData();
        caedoccidereScriptData.name = "Caedoccidere_1";
        ScriptInfo caedoccidereScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> caedoccidereList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        caedoccidereList.Add(w_caedoc);
        caedoccidereScript.mustInclude = caedoccidereList;
        caedoccidereScript.startingDemons = caedoccidereList;
        caedoccidereScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        caedoccidereScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        caedoccidereScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount caedoccidereCounter01 = new CharactersCount(7, 4, 1, 1, 1); // 4/1/1/1
        caedoccidereCounter01.dOuts = caedoccidereCounter01.outs + 1;
        CharactersCount caedoccidereCounter02 = new CharactersCount(7, 4, 1, 2, 0); // 4/2/0/1
        caedoccidereCounter02.dOuts = caedoccidereCounter02.outs + 1;
        CharactersCount caedoccidereCounter03 = new CharactersCount(8, 4, 1, 3, 0); // 4/3/0/1
        caedoccidereCounter03.dOuts = caedoccidereCounter03.outs + 1;
        CharactersCount caedoccidereCounter04 = new CharactersCount(8, 4, 1, 2, 1); // 4/2/1/1
        caedoccidereCounter04.dOuts = caedoccidereCounter04.outs + 1;
        CharactersCount caedoccidereCounter05 = new CharactersCount(8, 4, 1, 1, 2); // 4/1/2/1
        caedoccidereCounter05.dOuts = caedoccidereCounter05.outs + 1;
        CharactersCount caedoccidereCounter06 = new CharactersCount(9, 5, 1, 3, 0); // 5/3/0/1
        caedoccidereCounter06.dOuts = caedoccidereCounter06.outs + 1;
        CharactersCount caedoccidereCounter07 = new CharactersCount(9, 5, 1, 2, 1); // 5/2/1/1
        caedoccidereCounter07.dOuts = caedoccidereCounter07.outs + 1;
        CharactersCount caedoccidereCounter08 = new CharactersCount(9, 5, 1, 1, 2); // 5/1/2/1
        caedoccidereCounter08.dOuts = caedoccidereCounter08.outs + 1;
        CharactersCount caedoccidereCounter09 = new CharactersCount(10, 5, 1, 1, 3); // 5/1/3/1
        caedoccidereCounter09.dOuts = caedoccidereCounter09.outs + 1;
        CharactersCount caedoccidereCounter10 = new CharactersCount(10, 5, 1, 2, 2); // 5/2/2/1
        caedoccidereCounter10.dOuts = caedoccidereCounter10.outs + 1;
        CharactersCount caedoccidereCounter11 = new CharactersCount(10, 5, 1, 3, 1); // 5/3/1/1
        caedoccidereCounter11.dOuts = caedoccidereCounter11.outs + 1;
        CharactersCount caedoccidereCounter12 = new CharactersCount(10, 5, 1, 4, 0); // 5/4/0/1
        caedoccidereCounter12.dOuts = caedoccidereCounter12.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> caedoccidereCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        // 7 cards
        caedoccidereCounterList.Add(caedoccidereCounter01); // 4/1/1/1
        caedoccidereCounterList.Add(caedoccidereCounter01); // 4/1/1/1
        caedoccidereCounterList.Add(caedoccidereCounter02); // 4/2/0/1

        // 8 cards
        caedoccidereCounterList.Add(caedoccidereCounter03); // 4/3/0/1
        caedoccidereCounterList.Add(caedoccidereCounter04); // 4/2/1/1
        caedoccidereCounterList.Add(caedoccidereCounter04); // 4/2/1/1
        caedoccidereCounterList.Add(caedoccidereCounter05); // 4/1/2/1
        caedoccidereCounterList.Add(caedoccidereCounter05); // 4/1/2/1
        caedoccidereCounterList.Add(caedoccidereCounter05); // 4/1/2/1

        // 9 cards
        caedoccidereCounterList.Add(caedoccidereCounter06); // 5/3/0/1
        caedoccidereCounterList.Add(caedoccidereCounter07); // 5/2/1/1
        caedoccidereCounterList.Add(caedoccidereCounter07); // 5/2/1/1
        caedoccidereCounterList.Add(caedoccidereCounter08); // 5/1/2/1
        caedoccidereCounterList.Add(caedoccidereCounter08); // 5/1/2/1
        caedoccidereCounterList.Add(caedoccidereCounter08); // 5/1/2/1

        // 10 cards
        caedoccidereCounterList.Add(caedoccidereCounter09); // 5/1/3/1
        caedoccidereCounterList.Add(caedoccidereCounter09); // 5/1/3/1
        caedoccidereCounterList.Add(caedoccidereCounter09); // 5/1/3/1
        caedoccidereCounterList.Add(caedoccidereCounter09); // 5/1/3/1
        caedoccidereCounterList.Add(caedoccidereCounter10); // 5/2/2/1
        caedoccidereCounterList.Add(caedoccidereCounter10); // 5/2/2/1
        caedoccidereCounterList.Add(caedoccidereCounter10); // 5/2/2/1
        caedoccidereCounterList.Add(caedoccidereCounter11); // 5/3/1/1
        caedoccidereCounterList.Add(caedoccidereCounter11); // 5/3/1/1
        caedoccidereCounterList.Add(caedoccidereCounter12); // 5/4/0/1
        caedoccidereScript.characterCounts = caedoccidereCounterList;
        caedoccidereScriptData.scriptInfo = caedoccidereScript;




        CustomScriptData praesectScriptData = new CustomScriptData();
        praesectScriptData.name = "Praesect_1";
        ScriptInfo praesectScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> praesectList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        praesectList.Add(w_praesect);
        praesectScript.mustInclude = praesectList;
        praesectScript.startingDemons = praesectList;
        praesectScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        praesectScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        praesectScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        JinxCharacter(legionScript.startingTownsfolks, "Spy_WING");
        JinxCharacter(legionScript.startingMinions, "Swarm_Good_WING");
        CharactersCount praesectCounter2 = new CharactersCount(8, 5, 1, 2, 0);
        praesectCounter2.dOuts = praesectCounter2.outs + 1;
        CharactersCount praesectCounter3 = new CharactersCount(9, 6, 1, 1, 1);
        praesectCounter3.dOuts = praesectCounter3.outs + 1;
        CharactersCount praesectCounter4 = new CharactersCount(10, 6, 1, 2, 1);
        praesectCounter4.dOuts = praesectCounter4.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> praesectCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        praesectCounterList.Add(praesectCounter2);
        praesectCounterList.Add(praesectCounter3);
        praesectCounterList.Add(praesectCounter4);
        praesectScript.characterCounts = praesectCounterList;
        praesectScriptData.scriptInfo = praesectScript;




        CustomScriptData mendaverteScriptData = new CustomScriptData();
        mendaverteScriptData.name = "Mendaverte_1";
        ScriptInfo mendaverteScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> mendaverteList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        mendaverteList.Add(w_invertDemon);
        mendaverteScript.mustInclude = mendaverteList;
        mendaverteScript.startingDemons = mendaverteList;
        mendaverteScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        mendaverteScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        mendaverteScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        JinxCharacter(legionScript.startingTownsfolks, "Alchemist_94446803");
        JinxCharacter(legionScript.startingMinions, "Turncoat_WING");
        CharactersCount mendaverteCounter2 = new CharactersCount(8, 5, 1, 1, 1);
        mendaverteCounter2.dOuts = mendaverteCounter2.outs + 1;
        CharactersCount mendaverteCounter3 = new CharactersCount(9, 5, 1, 2, 1);
        mendaverteCounter3.dOuts = mendaverteCounter3.outs + 1;
        CharactersCount mendaverteCounter4 = new CharactersCount(10, 6, 1, 1, 2);
        mendaverteCounter4.dOuts = mendaverteCounter4.outs + 1;
        CharactersCount mendaverteCounter5 = new CharactersCount(10, 5, 1, 2, 2);
        mendaverteCounter5.dOuts = mendaverteCounter5.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> mendaverteCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        mendaverteCounterList.Add(mendaverteCounter2);
        mendaverteCounterList.Add(mendaverteCounter3);
        mendaverteCounterList.Add(mendaverteCounter4);
        mendaverteCounterList.Add(mendaverteCounter5);
        mendaverteScript.characterCounts = mendaverteCounterList;
        mendaverteScriptData.scriptInfo = mendaverteScript;




        CustomScriptData venelumScriptData = new CustomScriptData();
        venelumScriptData.name = "Venelum_1";
        ScriptInfo venelumScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> venelumList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        venelumList.Add(w_mezepheles);
        venelumScript.mustInclude = venelumList;
        venelumScript.startingDemons = venelumList;
        venelumScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        venelumScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        venelumScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount venelumCounter1 = new CharactersCount(8, 5, 1, 1, 1);
        venelumCounter1.dOuts = venelumCounter1.outs + 1;
        CharactersCount venelumCounter2 = new CharactersCount(9, 5, 1, 2, 1);
        venelumCounter2.dOuts = venelumCounter2.outs + 1;
        CharactersCount venelumCounter3 = new CharactersCount(9, 5, 1, 1, 2);
        venelumCounter3.dOuts = venelumCounter3.outs + 1;
        CharactersCount venelumCounter4 = new CharactersCount(10, 6, 1, 1, 2);
        venelumCounter4.dOuts = venelumCounter4.outs + 1;
        CharactersCount venelumCounter5 = new CharactersCount(10, 5, 1, 2, 2);
        venelumCounter5.dOuts = venelumCounter5.outs + 1;
        Il2CppSystem.Collections.Generic.List<CharactersCount> venelumCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        venelumCounterList.Add(venelumCounter1);
        venelumCounterList.Add(venelumCounter2);
        venelumCounterList.Add(venelumCounter3);
        venelumCounterList.Add(venelumCounter4);
        venelumCounterList.Add(venelumCounter5);
        venelumScript.characterCounts = venelumCounterList;
        venelumScriptData.scriptInfo = venelumScript;



        /*
        CustomScriptData shardScriptData = new CustomScriptData();
        shardScriptData.name = "Shard_1";
        ScriptInfo shardScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> shardList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        shardList.Add(w_shard);
        shardScript.mustInclude = shardList;
        shardScript.startingDemons = shardList;
        shardScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        shardScript.startingTownsfolks.Remove(w_prince);
        shardScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        shardScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        shardScript.startingMinions.Remove(w_undying);
        CharactersCount shardCounter1 = new CharactersCount(7, 5, 1, 0, 1);
        shardCounter1.dTown = 10;
        shardCounter1.dOuts = 4;
        CharactersCount shardCounter2 = new CharactersCount(7, 4, 1, 1, 1);
        shardCounter2.dTown = 10;
        shardCounter2.dOuts = 4;
        CharactersCount shardCounter3 = new CharactersCount(8, 5, 1, 1, 1);
        shardCounter3.dTown = 11;
        shardCounter3.dOuts = 5;
        CharactersCount shardCounter4 = new CharactersCount(8, 4, 1, 2, 1);
        shardCounter4.dTown = 11;
        shardCounter4.dOuts = 5;
        CharactersCount shardCounter5 = new CharactersCount(8, 4, 1, 1, 2);
        shardCounter5.dTown = 11;
        shardCounter5.dOuts = 5;
        CharactersCount shardCounter6 = new CharactersCount(8, 5, 1, 0, 2);
        shardCounter6.dTown = 11;
        shardCounter6.dOuts = 5;
        CharactersCount shardCounter7 = new CharactersCount(9, 5, 1, 1, 2);
        shardCounter7.dTown = 12;
        shardCounter7.dOuts = 6;
        CharactersCount shardCounter8 = new CharactersCount(9, 5, 1, 2, 1);
        shardCounter8.dTown = 12;
        shardCounter8.dOuts = 6;
        CharactersCount shardCounter9 = new CharactersCount(10, 5, 1, 2, 2);
        shardCounter9.dTown = 13;
        shardCounter9.dOuts = 7;
        CharactersCount shardCounter10 = new CharactersCount(10, 6, 1, 0, 3);
        shardCounter10.dTown = 13;
        shardCounter10.dOuts = 7;
        Il2CppSystem.Collections.Generic.List<CharactersCount> shardCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        shardCounterList.Add(shardCounter1);
        shardCounterList.Add(shardCounter2);
        shardCounterList.Add(shardCounter3);
        shardCounterList.Add(shardCounter4);
        shardCounterList.Add(shardCounter5);
        shardCounterList.Add(shardCounter6);
        shardCounterList.Add(shardCounter7);
        shardCounterList.Add(shardCounter8);
        shardCounterList.Add(shardCounter9);
        shardCounterList.Add(shardCounter10);
        shardScript.characterCounts = shardCounterList;
        shardScriptData.scriptInfo = shardScript
        */



        CustomScriptData minosScriptData = new CustomScriptData();
        minosScriptData.name = "Minos_1";
        ScriptInfo minosScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> minosList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        minosList.Add(w_minos);
        minosScript.mustInclude = minosList;
        minosScript.startingDemons = minosList;
        minosScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        minosScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        minosScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        // 7 characters
        CharactersCount minosCounter01 = setCharacterCount(3, 3, 0, 1);
        CharactersCount minosCounter02 = setCharacterCount(3, 2, 1, 1);
        CharactersCount minosCounter03 = setCharacterCount(4, 1, 1, 1);
        CharactersCount minosCounter04 = setCharacterCount(4, 0, 2, 1);
        // 8 characters
        CharactersCount minosCounter05 = setCharacterCount(3, 4, 0, 1);
        CharactersCount minosCounter06 = setCharacterCount(3, 3, 1, 1);
        CharactersCount minosCounter07 = setCharacterCount(4, 2, 1, 1);
        CharactersCount minosCounter08 = setCharacterCount(4, 1, 2, 1);
        CharactersCount minosCounter09 = setCharacterCount(4, 0, 3, 1);
        // 9 characters
        CharactersCount minosCounter10 = setCharacterCount(4, 4, 0, 1);
        CharactersCount minosCounter11 = setCharacterCount(4, 3, 1, 1);
        CharactersCount minosCounter12 = setCharacterCount(4, 2, 2, 1);
        CharactersCount minosCounter13 = setCharacterCount(4, 1, 3, 1);
        // 10 characters
        CharactersCount minosCounter14 = setCharacterCount(4, 5, 0, 1);
        CharactersCount minosCounter15 = setCharacterCount(4, 4, 1, 1);
        CharactersCount minosCounter16 = setCharacterCount(4, 3, 2, 1);
        CharactersCount minosCounter17 = setCharacterCount(5, 2, 2, 1);
        CharactersCount minosCounter18 = setCharacterCount(5, 1, 3, 1);
        Il2CppSystem.Collections.Generic.List<CharactersCount> minosCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();


        // 7 characters
        minosCounterList.Add(minosCounter01); // 3/3/0/1

        minosCounterList.Add(minosCounter02); // 3/2/1/1
        minosCounterList.Add(minosCounter02); // 3/2/1/1
        minosCounterList.Add(minosCounter02); // 3/2/1/1

        minosCounterList.Add(minosCounter03); // 4/1/1/1
        minosCounterList.Add(minosCounter03); // 4/1/1/1
        minosCounterList.Add(minosCounter03); // 4/1/1/1
        minosCounterList.Add(minosCounter03); // 4/1/1/1
        minosCounterList.Add(minosCounter03); // 4/1/1/1

        minosCounterList.Add(minosCounter04); // 4/0/2/1
        minosCounterList.Add(minosCounter04); // 4/0/2/1
        minosCounterList.Add(minosCounter04); // 4/0/2/1
        minosCounterList.Add(minosCounter04); // 4/0/2/1
        minosCounterList.Add(minosCounter04); // 4/0/2/1
        minosCounterList.Add(minosCounter04); // 4/0/2/1
        minosCounterList.Add(minosCounter04); // 4/0/2/1


        // 8 characters
        minosCounterList.Add(minosCounter05); // 3/4/0/1

        minosCounterList.Add(minosCounter06); // 3/3/1/1
        minosCounterList.Add(minosCounter06); // 3/3/1/1

        minosCounterList.Add(minosCounter07); // 4/2/1/1
        minosCounterList.Add(minosCounter07); // 4/2/1/1
        minosCounterList.Add(minosCounter07); // 4/2/1/1

        minosCounterList.Add(minosCounter08); // 4/1/2/1
        minosCounterList.Add(minosCounter08); // 4/1/2/1
        minosCounterList.Add(minosCounter08); // 4/1/2/1
        minosCounterList.Add(minosCounter08); // 4/1/2/1

        minosCounterList.Add(minosCounter09); // 4/0/3/1
        minosCounterList.Add(minosCounter09); // 4/0/3/1
        minosCounterList.Add(minosCounter09); // 4/0/3/1
        minosCounterList.Add(minosCounter09); // 4/0/3/1
        minosCounterList.Add(minosCounter09); // 4/0/3/1


        // 9 characters
        minosCounterList.Add(minosCounter10); // 4/4/0/1

        minosCounterList.Add(minosCounter11); // 4/3/1/1
        minosCounterList.Add(minosCounter11); // 4/3/1/1
        minosCounterList.Add(minosCounter11); // 4/3/1/1
        minosCounterList.Add(minosCounter11); // 4/3/1/1

        minosCounterList.Add(minosCounter12); // 4/2/2/1
        minosCounterList.Add(minosCounter12); // 4/2/2/1
        minosCounterList.Add(minosCounter12); // 4/2/2/1
        minosCounterList.Add(minosCounter12); // 4/2/2/1
        minosCounterList.Add(minosCounter12); // 4/2/2/1
        minosCounterList.Add(minosCounter12); // 4/2/2/1
        minosCounterList.Add(minosCounter12); // 4/2/2/1

        minosCounterList.Add(minosCounter13); // 4/1/3/1
        minosCounterList.Add(minosCounter13); // 4/1/3/1
        minosCounterList.Add(minosCounter13); // 4/1/3/1
        minosCounterList.Add(minosCounter13); // 4/1/3/1
        minosCounterList.Add(minosCounter13); // 4/1/3/1


        // 10 characters
        minosCounterList.Add(minosCounter14); // 4/5/0/1

        minosCounterList.Add(minosCounter15); // 4/4/1/1
        minosCounterList.Add(minosCounter15); // 4/4/1/1
        minosCounterList.Add(minosCounter15); // 4/4/1/1
        minosCounterList.Add(minosCounter15); // 4/4/1/1
        minosCounterList.Add(minosCounter15); // 4/4/1/1

        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1
        minosCounterList.Add(minosCounter16); // 4/3/2/1

        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1
        minosCounterList.Add(minosCounter17); // 5/2/2/1

        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1
        minosCounterList.Add(minosCounter18); // 5/1/3/1



        /*
        Layouts:
        3/3/0/1: 1%
        3/2/1/1: 4%
        4/1/1/1: 6%
        4/0/2/1: 6%
        3/4/0/1: 1%
        3/3/1/1: 2%
        4/2/1/1: 4%
        4/1/2/1: 5%
        4/0/3/1: 4%
        4/4/0/1: 1%
        4/3/1/1: 5%
        4/2/2/1: 9%
        4/1/3/1: 4%
        4/5/0/1: 1%
        4/4/1/1: 6%
        4/3/2/1: 13%
        5/2/2/1: 20%
        5/1/3/1: 9%
        */


        minosScript.characterCounts = minosCounterList;
        minosScriptData.scriptInfo = minosScript;





        /*
        CustomScriptData pandemoniumScriptData = new CustomScriptData();
        pandemoniumScriptData.name = "Pandemonium_1";
        ScriptInfo pandemoniumScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> pandemoniumList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        pandemoniumList.Add(w_pandemonium);
        pandemoniumScript.mustInclude = pandemoniumList;
        pandemoniumScript.startingDemons = pandemoniumList;
        pandemoniumScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        pandemoniumScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        pandemoniumScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount pandemoniumCounter01 = setCharacterCount(7, 1, 2, 1); // 11
        CharactersCount pandemoniumCounter02 = setCharacterCount(7, 2, 2, 1); // 12
        CharactersCount pandemoniumCounter03 = setCharacterCount(9, 0, 3, 1); // 13
        CharactersCount pandemoniumCounter04 = setCharacterCount(9, 1, 3, 1); // 14
        CharactersCount pandemoniumCounter05 = setCharacterCount(9, 2, 3, 1); // 15
        Il2CppSystem.Collections.Generic.List<CharactersCount> pandemoniumCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();


        pandemoniumCounterList.Add(pandemoniumCounter01);
        pandemoniumCounterList.Add(pandemoniumCounter02);
        pandemoniumCounterList.Add(pandemoniumCounter02);
        pandemoniumCounterList.Add(pandemoniumCounter03);
        pandemoniumCounterList.Add(pandemoniumCounter03);
        pandemoniumCounterList.Add(pandemoniumCounter03);
        pandemoniumCounterList.Add(pandemoniumCounter04);
        pandemoniumCounterList.Add(pandemoniumCounter04);
        pandemoniumCounterList.Add(pandemoniumCounter04);
        pandemoniumCounterList.Add(pandemoniumCounter04);
        pandemoniumCounterList.Add(pandemoniumCounter05);
        pandemoniumCounterList.Add(pandemoniumCounter05);
        pandemoniumCounterList.Add(pandemoniumCounter05);
        pandemoniumCounterList.Add(pandemoniumCounter05);
        pandemoniumCounterList.Add(pandemoniumCounter05);


        pandemoniumScript.characterCounts = pandemoniumCounterList;
        pandemoniumScriptData.scriptInfo = pandemoniumScript;
        */





        CustomScriptData tenecaligoScriptData = new CustomScriptData();
        tenecaligoScriptData.name = "Tenecaligo_1";
        ScriptInfo tenecaligoScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> tenecaligoList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        tenecaligoList.Add(w_fogDemon);
        tenecaligoScript.mustInclude = tenecaligoList;
        tenecaligoScript.startingDemons = tenecaligoList;
        tenecaligoScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        tenecaligoScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        tenecaligoScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount tenecaligoCounter01 = setCharacterCount(9, 0, 0, 1); // Always have 10 characters. Final ratio = 5 Villagers, 0-4 Outcasts, 0-4 Minions, 1 Demon.
        tenecaligoCounter01.dOuts = 0;
        Il2CppSystem.Collections.Generic.List<CharactersCount> tenecaligoCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();


        tenecaligoCounterList.Add(tenecaligoCounter01);


        tenecaligoScript.characterCounts = tenecaligoCounterList;
        tenecaligoScriptData.scriptInfo = tenecaligoScript;





        CustomScriptData leviathanScriptData = new CustomScriptData();
        leviathanScriptData.name = "Leviathan_1";
        ScriptInfo leviathanScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> leviathanList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        leviathanList.Add(w_leviathan);
        leviathanScript.mustInclude = leviathanList;
        leviathanScript.startingDemons = leviathanList;
        leviathanScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        leviathanScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        leviathanScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount leviathanCounter01 = setCharacterCount(1, 4, 1, 1); // 7
        leviathanCounter01.dOuts = 7;
        leviathanCounter01.dMinion = 4;
        CharactersCount leviathanCounter02 = setCharacterCount(2, 4, 1, 1); // 8
        leviathanCounter02.dOuts = 7;
        leviathanCounter02.dMinion = 4;
        CharactersCount leviathanCounter03 = setCharacterCount(2, 4, 2, 1); // 9
        leviathanCounter03.dOuts = 7;
        leviathanCounter03.dMinion = 5;
        CharactersCount leviathanCounter04 = setCharacterCount(2, 5, 2, 1); // 10
        leviathanCounter04.dOuts = 8;
        leviathanCounter04.dMinion = 5;
        CharactersCount leviathanCounter05 = setCharacterCount(3, 5, 1, 1); // 10
        leviathanCounter05.dOuts = 8;
        leviathanCounter05.dMinion = 4;
        Il2CppSystem.Collections.Generic.List<CharactersCount> leviathanCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();


        leviathanCounterList.Add(leviathanCounter01);
        for (int i = 0; i < 3; i++)
        {
            leviathanCounterList.Add(leviathanCounter02);
        }
        for (int i = 0; i < 9; i++)
        {
            leviathanCounterList.Add(leviathanCounter03);
        }
        for (int i = 0; i < (27 / 2); i++)
        {
            leviathanCounterList.Add(leviathanCounter04);
            leviathanCounterList.Add(leviathanCounter05);
        }


        leviathanScript.characterCounts = leviathanCounterList;
        leviathanScriptData.scriptInfo = leviathanScript;






        CustomScriptData twinDemonScriptData = new CustomScriptData();
        twinDemonScriptData.name = "TwinDemon_1";
        ScriptInfo twinDemonScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> twinDemonList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        twinDemonList.Add(w_twindemon);
        twinDemonList.Add(w_twindemontwin);
        twinDemonList.Add(w_twindemontriplet);
        twinDemonScript.startingDemons = twinDemonList;
        twinDemonScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        twinDemonScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        twinDemonScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        // 6 characters
        CharactersCount twinDemonCounter_06_4101 = setCharacterCount(4, 1, 0, 1);
        CharactersCount twinDemonCounter_06_4011 = setCharacterCount(4, 0, 1, 1);
        CharactersCount twinDemonCounter_06_4002 = setCharacterCount(4, 0, 0, 2);

        CharactersCount twinDemonCounter_07_4111 = setCharacterCount(4, 1, 1, 1);
        CharactersCount twinDemonCounter_07_4201 = setCharacterCount(4, 2, 0, 1);
        CharactersCount twinDemonCounter_07_4021 = setCharacterCount(4, 0, 2, 1);
        CharactersCount twinDemonCounter_07_4102 = setCharacterCount(4, 1, 0, 2);
        CharactersCount twinDemonCounter_07_4012 = setCharacterCount(4, 0, 1, 2);

        CharactersCount twinDemonCounter_08_4202 = setCharacterCount(4, 2, 0, 2);
        CharactersCount twinDemonCounter_08_4112 = setCharacterCount(4, 1, 1, 2);
        CharactersCount twinDemonCounter_08_4103 = setCharacterCount(4, 1, 0, 3);

        CharactersCount twinDemonCounter_09_4212 = setCharacterCount(4, 2, 1, 2);
        CharactersCount twinDemonCounter_09_5022 = setCharacterCount(5, 0, 2, 2);
        CharactersCount twinDemonCounter_09_4203 = setCharacterCount(4, 2, 0, 3);
        CharactersCount twinDemonCounter_09_5013 = setCharacterCount(5, 0, 1, 3);

        CharactersCount twinDemonCounter_10_4312 = setCharacterCount(4, 3, 1, 2);
        CharactersCount twinDemonCounter_10_5122 = setCharacterCount(5, 1, 2, 2);
        CharactersCount twinDemonCounter_10_5113 = setCharacterCount(5, 1, 1, 3);

        twinDemonCounter_06_4101.dDemon = 3;
        twinDemonCounter_06_4011.dDemon = 3;
        twinDemonCounter_06_4002.dDemon = 3;
        twinDemonCounter_07_4111.dDemon = 3;
        twinDemonCounter_07_4201.dDemon = 3;
        twinDemonCounter_07_4021.dDemon = 3;
        twinDemonCounter_07_4102.dDemon = 3;
        twinDemonCounter_07_4012.dDemon = 3;
        twinDemonCounter_08_4202.dDemon = 3;
        twinDemonCounter_08_4112.dDemon = 3;
        twinDemonCounter_08_4103.dDemon = 3;
        twinDemonCounter_09_4212.dDemon = 3;
        twinDemonCounter_09_5022.dDemon = 3;
        twinDemonCounter_09_4203.dDemon = 3;
        twinDemonCounter_09_5013.dDemon = 3;
        twinDemonCounter_10_4312.dDemon = 3;
        twinDemonCounter_10_5122.dDemon = 3;
        twinDemonCounter_10_5113.dDemon = 3;



        Il2CppSystem.Collections.Generic.List<CharactersCount> twinDemonCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();

        // 6-character villages will be fairly rare - 5 lots total, 7% chance.
        twinDemonCounterList = addCharacterCount(twinDemonCounter_06_4101, twinDemonCounterList, 1); // 20%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_06_4011, twinDemonCounterList, 1); // 20%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_06_4002, twinDemonCounterList, 3); // 60%

        // 7-characters will be fairly uncommon, 10 lots total for a 13% chance.
        twinDemonCounterList = addCharacterCount(twinDemonCounter_07_4111, twinDemonCounterList, 2); // 20%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_07_4201, twinDemonCounterList, 1); // 10%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_07_4021, twinDemonCounterList, 1); // 10%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_07_4102, twinDemonCounterList, 3); // 30%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_07_4012, twinDemonCounterList, 3); // 30%

        // 8-characters will have a 20% chance by having 15 lots.
        twinDemonCounterList = addCharacterCount(twinDemonCounter_08_4202, twinDemonCounterList, 5); // 33%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_08_4112, twinDemonCounterList, 7); // 47%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_08_4103, twinDemonCounterList, 3); // 20%

        // 9-characters will have a 27% chance by having 20 lots
        twinDemonCounterList = addCharacterCount(twinDemonCounter_09_4212, twinDemonCounterList, 7); // 35%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_09_5022, twinDemonCounterList, 7); // 35%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_09_4203, twinDemonCounterList, 3); // 15%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_09_5013, twinDemonCounterList, 3); // 15%

        // 10-characters will have a 33% chance by having 25 lots.
        twinDemonCounterList = addCharacterCount(twinDemonCounter_10_4312, twinDemonCounterList, 10); // 40%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_10_5122, twinDemonCounterList, 10); // 40%
        twinDemonCounterList = addCharacterCount(twinDemonCounter_10_5113, twinDemonCounterList, 5);  // 20%

        twinDemonScript.characterCounts = twinDemonCounterList;
        twinDemonScriptData.scriptInfo = twinDemonScript;



        CustomScriptData irisScriptData = new CustomScriptData();
        irisScriptData.name = "Iris_1";
        ScriptInfo irisScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> irisList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        irisList.Add(w_iris);
        irisScript.mustInclude = irisList;
        irisScript.startingDemons = irisList;
        irisScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        irisScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        irisScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount irisCounter01 = setCharacterCount(5, 2, 1, 1); // 9 characters
        CharactersCount irisCounter02 = setCharacterCount(4, 1, 1, 1); // 7 characters
        CharactersCount irisCounter03 = setCharacterCount(6, 0, 2, 1); // 9 characters
        CharactersCount irisCounter04 = setCharacterCount(4, 1, 0, 1); // 6 characters
        CharactersCount irisCounter05 = setCharacterCount(4, 2, 1, 1); // 8 characters
        CharactersCount irisCounter06 = setCharacterCount(6, 1, 2, 1); // 10 characters
        Il2CppSystem.Collections.Generic.List<CharactersCount> irisCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();


        irisCounterList.Add(irisCounter01);
        irisCounterList.Add(irisCounter02);
        irisCounterList.Add(irisCounter02);
        irisCounterList.Add(irisCounter03);
        irisCounterList.Add(irisCounter04);
        irisCounterList.Add(irisCounter04);
        irisCounterList.Add(irisCounter05);
        irisCounterList.Add(irisCounter05);
        irisCounterList.Add(irisCounter06);
        irisCounterList.Add(irisCounter06);


        irisScript.characterCounts = irisCounterList;
        irisScriptData.scriptInfo = irisScript;



        CustomScriptData carniScriptData = new CustomScriptData();
        carniScriptData.name = "Carnicarius_1";
        ScriptInfo carniScript = new ScriptInfo();
        Il2CppSystem.Collections.Generic.List<CharacterData> carniList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        carniList.Add(w_carnicarius);
        carniScript.mustInclude = carniList;
        carniScript.startingDemons = carniList;
        carniScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        carniScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        carniScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        CharactersCount carniCounter01 = setCharacterCount(4, 2, 1, 1); // 8 characters
        CharactersCount carniCounter02 = setCharacterCount(4, 1, 2, 1); // 8 characters
        CharactersCount carniCounter03 = setCharacterCount(5, 0, 3, 1); // 9 characters
        CharactersCount carniCounter04 = setCharacterCount(5, 1, 2, 1); // 9 characters
        CharactersCount carniCounter05 = setCharacterCount(5, 2, 1, 1); // 9 characters
        CharactersCount carniCounter06 = setCharacterCount(5, 1, 3, 1); // 10 characters
        CharactersCount carniCounter07 = setCharacterCount(5, 2, 2, 1); // 10 characters
        CharactersCount carniCounter08 = setCharacterCount(5, 3, 1, 1); // 10 characters
        CharactersCount carniCounter09 = setCharacterCount(5, 4, 1, 1); // 11 characters
        CharactersCount carniCounter10 = setCharacterCount(6, 2, 2, 1); // 11 characters
        CharactersCount carniCounter11 = setCharacterCount(6, 1, 3, 1); // 11 characters
        CharactersCount carniCounter12 = setCharacterCount(6, 0, 4, 1); // 11 characters
        Il2CppSystem.Collections.Generic.List<CharactersCount> carniCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();


        // 8 characters
        carniCounterList.Add(carniCounter01);
        carniCounterList.Add(carniCounter01);
        carniCounterList.Add(carniCounter01);
        carniCounterList.Add(carniCounter01);
        carniCounterList.Add(carniCounter02);
        carniCounterList.Add(carniCounter02);
        carniCounterList.Add(carniCounter02);
        carniCounterList.Add(carniCounter02);

        // 9 characters
        carniCounterList.Add(carniCounter03);
        carniCounterList.Add(carniCounter03);
        carniCounterList.Add(carniCounter04);
        carniCounterList.Add(carniCounter04);
        carniCounterList.Add(carniCounter04);
        carniCounterList.Add(carniCounter04);
        carniCounterList.Add(carniCounter04);
        carniCounterList.Add(carniCounter04);
        carniCounterList.Add(carniCounter05);
        carniCounterList.Add(carniCounter05);
        carniCounterList.Add(carniCounter05);
        carniCounterList.Add(carniCounter05);

        // 10 characters
        carniCounterList.Add(carniCounter06);
        carniCounterList.Add(carniCounter06);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter07);
        carniCounterList.Add(carniCounter08);
        carniCounterList.Add(carniCounter08);

        // 11 characters
        carniCounterList.Add(carniCounter09);
        carniCounterList.Add(carniCounter10);
        carniCounterList.Add(carniCounter10);
        carniCounterList.Add(carniCounter11);
        carniCounterList.Add(carniCounter11);
        carniCounterList.Add(carniCounter12);


        carniScript.characterCounts = carniCounterList;
        carniScriptData.scriptInfo = carniScript;




        //CustomScriptData tenecaligoScriptData = new CustomScriptData();
        //tenecaligoScriptData.name = "Tenecaligo_1";
        //ScriptInfo tenecaligoScript = new ScriptInfo();
        //Il2CppSystem.Collections.Generic.List<CharacterData> tenecaligoList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        //tenecaligoList.Add(w_fogDemon);
        //tenecaligoScript.mustInclude = tenecaligoList;
        //tenecaligoScript.startingDemons = tenecaligoList;
        //tenecaligoScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        //tenecaligoScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        //tenecaligoScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        //CharactersCount tenecaligoCounter2 = new CharactersCount(10, 9, 1, 0, 0);
        //tenecaligoCounter2.dOuts = 4;
        //Il2CppSystem.Collections.Generic.List<CharactersCount> tenecaligoCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        //tenecaligoCounterList.Add(tenecaligoCounter2);
        //tenecaligoScript.characterCounts = tenecaligoCounterList;
        //tenecaligoScriptData.scriptInfo = tenecaligoScript;




        //CustomScriptData furtamuScriptData = new CustomScriptData();
        //furtamuScriptData.name = "Furtamu_1";
        //ScriptInfo furtamuScript = new ScriptInfo();
        //Il2CppSystem.Collections.Generic.List<CharacterData> furtamuList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        //furtamuList.Add(w_switchDemon);
        //furtamuScript.mustInclude = furtamuList;
        //furtamuScript.startingDemons = furtamuList;
        //furtamuScript.startingTownsfolks = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingTownsfolks;
        //furtamuScript.startingOutsiders = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingOutsiders;
        //furtamuScript.startingMinions = ProjectContext.Instance.gameData.advancedAscension.possibleScriptsData[0].scriptInfo.startingMinions;
        //CharactersCount furtamuCounter1 = new CharactersCount(8, 5, 1, 1, 1);
        //furtamuCounter1.dOuts = furtamuCounter1.outs + 1;
        //CharactersCount furtamuCounter2 = new CharactersCount(9, 5, 1, 2, 1);
        //furtamuCounter2.dOuts = furtamuCounter2.outs + 1;
        //CharactersCount furtamuCounter3 = new CharactersCount(10, 6, 1, 1, 2);
        //furtamuCounter3.dOuts = furtamuCounter3.outs + 1;
        //Il2CppSystem.Collections.Generic.List<CharactersCount> furtamuCounterList = new Il2CppSystem.Collections.Generic.List<CharactersCount>();
        //furtamuCounterList.Add(furtamuCounter1);
        //furtamuCounterList.Add(furtamuCounter2);
        //furtamuCounterList.Add(furtamuCounter3);
        //furtamuScript.characterCounts = furtamuCounterList;
        //furtamuScriptData.scriptInfo = furtamuScript;

        //List<CharacterData> strangeDisguisesChars = new List<CharacterData>();
        //addCharacterDataToList("Clairvoyant_WING", strangeDisguisesChars);
        //addCharacterDataToList("Cleric_EP", strangeDisguisesChars);
        //addCharacterDataToList("Confessor_18741708", strangeDisguisesChars);
        //addCharacterDataToList("Detective_VP", strangeDisguisesChars);
        //addCharacterDataToList("Dreamer_32014895", strangeDisguisesChars);
        //addCharacterDataToList("Druid_89845092", strangeDisguisesChars);
        //addCharacterDataToList("Empress_13782227", strangeDisguisesChars);
        //addCharacterDataToList("Forager_WING", strangeDisguisesChars);
        //addCharacterDataToList("Lookout_41018246", strangeDisguisesChars);
        //addCharacterDataToList("Oracle_07039445", strangeDisguisesChars);
        //addCharacterDataToList("Prince_WING", strangeDisguisesChars);
        //addCharacterDataToList("Scout_88081716", strangeDisguisesChars);
        //addCharacterDataToList("Sentinel_WING", strangeDisguisesChars);
        //addCharacterDataToList("Village Idiot_VP", strangeDisguisesChars);
        //addCharacterDataToList("Witness_25155076", strangeDisguisesChars);
        //addCharacterDataToList("Doppleganger_52694042", strangeDisguisesChars);
        //addCharacterDataToList("Drunk_15369527", strangeDisguisesChars);
        //addCharacterDataToList("GoodTwin_VP", strangeDisguisesChars);
        //addCharacterDataToList("Lunatic_WING", strangeDisguisesChars);
        //addCharacterDataToList("Mayor_VP", strangeDisguisesChars);
        //addCharacterDataToList("Lycaon_VP", strangeDisguisesChars);
        //addCharacterDataToList("Shaman_26945607", strangeDisguisesChars);
        //addCharacterDataToList("Swarm_Good_WING", strangeDisguisesChars);
        //addCharacterDataToList("Turncoat_WING", strangeDisguisesChars);
        //addCharacterDataToList("Belias_EP", strangeDisguisesChars);
        //addCharacterDataToList("Illusionist_WING", strangeDisguisesChars);
        //addCharacterDataToList("TwinDemon_WING", strangeDisguisesChars);
        //replaceScriptChars(strangeDisguisesChars, strangeDisguisesScriptData);
        //Il2CppReferenceArray<CharacterData> advancedAscensionDemons = new Il2CppReferenceArray<CharacterData>(advancedAscension.demons.Length + 2);
        //advancedAscensionDemons = advancedAscension.demons;
        //advancedAscensionDemons[advancedAscensionDemons.Length - 2] = w_legion;
        //advancedAscensionDemons[advancedAscensionDemons.Length - 1] = w_twindemon;
        //advancedAscension.demons = advancedAscensionDemons;
        //Il2CppReferenceArray<CharacterData> advancedAscensionStartingDemons = new Il2CppReferenceArray<CharacterData>(advancedAscension.startingDemons.Length + 2);
        //advancedAscensionStartingDemons = advancedAscension.startingDemons;
        //advancedAscensionStartingDemons[advancedAscensionStartingDemons.Length - 2] = w_legion;
        //advancedAscensionStartingDemons[advancedAscensionStartingDemons.Length - 1] = w_twindemon;
        //advancedAscension.startingDemons = advancedAscensionStartingDemons;
        //Il2CppReferenceArray<CustomScriptData> advancedAscensionScriptsData = new Il2CppReferenceArray<CustomScriptData>(advancedAscension.possibleScriptsData.Length + 2);
        //advancedAscensionScriptsData = advancedAscension.possibleScriptsData;
        //advancedAscensionScriptsData[advancedAscensionScriptsData.Length - 2] = legionScriptData;
        //advancedAscensionScriptsData[advancedAscensionScriptsData.Length - 1] = twinDemonScriptData;
        //advancedAscension.possibleScriptsData = advancedAscensionScriptsData;
        MelonLogger.Msg($"Adding scripts");
        AscensionsData advancedAscension = ProjectContext.Instance.gameData.advancedAscension;
        w_addDemonRole(advancedAscension, w_legion, "Baa_Difficult", "Legion_1", legionScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Agmeres_Weight").Value);
        w_addDemonRole(advancedAscension, w_twindemon, "Baa_Difficult", "TwinDemon_1", twinDemonScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Veni-Vidi-Vici_Weight").Value);
        // w_addDemonRole(advancedAscension, w_twindemon, "Baa_Difficult", "TwinDemon_2", tripleDemonScriptData, emptyCharacterDataList);
        //w_addDemonRole(advancedAscension, w_illusionist, "Baa_Difficult", "Illusionist_1", illusionistScriptData, emptyCharacterDataList);
        //w_addDemonRole(advancedAscension, w_illusionist, "Baa_Difficult", "Illusionist_1", illusionistScriptData, emptyCharacterDataList);
        w_addDemonRole(advancedAscension, w_caedoc, "Baa_Difficult", "Caedoccidere_1", caedoccidereScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Caedoccidere_Weight").Value);
        w_addDemonRole(advancedAscension, w_praesect, "Baa_Difficult", "Praesect_1", praesectScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Praesect_Weight").Value);
        w_addDemonRole(advancedAscension, w_invertDemon, "Baa_Difficult", "Mendaverte_1", mendaverteScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Mendaverte_Weight").Value);
        w_addDemonRole(advancedAscension, w_mezepheles, "Baa_Difficult", "Venelum_1", venelumScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Venelum_Weight").Value);
        //w_addDemonRole(advancedAscension, w_shard, "Baa_Difficult", "Shard_1", shardScriptData, emptyCharacterDataList);
        //w_addDemonRole(advancedAscension, w_shard, "Baa_Difficult", "Shard_1", shardScriptData, emptyCharacterDataList);
        w_addDemonRole(advancedAscension, w_minos, "Baa_Difficult", "Minos_1", minosScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Sanguitaurus_Weight").Value);
        //w_addDemonRole(advancedAscension, w_pandemonium, "Baa_Difficult", "Pandemonium_1", pandemoniumScriptData, emptyCharacterDataList);
        //w_addDemonRole(advancedAscension, w_pandemonium, "Baa_Difficult", "Pandemonium_1", pandemoniumScriptData, emptyCharacterDataList);
        w_addDemonRole(advancedAscension, w_fogDemon, "Baa_Difficult", "Tenecaligo_1", tenecaligoScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Tenecaligo_Weight").Value);
        w_addDemonRole(advancedAscension, w_leviathan, "Baa_Difficult", "Leviathan_1", leviathanScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Leviathan_Weight").Value);
        w_addDemonRole(advancedAscension, w_iris, "Baa_Difficult", "Iris_1", irisScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Iris_Weight").Value);
        w_addDemonRole(advancedAscension, w_carnicarius, "Baa_Difficult", "Carnicarius_1", carniScriptData, emptyCharacterDataList, configCategory.GetEntry<int>("Carni_Weight").Value);



        for (int i = 0; i < 100; i++)
        {
            //w_addDemonRole(advancedAscension, w_pandemonium, "Baa_Difficult", "Pandemonium_1", pandemoniumScriptData, emptyCharacterDataList);
        }


        Il2CppSystem.Collections.Generic.List<CharacterData> undyingJinxes = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        //undyingJinxes.Add(w_shard);
        undyingJinxes.Add(w_invertDemon);

        Il2CppSystem.Collections.Generic.List<CharacterData> turncoatJinxes = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        //turncoatJinxes.Add(w_shard);
        turncoatJinxes.Add(w_invertDemon);

        Il2CppSystem.Collections.Generic.List<CharacterData> pilgrimJinxes = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        //pilgrimJinxes.Add(w_shard);


        MelonLogger.Msg($"Adding roles to scripts");
        foreach (CustomScriptData scriptData in advancedAscension.possibleScriptsData)
        {
            ScriptInfo script = scriptData.scriptInfo;
            addRole(script.startingTownsfolks, w_arbiter);
            addRole(script.startingTownsfolks, w_arithmetician);
            addRole(script.startingTownsfolks, w_bloodseer);
            addRole(script.startingTownsfolks, w_cardshark);
            addRole(script.startingTownsfolks, w_chiromancer);
            addRole(script.startingTownsfolks, w_clairvoyant);
            addRole(script.startingTownsfolks, w_copycat);
            addRole(script.startingTownsfolks, w_detective);
            addRole(script.startingTownsfolks, w_devoutNew);
            //addRole(script.startingTownsfolks, w_fiDragonfly); // Disabled until Uzabi fixes Good Disguises
            addRole(script.startingTownsfolks, w_forager);
            //addRole(script.startingTownsfolks, w_gambler);
            addRole(script.startingTownsfolks, w_gossip);
            addRole(script.startingTownsfolks, w_gravekeeper);
            //addRole(script.startingTownsfolks, w_insomniac);
            addRole(script.startingTownsfolks, w_introvert);
            addRole(script.startingTownsfolks, w_jewelsmith);
            addRole(script.startingTownsfolks, w_lamb);
            addRole(script.startingTownsfolks, w_performer);
            addRole(script.startingTownsfolks, w_prince);
            addRole(script.startingTownsfolks, w_ranger);
            // addRole(script.startingTownsfolks, w_saint);
            addRole(script.startingTownsfolks, w_scavenger);
            addRole(script.startingTownsfolks, w_sentinel);
            addRole(script.startingTownsfolks, w_sheriff);
            addRole(script.startingTownsfolks, w_spy);
            //addRole(script.startingTownsfolks, w_slayerRework);
            addRole(script.startingTownsfolks, w_warden);
            addRole(script.startingOutsiders, w_chatterbox);
            addRole(script.startingOutsiders, w_lunatic);
            addRole(script.startingOutsiders, w_marionette);
            addRole(script.startingOutsiders, w_mutant);
            //addRole(script.startingOutsiders, w_occultist);
            //addRoleIfNotJinxed(script.startingOutsiders, w_pilgrim, pilgrimJinxes, script.startingDemons);
            addRole(script.startingOutsiders, w_renegade);
            addRole(script.startingOutsiders, w_revolutionary);
            addRole(script.startingMinions, w_heretic);
            addRole(script.startingMinions, w_professional);
            addRole(script.startingMinions, w_saboteur);
            addRole(script.startingMinions, w_swarm_good);
            addRoleIfNotJinxed(script.startingMinions, w_turncoat, turncoatJinxes, script.startingDemons);
            addRoleIfNotJinxed(script.startingMinions, w_undying, undyingJinxes, script.startingDemons);
            for (int i = 0; i < 100; i++)
            {
                // addRoleEvenIfDupe(script.startingTownsfolks, w_saint);
            }
            for (int i = 0; i < allDatas.Length; i++)
            {
                //if (allDatas[i].characterId == "Gambler_42592744")
                //{
                //    script.startingTownsfolks.Remove(allDatas[i]);
                //}
            }
        }
        /*MelonLogger.Msg($"Trying to jinx roles...");
        foreach (CustomScriptData scriptData in advancedAscension.possibleScriptsData)
        {
            Il2CppSystem.Collections.Generic.List<string> JinxList = new Il2CppSystem.Collections.Generic.List<string>();
            JinxList.Add("Bishop_58855542");
            JinxList.Add("Empress_13782227");
            JinxList.Add("Oracle_07039445");
            JinxList.Add("Prince_WING");
            JinxList.Add("Doppleganger_52694042");
            JinxList.Add("Baron_04539999");
            JinxList.Add("Mezepheles_09511163");
            JinxList.Add("Shaman_26945607");
            JinxList.Add("Swarm_Good_WING");
            JinxList.Add("Toxomancer_WING");
            if (scriptData.name.ToString() == "Legion_1")
            {
                for (int i = 0; i < legionScript.startingTownsfolks.Count; i++)
                {
                    if (JinxList.Contains(legionScript.startingTownsfolks[i].characterId))
                    {
                        i -= 1;
                        legionScript.startingTownsfolks.RemoveAt(i);
                    }
                }
                for (int i = 0; i < legionScript.startingOutsiders.Count; i++)
                {
                    if (JinxList.Contains(legionScript.startingOutsiders[i].characterId))
                    {
                        i -= 1;
                        legionScript.startingOutsiders.RemoveAt(i);
                    }
                }
                for (int i = 0; i < legionScript.startingMinions.Count; i++)
                {
                    if (JinxList.Contains(legionScript.startingMinions[i].characterId))
                    {
                        i -= 1;
                        legionScript.startingMinions.RemoveAt(i);
                    }
                }
            }
        }
        */


        for (int j = 0; j < advancedAscension.possibleScriptsData.Length; j++)
        {
            Debug.LogWarning(advancedAscension.possibleScriptsData[j].name);
            MelonLogger.Msg($"Script: {advancedAscension.possibleScriptsData[j].name.ToString()}");
        }
        //w_addDemonRole(advancedAscension, w_switchDemon, "Baa_Difficult", "Furtamu_1", furtamuScriptData);
    }
    // By the vanilla rule of one demon per village.


    public CharactersCount setCharacterCount(int Villagers, int Outcasts, int Minions, int Demons)
    {
        CharactersCount myCharacterCount = new CharactersCount(Villagers + Outcasts + Minions + Demons, Villagers, Demons, Outcasts, Minions);
        myCharacterCount.dOuts = Outcasts + 1;
        return myCharacterCount;
    }

    public Il2CppSystem.Collections.Generic.List<CharactersCount> addCharacterCount(CharactersCount characterCount, Il2CppSystem.Collections.Generic.List<CharactersCount> addList, int weight)
    {
        Il2CppSystem.Collections.Generic.List<CharactersCount> returnList = addList;
        for (int i = 0; i < weight; i++)
        {
            returnList.Add(characterCount);
        }
        return returnList;
    }

    public void w_addDemonRole(AscensionsData advancedAscension, CharacterData? data, string oldScriptName, string newScriptName, CustomScriptData w_NewScript, Il2CppSystem.Collections.Generic.List<CharacterData> jinxList, int configAmount)
    {
        if (data == null)
        {
            return;
        }
        if (configAmount == 0)
        {
            return;
        }
        foreach (CustomScriptData scriptData in advancedAscension.possibleScriptsData)
        {
            if (scriptData.name == oldScriptName)
            {
                CustomScriptData newScriptData = GameObject.Instantiate(scriptData);
                newScriptData.name = newScriptName;
                ScriptInfo newScript = new ScriptInfo();
                ScriptInfo script = w_NewScript.scriptInfo;
                newScriptData.scriptInfo = newScript;
                newScript.startingTownsfolks = script.startingTownsfolks;
                newScript.startingOutsiders = script.startingOutsiders;
                newScript.startingMinions = script.startingMinions;
                newScript.startingDemons = script.startingDemons;
                newScript.characterCounts = w_NewScript.scriptInfo.characterCounts;
                //newScript.startingDemons = new Il2CppSystem.Collections.Generic.List<CharacterData>();
                //newScript.startingDemons.Add(data);
                var newPSD = advancedAscension.possibleScriptsData.Append(newScriptData);
                for (int i = 0; i < configAmount; i++)
                {
                    newPSD = newPSD.Append(newScriptData);
                }
                advancedAscension.possibleScriptsData = newPSD.ToArray();
                return;
            }
        }
    }
    public void addCharacterDataToList(string ID, List<CharacterData> Characters)
    {
        foreach (CharacterData targetChar in Gameplay.Instance.GetAllAscensionCharacters())
        {
            if (targetChar.characterId == ID)
            {
                Characters.Append(targetChar);
            }
        }
    }
    public void replaceScriptChars(List<CharacterData> Characters, CustomScriptData w_TargetScript)
    {
        w_TargetScript.scriptInfo.startingTownsfolks.Clear();
        w_TargetScript.scriptInfo.startingOutsiders.Clear();
        w_TargetScript.scriptInfo.startingMinions.Clear();
        w_TargetScript.scriptInfo.startingDemons.Clear();
        foreach (CharacterData targetChar in Characters)
        {
            if (targetChar.type == ECharacterType.Villager)
            {
                w_TargetScript.scriptInfo.startingTownsfolks.Add(targetChar);
            }
            if (targetChar.type == ECharacterType.Outcast)
            {
                w_TargetScript.scriptInfo.startingOutsiders.Add(targetChar);
            }
            if (targetChar.type == ECharacterType.Minion)
            {
                w_TargetScript.scriptInfo.startingMinions.Add(targetChar);
            }
            if (targetChar.type == ECharacterType.Demon)
            {
                w_TargetScript.scriptInfo.startingDemons.Add(targetChar);
            }
        }
    }
    public void addRole(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data)
    {
        if (list.Contains(data))
        {
            return;
        }
        list.Add(data);
    }
    public void addRoleEvenIfDupe(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data)
    {
        list.Add(data);
    }
    public void addRoleIfNotJinxed(Il2CppSystem.Collections.Generic.List<CharacterData> list, CharacterData data, Il2CppSystem.Collections.Generic.List<CharacterData> jinxList, Il2CppSystem.Collections.Generic.List<CharacterData> jinxCheckList)
    {
        if (list.Contains(data))
        {
            return;
        }
        bool jinxed = false;
        foreach (CharacterData character in jinxList)
        {
            foreach (CharacterData character2 in jinxCheckList)
            {
                if (character2 == character)
                {
                    jinxed = true;
                }
            }
        }
        if (jinxed) return;
        list.Add(data);
    }
    public CharacterData[] allDatas = System.Array.Empty<CharacterData>();
    public override void OnUpdate()
    {
        if (allDatas.Length == 0)
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                allDatas = new CharacterData[loadedCharList.Length];
                for (int i = 0; i < loadedCharList.Length; i++)
                {
                    allDatas[i] = loadedCharList[i]!.Cast<CharacterData>();
                }
            }
        }
        if (Statics.charactersArray.Length == 0)
        {
            var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
            if (loadedCharList != null)
            {
                Statics.charactersArray = new CharacterData[loadedCharList.Length];
                for (int i = 0; i < loadedCharList.Length; i++)
                {
                    CharacterData data = loadedCharList[i]!.Cast<CharacterData>();
                    Statics.CheckAddRole(data);
                    Statics.charactersArray[i] = data;
                }
            }
            if (Statics.charactersArray.Length > 0)
            {
                this.OnFirstUpdate();
            }
        }
    }
    public CharacterData[] InsertAfterAct(string previous, CharacterData data)
    {
        MelonLogger.Msg($"Adding {data.name.ToString()} after {previous}");
        CharacterData[] actList = Characters.Instance.startGameActOrder;

        int actSize = actList.Length;
        CharacterData[] newActList = new CharacterData[actSize + 1];
        bool inserted = false;
        for (int i = 0; i < actSize; i++)
        {
            if (inserted)
            {
                newActList[i + 1] = actList[i];
            }
            else
            {
                if (actList[i] != null)
                {
                    newActList[i] = actList[i];
                    if (actList[i].name == previous)
                    {
                        newActList[i + 1] = data;
                        inserted = true;
                    }
                }
            }
        }
        if (!inserted)
        {
            LoggerInstance.Msg("");
        }
        return newActList;
    }
    public CharacterData[] InsertAtStartOfActOrder(CharacterData data)
    {
        MelonLogger.Msg($"Adding {data.name.ToString()} to start of act order");
        CharacterData[] actList = Characters.Instance.startGameActOrder;
        int actSize = actList.Length;
        CharacterData[] newActList = new CharacterData[actSize + 1];
        for (int i = 0; i < actSize; i++)
        {
            newActList[i + 1] = actList[i];
        }
        newActList[0] = data;
        return newActList;
    }
    public CharacterData[] InsertAtEndOfActOrder(CharacterData data)
    {
        MelonLogger.Msg($"Adding {data.name.ToString()} to end of act order");
        CharacterData[] actList = Characters.Instance.startGameActOrder;
        int actSize = actList.Length;
        CharacterData[] newActList = new CharacterData[actSize + 1];
        for (int i = 0; i < actSize; i++)
        {
            newActList[i] = actList[i];
        }
        newActList[actSize] = data;
        return newActList;
    }
    public CharacterData[] insertBeforeAct(string next, CharacterData data)
    {
        MelonLogger.Msg($"insertBeforeAct called adding {data.name.ToString()} before {next}");
        int actSize = Characters.Instance.startGameActOrder.Length;
        Il2CppSystem.Collections.Generic.List<CharacterData> newActList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        bool added = false;
        foreach (CharacterData character in Characters.Instance.startGameActOrder)
        {
            MelonLogger.Msg($"Attempting to add {character.name.ToString()} to act order");
            if (character.name.ToString() == next) MelonLogger.Msg($"Found target {character.name.ToString()}");
            if (character.name.ToString() == next && added == false)
            {
                MelonLogger.Msg($"Adding target {data.name.ToString()} to newActList");
                newActList.Add(data);
                MelonLogger.Msg($"Added {data.name.ToString()} to newActList");
            }
            MelonLogger.Msg($"Adding {character.name.ToString()} to newActList");
            newActList.Add(character);
        }
        CharacterData[] newActArray = new CharacterData[actSize + 1];
        int counter = 0;
        MelonLogger.Msg($"Beginning loop");
        foreach (CharacterData character in newActList)
        {
            Debug.Log(string.Format("Adding {0} to act order at array position {1}", character.name.ToString(), counter));
            newActArray[counter] = character;
            counter += 1;
        }
        return newActArray;
    }
    public static Il2CppSystem.Collections.Generic.List<CharacterData> JinxCharacter(Il2CppSystem.Collections.Generic.List<CharacterData> inputList, string ID)
    {
        Il2CppSystem.Collections.Generic.List<CharacterData> outputList = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        foreach (CharacterData character in inputList)
        {
            if (character.characterId != ID)
            {
                outputList.Add(character);
            }
        }
        return outputList;
    }
    public void OnFirstUpdate()
    {
        PatchVanillaCharacterDescriptions();
        /*
        Transform chars = GameObject.Find("Game/Gameplay/Content/Canvas/Characters").transform;
        for (int i = 12; i < 16; i++)
        {
            Statics.checkCreateCircle(chars, i);
        }
        for (int j = 2; j < 5; j++)
        {
            Statics.checkCreateCircle(chars, j);
        }
        */
    }

    public static class HiddenRoleStatus
    {
        public static ECharacterStatus hiddenRole = (ECharacterStatus)999;
    }

    string customHint(string type, string parameter)
    {
        string hint = "Custom hint not working, please report to Wingidon";
        if (type == "Ability Refresh Hint")
        {
            if (parameter == "Each Night")
            {
                hint = "My ability refreshes each night and may be used again each day.";
            }
            if (parameter == "Once Per Game")
            {
                hint = "My ability does not refresh each night.";
            }
        }
        if (type == "Outcast Disguise Hint")
        {
            if (parameter == "Simple")
            {
                hint = "My Disguise choice follows standard Minion Disguise rules.";
            }
            if (parameter == "Advanced")
            {
                hint = "My Disguise choice follows standard Minion Disguise rules.\nThis means I may Disguise as an in-play or out-of-play character, and may even Disguise as another face-up Outcast.";
            }
        }
        if (type == "Interactions")
        {
            if (parameter == "Clone Evil")
            {
                hint = $"Due to the possibility of there being multiple of me, certain characters may have weird interactions with us.\nThe {roleColour("Villager")}Scout</color> may give info that's correct for one of us, but wrong for the other. This applies to Lying {roleColour("Villager")}Scouts</color> too.";
            }
            if (parameter == "Good Swarm")
            {
                hint = $"I am a Good Minion. As a result of this, a Lying {roleColour("Villager")}Oracle</color> may occasionally yield true info about me due to the way her Lying logic works.\nI can also be the other half of a Truthful {roleColour("Villager")}Oracle</color> ping on another Evil, including Evil {roleColour("Minion")}Swarm</color>.";
            }
            if (parameter == "Good Minion")
            {
                hint = $"I am a Good Minion. As a result of this, a Lying {roleColour("Villager")}Oracle</color> may occasionally yield true info about me due to the way her Lying logic works.\nI can also be the other half of a Truthful {roleColour("Villager")}Oracle</color> ping on another Evil.";
            }
        }
        if (type == "Keyword")
        {
            if (parameter == "Setup")
            {
                hint = $"<b>Setup:</b>\nThis ability applies <i>before</i> <b>Game Start</b> abilities. It only works if the current Demon is the primary Demon of the current board.\nThese effects are reflected in the role counts.";
            }
            if (parameter == "Bluff")
            {
                hint = $"<b>Bluff</b>:\nCharacters think I have the attribute that I am {formattedKeyText("Bluffing")}.";
            }
        }
        return hint;
    }


    public static CharacterData newCharacter(string name, EAlignment alignment, ECharacterType type, bool bluffable, bool usuallyDisguised, string flavour)
    {


        
        Il2CppSystem.Collections.Generic.List<string> refIDs = new Il2CppSystem.Collections.Generic.List<string>();
        refIDs = GetRolePlaceholderArt(type, alignment);
        MelonLogger.Msg($"refIDs[0] = {refIDs[0]}");
        MelonLogger.Msg($"refIDs[1] = {refIDs[1]}");
        CharacterData backgroundRef = ProjectContext.Instance.gameData.GetCharacterDataOfId(refIDs[0]);
        CharacterData artRef = ProjectContext.Instance.gameData.GetCharacterDataOfId(refIDs[1]);
        if (backgroundRef == null)
        {
            MelonLogger.Msg("backgroundRef is null! Resetting to Bishop...");
            backgroundRef = ProjectContext.Instance.gameData.GetCharacterDataOfId("Bishop_58855542");
        }
        if (artRef == null)
        {
            MelonLogger.Msg("artRef is null! Resetting to Bishop...");
            artRef = ProjectContext.Instance.gameData.GetCharacterDataOfId("Bishop_58855542");
        }
        MelonLogger.Msg($"backgroundRef = {backgroundRef.characterName}");
        MelonLogger.Msg($"artRef = {artRef.characterName}");
        



        CharacterData newCharacter = new CharacterData();
        //CharacterData bishopData = new CharacterData();
        //bishopData = ProjectContext.Instance.gameData.GetCharacterDataOfId("Bishop_58855542");
        //newCharacter.art = bishopData.art;
        //newCharacter.backgroundArt = bishopData.backgroundArt;
        //newCharacter.roguelikeInfo = bishopData.roguelikeInfo;

        MelonLogger.Msg("");
        MelonLogger.Msg($"Creating role {name} of type {type} and alignment {alignment}.");
        MelonLogger.Msg($"Name: {name}");
        newCharacter.name = name;
        newCharacter.characterName = name;
        MelonLogger.Msg($"Setting base desc...");
        newCharacter.description = "";
        MelonLogger.Msg($"Flavour: {flavour}");
        newCharacter.flavorText = flavour;
        newCharacter.hints = "";
        newCharacter.ifLies = "";
        newCharacter.picking = false;
        MelonLogger.Msg($"Alignment: {alignment.ToString()}");
        newCharacter.startingAlignment = alignment;
        MelonLogger.Msg($"Type: {type.ToString()}");
        newCharacter.type = type;
        MelonLogger.Msg($"Bluffable?: {bluffable.ToString()}");
        newCharacter.bluffable = bluffable;
        newCharacter.characterId = $"{name}_WING";
        newCharacter.artBgColor = getColour(type, alignment, "artBgColor");
        newCharacter.cardBgColor = getColour(type, alignment, "cardBgColor");
        newCharacter.cardBorderColor = getColour(type, alignment, "cardBorderColor");
        newCharacter.color = getColour(type, alignment, "color");
        MelonLogger.Msg($"Finished getting colours.");
        MelonLogger.Msg($"Usually Disguised?: {usuallyDisguised.ToString()}");
        newCharacter.usuallyDisguised = usuallyDisguised;
        newCharacter.additionalFlavorTexts = new Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppStringArray(1);
        newCharacter.additionalFlavorTexts[0] = flavour;

        newCharacter.bundledCharacters = new Il2CppSystem.Collections.Generic.List<CharacterData>();
        newCharacter.additionalPossibleCharacters = new AddedCharacterTypes();

        newCharacter.art_cute = artRef.art_cute;
        newCharacter.backgroundArt = backgroundRef.backgroundArt;

        newCharacter.localization_key = $"WINGMOD_{name}";

        return newCharacter;
    }

    public static CharacterCount NewPossibleCharacterCount(ECharacterType type, int amount)
    {
        CharacterCount returnVal = new CharacterCount();
        returnVal.type = type;
        returnVal.count = amount;
        return returnVal;
    }

    public static Il2CppSystem.Collections.Generic.List<string> GetRolePlaceholderArt(ECharacterType type, EAlignment alignment) // First item of the list is the background, second is the art.
    {
        Il2CppSystem.Collections.Generic.List<string> returnList = new Il2CppSystem.Collections.Generic.List<string>();
        if (alignment == EAlignment.Good)
        {
            returnList.Add("Bishop_58855542");
        }
        else
        {
            returnList.Add("Minion_71804875");
        }
        if (type == ECharacterType.Villager)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Knight_47970624"); // Good Villager: Knight
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Gambler_42592744"); // Evil Villager: Slayer
            }
        }
        if (type == ECharacterType.Outcast)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Wretch_80988916"); // Good Outcast: Wretch
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Bombardier_79093372"); // Evil Outcast: Bombardier
            }
        }
        if (type == ECharacterType.Minion)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Witch_25286521"); // Good Minion: Witch
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Poisoner_64796285"); // Evil Minion: Poisoner
            }
        }
        if (type == ECharacterType.Demon)
        {
            if (alignment == EAlignment.Good)
            {
                returnList.Add("Confessor_18741708"); // Good Demon: Confessor
            }
            if (alignment == EAlignment.Evil)
            {
                returnList.Add("Lillith_90453844"); // Evil Demon: Lilis
            }
        }
        return returnList;
    }

    string roleColour(string type)
    {
        switch (type)
        {
            // Types
            case "Villager": return formattedKeyText("VillagerColour");
            case "Outcast": return formattedKeyText("OutcastColour");
            case "Minion": return formattedKeyText("MinionColour");
            case "Demon": return formattedKeyText("DemonColour");
            case "EvilVillager": return formattedKeyText("EvilVillagerColour");
            case "EvilOutcast": return formattedKeyText("EvilOutcastColour");
            case "GoodMinion": return formattedKeyText("GoodMinionColour");
            case "GoodDemon": return formattedKeyText("GoodDemonColour");
        }
        return formattedKeyText("");
    }
    public static Color getColour(ECharacterType type, EAlignment alignment, string field)
    {
        // Type = character type
        // Alignment = character alignment
        // Field = "color" for text colour, "cardBgColor" for card background colour, "cardBorderColor" for the border colour and "artBgColor" for the art background colour.
        // In summary, field = "color", "cardBgColor", "cardBorderColor" or "artBgColor".
        Color returnColour = new Color(0, 0, 0);
        if (field == "artBgColor")
        {
            return getColour(type, alignment, "cardBorderColor");
        }
        if (type == ECharacterType.Villager)
        {
            if (alignment == EAlignment.Good)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1f, 0.9333f, 0.7294f);
                    case "cardBgColor": return new Color(0.2588f, 0.1529f, 0.3411f);
                    case "cardBorderColor": return new Color(0.7137f, 0.3372f, 0.8666f);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.9098f, 0.7764f, 1f); // E8C6FF
                    case "cardBgColor": return new Color(0.1647f, 0.1058f, 0.2f); // 2A1B33
                    case "cardBorderColor": return new Color(0.6078f, 0.1843f, 0.6823f); // 9B2FAE
                }
            }
        }
        if (type == ECharacterType.Outcast)
        {
            if (alignment == EAlignment.Good)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.9647f, 1, 0.447f);
                    case "cardBgColor": return new Color(0.1019f, 0.0666f, 0.0392f);
                    case "cardBorderColor": return new Color(0.7843f, 0.6470f, 0);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1, 0.6666f, 0.9568f); // E8C6FF
                    case "cardBgColor": return new Color(0.2509f, 0, 0.2156f); // 2A1B33
                    case "cardBorderColor": return new Color(1, 0, 0.8666f); // FF00DD
                }
            }
        }
        if (type == ECharacterType.Minion)
        {
            if (alignment == EAlignment.Evil)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.8509f, 0.4549f, 0);
                    case "cardBgColor": return new Color(0.094f, 0.0431f, 0.04313f);
                    case "cardBorderColor": return new Color(0.8196f, 0, 0.0235f);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(0.7882f, 1, 0.9490f); // E8C6FF
                    case "cardBgColor": return new Color(0.0588f, 0.1647f, 0.1647f); // 2A1B33
                    case "cardBorderColor": return new Color(0.2f, 0.8196f, 0.7764f); // 33D1C6
                }
            }
        }
        if (type == ECharacterType.Demon)
        {
            if (alignment == EAlignment.Evil)
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1, 0.3803f, 0.3803f);
                    case "cardBgColor": return new Color(0.0941f, 0.0431f, 0.0431f);
                    case "cardBorderColor": return new Color(0.8196f, 0, 0.0235f);
                }
            }
            else
            {
                switch (field)
                {
                    // Types
                    case "color": return new Color(1f, 0.9607f, 0.8784f); // E8C6FF
                    case "cardBgColor": return new Color(0.1019f, 0.0588f, 0.1803f); // 2A1B33
                    case "cardBorderColor": return new Color(0.4784f, 0.3607f, 1f); // 7A5CFF
                }
            }
        }
        return returnColour;
    }

    /*
    string characterColour(string character)
    {
        switch (character)
        {
            // Vanilla Villagers
            case "Alchemist": return "<color=#D2F7E4>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";

            // Vanilla Outcasts
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";

            // Vanilla Minions
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";

            // Vanilla Demons
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";

            // WEP Villagers
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";

            // WEP Outcasts
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";

            // WEP Minions
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";

            // WEP Demons
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
            case "": return "<color=#>";
        }
        return formattedKeyText("");
    }
    */
    string formattedKeyText(string target)
    {
        switch (target)
        {
            // Keywords
            case "Honest": return "<color=#7AC6FF>Honest</color>";
            case "Pure": return "<color=#7AFBFF>Pure</color>";
            case "Cure": return "<color=#7AFBFF>Cure</color>";
            case "Cured": return "<color=#7AFBFF>Cured</color>";
            case "Heal": return "<color=#2EFF43>Heal</color>";
            case "Max Health": return "<color=#7AFBFF>Max Health</color>";
            case "Health": return "<color=#7AFBFF>Health</color>";
            case "Damage": return "<color=#C72424>Damage</color>";
            case "True Role": return "<color=#57E69C>True Role</color>";
            case "Truthful": return "<color=#3A95D6>Truthful</color>";
            case "Truth": return "<color=#3A95D6>Truth</color>";
            case "Reveal": return "<color=#A1E6E2>Reveal</color>";
            case "Revealed": return "<color=#A1E6E2>Revealed</color>";
            case "Hidden": return "<color=#697D91>Hidden</color>";
            case "Unrevealed": return "<color=#697D91>Unrevealed</color>";
            case "Bluff": return "<color=#D96EDB>Bluff</color>";
            case "Bluffing": return "<color=#D96EDB>Bluffing</color>";
            case "Attack": return "<color=#FF0037>Attack</color>";
            case "Kill": return "<color=#FF0037>Kill</color>";
            case "Killed": return "<color=#FF0037>Killed</color>";
            case "Killing": return "<color=#FF0037>Killing</color>";
            case "Dead": return "<color=#B36979>Dead</color>";
            case "Die": return "<color=#B36979>Die</color>";
            case "Alive": return "<color=#A4EDB7>Alive</color>";
            case "Living": return "<color=#A4EDB7>Living</color>";
            case "Deck": return "<color=#789AF0>Deck</color>";
            case "Lose": return "<color=#FF0000>Lose</color>";

            // Custom role keywords
            case "Poison": return "<color=#3F8538>Poison</color>"; // For unused Toxomancer role.
            case "Poisoned": return "<color=#3F8538>Poisoned</color>";
            case "Trick": return "<color=#70E8FF>Trick</color>"; // Used by Faerie.
            case "Tricked": return "<color=#70E8FF>Tricked</color>";
            case "Bewildered": return "<color=#70E8FF>Bewil</color><color=#FF00DD>dered</color>"; // Also used by Faerie.
            case "Misled": return "<color=#FF00AE>Misled</color>"; // Used by Venelum and Vidiyon.


            // Devs
            case "Normandia": return "<color=#CE1119>Normandia</color>";
            case "Uzabi": return "<color=#CE1119>Uzabi</color>";

            // Modders
            case "@wingidon": return "<color=#7289DA>@</color><color=#C080FF>wingidon</color>";
            case "Wingidon": return "<color=#C080FF>Wingidon</color>";
            case "WWW": return "<color=#3BA55C>WWW is not taken</color>";
            case "@WWW": return "<color=#7289DA>@</color><color=#3BA55C>wwwisnottaken</color>";
            case "Carlz": return "<color=#5FC4F9>Carlz</color>";
            case "@Carlz": return "<color=#7289DA>@</color><color=#5FC4F9>carlz54339</color>";

            // Art credits
            case "Blue Cheesed": return "<color=#D8D8D8>Blue Cheesed</color>"; // Arithmetician
            case "@Blue Cheesed": return "<color=#7289DA>@</color><color=#D8D8D8>hydethefish</color>";
            case "WeekendWolf": return "<color=#5476ff>WeekendWolf</color>"; // Forager, Sentinel, Lunatic
            case "@weekendwolf": return "<color=#7289DA>@</color><color=#5476ff>hellzalley</color>";
            case "Astery": return "<color=#d506c7>Astery</color>"; // Gemcrafter
            case "@astery": return "<color=#7289DA>@</color><color=#d506c7>astery__</color>";
            case "LostIllustrator": return "<color=#45e0f8>Lost Illustrator</color>"; // Scavenger
            case "@lostillustrator": return "<color=#7289DA>@</color><color=#45e0f8>lostillustrator</color>";
            case "Hiraeth": return "<color=#4b53d5>Hiraeth</color>"; // Warden
            case "@hiraeth": return "<color=#7289DA>@</color><color=#4b53d5>lullabiesmourn</color>";
            case "Panda": return "<color=#cadee6>Panda</color>"; // Spy
            case "@Panda": return "<color=#7289DA>@</color><color=#cadee6>@pandacharly</color>";
            case "Derpy_Feesh": return "<color=#7948d7>Derpy_Feesh</color>"; // Leviathan
            case "@derpy_feesh": return "<color=#7289DA>@</color><color=#7948d7>derpy_feesh</color>"; // Leviathan

            // Special thanks
            case "NoLucksGiven": return "<color=#FFC07B>NoLucksGiven</color>"; // Played mod on YouTube, brought attention to it.
            case "D_NoLucksGiven": return "<color=#7289DA>@</color><color=#FFC07B>nolucksgiven</color>";
            case "Y_NoLucksGiven": return $"<color=#FFC07B>https://www.{formattedKeyText("YouTube")}.com/c/NoLucksGiven</color>";
            case "Fi": return "<color=#96EAFF>Fi the Dragonfly</color>"; // Faerie character is literally Fi lmao
            case "@fithedragonfly": return "<color=#96EAFF>@fithedragonfly</color>";

            // Colours
            case "VillagerColour": return "<color=#B656DD>";
            case "VillagerAltColour": return "<color=#C080FF>";
            case "OutcastColour": return "<color=#F6FF72>";
            case "OutcastAltColour": return "<color=#C8A500>";
            case "MinionColour": return "<color=#D97400>";
            case "DemonColour": return "<color=#FF6161>";

            // Colours, Alignment Flip
            case "EvilVillagerColour": return "<color=#9B2FAE>";
            case "EvilOutcastColour": return "<color=#FF00DD>";
            case "GoodMinionColour": return "<color=#33D1C6>";
            case "GoodDemonColour": return "<color=#7A5CFF>";

            // Platforms
            case "Discord": return "<color=#7289DA>Discord</color>";
            case "Tumblr": return "<color=#36465D>Tumblr</color>";
            case "YouTube": return "<color=#FE0000>YouTube</color>";
            case "Youtube": return "<color=#FE0000>YouTube</color>";
        }
        return "Formatted key text invalid, please report this to Wingidon.";
    }


    public void PatchVanillaCharacterDescriptions()
    {
        for (int i = 0; i < allDatas.Count(); i++)
        {
            MelonLogger.Msg($"Description Patcher: Found {allDatas[i].name.ToString()}");
            if (allDatas[i].characterName == "Witness")
            {
                allDatas[i].hints += "\n- Acolyte or Zealot created by Praesect" +
                                     "\n- Fanatic created by Undying" +
                                     // "\n- Character cloaked by Emenverax" +
                                     "\n- Character Hypnotised by Iris" +
                                     "\n- Character Misled by Venelum or Vidiyon" +
                                     "\n- Outcast or Minion created by Tenecaligo";
                MelonLogger.Msg($"Patched Witness. New description: {allDatas[i].hints}");
            }
        }
    }
    //int toxomancerPoisonTimer = 0;
    //int toxomancerDeathTimer = 0;


    /*private void OnCharacterRevealed(Character revealed)
    {
        toxomancerPoisonTimer -= 1;
        toxomancerDeathTimer -= 1;
        CharacterData charData = revealed.dataRef;
        Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);

        int revealCount = 0;
        for (int i = 0; i < allChars.Count; i++)
        {
            if (allChars[i].revealed == true)
            {
                revealCount++;
            }
        }
        if (revealCount == 1)
        {
            toxomancerPoisonTimer = 2;
            toxomancerDeathTimer = 4;
        }

        bool toxomancerInPlay = false;
        Character toxomancer = new Character();
        for (int i = 0; i < allChars.Count; i++)
        {
            if (allChars[i].dataRef.characterId == "Toxomancer_WING" && allChars[i].state != ECharacterState.Dead)
            {
                toxomancerInPlay = true;
                break;
            }
            if (allChars[i].dataRef.characterId == "Toxomancer_WING")
            {
                toxomancer = allChars[i];
            }
        }
        if (toxomancerInPlay)
        {
            if (toxomancerPoisonTimer == 0)
            {
                Il2CppSystem.Collections.Generic.List<Character> possiblePoisonTargets = new Il2CppSystem.Collections.Generic.List<Character>();
                foreach (Character character in allChars)
                {
                    if (character.GetRegisterAs().type == ECharacterType.Villager && character.GetRegisterAlignment() == EAlignment.Good && character.state != ECharacterState.Dead)
                    {
                        possiblePoisonTargets.Add(character);
                    }
                }
                Character poisonTarget = possiblePoisonTargets[UnityEngine.Random.RandomRangeInt(0, possiblePoisonTargets.Count)];
                poisonTarget.statuses.AddStatus(ECharacterStatus.Corrupted, toxomancer);
                poisonTarget.statuses.AddStatus(w_Toxomancer.ToxomancerPoison.toxomancerPoison, toxomancer);
                toxomancerPoisonTimer = 3;
                toxomancerDeathTimer = 2;
            }
        }
        if (toxomancerDeathTimer == 0)
        {
            foreach (Character character in allChars)
            {
                if (character.statuses.Contains(w_Toxomancer.ToxomancerPoison.toxomancerPoison))
                {
                    PlayerController.PlayerInfo.health.Damage(1);
                    character.RevealAllReal();
                    character.KillByDemon(toxomancer);
                }
            }
        }

    }*/

























    /*
    [HarmonyPatch(typeof(Gossip), nameof(Gossip.Act))]
    private static class GetPoetTrueInfo
    {
        private static bool Prefix(Gossip __instance, ETriggerPhase trigger, Character charRef)
        {
            if (trigger != ETriggerPhase.Day) return true;
            if (charRef.bluff)
            {
                if (charRef.bluff.characterId != "Gossip_85354100")
                {
                    return true;
                }
            }
            else if (charRef.dataRef.characterId != "Gossip_85354100")
            {
                return true;
            }
            Il2CppSystem.Collections.Generic.List<Role> infoRoles = new Il2CppSystem.Collections.Generic.List<Role>();
            infoRoles.Add(new Empath());
            infoRoles.Add(new Scout());
            infoRoles.Add(new Investigator());
            infoRoles.Add(new BountyHunter());
            infoRoles.Add(new Lookout());
            infoRoles.Add(new Knitter());
            infoRoles.Add(new Tracker());
            infoRoles.Add(new Shugenja());
            infoRoles.Add(new Noble());
            infoRoles.Add(new Bishop());
            infoRoles.Add(new Archivist());
            infoRoles.Add(new Acrobat2());
            infoRoles.Add(new w_Arithmetician());
            infoRoles.Add(new w_Chiromancer());
            infoRoles.Add(new w_Clairvoyant());
            infoRoles.Add(new w_Detective());
            infoRoles.Add(new w_Introvert());
            infoRoles.Add(new w_Jewelsmith());
            infoRoles.Add(new w_Lamb());
            infoRoles.Add(new w_Prince());
            infoRoles.Add(new w_Ranger());
            infoRoles.Add(new w_Sentinel());
            infoRoles.Add(new w_Sheriff());
            infoRoles.Add(new w_Spy());
            ActedInfo myInfo = infoRoles[UnityEngine.Random.RandomRangeInt(0, infoRoles.Count)].GetInfo(charRef);
            __instance.onActed?.Invoke(myInfo);
            return false;
        }
    }


    [HarmonyPatch(typeof(Gossip), nameof(Gossip.BluffAct))]
    private static class GetPoetFalseInfo
    {
        private static bool Prefix(Gossip __instance, ETriggerPhase trigger, Character charRef)
        {
            if (trigger != ETriggerPhase.Day) return true;
            if (charRef.bluff)
            {
                if (charRef.bluff.characterId != "Gossip_85354100")
                {
                    return true;
                }
            }
            else if (charRef.dataRef.characterId != "Gossip_85354100")
            {
                return true;
            }
            Il2CppSystem.Collections.Generic.List<Role> infoRoles = new Il2CppSystem.Collections.Generic.List<Role>();
            infoRoles.Add(new Empath());
            infoRoles.Add(new Scout());
            infoRoles.Add(new Investigator());
            infoRoles.Add(new BountyHunter());
            infoRoles.Add(new Lookout());
            infoRoles.Add(new Knitter());
            infoRoles.Add(new Tracker());
            infoRoles.Add(new Shugenja());
            infoRoles.Add(new Noble());
            infoRoles.Add(new Bishop());
            infoRoles.Add(new Archivist());
            infoRoles.Add(new Acrobat2());
            infoRoles.Add(new w_Arithmetician());
            infoRoles.Add(new w_Chiromancer());
            infoRoles.Add(new w_Clairvoyant());
            infoRoles.Add(new w_Detective());
            infoRoles.Add(new w_Introvert());
            infoRoles.Add(new w_Jewelsmith());
            infoRoles.Add(new w_Lamb());
            infoRoles.Add(new w_Prince());
            infoRoles.Add(new w_Ranger());
            infoRoles.Add(new w_Sentinel());
            infoRoles.Add(new w_Sheriff());
            infoRoles.Add(new w_Spy());
            ActedInfo myInfo = infoRoles[UnityEngine.Random.RandomRangeInt(0, infoRoles.Count)].GetBluffInfo(charRef);
            __instance.onActed?.Invoke(myInfo);
            return false;
        }
    }
    */


    /* Was causing crashes.
    [HarmonyPatch(typeof(Investigator), nameof(Investigator.BluffAct))]
    private static class GetOracleFalseInfo // Practically identical, save for the fact that it can't see Good Minions. Should fix problems with Good Swarm.
    {
        private static bool Prefix(Gossip __instance, ETriggerPhase trigger, Character charRef)
        {
            if (trigger != ETriggerPhase.Day) return true;
            if (charRef.bluff)
            {
                if (charRef.bluff.characterId != "Oracle_07039445")
                {
                    return true;
                }
            }
            else if (charRef.dataRef.characterId != "Oracle_07039445")
            {
                return true;
            }
            Il2CppSystem.Collections.Generic.List<Character> possibleInfoTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> infoTargets = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<CharacterData> deckMinions = Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Minion);
            CharacterData chosenMinion = new CharacterData();
            if (deckMinions.Count == 0)
            {
                foreach (CharacterData character in Gameplay.Instance.GetAllAscensionCharacters())
                {
                    if (character.type == ECharacterType.Minion)
                    {
                        deckMinions.Add(character);
                    }
                }
            }
            chosenMinion = deckMinions[UnityEngine.Random.RandomRangeInt(0, deckMinions.Count)];
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAlignment() == EAlignment.Good && character.GetCharacterType() != ECharacterType.Minion)
                {
                    possibleInfoTargets.Add(character);
                }
            }
            string actInfo = "";
            if (possibleInfoTargets.Count < 2)
            {
                actInfo = "This village confuses me.";
            }
            infoTargets.Add(possibleInfoTargets[UnityEngine.Random.RandomRangeInt(0, possibleInfoTargets.Count)]);
            possibleInfoTargets.Remove(infoTargets[0]);
            infoTargets.Add(possibleInfoTargets[UnityEngine.Random.RandomRangeInt(0, possibleInfoTargets.Count)]);

            if (infoTargets[0].id < infoTargets[1].id)
            {
                actInfo = string.Format("#{0} or #{1} is a {2}", infoTargets[0].id, infoTargets[1].id, chosenMinion.name.ToString());
            }
            else
            {
                actInfo = string.Format("#{0} or #{1} is a {2}", infoTargets[1].id, infoTargets[0].id, chosenMinion.name.ToString());
            }
            ActedInfo myInfo = new ActedInfo(actInfo, infoTargets);
            __instance.onActed?.Invoke(myInfo);
            return false;
        }
    }
    */


    [HarmonyPatch(typeof(ObjectivesUI), nameof(ObjectivesUI.UpdateObjectives))]
    public static class ChangeCounter
    {
        public static void Postfix(ObjectivesUI __instance)
        {
            //bool LilisInPlay = false;
            int minions = Gameplay.CurrentScript.minion;
            int demons = Gameplay.CurrentScript.demon;
            int MaxEvils = minions + demons;
            var deadCharacters = Gameplay.DeadCharacters;
            Il2CppSystem.Collections.Generic.List<Character> allCurrentCharacters = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);
            Il2CppSystem.Collections.Generic.List<CharacterData> allCurrentCharactersData = new Il2CppSystem.Collections.Generic.List<CharacterData>(Gameplay.Instance.GetScriptCharacters().Pointer);
            Il2CppSystem.Collections.Generic.List<string> Evils = new();
            //Il2CppSystem.Collections.Generic.List<string> allCurrentCharactersNames;
            //Il2CppSystem.Collections.Generic.List<string> allCurrentCharactersDataNames;

            //allCurrentCharactersNames = sortByName(allCurrentCharacters);
            //allCurrentCharactersDataNames = sortByName(allCurrentCharactersData);






            int minEvilsKilled = 0;
            int maxEvilsKilled = 0;
            int AddedEvils = 0;
            //int AddedEvils1 = 0;
            //int AddedEvils2 = 0;

            foreach (var deadCharacter in deadCharacters)
            {
                if (deadCharacter.alignment == EAlignment.Evil || deadCharacter.statuses.Contains(HiddenRoleStatus.hiddenRole))
                {
                    maxEvilsKilled++;
                    if (!deadCharacter.statuses.Contains(HiddenRoleStatus.hiddenRole))
                    {
                        minEvilsKilled++;
                    }
                }
            }


            //foreach (var character in allCurrentCharacters)
            //{

            //string characterData = allCurrentCharactersData[i].name.ToString();
            //string character;

            /*if (i <= allCurrentCharacters.Count - 1)
            {
               character = allCurrentCharactersNames[i];
            }
            else
            {
                character = "";
            }*/
            //MelonLogger.Msg("Character: " + character.dataRef.name.ToString());

            /*if (character == "Belias" || character == "Mayor" || character == "Good Twin" || character == "Puppeteer" || character == "Hypnotist" || character == "Executioner")
            {

                AddedEvils1++;
            }*/


            //if (character.dataRef.name == "Belias" || character.dataRef.name == "Mayor" || character.dataRef.name == "Good Twin" || character.dataRef.name == "Puppeteer" || character.dataRef.name == "Hypnotist" || character.dataRef.name == "Executioner")
            //{
            //if (Evils.Contains(character.dataRef.name.ToString()))
            //{
            //    AddedEvils++;
            //}

            //else
            //{
            //   Evils.Add(character.dataRef.name.ToString());
            //    AddedEvils++;
            //}

            //}

            //}

            //foreach (var characterData in allCurrentCharactersData)
            //{
            //   if (characterData.name.ToString() == "Hellspawn")
            //        MaxEvils++;
            //    if (characterData.name == "Belias" || characterData.name == "Mayor" || characterData.name == "Good Twin" || characterData.name == "Puppeteer" || characterData.name == "Hypnotist" || characterData.name == "Executioner")
            //    {

            //        if (!Evils.Contains(characterData.name.ToString()))
            //        {
            //            Evils.Add(characterData.name.ToString());
            //            AddedEvils++;
            //        }
            //    }

            //}

            /*if(AddedEvils2 > AddedEvils1)
            {
                AddedEvils = AddedEvils2;
            }

            else
            {
                AddedEvils = AddedEvils1;
            }*/

            //string EvilsKilledText = EvilsKilled.ToString();
            //string MaxEvilsAmount = AddedEvils.ToString();

            //if (MaxEvils < minions + demons)
            // MaxEvils++;
            if (minEvilsKilled == maxEvilsKilled)
            {
                __instance.evilsKilled.text = System.String.Format("<color=grey>Evils killed:</color> <color=red>{0}", minEvilsKilled);
            }
            else
            {
                __instance.evilsKilled.text = System.String.Format("<color=grey>Evils killed:</color> <color=red>{0}-{1}", minEvilsKilled, maxEvilsKilled);
            }


            /* else if(MaxEvils < minions + demons)
             {
                 MaxEvilsText = System.String.Format("<color=red>{0}-{1}", MaxEvils, minions + demons);
             }*/

            //if(LilisInPlay)
            // {
            //    EvilsKilledText = "?";
            // }

            // LilisInPlay = false;

            string minionCountText = "Minions";
            if (minions == 1)
            {
                minionCountText = "Minion";
            }
            string demonCountText = "Demons";
            if (demons == 1)
            {
                demonCountText = "Demon";
            }
            __instance.objective.text = System.String.Format("Find and Execute all Evil Characters<br><color=grey><size=18>(<color=orange>{0}+ {2}</color> and <color=red>{1}+ {3} </color>)", minions, demons, minionCountText, demonCountText);

        }
    }


    public static class Statics
    {
        public static Dictionary<string, CharacterData> roles = new Dictionary<string, CharacterData>();
        public static CharacterData[] charactersArray = Il2CppSystem.Array.Empty<CharacterData>();
        /*
        public static void checkCreateCircle(Transform parent, int size)
        {
            string name = "Circle_" + size;
            Transform t = parent.FindChild(name);
            if (t != null)
            {
                MelonLogger.Msg("Object Already exists!: " + name);
                return;
            }
            //createCircle(parent, size, name);
        }
        public static GameObject createCircle(int size) // I'm just gonna wait for WWW to figure this out
        {
            GameObject circle = new GameObject();
            circle.name = "Circle_" + size;
            circle.transform.SetParent(Characters.Instance.gameObject.transform);
            RectTransform rect = circle.AddComponent<RectTransform>();
            CharactersPool circPool = circle.AddComponent<CharactersPool>();
            GameObject circ6 = Characters.Instance.gameObject.transform.Find("Circle_6").gameObject;
            CharactersPool circ6Pool = circ6.GetComponent<CharactersPool>();
            circPool.characterPrefab = circ6Pool.characterPrefab;
            circPool.characters = new Character[0];
            circPool.cardPlaceHolders = new CardPlaceholder[size];
            for (int i = 0; i < size; i++)
            {
                GameObject cardHolder = new GameObject();
                cardHolder.transform.SetParent(circle.transform);
                string name = "CardPlaceholder";
                if (i > 0)
                {
                    name += " (" + i + ")";
                }
                cardHolder.name = name;
                RectTransform cardRect = cardHolder.AddComponent<RectTransform>();
                cardRect.anchoredPosition3D = new Vector3(0f, 0f, 0f);
                CardPlaceholder placeholder = cardHolder.AddComponent<CardPlaceholder>();
                int angle = i * 360 / size;
                if (angle <= 30)
                {
                    placeholder.actedSide = EActedSide.Down;
                }
                else if (angle <= 149)
                {
                    placeholder.actedSide = EActedSide.Left;
                }
                else if (angle <= 210)
                {
                    placeholder.actedSide = EActedSide.Up;
                }
                else if (angle <= 329)
                {
                    placeholder.actedSide = EActedSide.Right;
                }
                else
                {
                    placeholder.actedSide = EActedSide.Down;
                }
                circPool.cardPlaceHolders[i] = placeholder;
            }
            circle.transform.position = new UnityEngine.Vector3(0f, 1f, 85.9444f);
            circle.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
            circle.SetActive(false);
            addToCharsPool(circPool);
            return circle;
        }
        */
        public static void addToCharsPool(CharactersPool pool)
        {
            CharactersPool[] pools = Characters.Instance.characterPool;
            CharactersPool[] newPools = new CharactersPool[pools.Length + 1];
            for (int i = 0; i < pools.Length; i++)
            {
                newPools[i] = pools[i];
            }
            newPools[pools.Length] = pool;
            Characters.Instance.characterPool = newPools;
        }

        public static void GetStartingRoles()
        {
            AscensionsData allCharactersAscension = ProjectContext.Instance.gameData.allCharactersAscension;
            foreach (CharacterData data in allCharactersAscension.startingTownsfolks)
            {
                CheckAddRole(data);
            }
            foreach (CharacterData data in allCharactersAscension.startingOutsiders)
            {
                CheckAddRole(data);
            }
            foreach (CharacterData data in allCharactersAscension.startingMinions)
            {
                CheckAddRole(data);
            }
            foreach (CharacterData data in allCharactersAscension.startingDemons)
            {
                CheckAddRole(data);
            }
        }
        public static void CheckAddRole(CharacterData data)
        {
            string name = data.name;
            if (!roles.ContainsKey(name))
            {
                roles.Add(name, data);
            }
        }

    }
}