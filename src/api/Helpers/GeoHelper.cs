using System;


namespace api.Helpers
{
    public static class GeoHelper
    {
        public static double GetDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // earths radius
            double dLat =ToRad(lat2 - lat1);
            double dLon = ToRad(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        public static double ToRad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}