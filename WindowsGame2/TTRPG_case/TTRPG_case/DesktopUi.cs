using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squid;

namespace TTRPG_case
{
    public class DesktopUi : Desktop
    {
        public DesktopUi()
        {
            #region cursors
            Point cursorSize = new Point(32, 32);
            Point halfSize = cursorSize / 2;

            AddCursor(Cursors.Default, new Cursor { Texture = "cursors\\Arrow.png", Size = cursorSize, HotSpot = Point.Zero });
            AddCursor(Cursors.Link, new Cursor { Texture = "cursors\\Link.png", Size = cursorSize, HotSpot = Point.Zero });
            AddCursor(Cursors.Move, new Cursor { Texture = "cursors\\Move.png", Size = cursorSize, HotSpot = halfSize });
            AddCursor(Cursors.Select, new Cursor { Texture = "cursors\\Select.png", Size = cursorSize, HotSpot = halfSize });
            AddCursor(Cursors.SizeNS, new Cursor { Texture = "cursors\\SizeNS.png", Size = cursorSize, HotSpot = halfSize });
            AddCursor(Cursors.SizeWE, new Cursor { Texture = "cursors\\SizeWE.png", Size = cursorSize, HotSpot = halfSize });
            AddCursor(Cursors.HSplit, new Cursor { Texture = "cursors\\SizeNS.png", Size = cursorSize, HotSpot = halfSize });
            AddCursor(Cursors.VSplit, new Cursor { Texture = "cursors\\SizeWE.png", Size = cursorSize, HotSpot = halfSize });
            AddCursor(Cursors.SizeNESW, new Cursor { Texture = "cursors\\SizeNESW.png", Size = cursorSize, HotSpot = halfSize });
            AddCursor(Cursors.SizeNWSE, new Cursor { Texture = "cursors\\SizeNWSE.png", Size = cursorSize, HotSpot = halfSize });

            #endregion

            #region styles

            ControlStyle baseStyle = new ControlStyle();
            baseStyle.Tiling = TextureMode.Grid;
            baseStyle.Grid = new Margin(3);
            baseStyle.Texture = "button_hot.dds";
            baseStyle.Default.Texture = "button_default.dds";
            baseStyle.Pressed.Texture = "button_down.dds";
            baseStyle.SelectedPressed.Texture = "button_down.dds";
            baseStyle.Focused.Texture = "button_down.dds";
            baseStyle.SelectedFocused.Texture = "button_down.dds";
            baseStyle.Selected.Texture = "button_down.dds";
            baseStyle.SelectedHot.Texture = "button_down.dds";

            ControlStyle itemStyle = new ControlStyle(baseStyle);
            itemStyle.TextPadding = new Margin(10, 0, 0, 0);
            itemStyle.TextAlign = Alignment.MiddleLeft;

            ControlStyle buttonStyle = new ControlStyle(baseStyle);
            buttonStyle.TextPadding = new Margin(0);
            buttonStyle.TextAlign = Alignment.MiddleCenter;

            ControlStyle tooltipStyle = new ControlStyle(buttonStyle);
            tooltipStyle.TextPadding = new Margin(8);
            tooltipStyle.TextAlign = Alignment.TopLeft;

            ControlStyle inputStyle = new ControlStyle();
            inputStyle.Texture = "input_default.dds";
            inputStyle.Hot.Texture = "input_focused.dds";
            inputStyle.Focused.Texture = "input_focused.dds";
            inputStyle.TextPadding = new Margin(8);
            inputStyle.Tiling = TextureMode.Grid;
            inputStyle.Focused.Tint = ColorInt.RGBA(1, 0, 0, 1);
            inputStyle.Grid = new Margin(3);


            ControlStyle chatStyle = new ControlStyle();
            chatStyle.Texture = "input_default.dds";
            chatStyle.Hot.Texture = "input_focused.dds";
            chatStyle.Focused.Texture = "input_focused.dds";
            chatStyle.TextPadding = new Margin(5);
            chatStyle.Tiling = TextureMode.Grid;
            chatStyle.Focused.Tint = ColorInt.RGBA(1, 0, 0, 1);
            chatStyle.Grid = new Margin(3);
 
            ControlStyle windowStyle = new ControlStyle();
            windowStyle.Tiling = TextureMode.Grid;
            windowStyle.Grid = new Margin(9);
            windowStyle.Texture = "window.dds";

            ControlStyle frameStyle = new ControlStyle();
            frameStyle.Tiling = TextureMode.Grid;
            frameStyle.Grid = new Margin(4);
            frameStyle.Texture = "frame.dds";
            frameStyle.TextPadding = new Margin(8);

            ControlStyle vscrollTrackStyle = new ControlStyle();
            vscrollTrackStyle.Tiling = TextureMode.Grid;
            vscrollTrackStyle.Grid = new Margin(3);
            vscrollTrackStyle.Texture = "vscroll_track.dds";

            ControlStyle vscrollButtonStyle = new ControlStyle();
            vscrollButtonStyle.Tiling = TextureMode.Grid;
            vscrollButtonStyle.Grid = new Margin(3);
            vscrollButtonStyle.Texture = "vscroll_button.dds";
            vscrollButtonStyle.Hot.Texture = "vscroll_button_hot.dds";
            vscrollButtonStyle.Pressed.Texture = "vscroll_button_down.dds";

            ControlStyle vscrollUp = new ControlStyle();
            vscrollUp.Default.Texture = "vscrollUp_default.dds";
            vscrollUp.Hot.Texture = "vscrollUp_hot.dds";
            vscrollUp.Pressed.Texture = "vscrollUp_down.dds";
            vscrollUp.Focused.Texture = "vscrollUp_hot.dds";

            ControlStyle hscrollTrackStyle = new ControlStyle();
            hscrollTrackStyle.Tiling = TextureMode.Grid;
            hscrollTrackStyle.Grid = new Margin(3);
            hscrollTrackStyle.Texture = "hscroll_track.dds";

            ControlStyle hscrollButtonStyle = new ControlStyle();
            hscrollButtonStyle.Tiling = TextureMode.Grid;
            hscrollButtonStyle.Grid = new Margin(3);
            hscrollButtonStyle.Texture = "hscroll_button.dds";
            hscrollButtonStyle.Hot.Texture = "hscroll_button_hot.dds";
            hscrollButtonStyle.Pressed.Texture = "hscroll_button_down.dds";

            ControlStyle hscrollUp = new ControlStyle();
            hscrollUp.Default.Texture = "hscrollUp_default.dds";
            hscrollUp.Hot.Texture = "hscrollUp_hot.dds";
            hscrollUp.Pressed.Texture = "hscrollUp_down.dds";
            hscrollUp.Focused.Texture = "hscrollUp_hot.dds";

            ControlStyle checkButtonStyle = new ControlStyle();
            checkButtonStyle.Default.Texture = "checkbox_default.dds";
            checkButtonStyle.Hot.Texture = "checkbox_hot.dds";
            checkButtonStyle.Pressed.Texture = "checkbox_down.dds";
            checkButtonStyle.Checked.Texture = "checkbox_checked.dds";
            checkButtonStyle.CheckedFocused.Texture = "checkbox_checked_hot.dds";
            checkButtonStyle.CheckedHot.Texture = "checkbox_checked_hot.dds";
            checkButtonStyle.CheckedPressed.Texture = "checkbox_down.dds";

            ControlStyle comboLabelStyle = new ControlStyle();
            comboLabelStyle.TextPadding = new Margin(10, 0, 0, 0);
            comboLabelStyle.Default.Texture = "combo_default.dds";
            comboLabelStyle.Hot.Texture = "combo_hot.dds";
            comboLabelStyle.Pressed.Texture = "combo_down.dds";
            comboLabelStyle.Focused.Texture = "combo_hot.dds";
            comboLabelStyle.Tiling = TextureMode.Grid;
            comboLabelStyle.Grid = new Margin(3, 0, 0, 0);

            ControlStyle comboButtonStyle = new ControlStyle();
            comboButtonStyle.Default.Texture = "combo_button_default.dds";
            comboButtonStyle.Hot.Texture = "combo_button_hot.dds";
            comboButtonStyle.Pressed.Texture = "combo_button_down.dds";
            comboButtonStyle.Focused.Texture = "combo_button_hot.dds";

            ControlStyle labelStyle = new ControlStyle();
            labelStyle.TextAlign = Alignment.TopLeft;
            labelStyle.TextPadding = new Margin(8);

            Skin skin = new Skin();

            skin.Styles.Add("item", itemStyle);
            skin.Styles.Add("textbox", inputStyle);
            skin.Styles.Add("chatlog", chatStyle);
            skin.Styles.Add("button", buttonStyle);
            skin.Styles.Add("window", windowStyle);
            skin.Styles.Add("frame", frameStyle);
            skin.Styles.Add("checkBox", checkButtonStyle);
            skin.Styles.Add("comboLabel", comboLabelStyle);
            skin.Styles.Add("comboButton", comboButtonStyle);
            skin.Styles.Add("vscrollTrack", vscrollTrackStyle);
            skin.Styles.Add("vscrollButton", vscrollButtonStyle);
            skin.Styles.Add("vscrollUp", vscrollUp);
            skin.Styles.Add("hscrollTrack", hscrollTrackStyle);
            skin.Styles.Add("hscrollButton", hscrollButtonStyle);
            skin.Styles.Add("hscrollUp", hscrollUp);
            skin.Styles.Add("multiline", labelStyle);
            skin.Styles.Add("tooltip", tooltipStyle);
            #endregion
            GuiHost.SetSkin(skin);

            TooltipControl = new SimpleTooltip();

            #region sample window 5 - Panel, TextBox

            Window window5 = new Window();
            window5.Size = new Point(500, 130);
            window5.Position = new Point(0, 0);
            window5.Resizable = false;
            window5.AllowDragOut = false;
           // window5.Titlebar.Text = "Panel, TextBox";
            window5.Parent = this;

            Panel panel = new Panel();
            panel.Style = "frame";
            panel.Dock = DockStyle.Fill;
            panel.Parent = window5;
            panel.ClipFrame.Margin = new Margin(8);
            panel.ClipFrame.Style = "textbox";

            panel.VScroll.Margin = new Margin(0, 8, 8, 8);
            panel.VScroll.Size = new Squid.Point(14, 10);
            panel.VScroll.Slider.Style = "vscrollTrack";
            panel.VScroll.Slider.Button.Style = "vscrollButton";
            panel.VScroll.ButtonUp.Style = "vscrollUp";
            panel.VScroll.ButtonUp.Size = new Squid.Point(10, 20);
            panel.VScroll.ButtonDown.Style = "vscrollUp";
            panel.VScroll.ButtonDown.Size = new Squid.Point(10, 20);
            panel.VScroll.Slider.Margin = new Margin(0, 2, 0, 2);

            TextBox t = new TextBox();
            t.Text = "je suis un test";
            t.Dock = DockStyle.Fill;
            t.Parent = panel;
            t.AllowDrop = false;
            t.Style = "chatlog";
                          
            //for (int i = 0; i < 10; i++)
            //{
            //    Label label = new Label();
            //    label.Text = "label control:";
            //    label.Size = new Point(100, 35);
            //    label.Position = new Point(10, 10 + 45 * i);
            //    panel.Content.Controls.Add(label);

            //    TextBox txt = new TextBox();
            //    txt.Text = "lorem ipsum";
            //    txt.Size = new Squid.Point(222, 35);
            //    txt.Position = new Point(110, 10 + 45 * i);
            //    txt.Style = "textbox";
            //    txt.AllowDrop = true;
            //    txt.TabIndex = 1 + i;
            //    panel.Content.Controls.Add(txt);
            //}

            #endregion
        }
    }
}
