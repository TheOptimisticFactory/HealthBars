using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared.Cache;
using ExileCore.Shared.Enums;
using SharpDX;

namespace HealthBars
{
    public class HealthBars : BaseSettingsPlugin<HealthBarsSettings>
    {
        private Camera camera;
        private bool CanTick = true;
        private readonly List<Element> ElementForSkip = new List<Element>();
        private const string IGNORE_FILE = "IgnoredEntities.txt";
        private List<string> IgnoredSum;

        private readonly List<string> Ignored = new List<string>
        {
            // Delirium Ignores
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes1",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes2",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes3",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes2",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes3",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple1",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple2",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple3",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatFillet1Vanish",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatFillet2Vanish",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatRhoa1Vanish",
            "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatRhoa2Vanish",
            
            // Conquerors Ignores
            "Metadata/Monsters/AtlasExiles/AtlasExile1@",
            "Metadata/Monsters/AtlasExiles/CrusaderInfluenceMonsters/CrusaderArcaneRune",
            "Metadata/Monsters/AtlasExiles/AtlasExile2_",
            "Metadata/Monsters/AtlasExiles/EyrieInfluenceMonsters/EyrieFrostnadoDaemon",
            "Metadata/Monsters/AtlasExiles/AtlasExile3@",
            "Metadata/Monsters/AtlasExiles/AtlasExile3AcidPitDaemon",
            "Metadata/Monsters/AtlasExiles/AtlasExile3BurrowingViperMelee",
            "Metadata/Monsters/AtlasExiles/AtlasExile3BurrowingViperRanged",
            "Metadata/Monsters/AtlasExiles/AtlasExile4@",
            "Metadata/Monsters/AtlasExiles/AtlasExile4ApparitionCascade",
            "Metadata/Monsters/AtlasExiles/AtlasExile5Apparition",
            "Metadata/Monsters/AtlasExiles/AtlasExile5Throne",

            // Incursion Ignores
            "Metadata/Monsters/LeagueIncursion/VaalSaucerRoomTurret",
            "Metadata/Monsters/LeagueIncursion/VaalSaucerTurret",
            "Metadata/Monsters/LeagueIncursion/VaalSaucerTurret",
            
            // Betrayal Ignores
            "Metadata/Monsters/LeagueBetrayal/BetrayalTaserNet",
            "Metadata/Monsters/LeagueBetrayal/FortTurret/FortTurret1Safehouse",
            "Metadata/Monsters/LeagueBetrayal/FortTurret/FortTurret1",
            "Metadata/Monsters/LeagueBetrayal/MasterNinjaCop",
            "Metadata/Monsters/LeagueBetrayal/BetrayalRikerMortarDaemon",
            "Metadata/Monsters/LeagueBetrayal/BetrayalBoneNovaDaemon",
            "Metadata/Monsters/LeagueBetrayal/BetrayalCatarinaPillarDaemon_",
            "Metadata/Monsters/LeagueBetrayal/BetrayalUpgrades/BetrayalDaemonCorpseConsume",
            
            // Legion Ignores
            "Metadata/Monsters/LegionLeague/LegionVaalGeneralProjectileDaemon",
            "Metadata/Monsters/LegionLeague/LegionSergeantStampedeDaemon",
            "Metadata/Monsters/LegionLeague/LegionSandTornadoDaemon",

            // Random Ignores
            "Metadata/Monsters/InvisibleFire/InvisibleSandstorm_",
            "Metadata/Monsters/InvisibleFire/InvisibleFrostnado",
            "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionDemonColdDegen",
            "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionDemonColdDegenUnique",
            "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionCorpseDegen",
            "Metadata/Monsters/InvisibleFire/InvisibleFireEyrieHurricane",
            "Metadata/Monsters/InvisibleFire/InvisibleIonCannonFrost",
            "Metadata/Monsters/InvisibleFire/AfflictionBossFinalDeathZone",
            "Metadata/Monsters/InvisibleFire/InvisibleFireDoedreSewers",
            "Metadata/Monsters/InvisibleFire/InvisibleFireDelveFlameTornadoSpiked",
            "Metadata/Monsters/InvisibleFire/InvisibleHolyCannon",
            "Metadata/Monsters/InvisibleFire/DelveVaalBossInvisibleLight",

            "Metadata/Monsters/InvisibleCurse/InvisibleFrostbiteStationary",
            "Metadata/Monsters/InvisibleCurse/InvisibleConductivityStationary",
            "Metadata/Monsters/InvisibleCurse/InvisibleEnfeeble",

            "Metadata/Monsters/InvisibleAura/InvisibleWrathStationary",

            // "Metadata/Monsters/Labyrinth/GoddessOfJustice",
            // "Metadata/Monsters/Labyrinth/GoddessOfJusticeMapBoss",
            "Metadata/Monsters/Frog/FrogGod/SilverOrb",
            "Metadata/Monsters/Frog/FrogGod/SilverPool",
            "Metadata/Monsters/LunarisSolaris/SolarisCelestialFormAmbushUniqueMap",
            "Metadata/Monsters/Invisible/MaligaroSoulInvisibleBladeVortex",
            "Metadata/Monsters/Daemon",
            "Metadata/Monsters/Daemon/MaligaroBladeVortexDaemon",
            "Metadata/Monsters/Daemon/SilverPoolChillDaemon",
            "Metadata/Monsters/AvariusCasticus/AvariusCasticusStatue",
            "Metadata/Monsters/Maligaro/MaligaroDesecrate",

            "Metadata/Monsters/Avatar/AvatarMagmaOrbDaemon",
            "Metadata/Monsters/Monkeys/FlameBearerTalismanT2Ghost",
            "Metadata/Monsters/Totems/TalismanTotem/TalismanTotemDeathscape",
            "Metadata/Monsters/BeehiveBehemoth/BeehiveBehemothSwampDaemon",
            "Metadata/Monsters/VaalWraith/VaalWraithChampionMinion",
            
            // Synthesis
            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret1",
            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret2",
            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret3",
            "Metadata/Monsters/LeagueSynthesis/SynthesisDroneBossTurret4",
            "Metadata/Monsters/LeagueSynthesis/SynthesisWalkerSpawned_",

            //Ritual
            "Metadata/Monsters/LeagueRitual/FireMeteorDaemon",
            "Metadata/Monsters/LeagueRitual/GenericSpeedDaemon",
            "Metadata/Monsters/LeagueRitual/ColdRotatingBeamDaemon",
            "Metadata/Monsters/LeagueRitual/ColdRotatingBeamDaemonUber",
            "Metadata/Monsters/LeagueRitual/GenericEnergyShieldDaemon",
            "Metadata/Monsters/LeagueRitual/GenericMassiveDaemon",
            "Metadata/Monsters/LeagueRitual/ChaosGreenVinesDaemon_",
            "Metadata/Monsters/LeagueRitual/ChaosSoulrendPortalDaemon",
            "Metadata/Monsters/LeagueRitual/VaalAtziriDaemon",
            "Metadata/Monsters/LeagueRitual/LightningPylonDaemon",

            // Bestiary
            "Metadata/Monsters/LeagueBestiary/RootSpiderBestiaryAmbush",
            "Metadata/Monsters/LeagueBestiary/BlackScorpionBestiaryBurrowTornado",
            "Metadata/Monsters/LeagueBestiary/ModDaemonCorpseEruption",
            "Metadata/Monsters/LeagueBestiary/ModDaemonSandLeaperExplode1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonStampede1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonGraspingPincers1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonPouncingShade1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonPouncingShadeQuickHit",
            "Metadata/Monsters/LeagueBestiary/ModDaemonFire1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonVultureBomb1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonVultureBombCast1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonParasiticSquid1",
            "Metadata/Monsters/LeagueBestiary/ModDaemonBloodRaven1",
            "Metadata/Monsters/LeagueBestiary/SandLeaperBestiaryClone",
            "Metadata/Monsters/LeagueBestiary/SpiderPlagueBestiaryExplode",
            "Metadata/Monsters/LeagueBestiary/ParasiticSquidBestiaryClone",
            "Metadata/Monsters/LeagueBestiary/HellionBestiaryClone",
            "Metadata/Monsters/LeagueBestiary/BestiarySpiderCocoon",

            // Ritual
            "Metadata/Monsters/LeagueRitual/GoldenCoinDaemon",
            "Metadata/Monsters/LeagueRitual/GenericLifeDaemon",
            "Metadata/Monsters/LeagueRitual/GenericChargesDaemon",
        };

        private IngameUIElements ingameUI;
        private CachedValue<bool> ingameUICheckVisible;
        private Vector2 oldplayerCord;
        private Entity Player;
        private HealthBar PlayerBar;
        private RectangleF windowRectangle;
        private Size2F windowSize;

        public override void OnLoad()
        {
            CanUseMultiThreading = true;
            Graphics.InitImage("healthbar.png");
        }

        public override bool Initialise()
        {
            Player = GameController.Player;
            ingameUI = GameController.IngameState.IngameUi;
            PlayerBar = new HealthBar(Player, Settings);

            GameController.EntityListWrapper.PlayerUpdate += (sender, args) =>
            {
                Player = GameController.Player;

                PlayerBar = new HealthBar(Player, Settings);
            };

            ingameUICheckVisible = new TimeCache<bool>(() =>
            {
                windowRectangle = GameController.Window.GetWindowRectangleReal();
                windowSize = new Size2F(windowRectangle.Width / 2560, windowRectangle.Height / 1600);
                camera = GameController.Game.IngameState.Camera;

                return ingameUI.SyndicatePanel.IsVisibleLocal || ingameUI.SellWindow.IsVisibleLocal ||
                       ingameUI.DelveWindow.IsVisibleLocal || ingameUI.IncursionWindow.IsVisibleLocal ||
                       ingameUI.UnveilWindow.IsVisibleLocal || ingameUI.TreePanel.IsVisibleLocal || ingameUI.Atlas.IsVisibleLocal ||
                       ingameUI.CraftBench.IsVisibleLocal;
            }, 250);
            ReadIgnoreFile();

            return true;
        }
        private void CreateIgnoreFile()
        {
            var path = $"{DirectoryFullName}\\{IGNORE_FILE}";
            
            var defaultConfig =
            #region default Config
                "#default ignores\n" +
                "Metadata/Monsters/Daemon/SilverPoolChillDaemon\n" +
                "Metadata/Monsters/Daemon\n" +
                "Metadata/Monsters/Frog/FrogGod/SilverOrb\n" +
                "Metadata/Monsters/Frog/FrogGod/SilverPool\n" +
                "Metadata/Monsters/Labyrinth/GoddessOfJusticeMapBoss@7\n" +
                "Metadata/Monsters/Labyrinth/GoddessOfJustice@\n" +
                "Metadata/Monsters/LeagueBetrayal/MasterNinjaCop\n" +
                "#Delirium Ignores\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes1\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes2\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonEyes3\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes2\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonSpikes3\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple1\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple2\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonPimple3\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatFillet1Vanish\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatFillet2Vanish\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatRhoa1Vanish\n" +
                "Metadata/Monsters/LeagueAffliction/DoodadDaemons/DoodadDaemonGoatRhoa2Vanish\n" +
                "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionCorpseDegen\n" +
                "Metadata/Monsters/InvisibleFire/InvisibleFireAfflictionDemonColdDegenUnique\n";
            #endregion
            if (File.Exists(path)) return;
            using (var streamWriter = new StreamWriter(path, true))
            {
                streamWriter.Write(defaultConfig);
                streamWriter.Close();
            }
        }
        private void ReadIgnoreFile()
        {
            var path = $"{DirectoryFullName}\\{IGNORE_FILE}";
            if (File.Exists(path)) 
            {
                var text = File.ReadAllLines(path).Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#")).ToList();
                IgnoredSum = Ignored.Concat(text).ToList();
            } else 
                CreateIgnoreFile();
        }

        public override void AreaChange(AreaInstance area)
        {
            ingameUI = GameController.IngameState.IngameUi;
            ReadIgnoreFile();
        }

        private bool SkipHealthBar(HealthBar healthBar)
        {
            if (healthBar == null) return true;
            if (healthBar.Settings == null) return true;
            if (!healthBar.Settings.Enable) return true;
            if (!healthBar.Entity.IsAlive) return true;
            if (healthBar.HpPercent < 0.001f) return true;
            if (healthBar.Type == CreatureType.Minion && healthBar.HpPercent * 100 > Settings.ShowMinionOnlyBelowHp) return true;
            if (healthBar.Entity.League == LeagueType.Legion && healthBar.Entity.IsHidden && healthBar.Entity.Rarity != MonsterRarity.Unique) return true;

            return false;
        }

        public void HpBarWork(HealthBar healthBar)
        {
            if (healthBar == null) return;
            healthBar.Skip = SkipHealthBar(healthBar);
            if (healthBar.Skip) return;

            var healthBarDistance = healthBar.Distance;
            if (healthBarDistance > Settings.LimitDrawDistance)
            {
                healthBar.Skip = true;
                return;
            }

            var worldCoords = healthBar.Entity.Pos;
            worldCoords.Z += Settings.GlobalZ;
            var mobScreenCoords = camera.WorldToScreen(worldCoords);
            if (mobScreenCoords == Vector2.Zero) return;
            var scaledWidth = healthBar.Settings.Width * windowSize.Width;
            var scaledHeight = healthBar.Settings.Height * windowSize.Height;

            healthBar.BackGround = new RectangleF(mobScreenCoords.X - scaledWidth / 2f, mobScreenCoords.Y - scaledHeight / 2f, scaledWidth,
                scaledHeight);

            if (healthBarDistance > 80 && !windowRectangle.Intersects(healthBar.BackGround))
            {
                healthBar.Skip = true;
                return;
            }

            foreach (var forSkipBar in ElementForSkip)
            {
                if (forSkipBar.IsVisibleLocal && forSkipBar.GetClientRectCache.Intersects(healthBar.BackGround))
                {
                    healthBar.Skip = true;
                }
            }

            healthBar.HpWidth = healthBar.HpPercent * scaledWidth;
            healthBar.EsWidth = healthBar.Life.ESPercentage * scaledWidth;
        }

        public override Job Tick()
        {
            if (Settings.MultiThreading && GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster].Count >=
                Settings.MultiThreadingCountEntities)
            {
                return new Job(nameof(HealthBars), TickLogic);

                // return GameController.MultiThreadManager.AddJob(TickLogic, nameof(HealthBars));
            }

            TickLogic();
            return null;
        }

        private void TickLogic()
        {
            CanTick = true;

            if (ingameUICheckVisible == null
                || ingameUICheckVisible.Value
                || camera == null
                || GameController.Area.CurrentArea.IsTown && !Settings.ShowInTown)
            {
                CanTick = false;
                return;
            }

            var monster = GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster];
            foreach (var validEntity in monster)
            {
                var healthBar = validEntity.GetHudComponent<HealthBar>();
                try
                {
                    HpBarWork(healthBar);
                }
                catch (Exception e)
                {
                    DebugWindow.LogError(e.Message);
                }
            }

            foreach (var validEntity in GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Player])
            {
                var healthBar = validEntity.GetHudComponent<HealthBar>();

                if (healthBar != null)
                    HpBarWork(healthBar);
            }
        }

        public override void Render()
        {
            if (!CanTick) return;

            foreach (var entity in GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Monster])
            {
                var healthBar = entity.GetHudComponent<HealthBar>();
                if (healthBar == null) continue;

                if (healthBar.Skip)
                {
                    healthBar.Skip = false;
                    continue;
                }

                DrawBar(healthBar);
            }

            foreach (var entity in GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Player])
            {
                var healthBar = entity.GetHudComponent<HealthBar>();
                if (healthBar == null) continue;

                if (healthBar.Skip)
                {
                    healthBar.Skip = false;
                    continue;
                }

                DrawBar(healthBar);
            }

            if (Settings.SelfHealthBarShow)
            {
                var worldCoords = PlayerBar.Entity.Pos;
                worldCoords.Z += Settings.PlayerZ;
                var result = camera.WorldToScreen(worldCoords);

                if (Math.Abs(oldplayerCord.X - result.X) < 40 || Math.Abs(oldplayerCord.X - result.Y) < 40)
                    result = oldplayerCord;
                else
                    oldplayerCord = result;

                var scaledWidth = PlayerBar.Settings.Width * windowSize.Width;
                var scaledHeight = PlayerBar.Settings.Height * windowSize.Height;

                PlayerBar.BackGround = new RectangleF(result.X - scaledWidth / 2f, result.Y - scaledHeight / 2f, scaledWidth,
                    scaledHeight);

                PlayerBar.HpWidth = PlayerBar.HpPercent * scaledWidth;
                PlayerBar.EsWidth = PlayerBar.Life.ESPercentage * scaledWidth;
                DrawBar(PlayerBar);
            }
        }

        public void DrawBar(HealthBar bar)
        {
            if (Settings.ImGuiRender)
            {
                Graphics.DrawBox(bar.BackGround, bar.Settings.BackGround);
                Graphics.DrawBox(new RectangleF(bar.BackGround.X, bar.BackGround.Y, bar.HpWidth, bar.BackGround.Height), bar.Color);
            }
            else
            {
                Graphics.DrawImage("healthbar.png", bar.BackGround, bar.Settings.BackGround);

                Graphics.DrawImage("healthbar.png", new RectangleF(bar.BackGround.X, bar.BackGround.Y, bar.HpWidth, bar.BackGround.Height),
                    bar.Color);
            }

            Graphics.DrawBox(new RectangleF(bar.BackGround.X, bar.BackGround.Y, bar.EsWidth, bar.BackGround.Height * 0.33f), Color.Aqua);
            bar.BackGround.Inflate(1, 1);
            Graphics.DrawFrame(bar.BackGround, bar.Settings.Outline, 1);

            ShowPercents(bar);
            ShowNumbersInHealthbar(bar);
        }

        private void ShowNumbersInHealthbar(HealthBar bar)
        {
            if (!bar.Settings.ShowHealthText && !bar.Settings.ShowEnergyShieldText) return;

            string healthBarText = "";
            if (bar.Settings.ShowEnergyShieldText && bar.Life.CurES > 0)
            {
                healthBarText = $"{bar.Life.CurES:N0}";
                if (bar.Settings.ShowMaxEnergyShieldText)
                    healthBarText += $"/{bar.Life.MaxES:N0}";
            } 
            else if (bar.Settings.ShowHealthText)
            {
                healthBarText = $"{bar.Life.CurHP:N0}";
                if (bar.Settings.ShowMaxHealthText)
                    healthBarText += $"/{bar.Life.MaxHP:N0}";
            }

            Graphics.DrawText(healthBarText,
                new Vector2(bar.BackGround.Center.X, bar.BackGround.Center.Y - Graphics.Font.Size / 2f),
                bar.Settings.HealthTextColor,
                FontAlign.Center);
        }

        private void ShowPercents(HealthBar bar)
        {
            if (!bar.Settings.ShowHealthPercents && !bar.Settings.ShowEnergyShieldPercents) return;

            float percents = 0;
            if (bar.Settings.ShowHealthPercents)
            {
                percents = bar.Life.HPPercentage;
            }
            else if (bar.Settings.ShowEnergyShieldPercents)
            {
                percents = bar.Life.ESPercentage;
            }

            Graphics.DrawText(FloatToPercentString(percents),
                new Vector2(bar.BackGround.Right, bar.BackGround.Center.Y - Graphics.Font.Size / 2f),
                bar.Settings.PercentTextColor);
        }

        private string FloatToPercentString (float number)
        {
            return $"{Math.Floor(number * 100).ToString(CultureInfo.InvariantCulture)}";
        }

        public override void EntityAdded(Entity Entity)
        {
            if (Entity.Type != EntityType.Monster && Entity.Type != EntityType.Player 
                || Entity.Address == GameController.Player.Address 
                || Entity.Type == EntityType.Daemon) return;

            if (Entity.HasComponent<Life>() && Entity.GetComponent<Life>() != null && !Entity.IsAlive) return;
            if (IgnoredSum.Any(x => Entity.Path.StartsWith(x))) return;
            if (Entity.Path.StartsWith("Metadata/Monsters/AtlasExiles/BasiliskInfluenceMonsters/BasiliskBurrowingViper") && (Entity.Rarity != MonsterRarity.Unique)) return;
            Entity.SetHudComponent(new HealthBar(Entity, Settings));
        }
    }
}
