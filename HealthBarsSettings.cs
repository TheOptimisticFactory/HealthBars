using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace HealthBars
{
    public class HealthBarsSettings : ISettings
    {
        public ToggleNode ShowInTown { get; set; } = new ToggleNode(false);
        public ToggleNode ShowEnergyShield { get; set; } = new ToggleNode(true);
        public ToggleNode ShowEnemies { get; set; } = new ToggleNode(true);
        public UnitSettings Players { get; set; } = new UnitSettings(0x008000ff, 0, 0xffffffff, false, false, false, 120, 15);
        public UnitSettings Minions { get; set; } = new UnitSettings(0x90ee90ff, 0, 0xffffffff, false, false, false, 120, 3);
        public UnitSettings NormalEnemy { get; set; } = new UnitSettings(0xff0000ff, 0, 0x66ff99ff, false, false, false, 120, 3);
        public UnitSettings MagicEnemy { get; set; } = new UnitSettings(0x8888ffff, 0x8888ffff, 0x66ff99ff, false, false, false, 120, 3);
        public UnitSettings RareEnemy { get; set; } = new UnitSettings(0xf4ff19ff, 0xf4ff19ff, 0x66ff99ff, false, false, false, 120, 15);
        public UnitSettings UniqueEnemy { get; set; } = new UnitSettings(0xffa500ff, 0xffa500ff, 0x66ff99ff, true, false, true, 120, 22);
        public ToggleNode ShowDebuffPanel { get; set; } = new ToggleNode(false);
        public RangeNode<int> DebuffPanelIconSize { get; set; } = new RangeNode<int>(20, 15, 40);
        public RangeNode<int> GlobalZ { get; set; } = new RangeNode<int>(-100, -300, 300);
        public RangeNode<int> PlayerZ { get; set; } = new RangeNode<int>(-100, -300, 300);
        public ToggleNode ImGuiRender { get; set; } = new ToggleNode(false);
        public RangeNode<int> LimitDrawDistance { get; set; } = new RangeNode<int>(133, 0, 1000);
        public ToggleNode MultiThreading { get; set; } = new ToggleNode(false);
        public RangeNode<int> MultiThreadingCountEntities { get; set; } = new RangeNode<int>(10, 1, 200);
        public RangeNode<int> ShowMinionOnlyBelowHp { get; set; } = new RangeNode<int>(50, 1, 100);
        public ToggleNode SelfHealthBarShow { get; set; } = new ToggleNode(true);
        public ToggleNode Enable { get; set; } = new ToggleNode(false);
        // public RangeNode<int> OffsetBars { get; set; } = new RangeNode<int>(0, -300, 300);
        // public ToggleNode HideOverUi { get; set; } = new ToggleNode(true);
        // public RangeNode<float> Rounding { get; set; } = new RangeNode<float>(0, 0, 64);

    }

    public class UnitSettings : ISettings
    {

        public UnitSettings(uint color, uint outline, uint percentTextColor,
                            bool showEHPCurValue, bool showEHPMaxValue, bool showHPPercents,
                            int width, int height)
        {
            Width.Value = width;
            Height.Value = height;
            Color = color;
            Outline = outline;
            PercentTextColor = percentTextColor;

            ShowHealthPercentages.Value = showHPPercents;
            ShowEffectiveHealthPointsCurrentValue.Value = showEHPCurValue;
            ShowEffectiveHealthPointsMaximumValue.Value = showEHPMaxValue;
        }

        public RangeNode<float> Width { get; set; } = new RangeNode<float>(120, 20, 250);
        public RangeNode<float> Height { get; set; } = new RangeNode<float>(20, 3, 150);
        public ColorNode Color { get; set; }
        public ColorNode Outline { get; set; }
        public ColorNode Background { get; set; } = SharpDX.Color.Black;
        public ColorNode Under10Percent { get; set; } = SharpDX.Color.Red;
        public ColorNode PercentTextColor { get; set; } = 0xffffffff;
        public ColorNode HealthTextColor { get; set; } = 0xffffffff;
        public ColorNode HealthTextColorUnder10Percent { get; set; } = new ColorNode(0xffff00ff);
        #region Percentages
        public ToggleNode ShowEffectiveHealthPointsPercentages { get; set; } = new ToggleNode(false);
        public ToggleNode ShowEnergyShieldPercentages { get; set; } = new ToggleNode(false);
        public ToggleNode ShowHealthPercentages { get; set; } = new ToggleNode(false);
        #endregion
        #region Current Values
        public ToggleNode ShowEffectiveHealthPointsCurrentValue { get; set; } = new ToggleNode(false);
        public ToggleNode ShowEnergyShieldCurrentValue { get; set; } = new ToggleNode(false);
        public ToggleNode ShowHealthCurrentValue { get; set; } = new ToggleNode(false);
        #endregion
        #region Maximum Values
        public ToggleNode ShowEffectiveHealthPointsMaximumValue { get; set; } = new ToggleNode(false);
        public ToggleNode ShowEnergyShieldMaximumValue { get; set; } = new ToggleNode(false);
        public ToggleNode ShowHealthMaximumValue { get; set; } = new ToggleNode(false);
        #endregion
        // public RangeNode<int> TextSize { get; set; } = new RangeNode<int>(15, 10, 25);
        public ToggleNode ShowFloatingCombatDamage { get; set; } = new ToggleNode(false);
        public ColorNode FloatingCombatDamageColor { get; set; } = SharpDX.Color.Yellow;
        public ColorNode FloatingCombatHealColor { get; set; } = SharpDX.Color.Lime;
        public RangeNode<int> FloatingCombatTextSize { get; set; } = new RangeNode<int>(15, 10, 30);
        public RangeNode<int> FloatingCombatStackSize { get; set; } = new RangeNode<int>(1, 1, 10);
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
    }
}
