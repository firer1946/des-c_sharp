using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void print_arr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + ' ');
        }
        public static string bin_to_string(char[] arr)
        {
            string str = null;
            for (int i = 0; i < 64; i++)
            {
                str += Convert.ToString(arr[i]);
            }
            string z = Convert.ToString(Convert.ToInt64(str, 2), 16);
            char[] y = z.ToCharArray();
            string res = null;
            string[] recive = new string[y.Length / 2];
            for (int i = 0; i < y.Length - 1; i += 2)
            {
                string str1 = null;
                str1 = Convert.ToString(y[i]) + Convert.ToString(y[i + 1]);
                //recive[i] = str1;
                //Console.Write(str1);
                //Console.WriteLine(Convert.ToString((char)Convert.ToInt64(str1, 16)));
                res += Convert.ToString((char)Convert.ToInt64(str1, 16));


            }
            return res;
        }
        public static string binchar_to_hexstring(char[] arr)
        {
            string str = null;
            for (int i = 0; i < 64; i++)
            {
                str += Convert.ToString(arr[i]);
            }
            string z = Convert.ToString(Convert.ToInt64(str, 2), 16);
            //char[] y = z.ToCharArray();
            //string[] recive = new string[y.Length / 2];
            //for (int i = 0; i < y.Length - 1; i += 2)
            //{
            //    string str1 = null;
            //    str1 = Convert.ToString(y[i]) + Convert.ToString(y[i + 1]);
            //    recive[i] = 


            //}
            return z;
        }
        public static char[] hexstring_to_binchar(string arr)
        {
            string d = Convert.ToString(Int64.Parse(arr, System.Globalization.NumberStyles.HexNumber), 2).PadLeft(64, '0');
            char[] chars = d.ToCharArray();
            return chars;
        }
        public static char[] string_to_binchar(string a)//明文转换为二进制数组
        {

            //byte[] array = System.Text.Encoding.ASCII.GetBytes(Convert.ToString(a));
            char[] array = a.ToCharArray();
            string hexstr = null;
            for (int l = 0; l < array.Length; l++)
                hexstr += Convert.ToString(array[l], 16);
            //Console.Write(hexstr + '\n');
            string d = Convert.ToString(Int64.Parse(hexstr, System.Globalization.NumberStyles.HexNumber), 2).PadLeft(64, '0');
            char[] chars = d.ToCharArray();
            return chars;

        }
        public static char[] ip(char[] arr, int flag) //ip转换,1代表ip置换，2代表ip逆置换
        {

            char[] arr3 = new char[64];


            int x = 65;
            for (int i = 0; i < 32; i++)
            {
                if (x - 8 < 0)
                    x -= 6;
                else
                    x -= 8;
                if (x < 0)
                    x = 64 + x;
                if (x < 64)
                    if (flag == 1)
                        arr3[i] = arr[x];
                    else
                        arr3[x] = arr[i];
            }
            x = 64;
            for (int i = 32; i < 64; i++)
            {
                if (x - 8 < 0)
                    x -= 6;
                else
                    x -= 8;
                if (x < 0)
                    x = 64 + x;
                if (x < 64)
                    if (flag == 1)
                        arr3[i] = arr[x];
                    else
                        arr3[x] = arr[i];
            }
            return arr3;
        }
        public static char[] des_jia(char[] data, char[] keys)//64位明文输入，64位密匙输入，返回64位密文
        {
            char[] tmp1 = ip(data, 1);
            char[,] round_keys = Key.getkeys(keys);
            char[] L0 = new char[32];
            char[] R0 = new char[32];
            for (int i = 0; i < 32; i++)
                L0[i] = tmp1[i];
            for (int i = 32; i < 64; i++)
                R0[i - 32] = tmp1[i];
            char[] round_key = new char[48];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 48; j++)
                {
                    round_key[j] = round_keys[i, j];
                }
                char[] tmp = R0;
                R0 = F.XOR(L0, F.F_function(R0, round_key));
                L0 = tmp;
            }
            for (int j = 0; j < 48; j++)
                round_key[j] = round_keys[15, j];
            L0 = F.XOR(L0, F.F_function(R0, round_key));
            char[] recive = new char[64];
            L0.CopyTo(recive, 0);
            R0.CopyTo(recive, 32);
            return ip(recive, 2);



        }
        public static char[] des_jie(char[] data, char[] keys)//64位密文输入，64位密匙输入，返回64位明文
        {
            char[] tmp1 = ip(data, 1);
            char[,] round_keys = Key.getkeys(keys);
            char[] L0 = new char[32];
            char[] R0 = new char[32];
            for (int i = 0; i < 32; i++)
                L0[i] = tmp1[i];
            for (int i = 32; i < 64; i++)
                R0[i - 32] = tmp1[i];
            char[] round_key = new char[48];
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 48; j++)
                {
                    round_key[j] = round_keys[15 - i, j];
                }
                char[] tmp = R0;
                R0 = F.XOR(L0, F.F_function(R0, round_key));
                L0 = tmp;
            }
            for (int j = 0; j < 48; j++)
                round_key[j] = round_keys[0, j];
            L0 = F.XOR(L0, F.F_function(R0, round_key));
            char[] recive = new char[64];
            L0.CopyTo(recive, 0);
            R0.CopyTo(recive, 32);
            return ip(recive, 2);



        }
        public static string jia_final(string word, string key)
        {
            string a = binchar_to_hexstring(des_jia(string_to_binchar(word), string_to_binchar(key)));
            return a;
        }//输入小于八位的字符，返回16进制字符串
        public static String jie_final(string pwd, string key)
        {
            string c = bin_to_string(des_jie(hexstring_to_binchar(pwd), string_to_binchar(key)));
            return c;
        }//输入字符串，返回明文
        public static string jia_super(string word, string key)
        {
            string keyx = null;
            for (int i = 0; i < 8; i++)
            {
                if (i >= key.Length)
                    break;
                keyx += Convert.ToString(key[i]);

            }
            key = keyx;
            string output = null;
            for (int i = 0; i < word.Length; i += 8)
            {
                string str5 = null;
                for (int j = i; j < i + 8; j++)
                {
                    if (j >= word.Length)
                        break;
                    str5 += word[j];
                }
                output += jia_final(str5, key);
            }
            return output;
        }
        public static string jie_super(string pwd, string key)
        {
            string keyx = null;
            for (int i = 0; i < 8; i++)
            {
                if (i >= key.Length)
                    break;
                keyx += Convert.ToString(key[i]);

            }
            key = keyx;
            string output2 = null;
            for (int i = 0; i < pwd.Length; i += 16)
            {
                string str5 = null;
                for (int j = i; j < i + 16; j++)
                {
                    if (j >= pwd.Length)
                        break;
                    str5 += pwd[j];
                }
                output2 += jie_final(str5, key);
            }
            return output2;

        }
        public static string print()
        {
            return "hello,world";
        }
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //string a = jie_final("e788d7404d0893e2837f0a6063438afce0bb6", "123");
            //string a = jie_final("e788d7404d0893e2", "123");
            //Console.Write(a);
            //Console.Read();
        }
    }
    class Key
    {
        public static char[,] getkeys(char[] arr)//arr为密匙的二进制数组，64位输入，返回二维数组，包含16个密钥
        {
            char[,] keys = new char[16, 48];
            char[] pc_1 = F.exchange(F.pc_1, arr);
            char[] c0 = new char[28];
            char[] d0 = new char[28];
            for (int i = 0; i < 28; i++)
                c0[i] = pc_1[i];
            for (int i = 28; i < 56; i++)
                d0[i - 28] = pc_1[i];
            for (int i = 0; i < F.shift_size.Length; i++)
            {
                char[] c_shift = F.shift(c0, F.shift_size[i]);  //每一轮循环左移后的数组
                char[] d_shift = F.shift(d0, F.shift_size[i]);
                char[] shift = new char[56];
                c_shift.CopyTo(shift, 0);
                c_shift.CopyTo(shift, 28);
                char[] push = F.exchange(F.pc_2, shift);
                for (int j = 0; j < push.Length; j++)
                    keys[i, j] = push[j];
            }
            return keys;
        }
    }
    class F
    {

        public static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
        }
        public static char[] exchange(int[] form, char[] arr1) //form为要替换的置换表，arr1为要输入的字符数组
        {
            char[] recive = new char[form.Length];
            for (int i = 0; i < form.Length; i++)
                recive[i] = arr1[form[i] - 1];
            return recive;

        }
        public static char[] shift(char[] arr, int size)//arr为输入数组，循环位移大小为1或2
        {
            char a, b;
            if (size == 1)
            {
                a = arr[0];
                for (int i = 0; i < arr.Length - 1; i++)
                    arr[i] = arr[i + 1];
                arr[arr.Length - 1] = a;
            }
            if (size == 2)
            {
                a = arr[0];
                b = arr[1];
                for (int i = 0; i < arr.Length - 2; i++)
                    arr[i] = arr[i + 2];
                arr[arr.Length - 2] = a;
                arr[arr.Length - 1] = b;
            }
            return arr;


        }
        public static char[] XOR(char[] arr1, char[] arr2)//异或操作，两个数组长度要一致，返回同长的数组
        {
            char[] recive = new char[arr1.Length];
            for (int i = 0; i < arr1.Length; i++)
            {
                int a = arr1[i] ^ arr2[i];
                recive[i] = Convert.ToChar(a.ToString());
            }
            return recive;
        }
        public static char[] S_box_1(int[] form, char[] arr)//输入一个6位长度的二进制字符数组，返回一个4位长度的数组
        {
            int row;//表示行
            int col;//表示列
            string str2 = null, str1 = null;
            str1 = Convert.ToString(arr[0]) + Convert.ToString(arr[5]);
            row = Convert.ToInt32(str1, 2) + 1;
            for (int i = 1; i < arr.Length - 1; i++)
            {
                str2 += Convert.ToString(arr[i]);
            }
            col = Convert.ToInt32(str2, 2) + 1;
            int location = (row - 1) * 16 + col - 1;
            //Console.Write(location+" "+row + " "+ col);
            string x = Convert.ToString(form[location], 2).PadLeft(4, '0');
            char[] recive = x.ToCharArray();
            return recive;


        }
        public static char[] S_box_all(char[] arr)//输入一个48位长数组，返回一个32位长数组
        {
            char[] sp1 = new char[6];
            char[] recive = new char[32];
            char[] sp2 = new char[6];
            char[] sp3 = new char[6];
            char[] sp4 = new char[6];
            char[] sp5 = new char[6];
            char[] sp6 = new char[6];
            char[] sp7 = new char[6];
            char[] sp8 = new char[6];
            char[] re1 = new char[4];
            //char[] re2 = new char[4];
            //char[] re3 = new char[4];
            //char[] re4 = new char[4];
            //char[] re5 = new char[4];
            //char[] re6 = new char[4];
            //char[] re7 = new char[4];
            //char[] re8 = new char[4];
            for (int i = 0; i < 6; i++)
                sp1[i] = arr[i];
            for (int i = 6; i < 12; i++)
                sp2[i - 6] = arr[i];
            for (int i = 12; i < 18; i++)
                sp3[i - 12] = arr[i];
            for (int i = 18; i < 24; i++)
                sp4[i - 18] = arr[i];
            for (int i = 24; i < 30; i++)
                sp5[i - 24] = arr[i];
            for (int i = 30; i < 36; i++)
                sp6[i - 30] = arr[i];
            for (int i = 36; i < 42; i++)
                sp7[i - 36] = arr[i];
            for (int i = 42; i < 48; i++)
                sp8[i - 42] = arr[i];

            F.S_box_1(F.s1, sp1).CopyTo(recive, 0);
            F.S_box_1(F.s2, sp2).CopyTo(recive, 4);
            F.S_box_1(F.s3, sp3).CopyTo(recive, 8);
            F.S_box_1(F.s4, sp4).CopyTo(recive, 12);
            F.S_box_1(F.s5, sp5).CopyTo(recive, 16);
            F.S_box_1(F.s6, sp6).CopyTo(recive, 20);
            F.S_box_1(F.s7, sp7).CopyTo(recive, 24);
            F.S_box_1(F.s8, sp8).CopyTo(recive, 28);
            return recive;
        }
        public static char[] F_function(char[] data, char[] key)//data为32位数据，key为48位子密匙，返回32位数组
        {
            char[] E = F.exchange(F.pc_3, data);
            char[] xor = F.XOR(E, key);
            char[] sbox = S_box_all(xor);
            char[] pbox = F.exchange(F.p, sbox);
            return pbox;
        }
        public static int[] pc_1 =  {57, 49,  41, 33,  25,  17,  9,
								 1, 58,  50, 42,  34,  26, 18,
								10,  2,  59, 51,  43,  35, 27,
								19, 11,   3, 60,  52,  44, 36,
								63, 55,  47, 39,  31,  23, 15,
								 7, 62,  54, 46,  38,  30, 22,
								14,  6,  61, 53,  45,  37, 29,
								21, 13,   5, 28,  20,  12,  4};
        public static int[] pc_2 =  {14, 17, 11, 24,  1,  5,
								 3, 28, 15,  6, 21, 10,
								23, 19, 12,  4, 26,  8,
								16,  7, 27, 20, 13,  2,
								41, 52, 31, 37, 47, 55,
								30, 40, 51, 45, 33, 48,
								44, 49, 39, 56, 34, 53,
								46, 42, 50, 36, 29, 32};
        public static int[] pc_3 = {32,  1,  2,  3,  4,  5,
							 4,  5,  6,  7,  8,  9,
							 8,  9, 10, 11, 12, 13,
							12, 13, 14, 15, 16, 17,
							16, 17, 18, 19, 20, 21,
							20, 21, 22, 23, 24, 25,
							24, 25, 26, 27, 28, 29,
							28, 29, 30, 31, 32,  1};
        public static int[] s1 =  {14,  4, 13,  1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9,  0,  7,
			 0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8,
			 4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0,
			15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13};
        public static int[] s2 = {15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10,
			 3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5,
			 0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15,
			13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9};
        public static int[] s3 = {10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8,
			13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1,
			13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7,
			 1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12};
        public static int[] s4 = { 7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15,
			13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9,
			10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4,
			 3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14};
        public static int[] s5 = { 2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9,
			14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6,
			 4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14,
			11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3};
        public static int[] s6 =  {12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11,
			10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8,
			 9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6,
			 4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13};

        public static int[] s7 =  { 4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1,
			13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6,
			 1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2,
			 6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12};

        public static int[] s8 = {13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7,
			 1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2,
			 7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8,
			 2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11};

        public static int[] p =   {16,  7, 20, 21,
									29, 12, 28, 17,
									 1, 15, 23, 26,
									 5, 18, 31, 10,
									 2,  8, 24, 14,
									32, 27,  3,  9,
									19, 13, 30,  6,
									22, 11,  4, 25};
        public static int[] shift_size = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

    }
       
}
