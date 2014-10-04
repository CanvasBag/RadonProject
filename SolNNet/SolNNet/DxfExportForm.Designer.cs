namespace SolNNet
{
    partial class FormDxfExport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonDxfBrowse = new System.Windows.Forms.Button();
            this.textBoxDxfPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.scaleDeltaTBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.scaleEllipseTBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxLayerEllipR = new System.Windows.Forms.TextBox();
            this.comboBoxColorEllipR = new System.Windows.Forms.ComboBox();
            this.textBoxLayerEllipC = new System.Windows.Forms.TextBox();
            this.comboBoxColorEllipC = new System.Windows.Forms.ComboBox();
            this.textBoxLayerEllip = new System.Windows.Forms.TextBox();
            this.comboBoxColorEllip = new System.Windows.Forms.ComboBox();
            this.checkBoxEllipR = new System.Windows.Forms.CheckBox();
            this.checkBoxEllipC = new System.Windows.Forms.CheckBox();
            this.checkBoxEllip = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxLayerDelta = new System.Windows.Forms.TextBox();
            this.checkBoxDelta = new System.Windows.Forms.CheckBox();
            this.comboBoxColorDelta = new System.Windows.Forms.ComboBox();
            this.textBoxLayerAz = new System.Windows.Forms.TextBox();
            this.checkBoxAz = new System.Windows.Forms.CheckBox();
            this.comboBoxColorAz = new System.Windows.Forms.ComboBox();
            this.textBoxLayerDist = new System.Windows.Forms.TextBox();
            this.checkBoxDist = new System.Windows.Forms.CheckBox();
            this.comboBoxColorDist = new System.Windows.Forms.ComboBox();
            this.checkBoxDir = new System.Windows.Forms.CheckBox();
            this.textBoxLayerDir = new System.Windows.Forms.TextBox();
            this.comboBoxColorDir = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxLayerFixed = new System.Windows.Forms.TextBox();
            this.comboBoxColorFixed = new System.Windows.Forms.ComboBox();
            this.textBoxLayerControl = new System.Windows.Forms.TextBox();
            this.comboBoxColorControl = new System.Windows.Forms.ComboBox();
            this.checkBoxFixed = new System.Windows.Forms.CheckBox();
            this.checkBoxControl = new System.Windows.Forms.CheckBox();
            this.checkBoxTrig = new System.Windows.Forms.CheckBox();
            this.textBoxLayerTrig = new System.Windows.Forms.TextBox();
            this.comboBoxColorTrig = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDxfBrowse
            // 
            this.buttonDxfBrowse.Location = new System.Drawing.Point(426, 419);
            this.buttonDxfBrowse.Name = "buttonDxfBrowse";
            this.buttonDxfBrowse.Size = new System.Drawing.Size(44, 23);
            this.buttonDxfBrowse.TabIndex = 0;
            this.buttonDxfBrowse.Text = "...";
            this.buttonDxfBrowse.UseVisualStyleBackColor = true;
            this.buttonDxfBrowse.Click += new System.EventHandler(this.buttonDxfBrowse_Click);
            // 
            // textBoxDxfPath
            // 
            this.textBoxDxfPath.Location = new System.Drawing.Point(62, 422);
            this.textBoxDxfPath.Name = "textBoxDxfPath";
            this.textBoxDxfPath.Size = new System.Drawing.Size(358, 20);
            this.textBoxDxfPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 425);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DXF file";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(320, 454);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(401, 454);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 403);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.scaleDeltaTBox);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.scaleEllipseTBox);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(7, 347);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(451, 50);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Scales";
            // 
            // scaleDeltaTBox
            // 
            this.scaleDeltaTBox.Location = new System.Drawing.Point(358, 20);
            this.scaleDeltaTBox.Name = "scaleDeltaTBox";
            this.scaleDeltaTBox.Size = new System.Drawing.Size(80, 20);
            this.scaleDeltaTBox.TabIndex = 3;
            this.scaleDeltaTBox.Text = "500";
            this.scaleDeltaTBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Delta Scale";
            // 
            // scaleEllipseTBox
            // 
            this.scaleEllipseTBox.Location = new System.Drawing.Point(141, 20);
            this.scaleEllipseTBox.Name = "scaleEllipseTBox";
            this.scaleEllipseTBox.Size = new System.Drawing.Size(120, 20);
            this.scaleEllipseTBox.TabIndex = 1;
            this.scaleEllipseTBox.Text = "500";
            this.scaleEllipseTBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ellipse Design Scale";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Layer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Color";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxLayerEllipR);
            this.groupBox4.Controls.Add(this.comboBoxColorEllipR);
            this.groupBox4.Controls.Add(this.textBoxLayerEllipC);
            this.groupBox4.Controls.Add(this.comboBoxColorEllipC);
            this.groupBox4.Controls.Add(this.textBoxLayerEllip);
            this.groupBox4.Controls.Add(this.comboBoxColorEllip);
            this.groupBox4.Controls.Add(this.checkBoxEllipR);
            this.groupBox4.Controls.Add(this.checkBoxEllipC);
            this.groupBox4.Controls.Add(this.checkBoxEllip);
            this.groupBox4.Location = new System.Drawing.Point(6, 250);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(452, 90);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ellipses";
            // 
            // textBoxLayerEllipR
            // 
            this.textBoxLayerEllipR.Location = new System.Drawing.Point(278, 61);
            this.textBoxLayerEllipR.Name = "textBoxLayerEllipR";
            this.textBoxLayerEllipR.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerEllipR.TabIndex = 20;
            this.textBoxLayerEllipR.Text = "RelativeEllipses";
            // 
            // comboBoxColorEllipR
            // 
            this.comboBoxColorEllipR.FormattingEnabled = true;
            this.comboBoxColorEllipR.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorEllipR.Location = new System.Drawing.Point(141, 61);
            this.comboBoxColorEllipR.Name = "comboBoxColorEllipR";
            this.comboBoxColorEllipR.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorEllipR.TabIndex = 19;
            // 
            // textBoxLayerEllipC
            // 
            this.textBoxLayerEllipC.Location = new System.Drawing.Point(278, 39);
            this.textBoxLayerEllipC.Name = "textBoxLayerEllipC";
            this.textBoxLayerEllipC.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerEllipC.TabIndex = 18;
            this.textBoxLayerEllipC.Text = "ConfidenceEllipses";
            // 
            // comboBoxColorEllipC
            // 
            this.comboBoxColorEllipC.FormattingEnabled = true;
            this.comboBoxColorEllipC.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorEllipC.Location = new System.Drawing.Point(141, 39);
            this.comboBoxColorEllipC.Name = "comboBoxColorEllipC";
            this.comboBoxColorEllipC.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorEllipC.TabIndex = 17;
            // 
            // textBoxLayerEllip
            // 
            this.textBoxLayerEllip.Location = new System.Drawing.Point(278, 17);
            this.textBoxLayerEllip.Name = "textBoxLayerEllip";
            this.textBoxLayerEllip.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerEllip.TabIndex = 16;
            this.textBoxLayerEllip.Text = "ErrorEllipses";
            // 
            // comboBoxColorEllip
            // 
            this.comboBoxColorEllip.FormattingEnabled = true;
            this.comboBoxColorEllip.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorEllip.Location = new System.Drawing.Point(141, 17);
            this.comboBoxColorEllip.Name = "comboBoxColorEllip";
            this.comboBoxColorEllip.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorEllip.TabIndex = 15;
            // 
            // checkBoxEllipR
            // 
            this.checkBoxEllipR.AutoSize = true;
            this.checkBoxEllipR.Location = new System.Drawing.Point(14, 63);
            this.checkBoxEllipR.Name = "checkBoxEllipR";
            this.checkBoxEllipR.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEllipR.TabIndex = 2;
            this.checkBoxEllipR.Text = "Relative";
            this.checkBoxEllipR.UseVisualStyleBackColor = true;
            // 
            // checkBoxEllipC
            // 
            this.checkBoxEllipC.AutoSize = true;
            this.checkBoxEllipC.Checked = true;
            this.checkBoxEllipC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEllipC.Location = new System.Drawing.Point(14, 41);
            this.checkBoxEllipC.Name = "checkBoxEllipC";
            this.checkBoxEllipC.Size = new System.Drawing.Size(80, 17);
            this.checkBoxEllipC.TabIndex = 1;
            this.checkBoxEllipC.Text = "Confidence";
            this.checkBoxEllipC.UseVisualStyleBackColor = true;
            // 
            // checkBoxEllip
            // 
            this.checkBoxEllip.AutoSize = true;
            this.checkBoxEllip.Checked = true;
            this.checkBoxEllip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEllip.Location = new System.Drawing.Point(14, 19);
            this.checkBoxEllip.Name = "checkBoxEllip";
            this.checkBoxEllip.Size = new System.Drawing.Size(94, 17);
            this.checkBoxEllip.TabIndex = 0;
            this.checkBoxEllip.Text = "Standard Error";
            this.checkBoxEllip.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxLayerDelta);
            this.groupBox3.Controls.Add(this.checkBoxDelta);
            this.groupBox3.Controls.Add(this.comboBoxColorDelta);
            this.groupBox3.Controls.Add(this.textBoxLayerAz);
            this.groupBox3.Controls.Add(this.checkBoxAz);
            this.groupBox3.Controls.Add(this.comboBoxColorAz);
            this.groupBox3.Controls.Add(this.textBoxLayerDist);
            this.groupBox3.Controls.Add(this.checkBoxDist);
            this.groupBox3.Controls.Add(this.comboBoxColorDist);
            this.groupBox3.Controls.Add(this.checkBoxDir);
            this.groupBox3.Controls.Add(this.textBoxLayerDir);
            this.groupBox3.Controls.Add(this.comboBoxColorDir);
            this.groupBox3.Location = new System.Drawing.Point(6, 132);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(452, 112);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lines";
            // 
            // textBoxLayerDelta
            // 
            this.textBoxLayerDelta.Location = new System.Drawing.Point(278, 84);
            this.textBoxLayerDelta.Name = "textBoxLayerDelta";
            this.textBoxLayerDelta.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerDelta.TabIndex = 17;
            this.textBoxLayerDelta.Text = "Delta";
            // 
            // checkBoxDelta
            // 
            this.checkBoxDelta.AutoSize = true;
            this.checkBoxDelta.Checked = true;
            this.checkBoxDelta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDelta.Location = new System.Drawing.Point(14, 86);
            this.checkBoxDelta.Name = "checkBoxDelta";
            this.checkBoxDelta.Size = new System.Drawing.Size(122, 17);
            this.checkBoxDelta.TabIndex = 15;
            this.checkBoxDelta.Text = "Delta (displacement)";
            this.checkBoxDelta.UseVisualStyleBackColor = true;
            // 
            // comboBoxColorDelta
            // 
            this.comboBoxColorDelta.FormattingEnabled = true;
            this.comboBoxColorDelta.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorDelta.Location = new System.Drawing.Point(141, 84);
            this.comboBoxColorDelta.Name = "comboBoxColorDelta";
            this.comboBoxColorDelta.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorDelta.TabIndex = 16;
            // 
            // textBoxLayerAz
            // 
            this.textBoxLayerAz.Location = new System.Drawing.Point(278, 61);
            this.textBoxLayerAz.Name = "textBoxLayerAz";
            this.textBoxLayerAz.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerAz.TabIndex = 14;
            this.textBoxLayerAz.Text = "Azimuths";
            // 
            // checkBoxAz
            // 
            this.checkBoxAz.AutoSize = true;
            this.checkBoxAz.Location = new System.Drawing.Point(14, 63);
            this.checkBoxAz.Name = "checkBoxAz";
            this.checkBoxAz.Size = new System.Drawing.Size(63, 17);
            this.checkBoxAz.TabIndex = 2;
            this.checkBoxAz.Text = "Azimuth";
            this.checkBoxAz.UseVisualStyleBackColor = true;
            // 
            // comboBoxColorAz
            // 
            this.comboBoxColorAz.FormattingEnabled = true;
            this.comboBoxColorAz.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorAz.Location = new System.Drawing.Point(141, 61);
            this.comboBoxColorAz.Name = "comboBoxColorAz";
            this.comboBoxColorAz.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorAz.TabIndex = 13;
            // 
            // textBoxLayerDist
            // 
            this.textBoxLayerDist.Location = new System.Drawing.Point(278, 39);
            this.textBoxLayerDist.Name = "textBoxLayerDist";
            this.textBoxLayerDist.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerDist.TabIndex = 12;
            this.textBoxLayerDist.Text = "Distances";
            // 
            // checkBoxDist
            // 
            this.checkBoxDist.AutoSize = true;
            this.checkBoxDist.Checked = true;
            this.checkBoxDist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDist.Location = new System.Drawing.Point(14, 41);
            this.checkBoxDist.Name = "checkBoxDist";
            this.checkBoxDist.Size = new System.Drawing.Size(73, 17);
            this.checkBoxDist.TabIndex = 1;
            this.checkBoxDist.Text = "Distances";
            this.checkBoxDist.UseVisualStyleBackColor = true;
            // 
            // comboBoxColorDist
            // 
            this.comboBoxColorDist.FormattingEnabled = true;
            this.comboBoxColorDist.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorDist.Location = new System.Drawing.Point(141, 39);
            this.comboBoxColorDist.Name = "comboBoxColorDist";
            this.comboBoxColorDist.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorDist.TabIndex = 11;
            // 
            // checkBoxDir
            // 
            this.checkBoxDir.AutoSize = true;
            this.checkBoxDir.Checked = true;
            this.checkBoxDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDir.Location = new System.Drawing.Point(14, 19);
            this.checkBoxDir.Name = "checkBoxDir";
            this.checkBoxDir.Size = new System.Drawing.Size(73, 17);
            this.checkBoxDir.TabIndex = 0;
            this.checkBoxDir.Text = "Directions";
            this.checkBoxDir.UseVisualStyleBackColor = true;
            // 
            // textBoxLayerDir
            // 
            this.textBoxLayerDir.Location = new System.Drawing.Point(278, 17);
            this.textBoxLayerDir.Name = "textBoxLayerDir";
            this.textBoxLayerDir.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerDir.TabIndex = 10;
            this.textBoxLayerDir.Text = "Directions";
            // 
            // comboBoxColorDir
            // 
            this.comboBoxColorDir.FormattingEnabled = true;
            this.comboBoxColorDir.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorDir.Location = new System.Drawing.Point(141, 17);
            this.comboBoxColorDir.Name = "comboBoxColorDir";
            this.comboBoxColorDir.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorDir.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxLayerFixed);
            this.groupBox2.Controls.Add(this.comboBoxColorFixed);
            this.groupBox2.Controls.Add(this.textBoxLayerControl);
            this.groupBox2.Controls.Add(this.comboBoxColorControl);
            this.groupBox2.Controls.Add(this.checkBoxFixed);
            this.groupBox2.Controls.Add(this.checkBoxControl);
            this.groupBox2.Controls.Add(this.checkBoxTrig);
            this.groupBox2.Controls.Add(this.textBoxLayerTrig);
            this.groupBox2.Controls.Add(this.comboBoxColorTrig);
            this.groupBox2.Location = new System.Drawing.Point(6, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 94);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Points";
            // 
            // textBoxLayerFixed
            // 
            this.textBoxLayerFixed.Location = new System.Drawing.Point(278, 61);
            this.textBoxLayerFixed.Name = "textBoxLayerFixed";
            this.textBoxLayerFixed.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerFixed.TabIndex = 8;
            this.textBoxLayerFixed.Text = "FixedPoints";
            // 
            // comboBoxColorFixed
            // 
            this.comboBoxColorFixed.FormattingEnabled = true;
            this.comboBoxColorFixed.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorFixed.Location = new System.Drawing.Point(141, 61);
            this.comboBoxColorFixed.Name = "comboBoxColorFixed";
            this.comboBoxColorFixed.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorFixed.TabIndex = 7;
            // 
            // textBoxLayerControl
            // 
            this.textBoxLayerControl.Location = new System.Drawing.Point(278, 39);
            this.textBoxLayerControl.Name = "textBoxLayerControl";
            this.textBoxLayerControl.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerControl.TabIndex = 6;
            this.textBoxLayerControl.Text = "ControlPoints";
            // 
            // comboBoxColorControl
            // 
            this.comboBoxColorControl.FormattingEnabled = true;
            this.comboBoxColorControl.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorControl.Location = new System.Drawing.Point(141, 39);
            this.comboBoxColorControl.Name = "comboBoxColorControl";
            this.comboBoxColorControl.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorControl.TabIndex = 5;
            // 
            // checkBoxFixed
            // 
            this.checkBoxFixed.AutoSize = true;
            this.checkBoxFixed.Checked = true;
            this.checkBoxFixed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFixed.Location = new System.Drawing.Point(14, 63);
            this.checkBoxFixed.Name = "checkBoxFixed";
            this.checkBoxFixed.Size = new System.Drawing.Size(83, 17);
            this.checkBoxFixed.TabIndex = 4;
            this.checkBoxFixed.Text = "Fixed Points";
            this.checkBoxFixed.UseVisualStyleBackColor = true;
            // 
            // checkBoxControl
            // 
            this.checkBoxControl.AutoSize = true;
            this.checkBoxControl.Checked = true;
            this.checkBoxControl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxControl.Location = new System.Drawing.Point(14, 41);
            this.checkBoxControl.Name = "checkBoxControl";
            this.checkBoxControl.Size = new System.Drawing.Size(91, 17);
            this.checkBoxControl.TabIndex = 3;
            this.checkBoxControl.Text = "Control Points";
            this.checkBoxControl.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrig
            // 
            this.checkBoxTrig.AutoSize = true;
            this.checkBoxTrig.Checked = true;
            this.checkBoxTrig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrig.Location = new System.Drawing.Point(14, 19);
            this.checkBoxTrig.Name = "checkBoxTrig";
            this.checkBoxTrig.Size = new System.Drawing.Size(76, 17);
            this.checkBoxTrig.TabIndex = 0;
            this.checkBoxTrig.Text = "Trig Points";
            this.checkBoxTrig.UseVisualStyleBackColor = true;
            // 
            // textBoxLayerTrig
            // 
            this.textBoxLayerTrig.Location = new System.Drawing.Point(278, 17);
            this.textBoxLayerTrig.Name = "textBoxLayerTrig";
            this.textBoxLayerTrig.Size = new System.Drawing.Size(161, 20);
            this.textBoxLayerTrig.TabIndex = 2;
            this.textBoxLayerTrig.Text = "TrigPoints";
            // 
            // comboBoxColorTrig
            // 
            this.comboBoxColorTrig.FormattingEnabled = true;
            this.comboBoxColorTrig.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64",
            "65",
            "66",
            "67",
            "68",
            "69",
            "70",
            "71",
            "72",
            "73",
            "74",
            "75",
            "76",
            "77",
            "78",
            "79",
            "80",
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "100",
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107",
            "108",
            "109",
            "110",
            "111",
            "112",
            "113",
            "114",
            "115",
            "116",
            "117",
            "118",
            "119",
            "120",
            "121",
            "122",
            "123",
            "124",
            "125",
            "126",
            "127",
            "128",
            "129",
            "130",
            "131",
            "132",
            "133",
            "134",
            "135",
            "136",
            "137",
            "138",
            "139",
            "140",
            "141",
            "142",
            "143",
            "144",
            "145",
            "146",
            "147",
            "148",
            "149",
            "150",
            "151",
            "152",
            "153",
            "154",
            "155",
            "156",
            "157",
            "158",
            "159",
            "160",
            "161",
            "162",
            "163",
            "164",
            "165",
            "166",
            "167",
            "168",
            "169",
            "170",
            "171",
            "172",
            "173",
            "174",
            "175",
            "176",
            "177",
            "178",
            "179",
            "180",
            "181",
            "182",
            "183",
            "184",
            "185",
            "186",
            "187",
            "188",
            "189",
            "190",
            "191",
            "192",
            "193",
            "194",
            "195",
            "196",
            "197",
            "198",
            "199",
            "200",
            "201",
            "202",
            "203",
            "204",
            "205",
            "206",
            "207",
            "208",
            "209",
            "210",
            "211",
            "212",
            "213",
            "214",
            "215",
            "216",
            "217",
            "218",
            "219",
            "220",
            "221",
            "222",
            "223",
            "224",
            "225",
            "226",
            "227",
            "228",
            "229",
            "230",
            "231",
            "232",
            "233",
            "234",
            "235",
            "236",
            "237",
            "238",
            "239",
            "240",
            "241",
            "242",
            "243",
            "244",
            "245",
            "246",
            "247",
            "248",
            "249",
            "250",
            "251",
            "252",
            "253",
            "254",
            "255"});
            this.comboBoxColorTrig.Location = new System.Drawing.Point(141, 17);
            this.comboBoxColorTrig.Name = "comboBoxColorTrig";
            this.comboBoxColorTrig.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColorTrig.TabIndex = 1;
            // 
            // FormDxfExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 489);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDxfPath);
            this.Controls.Add(this.buttonDxfBrowse);
            this.Name = "FormDxfExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DXF Export";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDxfBrowse;
        private System.Windows.Forms.TextBox textBoxDxfPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxTrig;
        private System.Windows.Forms.ComboBox comboBoxColorTrig;
        private System.Windows.Forms.TextBox textBoxLayerTrig;
        private System.Windows.Forms.CheckBox checkBoxFixed;
        private System.Windows.Forms.CheckBox checkBoxControl;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxEllipR;
        private System.Windows.Forms.CheckBox checkBoxEllipC;
        private System.Windows.Forms.CheckBox checkBoxEllip;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxAz;
        private System.Windows.Forms.CheckBox checkBoxDist;
        private System.Windows.Forms.CheckBox checkBoxDir;
        private System.Windows.Forms.TextBox textBoxLayerEllipR;
        private System.Windows.Forms.ComboBox comboBoxColorEllipR;
        private System.Windows.Forms.TextBox textBoxLayerEllipC;
        private System.Windows.Forms.ComboBox comboBoxColorEllipC;
        private System.Windows.Forms.TextBox textBoxLayerEllip;
        private System.Windows.Forms.ComboBox comboBoxColorEllip;
        private System.Windows.Forms.TextBox textBoxLayerAz;
        private System.Windows.Forms.ComboBox comboBoxColorAz;
        private System.Windows.Forms.TextBox textBoxLayerDist;
        private System.Windows.Forms.ComboBox comboBoxColorDist;
        private System.Windows.Forms.TextBox textBoxLayerDir;
        private System.Windows.Forms.ComboBox comboBoxColorDir;
        private System.Windows.Forms.TextBox textBoxLayerFixed;
        private System.Windows.Forms.ComboBox comboBoxColorFixed;
        private System.Windows.Forms.TextBox textBoxLayerControl;
        private System.Windows.Forms.ComboBox comboBoxColorControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLayerDelta;
        private System.Windows.Forms.CheckBox checkBoxDelta;
        private System.Windows.Forms.ComboBox comboBoxColorDelta;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox scaleDeltaTBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox scaleEllipseTBox;
        private System.Windows.Forms.Label label4;
    }
}