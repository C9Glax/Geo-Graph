namespace GeoGraph
{
    public struct Utils
    {
        public static double DistanceBetween(Node n1, Node n2)
        {
            return DistanceBetween(n1.lat, n1.lon, n2.lat, n2.lon);
        }

        public static double DistanceBetween(Node n1, float lat2, float lon2)
        {
            return DistanceBetween(n1.lat, n1.lon, lat2, lon2);
        }

        public static double DistanceBetween(float lat1, float lon1, Node n2)
        {
            return DistanceBetween(n2, lat1, lon1);
        }

        public static double DistanceBetween(float lat1, float lon1, float lat2, float lon2) //Law of Cosines
        {
            /*
             * From https://www.movable-type.co.uk/scripts/latlong.html
             */
            double rlat1 = DegreesToRadians(lat1);
            double rlat2 = DegreesToRadians(lat2);
            double drlon = DegreesToRadians(lon2 - lon1);
            const int R = 6371;

            double d = Math.Acos(Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(drlon)) * R;

            return d;
        }

        public static double DistanceBetweenHaversine(float lat1, float lon1, float lat2, float lon2)
        {
            /*
             * From https://www.movable-type.co.uk/scripts/latlong.html
             */
            const int R = 6371;
            double rlat1 = DegreesToRadians(lat1);
            double rlat2 = DegreesToRadians(lat2);
            double rdlat = DegreesToRadians(lat2 - lat1);
            double drlon = DegreesToRadians(lon2 - lon1);
            double a = Math.Sin(rdlat / 2) * Math.Sin(rdlat / 2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Sin(drlon / 2) * Math.Sin(drlon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d;
        }

        private static double DegreesToRadians(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        private static double RadiansToDegrees(double rad)
        {
            return rad * 180.0 / Math.PI;
        }


    }
} 