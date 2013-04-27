using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squid;
using Point = Squid.Point;

namespace TTRPG_case
{
    public class BottomUI : DrawableGameComponent
    {
        private Desktop ui;
        public BottomUI(Game game) : base(game)
        {
            this.UpdateOrder = 10;
        }

        protected override void LoadContent()
        {
            this.ui = new DesktopUi() {Name = "ui"};
            ui.ShowCursor = true;
            
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            ui.Size = new Point(800,200);
            ui.Position = new Point(0,600);
            ui.Update();
            ui.Draw();

            base.Draw(gameTime);
        }
    }
}
