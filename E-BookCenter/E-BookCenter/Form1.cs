using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
namespace E_BookCenter
{
    public partial class 
        Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.MaximumSize.Width>1000 || this.MaximumSize.Height>500)
            {
                MessageBox.Show("Hey");
                
            }
        }
        string filename = "";
        int a = 0;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if(m.Msg==0x0112){
                if (m.WParam == new IntPtr(0xF030) || m.WParam == new IntPtr(0xF120)) {
                    if (a == 0)
                    {
                        pictureBox1.Location = new Point(400, 210);
                        label1.Location = new Point(635, 340);
                        label2.Location = new Point(635, 420);
                        label3.Location = new Point(530, 500);
                        linkLabel1.Location = new Point(780, 500);
                        a = 1;
                    }
                    else {
                        pictureBox1.Location = new Point(190, 68);
                        label1.Location = new Point(415, 197);
                        label2.Location = new Point(415, 268);
                        label3.Location = new Point(307, 351);
                        linkLabel1.Location = new Point(548, 352);
                        a = 0   ;
                    }

                }
            }
            
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit", "Are you sure?", MessageBoxButtons.YesNo);
            if (result.ToString() == "Yes")
            {
                base.OnFormClosing(e);
            }
            else { e.Cancel = true; }
        }
       
       
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {   
        }
       

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           DialogResult result=MessageBox.Show("Do you really want to exit","Are you sure?",MessageBoxButtons.YesNo);
           if (result.ToString() == "Yes")
           {
               System.Environment.Exit(0);
           }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
             openFileDialog1.InitialDirectory = @"C:\";
             openFileDialog1.Title = "Open PDF Files";
             openFileDialog1.CheckFileExists = true;
             openFileDialog1.CheckPathExists = true;
             openFileDialog1.DefaultExt = "txt";
             openFileDialog1.Filter = "Text files (*.pdf)|*.pdf";
             openFileDialog1.FilterIndex = 2;
             openFileDialog1.RestoreDirectory = true;
             openFileDialog1.ReadOnlyChecked = true;
             openFileDialog1.ShowReadOnly = true;

             if (openFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 filename = openFileDialog1.FileName;
                 if (testPdf(filename))
                 {
                     gggToolStripMenuItem.Image = System.Drawing.Image.FromFile("D:/favorite3.png");
                 }
                 else 
                 {
                     gggToolStripMenuItem.Image = System.Drawing.Image.FromFile("C:/Users/SU/Desktop/favourite4.png");
                 }
                 gggToolStripMenuItem.Visible = true;
                hideData();
                //axAcroPDF1.src = openFileDialog1.FileName;
                //axAcroPDF1.Visible = true;
               
                
                      
            }       
        }
       
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save PDF Files";

            saveFileDialog1.Filter = "Text files (*.pdf)|*.pdf";

            saveFileDialog1.InitialDirectory = @"C:\";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

                Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);

                PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(saveFileDialog1.FileName, FileMode.Create));
                doc.Open();
                int i = 0;
                foreach (TextBox item in this.Controls.OfType<TextBox>()) {


                    if (i > 0)
                    {
                        Paragraph p = new Paragraph(item.Text);
                        doc.NewPage();
                        doc.Add(p);
                    }
                    else { i = 2;
                    doc.Add(new Paragraph(item.Text));
                    }

                   }
               
                doc.Close();
            }

        }

       
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip2.Visible = true;
            TextBox page;
            char a=' ';
            do{
               
                page = new TextBox();
                page.Location = new Point(325,50);
                page.Size = new Size(850,650);
                page.Multiline = true;
               
                this.Controls.Add(page);
                hideData();
                page.KeyDown += page_KeyDown;
                page.Parent = this;
                if(page.Text.Length ==page.MaxLength ){a='y';}else{a='n';}
        } while (a=='y');
        }    

        TextBox items;
        private void page_KeyDown(object sender,KeyEventArgs e)
        {
            TextBox page;
            if(e.KeyCode==Keys.Enter){
                int b = 0;
               
                foreach (TextBox item in this.Controls.OfType<TextBox>()) { b++; }
                foreach (TextBox item in this.Controls.OfType<TextBox>())
                {
                    items = item;
                    int cline = item.Lines.Length;

                }
                    int cursorposition = items.SelectionStart;
                   int lineindex = items.GetLineFromCharIndex(cursorposition);
                    if (lineindex== 47)
                    {
                        items.Select(0,49);
                        items.Cut();

                        e.Handled = true;
                        if (sender == items)
                        {
                            page = new TextBox();
                            page.Location = new Point(325, 750);
                            page.Size = new Size(850, 650);
                            page.Multiline = true;
                            page.KeyDown += page_KeyDown;
                            page.Parent = this;
                            this.Controls.Add(page);
                        }

                    }
            }
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit", "Are you sure?", MessageBoxButtons.YesNo);
            if (result.ToString() == "Yes")
            {
                
                this.Close();
               
            }


        }

        private void back_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
         
            
        }
        public void hideData() {
            pictureBox1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            linkLabel1.Visible = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bToolStripMenuItem.BackColor = new Color();
            foreach (TextBox item in this.Controls.OfType<TextBox>()) 
            {
                if (item.SelectionStart > 0)
                {
                    item.Font = new System.Drawing.Font(item.Font, FontStyle.Bold);
                }
            }

        }

        private void updatePDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.FileName!=""){
                int p = 0;
               // int xp = 320, yp = 50;
                PdfReader rd = new PdfReader("D:/My pdf/LearnJava.pdf");
                while (p < rd.NumberOfPages)
                {


                    Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);

                    PdfWriter write = PdfWriter.GetInstance(doc, new FileStream("D:/Pdf/page"+p+".pdf", FileMode.Create));

                    doc.Open();
                 //   doc.Add(new Paragraph(PdfTextExtractor.ReferenceEquals(rd, p + 1)));
                    doc.Close();
                   // TextBox txt = new TextBox();

                   // txt.Multiline = true;
                   // txt.Size = new Size(800, 800);
                    
                   // txt.Location = new Point(xp, yp);
                   // this.Controls.Add(txt);

                    //yp += 850;
                    p++;

                } 
            }
            
        }
       
        public void savePdf(String page) {
           
        }
       
        private void gggToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (!filename.Equals("") ) {
                if (!testPdf(filename))
                {
                    gggToolStripMenuItem.Image = System.Drawing.Image.FromFile("D:/favorite3.png");
                }
                else {
                    gggToolStripMenuItem.Image = System.Drawing.Image.FromFile("C:/Users/SU/Desktop/favourite4.png");
                }
              
                } 
        }
        public Boolean testPdf(String pdfname){
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\v11.0; database=ebookdatabase;Integrated Security=true");
            String qry = "select pdfpath from Favourite where pdfpath="+pdfname;
            SqlDataAdapter da = new SqlDataAdapter(qry,con);
            DataSet ds = new DataSet();
            
                da.Fill(ds,"favourite");
            DataView dv = new DataView(ds.Tables["favourite"]);
            if (dv.Count != 0 && dv.Count == 1)
            {
                return true;
            }
            else {
                return false;
            }
            

        }
    }
}
