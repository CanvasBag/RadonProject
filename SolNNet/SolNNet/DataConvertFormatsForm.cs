using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeoMathLib.Calc;
using GeoMathLib.Calc.AjustNet2D;

namespace SolNNet
{
    public partial class DataConvertFormatsForm : Form
    {
        private XmlDataGenerator txtToXmlGenerator;
        private String metricSystem, angularSystem;
        private Char separator;

        public DataConvertFormatsForm()
        {
            InitializeComponent();
            comBoxMetricUnits.SelectedIndex = 0;
            comBoxAngUnits.SelectedIndex = 1;
            comBoxSeparator.SelectedIndex = 0;            
        }

        #region Buttons Events
        private void butBrowseTrigPts_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TXT Point Files (*.txt)|*.txt";

            dialog.Title = "Select a Trignometric Point File";
            

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBoxTrigPts.Text = dialog.FileName;
            }
        }

        private void butBrowseDist_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TXT Distance Files (*.txt)|*.txt";

            dialog.Title = "Select a Distance File";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBoxDist.Text = dialog.FileName;
            }
        }

        private void butBrowseDir_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TXT Directions Files (*.txt)|*.txt";

            dialog.Title = "Select a Direction Point File";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBoxDir.Text = dialog.FileName;
            }
        }

        private void butBrowseAz_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TXT Azimuth Files (*.txt)|*.txt";

            dialog.Title = "Select an Azimuth File";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBoxAz.Text = dialog.FileName;
            }
        }

        private void butBrowseXMLOut_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML Files (*.xml)|*.xml";

            dialog.Title = "Select a Trignometric Point File";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtBoxXMLOut.Text = dialog.FileName;
            }
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            XmlDataGenerator.Txt2Xml(txtBoxTrigPts.Text, txtBoxDist.Text, txtBoxDir.Text, AngularSystem, Separator, txtBoxXMLOut.Text);
            this.Close();
        }

        private void butConvert_Click(object sender, EventArgs e)
        {
            XmlDataGenerator.Txt2Xml(txtBoxTrigPts.Text, txtBoxDist.Text, txtBoxDir.Text, AngularSystem, Separator, txtBoxXMLOut.Text);
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region ComboBox Events
        private void comBoxSeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comBoxSeparator.SelectedIndex == 2)
                txtBoxSeparator.Enabled = true;
            else
                txtBoxSeparator.Enabled = false;
        }
        #endregion

        #region Sets & Gets

        private String MetricSystem
        {
            set {  metricSystem = value; }
            get
            {
                defineMetricSystem();
                return metricSystem;
            }
        }
        private void defineMetricSystem()
        {
            MetricSystem = comBoxMetricUnits.SelectedIndex == 0 ? "meters" : "";
        }

        private String AngularSystem
        {
            set { angularSystem = value; }
            get
            {
                defineAngularSystem();
                return angularSystem;
            }
        }
        private void defineAngularSystem()
        {
            AngularSystem = comBoxAngUnits.SelectedIndex == 0 ? "radian" : 
                            comBoxAngUnits.SelectedIndex == 1 ? "degree" : 
                            comBoxAngUnits.SelectedIndex == 2 ? "dms" : "gradian";
        }

        private Char Separator
        {
            set { separator = value; }
            get 
            {
                defineSeparator();
                return separator;
            }
        }

        private void defineSeparator()
        {
            Separator = comBoxSeparator.SelectedIndex == 0 ? ' ' :
                        comBoxSeparator.SelectedIndex == 1 ? ',' :
                        comBoxSeparator.SelectedIndex == 2 ? ';' : Convert.ToChar(txtBoxSeparator.Text);
        }
        #endregion
    }
}
