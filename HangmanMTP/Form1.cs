using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HangmanMTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string cuvant="";
        List<Label> labels = new List<Label>();
        int incercari = 0;

        enum BodyParts
        {
            Head,
            LeftEye,
            RightEye,
            Mouth,
            Body,
            RightArm,
            LeftArm,
            RightLeg,
            LeftLeg
        }

        void DrawHangPost()
        {
            Graphics g=panel1.CreateGraphics();
            Pen p = new Pen(Color.Brown, 10);
            g.DrawLine(p, new Point(100, 318), new Point(160, 318));
            g.DrawLine(p, new Point(130, 318), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));
            //DrawBodyPart(BodyParts.Head);
            //DrawBodyPart(BodyParts.LeftEye);
            //DrawBodyPart(BodyParts.RightEye);
            //DrawBodyPart(BodyParts.Mouth);
            //DrawBodyPart(BodyParts.Body);
            //DrawBodyPart(BodyParts.LeftArm);
           //DrawBodyPart(BodyParts.RightArm);
            //DrawBodyPart(BodyParts.LeftLeg);
            //DrawBodyPart(BodyParts.RightLeg);
            //MessageBox.Show(getRandomWord());
        }
        void DrawBodyPart(BodyParts bp)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 2);
            if (bp == BodyParts.Head)
                g.DrawEllipse(p, 40, 50, 40, 40);
            else if(bp == BodyParts.LeftEye)
            {
                SolidBrush brush = new SolidBrush(Color.Black);
                g.FillEllipse(brush, 50, 60, 5, 5);
            }
            else if(bp== BodyParts.RightEye)
            {
                SolidBrush brush = new SolidBrush(Color.Black);
                g.FillEllipse(brush, 63, 60, 5, 5);
            }
            else if(bp==BodyParts.Mouth)
            {
                g.DrawArc(p, 50, 60, 20, 20, 45, 90);
            }
            else if(bp==BodyParts.Body)
            {
                g.DrawLine(p, new Point(60, 90), new Point(60, 170));
            }
            else if (bp==BodyParts.LeftArm)
            {
                g.DrawLine(p, new Point(60, 100), new Point(30, 85));
            }
            else if (bp == BodyParts.RightArm)
            {
                g.DrawLine(p, new Point(60, 100), new Point(90, 85));
            }
            else if(bp==BodyParts.LeftLeg)
            {
                g.DrawLine(p, new Point(60, 170), new Point(30, 190));
            }
            else if(bp==BodyParts.RightLeg)
            {
                g.DrawLine(p, new Point(60, 170), new Point(90, 190));
            }
        }

        void MakeLable()
        {
            cuvant = getRandomWord();
            //MessageBox.Show(cuvant);
            char[] litere = cuvant.ToCharArray();
            int between = 330/litere.Length-1;
            for(int i=0; i<litere.Length; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * between) + 10, 80);
                labels[i].Text = "_";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();
            }
            label1.Text = "Lungime Cuvant: " + litere.Length.ToString();
        }

        string getRandomWord()
        {
            string[] linii = File.ReadAllLines("cuvinte.txt");
            Random ran = new Random();
            return linii[ran.Next(0, linii.Length-1)].Trim().ToLower();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DrawHangPost();
            MakeLable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char litera = textBox1.Text.ToLower().ToCharArray()[0];
            if(!char.IsLetter(litera))
            {
                MessageBox.Show("Nu s-a introdus nicio litera");
                return;
            }
            if(cuvant.Contains(litera))
            {
                char[] litere=cuvant.ToCharArray();
                for(int i=0;i<litere.Length;i++)
                {
                    if (litere[i] == litera)
                        labels[i].Text = litera.ToString();             
                }
                foreach(Label l in labels)
                {
                    if (l.Text == "_") return;
                    MessageBox.Show("Ai Castigat! Felicitari!");
                    //ResetGame();
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Cuvantul nu contine litera!");
                label2.Text+=" "+litera.ToString()+",";
                DrawBodyPart((BodyParts)incercari);
                incercari++;
                if(incercari==9)
                {
                    MessageBox.Show("Ai ramas fara incercari! Cuvantul corect era: "+cuvant);
                    //ResetGame();
                    Application.Exit();
                }
            }

        }

        void ResetGame()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            getRandomWord();
            MakeLable();
            DrawHangPost();
            label2.Text = "Gresite: ";
            List<Label> labels = new List<Label>();
            incercari = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawHangPost();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text==cuvant)
            {
                MessageBox.Show("Ai Castigat! Felicitari");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Cuvantul introdus este gresit");
                DrawBodyPart((BodyParts)incercari);
                incercari++;
                if (incercari == 9)
                {
                    MessageBox.Show("Ai ramas fara incercari! Cuvantul corect era: " + cuvant);
                    //ResetGame();
                    Application.Exit();
                }
            }
        }
    }
}
