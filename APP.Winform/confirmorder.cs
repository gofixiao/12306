using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using RockUtility;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Xml.Linq;


namespace APP.Winform
{


    public partial class confirmorder : Form
    {
        public Dictionary<string, string> orderParams;
        public Dictionary<string, string> queryTicketParams;
        public CookieCollection reqCookies = null;
        public List<Passenger> passengetList = new List<Passenger>();
        public string seatAndTicketType;
        public string TrainInfo;
        public string TicketInfo;
        public string token;
        public string leftTicketStr;
        public string validateCodePath;
        string passengerXMLPath = Application.StartupPath + "\\passengers.xml";
        Dictionary<string, string> cardTypeDic = new Dictionary<string, string>();

        JEnumerable<JToken> seatList;
        JEnumerable<JToken> ticketList;


        public confirmorder()
        {
            InitializeComponent();
        }


        private void confirmorder_Load(object sender, EventArgs e)
        {
            cardTypeDic.Add("1", "二代身份证");
            cardTypeDic.Add("2", "一代身份证");
            cardTypeDic.Add("C", "港澳通行证");
            cardTypeDic.Add("G", "台湾通行证");
            cardTypeDic.Add("B", "护照");

            this.pictureBox1.ImageLocation = validateCodePath;
            ConstructCombox();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].DataPropertyName = dataGridView1.Columns[i].Name;
            }

            this.label1.Text = TrainInfo;
            this.label2.Text = TicketInfo;

            if (File.Exists(passengerXMLPath))
            {
                XElement root = XElement.Load(passengerXMLPath);
                var list = root.Elements().ToList();
                this.checkedListBox1.Items.Clear();
                list.ForEach(x =>
                {
                    passengetList.Add(new Passenger()
                    {
                        first_letter = x.Attribute("first_letter").Value,
                        isUserSelf = x.Attribute("isUserSelf").Value,
                        mobile_no = x.Attribute("mobile_no").Value,
                        old_passenger_id_no = x.Attribute("old_passenger_id_no").Value,
                        old_passenger_id_type_code = x.Attribute("old_passenger_id_type_code").Value,
                        old_passenger_name = x.Attribute("old_passenger_name").Value,
                        passenger_flag = x.Attribute("passenger_flag").Value,
                        passenger_id_no = x.Attribute("passenger_id_no").Value,
                        passenger_id_type_code = x.Attribute("passenger_id_type_code").Value,
                        passenger_id_type_name = x.Attribute("passenger_id_type_name").Value,
                        passenger_name = x.Attribute("passenger_name").Value,
                        passenger_type = x.Attribute("passenger_type").Value,
                        passenger_type_name = x.Attribute("passenger_type_name").Value,
                        recordCount = x.Attribute("recordCount").Value
                    });
                    this.checkedListBox1.Items.Add(x.Value);
                });
            }
            else
            {
                passengetList = SavePassenger();

                if (passengetList != null)
                {
                    this.checkedListBox1.Items.Clear();
                    passengetList.ForEach(x =>
                    {
                        this.checkedListBox1.Items.Add(x.passenger_name);
                    });
                }
            }
        }
        public List<Passenger> SavePassenger()
        {
            HttpGet get = new HttpGet("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=getpassengerJson");
            get.req.CookieContainer.Add(reqCookies);
            string json = get.SendReqReturnResponseString();
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    passengetList = JsonConvert.DeserializeObject<List<Passenger>>(JObject.Parse(json)["passengerJson"].ToString());

                    if (passengetList.Count > 0)
                    {
                        XDocument xdoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
                        XElement rootele = new XElement("data");
                        passengetList.ForEach(x =>
                        {
                            XElement city = new XElement("city");
                            city.SetAttributeValue("first_letter", x.first_letter);
                            city.SetAttributeValue("isUserSelf", x.isUserSelf);
                            city.SetAttributeValue("mobile_no", x.mobile_no);
                            city.SetAttributeValue("old_passenger_id_no", x.old_passenger_id_no);
                            city.SetAttributeValue("old_passenger_id_type_code", x.old_passenger_id_type_code);
                            city.SetAttributeValue("old_passenger_name", x.old_passenger_name);
                            city.SetAttributeValue("passenger_flag", x.passenger_flag);
                            city.SetAttributeValue("passenger_id_no", x.passenger_id_no);
                            city.SetAttributeValue("passenger_id_type_code", x.passenger_id_type_code);
                            city.SetAttributeValue("passenger_id_type_name", x.passenger_id_type_name);
                            city.SetAttributeValue("passenger_name", x.passenger_name);
                            city.SetAttributeValue("passenger_type", x.passenger_type);
                            city.SetAttributeValue("passenger_type_name", x.passenger_type_name);
                            city.SetAttributeValue("recordCount", x.recordCount);
                            city.SetValue(x.passenger_name);
                            rootele.Add(city);
                        });
                        xdoc.Add(rootele);
                        xdoc.Save(passengerXMLPath);
                        return passengetList;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        private void ConstructCombox()
        {
            if (string.IsNullOrEmpty(seatAndTicketType))
            {
                return;
            }
            try
            {
                JObject json = JObject.Parse(seatAndTicketType);
                seatList = json["seat_type_codes"].Children();
                ticketList = json["ticket_type_codes"].Children();
            }
            catch
            {

            }

        }



        public void setDataGridRow(int index)
        {
            string name = checkedListBox1.Items[index].ToString();
            Passenger currentPassenger = passengetList.FirstOrDefault(x => x.passenger_name == name);
            bool isExits = false;

            foreach (DataGridViewRow item in this.dataGridView1.Rows)
            {
                if (item.Cells["name"] != null && item.Cells["name"].Value != null
                    && currentPassenger.passenger_name == item.Cells["name"].Value.ToString())
                {
                    this.dataGridView1.Rows.Remove(item);
                    isExits = true;
                }
            }

            if (!isExits)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridView1);

                ComboBox box = new ComboBox();
                //box.Items.Add();

                row.Cells[0].Value = string.Format("第{0}位", this.dataGridView1.Rows.Count + 1);
                row.Cells[3].Value = currentPassenger.passenger_name;
                row.Cells[4].Value = cardTypeDic[currentPassenger.passenger_id_type_code];
                row.Cells[5].Value = currentPassenger.passenger_id_no;
                row.Cells[6].Value = currentPassenger.mobile_no;

                this.dataGridView1.Rows.Add(row);
            }

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            setDataGridRow(e.Index);
            //for (int i = 0; i < checkedListBox1.Items.Count; i++)
            //{
            //    if (checkedListBox1.GetItemChecked(i))
            //    {

            //    }
            //}
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                return;
            }
            DataGridViewComboBoxColumn cbb = this.dataGridView1.Columns[1] as DataGridViewComboBoxColumn;
            cbb.DefaultCellStyle.NullValue = seatList.FirstOrDefault()["value"].ToString();
            if (cbb != null)
            {
                foreach (JToken item in seatList)
                {
                    cbb.Items.Add(item["value"].ToString());
                }
            }

            DataGridViewComboBoxColumn cbb1 = this.dataGridView1.Columns[2] as DataGridViewComboBoxColumn;
            cbb1.DefaultCellStyle.NullValue = cardTypeDic["1"];
            if (cbb1 != null)
            {
                foreach (JToken item in ticketList)
                {
                    cbb1.Items.Add(item["value"].ToString());
                }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //实现单击一次显示下拉列表框
            if (this.dataGridView1.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex != -1)
            {
                SendKeys.Send("{F4}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Dictionary<string, string> postdic = new Dictionary<string, string>();
            List<string> keys = new List<string>();
            List<string> values = new List<string>();
            SendOrderReqParams postdic = new SendOrderReqParams(keys, values);
            postdic.Add("org.apache.struts.taglib.html.TOKEN", token);
            postdic.Add("leftTicketStr", leftTicketStr);
            postdic.Add("textfield", "中文或拼音首字母");//搜索乘客框value
            for (int j = 0; j < this.checkedListBox1.Items.Count; j++)
            {
                if (checkedListBox1.GetItemChecked(j))
                {
                    postdic.Add("checkbox" + j.ToString(), j.ToString());
                }
            }
            postdic.Add("orderRequest.train_date", queryTicketParams["orderRequest.train_date"]);
            postdic.Add("orderRequest.train_no", orderParams["trainno4"]);
            postdic.Add("orderRequest.station_train_code", orderParams["station_train_code"]);
            postdic.Add("orderRequest.from_station_telecode", queryTicketParams["orderRequest.from_station_telecode"]);
            postdic.Add("orderRequest.to_station_telecode", queryTicketParams["orderRequest.to_station_telecode"]);
            postdic.Add("orderRequest.seat_type_code", string.Empty);
            postdic.Add("orderRequest.ticket_type_order_num", string.Empty);
            postdic.Add("orderRequest.bed_level_order_num", "000000000000000000000000000000");
            postdic.Add("orderRequest.start_time", orderParams["train_start_time"]);
            postdic.Add("orderRequest.end_time", orderParams["arrive_time"]);
            postdic.Add("orderRequest.from_station_name", orderParams["from_station_name"]);
            postdic.Add("orderRequest.to_station_name", orderParams["to_station_name"]);
            postdic.Add("orderRequest.cancel_flag", "1");
            postdic.Add("orderRequest.id_mode", "Y");

            int passengerCounts = 1;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (i == this.dataGridView1.Rows.Count - 1)
                {
                    continue;
                }
                Dictionary<string, string> passengetDic = new Dictionary<string, string>();
                passengetDic.Add(string.Format("passenger_{0}_seat", passengerCounts), "M");
                passengetDic.Add(string.Format("passenger_{0}_ticket", passengerCounts), "1");
                passengetDic.Add(string.Format("passenger_{0}_name", passengerCounts), this.dataGridView1.Rows[i].Cells[3].Value.ToString().Trim());
                foreach (string key in cardTypeDic.Keys)
                {
                    if (cardTypeDic[key] == this.dataGridView1.Rows[i].Cells[4].Value.ToString())
                    {
                        passengetDic.Add(string.Format("passenger_{0}_cardtype", passengerCounts), key);
                    }
                }
                passengetDic.Add(string.Format("passenger_{0}_cardno", passengerCounts), this.dataGridView1.Rows[i].Cells[5].Value.ToString().Trim());
                passengetDic.Add(string.Format("passenger_{0}_mobileno", passengerCounts), this.dataGridView1.Rows[i].Cells[6].Value.ToString().Trim());

                string passengerTickets = string.Format("M,0,1,{0},{1},{2},{3},Y", passengetDic[string.Format("passenger_{0}_name", passengerCounts)]
                    , passengetDic[string.Format("passenger_{0}_cardtype", passengerCounts)],
                     passengetDic[string.Format("passenger_{0}_cardno", passengerCounts)],
                     passengetDic[string.Format("passenger_{0}_mobileno", passengerCounts)]);

                string oldPassengers = passengetDic[string.Format("passenger_{0}_name", passengerCounts)] + "," +
                    passengetDic[string.Format("passenger_{0}_cardtype", passengerCounts)] + "," +
                     passengetDic[string.Format("passenger_{0}_cardno", passengerCounts)];

                postdic.Add("passengerTickets", passengerTickets);
                postdic.Add("oldPassengers", oldPassengers);

                foreach (string key in passengetDic.Keys)
                {
                    postdic.Add(key, passengetDic[key]);
                }
                postdic.Add("checkbox9", "Y");
                passengerCounts++;
            }

            for (int k = 0; k < 5 - passengerCounts + 1; k++)
            {
                postdic.Add("oldPassengers", string.Empty);
                postdic.Add("checkbox9", "Y");
            }

            postdic.Add("randCode", this.textBox1.Text.Trim());
            postdic.Add("orderRequest.reserve_flag", "A");
            postdic.Add("tFlag", "dc");

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keys.Count; i++)
            {
                sb.AppendFormat("{0}={1}&", keys[i], values[i]);
            }
            sb.Remove(sb.ToString().LastIndexOf("&"), 1);
            HttpPost post = new HttpPost("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=checkOrderInfo&rand=" + this.textBox1.Text + "",
                sb.ToString());
            post.req.CookieContainer.Add(reqCookies);
            string resultstr = post.SendReqReturnResponseString();

            if (!string.IsNullOrEmpty(resultstr))
            {
                JObject json1 = JObject.Parse(resultstr);
                if (json1["msg"] != null && !string.IsNullOrEmpty(json1["msg"].Value<string>()))
                {
                    MessageBox.Show(json1["msg"].Value<string>());
                    return;
                }
            }
            HttpGet ticketLeftGet = new HttpGet(string.Format("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=getQueueCount&train_date={0}&train_no={1}&station={2}&seat={3}&from={4}&to={5}&ticket={6}",
               queryTicketParams["orderRequest.train_date"], orderParams["trainno4"],
               orderParams["station_train_code"], "M", queryTicketParams["orderRequest.from_station_telecode"],
               queryTicketParams["orderRequest.to_station_telecode"],
               leftTicketStr));
            ticketLeftGet.req.CookieContainer.Add(reqCookies);
            string ticketLeftGetResult = ticketLeftGet.SendReqReturnResponseString();

            HttpPost orderpost = new HttpPost("https://dynamic.12306.cn/otsweb/order/confirmPassengerAction.do?method=confirmSingleForQueue",
                sb.ToString().Replace("&tFlag=dc", string.Empty));
            orderpost.req.CookieContainer.Add(reqCookies);
            string orderpostresult = orderpost.SendReqReturnResponseString();
        }


    }
}
