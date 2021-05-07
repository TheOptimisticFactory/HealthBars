using ImGuiNET;
using System;

namespace HealthBars
{
    internal class ImGuiDrawSettings
    {
        internal static void DrawImGuiSettings()
        {
            System.Numerics.Vector4 green = new System.Numerics.Vector4(0.102f, 0.388f, 0.106f, 1.000f);
            System.Numerics.Vector4 red = new System.Numerics.Vector4(0.388f, 0.102f, 0.102f, 1.000f);

            ImGuiTreeNodeFlags collapsingHeaderFlags = ImGuiTreeNodeFlags.CollapsingHeader;

            HealthBars.Plugin.Settings.MultiThreading.Value = ImGuiExtension.Checkbox("Multithreading", HealthBars.Plugin.Settings.MultiThreading);
            ImGui.Text("Min entities to activate multithreading:");
            HealthBars.Plugin.Settings.MultiThreadingCountEntities.Value = ImGuiExtension.IntSlider("", HealthBars.Plugin.Settings.MultiThreadingCountEntities);
            ImGui.Separator();
            HealthBars.Plugin.Settings.ImGuiRender.Value = ImGuiExtension.Checkbox("Using ImGui for render", HealthBars.Plugin.Settings.ImGuiRender);
            HealthBars.Plugin.Settings.ShowInTown.Value = ImGuiExtension.Checkbox("Show in town", HealthBars.Plugin.Settings.ShowInTown);
            HealthBars.Plugin.Settings.SelfHealthBarShow.Value = ImGuiExtension.Checkbox("Show self health bar", HealthBars.Plugin.Settings.SelfHealthBarShow);
            HealthBars.Plugin.Settings.ShowEnergyShield.Value = ImGuiExtension.Checkbox("Show energy shields", HealthBars.Plugin.Settings.ShowEnergyShield);
            HealthBars.Plugin.Settings.ShowEnemies.Value = ImGuiExtension.Checkbox("Show enemies", HealthBars.Plugin.Settings.ShowEnemies);
            ImGui.Separator();
            HealthBars.Plugin.Settings.ShowDebuffPanel.Value = ImGuiExtension.Checkbox("Show debuff panel", HealthBars.Plugin.Settings.ShowDebuffPanel);
            HealthBars.Plugin.Settings.DebuffPanelIconSize.Value = ImGuiExtension.IntSlider("Size debuff icon", HealthBars.Plugin.Settings.DebuffPanelIconSize);
            ImGui.Separator();
            HealthBars.Plugin.Settings.GlobalZ.Value = ImGuiExtension.IntSlider("Z", HealthBars.Plugin.Settings.GlobalZ);
            HealthBars.Plugin.Settings.PlayerZ.Value = ImGuiExtension.IntSlider("Player Z", HealthBars.Plugin.Settings.PlayerZ);
            // HealthBars.Plugin.Settings.OffsetBars.Value = ImGuiExtension.IntSlider("PlayerBar Y offset", HealthBars.Plugin.Settings.OffsetBars);
            // HealthBars.Plugin.Settings.HideOverUi.Value = ImGuiExtension.Checkbox("Hide over UI", HealthBars.Plugin.Settings.HideOverUi);
            HealthBars.Plugin.Settings.LimitDrawDistance.Value = ImGuiExtension.IntSlider("Limit Draw Distance", HealthBars.Plugin.Settings.LimitDrawDistance);
            // HealthBars.Plugin.Settings.Rounding.Value = ImGuiExtension.IntSlider("Rounding", HealthBars.Plugin.Settings.Rounding);
            ImGui.Separator();

            // Players Bars
            try
            {
                if (HealthBars.Plugin.Settings.Players.Enable)
                    ImGui.PushStyleColor(ImGuiCol.Header, green);
                else
                    ImGui.PushStyleColor(ImGuiCol.Header, red);

                if (ImGui.TreeNodeEx("Players", collapsingHeaderFlags))
                {
                    ImGui.Indent();
                    HealthBars.Plugin.Settings.Players.Enable.Value = ImGuiExtension.Checkbox("Enable", HealthBars.Plugin.Settings.Players.Enable);
                    HealthBars.Plugin.Settings.Players.Width.Value = ImGuiExtension.FloatSlider("Width", HealthBars.Plugin.Settings.Players.Width);
                    HealthBars.Plugin.Settings.Players.Height.Value = ImGuiExtension.FloatSlider("Height", HealthBars.Plugin.Settings.Players.Height);
                    HealthBars.Plugin.Settings.Players.Color.Value = ImGuiExtension.ColorPicker("Color", HealthBars.Plugin.Settings.Players.Color);
                    HealthBars.Plugin.Settings.Players.Outline.Value = ImGuiExtension.ColorPicker("Outline", HealthBars.Plugin.Settings.Players.Outline);
                    HealthBars.Plugin.Settings.Players.Background.Value =
                        ImGuiExtension.ColorPicker("Background", HealthBars.Plugin.Settings.Players.Background);
                    HealthBars.Plugin.Settings.Players.Under10Percent.Value =
                        ImGuiExtension.ColorPicker("Under 10 Percent", HealthBars.Plugin.Settings.Players.Under10Percent);
                    HealthBars.Plugin.Settings.Players.PercentTextColor.Value =
                        ImGuiExtension.ColorPicker("Percent Text Color", HealthBars.Plugin.Settings.Players.PercentTextColor);
                    HealthBars.Plugin.Settings.Players.HealthTextColor.Value =
                        ImGuiExtension.ColorPicker("Health Text Color", HealthBars.Plugin.Settings.Players.HealthTextColor);
                    HealthBars.Plugin.Settings.Players.HealthTextColorUnder10Percent.Value =
                        ImGuiExtension.ColorPicker("Health Text Color Under 10 Percent", HealthBars.Plugin.Settings.Players.HealthTextColorUnder10Percent);
                    ImGui.Separator();
                    #region Percentages
                    HealthBars.Plugin.Settings.Players.ShowEffectiveHealthPointsPercentages.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points percentages", HealthBars.Plugin.Settings.Players.ShowEffectiveHealthPointsPercentages);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield percentages\":" +
                                           "\n\tif both settings checked you'll see EHP percentages.");
                    HealthBars.Plugin.Settings.Players.ShowEnergyShieldPercentages.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield percentages", HealthBars.Plugin.Settings.Players.ShowEnergyShieldPercentages);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health percentages\":" +
                                           "\n\tif both settings checked you'll see ES percentages while enemy's ES still active.");
                    HealthBars.Plugin.Settings.Players.ShowHealthPercentages.Value =
                        ImGuiExtension.Checkbox("Show Health percentages", HealthBars.Plugin.Settings.Players.ShowHealthPercentages);
                    ImGui.Separator();
                    #endregion
                    #region Current Values
                    HealthBars.Plugin.Settings.Players.ShowEffectiveHealthPointsCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points current value", HealthBars.Plugin.Settings.Players.ShowEffectiveHealthPointsCurrentValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield current value\":" +
                                           "\n\tif both settings checked you'll see EHP value.");
                    HealthBars.Plugin.Settings.Players.ShowEnergyShieldCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield current value", HealthBars.Plugin.Settings.Players.ShowEnergyShieldCurrentValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health current value\":" +
                                           "\n\tif both settings checked you'll see ES value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.Players.ShowHealthCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Health current value", HealthBars.Plugin.Settings.Players.ShowHealthCurrentValue);
                    ImGui.Separator();
                    #endregion
                    #region Maximum Values
                    HealthBars.Plugin.Settings.Players.ShowEffectiveHealthPointsMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points maximum value", HealthBars.Plugin.Settings.Players.ShowEffectiveHealthPointsMaximumValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield maximum value\":" +
                                           "\n\tif both settings checked you'll see EHP maximum value.");
                    HealthBars.Plugin.Settings.Players.ShowEnergyShieldMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield maximum value", HealthBars.Plugin.Settings.Players.ShowEnergyShieldMaximumValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health maximum value\":" +
                                           "\n\tif both settings checked you'll see ES maximum value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.Players.ShowHealthMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Health maximum value", HealthBars.Plugin.Settings.Players.ShowHealthMaximumValue);
                    ImGui.Separator();
                    #endregion
                    // HealthBars.Plugin.Settings.Players.TextSize.Value = ImGuiExtension.IntSlider("Text Size", HealthBars.Plugin.Settings.Players.TextSize);
                    HealthBars.Plugin.Settings.Players.ShowFloatingCombatDamage.Value =
                        ImGuiExtension.Checkbox("Floating combat text", HealthBars.Plugin.Settings.Players.ShowFloatingCombatDamage);
                    HealthBars.Plugin.Settings.Players.FloatingCombatDamageColor.Value =
                        ImGuiExtension.ColorPicker("Floating damage color", HealthBars.Plugin.Settings.Players.FloatingCombatDamageColor);
                    HealthBars.Plugin.Settings.Players.FloatingCombatHealColor.Value =
                        ImGuiExtension.ColorPicker("Floating heal color", HealthBars.Plugin.Settings.Players.FloatingCombatHealColor);
                    HealthBars.Plugin.Settings.Players.FloatingCombatTextSize.Value =
                        ImGuiExtension.IntSlider("Floating text size", HealthBars.Plugin.Settings.Players.FloatingCombatTextSize);
                    HealthBars.Plugin.Settings.Players.FloatingCombatStackSize.Value =
                        ImGuiExtension.IntSlider("Number of lines", HealthBars.Plugin.Settings.Players.FloatingCombatStackSize);
                    ImGuiExtension.Spacing(3);
                    ImGui.Unindent();
                }
                ImGui.Separator();
            }
            catch (Exception e) { HealthBars.Plugin.LogError(e.ToString()); }

            // Minions Bars !! Attention: Settings.ShowMinionOnlyBelowHp displays here.
            try
            {
                if (HealthBars.Plugin.Settings.Minions.Enable)
                    ImGui.PushStyleColor(ImGuiCol.Header, green);
                else
                    ImGui.PushStyleColor(ImGuiCol.Header, red);

                if (ImGui.TreeNodeEx("Minions", collapsingHeaderFlags))
                {
                    ImGui.Indent();
                    HealthBars.Plugin.Settings.Minions.Enable.Value = ImGuiExtension.Checkbox("Enable", HealthBars.Plugin.Settings.Minions.Enable);
                    ImGui.Text("Show minion bar when the minion health below:");
                    HealthBars.Plugin.Settings.ShowMinionOnlyBelowHp.Value = ImGuiExtension.IntSlider("", HealthBars.Plugin.Settings.ShowMinionOnlyBelowHp);
                    ImGui.Separator();
                    HealthBars.Plugin.Settings.Minions.Width.Value = ImGuiExtension.FloatSlider("Width", HealthBars.Plugin.Settings.Minions.Width);
                    HealthBars.Plugin.Settings.Minions.Height.Value = ImGuiExtension.FloatSlider("Height", HealthBars.Plugin.Settings.Minions.Height);
                    HealthBars.Plugin.Settings.Minions.Color.Value = ImGuiExtension.ColorPicker("Color", HealthBars.Plugin.Settings.Minions.Color);
                    HealthBars.Plugin.Settings.Minions.Outline.Value = ImGuiExtension.ColorPicker("Outline", HealthBars.Plugin.Settings.Minions.Outline);
                    HealthBars.Plugin.Settings.Minions.Background.Value =
                        ImGuiExtension.ColorPicker("Background", HealthBars.Plugin.Settings.Minions.Background);
                    HealthBars.Plugin.Settings.Minions.Under10Percent.Value =
                        ImGuiExtension.ColorPicker("Under 10 Percent", HealthBars.Plugin.Settings.Minions.Under10Percent);
                    HealthBars.Plugin.Settings.Minions.PercentTextColor.Value =
                        ImGuiExtension.ColorPicker("Percent Text Color", HealthBars.Plugin.Settings.Minions.PercentTextColor);
                    HealthBars.Plugin.Settings.Minions.HealthTextColor.Value =
                        ImGuiExtension.ColorPicker("Health Text Color", HealthBars.Plugin.Settings.Minions.HealthTextColor);
                    HealthBars.Plugin.Settings.Minions.HealthTextColorUnder10Percent.Value =
                        ImGuiExtension.ColorPicker("Health Text Color Under 10 Percent", HealthBars.Plugin.Settings.Minions.HealthTextColorUnder10Percent);
                    ImGui.Separator();
                    #region Percentages
                    HealthBars.Plugin.Settings.Minions.ShowEffectiveHealthPointsPercentages.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points percentages", HealthBars.Plugin.Settings.Minions.ShowEffectiveHealthPointsPercentages);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield percentages\":" +
                                           "\n\tif both settings checked you'll see EHP percentages.");
                    HealthBars.Plugin.Settings.Minions.ShowEnergyShieldPercentages.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield percentages", HealthBars.Plugin.Settings.Minions.ShowEnergyShieldPercentages);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health percentages\":" +
                                           "\n\tif both settings checked you'll see ES percentages while enemy's ES still active.");
                    HealthBars.Plugin.Settings.Minions.ShowHealthPercentages.Value =
                        ImGuiExtension.Checkbox("Show Health percentages", HealthBars.Plugin.Settings.Minions.ShowHealthPercentages);
                    ImGui.Separator();
                    #endregion
                    #region Current Values
                    HealthBars.Plugin.Settings.Minions.ShowEffectiveHealthPointsCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points current value", HealthBars.Plugin.Settings.Minions.ShowEffectiveHealthPointsCurrentValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield current value\":" +
                                           "\n\tif both settings checked you'll see EHP value.");
                    HealthBars.Plugin.Settings.Minions.ShowEnergyShieldCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield current value", HealthBars.Plugin.Settings.Minions.ShowEnergyShieldCurrentValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health current value\":" +
                                           "\n\tif both settings checked you'll see ES value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.Minions.ShowHealthCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Health current value", HealthBars.Plugin.Settings.Minions.ShowHealthCurrentValue);
                    ImGui.Separator();
                    #endregion
                    #region Maximum Values
                    HealthBars.Plugin.Settings.Minions.ShowEffectiveHealthPointsMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points maximum value", HealthBars.Plugin.Settings.Minions.ShowEffectiveHealthPointsMaximumValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield maximum value\":" +
                                           "\n\tif both settings checked you'll see EHP maximum value.");
                    HealthBars.Plugin.Settings.Minions.ShowEnergyShieldMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield maximum value", HealthBars.Plugin.Settings.Minions.ShowEnergyShieldMaximumValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health maximum value\":" +
                                           "\n\tif both settings checked you'll see ES maximum value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.Minions.ShowHealthMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Health maximum value", HealthBars.Plugin.Settings.Minions.ShowHealthMaximumValue);
                    ImGui.Separator();
                    #endregion
                    // HealthBars.Plugin.Settings.Minions.TextSize.Value = ImGuiExtension.IntSlider("Text Size", HealthBars.Plugin.Settings.Minions.TextSize);
                    HealthBars.Plugin.Settings.Minions.ShowFloatingCombatDamage.Value =
                        ImGuiExtension.Checkbox("Floating combat text", HealthBars.Plugin.Settings.Minions.ShowFloatingCombatDamage);
                    HealthBars.Plugin.Settings.Minions.FloatingCombatDamageColor.Value =
                        ImGuiExtension.ColorPicker("Floating damage color", HealthBars.Plugin.Settings.Minions.FloatingCombatDamageColor);
                    HealthBars.Plugin.Settings.Minions.FloatingCombatHealColor.Value =
                        ImGuiExtension.ColorPicker("Floating heal color", HealthBars.Plugin.Settings.Minions.FloatingCombatHealColor);
                    HealthBars.Plugin.Settings.Minions.FloatingCombatTextSize.Value =
                        ImGuiExtension.IntSlider("Floating text size", HealthBars.Plugin.Settings.Minions.FloatingCombatTextSize);
                    HealthBars.Plugin.Settings.Minions.FloatingCombatStackSize.Value =
                        ImGuiExtension.IntSlider("Number of lines", HealthBars.Plugin.Settings.Minions.FloatingCombatStackSize);
                    ImGuiExtension.Spacing(3);
                    ImGui.Unindent();
                }
                ImGui.Separator();
            }
            catch (Exception e) { HealthBars.Plugin.LogError(e.ToString()); }

            // NormalEnemy Bars
            try
            {
                if (HealthBars.Plugin.Settings.NormalEnemy.Enable)
                    ImGui.PushStyleColor(ImGuiCol.Header, green);
                else
                    ImGui.PushStyleColor(ImGuiCol.Header, red);

                if (ImGui.TreeNodeEx("Normal enemies", collapsingHeaderFlags))
                {
                    ImGui.Indent();
                    HealthBars.Plugin.Settings.NormalEnemy.Enable.Value = ImGuiExtension.Checkbox("Enable", HealthBars.Plugin.Settings.NormalEnemy.Enable);
                    HealthBars.Plugin.Settings.NormalEnemy.Width.Value = ImGuiExtension.FloatSlider("Width", HealthBars.Plugin.Settings.NormalEnemy.Width);
                    HealthBars.Plugin.Settings.NormalEnemy.Height.Value = ImGuiExtension.FloatSlider("Height", HealthBars.Plugin.Settings.NormalEnemy.Height);
                    HealthBars.Plugin.Settings.NormalEnemy.Color.Value = ImGuiExtension.ColorPicker("Color", HealthBars.Plugin.Settings.NormalEnemy.Color);
                    HealthBars.Plugin.Settings.NormalEnemy.Outline.Value = ImGuiExtension.ColorPicker("Outline", HealthBars.Plugin.Settings.NormalEnemy.Outline);
                    HealthBars.Plugin.Settings.NormalEnemy.Background.Value =
                        ImGuiExtension.ColorPicker("Background", HealthBars.Plugin.Settings.NormalEnemy.Background);
                    HealthBars.Plugin.Settings.NormalEnemy.Under10Percent.Value =
                        ImGuiExtension.ColorPicker("Under 10 Percent", HealthBars.Plugin.Settings.NormalEnemy.Under10Percent);
                    HealthBars.Plugin.Settings.NormalEnemy.PercentTextColor.Value =
                        ImGuiExtension.ColorPicker("Percent Text Color", HealthBars.Plugin.Settings.NormalEnemy.PercentTextColor);
                    HealthBars.Plugin.Settings.NormalEnemy.HealthTextColor.Value =
                        ImGuiExtension.ColorPicker("Health Text Color", HealthBars.Plugin.Settings.NormalEnemy.HealthTextColor);
                    HealthBars.Plugin.Settings.NormalEnemy.HealthTextColorUnder10Percent.Value =
                        ImGuiExtension.ColorPicker("Health Text Color Under 10 Percent", HealthBars.Plugin.Settings.NormalEnemy.HealthTextColorUnder10Percent);
                    ImGui.Separator();
                    #region Percentages
                    HealthBars.Plugin.Settings.NormalEnemy.ShowEffectiveHealthPointsPercentages.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points percentages", HealthBars.Plugin.Settings.NormalEnemy.ShowEffectiveHealthPointsPercentages);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield percentages\":" +
                                           "\n\tif both settings checked you'll see EHP percentages.");
                    HealthBars.Plugin.Settings.NormalEnemy.ShowEnergyShieldPercentages.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield percentages", HealthBars.Plugin.Settings.NormalEnemy.ShowEnergyShieldPercentages);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health percentages\":" +
                                           "\n\tif both settings checked you'll see ES percentages while enemy's ES still active.");
                    HealthBars.Plugin.Settings.NormalEnemy.ShowHealthPercentages.Value =
                        ImGuiExtension.Checkbox("Show Health percentages", HealthBars.Plugin.Settings.NormalEnemy.ShowHealthPercentages);
                    ImGui.Separator();
                    #endregion
                    #region Current Values
                    HealthBars.Plugin.Settings.NormalEnemy.ShowEffectiveHealthPointsCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points current value", HealthBars.Plugin.Settings.NormalEnemy.ShowEffectiveHealthPointsCurrentValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield current value\":" +
                                           "\n\tif both settings checked you'll see EHP value.");
                    HealthBars.Plugin.Settings.NormalEnemy.ShowEnergyShieldCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield current value", HealthBars.Plugin.Settings.NormalEnemy.ShowEnergyShieldCurrentValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health current value\":" +
                                           "\n\tif both settings checked you'll see ES value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.NormalEnemy.ShowHealthCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Health current value", HealthBars.Plugin.Settings.NormalEnemy.ShowHealthCurrentValue);
                    ImGui.Separator();
                    #endregion
                    #region Maximum Values
                    HealthBars.Plugin.Settings.NormalEnemy.ShowEffectiveHealthPointsMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points maximum value", HealthBars.Plugin.Settings.NormalEnemy.ShowEffectiveHealthPointsMaximumValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield maximum value\":" +
                                           "\n\tif both settings checked you'll see EHP maximum value.");
                    HealthBars.Plugin.Settings.NormalEnemy.ShowEnergyShieldMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield maximum value", HealthBars.Plugin.Settings.NormalEnemy.ShowEnergyShieldMaximumValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health maximum value\":" +
                                           "\n\tif both settings checked you'll see ES maximum value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.NormalEnemy.ShowHealthMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Health maximum value", HealthBars.Plugin.Settings.NormalEnemy.ShowHealthMaximumValue);
                    ImGui.Separator();
                    #endregion
                    // HealthBars.Plugin.Settings.NormalEnemy.TextSize.Value = ImGuiExtension.IntSlider("Text Size", HealthBars.Plugin.Settings.NormalEnemy.TextSize);
                    HealthBars.Plugin.Settings.NormalEnemy.ShowFloatingCombatDamage.Value =
                        ImGuiExtension.Checkbox("Floating combat text", HealthBars.Plugin.Settings.NormalEnemy.ShowFloatingCombatDamage);
                    HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatDamageColor.Value =
                        ImGuiExtension.ColorPicker("Floating damage color", HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatDamageColor);
                    HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatHealColor.Value =
                        ImGuiExtension.ColorPicker("Floating heal color", HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatHealColor);
                    HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatTextSize.Value =
                        ImGuiExtension.IntSlider("Floating text size", HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatTextSize);
                    HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatStackSize.Value =
                        ImGuiExtension.IntSlider("Number of lines", HealthBars.Plugin.Settings.NormalEnemy.FloatingCombatStackSize);
                    ImGuiExtension.Spacing(3);
                    ImGui.Unindent();
                }
                ImGui.Separator();
            }
            catch (Exception e) { HealthBars.Plugin.LogError(e.ToString()); }

            // MagicEnemy Bars
            try
            {
                if (HealthBars.Plugin.Settings.MagicEnemy.Enable)
                    ImGui.PushStyleColor(ImGuiCol.Header, green);
                else
                    ImGui.PushStyleColor(ImGuiCol.Header, red);

                if (ImGui.TreeNodeEx("Magic enemies", collapsingHeaderFlags))
                {
                    ImGui.Indent();
                    HealthBars.Plugin.Settings.MagicEnemy.Enable.Value = ImGuiExtension.Checkbox("Enable", HealthBars.Plugin.Settings.MagicEnemy.Enable);
                    HealthBars.Plugin.Settings.MagicEnemy.Width.Value = ImGuiExtension.FloatSlider("Width", HealthBars.Plugin.Settings.MagicEnemy.Width);
                    HealthBars.Plugin.Settings.MagicEnemy.Height.Value = ImGuiExtension.FloatSlider("Height", HealthBars.Plugin.Settings.MagicEnemy.Height);
                    HealthBars.Plugin.Settings.MagicEnemy.Color.Value = ImGuiExtension.ColorPicker("Color", HealthBars.Plugin.Settings.MagicEnemy.Color);
                    HealthBars.Plugin.Settings.MagicEnemy.Outline.Value = ImGuiExtension.ColorPicker("Outline", HealthBars.Plugin.Settings.MagicEnemy.Outline);
                    HealthBars.Plugin.Settings.MagicEnemy.Background.Value =
                        ImGuiExtension.ColorPicker("Background", HealthBars.Plugin.Settings.MagicEnemy.Background);
                    HealthBars.Plugin.Settings.MagicEnemy.Under10Percent.Value =
                        ImGuiExtension.ColorPicker("Under 10 Percent", HealthBars.Plugin.Settings.MagicEnemy.Under10Percent);
                    HealthBars.Plugin.Settings.MagicEnemy.PercentTextColor.Value =
                        ImGuiExtension.ColorPicker("Percent Text Color", HealthBars.Plugin.Settings.MagicEnemy.PercentTextColor);
                    HealthBars.Plugin.Settings.MagicEnemy.HealthTextColor.Value =
                        ImGuiExtension.ColorPicker("Health Text Color", HealthBars.Plugin.Settings.MagicEnemy.HealthTextColor);
                    HealthBars.Plugin.Settings.MagicEnemy.HealthTextColorUnder10Percent.Value =
                        ImGuiExtension.ColorPicker("Health Text Color Under 10 Percent", HealthBars.Plugin.Settings.MagicEnemy.HealthTextColorUnder10Percent);
                    ImGui.Separator();
                    #region Percentages
                    HealthBars.Plugin.Settings.MagicEnemy.ShowEffectiveHealthPointsPercentages.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points percentages", HealthBars.Plugin.Settings.MagicEnemy.ShowEffectiveHealthPointsPercentages);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield percentages\":" +
                                           "\n\tif both settings checked you'll see EHP percentages.");
                    HealthBars.Plugin.Settings.MagicEnemy.ShowEnergyShieldPercentages.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield percentages", HealthBars.Plugin.Settings.MagicEnemy.ShowEnergyShieldPercentages);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health percentages\":" +
                                           "\n\tif both settings checked you'll see ES percentages while enemy's ES still active.");
                    HealthBars.Plugin.Settings.MagicEnemy.ShowHealthPercentages.Value =
                        ImGuiExtension.Checkbox("Show Health percentages", HealthBars.Plugin.Settings.MagicEnemy.ShowHealthPercentages);
                    ImGui.Separator();
                    #endregion
                    #region Current Values
                    HealthBars.Plugin.Settings.MagicEnemy.ShowEffectiveHealthPointsCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points current value", HealthBars.Plugin.Settings.MagicEnemy.ShowEffectiveHealthPointsCurrentValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield current value\":" +
                                           "\n\tif both settings checked you'll see EHP value.");
                    HealthBars.Plugin.Settings.MagicEnemy.ShowEnergyShieldCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield current value", HealthBars.Plugin.Settings.MagicEnemy.ShowEnergyShieldCurrentValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health current value\":" +
                                           "\n\tif both settings checked you'll see ES value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.MagicEnemy.ShowHealthCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Health current value", HealthBars.Plugin.Settings.MagicEnemy.ShowHealthCurrentValue);
                    ImGui.Separator();
                    #endregion
                    #region Maximum Values
                    HealthBars.Plugin.Settings.MagicEnemy.ShowEffectiveHealthPointsMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points maximum value", HealthBars.Plugin.Settings.MagicEnemy.ShowEffectiveHealthPointsMaximumValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield maximum value\":" +
                                           "\n\tif both settings checked you'll see EHP maximum value.");
                    HealthBars.Plugin.Settings.MagicEnemy.ShowEnergyShieldMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield maximum value", HealthBars.Plugin.Settings.MagicEnemy.ShowEnergyShieldMaximumValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health maximum value\":" +
                                           "\n\tif both settings checked you'll see ES maximum value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.MagicEnemy.ShowHealthMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Health maximum value", HealthBars.Plugin.Settings.MagicEnemy.ShowHealthMaximumValue);
                    ImGui.Separator();
                    #endregion
                    // HealthBars.Plugin.Settings.MagicEnemy.TextSize.Value = ImGuiExtension.IntSlider("Text Size", HealthBars.Plugin.Settings.MagicEnemy.TextSize);
                    HealthBars.Plugin.Settings.MagicEnemy.ShowFloatingCombatDamage.Value =
                        ImGuiExtension.Checkbox("Floating combat text", HealthBars.Plugin.Settings.MagicEnemy.ShowFloatingCombatDamage);
                    HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatDamageColor.Value =
                        ImGuiExtension.ColorPicker("Floating damage color", HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatDamageColor);
                    HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatHealColor.Value =
                        ImGuiExtension.ColorPicker("Floating heal color", HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatHealColor);
                    HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatTextSize.Value =
                        ImGuiExtension.IntSlider("Floating text size", HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatTextSize);
                    HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatStackSize.Value =
                        ImGuiExtension.IntSlider("Number of lines", HealthBars.Plugin.Settings.MagicEnemy.FloatingCombatStackSize);
                    ImGuiExtension.Spacing(3);
                    ImGui.Unindent();
                }
                ImGui.Separator();
            }
            catch (Exception e) { HealthBars.Plugin.LogError(e.ToString()); }

            // RareEnemy Bars
            try
            {
                if (HealthBars.Plugin.Settings.RareEnemy.Enable)
                    ImGui.PushStyleColor(ImGuiCol.Header, green);
                else
                    ImGui.PushStyleColor(ImGuiCol.Header, red);

                if (ImGui.TreeNodeEx("Rare enemies", collapsingHeaderFlags))
                {
                    ImGui.Indent();
                    HealthBars.Plugin.Settings.RareEnemy.Enable.Value = ImGuiExtension.Checkbox("Enable", HealthBars.Plugin.Settings.RareEnemy.Enable);
                    HealthBars.Plugin.Settings.RareEnemy.Width.Value = ImGuiExtension.FloatSlider("Width", HealthBars.Plugin.Settings.RareEnemy.Width);
                    HealthBars.Plugin.Settings.RareEnemy.Height.Value = ImGuiExtension.FloatSlider("Height", HealthBars.Plugin.Settings.RareEnemy.Height);
                    HealthBars.Plugin.Settings.RareEnemy.Color.Value = ImGuiExtension.ColorPicker("Color", HealthBars.Plugin.Settings.RareEnemy.Color);
                    HealthBars.Plugin.Settings.RareEnemy.Outline.Value = ImGuiExtension.ColorPicker("Outline", HealthBars.Plugin.Settings.RareEnemy.Outline);
                    HealthBars.Plugin.Settings.RareEnemy.Background.Value =
                        ImGuiExtension.ColorPicker("Background", HealthBars.Plugin.Settings.RareEnemy.Background);
                    HealthBars.Plugin.Settings.RareEnemy.Under10Percent.Value =
                        ImGuiExtension.ColorPicker("Under 10 Percent", HealthBars.Plugin.Settings.RareEnemy.Under10Percent);
                    HealthBars.Plugin.Settings.RareEnemy.PercentTextColor.Value =
                        ImGuiExtension.ColorPicker("Percent Text Color", HealthBars.Plugin.Settings.RareEnemy.PercentTextColor);
                    HealthBars.Plugin.Settings.RareEnemy.HealthTextColor.Value =
                        ImGuiExtension.ColorPicker("Health Text Color", HealthBars.Plugin.Settings.RareEnemy.HealthTextColor);
                    HealthBars.Plugin.Settings.RareEnemy.HealthTextColorUnder10Percent.Value =
                        ImGuiExtension.ColorPicker("Health Text Color Under 10 Percent", HealthBars.Plugin.Settings.RareEnemy.HealthTextColorUnder10Percent);
                    ImGui.Separator();
                    #region Percentages
                    HealthBars.Plugin.Settings.RareEnemy.ShowEffectiveHealthPointsPercentages.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points percentages", HealthBars.Plugin.Settings.RareEnemy.ShowEffectiveHealthPointsPercentages);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield percentages\":" +
                                           "\n\tif both settings checked you'll see EHP percentages.");
                    HealthBars.Plugin.Settings.RareEnemy.ShowEnergyShieldPercentages.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield percentages", HealthBars.Plugin.Settings.RareEnemy.ShowEnergyShieldPercentages);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health percentages\":" +
                                           "\n\tif both settings checked you'll see ES percentages while enemy's ES still active.");
                    HealthBars.Plugin.Settings.RareEnemy.ShowHealthPercentages.Value =
                        ImGuiExtension.Checkbox("Show Health percentages", HealthBars.Plugin.Settings.RareEnemy.ShowHealthPercentages);
                    ImGui.Separator();
                    #endregion
                    #region Current Values
                    HealthBars.Plugin.Settings.RareEnemy.ShowEffectiveHealthPointsCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points current value", HealthBars.Plugin.Settings.RareEnemy.ShowEffectiveHealthPointsCurrentValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield current value\":" +
                                           "\n\tif both settings checked you'll see EHP value.");
                    HealthBars.Plugin.Settings.RareEnemy.ShowEnergyShieldCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield current value", HealthBars.Plugin.Settings.RareEnemy.ShowEnergyShieldCurrentValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health current value\":" +
                                           "\n\tif both settings checked you'll see ES value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.RareEnemy.ShowHealthCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Health current value", HealthBars.Plugin.Settings.RareEnemy.ShowHealthCurrentValue);
                    ImGui.Separator();
                    #endregion
                    #region Maximum Values
                    HealthBars.Plugin.Settings.RareEnemy.ShowEffectiveHealthPointsMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points maximum value", HealthBars.Plugin.Settings.RareEnemy.ShowEffectiveHealthPointsMaximumValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield maximum value\":" +
                                           "\n\tif both settings checked you'll see EHP maximum value.");
                    HealthBars.Plugin.Settings.RareEnemy.ShowEnergyShieldMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield maximum value", HealthBars.Plugin.Settings.RareEnemy.ShowEnergyShieldMaximumValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health maximum value\":" +
                                           "\n\tif both settings checked you'll see ES maximum value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.RareEnemy.ShowHealthMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Health maximum value", HealthBars.Plugin.Settings.RareEnemy.ShowHealthMaximumValue);
                    ImGui.Separator();
                    #endregion
                    // HealthBars.Plugin.Settings.RareEnemy.TextSize.Value = ImGuiExtension.IntSlider("Text Size", HealthBars.Plugin.Settings.RareEnemy.TextSize);
                    HealthBars.Plugin.Settings.RareEnemy.ShowFloatingCombatDamage.Value =
                        ImGuiExtension.Checkbox("Floating combat text", HealthBars.Plugin.Settings.RareEnemy.ShowFloatingCombatDamage);
                    HealthBars.Plugin.Settings.RareEnemy.FloatingCombatDamageColor.Value =
                        ImGuiExtension.ColorPicker("Floating damage color", HealthBars.Plugin.Settings.RareEnemy.FloatingCombatDamageColor);
                    HealthBars.Plugin.Settings.RareEnemy.FloatingCombatHealColor.Value =
                        ImGuiExtension.ColorPicker("Floating heal color", HealthBars.Plugin.Settings.RareEnemy.FloatingCombatHealColor);
                    HealthBars.Plugin.Settings.RareEnemy.FloatingCombatTextSize.Value =
                        ImGuiExtension.IntSlider("Floating text size", HealthBars.Plugin.Settings.RareEnemy.FloatingCombatTextSize);
                    HealthBars.Plugin.Settings.RareEnemy.FloatingCombatStackSize.Value =
                        ImGuiExtension.IntSlider("Number of lines", HealthBars.Plugin.Settings.RareEnemy.FloatingCombatStackSize);
                    ImGuiExtension.Spacing(3);
                    ImGui.Unindent();
                }
                ImGui.Separator();
            }
            catch (Exception e) { HealthBars.Plugin.LogError(e.ToString()); }

            // UniqueEnemy Bars
            try
            {
                if (HealthBars.Plugin.Settings.UniqueEnemy.Enable)
                    ImGui.PushStyleColor(ImGuiCol.Header, green);
                else
                    ImGui.PushStyleColor(ImGuiCol.Header, red);

                if (ImGui.TreeNodeEx("Unique enemies", collapsingHeaderFlags))
                {
                    ImGui.Indent();
                    HealthBars.Plugin.Settings.UniqueEnemy.Enable.Value = ImGuiExtension.Checkbox("Enable", HealthBars.Plugin.Settings.UniqueEnemy.Enable);
                    HealthBars.Plugin.Settings.UniqueEnemy.Width.Value = ImGuiExtension.FloatSlider("Width", HealthBars.Plugin.Settings.UniqueEnemy.Width);
                    HealthBars.Plugin.Settings.UniqueEnemy.Height.Value = ImGuiExtension.FloatSlider("Height", HealthBars.Plugin.Settings.UniqueEnemy.Height);
                    HealthBars.Plugin.Settings.UniqueEnemy.Color.Value = ImGuiExtension.ColorPicker("Color", HealthBars.Plugin.Settings.UniqueEnemy.Color);
                    HealthBars.Plugin.Settings.UniqueEnemy.Outline.Value = ImGuiExtension.ColorPicker("Outline", HealthBars.Plugin.Settings.UniqueEnemy.Outline);
                    HealthBars.Plugin.Settings.UniqueEnemy.Background.Value =
                        ImGuiExtension.ColorPicker("Background", HealthBars.Plugin.Settings.UniqueEnemy.Background);
                    HealthBars.Plugin.Settings.UniqueEnemy.Under10Percent.Value =
                        ImGuiExtension.ColorPicker("Under 10 Percent", HealthBars.Plugin.Settings.UniqueEnemy.Under10Percent);
                    HealthBars.Plugin.Settings.UniqueEnemy.PercentTextColor.Value =
                        ImGuiExtension.ColorPicker("Percent Text Color", HealthBars.Plugin.Settings.UniqueEnemy.PercentTextColor);
                    HealthBars.Plugin.Settings.UniqueEnemy.HealthTextColor.Value =
                        ImGuiExtension.ColorPicker("Health Text Color", HealthBars.Plugin.Settings.UniqueEnemy.HealthTextColor);
                    HealthBars.Plugin.Settings.UniqueEnemy.HealthTextColorUnder10Percent.Value =
                        ImGuiExtension.ColorPicker("Health Text Color Under 10 Percent", HealthBars.Plugin.Settings.UniqueEnemy.HealthTextColorUnder10Percent);
                    ImGui.Separator();
                    #region Percentages
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowEffectiveHealthPointsPercentages.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points percentages", HealthBars.Plugin.Settings.UniqueEnemy.ShowEffectiveHealthPointsPercentages);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield percentages\":" +
                                           "\n\tif both settings checked you'll see EHP percentages.");
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowEnergyShieldPercentages.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield percentages", HealthBars.Plugin.Settings.UniqueEnemy.ShowEnergyShieldPercentages);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health percentages\":" +
                                           "\n\tif both settings checked you'll see ES percentages while enemy's ES still active.");
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowHealthPercentages.Value =
                        ImGuiExtension.Checkbox("Show Health percentages", HealthBars.Plugin.Settings.UniqueEnemy.ShowHealthPercentages);
                    ImGui.Separator();
                    #endregion
                    #region Current Values
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowEffectiveHealthPointsCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points current value", HealthBars.Plugin.Settings.UniqueEnemy.ShowEffectiveHealthPointsCurrentValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield current value\":" +
                                           "\n\tif both settings checked you'll see EHP value.");
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowEnergyShieldCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield current value", HealthBars.Plugin.Settings.UniqueEnemy.ShowEnergyShieldCurrentValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health current value\":" +
                                           "\n\tif both settings checked you'll see ES value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowHealthCurrentValue.Value =
                        ImGuiExtension.Checkbox("Show Health current value", HealthBars.Plugin.Settings.UniqueEnemy.ShowHealthCurrentValue);
                    ImGui.Separator();
                    #endregion
                    #region Maximum Values
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowEffectiveHealthPointsMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Effective Health Points maximum value", HealthBars.Plugin.Settings.UniqueEnemy.ShowEffectiveHealthPointsMaximumValue);
                    ImGuiExtension.ToolTip("Effective Health Points means combined Healths and Energy Shields values." +
                                           "\n\nThis setting have priority above \"Show Energy Shield maximum value\":" +
                                           "\n\tif both settings checked you'll see EHP maximum value.");
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowEnergyShieldMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Energy Shield maximum value", HealthBars.Plugin.Settings.UniqueEnemy.ShowEnergyShieldMaximumValue);
                    ImGuiExtension.ToolTip("This setting have priority above \"Show Health maximum value\":" +
                                           "\n\tif both settings checked you'll see ES maximum value while enemy's ES still active.");
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowHealthMaximumValue.Value =
                        ImGuiExtension.Checkbox("Show Health maximum value", HealthBars.Plugin.Settings.UniqueEnemy.ShowHealthMaximumValue);
                    ImGui.Separator();
                    #endregion
                    // HealthBars.Plugin.Settings.UniqueEnemy.TextSize.Value =ImGuiExtension.IntSlider("Text Size", HealthBars.Plugin.Settings.UniqueEnemy.TextSize);
                    HealthBars.Plugin.Settings.UniqueEnemy.ShowFloatingCombatDamage.Value =
                        ImGuiExtension.Checkbox("Floating combat text", HealthBars.Plugin.Settings.UniqueEnemy.ShowFloatingCombatDamage);
                    HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatDamageColor.Value =
                        ImGuiExtension.ColorPicker("Floating damage color", HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatDamageColor);
                    HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatHealColor.Value =
                        ImGuiExtension.ColorPicker("Floating heal color", HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatHealColor);
                    HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatTextSize.Value =
                        ImGuiExtension.IntSlider("Floating text size", HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatTextSize);
                    HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatStackSize.Value =
                        ImGuiExtension.IntSlider("Number of lines", HealthBars.Plugin.Settings.UniqueEnemy.FloatingCombatStackSize);
                    ImGuiExtension.Spacing(3);
                    ImGui.Unindent();
                }
                ImGui.Separator();
            }
            catch (Exception e) { HealthBars.Plugin.LogError(e.ToString()); }
        }
    }
}
