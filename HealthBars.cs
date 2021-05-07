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
using ImGuiNET;
using SharpDX;

namespace HealthBars
{
    public class HealthBars : BaseSettingsPlugin<HealthBarsSettings>
    {
        private Camera camera;
        private bool CanTick = true;
        private readonly List<Element> ElementForSkip = new List<Element>();
        private string IGNORE_FILE { get; } = Path.Combine("config", "ignored_entities.txt");
        private List<string> IgnoredEntities { get; set; }

        private IngameUIElements ingameUI;
        private CachedValue<bool> ingameUICheckVisible;
        private Vector2 oldplayerCord;
        private Entity Player;
        private HealthBar PlayerBar;
        private RectangleF windowRectangle;
        private Size2F windowSize;
        public static HealthBars Plugin;

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

            if (Plugin == null)
                Plugin = this;

            return true;
        }

        private void ReadIgnoreFile()
        {
            var path = Path.Combine(DirectoryFullName, IGNORE_FILE);
            if (File.Exists(path))
                IgnoredEntities = File.ReadAllLines(path).Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("#")).ToList();
            else
                LogError($"Ignored entities file does not exist. Path: {path}");
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
                Graphics.DrawBox(bar.BackGround, bar.Settings.Background);
                Graphics.DrawBox(new RectangleF(bar.BackGround.X, bar.BackGround.Y, bar.HpWidth, bar.BackGround.Height), bar.Color);
            }
            else
            {
                Graphics.DrawImage("healthbar.png", bar.BackGround, bar.Settings.Background);

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
            if (!bar.Settings.ShowHealthCurrentValue && !bar.Settings.ShowEnergyShieldCurrentValue && !bar.Settings.ShowEffectiveHealthPointsCurrentValue) return;

            // Do not forget change description on settings if order was changed
            string healthBarText = "";
            if (bar.Settings.ShowEffectiveHealthPointsCurrentValue)
            {
                healthBarText = $"{(bar.Life.CurHP + bar.Life.CurES):N0}";
                if (bar.Settings.ShowEffectiveHealthPointsMaximumValue)
                    healthBarText += $"/{(bar.Life.MaxHP + bar.Life.MaxES):N0}";
            } 
            else if (bar.Settings.ShowEnergyShieldCurrentValue && bar.Life.CurES > 0)
            {
                healthBarText = $"{bar.Life.CurES:N0}";
                if (bar.Settings.ShowEnergyShieldMaximumValue)
                    healthBarText += $"/{bar.Life.MaxES:N0}";
            }
            else if (bar.Settings.ShowHealthCurrentValue)
            {
                healthBarText = $"{bar.Life.CurHP:N0}";
                if (bar.Settings.ShowHealthMaximumValue)
                    healthBarText += $"/{bar.Life.MaxHP:N0}";
            }

            Graphics.DrawText(healthBarText,
                new Vector2(bar.BackGround.Center.X, bar.BackGround.Center.Y - Graphics.Font.Size / 2f),
                bar.Settings.HealthTextColor,
                FontAlign.Center);
        }

        private void ShowPercents(HealthBar bar)
        {
            if (!bar.Settings.ShowHealthPercentages && !bar.Settings.ShowEnergyShieldPercentages && !bar.Settings.ShowEffectiveHealthPointsPercentages) return;

            // Do not forget change description on settings if order was changed
            float percents = 0;
            if (bar.Settings.ShowEffectiveHealthPointsPercentages)
            {
                float curEHP = bar.Life.CurES + bar.Life.CurHP;
                float maxEHP = bar.Life.MaxES + bar.Life.MaxHP;
                percents = curEHP / maxEHP;
            }
            else if (bar.Settings.ShowEnergyShieldPercentages && bar.Life.CurES > 0)
            {
                percents = bar.Life.ESPercentage;
            }
            else if (bar.Settings.ShowHealthPercentages)
            {
                percents = bar.Life.HPPercentage;
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
            if (IgnoredEntities.Any(x => Entity.Path.StartsWith(x))) return;
            Entity.SetHudComponent(new HealthBar(Entity, Settings));
        }

        public override void DrawSettings()
        {
            if (Settings.Enable) ImGuiDrawSettings.DrawImGuiSettings();
            else ImGui.Text("Enable HealthBars plugin to display settings.");
        }
    }
}
