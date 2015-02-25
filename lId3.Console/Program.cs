using lID3;

namespace lId3.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Id3V2Parser("https://cs7-1v4.vk-cdn.net/p15/ce818944a2b8e2.mp3?extra=wMATG8xFElvjQcPjjituXZ3JrMfpF63SOYZeAX8xMYbqnDISqej4FEPRreqEVXD9LrM_Pot6JtJZqpq_cG4d0SkpV9Ta0BEqICk?/Vanessa%20Paradis%20-%20La%20Seine.mp3");
            var tags = p.GetInfo();
            System.Console.WriteLine(tags.Version);
            System.Console.WriteLine(tags.SongName);
            System.Console.WriteLine(tags.Performer);
            System.Console.WriteLine(tags.Composer);
            System.Console.WriteLine(tags.Year);
            System.Console.WriteLine(tags.Album);
            System.Console.Read();
        }
    }
}
