using MathNet.Numerics.LinearAlgebra.Factorization;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace GSC
{
    internal class Program
    {
        private const string GSC_DIRECTORY = "GSC";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var pathToDirectoryToScan = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), GSC_DIRECTORY);

            var list = new List<string>();

            if (Directory.Exists(pathToDirectoryToScan))
            {
                // Użyj metody GetFiles, aby pobrać listę plików z katalogu
                string[] files = Directory.GetFiles(pathToDirectoryToScan);

                // Wyświetl listę plików
                foreach (string file in files)
                    if (file.EndsWith(".xlsx"))
                    {
                        Console.WriteLine(file);
                        var fullPathToFile = Path.Combine(pathToDirectoryToScan, file);
                        var excel = new Excel(fullPathToFile);
                        Console.WriteLine($"{DateTime.Now.DateTimeToString(true)} Rozpoczęto czytanie arkusza Excel ({fullPathToFile})");
                        var sheetName = "Tabela";
                        var excelResult = excel.LoadUrlsFromSheet(sheetName);
                        Console.WriteLine($"{DateTime.Now.DateTimeToString(true)} Zakończono czytanie arkusza Excel ({fullPathToFile})");
                        Console.WriteLine($"{DateTime.Now.DateTimeToString(true)} Wczytano z arkusza Excel ({fullPathToFile}): {excelResult.Count}");
                        list.AddRange(excelResult);
                    }
            }
            else Console.WriteLine($"Podany katalog ({pathToDirectoryToScan}) nie istnieje");
            Console.WriteLine($"{DateTime.Now.DateTimeToString(true)} W sumie wczytano z arkuszy Excel: {list.Count}");
            var distinctList = list.Distinct();
            Console.WriteLine($"{DateTime.Now.DateTimeToString(true)} Po odfiltrowaniu duplikatów w sumie wczytano z arkuszy Excel: {distinctList.Count()}");

            var filePathToSaveUrls = Path.Combine(pathToDirectoryToScan, "out-all.txt");
            File.WriteAllLines(filePathToSaveUrls, distinctList);

            var filePathToSaveUrls2 = Path.Combine(pathToDirectoryToScan, "out-other.txt");
            File.WriteAllLines(filePathToSaveUrls2, distinctList.Where(x => !x.Contains("www.bielbit.pl") && !x.Contains("//bielbit.pl")).OrderBy(x => x));

            var bielbitDomainAndWwwUrls = distinctList.Where(x => x.Contains("www.bielbit.pl") || x.Contains("//bielbit.pl")).OrderBy(x => x);

            var filePathToSaveUrls3 = Path.Combine(pathToDirectoryToScan, "out-www-only.txt");
            File.WriteAllLines(filePathToSaveUrls3, bielbitDomainAndWwwUrls.Where(x => !x.Contains("/pliki/") && !x.Contains("/wp-content/")));

            var filePathToSaveUrls4 = Path.Combine(pathToDirectoryToScan, "out-www-files.txt");
            File.WriteAllLines(filePathToSaveUrls4, bielbitDomainAndWwwUrls.Where(x => x.Contains("/pliki/") || x.Contains("/wp-content/")));

            var finalList = new List<string>();
            foreach (var url in distinctList)
            {
                var client = new RestClient(url);
                var request = new RestRequest();
                RestResponse response = client.ExecuteGet(request);
                //Console.WriteLine($"{url} {response.ResponseUri?.OriginalString} {string.Join(" ", response.ResponseUri?.Segments)} {response.ResponseUri?.Scheme} {response.ResponseUri?.Host} {response.ResponseUri?.AbsolutePath} {response.ResponseUri?.Query} {response.ResponseUri?.PathAndQuery}");
                //Console.WriteLine($"{url} {response.ResponseUri?.Scheme}://{response.ResponseUri?.Host}{response.ResponseUri?.PathAndQuery} {response.ResponseUri?.OriginalString}");
                if (!url.Equals(response.ResponseUri?.OriginalString) && !string.IsNullOrEmpty(response.ResponseUri?.OriginalString))
                {
                    Console.WriteLine($"-Adres URL {url} prowadzi do adresu {response.ResponseUri?.OriginalString}");
                    finalList.Add($"{response.ResponseUri?.OriginalString}");
                }
                else
                {
                    Console.WriteLine($"+Adres URL {url} jest adresem docelowym");
                    finalList.Add($"{url}");
                }
            }

            var filePathToSaveUrls5 = Path.Combine(pathToDirectoryToScan, "out-target.txt");
            File.WriteAllLines(filePathToSaveUrls5, finalList.Distinct());

            //Console.ReadLine();
        }
    }
}