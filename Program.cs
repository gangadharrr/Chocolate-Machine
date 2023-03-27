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
using System.Transactions;
using ConsoleApp1;

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
        public void Quesionnare()
        {
            
        }
    }
    
    public class Program:Class1
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
          
            ProtectedClass pc = new ProtectedClass();
            ProtectedInternalClass pic = new ProtectedInternalClass();


        }
    }
}