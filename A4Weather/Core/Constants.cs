using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4Weather.Core
{
    class Constants
    {
        public static String API_KEY = "12faa7a7e299fe1c";

        // Maribor, Slovenia constants
        public static String MARIBOR_CURRENT_URL = "http://api.wunderground.com/api/"+API_KEY+"/conditions/q/CA/Maribor.json";
        public static String MARIBOR_ANIMATED_RADAR = "http://api.wunderground.com/api/" + API_KEY + "/animatedradar/q/Slovenia/Maribor.gif?newmaps=1&timelabel=1&timelabel.y=10&num=5&delay=50&width=431&height=431";
        public static String MARIBOR_ANIMATED_SATELLITE = "http://api.wunderground.com/api/" + API_KEY + "/animatedsatellite/q/Slovenia/Maribor.gif?num=6&delay=50&interval=30&width=431&height=431";
        public static String MARIBOR_ANIMATED_COMBINED = "http://api.wunderground.com/api/" + API_KEY + "/animatedradar/animatedsatellite/q/Slovenia/Maribor.gif?num=15&delay=50&interval=30&rad.width=431&rad.height=431&sat.height=431&sat.width=431";
        public static String MARIBOR_SUN_MOON = "http://api.wunderground.com/api/"+API_KEY+"/astronomy/q/Slovenia/Maribor.json";

        //Istanbul, Turkey constants
        public static String ISTANBUL_CURRENT_URL = "http://api.wunderground.com/api/" + API_KEY + "/conditions/q/CA/Istanbul.json";
        public static String ISTANBUL_ANIMATED_RADAR = "http://api.wunderground.com/api/" + API_KEY + "/animatedradar/q/Turkey/Istanbul.gif?newmaps=1&timelabel=1&timelabel.y=10&num=5&delay=50&width=431&height=431";
        public static String ISTANBUL_ANIMATED_SATELLITE = "http://api.wunderground.com/api/" + API_KEY + "/animatedsatellite/q/Turkey/Istanbul.gif?num=6&delay=50&interval=30&width=431&height=431";
        public static String ISTANBUL_ANIMATED_COMBINED = "http://api.wunderground.com/api/" + API_KEY + "/animatedradar/animatedsatellite/q/Turkey/Istanbul.gif?num=15&delay=50&interval=30&rad.width=431&rad.height=431&sat.height=431&sat.width=431";
        public static String ISTANBUL_SUN_MOON = "http://api.wunderground.com/api/" + API_KEY + "/astronomy/q/Turkey/Istanbul.json";

    }
}
