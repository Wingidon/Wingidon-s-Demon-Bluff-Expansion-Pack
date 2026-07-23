using HarmonyLib;
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
using static MelonLoader.MelonLaunchOptions;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Undying : Minion
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
                if (allDatas[j].characterId == "Fanatic_WING")
                {
                    Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, allDatas[j]);
                }
            }
            int diceRoll = Calculator.RollDice(100);
            if (diceRoll < 51)
            {
                Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);
                Character pickedChar = new Character();
                chars = Characters.Instance.FilterCharacterType(chars, ECharacterType.Villager);
                chars = Characters.Instance.FilterAlignmentCharacters(chars, EAlignment.Good);
                if (chars.Count > 0)
                {
                    pickedChar = chars[UnityEngine.Random.Range(0, chars.Count)];
                    foreach (Character c in Gameplay.CurrentCharacters)
                    {
                        if (c == pickedChar)
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
                                        c.statuses.AddStatus(ECharacterStatus.AlteredCharacter, charRef);
                                        c.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                                    }
                                }
                            }

                            for (int j = 0; j < allDatas.Length; j++)
                            {
                                if (allDatas[j].characterId == "Fanatic_WING")
                                {
                                    if (c.GetRegisterAs().characterId != allDatas[j].characterId)
                                    {
                                        c.Init(allDatas[j]);
                                        c.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, getTaunt("Reveal"));
        }
        if (trigger == ETriggerPhase.None)
        {
            if (charRef.statuses.Contains(undyingDie))
            {
                OnActed(ETriggerPhase.Day, charRef, getTaunt("Death"));
                charRef.statuses.statuses.Remove(undyingDie);
            }
            if (charRef.statuses.Contains(undyingFail))
            {
                OnActed(ETriggerPhase.Day, charRef, getTaunt("Fail"));
                charRef.statuses.statuses.Remove(undyingFail);
            }
            if (charRef.statuses.Contains(undyingWin))
            {
                OnActed(ETriggerPhase.Day, charRef, getTaunt("Win"));
                charRef.statuses.statuses.Remove(undyingWin);
            }
        }
    }
    public w_Undying() : base(ClassInjector.DerivedConstructorPointer<w_Undying>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Undying(System.IntPtr ptr) : base(ptr)
    {

    }
    public override ActedInfo GetInfo(Character charRef)
    {
        List<string> myTaunts = new List<string>();
        myTaunts.Add("Nice try!");
        myTaunts.Add("Not going down that easily!");
        myTaunts.Add("Try that on for size!");
        myTaunts.Add("Hah, as if!");
        myTaunts.Add("Wanna try that again?");
        myTaunts.Add("Hah, fool!");
        myTaunts.Add("Foolish!");
        myTaunts.Add("You know I can fend off the Executioner, right?");
        myTaunts.Add("You can't kill me!");
        myTaunts.Add("Bring it on!");
        myTaunts.Add("Hahahah!");
        myTaunts.Add("Heh...");
        string myChosenTaunt = myTaunts[UnityEngine.Random.RandomRangeInt(0, myTaunts.Count)];
        ActedInfo actedInfo = new ActedInfo(myChosenTaunt, null);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        List<string> myTaunts = new List<string>();
        myTaunts.Add("Bring it!");
        myTaunts.Add("Try me!");
        myTaunts.Add("Want some? Get some!");
        myTaunts.Add("Think you can beat me? Come and get it then!");
        myTaunts.Add("Come get some!");
        myTaunts.Add("Go on, draw your dagger, let's duel!");
        myTaunts.Add("You call that a knife?");
        myTaunts.Add("Your Executioner's power is nothing to me!");
        myTaunts.Add("Hah, I'd like to see you try!");
        myTaunts.Add("Is that flimsy little knife your weapon of choice?");
        myTaunts.Add("Interesting choice of weapon!");
        myTaunts.Add("I'd like to see you TRY to hurt me with that butterknife!");
        string myChosenTaunt = myTaunts[UnityEngine.Random.RandomRangeInt(0, myTaunts.Count)];
        ActedInfo actedInfo = new ActedInfo(myChosenTaunt, null);
        return actedInfo;
    }
    public override bool CheckIfCanBeKilled(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        bool evilLives = false;
        Il2CppSystem.Collections.Generic.List<Character> charactersNotMe = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in characters)
        {
            if (character.id != charRef.id && !LastStandCharacterIDs().Contains(character.dataRef.characterId))
            {
                Debug.Log(string.Format("Added #{0} to charactersNotMe list", character.id));
                charactersNotMe.Add(character);
            }
        }
        charactersNotMe = Characters.Instance.FilterAliveCharacters(charactersNotMe);
        foreach (Character character in charactersNotMe)
        {
            if (character.alignment == EAlignment.Evil)
            {
                Debug.Log(string.Format("Evil found at #{0}, setting EvilLives to true", character.id));
                evilLives = true;
            }
        }
        string myTaunt = "Fail";
        if (evilLives == true)
        {
            Debug.Log("Evil lives, dealing 4 damage...");
            Health health = PlayerController.PlayerInfo.health;
            health.Damage(4);
            Debug.Log("Checking win condition...");
            int healthCount = health.value.GetValue();
            if (healthCount <= 0)
            {
                Debug.Log("You lose due to low health!");
                myTaunt = "Win";
                charRef.statuses.AddStatus(undyingWin, charRef);
                // charRef.RevealAllReal();
                // UndyingLoss.getWinCons();
                // UndyingLoss.undyingLose();
            }
            else
            {
                myTaunt = "Fail";
                charRef.statuses.AddStatus(undyingFail, charRef);
            }
        }
        else
        {
            Debug.Log("Evil does not live.");
            charRef.statuses.AddStatus(undyingDie, charRef);
            myTaunt = "Death";
        }
        charRef.Act(ETriggerPhase.None);
        OnActed(ETriggerPhase.Day, charRef, getTaunt(myTaunt));
        Debug.Log(string.Format("Evil lives? {0}", evilLives));
        return !evilLives;
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        charRef.statuses.AddStatus(ECharacterStatus.UnkillableByDemon, charRef);
        charRef.statuses.AddStatus(ECharacterStatus.AppearTruthfull, charRef);
        charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        return null;
    }

    public Il2CppSystem.Collections.Generic.List<string> LastStandCharacterIDs()
    {
        return new wx_SavedScripts().GetLastStandIDs();
    }
    public ActedInfo getTaunt(string trigger)
    {
        string myChosenTaunt = "Try me.";
        Il2CppSystem.Collections.Generic.List<string> myTaunts = new Il2CppSystem.Collections.Generic.List<string>();
        Il2CppSystem.Collections.Generic.List<Character> mySelections = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> refChars = new Il2CppSystem.Collections.Generic.List<Character>();
        if (trigger == "Reveal")
        {
            myTaunts.Add("Bring it!");
            myTaunts.Add("Try me!");
            myTaunts.Add("Want some? Get some!");
            myTaunts.Add("Think you can beat me? Come and get it then!");
            myTaunts.Add("Come get some!");
            myTaunts.Add("Go on, draw your dagger, let's duel!");
            myTaunts.Add("You call that a knife?");
            myTaunts.Add("Your Executioner's power is nothing to me!");
            myTaunts.Add("Hah, I'd like to see you try!");
            myTaunts.Add("Is that flimsy little knife your weapon of choice?");
            myTaunts.Add("Interesting choice of weapon!");
            myTaunts.Add("I'd like to see you TRY to hurt me with that butterknife!");
        }
        if (trigger == "Fail")
        {
            myTaunts.Add("Nice try!");
            myTaunts.Add("Not going down that easily!");
            myTaunts.Add("Try that on for size!");
            myTaunts.Add("Hah, as if!");
            myTaunts.Add("Wanna try that again?");
            myTaunts.Add("Hah, fool!");
            myTaunts.Add("Foolish!");
            myTaunts.Add("You know I can fend off the Executioner, right?");
            myTaunts.Add("You can't kill me!");
            myTaunts.Add("Bring it on!");
            myTaunts.Add("Hahahah!");
            myTaunts.Add("Heh...");
        }
        if (trigger == "Win")
        {
            myTaunts.Add("We win!");
            myTaunts.Add("Get stuffed!");
            myTaunts.Add("Hell yeah!");
            myTaunts.Add("Got you!");
            myTaunts.Add("We did it! I mean... of course we did!");
            myTaunts.Add("We... We did it! I- I mean, of course we did!");
            myTaunts.Add("Didn't break a sweat!");
            myTaunts.Add("Hah, not a chance!");
            myTaunts.Add("Not a chance!");
            myTaunts.Add("We won! ...Right?");
            myTaunts.Add("Easy as pie... right?");
            myTaunts.Add("How's that for a victory!");
            myTaunts.Add("I've got your solve <i>right here!</i>");
        }
        if (trigger == "Death")
        {
            string demonName = "Iris";
            List<Character> allDemons = new List<Character>();
            List<string> myDemons = new List<string>();
            List<string> inPlayChars = new List<string>();
            bool demonInPlay = false;
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                inPlayChars.Add(character.dataRef.name.ToString());
                if (character.dataRef.type == ECharacterType.Demon)
                {
                    demonInPlay = true;
                    myDemons.Add(character.dataRef.name.ToString());
                }
                if (demonInPlay == true)
                {
                    int myChoice = UnityEngine.Random.RandomRangeInt(0, myDemons.Count);
                    demonName = myDemons[myChoice];
                }
            }
            // Default line
            myTaunts = AddStringToList("Fine, I yield!", myTaunts, 8);

            // Generic
            myTaunts = AddStringToList("Ow!", myTaunts, 4);
            myTaunts = AddStringToList("Ow, rude!", myTaunts, 4);
            myTaunts = AddStringToList("Cheap shot!", myTaunts, 4);
            myTaunts = AddStringToList("You got lucky!", myTaunts, 4);
            myTaunts = AddStringToList("I'll be back!", myTaunts, 4);
            myTaunts = AddStringToList("I'll get you next time!", myTaunts, 4);
            myTaunts = AddStringToList("Gah, damnit!", myTaunts, 4);
            myTaunts = AddStringToList("...Does that mean we lose?", myTaunts, 4);
            myTaunts = AddStringToList("Damn you!", myTaunts, 4);
            myTaunts = AddStringToList("I... lost?", myTaunts, 4);
            myTaunts = AddStringToList("I lost...? Aw, hell...", myTaunts, 4);
            myTaunts = AddStringToList("Aw, hell...", myTaunts, 4);

            // In-play Demon
            myTaunts = AddStringToList($"Oh... Oh, {demonName} is NOT going to be happy with me...", myTaunts, 3);
            myTaunts = AddStringToList($"I... lost? {demonName} is gonna kill me again...", myTaunts, 3);
            myTaunts = AddStringToList($"Aw hell... Okay, give me a moment to brace myself for {demonName}'s lecture...", myTaunts, 3);

            // Other Demons
            if (!inPlayChars.Contains("Agmeres")) myTaunts.Add($"Might need to call on Agmeres...");
            if (!inPlayChars.Contains("Agmeres")) myTaunts.Add($"...You wait here for a moment while I call on Agmeres!");
            if (!inPlayChars.Contains("Caedoccidere")) myTaunts.Add($"Oh, Caedoccidere is gonna kill me...");
            if (!inPlayChars.Contains("Caedoccidere")) myTaunts.Add($"...Okay, give me a minute to brace myself, Caedoccidere is gonna kill me...");
            if (!inPlayChars.Contains("Emenverax")) myTaunts.Add($"...Don't tell Emenverax we lost, alright? I made a bet with him...");
            if (!inPlayChars.Contains("Emenverax")) myTaunts.Add($"Starting to regret that bet I made with Emenverax...");
            if (!inPlayChars.Contains("Iris")) myTaunts.Add($"Oh, Iris is gonna be <i>pissed</i>...");
            if (!inPlayChars.Contains("Iris")) myTaunts.Add($"Don't tell Iris I lost, her lectures in particular are torture. And I say that as a resident of hell!");
            if (!inPlayChars.Contains("Leviathan")) myTaunts.Add($"...Leviathan's going to give me a meteor to the face for this...");
            if (!inPlayChars.Contains("Leviathan")) myTaunts.Add($"You're... good at reasoning with people, right? Can you... try to calm Leviathan down?");
            if (!inPlayChars.Contains("Magnere")) myTaunts.Add($"Magnere's gonna kill me...");
            if (!inPlayChars.Contains("Magnere")) myTaunts.Add($"Magnere won't be happy with this...");
            if (inPlayChars.Contains("Mendaverte")) myTaunts.Add("I am dizzy");
            if (inPlayChars.Contains("Mendaverte")) myTaunts.Add("Hang on, I feel a bit light-headed, I need to go sit down...");
            if (inPlayChars.Contains("Praesect")) myTaunts.Add("I'm sorry, Praesect. I hope my Acolyte served you well...");
            if (inPlayChars.Contains("Praesect")) myTaunts.Add("Praesect forgive me...");
            if (inPlayChars.Contains("Specularus")) myTaunts.Add("Specularus, black & white doesn't suit me, I'm not wearing that costume.");
            if (inPlayChars.Contains("Specularus")) myTaunts.Add("Specularus, don't you dare. I am <i>not</i> wearing that dress, no matter what you do.");
            if (inPlayChars.Contains("Tenecaligo")) myTaunts.Add("Oh boy, I'm not gonna be able to find my way for weeks after this...");
            if (!inPlayChars.Contains("Tenecaligo")) myTaunts.Add("Hope Tenecaligo's not gonna be mad at me...");
            if (!inPlayChars.Contains("Venelum")) myTaunts.Add("Glad I didn't sign Venelum's contract...");
            if (!inPlayChars.Contains("Venelum")) myTaunts.Add("Don't tell Venelum about this please...");
            if (inPlayChars.Contains("Veniyon")) myTaunts.Add("...Veniyon? Can we schedule a session later today?");
            if (inPlayChars.Contains("Vidiyon")) myTaunts.Add("Vidi, I tried my best, alright!");
            if (inPlayChars.Contains("Viciyon")) myTaunts.Add("Apologies, Vici.");

            // Easter Eggs
            if (inPlayChars.Contains("Judge")) myTaunts.Add("Did my brother set you up to this?");
            if (inPlayChars.Contains("Professional")) myTaunts.Add("Are we missing someone?");
            if (inPlayChars.Contains("Swarm")) myTaunts.Add("What's up with that defect anyway...");
            if (inPlayChars.Contains("Leviathan")) myTaunts.Add("What was that you said earlier, Levi? \"<i>With strange aeons, even death may die</i>\"? I think that applies here.");
        }
        if (myTaunts.Count != 0)
        {
            myChosenTaunt = myTaunts[UnityEngine.Random.RandomRangeInt(0, myTaunts.Count)];
        }
        return new ActedInfo(myChosenTaunt);
    }

    public Il2CppSystem.Collections.Generic.List<string> AddStringToList(string input, Il2CppSystem.Collections.Generic.List<string> list, int weight)
    {
        for (int i = 0; i < weight; i++)
        {
            list.Add(input);
        }
        return list;
    }
    public static ECharacterStatus undyingFail = (ECharacterStatus)2114495619;
    public static ECharacterStatus undyingDie = (ECharacterStatus)2114495161;
    public static ECharacterStatus undyingWin = (ECharacterStatus)2114495239;
    public static class UndyingLoss
    {
        public static WinConditions winConditions;
        public static GameObject undyingLoss;
        public static void getWinCons()
        {
            GameObject winCon = GameObject.Find("Game/Gameplay/Content/WinConditions");
            winConditions = winCon.GetComponent<WinConditions>();
            undyingLoss = GameObject.Instantiate(winConditions.autoLose);
            GameObject undyingLossNote = undyingLoss.transform.FindChild("Note/Text (TMP)").gameObject;
            TextMeshProUGUI undyingLossText = undyingLossNote.GetComponent<TextMeshProUGUI>();
            undyingLossText.text = $"<color=red>Evil Wins!</color>\n\nThe Undying dealt lethal damage to you.";
        }
        public static void undyingLose()
        {
            undyingLoss.SetActive(true);
            winConditions.Lose();
        }
    }


    [HarmonyPatch(typeof(Slayer), nameof(Slayer.CharacterPicked))]
    private static class SlayerUndying
    {
        private static bool Prefix(Slayer __instance)
        {
            CharacterPicker.OnCharactersPicked -= (Il2CppSystem.Action)__instance.CharacterPicked;
            CharacterPicker.OnStopPick -= (Il2CppSystem.Action)__instance.StopPick;

            Il2CppSystem.Collections.Generic.List<Character> chars = new Il2CppSystem.Collections.Generic.List<Character>();
            chars.Add(CharacterPicker.PickedCharacters[0]);

            string info = $"";
            bool shouldExecute = false;

            if (chars[0].GetRegisterAlignment() == EAlignment.Evil && chars[0].role.CheckIfCanBeKilled(chars[0]))
            {
                info = __instance.ConjourInfo(chars[0].id, EAlignment.Evil, chars[0]);
                shouldExecute = true;
            }
            else
                info = __instance.ConjourInfo(chars[0].id, EAlignment.Good, chars[0]);

            if (chars[0].state == ECharacterState.Dead)
            {
                shouldExecute = false;
                return false;
            }

            __instance.onActed?.Invoke(new ActedInfo(info, chars));
            Debug.Log($"{info}");

            if (shouldExecute)
                chars[0].KillAndReveal();
            return false;
        }
    }
}


