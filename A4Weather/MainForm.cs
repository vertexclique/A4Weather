using A4Weather.Core;
using A4Weather.Properties;
using A4Weather.Requesters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A4Weather
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            if (cbCity.SelectedIndex <= -1)
            {
                MessageBox.Show("Please select city...", "City Selection");
            }
            else
            {
                tabSelection();
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {

        }

        private void tcWeather_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabSelection();
        }

        public void tabSelection()
        {
            if (tcWeather.SelectedTab == tabCurrent)
            {
                if (bgwCurrent.IsBusy == false)
                {
                    this.bgwCurrent.RunWorkerAsync(cbCity.SelectedIndex);
                }
            }
            else if (tcWeather.SelectedTab == tabRadar)
            {
                if (bgwRadar.IsBusy == false)
                {
                    this.bgwRadar.RunWorkerAsync(cbCity.SelectedIndex);
                }
            }
            else if (tcWeather.SelectedTab == tabSatellite)
            {
                if (bgwSatellite.IsBusy == false)
                {
                    this.bgwSatellite.RunWorkerAsync(cbCity.SelectedIndex);
                }
            }
            else if (tcWeather.SelectedTab == tabCombined)
            {
                if (bgwCombined.IsBusy == false)
                {
                    this.bgwCombined.RunWorkerAsync(cbCity.SelectedIndex);
                }
            }
            else if (tcWeather.SelectedTab == tabSun)
            {
                if (bgwSunMoon.IsBusy == false)
                {
                    this.bgwSunMoon.RunWorkerAsync(cbCity.SelectedIndex);
                }
            }
            else
            {
                if (bgwSunMoon.IsBusy == false)
                {
                    this.bgwSunMoon.RunWorkerAsync(cbCity.SelectedIndex);
                }
            }
            
        }

        /**
         * Radar image
         */
        Image radar;

        private void bgwRadar_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGetter dg = new DataGetter();
            int selectedIndex = (int) e.Argument;
            if (selectedIndex > -1)
            {
                if (selectedIndex == 0)
                {
                    radar = dg.getMariborRadarImage();
                }
                else
                {
                    radar = dg.getIstanbulRadarImage();
                }
            }
        }

        private void bgwRadar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pbRadar.Image = radar;
        }

        /**
         * Satellite image
         */
        Image satellite;

        private void bgwSatellite_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGetter dg = new DataGetter();
            int selectedIndex = (int)e.Argument;
            if (selectedIndex > -1)
            {
                if (selectedIndex == 0)
                {
                    satellite = dg.getMariborSatelliteImage();
                }
                else
                {
                    satellite = dg.getIstanbulSatelliteImage();
                }
            }
        }

        private void bgwSatellite_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pbSatellite.Image = satellite;
        }



        /**
         * Combined Image
         */
        Image combined;

        private void bgwCombined_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGetter dg = new DataGetter();
            int selectedIndex = (int)e.Argument;
            if (selectedIndex > -1)
            {
                if (selectedIndex == 0)
                {
                    combined = dg.getMariborCombinedImage();
                }
                else
                {
                    combined = dg.getIstanbulCombinedImage();
                }
            }
        }

        private void bgwCombined_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pbCombined.Image = combined;
        }

        /**
         * Current weather data JSON serialization object
         */
        CurrentWeatherData currentData;
        Image weathericon;

        private void bgwCurrent_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGetter dg = new DataGetter();
            int selectedIndex = (int)e.Argument;
            if (selectedIndex > -1)
            {
                if (selectedIndex == 0)
                {
                    currentData = dg.getCurrentData(Constants.MARIBOR_CURRENT_URL);
                    weathericon = dg.getImage(currentData.current_observation.icon_url);
                }
                else
                {
                    currentData = dg.getCurrentData(Constants.ISTANBUL_CURRENT_URL);
                    weathericon = dg.getImage(currentData.current_observation.icon_url);
                }
            }
        }

        private void bgwCurrent_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Atmosphere
                this.lblHumidity.Text = currentData.current_observation.relative_humidity;
                this.lblVisibility.Text = currentData.current_observation.visibility_km;
                this.lblPressure.Text = currentData.current_observation.pressure_mb;
                this.lblPresTrend.Text = currentData.current_observation.pressure_trend;
                this.lblSolarRad.Text = currentData.current_observation.solarradiation + " nm";
                this.lblUV.Text = currentData.current_observation.UV;

                //Condition
                this.lblTemp.Text = "" + currentData.current_observation.temp_c;
                this.lblFeelsLike.Text = currentData.current_observation.feelslike_c;
                this.lblDate.Text = currentData.current_observation.observation_time_rfc822;
                this.lblWStat.Text = currentData.current_observation.weather;
                this.lblCode.Text = currentData.current_observation.icon;
                this.lblDew.Text = "" + currentData.current_observation.dewpoint_c;
                this.pbCurrent.Image = weathericon;

                //Location
                this.lblCity.Text = currentData.current_observation.observation_location.city;
                this.lblState.Text = currentData.current_observation.display_location.state_name;
                this.lblCountry.Text = currentData.current_observation.display_location.country_iso3166;

                //Wind
                this.lblDesc.Text = currentData.current_observation.wind_string;
                this.lblDirection.Text = currentData.current_observation.wind_dir;
                this.lblGust.Text = "" + currentData.current_observation.wind_gust_kph;
                this.lblSpeed.Text = "" + currentData.current_observation.wind_kph;

                //Geography
                this.lblLatitude.Text = currentData.current_observation.display_location.latitude;
                this.lblLongitude.Text = currentData.current_observation.display_location.longitude;
                this.lblElevation.Text = currentData.current_observation.display_location.elevation;


                /**
                 * Terminal JSON
                 */
                this.tbTerminal.Text += ("\n\n###### RESPONSE JSON ######\n\n" + currentData.strJSON);
            }
            catch (Exception ex)
            {
                this.tbTerminal.Text += ("\n\nException: " + ex.Message + "\n\n");
            }
            
        }


        /**
         * Sun and Moon data async worker
         * */

        SunMoonData sunmoon;

        private void bgwSunMoon_DoWork(object sender, DoWorkEventArgs e)
        {
            DataGetter dg = new DataGetter();
            int selectedIndex = (int)e.Argument;
            if (selectedIndex > -1)
            {
                if (selectedIndex == 0)
                {
                    sunmoon = dg.getSunMoonData(Constants.MARIBOR_SUN_MOON);
                }
                else
                {
                    sunmoon = dg.getSunMoonData(Constants.ISTANBUL_SUN_MOON);
                }
            }
        }

        private void bgwSunMoon_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //SUN
                this.lblSunrise.Text = sunmoon.moon_phase.sunrise.hour + ":" + sunmoon.moon_phase.sunrise.minute;
                this.lblSunset.Text = sunmoon.moon_phase.sunset.hour + ":" + sunmoon.moon_phase.sunset.minute;
                
                //MOON
                this.lblIlluminate.Text = sunmoon.moon_phase.percentIlluminated;
                this.lblMoonAge.Text = sunmoon.moon_phase.ageOfMoon;

                int moonPercentage = Convert.ToInt32(sunmoon.moon_phase.percentIlluminated);
                if (moonPercentage == 0m)
                {
                    this.pbMoon.Image = Resources.new_moon;
                }
                else if (moonPercentage < 7m)
                {
                    this.pbMoon.Image = Resources.waxing_crescent;
                }
                else if (moonPercentage == 7m)
                {
                    this.pbMoon.Image = Resources.first_quarter;
                }
                else if (moonPercentage > 7m && moonPercentage < 14m)
                {
                    this.pbMoon.Image = Resources.waxing_gibbous;
                }
                else if (moonPercentage == 14m)
                {
                    this.pbMoon.Image = Resources.full_moon;
                }
                else if (moonPercentage > 14m && moonPercentage < 22m)
                {
                    this.pbMoon.Image = Resources.waning_gibbous;
                }
                else if (moonPercentage == 22m)
                {
                    this.pbMoon.Image = Resources.third_quarter;
                }
                else
                {
                    this.pbMoon.Image = Resources.waning_crescent;
                }

                this.tbTerminal.Text += ("\n\n### RESPONSE SUN&MOON JSON ###\n\n" + sunmoon.rawJson);
            }
            catch (Exception ex)
            {
                this.tbTerminal.Text += ("\n\nException: " + ex.Message + "\n\n");
            }
            
        }




    }
}
