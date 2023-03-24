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

namespace Chocolate_Machine
{

    public class Dispenser
    {
        public int totalChocolates = 28;
        public Dictionary<string, int> chocolatesColors = new Dictionary<string, int>();
        public Dispenser() {
            chocolatesColors.Add("green", 2);
            chocolatesColors.Add("silver",1);
            chocolatesColors.Add("blue", 3);
            chocolatesColors.Add("crimson", 4);
            chocolatesColors.Add("purple", 5);
            chocolatesColors.Add("red", 6);
            chocolatesColors.Add("pink", 7);
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
            if (count >= totalChocolates)
            {
                int temp = count;
                Dictionary<string, int> result = new Dictionary<string, int>();
                while (count > 0)
                {
                    foreach (KeyValuePair<string, int> item in result)
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
        public int dispenseChocolates(int count)
        { 
            if (count >= totalChocolates)
            {
                int temp = count;
                Dictionary<string, int> result = new Dictionary<string, int>();
                while (count > 0)
                {
                    foreach (KeyValuePair<string, int> item in result)
                    {
                        if (chocolatesColors[item.Key] > 0) { 
                            result[item.Key] = (result.TryGetValue(item.Key, out var value) ? value : 0) + 1;
                            chocolatesColors[item.Key]--;
                            totalChocolates--;
                            count--;
                            if (count == 0) break;
                        }
                    }
                    if (count == 0) break;
                }
                Console.WriteLine($"\nDispensed items are:");
                foreach (KeyValuePair<string, int> item in result)
                {
                    Console.WriteLine($"[{item.Key}, {item.Value}]");
                }
                Console.WriteLine();
                return temp;
            }
            else
            {
               throw new Exception("Less Amount of Chocolate Available Cannot Dispense");
            }
        }
        public Dictionary<string,int> dispenseChocolatesOfColor(string color, int count)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            if (count >= chocolatesColors[color])
            {
                result[color] = count;
                chocolatesColors[color] -= count;
                totalChocolates -= count;
                
                Console.WriteLine($"\nDispensed items are:");
                foreach (KeyValuePair<string, int> item in result)
                {
                    Console.WriteLine($"[{item.Key}, {item.Value}]");
                }
                Console.WriteLine();
                return result;
            }
            else
            {
                throw new Exception("Less Amount of Chocolate Available Cannot Remove");
            }
            
        }
        public void noOfChocolates()
        {
            foreach (KeyValuePair<string, int> item in chocolatesColors)
            {
                Console.WriteLine($"{item.Key},{item.Value}");
            }
        }
        public void changeChocolateColor(int count, string color, string finalColor)
        {
            if (count > chocolatesColors[color])
            {
                chocolatesColors[color] -= count;
                chocolatesColors[finalColor] += count;
                Console.WriteLine($"{count} of {color} Chocolates are Changes to {finalColor} Chocolates");
            }
            else
            { 
                throw new Exception($"Not enough {color} Chocolates are available No Changes are made");
            }
        }
        public Dictionary<string, int> dispenseRainbowChocolates(int count)
        {
            if (count > totalChocolates)
            {
                Dictionary<string, int> result = new Dictionary<string, int>();
                while (count > 0)
                {
                    foreach (KeyValuePair<string, int> item in result)
                    {
                        result[item.Key] = (result.TryGetValue(item.Key, out var value) ? value : 0) + (count > 3 ? 3 : count);
                        chocolatesColors[item.Key] -= (count > 3 ? 3 : count);
                        totalChocolates -= (count > 3 ? 3 : count);
                        count -= (count > 3 ? 3 : count);
                        if (count == 0) break;
                    }
                    if (count == 0) break;
                }
                Console.WriteLine($"\nDispensed items are:");
                foreach (KeyValuePair<string, int> item in result)
                {
                    Console.WriteLine($"[{item.Key}, {item.Value}]");
                }
                Console.WriteLine();
                return result;
            }
            else
            {
                throw new Exception("!!!No enough chocolates!!!");
            } 
            
        }
        public List<string> sortChocolateBasedOnCount()
        {
            var list = new Stack<string>();
            foreach (KeyValuePair<string, int> item in chocolatesColors.OrderBy(x => x.Value))
            {
                list.Push(item.Key);
            }
            return list.ToList();
        }
    }
    public class Program
    {

        static void Main(string[] args)
        {
            try
            {
                Dispenser dm = new Dispenser();
                var list = dm.sortChocolateBasedOnCount();
                Console.WriteLine($"[{string.Join(", ", list)}]");
                var list1 =dm.dispenseChocolates(3);

                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}