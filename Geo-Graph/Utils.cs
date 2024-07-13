using Half = SystemHalf.Half;

namespace GeoGraph
{
    public struct Utils
    {
        const int EarthRadius = 6371000;
        public static Half DistanceBetween(Node n1, Node n2)
        {
            return DistanceBetween(n1.Lat, n1.Lon, n2.Lat, n2.Lon);
        }

        public static Half DistanceBetween(Node n1, Half lat2, Half lon2)
        {
            return DistanceBetween(n1.Lat, n1.Lon, lat2, lon2);
        }

        public static Half DistanceBetween(Half lat1, Half lon1, Node n2)
        {
            return DistanceBetween(n2, lat1, lon1);
        }

        public static Half DistanceBetween(Half lat1, Half lon1, Half lat2, Half lon2) //Law of Cosines
        {
            /*
             * From https://www.movable-type.co.uk/scripts/latlong.html
             */
            Half radiansLat1 = DegreesToRadians(lat1);
            Half radiansLat2 = DegreesToRadians(lat2);
            Half deltaRadiansLon = DegreesToRadians(lon2 - lon1);

            double d = Math.Acos(Math.Sin(radiansLat1) * Math.Sin(radiansLat2) + Math.Cos(radiansLat1) * Math.Cos(radiansLat2) * Math.Cos(deltaRadiansLon)) * EarthRadius;

            return (Half)d;
        }

        public static double DistanceBetweenHaversine(Half lat1, Half lon1, Half lat2, Half lon2)
        {
            /*
             * From https://www.movable-type.co.uk/scripts/latlong.html
             */
            Half radiansLat1 = DegreesToRadians(lat1);
            Half radiansLat2 = DegreesToRadians(lat2);
            Half deltaRadiansLat = DegreesToRadians(lat2 - lat1);
            Half deltaRadiansLon = DegreesToRadians(lon2 - lon1);
            double a = Math.Sin(deltaRadiansLat / 2) * Math.Sin(deltaRadiansLat / 2) + Math.Cos(radiansLat1) * Math.Cos(radiansLat2) * Math.Sin(deltaRadiansLon / 2) * Math.Sin(deltaRadiansLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = EarthRadius * c;
            return d;
        }

        private static Half DegreesToRadians(Half deg)
        {
            return deg * new Half(Math.PI) / new Half(180);
        }

        private static Half RadiansToDegrees(Half rad)
        {
            return rad * new Half(180)  / new Half(Math.PI);
        }


    }
} 