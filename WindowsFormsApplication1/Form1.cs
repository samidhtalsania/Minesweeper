using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        Dictionary<int, bool> map = new Dictionary<int, bool>();
        Dictionary<int, int> nonMinesValues = new Dictionary<int, int>();
        int minesCount = 99;
        List<int> minesPos = new List<int>(99);
        int FlaggedCount = 99;
        List<int> flaggedButtons = new List<int>(99);

        
         
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 30; j++)
                {

                    Button btn = new Button();
                    btn.Name = (i * 30 + j).ToString();
                    btn.Height = 20;
                    btn.Width = 20;
                    btn.Text = (i * 30 + j).ToString();
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    Point point = new Point(j*20, i*20);
                    btn.Location = point;
                    panel1.Controls.Add(btn);
                    btn.Text = string.Empty;
                    btn.MouseDown += btn_Click;
                    btn.EnabledChanged += btn_EnabledChanged;
                    
                    map.Add(i * 30 + j, false);
                }
            }

            Random rand = new Random();
            while (minesPos.Count < minesCount)
            {
                int temp = rand.Next(480);
                if (!minesPos.Contains(temp))
                {
                    minesPos.Add(temp);
                }
            }

            foreach (int pos in minesPos)
            {
                map[pos] = true;
            }


            //setup other numbers
            Control.ControlCollection col = panel1.Controls;
            for(int pos = 0 ; pos <= 479 ; pos++)
            {
                //Button b = panel1.Controls[pos] as Button;
                
                
                if (map[pos] == true) 
                {
                    continue;
                }

                /*
                 * 
                 * if(pos+1)%30 = 0 left end
                 * if(pos)%30 right end
                 *
                 * if(pos < 30) top row
                 * if(pos > 449) btm row
                 * 
                 * if 0 top left corner
                 * if 29 top right corner
                 * 
                 * if 450 btm left corner
                 * if 479 btmRight corner
                 * 
                 */

                int count = 0;
                int TopLeft = pos - 31;
                int Top = pos - 30;
                int TopRight = pos - 29;
                int CentreLeft = pos - 1;
                int CentrRight = pos + 1;
                int BottomLeft = pos + 29;
                int Bottom = pos + 30;
                int BottomRight = pos + 31;

                if (pos == 0)
                {
                    if (calcPos(CentrRight)) count++;
                    if (calcPos(Bottom)) count++;
                    if (calcPos(BottomRight)) count++;
                    //b.Text = count.ToString();
                    nonMinesValues.Add(pos, count);
                    continue;
                }

                if (pos == 29)
                {
                    if (calcPos(CentreLeft)) count++;
                    if (calcPos(Bottom)) count++;
                    if (calcPos(BottomLeft)) count++;
                    //b.Text = count.ToString();
                    nonMinesValues.Add(pos, count);
                    continue;
                }

                if (pos == 450)
                {
                    if (calcPos(Top)) count++;
                    if (calcPos(TopRight)) count++;
                    if (calcPos(CentrRight)) count++;
                    //b.Text = count.ToString();
                    nonMinesValues.Add(pos, count);
                    continue;
                }

                if (pos == 479)
                {
                    if (calcPos(Top)) count++;
                    if (calcPos(TopLeft)) count++;
                    if (calcPos(CentreLeft)) count++;
                    //b.Text = count.ToString();
                    nonMinesValues.Add(pos, count);
                    continue;
                }

                if (pos % 30 == 0) 
                {
                    if (calcPos(CentrRight)) count++;
                    if (calcPos(Bottom)) count++;
                    if (calcPos(BottomRight)) count++;
                    if (calcPos(Top)) count++;
                    if (calcPos(TopRight)) count++;
                    //b.Text = count.ToString();
                    nonMinesValues.Add(pos, count);
                    continue;
                }

                if ((pos+1) % 30 == 0)
                {
                    
                    if (calcPos(Bottom)) count++;
                    if (calcPos(BottomLeft)) count++;
                    if (calcPos(Top)) count++;
                    if (calcPos(TopLeft)) count++;
                    if (calcPos(CentreLeft)) count++;
                    nonMinesValues.Add(pos, count);
                    //b.Text = count.ToString();
                    continue;
                }

                if(pos < 30 ) 
                {
                    if (calcPos(CentrRight)) count++;
                    if (calcPos(CentreLeft)) count++;
                    if (calcPos(Bottom)) count++;
                    if (calcPos(BottomRight)) count++;
                    if (calcPos(BottomLeft)) count++;
                    nonMinesValues.Add(pos, count);
                    //b.Text = count.ToString();
                    continue;
                }
                if (pos > 449) 
                {
                    if (calcPos(CentrRight)) count++;
                    if (calcPos(CentreLeft)) count++;

                    if (calcPos(Top)) count++;
                    if (calcPos(TopRight)) count++;
                    if (calcPos(TopLeft)) count++;
                    nonMinesValues.Add(pos, count);
                    //b.Text = count.ToString();
                    continue;
                }

                if (calcPos(CentrRight)) count++;
                if (calcPos(CentreLeft)) count++;
                if (calcPos(Bottom)) count++;
                if (calcPos(BottomRight)) count++;
                if (calcPos(BottomLeft)) count++;
                if (calcPos(Top)) count++;
                if (calcPos(TopRight)) count++;
                if (calcPos(TopLeft)) count++;
                nonMinesValues.Add(pos, count);
                //b.Text = count.ToString();
                
                
            }

            flagCount.Text = FlaggedCount.ToString();

        }

       

        void btn_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Bold);
            switch (nonMinesValues[Int32.Parse(btn.Name)])
            {

                case 1:
                    btn.ForeColor = Color.Blue;
                    break;
                case 2:
                    btn.ForeColor = Color.Green;
                    break;
                case 3:
                    btn.ForeColor = Color.DarkCyan;
                    break;
                case 4:
                    btn.ForeColor = Color.Red;
                    break;
                case 5:
                    btn.ForeColor = Color.DarkBlue;
                    break;
                case 6:
                    btn.ForeColor = Color.DarkMagenta;
                    break;
                default:
                    btn.ForeColor = Color.DarkGray;
                    break;
            }
        }



        void btn_Click(object sender, EventArgs e)
        {

            MouseEventArgs ev = e as MouseEventArgs;
            Button b = sender as Button;
            
            if (ev.Button == System.Windows.Forms.MouseButtons.Left) 
            {

                b.Paint += b_Paint_left;    
                int pos = Int32.Parse(b.Name);
                if (map[pos] == true)
                {
                    //game over
                    MessageBox.Show("Game Over", "Bombed!", MessageBoxButtons.OK);
                    Application.Restart();
                }
                else
                {
                    if (b.Text.Equals("*"))
                    {
                        FlaggedCount++;
                        flagCount.Text = FlaggedCount.ToString();
                        flaggedButtons.Remove(Int32.Parse(b.Name));
                    }
                    b.Enabled = false;
                    b.Text = nonMinesValues[pos].ToString();

                    if (nonMinesValues[pos] == 0)
                    {
                        //reveal all zeroes
                        revealAll(pos, new List<int>());
                    }

                }
            }
            else if (ev.Button == System.Windows.Forms.MouseButtons.Right)
            {

                b.Paint += b_Paint_Right;
                //FIRST CHECK FLAG COUNT
                //IF FLAG COUNT IS > 0 then flag that button
                //If button already flagged remove thae flag
                //if flag count = 0  check game state

                if (FlaggedCount == 0)
                {
                    MessageBox.Show("No More Flags", "No More Flags", MessageBoxButtons.OK);
                }
                else
                {
                    //checked if Button is flagged
                    //if flagged remove that flag
                    if (b.Text.Equals("*"))
                    {
                        b.Text = string.Empty;
                        FlaggedCount++;
                        flagCount.Text = FlaggedCount.ToString();
                        flaggedButtons.Remove(Int32.Parse(b.Name));
                    }

                    //flag the button
                    else 
                    {
                        b.Text = "*";
                        FlaggedCount--;
                        flagCount.Text = FlaggedCount.ToString();
                        flaggedButtons.Add(Int32.Parse(b.Name));
                        if(FlaggedCount == 0)
                            checkGameState();
                    }

                }
               
            }
        }

        private void b_Paint_Right(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            dynamic drawBrush = new SolidBrush(btn.ForeColor);
            dynamic sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            if (btn.Text.Equals("*"))
            {
                e.Graphics.DrawString("*", btn.Font, drawBrush, e.ClipRectangle, sf);
            }
            else 
            {
                e.Graphics.DrawString(string.Empty, btn.Font, drawBrush, e.ClipRectangle, sf);
            }

            drawBrush.Dispose();
            sf.Dispose();
        }

        private void b_Paint_left(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            dynamic drawBrush = new SolidBrush(btn.ForeColor);
            dynamic sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            btn.Text = string.Empty;
            try
            {
                if (nonMinesValues[Int32.Parse(btn.Name)] != 0)
                    e.Graphics.DrawString(nonMinesValues[Int32.Parse(btn.Name)].ToString(), btn.Font, drawBrush, e.ClipRectangle, sf);
            }
            catch (KeyNotFoundException)
            {
                
            }

            drawBrush.Dispose();
            sf.Dispose();
        }

        private void checkGameState() 
        {
           
            bool finished = true;
            foreach (int flag in flaggedButtons)
            {
                if (!minesPos.Contains(flag))
                {
                    finished = false;
                    break;
                }
            }

            if (finished)
            {
                MessageBox.Show("No More Mines", "Game Won", MessageBoxButtons.OK);
                Application.Restart();
            }

            
           
            
        }

        private void revealAll(int pos, List<int> list)
        {
            //recursively reveal all the buttons values
            
            if (nonMinesValues[pos] != 0)
            {
                Button b = panel1.Controls[pos] as Button;
                b.Text = nonMinesValues[pos].ToString();
                b.Enabled = false;
                b.Paint += b_Paint_left;
                return;
            }

            else if (list.Contains(pos))
            {
               return;
            }
            else
            {
                if (pos < 0 || pos > 479)
                {
                    return;
                }
                if (pos == 0)
                {
                    list.Add(pos);
                    revealAll(pos + 1, list);
                    revealAll(pos + 30, list);
                    revealAll(pos + 31, list);
                    disableButton(pos);
                }

                else if (pos == 29)
                {
                    list.Add(pos);
                    revealAll(pos - 1, list);
                    revealAll(pos + 30, list);
                    revealAll(pos + 29, list);
                    disableButton(pos);
                }

                else if (pos == 450)
                {
                    list.Add(pos);
                    revealAll(pos + 1, list);
                    revealAll(pos - 30, list);
                    revealAll(pos - 29, list);
                    disableButton(pos);
                }

                else if (pos == 479)
                {
                    list.Add(pos);
                    revealAll(pos - 1, list);
                    revealAll(pos - 30, list);
                    revealAll(pos - 31, list);
                    disableButton(pos);
                }

                else if (pos % 30 == 0)
                {
                    list.Add(pos);
                    revealAll(pos + 30, list);
                    revealAll(pos + 31, list);
                    revealAll(pos - 30, list);
                    revealAll(pos - 29, list);
                    revealAll(pos + 1, list);
                    disableButton(pos);
                }

                else if ((pos + 1) % 30 == 0)
                {
                    list.Add(pos);
                    revealAll(pos + 30, list);
                    revealAll(pos + 29, list);
                    revealAll(pos - 30, list);
                    revealAll(pos - 31, list);
                    revealAll(pos - 1, list);
                    disableButton(pos);
                }

                else if (pos < 30)
                {
                    list.Add(pos);
                    revealAll(pos + 29, list);
                    revealAll(pos + 30, list);
                    revealAll(pos + 31, list);
                    
                    revealAll(pos - 1, list);
                    revealAll(pos + 1, list);
                    disableButton(pos);
                }
                else if (pos > 449)
                {
                    list.Add(pos);
                    revealAll(pos - 30, list);
                    revealAll(pos - 31, list);
                    revealAll(pos - 29, list);
                    revealAll(pos - 1, list);
                    revealAll(pos + 1, list);
                    disableButton(pos);
                }

                else
                {
                    list.Add(pos);
                    revealAll(pos + 30, list);
                    revealAll(pos + 31, list);
                    revealAll(pos + 29, list);
                    revealAll(pos - 30, list);
                    revealAll(pos - 29, list);
                    revealAll(pos - 31, list);
                    revealAll(pos - 1, list);
                    revealAll(pos + 1, list);
                    disableButton(pos);
                }
            }
        }

        private bool calcPos(int pos) 
        {
            try
            {
                return map[pos];
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private void disableButton(int pos) 
        {
            Button b = panel1.Controls[pos] as Button;
            b.Text = string.Empty;
            b.Enabled = false;
            b.Paint += b_Paint_left;
        }
    
    }
}
