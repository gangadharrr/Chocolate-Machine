using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Chocolate_Machine
{

    public class Dispenser
    {
        public int totalChocolates = 28;
        public Dictionary<string, int> chocolatesColors = new Dictionary<string, int>()
        {
            { "green", 2 },
            { "silver", 1 },
            { "blue",3},
            { "crimson",4},
            { "purple",5},
            { "red",6},
            { "pink",7}
        };
        public void addChocolates(string color, int count)
        {
            chocolatesColors[color] += count;
            totalChocolates += count;
        }
        public void removeChocolateOfColor(string color)
        {
            chocolatesColors[color]--;
            totalChocolates--;
        }
        public int removeChocolates(int count)
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
        public int dispenseChocolates(int count)
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

        public Dictionary<string, int> dispenseChocolatesOfColor(string color, int count)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result[color] = count;
            chocolatesColors[color] -= count;
            totalChocolates -= count;
            return result;

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
            chocolatesColors[color] -= count;
            chocolatesColors[finalColor] += count;
        }
        public Dictionary<string, int> dispenseRainbowChocolates(int count)
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
            return result;
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
            Dispenser dm = new Dispenser();
            var list = dm.sortChocolateBasedOnCount();
            Console.WriteLine($"[{string.Join(", ", list)}]");
            var list1 = dm.dispenseChocolatesOfColor("silver", 2);
            foreach (KeyValuePair<string, int> item in list1)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            dm.addChocolates("silver", 3);
            dm.noOfChocolates();
        }
    }
}