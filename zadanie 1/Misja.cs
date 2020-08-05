using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie_1
{
    public static class Misja
    {
        private static int size;
        private static float[] points;
        private static uint counter;
        public static void ReadData()
        {
            StreamReader reader = new StreamReader("in.txt");
            string value = reader.ReadLine();
            size = Int32.Parse(value);
            points = new float[size];
            int i = 0;
            while ((value = reader.ReadLine()) != null)
            {
                String[] s = value.Split(null);
                String g = String.Format("{0},{1}", s[0], s[1]);
                float newpoint = float.Parse(String.Format("{0},{1}", s[0], s[1]));
                points[i] = newpoint;
                i++;
            }
            reader.Close();
        }
        public static void Sort()
        {
            points = MergeSort(points);
        }
        private static float[] Merge(float[] left, float[] right)
        {
            float[] merged = new float[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while (i != left.Length && j != right.Length)
            {
                if (i != left.Length && j != right.Length)
                {
                    if (left[i] <= right[j])
                    {
                        merged[k] = left[i];
                        i++;
                        k++;
                    }
                    else
                    {
                        merged[k] = right[j];
                        j++;
                        k++;
                    }
                }
                counter++;
            }
            while (i < left.Length)
            {
                merged[k] = left[i];
                i++;
                k++;
                counter++;
            }
            while (j < right.Length)
            {
                merged[k] = right[j];
                j++;
                k++;
                counter++;
            }
            return merged;
        }
        private static float[] MergeSort(float[] toSort)
        {
            if (toSort.Length <= 1)
                return toSort;
            int middle = toSort.Length / 2;
            float[] left = new float[middle];
            float[] right = new float[toSort.Length - middle];
            for (int i = 0; i < middle; i++)
            {
                left[i] = toSort[i];
                counter++;
            }
            int g = 0;
            for (int i = middle; i < toSort.Length; i++)
            {
                right[g] = toSort[i];
                counter++;
                g++;
            }
            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }
        public static void WriteData()
        {
            foreach (float p in points)
            {
                Console.WriteLine(p);
            }
        }
        public static int GetResult() //algorytm naiwny n^2
        {
            counter = 0;
            int max = 0;
            for (int i = 0; i < size; i++)
            {
                int temp = 0;
                for (int j = 0; j < size; j++)
                {
                    counter++;
                    if (((points[j] <= 90 + points[i]) && (points[j] >= points[i]))
                            || ((points[j] <= points[i]) && (points[i] - points[j] >= 270)))
                    {
                        temp++;
                    }
                }
                if (temp >= max) max = temp;
            }
            return max;
        }
        public static int GetResultFaster() //algorytm ulepszony 4logn * n
        {
            counter = 0;
            Sort();
            int max = 0;
            int FindLastIndexInArea(float point) /*funkcja wewnętrzna szukająca indeksu dla ostatniej wartości mniejszej niz point+90
                funkcja oparta na algorytmie wyszukiwania binarnego*/
            {
                int FirstIndex = 0;
                int lastIndex = size - 1;
                int middleIndex = (FirstIndex + lastIndex) / 2;
                float value = point + 90; //wartosc konca zakresu dla punktu
                if (point >= 270)
                    value = point - 270; //jesli punkt jest powyzej 270 st bierzemy wartosci z <0,90>
                if (points[0] > value) //sprawdzenie czy najmniejsza wartosc nie jest wieksza niż gorna granica pola, jesli tak to przyjmuje 
                                       //wartosc ostatnia
                {
                    counter++;
                    return size - 1;
                }
                while (!(points[middleIndex] <= value && points[middleIndex + 1] > value) && FirstIndex != middleIndex)
                {
                    if (points[middleIndex] < value)
                    {
                        FirstIndex = middleIndex;
                    }
                    else
                    {
                        lastIndex = middleIndex;
                    }
                    middleIndex = (FirstIndex + lastIndex) / 2;
                    counter++;
                }
                return middleIndex;
            }
            for (int i = 0; i < size; i++)
            {
                int temp;
                int index = FindLastIndexInArea(points[i]);
                if (index < i)
                {
                    temp = size - i + index + 1;
                }
                else temp = FindLastIndexInArea(points[i]) - i + 1;
                if (temp > max) max = temp;
                counter++;
            }
            return max;
        }
        public static uint GetCounter() { return counter; }
        public static int GetSize() { return size; }
    }
}