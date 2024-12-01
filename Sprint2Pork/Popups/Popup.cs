using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Policy;
using System.Numerics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Image = System.Drawing.Image;

namespace Sprint2Pork.Popups
{
    public class Popup
    {

        private Form form;
        private bool formVisible;
        SolidBrush myBrush;
        Graphics formGraphics;

        private List<Rectangle> rects;
        private List<PictureBox> pbList;
        private List<Vector2> imgPos;
        private List<Vector2> displacement;
        private List<Vector2> previousDisplacement;

        private int amountCooldown = 30;
        private int popupCooldownCount = 0;

        private bool moved = false;

        public Popup(int x, int y, int width, int height)
        {
            rects = new();
            form = new Form()
            {
                Location = new Point(x, y),
                StartPosition = FormStartPosition.Manual,
                Size = new Size(width, height),
            };

            form.KeyDown += HandleKeyPresses;

            InitializeDrawing();
            InitializeSprites();
        }

        private void InitializeDrawing()
        {
            myBrush = new SolidBrush(Color.Red);
            formGraphics = form.CreateGraphics();
        }

        private void InitializeSprites()
        {
            pbList = new();
            imgPos = new();
            displacement = new();
            previousDisplacement = new();
        }

        public void ToggleRender()
        {
            if (!form.IsDisposed && popupCooldownCount > amountCooldown)
            {
                if (!formVisible)
                {
                    form.Show();
                }
                else
                {
                    form.Hide();
                }
                formVisible = !formVisible;
                popupCooldownCount = 0;
            }
        }

        public void AddRectangle(int x, int y, int width, int height)
        {
            rects.Add(new Rectangle(x, y, width, height));
        }

        public int AddImage(string location, int x, int y, int width, int height)
        {
            PictureBox pb1 = new() {
                Location = new Point(x, y),
                Size = new Size(width, height),
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = true,
            };
            pb1.Image?.Dispose();
            pb1.Image = null;
            pb1.Image = Image.FromFile(location);
            pbList.Add(pb1);
            form.Controls.Add(pbList[pbList.Count - 1]);
            imgPos.Add(new Vector2(x, y));
            displacement.Add(new Vector2(0, 0));
            previousDisplacement.Add(new Vector2(0, 0));
            return pbList.Count - 1;
        }

        private void HandleKeyPresses(object sender, KeyEventArgs e){
            if (e.KeyCode == System.Windows.Forms.Keys.P || e.KeyCode == System.Windows.Forms.Keys.Escape){
                ToggleRender();
            }
        }

        public void Draw()
        {
            foreach (Rectangle r in rects)
            {
                formGraphics.FillRectangle(myBrush, r);
            }
        }

        public void moveImage(int index, int dX, int dY)
        {
            displacement[index] = new Vector2(imgPos[index].X + dX, imgPos[index].Y + dY);
            moved = true;
        }

        public void setImagePos(int index, int x, int y)
        {
            displacement[index] = new Vector2(x, y);
            moved = true;
        }

        public void Update()
        {
            popupCooldownCount++;
            if (moved)
            {
                for (int i = 0; i < displacement.Count; i++)
                {
                    if (displacement[i].X != previousDisplacement[i].X ||
                        displacement[i].Y != previousDisplacement[i].Y)
                    {
                        form.Controls.Remove(pbList[i]);
                        pbList[i].Location = new Point((int)(imgPos[i].X + displacement[i].X), (int)(imgPos[i].Y + displacement[i].Y));
                        previousDisplacement[i] = displacement[i];
                        form.Controls.Add(pbList[i]);
                    }
                }
                moved = false;
            }
        }

    }
}
