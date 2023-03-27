using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Transactions;


namespace Chocolate_Machine
{

    public class Dispenser
    {
        public int totalChocolates = 0;
        public Dictionary<string, int> chocolatesColors = new Dictionary<string, int>();
        public Dispenser() {
            chocolatesColors.Add("green", 0);
            chocolatesColors.Add("silver",0);
            chocolatesColors.Add("blue", 0);
            chocolatesColors.Add("crimson", 0);
            chocolatesColors.Add("purple", 0);
            chocolatesColors.Add("red", 0);
            chocolatesColors.Add("pink", 0);
        }
        public void addChocolates(string color, int count)
        {
            chocolatesColors[color] += count;
            totalChocolates += count;
        }
        public void removeChocolateOfColor(string color)
        {
            if (chocolatesColors[color] != 0)
            {
                chocolatesColors[color]--;
                totalChocolates--;
            }
            else
            {
                throw new Exception("Less Amount of Chocolate Available Cannot Dispense");
            }
        }
        public int removeChocolates(int count)
        {
            if (count <= totalChocolates)
            {
                int temp = count;
                Dictionary<string, int> result = new Dictionary<string, int>();
                while (count > 0)
                {
                    foreach (KeyValuePair<string, int> item in chocolatesColors.OrderByDescending(x=>x.Value))
                    {
                        result[item.Key] = (result.TryGetValue(item.Key, out var value) ? value : 0) + 1;
                        this.chocolatesColors[item.Key]--;
                        totalChocolates--;
                        count--;
                        if (count == 0) break;
                    }
                    if (count == 0) break;
                }
                return temp;
            }
            else {
               throw new Exception("Less Amount of Chocolate Available Cannot Remove");
                
            }
        }
        public Dictionary<string, int> dispenseChocolates(int count)
        { 
            if (count <= totalChocolates)
            {
                int temp = count;
                Dictionary<string, int> result = new Dictionary<string, int>();
                while (count > 0 && totalChocolates>0)
                {
                   
                    foreach (var item in chocolatesColors.OrderByDescending(x=>x.Value))
                    { 
                        if (chocolatesColors[item.Key] > 0) { 
                            result[item.Key] = result.GetValueOrDefault(item.Key,0) + 1;
                            chocolatesColors[item.Key]--;
                            totalChocolates--;
                            count--;  

                            if (count == 0) break;
                        }
                    }
                    if (count == 0) break;
                }
                Console.WriteLine($"-------------------");
                Console.WriteLine($"Dispensed items are:");
                foreach (KeyValuePair<string, int> item in result)
                {
                    Console.WriteLine($"[{item.Key}, {item.Value}]");
                }
                Console.WriteLine($"-------------------");
                return result;
            }
            else
            {
               throw new Exception("Less Amount of Chocolate Available Cannot Dispense");
            }
        }
        public Dictionary<string,int> dispenseChocolatesOfColor(string color, int count)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            if (count <=chocolatesColors[color])
            {
                result[color] = count;
                chocolatesColors[color] -= count;
                totalChocolates -= count;
                Console.WriteLine($"-------------------");
                Console.WriteLine($"Dispensed items are:");
                foreach (KeyValuePair<string, int> item in result)
                {
                    Console.WriteLine($"[{item.Key}, {item.Value}]");
                }
                Console.WriteLine($"-------------------");
                return result;
            }
            else
            {
                throw new Exception($"Less Amount of \"{color}\" Chocolate Available Cannot Remove");
            }
            
        }
        public Dictionary<string, int> dispenseRainbowChocolates(int count)
        {
            if (count <= totalChocolates)
            {
                Dictionary<string, int> result = new Dictionary<string, int>();
                while (count > 0)
                {
                    foreach (KeyValuePair<string, int> item in chocolatesColors.OrderByDescending(x => x.Value))
                    {
                        result[item.Key] = (result.TryGetValue(item.Key, out var value) ? value : 0) + (count > 3 ? 3 : count);
                        chocolatesColors[item.Key] -= (count > 3 ? 3 : count);
                        totalChocolates -= (count > 3 ? 3 : count);
                        count -= (count > 3 ? 3 : count);
                        if (count == 0) break;
                    }
                    if (count == 0) break;
                }
                Console.WriteLine($"-------------------");
                Console.WriteLine($"Dispensed items are:");
                foreach (KeyValuePair<string, int> item in result)
                {
                    Console.WriteLine($"[{item.Key}, {item.Value}]");
                }
                Console.WriteLine($"-------------------");
                return result;
            }
            else
            {
                throw new Exception("!!!No enough chocolates!!!");
            }

        }
        public void noOfChocolates()
        {
            Console.WriteLine($"-------------------");
            foreach (KeyValuePair<string, int> item in chocolatesColors)
            {
                Console.WriteLine($"[{item.Key}:  {item.Value}]");
            }
            Console.WriteLine($"-------------------");

        }
        public void changeChocolateColor(int count, string color, string finalColor)
        {
            if (count <= chocolatesColors[color])
            {
                chocolatesColors[color] -= count;
                chocolatesColors[finalColor] += count;
                Console.WriteLine($"\"{count}\" of \"{color}\" Chocolates are Changes to \"{finalColor}\" Chocolates");
            }
            else
            { 
                throw new Exception($"Not enough \"{color}\" Chocolates are available No Changes are made");
            }
        }
         
        public List <string> changeChocolateColorAllOfxCount(string color,string finalColor)
        {
            chocolatesColors[finalColor] = chocolatesColors.GetValueOrDefault(finalColor)+chocolatesColors[color];
            chocolatesColors[color] = 0;
            Console.WriteLine($"{chocolatesColors[finalColor]} of {color} Chocolates are Changes to {finalColor} Chocolates");

            return new List <string>(){
                $"[{finalColor},{chocolatesColors[finalColor]}]", 
                $"[{color},{chocolatesColors[color]}]" 
            };
        }

        public List<string> sortChocolateBasedOnCount()
        {
            var list = new Stack<string>();
            foreach (KeyValuePair<string, int> item in chocolatesColors.OrderBy(x => x.Value))
            {
                list.Push(item.Key);
            }

            Console.WriteLine($"-------------------");
            Console.WriteLine($"Sorted Colors Based on Values : [{string.Join(", ",list)}]");
            Console.WriteLine($"-------------------");

            return list.ToList();
        }
    }

    public class SaraPSY
    {
        Dictionary<string, List<int>> ZodiacSigns = new Dictionary<string, List<int>>()
        {
            { "Aries",new List<int>(){3,21,4,19} },
            { "Taurus",new List<int>(){4,20,5,20} },
            { "Gemini",new List<int>(){5,21,6,21} },
            { "Cancer",new List<int>(){6,22,7,22} },
            { "Leo",new List<int>(){7,23,8,22} },
            { "Virgo",new List<int>(){8,23,9,22} },
            { "Libra",new List<int>(){9,23,10,23} },
            { "Scorpio",new List<int>(){10,24,11,21} },
            { "Sagittarius",new List<int>(){11,22,12,21} },
            { "Capricorn",new List<int>(){12,22,1,19} },
            { "Aquarius",new List<int>(){1,20,2,18} },
            { "Pisces" ,new List<int>(){2,19,3,20}}
        };
        public string ZodiacSignOfPerson;
        Dictionary<string, int> ZodiacSignCalculator = new Dictionary<string, int>();
        Dictionary<string,List<string>> CompatabilitySigns=new Dictionary<string, List<string>>()
        {
            {"Aries",new List<string>(){ "Gemini", "Libra" } },
            { "Taurus",new List<string>(){ "Capricorn", "Virgo", "Cancer" } },
            {"Gemini",new List<string>(){"Libra", "Aquarius", "Sagittarius" } },
            { "Cancer",new List<string>(){ "Virgo", "Scorpio","Pisces" } },
            {"Leo",new List<string>(){"Sagittarius","Aquarius","Gemini" } },
            {"Virgo",new List<string>(){ "Capricorn","Taurus","Scorpio" } },
            {"Libra",new List<string>(){ "Aries","Aquarius" } },
            {"Scorpio",new List<string>(){ "Pisces","Cancer", "Virgo" } },
            {"Sagittarius",new List<string>(){ "Gemini", "Aries" } },
            {"Capricorn",new List<string>(){ "Virgos", "Scorpios" } },
            {"Aquarius",new List<string>(){ "Gemini", "Libra" } },
            {"Pisces",new List<string>(){ "Scorpio","Cancer","Capricorn" } }
        };
       

        public void Quesionnare()
        {
            Console.Write("Do you like to do dangerous stunts? Rate(1-5) :- ");
            ZodiacSignCalculator["Aries"]=Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you Feel like you Are Down To Earth Person? Rate(1-5) :- ");
            ZodiacSignCalculator["Taurus"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you Feel like you are Center Of Attraction? Rate(1-5) :- ");
            ZodiacSignCalculator["Gemini"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you Feel like you can Hold a Secret? Rate(1-5) :- ");
            ZodiacSignCalculator["Cancer"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you like to Start Any Conversation? Rate(1-5) :- ");
            ZodiacSignCalculator["Leo"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you like to Organize Things Perfectly? Rate(1-5) :- ");
            ZodiacSignCalculator["Virgo"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you Feel Bad if Injustice Happen to someone? Rate(1-5) :- ");
            ZodiacSignCalculator["Libra"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you Feel like you would do Anything \n" +
                "for the things you like most? Rate(1-5) :- ");
            ZodiacSignCalculator["Scorpio"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you like to Travel alot? Rate(1-5) :- ");
            ZodiacSignCalculator["Sagittarius"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you like Impression someone with Flattery? Rate(1-5) :- ");
            ZodiacSignCalculator["Capricorn"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you like to take risk of follwing your own path? Rate(1-5) :- ");
            ZodiacSignCalculator["Aquarius"] =Convert.ToInt32(Console.ReadLine());

            Console.Write("Do you Feel making strong relationships with everyone? Rate(1-5) :- ");
            ZodiacSignCalculator["Pisces"] = Convert.ToInt32(Console.ReadLine());

            ZodiacSignOfPerson= ZodiacSignCalculator.OrderByDescending(x => x.Value).ToList()[0].Key;
            Console.WriteLine(ZodiacSignOfPerson);
        }
        public void Compatability()
        {
            Console.WriteLine($"Your Compatible Signs Are :[{string.Join(", ", CompatabilitySigns[ZodiacSignOfPerson])}]");
        }
        public void PredictBirthday()
        {
            Console.WriteLine($"\nYour Birthday will be on: {ZodiacSigns[ZodiacSignOfPerson][0]}/{ZodiacSigns[ZodiacSignOfPerson][1]} - {ZodiacSigns[ZodiacSignOfPerson][2]}/{ZodiacSigns[ZodiacSignOfPerson][3]}");
        }

    }

    public class Program
    {

        void Main()
        {
            try
            {
                Dispenser dm = new Dispenser();
                dm.addChocolates("green", 2);
                dm.addChocolates("silver",1);
                dm.addChocolates("blue", 3);
                dm.addChocolates("crimson", 4);
                dm.addChocolates("purple", 5);
                dm.addChocolates("red", 6);
                dm.addChocolates("pink", 7);
                dm.noOfChocolates();
                Console.WriteLine($"Total No. of chocolates : {dm.totalChocolates}");
                dm.removeChocolates(1);
                dm.noOfChocolates();
                dm.removeChocolateOfColor("blue");
                dm.noOfChocolates();
                dm.dispenseChocolates(3);
                dm.dispenseChocolatesOfColor("crimson", 2);
                dm.dispenseRainbowChocolates(9);
                dm.noOfChocolates();
                dm.changeChocolateColor(2, "pink", "silver");
                dm.noOfChocolates();
                dm.changeChocolateColorAllOfxCount("purple", "skyblue");
                dm.noOfChocolates();
                dm.sortChocolateBasedOnCount();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void Main(String[] args)
        {

            SaraPSY saraPSY = new SaraPSY();
            saraPSY.Quesionnare();
            saraPSY.Compatability();
            saraPSY.PredictBirthday();

        }
    }
}