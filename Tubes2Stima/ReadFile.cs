/*Tugas Besar II [IF2211] Strategi Algoritma
Anggota Kelompok :
    Aditya Putra Santosa    / 13517013
    Steve Andreas Immanuel  / 13517039
    Leonardo                / 13517048
Nama File   : ReadFile.cs
Deskripsi   : 2 Prosedur untuk membuka file peta dan file pertanyaan Ferdiant (WiP)
*/
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Tubes2Stima {
    public class ReadFile {
        public static void ReadMap(string filename, ref int Vertexes, ref Graph listOfEdge, ref string strText) {
            try {
                var map_file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                using (var map_file_reader = new StreamReader(map_file, Encoding.UTF8)){
                    Vertexes = int.Parse(map_file_reader.ReadLine());
                    for (int i = 0; i < Vertexes; i++)
                    {
                        listOfEdge.addNode(new Node(i + 1));
                    }
                    string str;
                    while ((str = map_file_reader.ReadLine()) != null){
                        string[] tuple = str.Split(' ');
                        strText += "\r\n" + tuple[0] + " " + tuple[1];
                        int awal = int.Parse(tuple[0])-1;
                        int akhir = int.Parse(tuple[1])-1;
                        listOfEdge.addEdge(awal, akhir);
                    }
                }
            } catch (Exception err) {
                 Console.WriteLine(err);
            }
        }

        public static void ReadCommand(string filename, ref int comnum, ref List<Command> listOfCommand) {
            try {
                var command_file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                using (var command_file_reader = new StreamReader(command_file, Encoding.UTF8)){
                    comnum = int.Parse(command_file_reader.ReadLine());
                    string str;
                    while ((str = command_file_reader.ReadLine()) != null){
                        string[] triple = str.Split(' ');
                        Command C = new Command(int.Parse(triple[0]), int.Parse(triple[1]), int.Parse(triple[2]));
                        listOfCommand.Add(C);
                    }
                }
            } catch (Exception err) {
                Console.WriteLine(err);
            }
        }
    }
}