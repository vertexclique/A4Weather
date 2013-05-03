using A4Weather.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace A4Weather.Requesters
{
    class DataGetter
    {
        public Image getImage(String url)
        {
            HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse) req.GetResponse();
            Stream stream = resp.GetResponseStream();
            return Image.FromStream(stream, true, true);
        }

        /**
         * Maribor Images
         */
        public Image getMariborRadarImage()
        {
            return getImage(Constants.MARIBOR_ANIMATED_RADAR);
        }

        public Image getMariborSatelliteImage()
        {
            return getImage(Constants.MARIBOR_ANIMATED_SATELLITE);
        }

        public Image getMariborCombinedImage()
        {
            return getImage(Constants.MARIBOR_ANIMATED_COMBINED);
        }

        /**
         * Istanbul Images
         */
        public Image getIstanbulRadarImage()
        {
            return getImage(Constants.ISTANBUL_ANIMATED_RADAR);
        }

        public Image getIstanbulSatelliteImage()
        {
            return getImage(Constants.ISTANBUL_ANIMATED_SATELLITE);
        }

        public Image getIstanbulCombinedImage()
        {
            return getImage(Constants.ISTANBUL_ANIMATED_COMBINED);
        }

        /**
         * JSON getter of Current Weather data
         * */

        public CurrentWeatherData getCurrentData(String url)
        {
            var json = new WebClient().DownloadString(url);
            CurrentWeatherData current = JsonConvert.DeserializeObject<CurrentWeatherData>(json);
            current.strJSON = json;
            return current;
        }

        /**
         * JSON getter of Sun and Moon data
         * */
        public SunMoonData getSunMoonData(String url)
        {
            var json = new WebClient().DownloadString(url);
            SunMoonData sunmoon = JsonConvert.DeserializeObject<SunMoonData>(json);
            sunmoon.rawJson = json;
            return sunmoon;
        }

        /**
         * JSON getter of Date to Date data
         * */
        public HistoryData getHistoryData(String url)
        {
            var json = new WebClient().DownloadString(url);
            HistoryData histdata = JsonConvert.DeserializeObject<HistoryData>(json);
            return histdata;
        }
    }
}
