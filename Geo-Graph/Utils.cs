namespace GeoGraph
{
    public struct Utils
    {
        const int EarthRadius = 6371000;
        public static double DistanceBetween(Node n1, Node n2)
        {
            return DistanceBetween(n1.Lat, n1.Lon, n2.Lat, n2.Lon);
        }

        public static double DistanceBetween(Node n1, float lat2, float lon2)
        {
            return DistanceBetween(n1.Lat, n1.Lon, lat2, lon2);
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
            double radiansLat1 = DegreesToRadians(lat1);
            double radiansLat2 = DegreesToRadians(lat2);
            double deltaRadiansLon = DegreesToRadians(lon2 - lon1);

            double d = Math.Acos(Math.Sin(radiansLat1) * Math.Sin(radiansLat2) + Math.Cos(radiansLat1) * Math.Cos(radiansLat2) * Math.Cos(deltaRadiansLon)) * EarthRadius;

            return d;
        }

        public static double DistanceBetweenHaversine(float lat1, float lon1, float lat2, float lon2)
        {
            /*
             * From https://www.movable-type.co.uk/scripts/latlong.html
             */
            double radiansLat1 = DegreesToRadians(lat1);
            double radiansLat2 = DegreesToRadians(lat2);
            double deltaRadiansLat = DegreesToRadians(lat2 - lat1);
            double deltaRadiansLon = DegreesToRadians(lon2 - lon1);
            double a = Math.Sin(deltaRadiansLat / 2) * Math.Sin(deltaRadiansLat / 2) + Math.Cos(radiansLat1) * Math.Cos(radiansLat2) * Math.Sin(deltaRadiansLon / 2) * Math.Sin(deltaRadiansLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = EarthRadius * c;
            return d;
        }

        private static double DegreesToRadians(float deg)
        {
            return deg * Math.PI / 180;
        }

        private static double RadiansToDegrees(float rad)
        {
            return rad * 180  / Math.PI;
        }


    }
} 