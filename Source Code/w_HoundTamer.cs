using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using JetBrains.Annotations;
using MelonLoader;
using System;
using static MelonLoader.MelonLaunchOptions;
using static MelonLoader.Modules.MelonModule;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_HoundTamer : Role
{
    string dogName = "My dog";
    public override ActedInfo GetInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<Character> werewolves = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character character in Gameplay.CurrentCharacters)
        {
            if (character.GetRegisterAs().characterId == "Werewolf_78350415")
            {
                werewolves.Add(character);
            }
        }
        if (werewolves.Count != 0)
        {
            Character theWerewolf = sharedScripts.GetRandomItemOfList(werewolves);
            return sharedScripts.ReturnInfoWithSingleSelection($"{dogName} is barking at #{theWerewolf.id}!", theWerewolf);
        }
        Il2CppSystem.Collections.Generic.List<Character> possibleChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> suspects = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<ECharacterType> suspiciousTypes = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        suspiciousTypes.Add(ECharacterType.Demon);
        suspiciousTypes.Add(ECharacterType.Demon);
        suspiciousTypes.Add(ECharacterType.Demon);
        suspiciousTypes.Add(ECharacterType.Minion);
        suspiciousTypes.Add(ECharacterType.Minion);
        suspiciousTypes.Add(ECharacterType.Outcast);
        foreach (Character character in Gameplay.CurrentCharacters) possibleChars.Add(character);
        possibleChars.Remove(charRef);
        foreach (Character character in possibleChars)
        {
            if (CharacterHelper.CheckIfDisguisedAppearance(character)) suspects.Add(character);
            if (CharacterHelper.CheckLyingAppearance(character)) suspects.Add(character);
            foreach (ECharacterType type in suspiciousTypes)
            {
                if (character.GetRegisterAs().type == type) suspects.Add(character);
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil) suspects.Add(character);
        }
        if (suspects.Count == 0)
        {
            return new ActedInfo($"{dogName} loves everyone!");
        }
        suspects = sharedScripts.SortList(suspects);
        sharedScripts.DebugMessage($"Hound Tamer suspects: {sharedScripts.MentionEveryCharacterInList(suspects, "")}");

        Character chosenSuspect = sharedScripts.GetRandomItemOfList(suspects);
        return sharedScripts.ReturnInfoWithSingleSelection($"{dogName} is growling at #{chosenSuspect.id}", chosenSuspect);
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        wx_SavedScripts sharedScripts = new wx_SavedScripts();
        Il2CppSystem.Collections.Generic.List<Character> possibleChars = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> suspects = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> fakeSuspects = new Il2CppSystem.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<ECharacterType> suspiciousTypes = new Il2CppSystem.Collections.Generic.List<ECharacterType>();
        suspiciousTypes.Add(ECharacterType.Demon);
        suspiciousTypes.Add(ECharacterType.Demon);
        suspiciousTypes.Add(ECharacterType.Demon);
        suspiciousTypes.Add(ECharacterType.Minion);
        suspiciousTypes.Add(ECharacterType.Minion);
        suspiciousTypes.Add(ECharacterType.Outcast);
        foreach (Character character in Gameplay.CurrentCharacters) possibleChars.Add(character);
        possibleChars.Remove(charRef);
        foreach (Character character in possibleChars)
        {
            if (CharacterHelper.CheckIfDisguisedAppearance(character)) suspects.Add(character);
            if (CharacterHelper.CheckLyingAppearance(character)) suspects.Add(character);
            foreach (ECharacterType type in suspiciousTypes)
            {
                if (character.GetRegisterAs().type == type) suspects.Add(character);
            }
            if (character.GetRegisterAlignment() == EAlignment.Evil) suspects.Add(character);
        }

        foreach (Character character in possibleChars)
        {
            if (!suspects.Contains(character)) fakeSuspects.Add(character);
        }
        fakeSuspects.Remove(charRef);
        if (fakeSuspects.Count == 0)
        {
            return new ActedInfo($"{dogName} loves everyone!");
        }
        fakeSuspects = sharedScripts.SortList(fakeSuspects);
        sharedScripts.DebugMessage($"Fake Hound Tamer suspects: {sharedScripts.MentionEveryCharacterInList(fakeSuspects, "")}");

        Character chosenSuspect = sharedScripts.GetRandomItemOfList(fakeSuspects);
        return sharedScripts.ReturnInfoWithSingleSelection($"{dogName} is growling at #{chosenSuspect.id}", chosenSuspect);
    }
    public override string Description
    {
        get
        {
            return "Learn a character who isn't trustworthy";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            dogName = GetDogName();
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
            dogName = GetDogName();
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }

    public string GetDogName()
    {
        Il2CppSystem.Collections.Generic.List<string> dogNames = new Il2CppSystem.Collections.Generic.List<string>();
        // Sourced from https://www.dhot.com.au/wp-content/uploads/2021/09/A-Z-of-Dog-Names-A4-Document-1.pdf
        dogNames.Add("Acacia");
        dogNames.Add("Addison");
        dogNames.Add("Ajax");
        dogNames.Add("Alan");
        dogNames.Add("Albie");
        dogNames.Add("Alice");
        dogNames.Add("Amber");
        dogNames.Add("Ambrose");
        dogNames.Add("Amelia");
        dogNames.Add("Angel");
        dogNames.Add("Angus");
        dogNames.Add("Annie");
        dogNames.Add("Apache");
        dogNames.Add("Apollo");
        dogNames.Add("Archer");
        dogNames.Add("Arlo");
        dogNames.Add("Arthur");
        dogNames.Add("Ash");
        dogNames.Add("Astrid");
        dogNames.Add("Astro");
        dogNames.Add("Atlas");
        dogNames.Add("Aubrey");
        dogNames.Add("Audrey");
        dogNames.Add("Aurora");
        dogNames.Add("Austin");
        dogNames.Add("Ava");
        dogNames.Add("Baffin");
        dogNames.Add("Bailey");
        dogNames.Add("Bambi");
        dogNames.Add("Banana");
        dogNames.Add("Bandit");
        dogNames.Add("Banjo");
        dogNames.Add("Barron");
        dogNames.Add("Barry");
        dogNames.Add("Baxter");
        dogNames.Add("Benji");
        dogNames.Add("Benny");
        dogNames.Add("Benson");
        dogNames.Add("Bessie");
        dogNames.Add("Bethany");
        dogNames.Add("Betty");
        dogNames.Add("Billie");
        dogNames.Add("Billy");
        dogNames.Add("Birch");
        dogNames.Add("Biscuit");
        dogNames.Add("Blair");
        dogNames.Add("Blaze");
        dogNames.Add("Blitz");
        dogNames.Add("Blue");
        dogNames.Add("Bo");
        dogNames.Add("Bob");
        dogNames.Add("Bolt");
        dogNames.Add("Bonnie");
        dogNames.Add("Boof");
        dogNames.Add("Boston");
        dogNames.Add("Bowie");
        dogNames.Add("Brady");
        dogNames.Add("Brandon");
        dogNames.Add("Brandy");
        dogNames.Add("Brigett");
        dogNames.Add("Bronson");
        dogNames.Add("Bruce");
        dogNames.Add("Bruno");
        dogNames.Add("Bubbles");
        dogNames.Add("Buddy");
        dogNames.Add("Bullet");
        dogNames.Add("Bumble");
        dogNames.Add("Burt");
        dogNames.Add("Buster");
        dogNames.Add("Byron");
        dogNames.Add("Caesar");
        dogNames.Add("Cali");
        dogNames.Add("Calvin");
        dogNames.Add("Carla");
        dogNames.Add("Casey");
        dogNames.Add("Cass");
        dogNames.Add("Cassandra");
        dogNames.Add("Cassie");
        dogNames.Add("Chad");
        dogNames.Add("Champ");
        dogNames.Add("Clancey");
        dogNames.Add("Clare");
        dogNames.Add("Clarence");
        dogNames.Add("Clayton");
        dogNames.Add("Cleo");
        dogNames.Add("Cloud");
        dogNames.Add("Clover");
        dogNames.Add("Clyde");
        dogNames.Add("Cole");
        dogNames.Add("Colt");
        dogNames.Add("Comet");
        dogNames.Add("Conan");
        dogNames.Add("Cooper");
        dogNames.Add("Cosmo");
        dogNames.Add("Cougar");
        dogNames.Add("Cowboy");
        dogNames.Add("Cruiser");
        dogNames.Add("Cupcake");
        dogNames.Add("Curtis");
        dogNames.Add("Cypher");
        dogNames.Add("Cyril");
        dogNames.Add("Daisy");
        dogNames.Add("Dakota");
        dogNames.Add("Danni");
        dogNames.Add("Darren");
        dogNames.Add("Dash");
        dogNames.Add("Davey");
        dogNames.Add("Davis");
        dogNames.Add("Dax");
        dogNames.Add("Daxton");
        dogNames.Add("Delanie");
        dogNames.Add("Delilah");
        dogNames.Add("Delta");
        dogNames.Add("Denzel");
        dogNames.Add("Destiny");
        dogNames.Add("Dexter");
        dogNames.Add("Diamond");
        dogNames.Add("Diaz");
        dogNames.Add("Diego");
        dogNames.Add("Diesel");
        dogNames.Add("Dirk");
        dogNames.Add("Ditzy");
        dogNames.Add("DJ");
        dogNames.Add("Dorothy");
        dogNames.Add("Drake");
        dogNames.Add("Drover");
        dogNames.Add("Dudley");
        dogNames.Add("Duffy");
        dogNames.Add("Duke");
        dogNames.Add("Duncan");
        dogNames.Add("Dusty");
        dogNames.Add("Eddie");
        dogNames.Add("Eli");
        dogNames.Add("Ella");
        dogNames.Add("Ellis");
        dogNames.Add("Elsa");
        dogNames.Add("Elvis");
        dogNames.Add("Ember");
        dogNames.Add("Emerald");
        dogNames.Add("Emma");
        dogNames.Add("Endo");
        dogNames.Add("Eric");
        dogNames.Add("Ernie");
        dogNames.Add("Evie");
        dogNames.Add("Ezekiel");
        dogNames.Add("Faith");
        dogNames.Add("Fawn");
        dogNames.Add("Felix");
        dogNames.Add("Fergus");
        dogNames.Add("Fifi");
        dogNames.Add("Finn");
        dogNames.Add("Fiona");
        dogNames.Add("Flick");
        dogNames.Add("Florence");
        dogNames.Add("Floyd");
        dogNames.Add("Foxy");
        dogNames.Add("Freddy");
        dogNames.Add("Frost");
        dogNames.Add("Garfield");
        dogNames.Add("Garfunkle");
        dogNames.Add("Gary");
        dogNames.Add("Gina");
        dogNames.Add("Gizmo");
        dogNames.Add("Glenn");
        dogNames.Add("Gonzalez");
        dogNames.Add("Gordon");
        dogNames.Add("Gregory");
        dogNames.Add("Griffin");
        dogNames.Add("Grimm");
        dogNames.Add("Gus");
        dogNames.Add("Gwen");
        dogNames.Add("Gypsy");
        dogNames.Add("Hades");
        dogNames.Add("Hagrid");
        dogNames.Add("Halo");
        dogNames.Add("Harbor");
        dogNames.Add("Harley");
        dogNames.Add("Harper");
        dogNames.Add("Harry");
        dogNames.Add("Harvey");
        dogNames.Add("Hector");
        dogNames.Add("Henry");
        dogNames.Add("Herbie");
        dogNames.Add("Hermione");
        dogNames.Add("Hero");
        dogNames.Add("Hobbes");
        dogNames.Add("Holden");
        dogNames.Add("Holly");
        dogNames.Add("Hondo");
        dogNames.Add("Honey");
        dogNames.Add("Hope");
        dogNames.Add("Howie");
        dogNames.Add("Hubble");
        dogNames.Add("Hudson");
        dogNames.Add("Huey");
        dogNames.Add("Huxley");
        dogNames.Add("Hyde");
        dogNames.Add("Hydro");
        dogNames.Add("Ian");
        dogNames.Add("Iggy");
        dogNames.Add("Indi");
        dogNames.Add("Indiana");
        dogNames.Add("Isla");
        dogNames.Add("Ivy");
        dogNames.Add("Izzy");
        dogNames.Add("Jake");
        dogNames.Add("Jango");
        dogNames.Add("Janice");
        dogNames.Add("Jarred");
        dogNames.Add("Jasmine");
        dogNames.Add("Jasper");
        dogNames.Add("Jaycee");
        dogNames.Add("Jayme");
        dogNames.Add("Jayrod");
        dogNames.Add("Jeff");
        dogNames.Add("Jengo");
        dogNames.Add("Jerry");
        dogNames.Add("Jinx");
        dogNames.Add("Jock");
        dogNames.Add("Johnny");
        dogNames.Add("Joker");
        dogNames.Add("Jose");
        dogNames.Add("Josie");
        dogNames.Add("Judge");
        dogNames.Add("Jules");
        dogNames.Add("Junior");
        dogNames.Add("Jupiter");
        dogNames.Add("Justice");
        dogNames.Add("Jynx");
        dogNames.Add("Kade");
        dogNames.Add("Kaos");
        dogNames.Add("Karen");
        dogNames.Add("Karl");
        dogNames.Add("Karma");
        dogNames.Add("Katana");
        dogNames.Add("Katelyn");
        dogNames.Add("Kaylee");
        dogNames.Add("Keanu");
        dogNames.Add("Keeper");
        dogNames.Add("Kelly");
        dogNames.Add("Kelpie");
        dogNames.Add("Kenai");
        dogNames.Add("Kenko");
        dogNames.Add("Kennie");
        dogNames.Add("Kenzo");
        dogNames.Add("Kesha");
        dogNames.Add("Kiera");
        dogNames.Add("Kilo");
        dogNames.Add("Kim");
        dogNames.Add("Kimba");
        dogNames.Add("Kinglsey");
        dogNames.Add("Kip");
        dogNames.Add("Kiri");
        dogNames.Add("Knox");
        dogNames.Add("Kobi");
        dogNames.Add("Koops");
        dogNames.Add("Kosta");
        dogNames.Add("Lacey");
        dogNames.Add("Laddie");
        dogNames.Add("Lady");
        dogNames.Add("Larry");
        dogNames.Add("Layla");
        dogNames.Add("Lenny");
        dogNames.Add("Leo");
        dogNames.Add("Leon");
        dogNames.Add("Leroy");
        dogNames.Add("Levi");
        dogNames.Add("Lewis");
        dogNames.Add("Lightning");
        dogNames.Add("Lincoln");
        dogNames.Add("Logan");
        dogNames.Add("Loki");
        dogNames.Add("Lola");
        dogNames.Add("Lucky");
        dogNames.Add("Lucy");
        dogNames.Add("Luger");
        dogNames.Add("Luke");
        dogNames.Add("Lulu");
        dogNames.Add("Luna");
        dogNames.Add("Lunar");
        dogNames.Add("Luther");
        dogNames.Add("Lydia");
        dogNames.Add("Lynx");
        dogNames.Add("Lyra");
        dogNames.Add("Mac");
        dogNames.Add("Macey");
        dogNames.Add("Mackenzie");
        dogNames.Add("Madison");
        dogNames.Add("Mae");
        dogNames.Add("Maggie");
        dogNames.Add("Magic");
        dogNames.Add("Magnet");
        dogNames.Add("Mahli");
        dogNames.Add("Maisie");
        dogNames.Add("Major");
        dogNames.Add("Mambo");
        dogNames.Add("Mango");
        dogNames.Add("Maple");
        dogNames.Add("Marco");
        dogNames.Add("Mason");
        dogNames.Add("Matilda");
        dogNames.Add("Maverick");
        dogNames.Add("Maya");
        dogNames.Add("Memphis");
        dogNames.Add("Merlin");
        dogNames.Add("Mickey");
        dogNames.Add("Middy");
        dogNames.Add("Midnight");
        dogNames.Add("Mikey");
        dogNames.Add("Mila");
        dogNames.Add("Millie");
        dogNames.Add("Milo");
        dogNames.Add("Mimi");
        dogNames.Add("Minnie");
        dogNames.Add("Mino");
        dogNames.Add("Mischief");
        dogNames.Add("Missy");
        dogNames.Add("Mist");
        dogNames.Add("Misty");
        dogNames.Add("Moe");
        dogNames.Add("Molly");
        dogNames.Add("Monty");
        dogNames.Add("Moon");
        dogNames.Add("Moose");
        dogNames.Add("Morpheus");
        dogNames.Add("Moshi");
        dogNames.Add("Muddy");
        dogNames.Add("Mulan");
        dogNames.Add("Murphy");
        dogNames.Add("Muscles");
        dogNames.Add("Mutley");
        dogNames.Add("Myla");
        dogNames.Add("Nala");
        dogNames.Add("Nancy");
        dogNames.Add("Natalie");
        dogNames.Add("Nate");
        dogNames.Add("Natta");
        dogNames.Add("Nayla");
        dogNames.Add("Ned");
        dogNames.Add("Neo");
        dogNames.Add("Nermal");
        dogNames.Add("Nessa");
        dogNames.Add("Neville");
        dogNames.Add("Nina");
        dogNames.Add("Nitro");
        dogNames.Add("Noodle");
        dogNames.Add("Nova");
        dogNames.Add("Nugget");
        dogNames.Add("Oak");
        dogNames.Add("Oakley");
        dogNames.Add("Oblivion");
        dogNames.Add("Odie");
        dogNames.Add("Odin");
        dogNames.Add("Oleander");
        dogNames.Add("Olive");
        dogNames.Add("Oliver");
        dogNames.Add("Ollie");
        dogNames.Add("Onyx");
        dogNames.Add("Orlando");
        dogNames.Add("Oscar");
        dogNames.Add("Otto");
        dogNames.Add("Pablo");
        dogNames.Add("Pacho");
        dogNames.Add("Paddy");
        dogNames.Add("Pancho");
        dogNames.Add("Panda");
        dogNames.Add("Pandora");
        dogNames.Add("Parker");
        dogNames.Add("Pat");
        dogNames.Add("Patches");
        dogNames.Add("Patrick");
        dogNames.Add("Patsy");
        dogNames.Add("Pawson");
        dogNames.Add("Peanut");
        dogNames.Add("Pearl");
        dogNames.Add("Pedro");
        dogNames.Add("Peggy");
        dogNames.Add("Penelope");
        dogNames.Add("Penny");
        dogNames.Add("Phil");
        dogNames.Add("Phillip");
        dogNames.Add("Phoebe");
        dogNames.Add("Piper");
        dogNames.Add("Pixi");
        dogNames.Add("Plum");
        dogNames.Add("Po");
        dogNames.Add("Pokie");
        dogNames.Add("Polar");
        dogNames.Add("Polo");
        dogNames.Add("Poppet");
        dogNames.Add("Poppy");
        dogNames.Add("Posey");
        //dogNames.Add("Prince");
        dogNames.Add("Princess");
        dogNames.Add("Priscilla");
        dogNames.Add("Pudding");
        dogNames.Add("Puddles");
        dogNames.Add("Pumba");
        dogNames.Add("Pup");
        dogNames.Add("Purple");
        dogNames.Add("Quinn");
        dogNames.Add("Rain");
        dogNames.Add("Ralph");
        dogNames.Add("Rambo");
        dogNames.Add("Rascal");
        dogNames.Add("Raspberry");
        dogNames.Add("Rayne");
        dogNames.Add("Red");
        dogNames.Add("Reggie");
        dogNames.Add("Remi");
        dogNames.Add("Remy");
        dogNames.Add("Rex");
        dogNames.Add("Rey");
        dogNames.Add("Reynold");
        dogNames.Add("Rhea");
        dogNames.Add("Riley");
        dogNames.Add("Ritz");
        dogNames.Add("Rocco");
        dogNames.Add("Rocket");
        dogNames.Add("Roger");
        dogNames.Add("Ron");
        dogNames.Add("Rook");
        dogNames.Add("Rory");
        dogNames.Add("Rosco");
        dogNames.Add("Rosie");
        dogNames.Add("Roughie");
        dogNames.Add("Roxie");
        dogNames.Add("Roy");
        dogNames.Add("Rubble");
        dogNames.Add("Ruby");
        dogNames.Add("Ruckus");
        dogNames.Add("Ruffles");
        dogNames.Add("Rufus");
        dogNames.Add("Runa");
        dogNames.Add("Russel");
        dogNames.Add("Rusty");
        dogNames.Add("Ryder");
        dogNames.Add("Ryker");
        dogNames.Add("Sadie");
        dogNames.Add("Sage");
        dogNames.Add("Sahara");
        dogNames.Add("Salem");
        dogNames.Add("Sally");
        dogNames.Add("Sam");
        dogNames.Add("Sampson");
        dogNames.Add("Sarge");
        dogNames.Add("Sasha");
        dogNames.Add("Sassy");
        dogNames.Add("Sausage");
        dogNames.Add("Savvy");
        dogNames.Add("Saxon");
        dogNames.Add("Scarlett");
        dogNames.Add("Scoob");
        dogNames.Add("Seth");
        dogNames.Add("Shadow");
        dogNames.Add("Shandy");
        dogNames.Add("Sheeba");
        dogNames.Add("Sheenie");
        dogNames.Add("Shelby");
        dogNames.Add("Sherbert");
        dogNames.Add("Sheriff");
        dogNames.Add("Sherman");
        dogNames.Add("Shinji");
        dogNames.Add("Shorty");
        dogNames.Add("Sierra");
        dogNames.Add("Silvie");
        dogNames.Add("Simon");
        dogNames.Add("Skip");
        dogNames.Add("Skye");
        dogNames.Add("Skylar");
        dogNames.Add("Smartie");
        dogNames.Add("Smash");
        dogNames.Add("Smith");
        dogNames.Add("Smithy");
        dogNames.Add("Snowball");
        dogNames.Add("Snowflake");
        dogNames.Add("Snowy");
        dogNames.Add("Soak");
        dogNames.Add("Sonnie");
        dogNames.Add("Sophy");
        dogNames.Add("Spanner");
        dogNames.Add("Sparkie");
        dogNames.Add("Sparrow");
        dogNames.Add("Spartan");
        dogNames.Add("Spike");
        dogNames.Add("Spirit");
        dogNames.Add("Spooky");
        dogNames.Add("Spot");
        dogNames.Add("Sprocket");
        dogNames.Add("Spruce");
        dogNames.Add("Stampy");
        dogNames.Add("Stan");
        dogNames.Add("Stella");
        dogNames.Add("Stewie");
        dogNames.Add("Stormy");
        dogNames.Add("Strawberry");
        dogNames.Add("Stretch");
        dogNames.Add("Strider");
        dogNames.Add("Stumpy");
        dogNames.Add("Sugar Pie");
        dogNames.Add("Sullivan");
        dogNames.Add("Sully");
        dogNames.Add("Summer");
        dogNames.Add("Sunny");
        dogNames.Add("Sunshine");
        dogNames.Add("Sushi");
        dogNames.Add("Susie");
        dogNames.Add("Suzie");
        dogNames.Add("Swansea");
        dogNames.Add("Sweetie");
        dogNames.Add("Sybil");
        dogNames.Add("Sylvester");
        dogNames.Add("Tabby");
        dogNames.Add("Tasman");
        dogNames.Add("Tayah");
        dogNames.Add("Teddy");
        dogNames.Add("Tenna");
        dogNames.Add("Tequila");
        dogNames.Add("Tex");
        dogNames.Add("Theia");
        dogNames.Add("Theo");
        dogNames.Add("Thor");
        dogNames.Add("Thrasher");
        dogNames.Add("Thunder");
        dogNames.Add("Tia");
        dogNames.Add("Tiffany");
        dogNames.Add("Tiger");
        dogNames.Add("Tiki");
        dogNames.Add("Timber");
        dogNames.Add("Timmy");
        dogNames.Add("Tinker");
        dogNames.Add("Truffle");
        dogNames.Add("Tundra");
        dogNames.Add("Turbo");
        dogNames.Add("Turner");
        dogNames.Add("Twiggy");
        dogNames.Add("Twitch");
        dogNames.Add("Tyson");
        dogNames.Add("Valentino");
        dogNames.Add("Vanilla");
        dogNames.Add("Violet");
        dogNames.Add("Vixen");
        dogNames.Add("Watson");
        dogNames.Add("Wendy");
        dogNames.Add("Wesley");
        dogNames.Add("Whisky");
        dogNames.Add("Whisper");
        dogNames.Add("Willow");
        dogNames.Add("Wilson");
        dogNames.Add("Winnie");
        dogNames.Add("Winston");
        dogNames.Add("Winter");
        dogNames.Add("Wishbone");
        dogNames.Add("Wizz");
        dogNames.Add("Woody");
        dogNames.Add("Xanthe");
        dogNames.Add("Xavier");
        dogNames.Add("Xena");
        dogNames.Add("Yoda");
        dogNames.Add("Zali");
        dogNames.Add("Zander");
        dogNames.Add("Zane");
        dogNames.Add("Zara");
        dogNames.Add("Zeke");
        dogNames.Add("Zeus");
        dogNames.Add("Zip");
        dogNames.Add("Zoe");
        dogNames.Add("Zuzu");
        // this list has 568 names in it, good lord
        return dogNames[UnityEngine.Random.RandomRangeInt(0, dogNames.Count)];
    }
    public w_HoundTamer() : base(ClassInjector.DerivedConstructorPointer<w_HoundTamer>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_HoundTamer(IntPtr ptr) : base(ptr)
    {
    }
}


