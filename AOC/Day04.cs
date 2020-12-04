using System;
using System.IO;

namespace AOC
{
    public static class Day04
    {
        public static void AOCDay04()
        {
            var fileName = @".\InputData\AOCDay04.txt";
            //var fileName = @".\InputData\AOCDay04testV.txt";
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);
            var data = streamReader.ReadToEnd().Split(new string[] { Environment.NewLine + Environment.NewLine },
                                                                   StringSplitOptions.RemoveEmptyEntries);
            CycleThoughData(data);
        }

        //byr(Birth Year)
        //iyr(Issue Year)
        //eyr(Expiration Year)
        //hgt(Height)
        //hcl(Hair Color)
        //ecl(Eye Color)
        //pid(Passport ID)
        //cid(Country ID)

        public static void CycleThoughData(string[] passports)
        {
            string[] fields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            var validPass = 0;
            var validPassData = 0;
            foreach (var item in passports)
            {
                //Console.WriteLine(item);
                var matchedFields = 0;

                foreach (var field in fields)
                {
                    if (item.Contains(field))
                    {
                        matchedFields++;
                    }
                }
                if (matchedFields == 7)
                {
                    //Console.WriteLine(item);
                    validPass++;
                    if (ValidateData(item))
                    {
                        validPassData++;
                    }
                }
            }
            Console.WriteLine($"Day 4 Part 1: {validPass}");
            Console.WriteLine($"Day 4 Part 2: {validPassData}");
        }

        public static bool ValidateData(string passport)
        {
            var passportItems = passport.Replace(Environment.NewLine, " ")
                                        .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var validDataCount = 0;

            foreach (var item in passportItems)
            {
                //Console.WriteLine(item);
                var parts = item.Split(':');
                var isINT = int.TryParse(parts[1], out int x);

                if (parts[0] == "byr" && isINT && x >= 1920 && x < 2003)
                {
                    validDataCount++;
                }
                else if (parts[0] == "eyr" && isINT && x >= 2020 && x < 2031)
                {
                    validDataCount++;
                }
                else if (parts[0] == "iyr" && isINT && x >= 2010 && x < 2021)
                {
                    validDataCount++;
                }
                else if (parts[0] == "pid" && isINT && parts[1].Length == 9)
                {
                    validDataCount++;
                }
                else if (parts[0] == "ecl")
                {
                    string[] colors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

                    foreach (var color in colors)
                    {
                        if (parts[1] == color)
                        {
                            validDataCount++;
                        }
                    }
                }
                else if (parts[0] == "hcl" && parts[1][0] == '#')
                {
                    var correct = true;
                    foreach (char c in parts[1].Substring(1))
                    {
                        if (!char.IsLetterOrDigit(c))
                        {
                            correct = false;
                        }
                    }

                    if (correct)
                    {
                        validDataCount++;
                    }
                }
                else if (parts[0] == "hgt" && !isINT)
                {
                    var len = parts[1].Length - 2;
                    var unit = parts[1].Substring(len);
                    var isSubInt = int.TryParse(parts[1].Substring(0, len), out int z);

                    if (isSubInt && (unit == "cm" && z >= 150 && z < 194)
                                 || (unit == "in" && z >= 59 && z < 77))
                    {
                        validDataCount++;
                    }
                }
            }

            if (validDataCount == 7)
            {
                return true;
            }

            return false;
        }

    }
}
