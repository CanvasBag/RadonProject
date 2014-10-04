using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AjustLeastSquare.Statistics;

namespace SolNNet
{
    public partial class FormDxfExport : Form
    {
        List<CheckedListBox> clBoxList;
        private List<Ellipse> errorEllipseList, confidanceEllipseList, relativeEllipseList;
        private List<double> displacements;

        public FormDxfExport(List<CheckedListBox> clBoxList, List<double> displacements, List<Ellipse> errorEllipseList = null,
                             List<Ellipse> confidanceEllipseList = null, List<Ellipse> relativeEllipseList = null)
        {
            this.clBoxList = clBoxList;
            this.errorEllipseList = errorEllipseList;
            this.confidanceEllipseList = confidanceEllipseList;
            this.relativeEllipseList = relativeEllipseList;
            this.displacements = displacements;

            InitializeComponent();

            comboBoxColorTrig.SelectedIndex = 1;
            comboBoxColorControl.SelectedIndex = 2;
            comboBoxColorFixed.SelectedIndex = 3;
            comboBoxColorDir.SelectedIndex = 4;
            comboBoxColorDist.SelectedIndex = 5;
            comboBoxColorAz.SelectedIndex = 6;
            comboBoxColorDelta.SelectedIndex = 7;
            comboBoxColorEllip.SelectedIndex = 8;
            comboBoxColorEllipC.SelectedIndex = 9;
            comboBoxColorEllipR.SelectedIndex = 10;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDxfBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "DXF Files (*.dxf)|*.dxf";

            dialog.Title = "Save a dxf file...";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDxfPath.Text = dialog.FileName;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                List<Tuple<bool, string, short>> layersAndColors = new List<Tuple<bool, string, short>>();

                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxTrig.Checked, textBoxLayerTrig.Text, Convert.ToInt16(comboBoxColorTrig.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxControl.Checked, textBoxLayerControl.Text, Convert.ToInt16(comboBoxColorControl.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxFixed.Checked, textBoxLayerFixed.Text, Convert.ToInt16(comboBoxColorFixed.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxDir.Checked, textBoxLayerDir.Text, Convert.ToInt16(comboBoxColorDir.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxDist.Checked, textBoxLayerDist.Text, Convert.ToInt16(comboBoxColorDist.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxAz.Checked, textBoxLayerAz.Text, Convert.ToInt16(comboBoxColorAz.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxDelta.Checked, textBoxLayerDelta.Text, Convert.ToInt16(comboBoxColorDelta.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxEllip.Checked, textBoxLayerEllip.Text, Convert.ToInt16(comboBoxColorEllip.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxEllipC.Checked, textBoxLayerEllipC.Text, Convert.ToInt16(comboBoxColorEllipC.SelectedItem)));
                layersAndColors.Add(new Tuple<bool, string, short>(checkBoxEllipR.Checked, textBoxLayerEllipR.Text, Convert.ToInt16(comboBoxColorEllipR.SelectedItem)));



                DxfExport newDxf = new DxfExport(textBoxDxfPath.Text, layersAndColors, clBoxList, displacements, errorEllipseList,
                                confidanceEllipseList, relativeEllipseList, Convert.ToDouble(scaleEllipseTBox.Text), Convert.ToDouble(scaleDeltaTBox.Text));

                this.Close();
            }
            catch
            {
                MessageBox.Show("Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




    }
}
