using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace HealthBars
{
    public class HealthBarsSettings : ISettings
    {
        public HealthBarsSettings()
        {
            Enable = new ToggleNode(false);
            ShowInTown = new ToggleNode(false);
            ShowES = new ToggleNode(true);
            ShowEnemies = new ToggleNode(true);
            Players = new UnitSettings(0x008000ff, 0);
            Minions = new UnitSettings(0x90ee90ff, 0);
            NormalEnemy = new UnitSettings(0xff0000ff, 0, 0x66ff66ff, false, false, false, 100, 3);
            MagicEnemy = new UnitSettings(0x8888ffff, 0x8888ffff, 0x66ff99ff, false, false, false, 100, 3);
            RareEnemy = new UnitSettings(0xf4ff19ff, 0xf4ff19ff, 0x66ff99ff, false, false, false, 120, 20);
            UniqueEnemy = new UnitSettings(0xffa500ff, 0xffa500ff, 0x66ff99ff, true, true, false, 120, 20);
            ShowDebuffPanel = new ToggleNode(false);
            DebuffPanelIconSize = new RangeNode<int>(20, 15, 40);
            GlobalZ = new RangeNode<int>(-100, -300, 300);
            PlayerZ = new RangeNode<int>(-100, -300, 300);
            OffsetBars = new RangeNode<int>(0, -300, 300);
            HideOverUi = new ToggleNode(true);
            ImGuiRender = new ToggleNode(false);
            LimitDrawDistance = new RangeNode<int>(133, 0, 1000);
            Rounding = new RangeNode<float>(0, 0, 64);
            MultiThreading = new ToggleNode(false);
            MultiThreadingCountEntities = new RangeNode<int>(10, 1, 200);
            ShowMinionOnlyBelowHp = new RangeNode<int>(50, 1, 100);
            SelfHealthBarShow = new ToggleNode(true);
        }

        [Menu("Show in town")] public ToggleNode ShowInTown { get; set; }
        [Menu("Show Energy Shield")] public ToggleNode ShowES { get; set; }
        [Menu("Show enemies", 0, 3)] public ToggleNode ShowEnemies { get; set; }
        [Menu("Players", 1)] public UnitSettings Players { get; set; }
        [Menu("Minions", 2)] public UnitSettings Minions { get; set; }
        [Menu("Normal enemy", 3)] public UnitSettings NormalEnemy { get; set; }
        [Menu("Magic enemy", 4)] public UnitSettings MagicEnemy { get; set; }
        [Menu("Rare enemy", 5)] public UnitSettings RareEnemy { get; set; }
        [Menu("Unique enemy", 6)] public UnitSettings UniqueEnemy { get; set; }
        [Menu("Show debuff panel")] public ToggleNode ShowDebuffPanel { get; set; }
        [Menu("Size debuff icon")] public RangeNode<int> DebuffPanelIconSize { get; set; }
        [Menu("Z")] public RangeNode<int> GlobalZ { get; set; }
        [Menu("Player Z")] public RangeNode<int> PlayerZ { get; set; }
        [Menu("PlayerBar Y offset")] public RangeNode<int> OffsetBars { get; set; }
        [Menu("Hide over UI")] public ToggleNode HideOverUi { get; set; }
        [Menu("Using ImGui for render")] public ToggleNode ImGuiRender { get; set; }
        public RangeNode<int> LimitDrawDistance { get; set; }
        [Menu("Rounding")] public RangeNode<float> Rounding { get; set; }
        public ToggleNode MultiThreading { get; set; }
        public RangeNode<int> MultiThreadingCountEntities { get; set; }
        public RangeNode<int> ShowMinionOnlyBelowHp { get; set; }
        public ToggleNode SelfHealthBarShow { get; set; }
        public ToggleNode Enable { get; set; }
        
    }

    public class UnitSettings : ISettings
    {
        public UnitSettings(uint color, uint outline)
        {
            Enable = new ToggleNode(true);
            Width = new RangeNode<float>(100, 20, 250);
            Height = new RangeNode<float>(20, 5, 150);
            Color = color;
            Outline = outline;
            Under10Percent = 0xffffffff;
            PercentTextColor = 0xffffffff;
            HealthTextColor = 0xffffffff;
            HealthTextColorUnder10Percent = 0xffff00ff;

            ShowHealthPercentages = new ToggleNode(false);
            ShowEnergyShieldPercentages = new ToggleNode(false);
            ShowEffectiveHealthPointsPercentages = new ToggleNode(false);

            ShowHealthCurrentValue = new ToggleNode(false);
            ShowEnergyShieldCurrentValue = new ToggleNode(false);
            ShowEffectiveHealthPointsCurrentValue = new ToggleNode(false);

            ShowHealthMaximumValue = new ToggleNode(false);
            ShowEnergyShieldMaximumValue = new ToggleNode(false);
            ShowEffectiveHealthPointsMaximumValue = new ToggleNode(false);

            ShowFloatingCombatDamage = new ToggleNode(false);
            FloatingCombatTextSize = new RangeNode<int>(15, 10, 30);
            FloatingCombatDamageColor = SharpDX.Color.Yellow;
            FloatingCombatHealColor = SharpDX.Color.Lime;
            Background = SharpDX.Color.Black;
            TextSize = new RangeNode<int>(15, 10, 25);
            FloatingCombatStackSize = new RangeNode<int>(1, 1, 10);
        }

        public UnitSettings(uint color,
                            uint outline,
                            uint percentTextColor,
                            bool showText,
                            bool showPercents,
                            bool showMaxText,
                            int width,
                            int height) : this(color, outline)
        {
            PercentTextColor = percentTextColor;
            ShowHealthPercentages.Value = showPercents;
            ShowEnergyShieldPercentages.Value = showPercents;
            ShowEffectiveHealthPointsPercentages.Value = showPercents;
            ShowHealthCurrentValue.Value = showText;
            ShowEnergyShieldCurrentValue.Value = showText;
            ShowEffectiveHealthPointsCurrentValue.Value = showText;
            ShowHealthMaximumValue.Value = showMaxText;
            ShowEnergyShieldMaximumValue.Value = showMaxText;
            ShowEffectiveHealthPointsMaximumValue.Value = showMaxText;
            Width = new RangeNode<float>(width, 20, 250);
            Height = new RangeNode<float>(height, 3, 150);
        }

        public RangeNode<float> Width { get; set; }
        public RangeNode<float> Height { get; set; }
        public ColorNode Color { get; set; }
        public ColorNode Outline { get; set; }
        public ColorNode Background { get; set; }
        [Menu("Under 10 Percent")]
        public ColorNode Under10Percent { get; set; }
        public ColorNode PercentTextColor { get; set; }
        public ColorNode HealthTextColor { get; set; }
        [Menu("Health Text Color Under 10 Percent")]
        public ColorNode HealthTextColorUnder10Percent { get; set; }
        #region Percentages
        [Menu("Show Effective Health Points percentages",
            "Effective Health Points means combined Healths and Energy Shields values." +
            "\n" +
            "\nThis setting have priority above \"Show Energy Shield percentages\":" +
            "\n\tif both settings checked you'll see EHP percentages.")]
        public ToggleNode ShowEffectiveHealthPointsPercentages { get; set; }
        [Menu("Show Energy Shield percentages",
            "This setting have priority above \"Show Health percentages\":" +
            "\n\tif both settings checked you'll see ES percentages while enemy's ES still active.")]
        public ToggleNode ShowEnergyShieldPercentages { get; set; }
        [Menu("Show Health percentages")]
        public ToggleNode ShowHealthPercentages { get; set; }
        #endregion
        #region Current Values
        [Menu("Show Effective Health Points current value",
            "Effective Health Points means combined Healths and Energy Shields values." +
            "\n" +
            "\nThis setting have priority above \"Show Energy Shield current value\":" +
            "\n\tif both settings checked you'll see EHP value.")]
        public ToggleNode ShowEffectiveHealthPointsCurrentValue { get; set; }
        [Menu("Show Energy Shield current value",
            "This setting have priority above \"Show Health current value\":" +
            "\n\tif both settings checked you'll see ES value while enemy's ES still active.")]
        public ToggleNode ShowEnergyShieldCurrentValue { get; set; }
        [Menu("Show Health current value")]
        public ToggleNode ShowHealthCurrentValue { get; set; }
        #endregion
        #region Maximum Values
        [Menu("Show Effective Health Points maximum value",
            "Effective Health Points means combined Healths and Energy Shields values." +
            "\n" +
            "\nThis setting have priority above \"Show Energy Shield maximum value\":" +
            "\n\tif both settings checked you'll see EHP maximum value.")]
        public ToggleNode ShowEffectiveHealthPointsMaximumValue { get; set; }
        [Menu("Show Energy Shield maximum value",
            "This setting have priority above \"Show Health maximum value\":" +
            "\n\tif both settings checked you'll see ES maximum value while enemy's ES still active.")]
        public ToggleNode ShowEnergyShieldMaximumValue { get; set; }
        [Menu("Show Health maximum value")]
        public ToggleNode ShowHealthMaximumValue { get; set; }
        #endregion
        public RangeNode<int> TextSize { get; set; }
        [Menu("Floating combat text")]
        public ToggleNode ShowFloatingCombatDamage { get; set; }
        [Menu("Damage color")]
        public ColorNode FloatingCombatDamageColor { get; set; }
        [Menu("Heal color")]
        public ColorNode FloatingCombatHealColor { get; set; }
        [Menu("Text size")]
        public RangeNode<int> FloatingCombatTextSize { get; set; }
        [Menu("Number of lines")]
        public RangeNode<int> FloatingCombatStackSize { get; set; }
        [Menu("Enable")]
        public ToggleNode Enable { get; set; }
    }
}
