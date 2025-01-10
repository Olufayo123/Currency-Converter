using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace Currency_Converter
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private Dictionary<string, double> _currencies = new Dictionary<string, double>();

        public Form1()
        {
            InitializeComponent();

            var currencies = GetLatestCurrrencies();
            dynamic responseObject = JsonConvert.DeserializeObject(currencies);

            foreach (var item in responseObject.data)
            {
                SourceCombo.Items.Add(item.First.code.ToString());
                ConvertedCombo.Items.Add(item.First.code.ToString());
                _currencies.Add(item.First.code.ToString(), item.First.value.Value);
            }


        }

        private string GetLatestCurrrencies()
        {
            var client = new RestClient("https://api.currencyapi.com/v3/latest");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "cur_live_AepvhN1R1ehxyRFSlpRbb4KAtK6D86oXkBHm454S");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            var selectedSourceCurrency = SourceCombo.SelectedItem;
            var selectedConvertedCurrency = ConvertedCombo.SelectedItem;
            var amount = double.Parse(txtAmount.Text);

            var sourceCurrentExchangeRate = _currencies
                .First (c=> c.Key == selectedSourceCurrency)
            .Value;

            var convertedCurrencyExchangeRate = _currencies.
                First(c => c.Key == selectedConvertedCurrency)
            .Value;

            var calculatedAmount = (amount * sourceCurrentExchangeRate) * convertedCurrencyExchangeRate;

            TextResult.Text = $"Result : {calculatedAmount} {_currencies.First(c => c.Key == selectedConvertedCurrency).Key}" ;

        }

        
    }
        }

     
