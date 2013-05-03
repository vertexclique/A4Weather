using A4Weather.Core;
using A4Weather.Requesters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A4Weather
{
    public partial class DateToDate : Form
    {
        public static int cln1Day = 0;
        public static int cln1Month = 0;
        public static int cln1Year = 0;

        public static int cln2Day = 0;
        public static int cln2Month = 0;
        public static int cln2Year = 0;

        public DateToDate()
        {
            InitializeComponent();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (bgw1.IsBusy == false)
            {
                this.bgw1.RunWorkerAsync(MainForm.selectedIndex);
            }
            if (bgw2.IsBusy == false)
            {
                this.bgw2.RunWorkerAsync(MainForm.selectedIndex);
            }
        }

        /**
         * Date to Date Comparison Data
         */

        HistoryData histdata1;
        HistoryData histdata2;

        private void bgw1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGetter dg = new DataGetter();
            if (MainForm.selectedIndex > -1)
            {
                if (MainForm.selectedIndex == 0)
                {
                    cln1Day = cln1.SelectionStart.Day; cln2Day = cln2.SelectionStart.Day;
                    cln1Month = cln1.SelectionStart.Month; cln2Month = cln2.SelectionStart.Month;
                    cln1Year = cln1.SelectionStart.Year; cln2Year = cln2.SelectionStart.Year;
                    histdata1 = dg.getHistoryData(Constants.makeMariborHistory(cln1Year,cln1Month,cln1Day));
                    histdata2 = dg.getHistoryData(Constants.makeMariborHistory(cln2Year, cln2Month, cln2Day));
                }
                else
                {
                    cln1Day = cln1.SelectionStart.Day; cln2Day = cln2.SelectionStart.Day;
                    cln1Month = cln1.SelectionStart.Month; cln2Month = cln2.SelectionStart.Month;
                    cln1Year = cln1.SelectionStart.Year; cln2Year = cln2.SelectionStart.Year;
                    histdata1 = dg.getHistoryData(Constants.makeIstanbulHistory(cln1Year, cln1Month, cln1Day));
                    histdata2 = dg.getHistoryData(Constants.makeIstanbulHistory(cln2Year, cln2Month, cln2Day));
                }
            }
        }

        private void bgw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                /**
                 * bgw1 Work
                 * */

                //Atmosphere
                this.lblHumidity1.Text = histdata1.history.dailysummary[0].humidity;
                this.lblVisibility1.Text = histdata1.history.dailysummary[0].meanvism;
                this.lblPressure1.Text = histdata1.history.observations[0].pressurem;
                this.lblPresTrend1.Text = histdata1.history.dailysummary[0].meanpressurem;

                //Condition
                this.lblTemp1.Text = histdata1.history.dailysummary[0].meantempm;
                this.lblDate1.Text = histdata1.history.date.pretty;
                this.lblCode1.Text = histdata1.history.date.tzname;
                this.lblDew1.Text = histdata1.history.observations[0].dewptm;

                //Wind
                this.lblDirection1.Text = histdata1.history.observations[0].wdire;
                this.lblGust1.Text = histdata1.history.observations[0].wgustm;
                this.lblSpeed1.Text = histdata1.history.dailysummary[0].meanwindspdm;

                
                /**
                 * bgw2 Work
                 * */

                //Atmosphere
                this.lblHumidity2.Text = histdata2.history.dailysummary[0].humidity;
                this.lblVisibility2.Text = histdata2.history.dailysummary[0].meanvism;
                this.lblPressure2.Text = histdata2.history.observations[0].pressurem;
                this.lblPresTrend2.Text = histdata2.history.dailysummary[0].meanpressurem;

                //Condition
                this.lblTemp2.Text = histdata2.history.dailysummary[0].meantempm;
                this.lblDate2.Text = histdata2.history.date.pretty;
                this.lblCode2.Text = histdata2.history.date.tzname;
                this.lblDew2.Text = histdata2.history.observations[0].dewptm;

                //Wind
                this.lblDirection2.Text = histdata2.history.observations[0].wdire;
                this.lblGust2.Text = histdata2.history.observations[0].wgustm;
                this.lblSpeed2.Text = histdata2.history.dailysummary[0].meanwindspdm;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probably date is not in range. See the exception message also: \n\n"+ex.Message);
            }
        }


    }
}
